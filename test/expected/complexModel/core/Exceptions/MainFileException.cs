// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Darabonba.Exceptions;
using Darabonba.Test.Models;

namespace Darabonba.Test.Exceptions
{
    public class MainFileException : DaraException {
        public int? Size { get; set; }
        public Dictionary<string, Model> Data { get; set; }
        public MainFileExceptionModel Model { get; set; }
        public class MainFileExceptionModel : Model
        {
            [NameInMap("str")]
            [Validation(Required=true)]
            public string Str { get; set; }

            [NameInMap("model")]
            [Validation(Required=true)]
            public MainFileExceptionModelModel Model { get; set; }
            public class MainFileExceptionModelModel : Model {
                [NameInMap("str")]
                [Validation(Required=true)]
                public string Str { get; set; }

                public MainFileExceptionModelModel Copy()
                {
                    MainFileExceptionModelModel copy = FromMap(ToMap());
                    return copy;
                }

                public MainFileExceptionModelModel CopyWithoutStream()
                {
                    MainFileExceptionModelModel copy = FromMap(ToMap(true));
                    return copy;
                }

                public Dictionary<string, object> ToMap(bool noStream = false)
                {
                    var map = new Dictionary<string, object>();
                    if (Str != null)
                    {
                        map["str"] = Str;
                    }

                    return map;
                }

                public static MainFileExceptionModelModel FromMap(Dictionary<string, object> map)
                {
                    var model = new MainFileExceptionModelModel();
                    if (map.ContainsKey("str"))
                    {
                        model.Str = (string)map["str"];
                    }

                    return model;
                }
            }

            public MainFileExceptionModel Copy()
            {
                MainFileExceptionModel copy = FromMap(ToMap());
                return copy;
            }

            public MainFileExceptionModel CopyWithoutStream()
            {
                MainFileExceptionModel copy = FromMap(ToMap(true));
                return copy;
            }

            public Dictionary<string, object> ToMap(bool noStream = false)
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

            public static MainFileExceptionModel FromMap(Dictionary<string, object> map)
            {
                var model = new MainFileExceptionModel();
                if (map.ContainsKey("str"))
                {
                    model.Str = (string)map["str"];
                }

                if (map.ContainsKey("model"))
                {
                    if (map["model"] != null)
                    {
                        var temp = (Dictionary<string, object>)map["model"];
                        model.Model = MainFileExceptionModelModel.FromMap(temp);
                    }
                }

                return model;
            }
        }
        public MainFileExceptionModel1 Model1 { get; set; }
        public class MainFileExceptionModel1 : Model
        {
            [NameInMap("code")]
            [Validation(Required=true)]
            public int? Code { get; set; }

            public MainFileExceptionModel1 Copy()
            {
                MainFileExceptionModel1 copy = FromMap(ToMap());
                return copy;
            }

            public MainFileExceptionModel1 CopyWithoutStream()
            {
                MainFileExceptionModel1 copy = FromMap(ToMap(true));
                return copy;
            }

            public Dictionary<string, object> ToMap(bool noStream = false)
            {
                var map = new Dictionary<string, object>();
                if (Code != null)
                {
                    map["code"] = Code;
                }

                return map;
            }

            public static MainFileExceptionModel1 FromMap(Dictionary<string, object> map)
            {
                var model = new MainFileExceptionModel1();
                if (map.ContainsKey("code"))
                {
                    model.Code = (int?)map["code"];
                }

                return model;
            }
        }

        public class MainFileExceptionModel : Model
        {
            [NameInMap("str")]
            [Validation(Required=true)]
            public string Str { get; set; }

            [NameInMap("model")]
            [Validation(Required=true)]
            public MainFileExceptionModelModel Model { get; set; }
            public class MainFileExceptionModelModel : Model {
                [NameInMap("str")]
                [Validation(Required=true)]
                public string Str { get; set; }

                public MainFileExceptionModelModel Copy()
                {
                    MainFileExceptionModelModel copy = FromMap(ToMap());
                    return copy;
                }

                public MainFileExceptionModelModel CopyWithoutStream()
                {
                    MainFileExceptionModelModel copy = FromMap(ToMap(true));
                    return copy;
                }

                public Dictionary<string, object> ToMap(bool noStream = false)
                {
                    var map = new Dictionary<string, object>();
                    if (Str != null)
                    {
                        map["str"] = Str;
                    }

                    return map;
                }

                public static MainFileExceptionModelModel FromMap(Dictionary<string, object> map)
                {
                    var model = new MainFileExceptionModelModel();
                    if (map.ContainsKey("str"))
                    {
                        model.Str = (string)map["str"];
                    }

                    return model;
                }
            }

            public MainFileExceptionModel Copy()
            {
                MainFileExceptionModel copy = FromMap(ToMap());
                return copy;
            }

            public MainFileExceptionModel CopyWithoutStream()
            {
                MainFileExceptionModel copy = FromMap(ToMap(true));
                return copy;
            }

            public Dictionary<string, object> ToMap(bool noStream = false)
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

            public static MainFileExceptionModel FromMap(Dictionary<string, object> map)
            {
                var model = new MainFileExceptionModel();
                if (map.ContainsKey("str"))
                {
                    model.Str = (string)map["str"];
                }

                if (map.ContainsKey("model"))
                {
                    if (map["model"] != null)
                    {
                        var temp = (Dictionary<string, object>)map["model"];
                        model.Model = MainFileExceptionModelModel.FromMap(temp);
                    }
                }

                return model;
            }
        }
        public class MainFileExceptionModel1 : Model
        {
            [NameInMap("code")]
            [Validation(Required=true)]
            public int? Code { get; set; }

            public MainFileExceptionModel1 Copy()
            {
                MainFileExceptionModel1 copy = FromMap(ToMap());
                return copy;
            }

            public MainFileExceptionModel1 CopyWithoutStream()
            {
                MainFileExceptionModel1 copy = FromMap(ToMap(true));
                return copy;
            }

            public Dictionary<string, object> ToMap(bool noStream = false)
            {
                var map = new Dictionary<string, object>();
                if (Code != null)
                {
                    map["code"] = Code;
                }

                return map;
            }

            public static MainFileExceptionModel1 FromMap(Dictionary<string, object> map)
            {
                var model = new MainFileExceptionModel1();
                if (map.ContainsKey("code"))
                {
                    model.Code = (int?)map["code"];
                }

                return model;
            }
        }
        public MainFileException() : base()
        {
        }
    }

}

