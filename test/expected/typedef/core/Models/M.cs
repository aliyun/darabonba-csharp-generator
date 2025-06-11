using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Darabonba;
using Darabonba.Utils;
using System.Net.Http;
using System.Net.Http.Headers;
using Tea;

namespace Darabonba.Test.Models
{
    public class M : Darabonba.Model {
        [NameInMap("a")]
        [Validation(Required=false)]
        public HttpRequestMessage A { get; set; }

        [NameInMap("b")]
        [Validation(Required=false)]
        public HttpRequestHeaders B { get; set; }

        [NameInMap("c")]
        [Validation(Required=false)]
        public TeaModel C { get; set; }

    }

}

