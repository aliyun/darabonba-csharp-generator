// This file is auto-generated, don't edit it. Thanks.

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using Tea;
using Tea.Utils;

using Darabonba.import;
using Darabonba.import.Models;
using AlibabaCloud.import;
using AlibabaCloud.import.Models;

namespace Darabonba.Test
{
    public class Client 
    {

        public static void Sample(Darabonba.import.Client client)
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
