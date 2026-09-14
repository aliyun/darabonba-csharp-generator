using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Darabonba;
using Darabonba.Utils;

namespace Test.OpenApiClient.Models
{
    public class DaraModel : Darabonba.Model {
        [NameInMap("test")]
        [Validation(Required=true)]
        public string Test { get; set; }

        public override Dictionary<string, object> ToMapCore(bool noStream = false)
        {
            var map = new Dictionary<string, object>();
            map["test"] = this.Test;
            return map;
        }

        public new static DaraModel FromMap(IDictionary map)
        {
            if (map == null)
            {
                return null;
            }
            var model = new DaraModel();
            if (map.Contains("test"))
            {
                model.Test = (string)map["test"];
            }
            return model;
        }
    }

}

