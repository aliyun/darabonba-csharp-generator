// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Darabonba.Utils;
using SourceClient = Darabonba.import.Client;
using Darabonba.import.Models;
using Test.import.Models;
using Darabonba.import.Util;

namespace Darabonba.Test
{
    public class Client 
    {

        public static void Sample(SourceClient client)
        {
            RuntimeObject runtime = new RuntimeObject();
            Request request = new Request
            {
                Accesskey = "accesskey",
                Region = "region",
            };
            Dictionary<string, string> test = ApiClient.GetQuery();
            client.Print(runtime);
        }

    }
}

