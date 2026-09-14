// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Darabonba;
using Darabonba.Utils;

namespace Darabonba.Test.Models
{
    public class ImportedGrandchild : LegacyChild {
        [NameInMap("values")]
        [Validation(Required=true)]
        public List<Darabonba.import.Models.Request> Values { get; set; }

        public override Dictionary<string, object> ToMapCore(bool noStream = false)
        {
            var map = new Dictionary<string, object>();
            map["count"] = this.Count;
            map["accesskey"] = this.Accesskey;
            map["region"] = this.Region;
            {
                List<object> value0 = null;
                if (this.Values != null)
                {
                    value0 = new List<object>();
                    foreach (var item0 in this.Values)
                    {
                        value0.Add(item0 == null ? null : item0.ToMap());
                    }
                }
                map["values"] = value0;
            }
            return map;
        }

        public new static ImportedGrandchild FromMap(IDictionary map)
        {
            if (map == null)
            {
                return null;
            }
            var model = new ImportedGrandchild();
            if (map.Contains("count"))
            {
                model.Count = map["count"] == null ? (int?)null : System.Convert.ToInt32(map["count"]);
            }
            if (map.Contains("accesskey"))
            {
                model.Accesskey = (string)map["accesskey"];
            }
            if (map.Contains("region"))
            {
                model.Region = (string)map["region"];
            }
            if (map.Contains("values"))
            {
                List<Darabonba.import.Models.Request> value0 = null;
                if (map["values"] != null)
                {
                    value0 = new List<Darabonba.import.Models.Request>();
                    foreach (var item0 in (IList)map["values"])
                    {
                        value0.Add(item0 == null ? null : Darabonba.import.Models.Request.FromMap((IDictionary)item0));
                    }
                }
                model.Values = value0;
            }
            return model;
        }
    }

}

