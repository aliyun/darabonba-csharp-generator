// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Darabonba;

namespace Darabonba.Test.Models
{
    public class M : Model {
        [NameInMap("subM")]
        [Validation(Required=true)]
        public MSubM SubM { get; set; }
        public class MSubM : Model {
            public MSubM Copy()
            {
                MSubM copy = FromMap(ToMap());
                return copy;
            }

            public MSubM CopyWithoutStream()
            {
                MSubM copy = FromMap(ToMap(true));
                return copy;
            }

            public Dictionary<string, object> ToMap(bool noStream = false)
            {
                var map = new Dictionary<string, object>();
                return map;
            }

            public static MSubM FromMap(Dictionary<string, object> map)
            {
                var model = new MSubM();
                return model;
            }
        }

        public M Copy()
        {
            M copy = FromMap(ToMap());
            return copy;
        }

        public M CopyWithoutStream()
        {
            M copy = FromMap(ToMap(true));
            return copy;
        }

        public Dictionary<string, object> ToMap(bool noStream = false)
        {
            var map = new Dictionary<string, object>();
            if (SubM != null)
            {
                map["subM"] = SubM != null ? SubM.ToMap(noStream) : null;
            }

            return map;
        }

        public static M FromMap(Dictionary<string, object> map)
        {
            var model = new M();
            if (map.ContainsKey("subM"))
            {
                if (map["subM"] != null)
                {
                    var temp = (Dictionary<string, object>)map["subM"];
                    model.SubM = MSubM.FromMap(temp);
                }
            }

            return model;
        }
    }

}

