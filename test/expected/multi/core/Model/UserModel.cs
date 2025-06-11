using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Darabonba.Utils;
using ConsoleClient = AlibabaCloud.TeaConsole.Client;
using AlibabaCloud.TeaUtil;
using Darabonba.Test.Lib;
using CommonCommonClient = Darabonba.Test.Repeat.CommonClient;

namespace Darabonba.Test.Model
{
    public class UserModel 
    {

        public static IAsyncEnumerable<string> Test()
        {
            ConsoleClient.Log("test");
            string a = CommonClient.GetNonce();
            yield return a;
            IEnumerable<string> it = UtilClient.Test1();
            IEnumerable<string> it1 = CommonCommonClient.Test2();

            foreach (var test in it) {
                yield return test;
            }
        }

        public static async IAsyncEnumerable<string> TestAsync()
        {
            ConsoleClient.Log("test");
            string a = CommonClient.GetNonce();
            yield return a;
            IEnumerable<string> it = UtilClient.Test1();
            IEnumerable<string> it1 = CommonCommonClient.Test2();

            foreach (var test in it) {
                yield return test;
            }
        }

    }
}

