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

        public override Dictionary<string, object> ToMapCore(bool noStream = false)
        {
            var map = new Dictionary<string, object>();
            map["test"] = this.Test;
            map["test2"] = this.Test2_;
            return map;
        }

        public new static Test2 FromMap(IDictionary map)
        {
            if (map == null)
            {
                return null;
            }
            var model = new Test2();
            if (map.Contains("test"))
            {
                model.Test = (string)map["test"];
            }
            if (map.Contains("test2"))
            {
                model.Test2_ = (string)map["test2"];
            }
            return model;
        }
    }

}

