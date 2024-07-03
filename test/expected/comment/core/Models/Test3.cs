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
    /// <para>TestModel3</para>
    /// </description>
    public class Test3 : DaraModel {
        // empty comment1
        // empy comment2
        public Test3 Copy()
        {
            Test3 copy = FromMap(ToMap());
            return copy;
        }

        public Test3 CopyWithoutStream()
        {
            Test3 copy = FromMap(ToMap(true));
            return copy;
        }

        public Dictionary<string, object> ToMap(bool noStream = false)
        {
            var map = new Dictionary<string, object>();
            return map;
        }

        public static Test3 FromMap(Dictionary<string, object> map)
        {
            var model = new Test3();
            return model;
        }
    }

}

