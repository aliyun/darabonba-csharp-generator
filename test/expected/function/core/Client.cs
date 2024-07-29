// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Darabonba;
using Darabonba.Utils;

namespace Darabonba.Test
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
            return ConverterUtil.Merge<string>
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
            bool? x = false;
            bool? y = true;
            bool? z = false;
            if (x && y || !z)
            {
            }
        }

        public static async Task HelloParamsAsync(string a, string b)
        {
            bool? x = false;
            bool? y = true;
            bool? z = false;
            if (x && y || !z)
            {
            }
        }

        // interface mode
        public static void HelloInterface()
        {
            throw new NotImplementedException();
        }

        // interface mode
        public static async Task HelloInterfaceAsync()
        {
            throw new NotImplementedException();
        }

    }
}

