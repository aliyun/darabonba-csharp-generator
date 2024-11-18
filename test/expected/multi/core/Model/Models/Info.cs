using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Darabonba;
using AlibabaCloud.TeaUtil.Models;

namespace Darabonba.Test.Model.Models
{
    public class Info : Model {
        [NameInMap("name")]
        [Validation(Required=true)]
        public string Name { get; set; }

        [NameInMap("age")]
        [Validation(Required=true)]
        public int? Age { get; set; }

        [NameInMap("runtime")]
        [Validation(Required=true)]
        public RuntimeOptions Runtime { get; set; }

        public Info Copy()
        {
            Info copy = FromMap(ToMap());
            return copy;
        }

        public Info CopyWithoutStream()
        {
            Info copy = FromMap(ToMap(true));
            return copy;
        }

        public Dictionary<string, object> ToMap(bool noStream = false)
        {
            var map = new Dictionary<string, object>();
            if (Name != null)
            {
                map["name"] = Name;
            }

            if (Age != null)
            {
                map["age"] = Age;
            }

            if (Runtime != null)
            {
                map["runtime"] = Runtime != null ? Runtime.ToMap(noStream) : null;
            }

            return map;
        }

        public static Info FromMap(Dictionary<string, object> map)
        {
            var model = new Info();
            if (map.ContainsKey("name"))
            {
                model.Name = (string)map["name"];
            }

            if (map.ContainsKey("age"))
            {
                model.Age = (int?)map["age"];
            }

            if (map.ContainsKey("runtime"))
            {
                model.Runtime = RuntimeOptions.FromMap(map["runtime"]);
            }

            return model;
        }
    }

}

