using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Darabonba;
using Darabonba.Utils;

namespace Test.OpenApiClient.Models
{
    public class DaraModel : Darabonba.Model {
        [NameInMap("test")]
        [Validation(Required=true)]
        public string Test { get; set; }

    }

}

