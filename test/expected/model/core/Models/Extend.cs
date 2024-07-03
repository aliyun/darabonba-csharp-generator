// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Darabonba;

namespace Darabonba.Test.Models
{
    public class Extend : Parent {
        [NameInMap("size")]
        [Validation(Required=true)]
        public int? Size { get; set; }

        public Extend Copy()
        {
            Extend copy = FromMap(ToMap());
            return copy;
        }

        public Extend CopyWithoutStream()
        {
            Extend copy = FromMap(ToMap(true));
            return copy;
        }

        public Dictionary<string, object> ToMap(bool noStream = false)
        {
            var map = new Dictionary<string, object>();
            if (Size != null)
            {
                map["size"] = Size;
            }

            return map;
        }

        public static Extend FromMap(Dictionary<string, object> map)
        {
            var model = new Extend();
            if (map.ContainsKey("size"))
            {
                model.Size = (int?)map["size"];
            }

            return model;
        }
    }

}

