using Realms;

using NET_MAUI_BLE.Models;
using NET_MAUI_BLE.Services;


namespace NET_MAUI_BLE.API;

public static class RealmAPI
{
    public static async Task Add(Realm realm, string recordName, byte[] binaryData)
    {
        await realm.WriteAsync(() =>
        {
            var new_item = new Item_()
            {
                OwnerId = RealmService.CurrentUser.Id,
                RecordName = recordName,
                BinaryData = binaryData
            };

            realm.Add(new_item);
        });
    }

    public static async Task Delete(Realm realm, Item_ item)
    {
        await realm.WriteAsync(() =>
        {
            realm.Remove(item);
        });
    }

    public static IQueryable<Item_> GetAll(Realm realm)
    {
        return realm.All<Item_>().OrderBy(i => i.Id);
    }

}

