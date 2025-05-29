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
    public class Client : Darabonba.import.Client, IClient
    {
        protected string _protocol;
        protected string _pathname;
        protected Dictionary<string, string> _endpointMap;
        protected Darabonba.import.Client _source;
        protected bool? _boolVirtual;
        protected List<Darabonba.import.Models.Config> _configs;

        public Client(Darabonba.import.Models.Config config, string secondParam): base(config, secondParam)
        {
            this._protocol = config.Protocol;
            this._pathname = secondParam;
            this._boolVirtual = true;
            this._configs[0] = config;
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
                    Config conf = new Config
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
                    TeaRequest reqInstance = request_;
                    bool? boolItem = !_boolVirtual;
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
                    return TeaModel.ToObject<Darabonba.import.Models.RuntimeObject>(new Dictionary<string, object>(){});
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
                    Config conf = new Config
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
                    TeaRequest reqInstance = request_;
                    bool? boolItem = !_boolVirtual;
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
                    return TeaModel.ToObject<Darabonba.import.Models.RuntimeObject>(new Dictionary<string, object>(){});
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

        public Dictionary<string, object> Complex2(ComplexRequest request, List<string> str, Dictionary<string, string> val, List<List<List<string>>> complexList)
        {
            request.Validate();
            TeaRequest request_ = new TeaRequest();
            string name = "complex";
            Darabonba.import.Models.Config config = new Darabonba.import.Models.Config();
            Darabonba.import.Client client = new Darabonba.import.Client(config, "testSecond");
            Darabonba.import.Models.Request.RequestSubmodel subModel = new Darabonba.import.Models.Request.RequestSubmodel();
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
            TeaResponse response_ = TeaCore.DoAction(request_);

            return;
        }

        public async Task<Dictionary<string, object>> Complex2Async(ComplexRequest request, List<string> str, Dictionary<string, string> val, List<List<List<string>>> complexList)
        {
            request.Validate();
            TeaRequest request_ = new TeaRequest();
            string name = "complex";
            Darabonba.import.Models.Config config = new Darabonba.import.Models.Config();
            Darabonba.import.Client client = new Darabonba.import.Client(config, "testSecond");
            Darabonba.import.Models.Request.RequestSubmodel subModel = new Darabonba.import.Models.Request.RequestSubmodel();
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

            if (true)
            {
                throw new TeaRetryableException(request_, response_);
            }
            TeaResponse resp = response_;
            Darabonba.import.Models.Request req = new Darabonba.import.Models.Request
            {
                Accesskey = request.AccessKey,
                Region = resp.StatusMessage,
            };
            Array0(request.ToMap());
            req.Accesskey = "accesskey";
            req.Accesskey = request.AccessKey;
            PrintNull(typeof(Config));
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

            if (true)
            {
                throw new TeaRetryableException(request_, response_);
            }
            TeaResponse resp = response_;
            Darabonba.import.Models.Request req = new Darabonba.import.Models.Request
            {
                Accesskey = request.AccessKey,
                Region = resp.StatusMessage,
            };
            Array0(request.ToMap());
            req.Accesskey = "accesskey";
            req.Accesskey = request.AccessKey;
            await PrintNullAsync(typeof(Config));
            Darabonba.import.Common.Array(request.ToMap(), "1");
            return TeaModel.ToObject<ComplexRequest>(TeaConverter.merge<string>
            (
                request_.Query
            ));
        }

        public static void ArrayAssign3(ComplexRequest request, string config)
        {
            request.Configs.Value[0] = config;
            int? i = 0;
            request.Configs.Value[i.Value] = config;
        }

        public static string MapAccess(ComplexRequest request)
        {
            string configInfo = request.Configs.Extra.Get("name");
            return configInfo;
        }

        public static string MapAccess2(Darabonba.import.Models.Request request)
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

        public static void MapAssign(ComplexRequest request, string name)
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

        public static string ArrayAccess3(ComplexRequest request)
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

        public static Darabonba.import.Models.Request Print(TeaRequest reqeust, List<ComplexRequest> reqs, TeaResponse response, Dictionary<string, string> val)
        {
            return new Darabonba.import.Models.Request();
        }

        public static async Task<Darabonba.import.Models.Request> PrintAsync(TeaRequest reqeust, List<ComplexRequest> reqs, TeaResponse response, Dictionary<string, string> val)
        {
            return new Darabonba.import.Models.Request();
        }

        public static void PrintNull(Type cls)
        {
            try
            {
                string str = TemplateString();
            }
            catch (TeaException e)
            {
                string errStr = e.Message;
            }
            catch (Exception _e)
            {
                TeaException e = new TeaException(new Dictionary<string, object>
                {
                    { "message", _e.Message }
                });
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
            catch (TeaException e)
            {
                string errStr = e.Message;
            }
            catch (Exception _e)
            {
                TeaException e = new TeaException(new Dictionary<string, object>
                {
                    { "message", _e.Message }
                });
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
