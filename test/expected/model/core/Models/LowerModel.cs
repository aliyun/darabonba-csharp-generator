// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Tea;

namespace Darabonba.Test.Models
{
    public class LowerModel : TeaModel {
        [NameInMap("stringfield")]
        [Validation(Required=true)]
        public string Stringfield { get; set; }

        public new LowerModel Copy()
        {
            LowerModel copy = FromMap(ToMap());
            return copy;
        }

        public new LowerModel CopyWithoutStream()
        {
            LowerModel copy = FromMap(ToMap(true));
            return copy;
        }

        public new void Validate()
        {
            TeaModel.ValidateRequired("Stringfield", Stringfield, true);
            base.Validate();
        }

        public new Dictionary<string, object> ToMap(bool noStream = false)
        {
            var map = new Dictionary<string, object>();
            if (Stringfield != null)
            {
                map["stringfield"] = Stringfield;
            }

            return map;
        }

        public static new LowerModel FromMap(Dictionary<string, object> map)
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

