using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tea;
using Tea.Utils;
using Darabonba.Test.Model.Models;
using Darabonba.Test.Lib;
using Darabonba.Test.Repeat;
using TestCommonClient = Darabonba.Test.Lib.Test.CommonClient;
using AlibabaCloud.TeaUtil;

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


        public IAsyncEnumerable<string> Test3()
        {
            IEnumerable<string> it = UtilClient.Test1();
            IEnumerable<string> it1 = CommonClient.Test2();
            IEnumerable<string> it3 = TestCommonClient.Test3();
            string a = Common.GetNonce();

            foreach (var test in it) {
                yield return test;
            }
        }

        public async IAsyncEnumerable<string> Test3Async()
        {
            IEnumerable<string> it = UtilClient.Test1();
            IEnumerable<string> it1 = CommonClient.Test2();
            IEnumerable<string> it3 = TestCommonClient.Test3();
            string a = Common.GetNonce();

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

