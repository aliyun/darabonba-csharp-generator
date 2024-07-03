// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Darabonba;

namespace Darabonba.Test.Models
{
    public class Model : DaraModel {
        [NameInMap("str")]
        [Validation(Required=true)]
        public string Str { get; set; }

        public Model Copy()
        {
            Model copy = FromMap(ToMap());
            return copy;
        }

        public Model CopyWithoutStream()
        {
            Model copy = FromMap(ToMap(true));
            return copy;
        }

        public Dictionary<string, object> ToMap(bool noStream = false)
        {
            var map = new Dictionary<string, object>();
            if (Str != null)
            {
                map["str"] = Str;
            }

            return map;
        }

        public static Model FromMap(Dictionary<string, object> map)
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

