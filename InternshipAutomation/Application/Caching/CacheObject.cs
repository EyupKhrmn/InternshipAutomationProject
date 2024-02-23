using System.Text;
using Newtonsoft.Json;

namespace InternshipAutomation.Application.Caching;

public class CacheObject
{
    public async Task<byte[]> SerializeObject(object obj)
    {
        var jsonData = JsonConvert.SerializeObject(obj);
        return Encoding.UTF8.GetBytes(jsonData);
    }
    
    public async Task<T> DeserializeObject<T>(byte[] byteData)
    {
        var jsonData = Encoding.UTF8.GetString(byteData);
        return JsonConvert.DeserializeObject<T>(jsonData);
    }
}