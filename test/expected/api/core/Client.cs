// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tea;
using Tea.Utils;
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
            TeaRequest request_ = new TeaRequest();
            request_.Method = "GET";
            request_.Pathname = "/";
            request_.Headers = new Dictionary<string, string>
            {
                {"host", "www.test.com"},
            };
            TeaResponse response_ = TeaCore.DoAction(request_);

            return ;
        }

        public async Task HelloAsync()
        {
            TeaRequest request_ = new TeaRequest();
            request_.Method = "GET";
            request_.Pathname = "/";
            request_.Headers = new Dictionary<string, string>
            {
                {"host", "www.test.com"},
            };
            TeaResponse response_ = await TeaCore.DoActionAsync(request_);

            return ;
        }

        public string HelloRuntime()
        {
            Dictionary<string, object> runtime_ = new Dictionary<string, object>(){};

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
                    request_.Method = "GET";
                    request_.Pathname = "/";
                    request_.Headers = new Dictionary<string, string>
                    {
                        {"host", "www.test.com"},
                    };
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    return "test";
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

        public async Task<string> HelloRuntimeAsync()
        {
            Dictionary<string, object> runtime_ = new Dictionary<string, object>(){};

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
                    request_.Method = "GET";
                    request_.Pathname = "/";
                    request_.Headers = new Dictionary<string, string>
                    {
                        {"host", "www.test.com"},
                    };
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    return "test";
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

        public void HelloVirtualCall(M m)
        {
            m.Validate();
            TeaRequest request_ = new TeaRequest();
            request_.Method = "GET";
            request_.Pathname = "/";
            request_.Headers = new Dictionary<string, string>
            {
                {"key", ""},
            };
            TeaResponse response_ = TeaCore.DoAction(request_);

            return ;
        }

        public async Task HelloVirtualCallAsync(M m)
        {
            m.Validate();
            TeaRequest request_ = new TeaRequest();
            request_.Method = "GET";
            request_.Pathname = "/";
            request_.Headers = new Dictionary<string, string>
            {
                {"key", ""},
            };
            TeaResponse response_ = await TeaCore.DoActionAsync(request_);

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

