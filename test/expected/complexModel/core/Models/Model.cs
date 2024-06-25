// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Tea;

namespace Darabonba.Test.Models
{
    public class Model : TeaModel {
        [NameInMap("str")]
        [Validation(Required=true)]
        public string Str { get; set; }

        public new Model Copy()
        {
            Model copy = FromMap(ToMap());
            return copy;
        }

        public new Model CopyWithoutStream()
        {
            Model copy = FromMap(ToMap(true));
            return copy;
        }

        public new void Validate()
        {
            TeaModel.ValidateRequired("Str", Str, true);
            base.Validate();
        }

        public new Dictionary<string, object> ToMap(bool noStream = false)
        {
            var map = new Dictionary<string, object>();
            if (Str != null)
            {
                map["str"] = Str;
            }

            return map;
        }

        public static new Model FromMap(Dictionary<string, object> map)
        {
            var model = new Model();
            if (map.ContainsKey("str"))
            {
                model.Str = (string)map["str"];
            }

            return model;
        }
    }

}

