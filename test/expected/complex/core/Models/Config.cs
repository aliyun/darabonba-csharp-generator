// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Darabonba;

namespace Darabonba.Test.Models
{
    public class Config : DaraModel {
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

        public Config Copy()
        {
            Config copy = FromMap(ToMap());
            return copy;
        }

        public Config CopyWithoutStream()
        {
            Config copy = FromMap(ToMap(true));
            return copy;
        }

        public Dictionary<string, object> ToMap(bool noStream = false)
        {
            var map = new Dictionary<string, object>();
            if (Protocol != null)
            {
                map["protocol"] = Protocol;
            }

            if (ImportConfig != null)
            {
                map["importConfig"] = ImportConfig != null ? ImportConfig.ToMap(noStream) : null;
            }

            if (Query != null)
            {
                map["query"] = Query;
            }

            if (ComplexList != null)
            {
                var list1 = new List<List<string>>();
                int n1 = 0;
                foreach (var item1 in ComplexList) 
                {
                    var list2 = new List<string>();
                    int n2 = 0;
                    foreach (var item2 in item1) 
                    {
                        list2[n2++] = item2;
                    }
                    list1[n1++] = list2;
                }
                map["complexList"] = list1;
            }

            if (FloatNum != null)
            {
                map["floatNum"] = FloatNum;
            }

            if (LongNum != null)
            {
                map["longNum"] = LongNum;
            }

            return map;
        }

        public static Config FromMap(Dictionary<string, object> map)
        {
            var model = new Config();
            if (map.ContainsKey("protocol"))
            {
                model.Protocol = (string)map["protocol"];
            }

            if (map.ContainsKey("importConfig"))
            {
                model.ImportConfig = Darabonba.import.Models.Config.FromMap(map["importConfig"]);
            }

            if (map.ContainsKey("query"))
            {
                model.Query = (string)map["query"];
            }

            if (map.ContainsKey("complexList"))
            {
                var list1 = map["complexList"] as List<List<string>>;
                if (list1 != null && list1.Count > 0)
                {
                    int n1 = 0;
                    var modelList1 = new List<List<string>>();
                    foreach(var item1 in list1)
                    {
                        var list2 = item1;
                        if (list2 != null && list2.Count > 0)
                        {
                            int n2 = 0;
                            var modelList2 = new List<string>();
                            foreach(var item2 in list2)
                            {
                                modelList2[n2++] = (string)item2;
                            }
                            modelList1[n1++] = modelList2;
                        }
                    }
                    model.ComplexList = modelList1;
                }
            }

            if (map.ContainsKey("floatNum"))
            {
                model.FloatNum = (float?)map["floatNum"];
            }

            if (map.ContainsKey("longNum"))
            {
                model.LongNum = (long?)map["longNum"];
            }

            return model;
        }
    }

}

