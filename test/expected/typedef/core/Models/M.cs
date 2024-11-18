using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Darabonba;
using System.Net.Http;
using System.Net.Http.Headers;
using Tea;

namespace Darabonba.Test.Models
{
    public class M : Model {
        [NameInMap("a")]
        [Validation(Required=false)]
        public HttpRequestMessage A { get; set; }

        [NameInMap("b")]
        [Validation(Required=false)]
        public HttpRequestHeaders B { get; set; }

        [NameInMap("c")]
        [Validation(Required=false)]
        public TeaModel C { get; set; }

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
            if (A != null)
            {
                map["a"] = A;
            }

            if (B != null)
            {
                map["b"] = B;
            }

            if (C != null)
            {
                map["c"] = C;
            }

            return map;
        }

        public static M FromMap(Dictionary<string, object> map)
        {
            var model = new M();
            if (map.ContainsKey("a"))
            {
                model.A = (HttpRequestMessage)map["a"];
            }

            if (map.ContainsKey("b"))
            {
                model.B = (HttpRequestHeaders)map["b"];
            }

            if (map.ContainsKey("c"))
            {
                model.C = (TeaModel)map["c"];
            }

            return model;
        }
    }

}

