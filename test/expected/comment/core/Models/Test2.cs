// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Darabonba;
using Darabonba.Utils;

namespace Darabonba.Test.Models
{
    /// <term><b>Description:</b></term>
    /// <description>
    /// <para>TestModel2</para>
    /// </description>
    public class Test2 : Darabonba.Model {
        // model的test front comment
        /// <summary>
        /// <para>test desc</para>
        /// </summary>
        [NameInMap("test")]
        [Validation(Required=true)]
        public string Test { get; set; }

        // model的test front comment
        /// <summary>
        /// <para>test2 desc</para>
        /// </summary>
        [NameInMap("test2")]
        [Validation(Required=true)]
        public string Test2_ { get; set; }

    }

}

