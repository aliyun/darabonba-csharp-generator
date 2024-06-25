// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Tea;

namespace Darabonba.Test.Models
{
    public class Extend : Parent {
        [NameInMap("size")]
        [Validation(Required=true)]
        public int? Size { get; set; }

        public new Extend Copy()
        {
            Extend copy = FromMap(ToMap());
            return copy;
        }

        public new Extend CopyWithoutStream()
        {
            Extend copy = FromMap(ToMap(true));
            return copy;
        }

        public new void Validate()
        {
            TeaModel.ValidateRequired("Size", Size, true);
            base.Validate();
        }

        public new Dictionary<string, object> ToMap(bool noStream = false)
        {
            var map = new Dictionary<string, object>();
            if (Size != null)
            {
                map["size"] = Size;
            }

            return map;
        }

        public static new Extend FromMap(Dictionary<string, object> map)
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

