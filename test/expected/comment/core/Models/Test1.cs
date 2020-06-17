// top comment
/**
 top annotation
*/
// This file is auto-generated, don't edit it. Thanks.

using System;
using System.Collections.Generic;
using System.IO;

using Tea;

namespace AlibabaCloud.Test.Models
{
    /**
      TestModel
    */
    public class Test1 : TeaModel {
        [NameInMap("test")]
        [Validation(Required=true)]
        public string Test { get; set; }

        //model的test back comment
        [NameInMap("test2")]
        [Validation(Required=true)]
        public string Test2 { get; set; }

        //model的test2 back comment
    }

}
