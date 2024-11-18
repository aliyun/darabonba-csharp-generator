// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Darabonba;

namespace Darabonba.Test.Models
{
    public class MyModel : Model {
        [NameInMap("stringfield")]
        [Validation(Required=true)]
        public string Stringfield { get; set; }

        [NameInMap("stringarrayfield")]
        [Validation(Required=true)]
        public List<string> Stringarrayfield { get; set; }

        [NameInMap("mapfield")]
        [Validation(Required=true)]
        public Dictionary<string, string> Mapfield { get; set; }

        [NameInMap("realName")]
        [Validation(Required=true)]
        public string Name { get; set; }

        [NameInMap("submodel")]
        [Validation(Required=true)]
        public MyModelSubmodel Submodel { get; set; }
        public class MyModelSubmodel : Model {
            [NameInMap("stringfield")]
            [Validation(Required=true)]
            public string Stringfield { get; set; }

            public MyModelSubmodel Copy()
            {
                MyModelSubmodel copy = FromMap(ToMap());
                return copy;
            }

            public MyModelSubmodel CopyWithoutStream()
            {
                MyModelSubmodel copy = FromMap(ToMap(true));
                return copy;
            }

            public Dictionary<string, object> ToMap(bool noStream = false)
            {
                var map = new Dictionary<string, object>();
                if (Stringfield != null)
                {
                    map["stringfield"] = Stringfield;
                }

                return map;
            }

            public static MyModelSubmodel FromMap(Dictionary<string, object> map)
            {
                var model = new MyModelSubmodel();
                if (map.ContainsKey("stringfield"))
                {
                    model.Stringfield = (string)map["stringfield"];
                }

                return model;
            }
        }

        [NameInMap("object")]
        [Validation(Required=true)]
        public Dictionary<string, object> Object { get; set; }

        [NameInMap("numberfield")]
        [Validation(Required=true)]
        public int? Numberfield { get; set; }

        [NameInMap("readable")]
        [Validation(Required=true)]
        public Stream Readable { get; set; }

        [NameInMap("request")]
        [Validation(Required=true)]
        public Request Request { get; set; }

        [NameInMap("m")]
        [Validation(Required=true)]
        public Model M { get; set; }

        [NameInMap("mapModel")]
        [Validation(Required=true)]
        public Dictionary<string, LowerModel> MapModel { get; set; }

        [NameInMap("submodelMap")]
        [Validation(Required=true)]
        public Dictionary<string, MyModel.MyModelSubmodel> SubmodelMap { get; set; }

        public MyModel Copy()
        {
            MyModel copy = FromMap(ToMap());
            return copy;
        }

        public MyModel CopyWithoutStream()
        {
            MyModel copy = FromMap(ToMap(true));
            return copy;
        }

        public Dictionary<string, object> ToMap(bool noStream = false)
        {
            var map = new Dictionary<string, object>();
            if (Stringfield != null)
            {
                map["stringfield"] = Stringfield;
            }

            if (Stringarrayfield != null)
            {
                var list1 = new List<string>();
                int n1 = 0;
                foreach (var item1 in Stringarrayfield) 
                {
                    list1[n1++] = item1;
                }
                map["stringarrayfield"] = list1;
            }

            if (Mapfield != null)
            {
                var dict = new Dictionary<string, string>();
                foreach (var item1 in Mapfield) 
                {
                    dict[item1.Key] = item1.Value;
                }
                map["mapfield"] = dict;
            }

            if (Name != null)
            {
                map["realName"] = Name;
            }

            if (Submodel != null)
            {
                map["submodel"] = Submodel != null ? Submodel.ToMap(noStream) : null;
            }

            if (Object != null)
            {
                map["object"] = Object;
            }

            if (Numberfield != null)
            {
                map["numberfield"] = Numberfield;
            }

            if (Readable != null)
            {
                map["readable"] = Readable;
            }

            if (Request != null)
            {
                map["request"] = Request != null ? Request.ToMap(noStream) : null;
            }

            if (M != null)
            {
                map["m"] = M != null ? M.ToMap(noStream) : null;
            }

            if (MapModel != null)
            {
                var dict = new Dictionary<string, Dictionary<string, object>>();
                foreach (var item1 in MapModel) 
                {
                    dict[item1.Key] = item1.Value != null ? item1.Value.ToMap(noStream) : null;
                }
                map["mapModel"] = dict;
            }

            if (SubmodelMap != null)
            {
                var dict = new Dictionary<string, Dictionary<string, object>>();
                foreach (var item1 in SubmodelMap) 
                {
                    dict[item1.Key] = item1.Value != null ? item1.Value.ToMap(noStream) : null;
                }
                map["submodelMap"] = dict;
            }

            return map;
        }

        public static MyModel FromMap(Dictionary<string, object> map)
        {
            var model = new MyModel();
            if (map.ContainsKey("stringfield"))
            {
                model.Stringfield = (string)map["stringfield"];
            }

            if (map.ContainsKey("stringarrayfield"))
            {
                var list1 = map["stringarrayfield"] as List<string>;
                if (list1 != null && list1.Count > 0)
                {
                    int n1 = 0;
                    var modelList1 = new List<string>();
                    foreach(var item1 in list1)
                    {
                        modelList1[n1++] = (string)item1;
                    }
                    model.Stringarrayfield = modelList1;
                }
            }

            if (map.ContainsKey("mapfield"))
            {
                var dict = map["mapfield"] as Dictionary<string, string>;
                if (dict != null && dict.Count > 0)
                {
                    var modelMap1 = new Dictionary<string, string>();
                    foreach (KeyValuePair<string, string> entry1 in dict)
                    {
                        modelMap1[entry1.Key] = (string)entry1.Value;
                    }
                    model.Mapfield = modelMap1;
                }
            }

            if (map.ContainsKey("realName"))
            {
                model.Name = (string)map["realName"];
            }

            if (map.ContainsKey("submodel"))
            {
                if (map["submodel"] != null)
                {
                    var temp = (Dictionary<string, object>)map["submodel"];
                    model.Submodel = MyModelSubmodel.FromMap(temp);
                }
            }

            if (map.ContainsKey("object"))
            {
                model.Object = (Dictionary<string, object>)map["object"];
            }

            if (map.ContainsKey("numberfield"))
            {
                model.Numberfield = (int?)map["numberfield"];
            }

            if (map.ContainsKey("readable"))
            {
                model.Readable = (Stream)map["readable"];
            }

            if (map.ContainsKey("request"))
            {
                var temp = (Dictionary<string, object>)map["request"];
                model.Request = Request.FromMap(temp);
            }

            if (map.ContainsKey("m"))
            {
                var temp = (Dictionary<string, object>)map["m"];
                model.M = Model.FromMap(temp);
            }

            if (map.ContainsKey("mapModel"))
            {
                var dict = map["mapModel"] as Dictionary<string, Dictionary<string, object>>;
                if (dict != null && dict.Count > 0)
                {
                    var modelMap1 = new Dictionary<string, LowerModel>();
                    foreach (KeyValuePair<string, Dictionary<string, object>> entry1 in dict)
                    {
                        modelMap1[entry1.Key] = LowerModel.FromMap(entry1.Value);
                    }
                    model.MapModel = modelMap1;
                }
            }

            if (map.ContainsKey("submodelMap"))
            {
                var dict = map["submodelMap"] as Dictionary<string, Dictionary<string, object>>;
                if (dict != null && dict.Count > 0)
                {
                    var modelMap1 = new Dictionary<string, MyModelSubmodel>();
                    foreach (KeyValuePair<string, Dictionary<string, object>> entry1 in dict)
                    {
                        modelMap1[entry1.Key] = MyModelSubmodel.FromMap(entry1.Value);
                    }
                    model.SubmodelMap = modelMap1;
                }
            }

            return model;
        }
    }

}

