// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Darabonba;
using Darabonba.Utils;

namespace Darabonba.Test.Models
{
    public class Right : Darabonba.Model {
        [NameInMap("left")]
        [Validation(Required=true)]
        public Left Left { get; set; }

        public override Dictionary<string, object> ToMapCore(bool noStream = false)
        {
            var map = new Dictionary<string, object>();
            map["left"] = this.Left == null ? null : this.Left.ToMap();
            return map;
        }

        public new static Right FromMap(IDictionary map)
        {
            if (map == null)
            {
                return null;
            }
            var model = new Right();
            if (map.Contains("left"))
            {
                model.Left = map["left"] == null ? null : Darabonba.Test.Models.Left.FromMap((IDictionary)map["left"]);
            }
            return model;
        }
    }

}

