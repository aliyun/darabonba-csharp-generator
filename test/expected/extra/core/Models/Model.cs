using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Darabonba;
using Darabonba.Utils;

namespace AlibabaCloud.OpenApiClient.Models
{
    public class Model : Darabonba.Model {
        [NameInMap("Model")]
        [Validation(Required=false)]
        public ModelModel Model { get; set; }
        public class ModelModel : Darabonba.Model {
            /// <summary>
            /// <para>错误手机号</para>
            /// </summary>
            [NameInMap("UnHandleNumbers")]
            [Validation(Required=false)]
            public List<string> UnHandleNumbers { get; set; }

        }

    }

}

