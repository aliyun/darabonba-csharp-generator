// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Darabonba.Utils;
using SourceClient = Darabonba.import.Client;
using Darabonba.import.Models;
using System.Threading;
using Darabonba.Test.Models;

namespace Darabonba.Test
{
    public class Client : SourceClient, IClient
    {
        protected string _protocol;
        protected string _pathname;
        protected Dictionary<string, string> _endpointMap;
        protected SourceClient _source;
        protected bool? _boolVirtual;
        protected List<Darabonba.import.Models.Config> _configs;

        public Client(Darabonba.import.Models.Config config, string secondParam): base(config, secondParam)
        {
            this._protocol = config.Protocol;
            this._pathname = secondParam;
            this._boolVirtual = true;
            this._configs[0] = config;
        }

        public RuntimeObject Complex1(Darabonba.Test.Models.ComplexRequest request, SourceClient client)
        {
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"retry", new Dictionary<string, string>
                {
                    {"retryable", "xxx"},
                }},
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
                    string name = "complex";
                    Darabonba.Test.Models.Config conf = new Darabonba.Test.Models.Config
                    {
                        FloatNum = 0.1f,
                    };
                    conf.FloatNum = 1.1f;
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
                    Darabonba.Request reqInstance = request_;
                    bool? boolItem = !_boolVirtual;
                    _lastRequest = request_;
                    Darabonba.Response response_ = Darabonba.Core.DoAction(request_, runtime_);
                    _lastResponse = response_;

                    if (true && true)
                    {
                        return null;
                    }
                    else if (true || false)
                    {
                        return new RuntimeObject();
                    }
                    else
                    {
                        return null;
                    }
                    client.Print(request.ToMap(), "1");
                    Hello(request.ToMap(), new List<string>
                    {
                        "1",
                        "2"
                    });
                    Hello(null, null);
                    Complex3(null);
                    return Darabonba.Model.ToObject<RuntimeObject>(new Dictionary<string, object>(){});
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

        public async Task<RuntimeObject> Complex1Async(Darabonba.Test.Models.ComplexRequest request, SourceClient client)
        {
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"retry", new Dictionary<string, string>
                {
                    {"retryable", "xxx"},
                }},
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
                    string name = "complex";
                    Darabonba.Test.Models.Config conf = new Darabonba.Test.Models.Config
                    {
                        FloatNum = 0.1f,
                    };
                    conf.FloatNum = 1.1f;
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
                    Darabonba.Request reqInstance = request_;
                    bool? boolItem = !_boolVirtual;
                    _lastRequest = request_;
                    Darabonba.Response response_ = await Darabonba.Core.DoActionAsync(request_, runtime_);
                    _lastResponse = response_;

                    if (true && true)
                    {
                        return null;
                    }
                    else if (true || false)
                    {
                        return new RuntimeObject();
                    }
                    else
                    {
                        return null;
                    }
                    client.Print(request.ToMap(), "1");
                    await HelloAsync(request.ToMap(), new List<string>
                    {
                        "1",
                        "2"
                    });
                    await HelloAsync(null, null);
                    await Complex3Async(null);
                    return Darabonba.Model.ToObject<RuntimeObject>(new Dictionary<string, object>(){});
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

        public Dictionary<string, object> Complex2(Darabonba.Test.Models.ComplexRequest request, List<string> str, Dictionary<string, string> val, List<List<List<string>>> complexList)
        {
            Darabonba.Request request_ = new Darabonba.Request();
            string name = "complex";
            Darabonba.import.Models.Config config = new Darabonba.import.Models.Config();
            SourceClient client = new SourceClient(config, "testSecond");
            Request.RequestSubmodel subModel = new Request.RequestSubmodel();
            List<List<List<List<string>>>> nestingList = new List<List<List<List<string>>>>
            {
                new List<List<List<string>>>
                {
                    new List<List<string>>
                    {
                        new List<string>
                        {
                            "test"
                        }
                    }
                }
            };
            request_.Protocol = "HTTP";
            request_.Port = 80;
            request_.Method = "GET";
            request_.Pathname = "/";
            request_.Query = new Dictionary<string, string>
            {
                {"date", "2019"},
                {"protocol", request_.Protocol},
            };
            return new Dictionary<string, object>(){};
            Darabonba.Response response_ = Darabonba.Core.DoAction(request_);

            return;
        }

        public async Task<Dictionary<string, object>> Complex2Async(Darabonba.Test.Models.ComplexRequest request, List<string> str, Dictionary<string, string> val, List<List<List<string>>> complexList)
        {
            Darabonba.Request request_ = new Darabonba.Request();
            string name = "complex";
            Darabonba.import.Models.Config config = new Darabonba.import.Models.Config();
            SourceClient client = new SourceClient(config, "testSecond");
            Request.RequestSubmodel subModel = new Request.RequestSubmodel();
            List<List<List<List<string>>>> nestingList = new List<List<List<List<string>>>>
            {
                new List<List<List<string>>>
                {
                    new List<List<string>>
                    {
                        new List<string>
                        {
                            "test"
                        }
                    }
                }
            };
            request_.Protocol = "HTTP";
            request_.Port = 80;
            request_.Method = "GET";
            request_.Pathname = "/";
            request_.Query = new Dictionary<string, string>
            {
                {"date", "2019"},
                {"protocol", request_.Protocol},
            };
            return new Dictionary<string, object>(){};
            Darabonba.Response response_ = await Darabonba.Core.DoActionAsync(request_);

            return;
        }

        public Darabonba.Test.Models.ComplexRequest Complex3(Darabonba.Test.Models.ComplexRequest request)
        {
            Darabonba.Request request_ = new Darabonba.Request();
            string name = "complex";
            request_.Protocol = TemplateString();
            request_.Port = 80;
            request_.Method = "GET";
            request_.Pathname = "/";
            request_.Body = Darabonba.Utils.StreamUtils.BytesReadable("body");
            request_.Query = new Dictionary<string, string>
            {
                {"date", "2019"},
            };
            Darabonba.Response response_ = Darabonba.Core.DoAction(request_);

            if (true)
            {
                throw new DaraRetryableException(request_, response_);
            }
            Darabonba.Response resp = response_;
            Request req = new Request
            {
                Accesskey = request.AccessKey,
                Region = resp.StatusMessage,
            };
            Array0(request.ToMap());
            req.Accesskey = "accesskey";
            req.Accesskey = request.AccessKey;
            PrintNull(typeof(Config));
            SourceClient.Array(request.ToMap(), "1");
            return Darabonba.Model.ToObject<Darabonba.Test.Models.ComplexRequest>(Darabonba.Utils.ConverterUtils.Merge<string>
            (
                request_.Query
            ));
        }

        public async Task<Darabonba.Test.Models.ComplexRequest> Complex3Async(Darabonba.Test.Models.ComplexRequest request)
        {
            Darabonba.Request request_ = new Darabonba.Request();
            string name = "complex";
            request_.Protocol = await TemplateStringAsync();
            request_.Port = 80;
            request_.Method = "GET";
            request_.Pathname = "/";
            request_.Body = Darabonba.Utils.StreamUtils.BytesReadable("body");
            request_.Query = new Dictionary<string, string>
            {
                {"date", "2019"},
            };
            Darabonba.Response response_ = await Darabonba.Core.DoActionAsync(request_);

            if (true)
            {
                throw new DaraRetryableException(request_, response_);
            }
            Darabonba.Response resp = response_;
            Request req = new Request
            {
                Accesskey = request.AccessKey,
                Region = resp.StatusMessage,
            };
            Array0(request.ToMap());
            req.Accesskey = "accesskey";
            req.Accesskey = request.AccessKey;
            await PrintNullAsync(typeof(Config));
            SourceClient.Array(request.ToMap(), "1");
            return Darabonba.Model.ToObject<Darabonba.Test.Models.ComplexRequest>(Darabonba.Utils.ConverterUtils.Merge<string>
            (
                request_.Query
            ));
        }

        public static void ArrayAssign3(Darabonba.Test.Models.ComplexRequest request, string config)
        {
            request.Configs.Value[0] = config;
            int? i = 0;
            request.Configs.Value[i.Value] = config;
        }

        public static string MapAccess(Darabonba.Test.Models.ComplexRequest request)
        {
            string configInfo = request.Configs.Extra.Get("name");
            return configInfo;
        }

        public static string MapAccess2(Request request)
        {
            string configInfo = request.Configs.Extra.Get("name");
            return configInfo;
        }

        public static string MapAccess3()
        {
            Dictionary<string, Dictionary<string, Dictionary<string, string>>> data = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>
            {
                {"mapAcc", new Dictionary<string, Dictionary<string, string>>
                {
                    {"map2", new Dictionary<string, string>
                    {
                        {"value", "string"},
                    }},
                }},
            };
            return data["mapAcc"]["map2"].Get("value");
        }

        public static void MapAssign(Darabonba.Test.Models.ComplexRequest request, string name)
        {
            request.Configs.Extra["name"] = name;
            Dictionary<string, object> data = new Dictionary<string, object>(){};
            data["header"] = null;
            request.Dict = new Dictionary<string, object>
            {
                {"test", "demo"},
            };
        }

        public static List<string> ArrayAssign2(string config)
        {
            Dictionary<string, List<string>> data = new Dictionary<string, List<string>>
            {
                {"configs", new List<string>
                {
                    "a",
                    "b",
                    "c"
                }},
            };
            data["configs"][3] = config;
            int? i = 3;
            data["configs"][i.Value] = config;
            return data.Get("configs");
        }

        public static List<string> ArrayAssign(string config)
        {
            List<string> configs = new List<string>
            {
                "a",
                "b",
                "c"
            };
            configs[3] = config;
            int? i = 3;
            configs[i.Value] = config;
            int? i32 = 3;
            configs[i32.Value] = config;
            long? i64 = 3;
            configs[i64.Value] = config;
            int? num = 3;
            configs[num.Value] = config;
            return configs;
        }

        public static string ArrayAccess3(Darabonba.Test.Models.ComplexRequest request)
        {
            string configVal = request.Configs.Value[0];
            int? i = 0;
            configVal = request.Configs.Value[i.Value];
            return configVal;
        }

        public static string ArrayAccess2()
        {
            Dictionary<string, List<string>> data = new Dictionary<string, List<string>>
            {
                {"configs", new List<string>
                {
                    "a",
                    "b",
                    "c"
                }},
            };
            string config = data["configs"][0];
            int? i = 0;
            i++;
            ++i;
            i--;
            --i;
            config = data["configs"][i.Value];
            return config;
        }

        public static string ArrayAccess()
        {
            List<string> configs = new List<string>
            {
                "a",
                "b",
                "c"
            };
            string config = configs[0];
            int? i = 0;
            config = configs[i.Value];
            int? i32 = 3;
            config = configs[i32.Value];
            long? i64 = 3;
            config = configs[i64.Value];
            int? num = 3;
            config = configs[num.Value];
            return config;
        }

        public List<string> Hello(Dictionary<string, object> request, List<string> strs)
        {
            return Array1();
        }

        public async Task<List<string>> HelloAsync(Dictionary<string, object> request, List<string> strs)
        {
            return Array1();
        }

        public static Request Print(Darabonba.Request reqeust, List<Darabonba.Test.Models.ComplexRequest> reqs, Darabonba.Response response, Dictionary<string, string> val)
        {
            return new Request();
        }

        public static async Task<Request> PrintAsync(Darabonba.Request reqeust, List<Darabonba.Test.Models.ComplexRequest> reqs, Darabonba.Response response, Dictionary<string, string> val)
        {
            return new Request();
        }

        public static void PrintNull(Type cls)
        {
            try
            {
                string str = TemplateString();
            }
            catch (Darabonba.Exceptions.DaraException e)
            {
                string errStr = e.Message;
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

        public static async Task PrintNullAsync(Type cls)
        {
            try
            {
                string str = await TemplateStringAsync();
            }
            catch (Darabonba.Exceptions.DaraException e)
            {
                string errStr = e.Message;
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
            Darabonba.import.Models.Config temp = new Darabonba.import.Models.Config();
            List<Darabonba.import.Models.Config> anyArr = new List<Darabonba.import.Models.Config>
            {
                temp
            };
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
            return "" + _protocol;
        }

        public async Task<string> TemplateStringAsync()
        {
            return "" + _protocol;
        }

        public static Dictionary<string, object> ReturnObj(string params_)
        {
            params_ = "test";
            return new Dictionary<string, object>(){};
        }

    }
}

