// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tea;
using Tea.Utils;
using ImportClient = Darabonba.Import.Client;
using SourceClient = Darabonba.Source.Client;

namespace Darabonba.Test
{
    public class Client 
    {

        public static void EmptyModel()
        {
            Darabonba.Source.Models.M m1 = new Darabonba.Source.Models.M();
            Darabonba.Import.Models.M m2 = new Darabonba.Import.Models.M();
            ImportClient.Test(m2);
            SourceClient.Test(m1);
        }

        public static async Task EmptyModelAsync()
        {
            Darabonba.Source.Models.M m1 = new Darabonba.Source.Models.M();
            Darabonba.Import.Models.M m2 = new Darabonba.Import.Models.M();
            ImportClient.Test(m2);
            SourceClient.Test(m1);
        }

    }
}

