// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Darabonba;
using Darabonba.Utils;

namespace Darabonba.Test.Models
{
    public class Left : Darabonba.Model {
        [NameInMap("right")]
        [Validation(Required=true)]
        public Right Right { get; set; }

        public override Dictionary<string, object> ToMapCore(bool noStream = false)
        {
            var map = new Dictionary<string, object>();
            map["right"] = this.Right == null ? null : this.Right.ToMap();
            return map;
        }

        public new static Left FromMap(IDictionary map)
        {
            if (map == null)
            {
                return null;
            }
            var model = new Left();
            if (map.Contains("right"))
            {
                model.Right = map["right"] == null ? null : Darabonba.Test.Models.Right.FromMap((IDictionary)map["right"]);
            }
            return model;
        }
    }

}

