// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Darabonba.Utils;
using Darabonba.Test.Models;
using System.Threading;

namespace Darabonba.Test
{
    public class Client 
    {
        protected string _httpProxy;
        protected string _httpsProxy;
        protected string _socks5Proxy;
        protected string _socks5NetWork;
        protected int? _maxIdleConns;
        protected int? _readTimeout;
        protected int? _connectTimeout;
        protected string _noProxy;
        protected string _key;
        protected string _cert;
        protected string _ca;
        protected Darabonba.RetryPolicy.RetryOptions _retryOptions;
        protected bool? _disableHttp2;

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
            Darabonba.Response response_ = Darabonba.Core.DoAction(request_);

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
            Darabonba.Response response_ = await Darabonba.Core.DoActionAsync(request_);

            return ;
        }

        public string HelloRuntime(string bodyType, Darabonba.Models.RuntimeOptions runtime)
        {
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"key", runtime.Key},
                {"cert", runtime.Cert},
                {"ca", runtime.Ca},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"retryOptions", _retryOptions},
                {"ignoreSSL", runtime.IgnoreSSL},
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
                        Thread.Sleep(backoffTime);
                    }
                }
                try
                {
                    Darabonba.Request request_ = new Darabonba.Request();
                    request_.Method = "GET";
                    request_.Pathname = "/";
                    request_.Headers = new Dictionary<string, string>
                    {
                        {"host", "www.test.com"},
                    };
                    _lastRequest = request_;
                    Darabonba.Response response_ = Darabonba.Core.DoAction(request_, runtime_);
                    _lastResponse = response_;

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

        public async Task<string> HelloRuntimeAsync(string bodyType, Darabonba.Models.RuntimeOptions runtime)
        {
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"key", runtime.Key},
                {"cert", runtime.Cert},
                {"ca", runtime.Ca},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"retryOptions", _retryOptions},
                {"ignoreSSL", runtime.IgnoreSSL},
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
                        await Task.Delay(backoffTime);
                    }
                }
                try
                {
                    Darabonba.Request request_ = new Darabonba.Request();
                    request_.Method = "GET";
                    request_.Pathname = "/";
                    request_.Headers = new Dictionary<string, string>
                    {
                        {"host", "www.test.com"},
                    };
                    _lastRequest = request_;
                    Darabonba.Response response_ = await Darabonba.Core.DoActionAsync(request_, runtime_);
                    _lastResponse = response_;

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

        public void HelloVirtualCall(M m)
        {
            Darabonba.Request request_ = new Darabonba.Request();
            request_.Method = "GET";
            request_.Pathname = "/";
            request_.Headers = new Dictionary<string, string>
            {
                {"key", ""},
            };
            Darabonba.Response response_ = Darabonba.Core.DoAction(request_);

            return ;
        }

        public async Task HelloVirtualCallAsync(M m)
        {
            Darabonba.Request request_ = new Darabonba.Request();
            request_.Method = "GET";
            request_.Pathname = "/";
            request_.Headers = new Dictionary<string, string>
            {
                {"key", ""},
            };
            Darabonba.Response response_ = await Darabonba.Core.DoActionAsync(request_);

            return ;
        }

        public IEnumerable<SSEResponse> CallSSEApi(Darabonba.Models.RuntimeOptions runtime)
        {
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"key", runtime.Key},
                {"cert", runtime.Cert},
                {"ca", runtime.Ca},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"retryOptions", _retryOptions},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            Darabonba.Request request_ = new Darabonba.Request();
            request_.Method = "GET";
            request_.Pathname = "/";
            request_.Headers = new Dictionary<string, string>
            {
                {"host", "www.test.com"},
            };
            Darabonba.Response response_ = Darabonba.Core.DoSSEAction(request_, runtime_);

            if ((response_.StatusCode >= 400) && (response_.StatusCode < 600))
            {
                Dictionary<string, object> err = new Dictionary<string, object>(){};
                if (!response_.Headers.Get("content-type").IsNull() && response_.Headers.Get("content-type") == "text/xml;charset=utf-8")
                {
                    string _str = Darabonba.Utils.StreamUtils.ReadAsString(response_.Body);
                    Dictionary<string, object> respMap = Darabonba.Utils.XmlUtils.ParseXml(_str, null);
                    err = (Dictionary<string, object>)(respMap.Get("Error"));
                }
                else
                {
                    object _res = Darabonba.Utils.StreamUtils.ReadAsJSON(response_.Body);
                    err = (Dictionary<string, object>)(_res);
                }
                err["statusCode"] = response_.StatusCode;
                throw new Darabonba.Exceptions.DaraException(new Dictionary<string, object>
                {
                    {"code", "" + Darabonba.Core.GetDefaultValue(err.Get("Code"), err.Get("code"))},
                    {"message", "code: " + response_.StatusCode + ", " + Darabonba.Core.GetDefaultValue(err.Get("Message"), err.Get("message")) + " request id: " + Darabonba.Core.GetDefaultValue(err.Get("RequestId"), err.Get("requestId"))},
                    {"data", err},
                    {"description", "" + Darabonba.Core.GetDefaultValue(err.Get("Description"), err.Get("description"))},
                    {"accessDeniedDetail", Darabonba.Core.GetDefaultValue(err.Get("AccessDeniedDetail"), err.Get("accessDeniedDetail"))},
                });
            }
            IEnumerable<Darabonba.Models.SSEEvent> events = Darabonba.Utils.StreamUtils.ReadAsSSE(response_.Body);

            foreach (var event_ in events) {
                yield return new SSEResponse
                {
                    StatusCode = response_.StatusCode,
                    Headers = response_.Headers,
                    Event = event_,
                };
            }
        }


        public static object DefaultAny(object inputValue, object defaultValue)
        {
            return inputValue;
        }

        public object Execute(bool? param)
        {
            bool? test = !_disableHttp2;
            if (_disableHttp2.Value)
            {
                return true;
            }
            return DefaultAny(_disableHttp2, false);
        }

        public Vno VnoPayCallBackNotifyEx()
        {
            return null;
        }

        public async Task<Vno> VnoPayCallBackNotifyExAsync()
        {
            return null;
        }

    }
}

