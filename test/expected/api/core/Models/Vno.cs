// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Darabonba;

namespace Darabonba.Test.Models
{
    public class Vno : DaraModel {
        public Vno Copy()
        {
            Vno copy = FromMap(ToMap());
            return copy;
        }

        public Vno CopyWithoutStream()
        {
            Vno copy = FromMap(ToMap(true));
            return copy;
        }

        public Dictionary<string, object> ToMap(bool noStream = false)
        {
            var map = new Dictionary<string, object>();
            return map;
        }

        public static Vno FromMap(Dictionary<string, object> map)
        {
            var model = new Vno();
            return model;
        }
    }

}

