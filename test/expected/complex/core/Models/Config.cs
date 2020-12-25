// This file is auto-generated, don't edit it. Thanks.

using System;
using System.Collections.Generic;
using System.IO;

using Tea;

namespace Darabonba.Test.Models
{
    public class Config : TeaModel {
        [NameInMap("protocol")]
        [Validation(Required=true, MaxLength=50, Pattern="pattern")]
        public string Protocol { get; set; }

        [NameInMap("importConfig")]
        [Validation(Required=true)]
        public Darabonba.import.Models.Config ImportConfig { get; set; }

        [NameInMap("query")]
        [Validation(Required=true)]
        public string Query { get; set; }

        [NameInMap("complexList")]
        [Validation(Required=true)]
        public List<List<string>> ComplexList { get; set; }

        [NameInMap("floatNum")]
        [Validation(Required=true)]
        public float? FloatNum { get; set; }

        [NameInMap("longNum")]
        [Validation(Required=true)]
        public long? LongNum { get; set; }

    }

}
