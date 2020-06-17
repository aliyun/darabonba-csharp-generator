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

        public static void Sample(AlibabaCloud.import.Client client)
        {
            AlibabaCloud.import.Models.RuntimeObject runtime = new AlibabaCloud.import.Models.RuntimeObject();
            AlibabaCloud.import.Models.Request request = new AlibabaCloud.import.Models.Request
            {
                Accesskey = "accesskey",
                Region = "region",
            };
            client.Print(runtime);
        }

    }
}
