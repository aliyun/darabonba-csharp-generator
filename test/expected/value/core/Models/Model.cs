using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Darabonba;
using Darabonba.Utils;

namespace Test.OpenApiClient.Models
{
    public class Model : Darabonba.Model {
        [NameInMap("Model")]
        [Validation(Required=false)]
        public ModelModel Model { get; set; }
        public class ModelModel : Darabonba.Model {
            /// <summary>
            /// <para>错误手机号</para>
            /// </summary>
            [NameInMap("UnHandleNumbers")]
            [Validation(Required=false)]
            public List<string> UnHandleNumbers { get; set; }

            public override Dictionary<string, object> ToMapCore(bool noStream = false)
            {
                var map = new Dictionary<string, object>();
                {
                    List<object> value0 = null;
                    if (this.UnHandleNumbers != null)
                    {
                        value0 = new List<object>();
                        foreach (var item0 in this.UnHandleNumbers)
                        {
                            value0.Add(item0);
                        }
                    }
                    map["UnHandleNumbers"] = value0;
                }
                return map;
            }

            public new static ModelModel FromMap(IDictionary map)
            {
                if (map == null)
                {
                    return null;
                }
                var model = new ModelModel();
                if (map.Contains("UnHandleNumbers"))
                {
                    List<string> value0 = null;
                    if (map["UnHandleNumbers"] != null)
                    {
                        value0 = new List<string>();
                        foreach (var item0 in (IList)map["UnHandleNumbers"])
                        {
                            value0.Add((string)item0);
                        }
                    }
                    model.UnHandleNumbers = value0;
                }
                return model;
            }
        }

        public override Dictionary<string, object> ToMapCore(bool noStream = false)
        {
            var map = new Dictionary<string, object>();
            map["Model"] = this.Model_ == null ? null : this.Model_.ToMap();
            return map;
        }

        public new static Model FromMap(IDictionary map)
        {
            if (map == null)
            {
                return null;
            }
            var model = new Model();
            if (map.Contains("Model"))
            {
                model.Model_ = map["Model"] == null ? null : ModelModel_.FromMap((IDictionary)map["Model"]);
            }
            return model;
        }
    }

}

