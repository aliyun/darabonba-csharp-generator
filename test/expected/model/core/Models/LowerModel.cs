// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Darabonba;

namespace Darabonba.Test.Models
{
    public class LowerModel : DaraModel {
        [NameInMap("stringfield")]
        [Validation(Required=true)]
        public string Stringfield { get; set; }

        public LowerModel Copy()
        {
            LowerModel copy = FromMap(ToMap());
            return copy;
        }

        public LowerModel CopyWithoutStream()
        {
            LowerModel copy = FromMap(ToMap(true));
            return copy;
        }

        public Dictionary<string, object> ToMap(bool noStream = false)
        {
            var map = new Dictionary<string, object>();
            if (Stringfield != null)
            {
                map["stringfield"] = Stringfield;
            }

            return map;
        }

        public static LowerModel FromMap(Dictionary<string, object> map)
        {
            var model = new LowerModel();
            if (map.ContainsKey("stringfield"))
            {
                model.Stringfield = (string)map["stringfield"];
            }

            return model;
        }
    }

}

