// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Tea;

namespace Darabonba.Test.Models
{
    public class Vno : TeaModel {
        public new Vno Copy()
        {
            Vno copy = FromMap(ToMap());
            return copy;
        }

        public new Vno CopyWithoutStream()
        {
            Vno copy = FromMap(ToMap(true));
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

        public static new Vno FromMap(Dictionary<string, object> map)
        {
            var model = new Vno();
            return model;
        }
    }

}

