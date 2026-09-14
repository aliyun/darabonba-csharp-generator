// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Darabonba;
using Darabonba.Utils;

namespace Darabonba.Test.Models
{
    public class LowerModel : Darabonba.Model {
        [NameInMap("stringfield")]
        [Validation(Required=true)]
        public string Stringfield { get; set; }

        public override Dictionary<string, object> ToMapCore(bool noStream = false)
        {
            var map = new Dictionary<string, object>();
            map["stringfield"] = this.Stringfield;
            return map;
        }

        public new static LowerModel FromMap(IDictionary map)
        {
            if (map == null)
            {
                return null;
            }
            var model = new LowerModel();
            if (map.Contains("stringfield"))
            {
                model.Stringfield = (string)map["stringfield"];
            }
            return model;
        }
    }

}

