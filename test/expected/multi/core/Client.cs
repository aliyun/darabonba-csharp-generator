using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tea;
using Tea.Utils;
using Darabonba.Test.Model.Models;
using System.Linq;
using TeaTeaString = Tea.TeaString;
using Darabonba.Test.Repeat;
using Darabonba.Test.Lib;
using TestCommonClient = Darabonba.Test.Lib.Test.CommonClient;
using DARAUtilCommonClient = AlibabaCloud.TeaUtil.CommonClient;

namespace Darabonba.Test
{
    public class Client 
    {
        protected Info _user;

        public Client()
        {
            this._user = new Info
            {
                Name = "test",
            };
        }


        public static void StringTest(List<string> args)
        {
            string fullStr = string.Join(",", args);
            args = fullStr.Split(",").ToList();
            if ((fullStr.Length > 0) && fullStr.Contains("hangzhou"))
            {
                string newStr1 = TeaTeaString.Replace(fullStr, "/hangzhou/g", "beijing");
            }
            TeaString.TestRepeatBultin();
        }

        public static async Task StringTestAsync(List<string> args)
        {
            string fullStr = string.Join(",", args);
            args = fullStr.Split(",").ToList();
            if ((fullStr.Length > 0) && fullStr.Contains("hangzhou"))
            {
                string newStr1 = TeaTeaString.Replace(fullStr, "/hangzhou/g", "beijing");
            }
            TeaString.TestRepeatBultin();
        }

        public IAsyncEnumerable<string> Test3()
        {
            IEnumerable<string> it = UtilClient.Test1();
            IEnumerable<string> it1 = CommonClient.Test2();
            IEnumerable<string> it3 = TestCommonClient.Test3();
            string a = DARAUtilCommonClient.GetNonce();

            foreach (var test in it) {
                yield return test;
            }
        }

        public async IAsyncEnumerable<string> Test3Async()
        {
            IEnumerable<string> it = UtilClient.Test1();
            IEnumerable<string> it1 = CommonClient.Test2();
            IEnumerable<string> it3 = TestCommonClient.Test3();
            string a = DARAUtilCommonClient.GetNonce();

            foreach (var test in it) {
                yield return test;
            }
        }

        public int? Test4()
        {
            ApiClient api = new ApiClient();
            int? status = api.Test3();
            return status;
        }

        public async Task<int?> Test4Async()
        {
            ApiClient api = new ApiClient();
            int? status = await api.Test3Async();
            return status;
        }

    }
}

