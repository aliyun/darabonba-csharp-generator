// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Darabonba;

namespace Darabonba.Test.Models
{
    /// <term><b>Description:</b></term>
    /// <description>
    /// <para>TestModel2</para>
    /// </description>
    public class Test2 : Model {
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

        public Test2 Copy()
        {
            Test2 copy = FromMap(ToMap());
            return copy;
        }

        public Test2 CopyWithoutStream()
        {
            Test2 copy = FromMap(ToMap(true));
            return copy;
        }

        public Dictionary<string, object> ToMap(bool noStream = false)
        {
            var map = new Dictionary<string, object>();
            if (Test != null)
            {
                map["test"] = Test;
            }

            if (Test2_ != null)
            {
                map["test2"] = Test2_;
            }

            return map;
        }

        public static Test2 FromMap(Dictionary<string, object> map)
        {
            var model = new Test2();
            if (map.ContainsKey("test"))
            {
                model.Test = (string)map["test"];
            }

            if (map.ContainsKey("test2"))
            {
                model.Test2_ = (string)map["test2"];
            }

            return model;
        }
    }

}

