using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Darabonba;

namespace AlibabaCloud.OpenApiClient.Models
{
    /// <term><b>Description:</b></term>
    /// <description>
    /// <para>This is for OpenApi Util</para>
    /// </description>
    public class GlobalParameters : Darabonba.Model {
        [NameInMap("headers")]
        [Validation(Required=false)]
        public Dictionary<string, string> Headers { get; set; }

        [NameInMap("queries")]
        [Validation(Required=false)]
        public Dictionary<string, string> Queries { get; set; }

    }

}

