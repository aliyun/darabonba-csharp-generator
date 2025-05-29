// This file is auto-generated, don't edit it. Thanks.

using System;
using System.Collections.Generic;
using System.IO;

using Tea;

namespace Darabonba.Test.Models
{
    public class Config : TeaModel {
        /// <summary>
        /// <para>disable HTTP/2</para>
        /// 
        /// <b>Example:</b>
        /// <para>false</para>
        /// </summary>
        [NameInMap("disableHttp2")]
        [Validation(Required=false)]
        public bool? DisableHttp2 { get; set; }

    }

}
