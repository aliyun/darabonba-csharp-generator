using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Darabonba;
using Darabonba.Utils;
using Darabonba.Test.Lib;
using Tea;

namespace Darabonba.Test
{
    public class ApiClient 
    {

        public ApiClient()
        {
        }

        public int? Test3()
        {
            Darabonba.Models.RuntimeOptions runtime_ = new Dictionary<string, string>
            {
                {"timeouted", "retry"},
            };

            RetryPolicyContext retryPolicyContext = null;
            DaraRequest _lastRequest = null;
            DaraResponse _lastResponse = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retriesAttempted = 0;
            retryPolicyContext = new RetryPolicyContext
            {
                RetriesAttempted = _retriesAttempted
            };
            while (DaraCore.ShouldRetry(runtime_["retryOptions"], retryPolicyContext))
            {
                if (_retriesAttempted > 0)
                {
                    int backoffTime = DaraCore.GetBackoffDelay(runtime_["retryOptions"], retryPolicyContext);
                    if (backoffTime > 0)
                    {
                        DaraCore.Sleep(backoffTime);
                    }
                }
                _retriesAttempted = _retriesAttempted + 1;
                try
                {
                    DaraRequest request_ = new DaraRequest();
                    request_.Protocol = "https";
                    request_.Method = "DELETE";
                    request_.Pathname = "/";
                    request_.Headers = new Dictionary<string, string>
                    {
                        {"host", "test.aliyun.com"},
                        {"accept", "application/json"},
                    };
                    request_.Query = UtilClient.GetQuery();
                    _lastRequest = request_;
                    DaraResponse response_ = DaraCore.DoAction(request_, runtime_);

                    return response_.StatusCode;
                }
                catch (Exception e)
                {
                    _retriesAttempted++;
                    retryPolicyContext = new RetryPolicyContext
                    {
                        RetriesAttempted = _retriesAttempted,
                        Request = _lastRequest,
                        Response = _lastResponse,
                        Exception = _lastException
                    };
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        public async Task<int?> Test3Async()
        {
            Darabonba.Models.RuntimeOptions runtime_ = new Dictionary<string, string>
            {
                {"timeouted", "retry"},
            };

            RetryPolicyContext retryPolicyContext = null;
            DaraRequest _lastRequest = null;
            DaraResponse _lastResponse = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retriesAttempted = 0;
            retryPolicyContext = new RetryPolicyContext
            {
                RetriesAttempted = _retriesAttempted
            };
            while (DaraCore.ShouldRetry(runtime_["retryOptions"], retryPolicyContext))
            {
                if (_retriesAttempted > 0)
                {
                    int backoffTime = DaraCore.GetBackoffDelay(runtime_["retryOptions"], retryPolicyContext);
                    if (backoffTime > 0)
                    {
                        DaraCore.Sleep(backoffTime);
                    }
                }
                _retriesAttempted = _retriesAttempted + 1;
                try
                {
                    DaraRequest request_ = new DaraRequest();
                    request_.Protocol = "https";
                    request_.Method = "DELETE";
                    request_.Pathname = "/";
                    request_.Headers = new Dictionary<string, string>
                    {
                        {"host", "test.aliyun.com"},
                        {"accept", "application/json"},
                    };
                    request_.Query = UtilClient.GetQuery();
                    _lastRequest = request_;
                    DaraResponse response_ = await DaraCore.DoActionAsync(request_, runtime_);

                    return response_.StatusCode;
                }
                catch (Exception e)
                {
                    _retriesAttempted++;
                    retryPolicyContext = new RetryPolicyContext
                    {
                        RetriesAttempted = _retriesAttempted,
                        Request = _lastRequest,
                        Response = _lastResponse,
                        Exception = _lastException
                    };
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

    }
}

