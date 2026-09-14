// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Darabonba;
using Darabonba.Utils;

namespace Darabonba.Test.Models
{
    public class Config : Darabonba.Model {
        [NameInMap("protocol")]
        [Validation(Required=true, MaxLength=50, Pattern="pattern")]
        public string Protocol { get; set; }

        [NameInMap("importConfig")]
        [Validation(Required=true)]
        public Darabonba.import.Models.Config ImportConfig { get; set; }

        [NameInMap("query")]
        [Validation(Required=true)]
        public string Query { get; set; }

        [NameInMap("complexList")]
        [Validation(Required=true)]
        public List<List<string>> ComplexList { get; set; }

        [NameInMap("floatNum")]
        [Validation(Required=true)]
        public float? FloatNum { get; set; }

        [NameInMap("longNum")]
        [Validation(Required=true)]
        public long? LongNum { get; set; }

        public override Dictionary<string, object> ToMapCore(bool noStream = false)
        {
            var map = new Dictionary<string, object>();
            map["protocol"] = this.Protocol;
            map["importConfig"] = this.ImportConfig == null ? null : this.ImportConfig.ToMap();
            map["query"] = this.Query;
            {
                List<object> value0 = null;
                if (this.ComplexList != null)
                {
                    value0 = new List<object>();
                    foreach (var item0 in this.ComplexList)
                    {
                        List<object> value1 = null;
                        if (item0 != null)
                        {
                            value1 = new List<object>();
                            foreach (var item1 in item0)
                            {
                                value1.Add(item1);
                            }
                        }
                        value0.Add(value1);
                    }
                }
                map["complexList"] = value0;
            }
            map["floatNum"] = this.FloatNum;
            map["longNum"] = this.LongNum;
            return map;
        }

        public new static Config FromMap(IDictionary map)
        {
            if (map == null)
            {
                return null;
            }
            var model = new Config();
            if (map.Contains("protocol"))
            {
                model.Protocol = (string)map["protocol"];
            }
            if (map.Contains("importConfig"))
            {
                model.ImportConfig = map["importConfig"] == null ? null : Darabonba.import.Models.Config.FromMap((IDictionary)map["importConfig"]);
            }
            if (map.Contains("query"))
            {
                model.Query = (string)map["query"];
            }
            if (map.Contains("complexList"))
            {
                List<List<string>> value0 = null;
                if (map["complexList"] != null)
                {
                    value0 = new List<List<string>>();
                    foreach (var item0 in (IList)map["complexList"])
                    {
                        List<string> value1 = null;
                        if (item0 != null)
                        {
                            value1 = new List<string>();
                            foreach (var item1 in (IList)item0)
                            {
                                value1.Add((string)item1);
                            }
                        }
                        value0.Add(value1);
                    }
                }
                model.ComplexList = value0;
            }
            if (map.Contains("floatNum"))
            {
                model.FloatNum = map["floatNum"] == null ? (float?)null : System.Convert.ToSingle(map["floatNum"]);
            }
            if (map.Contains("longNum"))
            {
                model.LongNum = map["longNum"] == null ? (long?)null : System.Convert.ToInt64(map["longNum"]);
            }
            return model;
        }
    }

}

