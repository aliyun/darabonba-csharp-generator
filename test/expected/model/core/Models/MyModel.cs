// This file is auto-generated, don't edit it. Thanks.

using System;
using System.Collections.Generic;
using System.IO;

using Tea;

namespace AlibabaCloud.Test.Models
{
    public class MyModel : TeaModel {
        [NameInMap("stringfield")]
        [Validation(Required=true)]
        public string Stringfield { get; set; }

        [NameInMap("stringarrayfield")]
        [Validation(Required=true)]
        public List<string> Stringarrayfield { get; set; }

        [NameInMap("mapfield")]
        [Validation(Required=true)]
        public Dictionary<string, string> Mapfield { get; set; }

        [NameInMap("realName")]
        [Validation(Required=true)]
        public string Name { get; set; }

        [NameInMap("submodel")]
        [Validation(Required=true)]
        public MyModelSubmodel Submodel { get; set; }
        public class MyModelSubmodel : TeaModel {
            [NameInMap("stringfield")]
            [Validation(Required=true)]
            public string Stringfield { get; set; }
        };

        [NameInMap("object")]
        [Validation(Required=true)]
        public Dictionary<string, object> Object { get; set; }

        [NameInMap("numberfield")]
        [Validation(Required=true)]
        public int? Numberfield { get; set; }

        [NameInMap("readable")]
        [Validation(Required=true)]
        public Stream Readable { get; set; }

        [NameInMap("request")]
        [Validation(Required=true)]
        public TeaRequest Request { get; set; }

    }

}
