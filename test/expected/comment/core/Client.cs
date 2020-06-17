// This file is auto-generated, don't edit it. Thanks.

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using Tea;
using Tea.Utils;

using Darabonba.Test.Models;

namespace Darabonba.Test
{
    public class Client 
    {
        // type's comment
        protected List<string> _a;

        /**
          Init Func
        */
        // comment between init and annotation
        public Client()
        {
            // string declate comment
            string str = "sss";
            // new model instance comment
            Test1 modelInstance = new Test1
            {
                Test = "test",
                //test declare back comment
                Test2 = "test2",
                //test2 declare back comment
            };
            List<object> array = new List<object>
            {
                // array string comment
                "string",
                // array number comment
                300
                // array back comment
            };
        }

        /**
          testAPI
        */
        //testAPI comment one
        //testAPI comment two
        public void TestAPI()
        {
            Dictionary<string, object> runtime_ = new Dictionary<string, object>()
            {                // empty runtime comment
                // another runtime comment
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    // new model instance comment
                    Test1 modelInstance = new Test1
                    {
                        // test declare front comment
                        Test = "test",
                        // test2 declare front comment
                        Test2 = "test2",
                    };
                    // number declare comment
                    int? num = 123;
                    // static function call comment
                    Client.StaticFunc();
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    // static async function call
                    Client.TestFunc();
                    // return comment
                    return ;
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
          testAPI
        */
        //testAPI comment one
        //testAPI comment two
        public async Task TestAPIAsync()
        {
            Dictionary<string, object> runtime_ = new Dictionary<string, object>()
            {                // empty runtime comment
                // another runtime comment
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    // new model instance comment
                    Test1 modelInstance = new Test1
                    {
                        // test declare front comment
                        Test = "test",
                        // test2 declare front comment
                        Test2 = "test2",
                    };
                    // number declare comment
                    int? num = 123;
                    // static function call comment
                    Client.StaticFunc();
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    // static async function call
                    await Client.TestFuncAsync();
                    // return comment
                    return ;
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        // testAPI2 comment
        public void TestAPI2()
        {
            Dictionary<string, bool?> runtime_ = new Dictionary<string, bool?>
            {
                // runtime retry comment
                {"retry", true},
                // runtime back comment one
                // runtime back comment two
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    // new model instance comment
                    Test3 modelInstance = new Test3();
                    // boolean declare comment
                    bool? bool_ = true;
                    if (bool_)
                    {
                        //empty if
                    }
                    else
                    {
                        //empty else
                    }
                    // api function call comment
                    TestAPI();
                    // back comment
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    // empty return comment
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        // testAPI2 comment
        public async Task TestAPI2Async()
        {
            Dictionary<string, bool?> runtime_ = new Dictionary<string, bool?>
            {
                // runtime retry comment
                {"retry", true},
                // runtime back comment one
                // runtime back comment two
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    // new model instance comment
                    Test3 modelInstance = new Test3();
                    // boolean declare comment
                    bool? bool_ = true;
                    if (bool_)
                    {
                        //empty if
                    }
                    else
                    {
                        //empty else
                    }
                    // api function call comment
                    await TestAPIAsync();
                    // back comment
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    // empty return comment
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        public static void StaticFunc()
        {
            List<object> a = new List<object>
            {
            };
        }

        /**
          testFunc
        */
        public static void TestFunc()
        {
            // empty comment1
            // empty comment2
        }

        /**
          testFunc
        */
        public static async Task TestFuncAsync()
        {
            // empty comment1
            // empty comment2
        }

    }
}
