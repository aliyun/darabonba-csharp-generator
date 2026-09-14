using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Darabonba;
using Darabonba.Utils;
using Test.TeaUtil.Models;

namespace Darabonba.Test.Model.Models
{
    public class Info : Darabonba.Model {
        [NameInMap("name")]
        [Validation(Required=true)]
        public string Name { get; set; }

        [NameInMap("age")]
        [Validation(Required=true)]
        public int? Age { get; set; }

        [NameInMap("runtime")]
        [Validation(Required=true)]
        public RuntimeOptions Runtime { get; set; }

        public override Dictionary<string, object> ToMapCore(bool noStream = false)
        {
            var map = new Dictionary<string, object>();
            map["name"] = this.Name;
            map["age"] = this.Age;
            map["runtime"] = this.Runtime == null ? null : this.Runtime.ToMap();
            return map;
        }

        public new static Info FromMap(IDictionary map)
        {
            if (map == null)
            {
                return null;
            }
            var model = new Info();
            if (map.Contains("name"))
            {
                model.Name = (string)map["name"];
            }
            if (map.Contains("age"))
            {
                model.Age = map["age"] == null ? (int?)null : System.Convert.ToInt32(map["age"]);
            }
            if (map.Contains("runtime"))
            {
                model.Runtime = map["runtime"] == null ? null : Test.TeaUtil.Models.RuntimeOptions.FromMap((IDictionary)map["runtime"]);
            }
            return model;
        }
    }

}

