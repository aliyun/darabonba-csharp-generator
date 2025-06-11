using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Darabonba;
using Darabonba.Utils;

namespace AlibabaCloud.OpenApiClient.Models
{
    public class Params : Darabonba.Model {
        [NameInMap("action")]
        [Validation(Required=true)]
        public string Action { get; set; }

        [NameInMap("version")]
        [Validation(Required=true)]
        public string Version { get; set; }

        [NameInMap("protocol")]
        [Validation(Required=true)]
        public string Protocol { get; set; }

        [NameInMap("pathname")]
        [Validation(Required=true)]
        public string Pathname { get; set; }

        [NameInMap("method")]
        [Validation(Required=true)]
        public string Method { get; set; }

        [NameInMap("authType")]
        [Validation(Required=true)]
        public string AuthType { get; set; }

        [NameInMap("bodyType")]
        [Validation(Required=true)]
        public string BodyType { get; set; }

        [NameInMap("reqBodyType")]
        [Validation(Required=true)]
        public string ReqBodyType { get; set; }

        [NameInMap("style")]
        [Validation(Required=false)]
        public string Style { get; set; }

    }

}

