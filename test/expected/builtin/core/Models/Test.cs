using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Darabonba;

namespace Darabonba.Test.Models
{
    public class Test : Darabonba.Model {
        [NameInMap("name")]
        [Validation(Required=true)]
        public string Name { get; set; }

    }

}

