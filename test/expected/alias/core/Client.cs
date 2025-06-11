// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Darabonba.Utils;
using _ImportClient = Darabonba.ImportClient.Client;
using SourceClient = Darabonba.ImportClientClient.Client;

namespace Darabonba.Test
{
    public class Client 
    {

        public static void EmptyModel()
        {
            Darabonba.ImportClientClient.Models.M m1 = new Darabonba.ImportClientClient.Models.M();
            Darabonba.ImportClient.Models.M m2 = new Darabonba.ImportClient.Models.M();
            _ImportClient.Test(m2);
            SourceClient.Test(m1);
        }

        public static async Task EmptyModelAsync()
        {
            Darabonba.ImportClientClient.Models.M m1 = new Darabonba.ImportClientClient.Models.M();
            Darabonba.ImportClient.Models.M m2 = new Darabonba.ImportClient.Models.M();
            _ImportClient.Test(m2);
            SourceClient.Test(m1);
        }

    }
}

