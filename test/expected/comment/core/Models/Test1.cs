// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Darabonba;
using Darabonba.Utils;

namespace Darabonba.Test.Models
{
    // top comment
    /// <term><b>Description:</b></term>
    /// <description>
    /// <para>top annotation</para>
    /// </description>
    /// <term><b>Description:</b></term>
    /// <description>
    /// <para>TestModel</para>
    /// </description>
    public class Test1 : Darabonba.Model {
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
        public override Dictionary<string, object> ToMapCore(bool noStream = false)
        {
            var map = new Dictionary<string, object>();
            map["test"] = this.Test;
            map["test2"] = this.Test2;
            return map;
        }

        public new static Test1 FromMap(IDictionary map)
        {
            if (map == null)
            {
                return null;
            }
            var model = new Test1();
            if (map.Contains("test"))
            {
                model.Test = (string)map["test"];
            }
            if (map.Contains("test2"))
            {
                model.Test2 = (string)map["test2"];
            }
            return model;
        }
    }

}

