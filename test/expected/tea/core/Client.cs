// This file is auto-generated, don't edit it. Thanks.

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using Tea;
using Tea.Utils;


namespace Darabonba.Test
{
    public class Client 
    {

        public static void Sample(Darabonba.import.Client client)
        {
            Darabonba.import.Models.RuntimeObject runtime = new Darabonba.import.Models.RuntimeObject();
            AlibabaCloud.import.Models.Request request = new AlibabaCloud.import.Models.Request
            {
                Accesskey = "accesskey",
                Region = "region",
            };
            client.Print(runtime);
        }

    }
}
