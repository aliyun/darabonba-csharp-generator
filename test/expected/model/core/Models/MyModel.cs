// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Darabonba;

namespace Darabonba.Test.Models
{
    public class MyModel : Darabonba.Model {
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
        public class MyModelSubmodel : Darabonba.Model {
            [NameInMap("stringfield")]
            [Validation(Required=true)]
            public string Stringfield { get; set; }

        }

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
        public Darabonba.Request Request { get; set; }

        [NameInMap("m")]
        [Validation(Required=true)]
        public Darabonba.Model M { get; set; }

        [NameInMap("mapModel")]
        [Validation(Required=true)]
        public Dictionary<string, LowerModel> MapModel { get; set; }

        [NameInMap("submodelMap")]
        [Validation(Required=true)]
        public Dictionary<string, MyModel.MyModelSubmodel> SubmodelMap { get; set; }

    }

}

