using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tea;
using Tea.Utils;
using Newtonsoft.Json;

namespace Darabonba.Test
{
    public class Client 
    {


        public static async Task JsonTestAsync(List<string> args)
        {
            Dictionary<string, object> m = new Dictionary<string, object>
            {
                {"key1", "test1"},
                {"key2", "test2"},
                {"key3", 3},
                {"key4", new Dictionary<string, object>
                {
                    {"key5", 123},
                    {"key6", "321"},
                }},
            };
            await Task.Delay(10);
            string ms = Common.ToJSONString(m);
            object ma = JsonConvert.DeserializeObject(ms);
            string arrStr = "[1,2,3,4]";
            object arr = JsonConvert.DeserializeObject(arrStr);
        }

        public static object ReturnAny()
        {
            throw new NotImplementedException();
        }


        public static async Task Main(string[] args)
        {
            ReturnAny();
            await JsonTestAsync(args);
            int? a = (int)args[0] + 10;
        }

    }
}

