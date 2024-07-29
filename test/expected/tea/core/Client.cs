// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Darabonba;
using Darabonba.Utils;
using SourceClient = Darabonba.import.Client;
using Darabonba.import.Models;
using AlibabaCloud.import.Models;

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
            client.Print(runtime);
        }

    }
}

