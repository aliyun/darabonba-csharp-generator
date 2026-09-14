// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Darabonba.Utils;
using Darabonba;

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

                public override Dictionary<string, object> ToMapCore(bool noStream = false)
                {
                    var map = new Dictionary<string, object>();
                    map["str"] = this.Str;
                    return map;
                }

                public new static MainFileExceptionModelModel FromMap(IDictionary map)
                {
                    if (map == null)
                    {
                        return null;
                    }
                    var model = new MainFileExceptionModelModel();
                    if (map.Contains("str"))
                    {
                        model.Str = (string)map["str"];
                    }
                    return model;
                }
            }

            public override Dictionary<string, object> ToMapCore(bool noStream = false)
            {
                var map = new Dictionary<string, object>();
                map["str"] = this.Str;
                map["model"] = this.Model == null ? null : this.Model.ToMap();
                return map;
            }

            public new static MainFileExceptionModel FromMap(IDictionary map)
            {
                if (map == null)
                {
                    return null;
                }
                var model = new MainFileExceptionModel();
                if (map.Contains("str"))
                {
                    model.Str = (string)map["str"];
                }
                if (map.Contains("model"))
                {
                    model.Model = map["model"] == null ? null : MainFileExceptionModelModel.FromMap((IDictionary)map["model"]);
                }
                return model;
            }
        }
        public MainFileExceptionModel1 Model1 { get; set; }
        public class MainFileExceptionModel1 : Darabonba.Model
        {
            [NameInMap("code")]
            [Validation(Required=true)]
            public int? Code { get; set; }

            public override Dictionary<string, object> ToMapCore(bool noStream = false)
            {
                var map = new Dictionary<string, object>();
                map["code"] = this.Code;
                return map;
            }

            public new static MainFileExceptionModel1 FromMap(IDictionary map)
            {
                if (map == null)
                {
                    return null;
                }
                var model = new MainFileExceptionModel1();
                if (map.Contains("code"))
                {
                    model.Code = map["code"] == null ? (int?)null : System.Convert.ToInt32(map["code"]);
                }
                return model;
            }
        }

    }

}

