// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Darabonba;
using Darabonba.Utils;

namespace Darabonba.Test.Models
{
    public class M : Darabonba.Model {
        [NameInMap("subM")]
        [Validation(Required=true)]
        public MSubM SubM { get; set; }
        public class MSubM : Darabonba.Model {
            public override Dictionary<string, object> ToMapCore(bool noStream = false)
            {
                var map = new Dictionary<string, object>();
                return map;
            }

            public new static MSubM FromMap(IDictionary map)
            {
                if (map == null)
                {
                    return null;
                }
                var model = new MSubM();
                return model;
            }
        }

        public override Dictionary<string, object> ToMapCore(bool noStream = false)
        {
            var map = new Dictionary<string, object>();
            map["subM"] = this.SubM == null ? null : this.SubM.ToMap();
            return map;
        }

        public new static M FromMap(IDictionary map)
        {
            if (map == null)
            {
                return null;
            }
            var model = new M();
            if (map.Contains("subM"))
            {
                model.SubM = map["subM"] == null ? null : MSubM.FromMap((IDictionary)map["subM"]);
            }
            return model;
        }
    }

}

