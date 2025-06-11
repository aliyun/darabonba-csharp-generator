using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Darabonba.Utils;
using System.Net.Http;
using System.Threading;
using Newtonsoft.Json;

namespace Darabonba.Test
{
    public class Client 
    {
        protected HttpResponseMessage _vid;

        public Client(HttpResponseMessage test)
        {
            this._vid = test;
        }


        public static void Main(string[] args)
        {
            string str = "";
        }


        public static void JsonTest(List<string> args)
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
            Thread.Sleep(10);
            string ms = Darabonba.Utils.JSONUtils.SerializeObject(m);
            object ma = JsonConvert.DeserializeObject(ms);
            if (WaitForDiskAttached("test").Value)
            {
                Console.WriteLine("disk attached");
            }
        }

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
            string ms = Darabonba.Utils.JSONUtils.SerializeObject(m);
            object ma = JsonConvert.DeserializeObject(ms);
            if (WaitForDiskAttached("test").Value)
            {
                Console.WriteLine("disk attached");
            }
        }

        public static bool? WaitForDiskAttached(string diskId)
        {
            return false;
        }

    }
}

