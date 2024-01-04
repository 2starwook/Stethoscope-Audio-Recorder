using System.Text;

namespace Object.MyDataConverter;
public class DataConverter
{

	public DataConverter() {
	}

    public byte[] ConvertString2Byte(string data){
        return Encoding.Default.GetBytes(data);
    }

}
