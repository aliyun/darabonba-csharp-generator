// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Darabonba;
using Darabonba.Utils;
using Darabonba.RetryPolicy;
using Darabonba.Test.Models;
using AlibabaCloud.TeaUtil.Models;
using AlibabaCloud.TeaUtil;

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
        protected RetryOptions _retryOptions;

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
            Darabonba.Response response_ = Core.DoAction(request_);

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
            Darabonba.Response response_ = await Core.DoActionAsync(request_);

            return ;
        }

        public string HelloRuntime(string bodyType, RuntimeOptions runtime)
        {
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"key", Common.DefaultString(runtime.Key, _key)},
                {"cert", Common.DefaultString(runtime.Cert, _cert)},
                {"ca", Common.DefaultString(runtime.Ca, _ca)},
                {"readTimeout", Common.DefaultNumber(runtime.ReadTimeout, _readTimeout)},
                {"connectTimeout", Common.DefaultNumber(runtime.ConnectTimeout, _connectTimeout)},
                {"httpProxy", Common.DefaultString(runtime.HttpProxy, _httpProxy)},
                {"httpsProxy", Common.DefaultString(runtime.HttpsProxy, _httpsProxy)},
                {"noProxy", Common.DefaultString(runtime.NoProxy, _noProxy)},
                {"socks5Proxy", Common.DefaultString(runtime.Socks5Proxy, _socks5Proxy)},
                {"socks5NetWork", Common.DefaultString(runtime.Socks5NetWork, _socks5NetWork)},
                {"maxIdleConns", Common.DefaultNumber(runtime.MaxIdleConns, _maxIdleConns)},
                {"retryOptions", _retryOptions},
                {"ignoreSSL", runtime.IgnoreSSL},
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
                    request_.Method = "GET";
                    request_.Pathname = "/";
                    request_.Headers = new Dictionary<string, string>
                    {
                        {"host", "www.test.com"},
                    };
                    Darabonba.Response response_ = Core.DoAction(request_, runtime_);
                    _lastRequest = request_;
                    _lastResponse = response_;

                    return "test";
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

        public async Task<string> HelloRuntimeAsync(string bodyType, RuntimeOptions runtime)
        {
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"key", Common.DefaultString(runtime.Key, _key)},
                {"cert", Common.DefaultString(runtime.Cert, _cert)},
                {"ca", Common.DefaultString(runtime.Ca, _ca)},
                {"readTimeout", Common.DefaultNumber(runtime.ReadTimeout, _readTimeout)},
                {"connectTimeout", Common.DefaultNumber(runtime.ConnectTimeout, _connectTimeout)},
                {"httpProxy", Common.DefaultString(runtime.HttpProxy, _httpProxy)},
                {"httpsProxy", Common.DefaultString(runtime.HttpsProxy, _httpsProxy)},
                {"noProxy", Common.DefaultString(runtime.NoProxy, _noProxy)},
                {"socks5Proxy", Common.DefaultString(runtime.Socks5Proxy, _socks5Proxy)},
                {"socks5NetWork", Common.DefaultString(runtime.Socks5NetWork, _socks5NetWork)},
                {"maxIdleConns", Common.DefaultNumber(runtime.MaxIdleConns, _maxIdleConns)},
                {"retryOptions", _retryOptions},
                {"ignoreSSL", runtime.IgnoreSSL},
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
                    request_.Method = "GET";
                    request_.Pathname = "/";
                    request_.Headers = new Dictionary<string, string>
                    {
                        {"host", "www.test.com"},
                    };
                    Darabonba.Response response_ = await Core.DoActionAsync(request_, runtime_);
                    _lastRequest = request_;
                    _lastResponse = response_;

                    return "test";
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

        public void HelloVirtualCall(M m)
        {
            Darabonba.Request request_ = new Darabonba.Request();
            request_.Method = "GET";
            request_.Pathname = "/";
            request_.Headers = new Dictionary<string, string>
            {
                {"key", ""},
            };
            Darabonba.Response response_ = Core.DoAction(request_);

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
            Darabonba.Response response_ = await Core.DoActionAsync(request_);

            return ;
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

