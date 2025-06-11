// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Darabonba.Utils;

namespace Darabonba.Test.Exceptions
{
    public class MainFileException : Darabonba.Exceptions.DaraException
    {
        public int? Size { get; set; }
        public MainFileExceptionModel Model { get; set; }
        public class MainFileExceptionModel : Darabonba.Model
        {
            [NameInMap("str")]
            [Validation(Required=true)]
            public string Str { get; set; }

            [NameInMap("model")]
            [Validation(Required=true)]
            public MainFileExceptionModelModel Model { get; set; }
            public class MainFileExceptionModelModel : Darabonba.Model {
                [NameInMap("str")]
                [Validation(Required=true)]
                public string Str { get; set; }

            }

        }
        public MainFileExceptionModel1 Model1 { get; set; }
        public class MainFileExceptionModel1 : Darabonba.Model
        {
            [NameInMap("code")]
            [Validation(Required=true)]
            public int? Code { get; set; }

        }

        public class MainFileExceptionModel : Darabonba.Model
        {
            [NameInMap("str")]
            [Validation(Required=true)]
            public string Str { get; set; }

            [NameInMap("model")]
            [Validation(Required=true)]
            public MainFileExceptionModelModel Model { get; set; }
            public class MainFileExceptionModelModel : Darabonba.Model {
                [NameInMap("str")]
                [Validation(Required=true)]
                public string Str { get; set; }

            }

        }
        public class MainFileExceptionModel1 : Darabonba.Model
        {
            [NameInMap("code")]
            [Validation(Required=true)]
            public int? Code { get; set; }

        }
    }

}

