// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Tea;
using Darabonba.Test.Models;

namespace Darabonba.Test.Exceptions
{
    public class MainFileError : TeaException {
        public int? Size { get; set; }
        public new Dictionary<string, Model> Data { get; set; }
        public MainFileErrorModel Model { get; set; }
        public class MainFileErrorModel : TeaModel
        {
            [NameInMap("str")]
            [Validation(Required=true)]
            public string Str { get; set; }

            [NameInMap("model")]
            [Validation(Required=true)]
            public MainFileErrorModelModel Model { get; set; }
            public class MainFileErrorModelModel : TeaModel {
                [NameInMap("str")]
                [Validation(Required=true)]
                public string Str { get; set; }

                public new MainFileErrorModelModel Copy()
                {
                    MainFileErrorModelModel copy = FromMap(ToMap());
                    return copy;
                }

                public new MainFileErrorModelModel CopyWithoutStream()
                {
                    MainFileErrorModelModel copy = FromMap(ToMap(true));
                    return copy;
                }

                public new void Validate()
                {
                    TeaModel.ValidateRequired("Str", Str, true);
                    base.Validate();
                }

                public new Dictionary<string, object> ToMap(bool noStream = false)
                {
                    var map = new Dictionary<string, object>();
                    if (Str != null)
                    {
                        map["str"] = Str;
                    }

                    return map;
                }

                public static new MainFileErrorModelModel FromMap(Dictionary<string, object> map)
                {
                    var model = new MainFileErrorModelModel();
                    if (map.ContainsKey("str"))
                    {
                        model.Str = (string)map["str"];
                    }

                    return model;
                }
            }

            public new MainFileErrorModel Copy()
            {
                MainFileErrorModel copy = FromMap(ToMap());
                return copy;
            }

            public new MainFileErrorModel CopyWithoutStream()
            {
                MainFileErrorModel copy = FromMap(ToMap(true));
                return copy;
            }

            public new void Validate()
            {
                TeaModel.ValidateRequired("Str", Str, true);
                if (Model != null) {
                    Model.Validate();
                }
                TeaModel.ValidateRequired("Model", Model, true);
                base.Validate();
            }

            public new Dictionary<string, object> ToMap(bool noStream = false)
            {
                var map = new Dictionary<string, object>();
                if (Str != null)
                {
                    map["str"] = Str;
                }

                if (Model != null)
                {
                    map["model"] = Model != null ? Model.ToMap(noStream) : null;
                }

                return map;
            }

            public static new MainFileErrorModel FromMap(Dictionary<string, object> map)
            {
                var model = new MainFileErrorModel();
                if (map.ContainsKey("str"))
                {
                    model.Str = (string)map["str"];
                }

                if (map.ContainsKey("model"))
                {
                    if (map["model"] != null)
                    {
                        var temp = (Dictionary<string, object>)map["model"];
                        model.Model = MainFileErrorModelModel.FromMap(temp);
                    }
                }

                return model;
            }
        }
        public MainFileErrorModel1 Model1 { get; set; }
        public class MainFileErrorModel1 : TeaModel
        {
            [NameInMap("code")]
            [Validation(Required=true)]
            public int? Code { get; set; }

            public new MainFileErrorModel1 Copy()
            {
                MainFileErrorModel1 copy = FromMap(ToMap());
                return copy;
            }

            public new MainFileErrorModel1 CopyWithoutStream()
            {
                MainFileErrorModel1 copy = FromMap(ToMap(true));
                return copy;
            }

            public new void Validate()
            {
                TeaModel.ValidateRequired("Code", Code, true);
                base.Validate();
            }

            public new Dictionary<string, object> ToMap(bool noStream = false)
            {
                var map = new Dictionary<string, object>();
                if (Code != null)
                {
                    map["code"] = Code;
                }

                return map;
            }

            public static new MainFileErrorModel1 FromMap(Dictionary<string, object> map)
            {
                var model = new MainFileErrorModel1();
                if (map.ContainsKey("code"))
                {
                    model.Code = (int?)map["code"];
                }

                return model;
            }
        }

        public class MainFileErrorModel : TeaModel
        {
            [NameInMap("str")]
            [Validation(Required=true)]
            public string Str { get; set; }

            [NameInMap("model")]
            [Validation(Required=true)]
            public MainFileErrorModelModel Model { get; set; }
            public class MainFileErrorModelModel : TeaModel {
                [NameInMap("str")]
                [Validation(Required=true)]
                public string Str { get; set; }

                public new MainFileErrorModelModel Copy()
                {
                    MainFileErrorModelModel copy = FromMap(ToMap());
                    return copy;
                }

                public new MainFileErrorModelModel CopyWithoutStream()
                {
                    MainFileErrorModelModel copy = FromMap(ToMap(true));
                    return copy;
                }

                public new void Validate()
                {
                    TeaModel.ValidateRequired("Str", Str, true);
                    base.Validate();
                }

                public new Dictionary<string, object> ToMap(bool noStream = false)
                {
                    var map = new Dictionary<string, object>();
                    if (Str != null)
                    {
                        map["str"] = Str;
                    }

                    return map;
                }

                public static new MainFileErrorModelModel FromMap(Dictionary<string, object> map)
                {
                    var model = new MainFileErrorModelModel();
                    if (map.ContainsKey("str"))
                    {
                        model.Str = (string)map["str"];
                    }

                    return model;
                }
            }

            public new MainFileErrorModel Copy()
            {
                MainFileErrorModel copy = FromMap(ToMap());
                return copy;
            }

            public new MainFileErrorModel CopyWithoutStream()
            {
                MainFileErrorModel copy = FromMap(ToMap(true));
                return copy;
            }

            public new void Validate()
            {
                TeaModel.ValidateRequired("Str", Str, true);
                if (Model != null) {
                    Model.Validate();
                }
                TeaModel.ValidateRequired("Model", Model, true);
                base.Validate();
            }

            public new Dictionary<string, object> ToMap(bool noStream = false)
            {
                var map = new Dictionary<string, object>();
                if (Str != null)
                {
                    map["str"] = Str;
                }

                if (Model != null)
                {
                    map["model"] = Model != null ? Model.ToMap(noStream) : null;
                }

                return map;
            }

            public static new MainFileErrorModel FromMap(Dictionary<string, object> map)
            {
                var model = new MainFileErrorModel();
                if (map.ContainsKey("str"))
                {
                    model.Str = (string)map["str"];
                }

                if (map.ContainsKey("model"))
                {
                    if (map["model"] != null)
                    {
                        var temp = (Dictionary<string, object>)map["model"];
                        model.Model = MainFileErrorModelModel.FromMap(temp);
                    }
                }

                return model;
            }
        }
        public class MainFileErrorModel1 : TeaModel
        {
            [NameInMap("code")]
            [Validation(Required=true)]
            public int? Code { get; set; }

            public new MainFileErrorModel1 Copy()
            {
                MainFileErrorModel1 copy = FromMap(ToMap());
                return copy;
            }

            public new MainFileErrorModel1 CopyWithoutStream()
            {
                MainFileErrorModel1 copy = FromMap(ToMap(true));
                return copy;
            }

            public new void Validate()
            {
                TeaModel.ValidateRequired("Code", Code, true);
                base.Validate();
            }

            public new Dictionary<string, object> ToMap(bool noStream = false)
            {
                var map = new Dictionary<string, object>();
                if (Code != null)
                {
                    map["code"] = Code;
                }

                return map;
            }

            public static new MainFileErrorModel1 FromMap(Dictionary<string, object> map)
            {
                var model = new MainFileErrorModel1();
                if (map.ContainsKey("code"))
                {
                    model.Code = (int?)map["code"];
                }

                return model;
            }
        }
        public MainFileError() : base()
        {
        }
    }

}

