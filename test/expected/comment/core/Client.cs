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

        /// <term><b>Description:</b></term>
        /// <description>
        /// <para>Init Func</para>
        /// </description>
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

        /// <term><b>Description:</b></term>
        /// <description>
        /// <para>testAPI</para>
        /// </description>
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
                    StaticFunc();
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    // static async function call
                    TestFunc();
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

        /// <term><b>Description:</b></term>
        /// <description>
        /// <para>testAPI</para>
        /// </description>
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
                    StaticFunc();
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    // static async function call
                    await TestFuncAsync();
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
                    if (bool_.Value)
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
                    if (bool_.Value)
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

        /// <term><b>Description:</b></term>
        /// <description>
        /// <para>testFunc</para>
        /// </description>
        public static void TestFunc()
        {
            // empty comment1
            // empty comment2
        }

        /// <term><b>Description:</b></term>
        /// <description>
        /// <para>testFunc</para>
        /// </description>
        public static async Task TestFuncAsync()
        {
            // empty comment1
            // empty comment2
        }

        /// <term><b>Summary:</b></term>
        /// <summary>
        /// <para>annotation test summary</para>
        /// </summary>
        /// 
        /// <term><b>Description:</b></term>
        /// <description>
        /// <para>annotation test description</para>
        /// <list type="bullet">
        /// <item><description>description1 test for md2Xml</description></item>
        /// <item><description>description2 test for md2Xml</description></item>
        /// </list>
        /// </description>
        /// 
        /// <param name="test">
        /// string param1
        /// </param>
        /// <param name="_test">
        /// string param2
        /// </param>
        /// 
        /// <returns>
        /// void
        /// </returns>
        /// 
        /// <term><b>Exception:</b></term>
        /// InternalError Server error. 500 服务器端出现未知异常。
        /// 
        /// <term><b>Exception:</b></term>
        /// StackNotFound The Stack (%(stack_name)s) could not be found.  404 资源栈不存在。
        public static void TestFuncWithAnnotation1(string test, string _test)
        {
            // empty comment1
            // empty comment2
        }

        /// <term><b>Summary:</b></term>
        /// <summary>
        /// <para>annotation test summary</para>
        /// </summary>
        /// 
        /// <term><b>Description:</b></term>
        /// <description>
        /// <para>annotation test description</para>
        /// <list type="bullet">
        /// <item><description>description1 test for md2Xml</description></item>
        /// <item><description>description2 test for md2Xml</description></item>
        /// </list>
        /// </description>
        /// 
        /// <param name="test">
        /// string param1
        /// </param>
        /// <param name="_test">
        /// string param2
        /// </param>
        /// 
        /// <returns>
        /// void
        /// </returns>
        /// 
        /// <term><b>Exception:</b></term>
        /// InternalError Server error. 500 服务器端出现未知异常。
        /// 
        /// <term><b>Exception:</b></term>
        /// StackNotFound The Stack (%(stack_name)s) could not be found.  404 资源栈不存在。
        public static async Task TestFuncWithAnnotation1Async(string test, string _test)
        {
            // empty comment1
            // empty comment2
        }

        /// <term><b>Deprecated</b></term>
        /// 
        /// this is deprecated, use new xx instead.
        /// 
        /// <term><b>Summary:</b></term>
        /// <summary>
        /// <para>annotation test summary</para>
        /// </summary>
        /// 
        /// <param name="test">
        /// string param1
        /// </param>
        /// <param name="_test">
        /// string param2
        /// </param>
        /// 
        /// <returns>
        /// void
        /// </returns>
        /// 
        /// <term><b>Exception:</b></term>
        /// InternalError Server error. 500 服务器端出现未知异常。
        /// 
        /// <term><b>Exception:</b></term>
        /// StackNotFound The Stack (%(stack_name)s) could not be found.  404 资源栈不存在。
        [Obsolete("this is deprecated, use new xx instead.\n")]
        public static void TestFuncWithAnnotation2(string test, string _test)
        {
            // empty comment1
            // empty comment2
        }

        /// <term><b>Deprecated</b></term>
        /// 
        /// this is deprecated, use new xx instead.
        /// 
        /// <term><b>Summary:</b></term>
        /// <summary>
        /// <para>annotation test summary</para>
        /// </summary>
        /// 
        /// <param name="test">
        /// string param1
        /// </param>
        /// <param name="_test">
        /// string param2
        /// </param>
        /// 
        /// <returns>
        /// void
        /// </returns>
        /// 
        /// <term><b>Exception:</b></term>
        /// InternalError Server error. 500 服务器端出现未知异常。
        /// 
        /// <term><b>Exception:</b></term>
        /// StackNotFound The Stack (%(stack_name)s) could not be found.  404 资源栈不存在。
        [Obsolete("this is deprecated, use new xx instead.\n")]
        public static async Task TestFuncWithAnnotation2Async(string test, string _test)
        {
            // empty comment1
            // empty comment2
        }

        /// <term><b>Deprecated</b></term>
        /// 
        /// test is deprecated, use xxx instead.
        /// deprecated description1
        /// deprecated description2
        /// 
        /// <term><b>Summary:</b></term>
        /// <summary>
        /// <para>annotation test summary
        /// summary description1
        /// summary description2</para>
        /// </summary>
        /// 
        /// <param name="test">
        /// string param1
        /// </param>
        /// <param name="_test">
        /// string param2
        /// </param>
        /// 
        /// <returns>
        /// void
        /// </returns>
        /// 
        /// <term><b>Exception:</b></term>
        /// InternalError Server error. 500 服务器端出现未知异常。
        [Obsolete("test is deprecated, use xxx instead.\ndeprecated description1\ndeprecated description2\n")]
        public static void MultiLineAnnotation(string test, string _test)
        {
        }

        /// <term><b>Deprecated</b></term>
        /// 
        /// test is deprecated, use xxx instead.
        /// deprecated description1
        /// deprecated description2
        /// 
        /// <term><b>Summary:</b></term>
        /// <summary>
        /// <para>annotation test summary
        /// summary description1
        /// summary description2</para>
        /// </summary>
        /// 
        /// <param name="test">
        /// string param1
        /// </param>
        /// <param name="_test">
        /// string param2
        /// </param>
        /// 
        /// <returns>
        /// void
        /// </returns>
        /// 
        /// <term><b>Exception:</b></term>
        /// InternalError Server error. 500 服务器端出现未知异常。
        [Obsolete("test is deprecated, use xxx instead.\ndeprecated description1\ndeprecated description2\n")]
        public static async Task MultiLineAnnotationAsync(string test, string _test)
        {
        }

        /// <term><b>Deprecated</b></term>
        /// 
        /// deprecated test for line break.
        /// 
        /// <param name="test">
        /// string param1
        /// param test for line break.
        /// </param>
        /// <param name="_test">
        /// string param2
        /// </param>
        /// 
        /// <returns>
        /// void
        /// return test for line break.
        /// </returns>
        /// 
        /// <term><b>Exception:</b></term>
        /// InternalError Server error. 500 服务器端出现未知异常。
        /// throws test for line break.
        [Obsolete("deprecated test for line break.\n")]
        public static void LineBreakAnnotation(string test, string _test)
        {
        }

        /// <term><b>Deprecated</b></term>
        /// 
        /// deprecated test for line break.
        /// 
        /// <param name="test">
        /// string param1
        /// param test for line break.
        /// </param>
        /// <param name="_test">
        /// string param2
        /// </param>
        /// 
        /// <returns>
        /// void
        /// return test for line break.
        /// </returns>
        /// 
        /// <term><b>Exception:</b></term>
        /// InternalError Server error. 500 服务器端出现未知异常。
        /// throws test for line break.
        [Obsolete("deprecated test for line break.\n")]
        public static async Task LineBreakAnnotationAsync(string test, string _test)
        {
        }

    }
}
