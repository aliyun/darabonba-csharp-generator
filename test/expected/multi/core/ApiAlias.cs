using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Darabonba;
using Darabonba.Utils;
using Darabonba.RetryPolicy;
using Darabonba.Test.Lib;
using ConsoleClient = AlibabaCloud.TeaConsole.Client;

namespace Darabonba.Test
{
    public class ApiAlias 
    {

        public ApiAlias()
        {
        }

        public int? Test3()
        {
            Dictionary<string, object> runtime_ = new Dictionary<string, string>
            {
                {"timeouted", "retry"},
            };

            RetryPolicyContext _retryPolicyContext = null;
            Darabonba.Request _lastRequest = null;
            Darabonba.Response _lastResponse = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retriesAttempted = 0;
            _retryPolicyContext = new RetryPolicyContext
            {
                RetriesAttempted = _retriesAttempted
            };
            while (Core.ShouldRetry((RetryOptions)runtime_["retryOptions"], _retryPolicyContext))
            {
                if (_retriesAttempted > 0)
                {
                    int backoffTime = Core.GetBackoffDelay((RetryOptions)runtime_["retryOptions"], _retryPolicyContext);
                    if (backoffTime > 0)
                    {
                        Core.Sleep(backoffTime);
                    }
                }
                try
                {
                    Darabonba.Request request_ = new Darabonba.Request();
                    request_.Protocol = "https";
                    request_.Method = "DELETE";
                    request_.Pathname = "/";
                    request_.Headers = new Dictionary<string, string>
                    {
                        {"host", "test.aliyun.com"},
                        {"accept", "application/json"},
                    };
                    request_.Query = UtilClient.GetQuery();
                    ConsoleClient.Log("test");
                    Darabonba.Response response_ = Core.DoAction(request_, runtime_);
                    _lastRequest = request_;
                    _lastResponse = response_;

                    return response_.StatusCode;
                }
                catch (Exception e)
                {
                    _retriesAttempted++;
                    _lastException = e;
                    _retryPolicyContext = new RetryPolicyContext
                    {
                        RetriesAttempted = _retriesAttempted,
                        Request = _lastRequest,
                        Response = _lastResponse,
                        Exception = _lastException
                    };
                }
            }

            throw Core.ThrowException(_retryPolicyContext);
        }

        public async Task<int?> Test3Async()
        {
            Dictionary<string, object> runtime_ = new Dictionary<string, string>
            {
                {"timeouted", "retry"},
            };

            RetryPolicyContext _retryPolicyContext = null;
            Darabonba.Request _lastRequest = null;
            Darabonba.Response _lastResponse = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retriesAttempted = 0;
            _retryPolicyContext = new RetryPolicyContext
            {
                RetriesAttempted = _retriesAttempted
            };
            while (Core.ShouldRetry((RetryOptions)runtime_["retryOptions"], _retryPolicyContext))
            {
                if (_retriesAttempted > 0)
                {
                    int backoffTime = Core.GetBackoffDelay((RetryOptions)runtime_["retryOptions"], _retryPolicyContext);
                    if (backoffTime > 0)
                    {
                        Core.Sleep(backoffTime);
                    }
                }
                try
                {
                    Darabonba.Request request_ = new Darabonba.Request();
                    request_.Protocol = "https";
                    request_.Method = "DELETE";
                    request_.Pathname = "/";
                    request_.Headers = new Dictionary<string, string>
                    {
                        {"host", "test.aliyun.com"},
                        {"accept", "application/json"},
                    };
                    request_.Query = UtilClient.GetQuery();
                    ConsoleClient.Log("test");
                    Darabonba.Response response_ = await Core.DoActionAsync(request_, runtime_);
                    _lastRequest = request_;
                    _lastResponse = response_;

                    return response_.StatusCode;
                }
                catch (Exception e)
                {
                    _retriesAttempted++;
                    _lastException = e;
                    _retryPolicyContext = new RetryPolicyContext
                    {
                        RetriesAttempted = _retriesAttempted,
                        Request = _lastRequest,
                        Response = _lastResponse,
                        Exception = _lastException
                    };
                }
            }

            throw Core.ThrowException(_retryPolicyContext);
        }

    }
}

