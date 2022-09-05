using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheToolkit
{
    public interface ICacheService
    {
        string GetStringValue(string key);
        void SetStringValue(string key, string value, int? timeOutMinutes);
        void Remove(string key);
        T GetObject<T>(string key);
        void SetObject(string key, object value, int? timeOutMinutes);
    }
}
