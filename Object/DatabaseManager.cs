using CommunityToolkit.Maui.Storage;
using MyConfig;


namespace Object.MyDatabase;
public class DatabaseManager
{

	public DatabaseManager() {
        if(!File.Exists(Config.dataDirPath)){
            System.IO.Directory.CreateDirectory(Config.dataDirPath);
        }
	}

    public void CreateDirectory(string path){
        Directory.CreateDirectory(path);
    }

}
