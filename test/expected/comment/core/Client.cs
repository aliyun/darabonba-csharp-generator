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
        public string TestAPI()
        {
            Dictionary<string, object> runtime_ = new Dictionary<string, object>()
            {
                // empty runtime comment
                // another runtime comment
            };

            Darabonba.RetryPolicy.RetryPolicyContext _retryPolicyContext = null;
            Darabonba.Request _lastRequest = null;
            Darabonba.Response _lastResponse = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retriesAttempted = 0;
            _retryPolicyContext = new Darabonba.RetryPolicy.RetryPolicyContext
            {
                RetriesAttempted = _retriesAttempted
            };
            while (Darabonba.Core.ShouldRetry((Darabonba.RetryPolicy.RetryOptions)runtime_["retryOptions"], _retryPolicyContext))
            {
                if (_retriesAttempted > 0)
                {
                    int backoffTime = Darabonba.Core.GetBackoffDelay((Darabonba.RetryPolicy.RetryOptions)runtime_["retryOptions"], _retryPolicyContext);
                    if (backoffTime > 0)
                    {
                        Darabonba.Core.Sleep(backoffTime);
                    }
                }
                try
                {
                    Darabonba.Request request_ = new Darabonba.Request();
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
                    Darabonba.Response response_ = Darabonba.Core.DoAction(request_, runtime_);
                    _lastRequest = request_;
                    _lastResponse = response_;

                    // static async function call
                    TestFunc();
                    // return comment
                    return "test";
                }
                catch (Exception e)
                {
                    _retriesAttempted++;
                    _lastException = e;
                    _retryPolicyContext = new Darabonba.RetryPolicy.RetryPolicyContext
                    {
                        RetriesAttempted = _retriesAttempted,
                        Request = _lastRequest,
                        Response = _lastResponse,
                        Exception = _lastException
                    };
                }
            }

            throw Darabonba.Core.ThrowException(_retryPolicyContext);
        }

        /// <term><b>Description:</b></term>
        /// <description>
        /// <para>testAPI</para>
        /// </description>
        //testAPI comment one
        //testAPI comment two
        public async Task<string> TestAPIAsync()
        {
            Dictionary<string, object> runtime_ = new Dictionary<string, object>()
            {
                // empty runtime comment
                // another runtime comment
            };

            Darabonba.RetryPolicy.RetryPolicyContext _retryPolicyContext = null;
            Darabonba.Request _lastRequest = null;
            Darabonba.Response _lastResponse = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retriesAttempted = 0;
            _retryPolicyContext = new Darabonba.RetryPolicy.RetryPolicyContext
            {
                RetriesAttempted = _retriesAttempted
            };
            while (Darabonba.Core.ShouldRetry((Darabonba.RetryPolicy.RetryOptions)runtime_["retryOptions"], _retryPolicyContext))
            {
                if (_retriesAttempted > 0)
                {
                    int backoffTime = Darabonba.Core.GetBackoffDelay((Darabonba.RetryPolicy.RetryOptions)runtime_["retryOptions"], _retryPolicyContext);
                    if (backoffTime > 0)
                    {
                        Darabonba.Core.Sleep(backoffTime);
                    }
                }
                try
                {
                    Darabonba.Request request_ = new Darabonba.Request();
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
                    Darabonba.Response response_ = await Darabonba.Core.DoActionAsync(request_, runtime_);
                    _lastRequest = request_;
                    _lastResponse = response_;

                    // static async function call
                    await TestFuncAsync();
                    // return comment
                    return "test";
                }
                catch (Exception e)
                {
                    _retriesAttempted++;
                    _lastException = e;
                    _retryPolicyContext = new Darabonba.RetryPolicy.RetryPolicyContext
                    {
                        RetriesAttempted = _retriesAttempted,
                        Request = _lastRequest,
                        Response = _lastResponse,
                        Exception = _lastException
                    };
                }
            }

            throw Darabonba.Core.ThrowException(_retryPolicyContext);
        }

        // testAPI2 comment
        public string TestAPI2()
        {
            Dictionary<string, object> runtime_ = new Dictionary<string, bool?>
            {
                // runtime retry comment
                {"retry", true},
                // runtime back comment one
                // runtime back comment two
            };

            Darabonba.RetryPolicy.RetryPolicyContext _retryPolicyContext = null;
            Darabonba.Request _lastRequest = null;
            Darabonba.Response _lastResponse = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retriesAttempted = 0;
            _retryPolicyContext = new Darabonba.RetryPolicy.RetryPolicyContext
            {
                RetriesAttempted = _retriesAttempted
            };
            while (Darabonba.Core.ShouldRetry((Darabonba.RetryPolicy.RetryOptions)runtime_["retryOptions"], _retryPolicyContext))
            {
                if (_retriesAttempted > 0)
                {
                    int backoffTime = Darabonba.Core.GetBackoffDelay((Darabonba.RetryPolicy.RetryOptions)runtime_["retryOptions"], _retryPolicyContext);
                    if (backoffTime > 0)
                    {
                        Darabonba.Core.Sleep(backoffTime);
                    }
                }
                try
                {
                    Darabonba.Request request_ = new Darabonba.Request();
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
                    Darabonba.Response response_ = Darabonba.Core.DoAction(request_, runtime_);
                    _lastRequest = request_;
                    _lastResponse = response_;

                    // empty return comment
                    return "test";
                }
                catch (Exception e)
                {
                    _retriesAttempted++;
                    _lastException = e;
                    _retryPolicyContext = new Darabonba.RetryPolicy.RetryPolicyContext
                    {
                        RetriesAttempted = _retriesAttempted,
                        Request = _lastRequest,
                        Response = _lastResponse,
                        Exception = _lastException
                    };
                }
            }

            throw Darabonba.Core.ThrowException(_retryPolicyContext);
        }

        // testAPI2 comment
        public async Task<string> TestAPI2Async()
        {
            Dictionary<string, object> runtime_ = new Dictionary<string, bool?>
            {
                // runtime retry comment
                {"retry", true},
                // runtime back comment one
                // runtime back comment two
            };

            Darabonba.RetryPolicy.RetryPolicyContext _retryPolicyContext = null;
            Darabonba.Request _lastRequest = null;
            Darabonba.Response _lastResponse = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retriesAttempted = 0;
            _retryPolicyContext = new Darabonba.RetryPolicy.RetryPolicyContext
            {
                RetriesAttempted = _retriesAttempted
            };
            while (Darabonba.Core.ShouldRetry((Darabonba.RetryPolicy.RetryOptions)runtime_["retryOptions"], _retryPolicyContext))
            {
                if (_retriesAttempted > 0)
                {
                    int backoffTime = Darabonba.Core.GetBackoffDelay((Darabonba.RetryPolicy.RetryOptions)runtime_["retryOptions"], _retryPolicyContext);
                    if (backoffTime > 0)
                    {
                        Darabonba.Core.Sleep(backoffTime);
                    }
                }
                try
                {
                    Darabonba.Request request_ = new Darabonba.Request();
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
                    Darabonba.Response response_ = await Darabonba.Core.DoActionAsync(request_, runtime_);
                    _lastRequest = request_;
                    _lastResponse = response_;

                    // empty return comment
                    return "test";
                }
                catch (Exception e)
                {
                    _retriesAttempted++;
                    _lastException = e;
                    _retryPolicyContext = new Darabonba.RetryPolicy.RetryPolicyContext
                    {
                        RetriesAttempted = _retriesAttempted,
                        Request = _lastRequest,
                        Response = _lastResponse,
                        Exception = _lastException
                    };
                }
            }

            throw Darabonba.Core.ThrowException(_retryPolicyContext);
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
        /// param description1
        /// </param>
        /// <param name="_test">
        /// string param2
        /// </param>
        /// 
        /// <returns>
        /// void
        /// return description1
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
        /// param description1
        /// </param>
        /// <param name="_test">
        /// string param2
        /// </param>
        /// 
        /// <returns>
        /// void
        /// return description1
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
        /// deprecated description 1
        /// deprecated description 2
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
        [Obsolete("this is deprecated, use new xx instead.\ndeprecated description 1\ndeprecated description 2")]
        public static void TestFuncWithAnnotation2(string test, string _test)
        {
            // empty comment1
            // empty comment2
        }

        /// <term><b>Deprecated</b></term>
        /// 
        /// this is deprecated, use new xx instead.
        /// deprecated description 1
        /// deprecated description 2
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
        [Obsolete("this is deprecated, use new xx instead.\ndeprecated description 1\ndeprecated description 2")]
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
        [Obsolete("test is deprecated, use xxx instead.\ndeprecated description1\ndeprecated description2")]
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
        [Obsolete("test is deprecated, use xxx instead.\ndeprecated description1\ndeprecated description2")]
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
        [Obsolete("deprecated test for line break.")]
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
        [Obsolete("deprecated test for line break.")]
        public static async Task LineBreakAnnotationAsync(string test, string _test)
        {
        }

    }
}

