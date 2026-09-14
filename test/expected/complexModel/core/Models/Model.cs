// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Darabonba;
using Darabonba.Utils;

namespace Darabonba.Test.Models
{
    public class Model : Darabonba.Model {
        [NameInMap("str")]
        [Validation(Required=true)]
        public string Str { get; set; }

        public override Dictionary<string, object> ToMapCore(bool noStream = false)
        {
            var map = new Dictionary<string, object>();
            map["str"] = this.Str;
            return map;
        }

        public new static Model FromMap(IDictionary map)
        {
            if (map == null)
            {
                return null;
            }
            var model = new Model();
            if (map.Contains("str"))
            {
                model.Str = (string)map["str"];
            }
            return model;
        }
    }

}

