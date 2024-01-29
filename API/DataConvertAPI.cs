using System.Text;

namespace MYAPI;
static public class DataConvertAPI
{
    static public byte[] ConvertString2Byte(string data){
        return Encoding.Default.GetBytes(data);
    }


}
