// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Tea;

namespace Darabonba.Test.Models
{
    /// <term><b>Description:</b></term>
    /// <description>
    /// <para>TestModel2</para>
    /// </description>
    public class Test2 : TeaModel {
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

        public new Test2 Copy()
        {
            Test2 copy = FromMap(ToMap());
            return copy;
        }

        public new Test2 CopyWithoutStream()
        {
            Test2 copy = FromMap(ToMap(true));
            return copy;
        }

        public new void Validate()
        {
            TeaModel.ValidateRequired("Test", Test, true);
            TeaModel.ValidateRequired("Test2", Test2, true);
            base.Validate();
        }

        public new Dictionary<string, object> ToMap(bool noStream = false)
        {
            var map = new Dictionary<string, object>();
            if (Test != null)
            {
                map["test"] = Test;
            }

            if (Test2 != null)
            {
                map["test2"] = Test2;
            }

            return map;
        }

        public static new Test2 FromMap(Dictionary<string, object> map)
        {
            var model = new Test2();
            if (map.ContainsKey("test"))
            {
                model.Test = (string)map["test"];
            }

            if (map.ContainsKey("test2"))
            {
                model.Test2 = (string)map["test2"];
            }

            return model;
        }
    }

}

