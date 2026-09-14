// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Darabonba;
using Darabonba.Utils;

namespace Darabonba.Test.Models
{
    public class Extend : Parent {
        [NameInMap("size")]
        [Validation(Required=true)]
        public int? Size { get; set; }

        public override Dictionary<string, object> ToMapCore(bool noStream = false)
        {
            var map = new Dictionary<string, object>();
            map["name"] = this.Name;
            map["details"] = this.Details == null ? null : this.Details.ToMap();
            map["size"] = this.Size;
            return map;
        }

        public new static Extend FromMap(IDictionary map)
        {
            if (map == null)
            {
                return null;
            }
            var model = new Extend();
            if (map.Contains("name"))
            {
                model.Name = (string)map["name"];
            }
            if (map.Contains("details"))
            {
                model.Details = map["details"] == null ? null : ParentDetails.FromMap((IDictionary)map["details"]);
            }
            if (map.Contains("size"))
            {
                model.Size = map["size"] == null ? (int?)null : System.Convert.ToInt32(map["size"]);
            }
            return model;
        }
    }

}

