// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Tea;

namespace Darabonba.Test.Models
{
    public class Config : TeaModel {
        public new Config Copy()
        {
            Config copy = FromMap(ToMap());
            return copy;
        }

        public new Config CopyWithoutStream()
        {
            Config copy = FromMap(ToMap(true));
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

        public static new Config FromMap(Dictionary<string, object> map)
        {
            var model = new Config();
            return model;
        }
    }

}

