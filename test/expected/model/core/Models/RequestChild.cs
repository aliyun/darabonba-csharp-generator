// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Darabonba;
using Darabonba.Utils;

namespace Darabonba.Test.Models
{
    public class RequestChild : Darabonba.Request {
        [NameInMap("count")]
        [Validation(Required=true)]
        public int? Count { get; set; }

        public override Dictionary<string, object> ToMapCore(bool noStream = false)
        {
            var map = new Dictionary<string, object>();
            map["protocol"] = this.Protocol;
            map["port"] = this.Port;
            map["method"] = this.Method;
            map["pathname"] = this.Pathname;
            {
                Dictionary<string, object> value0 = null;
                if (this.Query != null)
                {
                    value0 = new Dictionary<string, object>();
                    foreach (var item0 in this.Query)
                    {
                        value0.Add(item0.Key, item0.Value);
                    }
                }
                map["query"] = value0;
            }
            {
                Dictionary<string, object> value0 = null;
                if (this.Headers != null)
                {
                    value0 = new Dictionary<string, object>();
                    foreach (var item0 in this.Headers)
                    {
                        value0.Add(item0.Key, item0.Value);
                    }
                }
                map["headers"] = value0;
            }
            map["count"] = this.Count;
            return map;
        }

        public new static RequestChild FromMap(IDictionary map)
        {
            if (map == null)
            {
                return null;
            }
            var model = new RequestChild();
            if (map.Contains("protocol"))
            {
                model.Protocol = (string)map["protocol"];
            }
            if (map.Contains("port"))
            {
                model.Port = System.Convert.ToInt32(map["port"]);
            }
            if (map.Contains("method"))
            {
                model.Method = (string)map["method"];
            }
            if (map.Contains("pathname"))
            {
                model.Pathname = (string)map["pathname"];
            }
            if (map.Contains("query"))
            {
                Dictionary<string, string> value0 = null;
                if (map["query"] != null)
                {
                    value0 = new Dictionary<string, string>();
                    foreach (DictionaryEntry item0 in (IDictionary)map["query"])
                    {
                        value0.Add((string)item0.Key, (string)item0.Value);
                    }
                }
                model.Query = value0;
            }
            if (map.Contains("headers"))
            {
                Dictionary<string, string> value0 = null;
                if (map["headers"] != null)
                {
                    value0 = new Dictionary<string, string>();
                    foreach (DictionaryEntry item0 in (IDictionary)map["headers"])
                    {
                        value0.Add((string)item0.Key, (string)item0.Value);
                    }
                }
                model.Headers = value0;
            }
            if (map.Contains("body"))
            {
                model.Body = (Stream)map["body"];
            }
            if (map.Contains("count"))
            {
                model.Count = map["count"] == null ? (int?)null : System.Convert.ToInt32(map["count"]);
            }
            return model;
        }
    }

}

