// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Darabonba;

namespace Darabonba.Test.Models
{
    public class Config : Model {
        public Config Copy()
        {
            Config copy = FromMap(ToMap());
            return copy;
        }

        public Config CopyWithoutStream()
        {
            Config copy = FromMap(ToMap(true));
            return copy;
        }

        public Dictionary<string, object> ToMap(bool noStream = false)
        {
            var map = new Dictionary<string, object>();
            return map;
        }

        public static Config FromMap(Dictionary<string, object> map)
        {
            var model = new Config();
            return model;
        }
    }

}

