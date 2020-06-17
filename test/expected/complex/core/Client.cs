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
    public class Client : Darabonba.import.Client
    {
        protected string _protocol;
        protected string _pathname;
        protected Dictionary<string, string> _endpointMap;

        public Client(Darabonba.import.Models.Config config): base(config)
        {
            this._protocol = config.Protocol;
        }

        public Darabonba.import.Models.RuntimeObject Complex1(ComplexRequest request, Darabonba.import.Client client)
        {
            request.Validate();
            client.Validate();
            Dictionary<string, string> runtime_ = new Dictionary<string, string>
            {
                {"timeouted", "retry"},
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
                    string name = "complex";
                    Dictionary<string, string> mapVal = new Dictionary<string, string>
                    {
                        {"test", "ok"},
                    };
                    request_.Protocol = _endpointMap.Get(_protocol);
                    request_.Port = request.Num;
                    request_.Method = "GET";
                    request_.Pathname = "/" + _pathname;
                    request_.Query = new Dictionary<string, string>
                    {
                        {"date", "2019"},
                    };
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    if (true && true)
                    {
                        return null;
                    }
                    else if (true || false)
                    {
                        return new Darabonba.import.Models.RuntimeObject();
                    }
                    client.Print(request.ToMap(), "1");
                    Hello(request.ToMap(), new List<string>
                    {
                        "1",
                        "2"
                    });
                    Hello(null, null);
                    return TeaModel.ToObject<Darabonba.import.Models.RuntimeObject>(new Dictionary<string, object>(){});
                    Complex3(null);
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

        public async Task<Darabonba.import.Models.RuntimeObject> Complex1Async(ComplexRequest request, Darabonba.import.Client client)
        {
            request.Validate();
            client.Validate();
            Dictionary<string, string> runtime_ = new Dictionary<string, string>
            {
                {"timeouted", "retry"},
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
                    string name = "complex";
                    Dictionary<string, string> mapVal = new Dictionary<string, string>
                    {
                        {"test", "ok"},
                    };
                    request_.Protocol = _endpointMap.Get(_protocol);
                    request_.Port = request.Num;
                    request_.Method = "GET";
                    request_.Pathname = "/" + _pathname;
                    request_.Query = new Dictionary<string, string>
                    {
                        {"date", "2019"},
                    };
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    if (true && true)
                    {
                        return null;
                    }
                    else if (true || false)
                    {
                        return new Darabonba.import.Models.RuntimeObject();
                    }
                    client.Print(request.ToMap(), "1");
                    await HelloAsync(request.ToMap(), new List<string>
                    {
                        "1",
                        "2"
                    });
                    await HelloAsync(null, null);
                    return TeaModel.ToObject<Darabonba.import.Models.RuntimeObject>(new Dictionary<string, object>(){});
                    await Complex3Async(null);
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

        public Dictionary<string, object> Complex2(ComplexRequest request, List<string> str, Dictionary<string, string> val)
        {
            request.Validate();
            TeaRequest request_ = new TeaRequest();
            string name = "complex";
            Darabonba.import.Models.Config config = new Darabonba.import.Models.Config();
            Darabonba.import.Client client = new Darabonba.import.Client(config);
            request_.Protocol = "HTTP";
            request_.Port = 80;
            request_.Method = "GET";
            request_.Pathname = "/";
            request_.Query = new Dictionary<string, string>
            {
                {"date", "2019"},
                {"protocol", request_.Protocol},
            };
            TeaResponse response_ = TeaCore.DoAction(request_);

            return;
        }

        public async Task<Dictionary<string, object>> Complex2Async(ComplexRequest request, List<string> str, Dictionary<string, string> val)
        {
            request.Validate();
            TeaRequest request_ = new TeaRequest();
            string name = "complex";
            Darabonba.import.Models.Config config = new Darabonba.import.Models.Config();
            Darabonba.import.Client client = new Darabonba.import.Client(config);
            request_.Protocol = "HTTP";
            request_.Port = 80;
            request_.Method = "GET";
            request_.Pathname = "/";
            request_.Query = new Dictionary<string, string>
            {
                {"date", "2019"},
                {"protocol", request_.Protocol},
            };
            TeaResponse response_ = await TeaCore.DoActionAsync(request_);

            return;
        }

        public ComplexRequest Complex3(ComplexRequest request)
        {
            request.Validate();
            TeaRequest request_ = new TeaRequest();
            string name = "complex";
            request_.Protocol = TemplateString();
            request_.Port = 80;
            request_.Method = "GET";
            request_.Pathname = "/";
            request_.Body = TeaCore.BytesReadable("body");
            request_.Query = new Dictionary<string, string>
            {
                {"date", "2019"},
            };
            TeaResponse response_ = TeaCore.DoAction(request_);

            TeaResponse resp = response_;
            Darabonba.import.Models.Request req = new Darabonba.import.Models.Request
            {
                Accesskey = request.AccessKey,
                Region = resp.StatusMessage,
            };
            Client.Array0(request.ToMap());
            req.Accesskey = "accesskey";
            req.Accesskey = request.AccessKey;
            Client.PrintNull();
            Darabonba.import.Common.Array(request.ToMap(), "1");
            return TeaModel.ToObject<ComplexRequest>(TeaConverter.merge<string>
            (
                request_.Query
            ));
        }

        public async Task<ComplexRequest> Complex3Async(ComplexRequest request)
        {
            request.Validate();
            TeaRequest request_ = new TeaRequest();
            string name = "complex";
            request_.Protocol = await TemplateStringAsync();
            request_.Port = 80;
            request_.Method = "GET";
            request_.Pathname = "/";
            request_.Body = TeaCore.BytesReadable("body");
            request_.Query = new Dictionary<string, string>
            {
                {"date", "2019"},
            };
            TeaResponse response_ = await TeaCore.DoActionAsync(request_);

            TeaResponse resp = response_;
            Darabonba.import.Models.Request req = new Darabonba.import.Models.Request
            {
                Accesskey = request.AccessKey,
                Region = resp.StatusMessage,
            };
            Client.Array0(request.ToMap());
            req.Accesskey = "accesskey";
            req.Accesskey = request.AccessKey;
            await Client.PrintNullAsync();
            Darabonba.import.Common.Array(request.ToMap(), "1");
            return TeaModel.ToObject<ComplexRequest>(TeaConverter.merge<string>
            (
                request_.Query
            ));
        }

        public List<string> Hello(Dictionary<string, object> request, List<string> strs)
        {
            return Client.Array1();
        }

        public async Task<List<string>> HelloAsync(Dictionary<string, object> request, List<string> strs)
        {
            return Client.Array1();
        }

        public static Darabonba.import.Models.Request Print(TeaRequest reqeust, List<ComplexRequest> reqs, TeaResponse response, Dictionary<string, string> val)
        {
        }

        public static async Task<Darabonba.import.Models.Request> PrintAsync(TeaRequest reqeust, List<ComplexRequest> reqs, TeaResponse response, Dictionary<string, string> val)
        {
        }

        public static void PrintNull()
        {
            try
            {
                string str = TemplateString();
            }
            catch(Exception e)
            {
            }
            finally
            {
                string final = "ok";
            }
            try
            {
                string strNoCatch = TemplateString();
            }
            finally
            {
                string finalNoCatch = "ok";
            }
        }

        public static async Task PrintNullAsync()
        {
            try
            {
                string str = await TemplateStringAsync();
            }
            catch(Exception e)
            {
            }
            finally
            {
                string final = "ok";
            }
            try
            {
                string strNoCatch = await TemplateStringAsync();
            }
            finally
            {
                string finalNoCatch = "ok";
            }
        }

        public static List<object> Array0(Dictionary<string, object> req)
        {
            return new List<object>
            {
            };
        }

        public static List<string> Array1()
        {
            return new List<string>
            {
                "1"
            };
        }

        public string TemplateString()
        {
            return "/" + _protocol;
        }

        public async Task<string> TemplateStringAsync()
        {
            return "/" + _protocol;
        }

    }
}
