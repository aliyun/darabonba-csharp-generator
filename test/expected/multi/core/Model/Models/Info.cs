using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Darabonba;
using Darabonba.Utils;
using Test.TeaUtil.Models;

namespace Darabonba.Test.Model.Models
{
    public class Info : Darabonba.Model {
        [NameInMap("name")]
        [Validation(Required=true)]
        public string Name { get; set; }

        [NameInMap("age")]
        [Validation(Required=true)]
        public int? Age { get; set; }

        [NameInMap("runtime")]
        [Validation(Required=true)]
        public RuntimeOptions Runtime { get; set; }

    }

}

