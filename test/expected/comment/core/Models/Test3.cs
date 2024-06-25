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
    /// <para>TestModel3</para>
    /// </description>
    public class Test3 : TeaModel {
        // empty comment1
        // empy comment2
        public new Test3 Copy()
        {
            Test3 copy = FromMap(ToMap());
            return copy;
        }

        public new Test3 CopyWithoutStream()
        {
            Test3 copy = FromMap(ToMap(true));
            return copy;
        }

        public new void Validate()
        {
            base.Validate();
        }

        public new Dictionary<string, object> ToMap(bool noStream = false)
        {
            var map = new Dictionary<string, object>();
            return map;
        }

        public static new Test3 FromMap(Dictionary<string, object> map)
        {
            var model = new Test3();
            return model;
        }
    }

}

