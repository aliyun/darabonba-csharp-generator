// top comment
/// <term><b>Description:</b></term>
/// <description>
/// <para>top annotation</para>
/// </description>
// This file is auto-generated, don't edit it. Thanks.

using System;
using System.Collections.Generic;
using System.IO;

using Tea;

namespace Darabonba.Test.Models
{
    /// <term><b>Description:</b></term>
    /// <description>
    /// <para>TestModel</para>
    /// </description>
    public class Test1 : TeaModel {
        /// <term><b>Obsolete</b></term>
        /// 
        /// <summary>
        /// <para>test desc</para>
        /// 
        /// <b>check if is blank:</b>
        /// <c>true</c>
        /// 
        /// <b>if can be null:</b>
        /// <c>true</c>
        /// 
        /// <b>if is sensitive:</b>
        /// <c>true</c>
        /// </summary>
        [NameInMap("test")]
        [Validation(Required=true)]
        [Obsolete]
        public string Test { get; set; }

        //model的test back comment
        /// <summary>
        /// <para>test2 desc</para>
        /// </summary>
        [NameInMap("test2")]
        [Validation(Required=true)]
        public string Test2 { get; set; }

        //model的test2 back comment
    }

}
