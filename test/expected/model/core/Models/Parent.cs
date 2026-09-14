// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Darabonba;
using Darabonba.Utils;

namespace Darabonba.Test.Models
{
    public class Parent : Darabonba.Model {
        [NameInMap("name")]
        [Validation(Required=true)]
        public string Name { get; set; }

        [NameInMap("details")]
        [Validation(Required=true)]
        public ParentDetails Details { get; set; }
        public class ParentDetails : Darabonba.Model {
            [NameInMap("value")]
            [Validation(Required=true)]
            public string Value { get; set; }

            public override Dictionary<string, object> ToMapCore(bool noStream = false)
            {
                var map = new Dictionary<string, object>();
                map["value"] = this.Value;
                return map;
            }

            public new static ParentDetails FromMap(IDictionary map)
            {
                if (map == null)
                {
                    return null;
                }
                var model = new ParentDetails();
                if (map.Contains("value"))
                {
                    model.Value = (string)map["value"];
                }
                return model;
            }
        }

        public override Dictionary<string, object> ToMapCore(bool noStream = false)
        {
            var map = new Dictionary<string, object>();
            map["name"] = this.Name;
            map["details"] = this.Details == null ? null : this.Details.ToMap();
            return map;
        }

        public new static Parent FromMap(IDictionary map)
        {
            if (map == null)
            {
                return null;
            }
            var model = new Parent();
            if (map.Contains("name"))
            {
                model.Name = (string)map["name"];
            }
            if (map.Contains("details"))
            {
                model.Details = map["details"] == null ? null : ParentDetails.FromMap((IDictionary)map["details"]);
            }
            return model;
        }
    }

}

