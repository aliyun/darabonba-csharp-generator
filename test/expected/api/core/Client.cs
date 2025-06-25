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
                        Darabonba.Core.Sleep(backoffTime);
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
                    Darabonba.Response response_ = Darabonba.Core.DoAction(request_, runtime_);
                    _lastRequest = request_;
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
                        Darabonba.Core.Sleep(backoffTime);
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
                    Darabonba.Response response_ = await Darabonba.Core.DoActionAsync(request_, runtime_);
                    _lastRequest = request_;
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

