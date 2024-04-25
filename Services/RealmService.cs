using System.Text.Json;
using Realms;
using Realms.Sync;

using NET_MAUI_BLE.Models;
using System.Diagnostics;

namespace NET_MAUI_BLE.Services;

public static class RealmService
{
    private static bool serviceInitialised;

    private static Realms.Sync.App app;

    private static Realm mainThreadRealm;

    public static User CurrentUser;

    public static string DataExplorerLink;

    public static async Task Init()
    {
        if (serviceInitialised)
        {
            return;
        }

        using Stream fileStream = await FileSystem.Current.OpenAppPackageFileAsync("atlasConfig.json");
        using StreamReader reader = new(fileStream);
        var fileContent = await reader.ReadToEndAsync();

        var config = JsonSerializer.Deserialize<RealmAppConfig>(fileContent,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        var appConfiguration = new AppConfiguration(config.AppId)
        {
            BaseUri = new Uri(config.BaseUrl)
        };

        app = Realms.Sync.App.Create(appConfiguration);

        CurrentUser = await app.LogInAsync(Credentials.Anonymous());

        serviceInitialised = true;

        DataExplorerLink = config.DataExplorerLink;
        Console.WriteLine($"To view your data in Atlas, use this link: {DataExplorerLink}");
    }

    public static Realm GetMainThreadRealm()
    {
        return mainThreadRealm ??= GetRealm();
    }

    public static Realm GetRealm()
    {
        try
        {
            var config = new FlexibleSyncConfiguration(app.CurrentUser)
            {
                PopulateInitialSubscriptions = (realm) =>
                {
                    var (query, queryName) = GetQueryForSubscriptionType(realm, SubscriptionType.Mine);
                    realm.Subscriptions.Add(query, new SubscriptionOptions { Name = queryName });
                }
            };

            return Realm.GetInstance(config);
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
        }
        return null;
    }

    public static async Task SetSubscription(Realm realm, SubscriptionType subType)
    {
        if (GetCurrentSubscriptionType(realm) == subType)
        {
            return;
        }

        realm.Subscriptions.Update(() =>
        {
            realm.Subscriptions.RemoveAll(true);

            var (query, queryName) = GetQueryForSubscriptionType(realm, subType);

            realm.Subscriptions.Add(query, new SubscriptionOptions { Name = queryName });
        });

        //There is no need to wait for synchronization if we are disconnected
        if (realm.SyncSession.ConnectionState != ConnectionState.Disconnected)
        {
            await realm.Subscriptions.WaitForSynchronizationAsync();
        }
    }

    public static SubscriptionType GetCurrentSubscriptionType(Realm realm)
    {
        var activeSubscription = realm.Subscriptions.FirstOrDefault();

        return activeSubscription.Name switch
        {
            "all" => SubscriptionType.All,
            "mine" => SubscriptionType.Mine,
            _ => throw new InvalidOperationException("Unknown subscription type")
        };
    }

    private static (IQueryable<Item_> Query, string Name) GetQueryForSubscriptionType(Realm realm, SubscriptionType subType)
    {
        IQueryable<Item_> query = null;
        string queryName = null;

        if (subType == SubscriptionType.Mine)
        {
            query = realm.All<Item_>().Where(i => i.OwnerId == CurrentUser.Id);
            queryName = "mine";
        }
        else if (subType == SubscriptionType.All)
        {
            query = realm.All<Item_>();
            queryName = "all";
        }
        else
        {
            throw new ArgumentException("Unknown subscription type");
        }

        return (query, queryName);
    }
}

public enum SubscriptionType
{
    Mine,
    All,
}

public class RealmAppConfig
{
    public string AppId { get; set; }

    public string BaseUrl { get; set; }

    public string DataExplorerLink { get; set; }
}