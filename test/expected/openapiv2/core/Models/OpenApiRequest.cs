using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Darabonba;

namespace AlibabaCloud.OpenApiClient.Models
{
    public class OpenApiRequest : Darabonba.Model {
        [NameInMap("headers")]
        [Validation(Required=false)]
        public Dictionary<string, string> Headers { get; set; }

        [NameInMap("query")]
        [Validation(Required=false)]
        public Dictionary<string, string> Query { get; set; }

        [NameInMap("body")]
        [Validation(Required=false)]
        public object Body { get; set; }

        [NameInMap("stream")]
        [Validation(Required=false)]
        public Stream Stream { get; set; }

        [NameInMap("hostMap")]
        [Validation(Required=false)]
        public Dictionary<string, string> HostMap { get; set; }

        [NameInMap("endpointOverride")]
        [Validation(Required=false)]
        public string EndpointOverride { get; set; }

    }

}

