// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Tea;

namespace Darabonba.Test.Models
{
    public class Parent : TeaModel {
        [NameInMap("name")]
        [Validation(Required=true)]
        public string Name { get; set; }

        public new Parent Copy()
        {
            Parent copy = FromMap(ToMap());
            return copy;
        }

        public new Parent CopyWithoutStream()
        {
            Parent copy = FromMap(ToMap(true));
            return copy;
        }

        public new void Validate()
        {
            TeaModel.ValidateRequired("Name", Name, true);
            base.Validate();
        }

        public new Dictionary<string, object> ToMap(bool noStream = false)
        {
            var map = new Dictionary<string, object>();
            if (Name != null)
            {
                map["name"] = Name;
            }

            return map;
        }

        public static new Parent FromMap(Dictionary<string, object> map)
        {
            var model = new Parent();
            if (map.ContainsKey("name"))
            {
                model.Name = (string)map["name"];
            }

            return model;
        }
    }

}

