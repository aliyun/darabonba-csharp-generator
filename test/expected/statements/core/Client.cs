// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Darabonba.Utils;
using Darabonba.Test.Models;

namespace Darabonba.Test
{
    public class Client 
    {

        public Client(Config config)
        {
        }

        public void Hello()
        {
            Darabonba.Request request_ = new Darabonba.Request();
            request_.Method = "GET";
            request_.Pathname = "/";
            request_.Headers = new Dictionary<string, string>
            {
                {"host", "www.test.com"},
            };
            string host = request_.Headers.Get("host");
            if (true)
            {
                request_.Headers["host"] = "www.test2.com";
            }
            Darabonba.Response response_ = Darabonba.Core.DoAction(request_);

            HelloIf();
            return ;
        }

        public async Task HelloAsync()
        {
            Darabonba.Request request_ = new Darabonba.Request();
            request_.Method = "GET";
            request_.Pathname = "/";
            request_.Headers = new Dictionary<string, string>
            {
                {"host", "www.test.com"},
            };
            string host = request_.Headers.Get("host");
            if (true)
            {
                request_.Headers["host"] = "www.test2.com";
            }
            Darabonba.Response response_ = await Darabonba.Core.DoActionAsync(request_);

            HelloIf();
            return ;
        }

        public static void HelloIf()
        {
            if (true)
            {
            }
            if (true)
            {
            }
            else if (true)
            {
            }
            else
            {
            }
        }

        public static void HelloThrow()
        {
            throw new Darabonba.Exceptions.DaraException(new Dictionary<string, object>(){});
        }

        public static void HelloForBreak()
        {

            foreach (var item in new List<string>
                {
                    "1",
                    "2"
                }) {
                break;
            }
        }

        public static void HelloWhile()
        {

            while (true) {
                break;
            }
        }

        public static void HelloDeclare()
        {
            string hello = "world";
            string helloNull = null;
            hello = "hehe";
        }

    }
}

