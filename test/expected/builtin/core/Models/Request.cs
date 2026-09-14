using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Darabonba;
using Darabonba.Utils;

namespace Darabonba.Test.Models
{
    public class Request : Darabonba.Model {
        [NameInMap("name")]
        [Validation(Required=true)]
        public string Name { get; set; }

        public override Dictionary<string, object> ToMapCore(bool noStream = false)
        {
            var map = new Dictionary<string, object>();
            map["name"] = this.Name;
            return map;
        }

        public new static Request FromMap(IDictionary map)
        {
            if (map == null)
            {
                return null;
            }
            var model = new Request();
            if (map.Contains("name"))
            {
                model.Name = (string)map["name"];
            }
            return model;
        }
    }

}

