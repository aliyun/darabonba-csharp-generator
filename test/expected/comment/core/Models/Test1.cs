// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Tea;

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
        public new Test1 Copy()
        {
            Test1 copy = FromMap(ToMap());
            return copy;
        }

        public new Test1 CopyWithoutStream()
        {
            Test1 copy = FromMap(ToMap(true));
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

        public static new Test1 FromMap(Dictionary<string, object> map)
        {
            var model = new Test1();
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

