// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Darabonba;

namespace Darabonba.Test.Models
{
    public class Parent : DaraModel {
        [NameInMap("name")]
        [Validation(Required=true)]
        public string Name { get; set; }

        public Parent Copy()
        {
            Parent copy = FromMap(ToMap());
            return copy;
        }

        public Parent CopyWithoutStream()
        {
            Parent copy = FromMap(ToMap(true));
            return copy;
        }

        public Dictionary<string, object> ToMap(bool noStream = false)
        {
            var map = new Dictionary<string, object>();
            if (Name != null)
            {
                map["name"] = Name;
            }

            return map;
        }

        public static Parent FromMap(Dictionary<string, object> map)
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

