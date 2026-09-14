// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Darabonba;
using Darabonba.Utils;

namespace Darabonba.Test.Models
{
    public class LegacyChild : Darabonba.import.Models.Request {
        [NameInMap("count")]
        [Validation(Required=true)]
        public int? Count { get; set; }

        public override Dictionary<string, object> ToMapCore(bool noStream = false)
        {
            var map = new Dictionary<string, object>();
            map["accesskey"] = this.Accesskey;
            map["region"] = this.Region;
            map["count"] = this.Count;
            return map;
        }

        public new static LegacyChild FromMap(IDictionary map)
        {
            if (map == null)
            {
                return null;
            }
            var model = new LegacyChild();
            if (map.Contains("accesskey"))
            {
                model.Accesskey = (string)map["accesskey"];
            }
            if (map.Contains("region"))
            {
                model.Region = (string)map["region"];
            }
            if (map.Contains("count"))
            {
                model.Count = map["count"] == null ? (int?)null : System.Convert.ToInt32(map["count"]);
            }
            return model;
        }
    }

}

