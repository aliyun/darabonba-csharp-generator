using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Tea;
using AlibabaCloud.TeaUtil.Models;

namespace Darabonba.Test.Model.Models
{
    public class Info : TeaModel {
        [NameInMap("name")]
        [Validation(Required=true)]
        public string Name { get; set; }

        [NameInMap("age")]
        [Validation(Required=true)]
        public int? Age { get; set; }

        [NameInMap("runtime")]
        [Validation(Required=true)]
        public RuntimeOptions Runtime { get; set; }

        public new Info Copy()
        {
            Info copy = FromMap(ToMap());
            return copy;
        }

        public new Info CopyWithoutStream()
        {
            Info copy = FromMap(ToMap(true));
            return copy;
        }

        public new void Validate()
        {
            TeaModel.ValidateRequired("Name", Name, true);
            TeaModel.ValidateRequired("Age", Age, true);
            if (Runtime != null) {
                Runtime.Validate();
            }
            TeaModel.ValidateRequired("Runtime", Runtime, true);
            base.Validate();
        }

        public new Dictionary<string, object> ToMap(bool noStream = false)
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

        public static new Info FromMap(Dictionary<string, object> map)
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

