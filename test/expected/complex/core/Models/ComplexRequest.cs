// This file is auto-generated, don't edit it. Thanks.

using System;
using System.Collections.Generic;
using System.IO;

using Tea;

namespace AlibabaCloud.Test.Models
{
    public class ComplexRequest : TeaModel {
        [NameInMap("accessKey")]
        [Validation(Required=true)]
        public string AccessKey { get; set; }

        [NameInMap("Body")]
        [Validation(Required=true)]
        public Stream Body { get; set; }

        [NameInMap("Strs")]
        [Validation(Required=true)]
        public List<string> Strs { get; set; }

        [NameInMap("header")]
        [Validation(Required=true)]
        public ComplexRequestHeader Header { get; set; }
        public class ComplexRequestHeader : TeaModel {
            [NameInMap("Content")]
            [Validation(Required=true)]
            public string Content { get; set; }
        };

        [NameInMap("num")]
        [Validation(Required=true)]
        public int? Num { get; set; }

        [NameInMap("client")]
        [Validation(Required=true)]
        public AlibabaCloud.import.Client Client { get; set; }

        [NameInMap("Part")]
        [Validation(Required=false)]
        [Obsolete]
        public List<ComplexRequestPart> Part { get; set; }
        public class ComplexRequestPart : TeaModel {
            [NameInMap("PartNumber")]
            [Validation(Required=false)]
            public string PartNumber { get; set; }

        }

    }

}
