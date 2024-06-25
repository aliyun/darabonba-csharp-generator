using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tea;
using Tea.Utils;
using AlibabaCloud.TeaUtil;
using Darabonba.Test.Lib;
using Darabonba.Test.Repeat;

namespace Darabonba.Test.Model
{
    public class UserModel 
    {

        public static IAsyncEnumerable<string> Test()
        {
            string a = Common.GetNonce();
            yield return a;
            IEnumerable<string> it = UtilClient.Test1();
            IEnumerable<string> it1 = CommonClient.Test2();

            foreach (var test in it) {
                yield return test;
            }
        }

        public static async IAsyncEnumerable<string> TestAsync()
        {
            string a = Common.GetNonce();
            yield return a;
            IEnumerable<string> it = UtilClient.Test1();
            IEnumerable<string> it1 = CommonClient.Test2();

            foreach (var test in it) {
                yield return test;
            }
        }

    }
}

