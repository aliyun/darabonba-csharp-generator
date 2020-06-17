// This file is auto-generated, don't edit it. Thanks.

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using Tea;
using Tea.Utils;


namespace AlibabaCloud.Test
{
    public class Client 
    {

        public static void Hello()
        {
            return ;
        }

        public static Dictionary<string, string> HelloMap()
        {
            Dictionary<string, string> m = new Dictionary<string, string>(){};
            return TeaConverter.merge<string>
            (
                new Dictionary<string, string>()
                {
                    {"key", "value"},
                    {"key-1", "value-1"},
                },
                m
            );
        }

        public static List<Dictionary<string, string>> HelloArrayMap()
        {
            return new List<Dictionary<string, string>>
            {
                new Dictionary<string, string>
                {
                    {"key", "value"},
                }
            };
        }

        public static void HelloParams(string a, string b)
        {
            return ;
        }

        public static async Task HelloParamsAsync(string a, string b)
        {
            return ;
        }

        public static void Main(string[] args)
        {
            return ;
        }


    }
}
