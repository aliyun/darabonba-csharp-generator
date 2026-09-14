// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Darabonba;
using Darabonba.Utils;

namespace Darabonba.Test.Models
{
    public class BuiltinChild : Darabonba.Models.SSEEvent {
        [NameInMap("name")]
        [Validation(Required=true)]
        public string Name { get; set; }

        public override Dictionary<string, object> ToMapCore(bool noStream = false)
        {
            var map = new Dictionary<string, object>();
            map["id"] = this.Id;
            map["event"] = this.Event;
            map["data"] = this.Data;
            map["retry"] = this.Retry;
            map["name"] = this.Name;
            return map;
        }

        public new static BuiltinChild FromMap(IDictionary map)
        {
            if (map == null)
            {
                return null;
            }
            var model = new BuiltinChild();
            if (map.Contains("id"))
            {
                model.Id = (string)map["id"];
            }
            if (map.Contains("event"))
            {
                model.Event = (string)map["event"];
            }
            if (map.Contains("data"))
            {
                model.Data = (string)map["data"];
            }
            if (map.Contains("retry"))
            {
                model.Retry = map["retry"] == null ? (int?)null : System.Convert.ToInt32(map["retry"]);
            }
            if (map.Contains("name"))
            {
                model.Name = (string)map["name"];
            }
            return model;
        }
    }

}

