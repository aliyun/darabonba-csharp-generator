// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Darabonba;
using Darabonba.Utils;

namespace Darabonba.Test.Models
{
    public class MyModel : Darabonba.Model {
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
        public class MyModelSubmodel : Darabonba.Model {
            [NameInMap("stringfield")]
            [Validation(Required=true)]
            public string Stringfield { get; set; }

            public override Dictionary<string, object> ToMapCore(bool noStream = false)
            {
                var map = new Dictionary<string, object>();
                map["stringfield"] = this.Stringfield;
                return map;
            }

            public new static MyModelSubmodel FromMap(IDictionary map)
            {
                if (map == null)
                {
                    return null;
                }
                var model = new MyModelSubmodel();
                if (map.Contains("stringfield"))
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
        public Darabonba.Request Request { get; set; }

        [NameInMap("m")]
        [Validation(Required=true)]
        public Darabonba.Model M { get; set; }

        [NameInMap("mapModel")]
        [Validation(Required=true)]
        public Dictionary<string, LowerModel> MapModel { get; set; }

        [NameInMap("submodelMap")]
        [Validation(Required=true)]
        public Dictionary<string, MyModel.MyModelSubmodel> SubmodelMap { get; set; }

        public override Dictionary<string, object> ToMapCore(bool noStream = false)
        {
            var map = new Dictionary<string, object>();
            map["stringfield"] = this.Stringfield;
            {
                List<object> value0 = null;
                if (this.Stringarrayfield != null)
                {
                    value0 = new List<object>();
                    foreach (var item0 in this.Stringarrayfield)
                    {
                        value0.Add(item0);
                    }
                }
                map["stringarrayfield"] = value0;
            }
            {
                Dictionary<string, object> value0 = null;
                if (this.Mapfield != null)
                {
                    value0 = new Dictionary<string, object>();
                    foreach (var item0 in this.Mapfield)
                    {
                        value0.Add(item0.Key, item0.Value);
                    }
                }
                map["mapfield"] = value0;
            }
            map["realName"] = this.Name;
            map["submodel"] = this.Submodel == null ? null : this.Submodel.ToMap();
            {
                Dictionary<string, object> value0 = null;
                if (this.Object != null)
                {
                    value0 = new Dictionary<string, object>();
                    foreach (var item0 in this.Object)
                    {
                        value0.Add(item0.Key, item0.Value);
                    }
                }
                map["object"] = value0;
            }
            map["numberfield"] = this.Numberfield;
            map["request"] = this.Request == null ? null : this.Request.ToMap();
            map["m"] = this.M == null ? null : this.M.ToMap();
            {
                Dictionary<string, object> value0 = null;
                if (this.MapModel != null)
                {
                    value0 = new Dictionary<string, object>();
                    foreach (var item0 in this.MapModel)
                    {
                        value0.Add(item0.Key, item0.Value == null ? null : item0.Value.ToMap());
                    }
                }
                map["mapModel"] = value0;
            }
            {
                Dictionary<string, object> value0 = null;
                if (this.SubmodelMap != null)
                {
                    value0 = new Dictionary<string, object>();
                    foreach (var item0 in this.SubmodelMap)
                    {
                        value0.Add(item0.Key, item0.Value == null ? null : item0.Value.ToMap());
                    }
                }
                map["submodelMap"] = value0;
            }
            return map;
        }

        public new static MyModel FromMap(IDictionary map)
        {
            if (map == null)
            {
                return null;
            }
            var model = new MyModel();
            if (map.Contains("stringfield"))
            {
                model.Stringfield = (string)map["stringfield"];
            }
            if (map.Contains("stringarrayfield"))
            {
                List<string> value0 = null;
                if (map["stringarrayfield"] != null)
                {
                    value0 = new List<string>();
                    foreach (var item0 in (IList)map["stringarrayfield"])
                    {
                        value0.Add((string)item0);
                    }
                }
                model.Stringarrayfield = value0;
            }
            if (map.Contains("mapfield"))
            {
                Dictionary<string, string> value0 = null;
                if (map["mapfield"] != null)
                {
                    value0 = new Dictionary<string, string>();
                    foreach (DictionaryEntry item0 in (IDictionary)map["mapfield"])
                    {
                        value0.Add((string)item0.Key, (string)item0.Value);
                    }
                }
                model.Mapfield = value0;
            }
            if (map.Contains("realName"))
            {
                model.Name = (string)map["realName"];
            }
            if (map.Contains("submodel"))
            {
                model.Submodel = map["submodel"] == null ? null : MyModelSubmodel.FromMap((IDictionary)map["submodel"]);
            }
            if (map.Contains("object"))
            {
                Dictionary<string, object> value0 = null;
                if (map["object"] != null)
                {
                    value0 = new Dictionary<string, object>();
                    foreach (DictionaryEntry item0 in (IDictionary)map["object"])
                    {
                        value0.Add((string)item0.Key, (object)item0.Value);
                    }
                }
                model.Object = value0;
            }
            if (map.Contains("numberfield"))
            {
                model.Numberfield = map["numberfield"] == null ? (int?)null : System.Convert.ToInt32(map["numberfield"]);
            }
            if (map.Contains("readable"))
            {
                model.Readable = (Stream)map["readable"];
            }
            if (map.Contains("request"))
            {
                model.Request = map["request"] == null ? null : Darabonba.Request.FromMap((IDictionary)map["request"]);
            }
            if (map.Contains("m"))
            {
                model.M = map["m"] == null ? null : Darabonba.Model.FromMap((IDictionary)map["m"]);
            }
            if (map.Contains("mapModel"))
            {
                Dictionary<string, LowerModel> value0 = null;
                if (map["mapModel"] != null)
                {
                    value0 = new Dictionary<string, LowerModel>();
                    foreach (DictionaryEntry item0 in (IDictionary)map["mapModel"])
                    {
                        value0.Add((string)item0.Key, item0.Value == null ? null : Darabonba.Test.Models.LowerModel.FromMap((IDictionary)item0.Value));
                    }
                }
                model.MapModel = value0;
            }
            if (map.Contains("submodelMap"))
            {
                Dictionary<string, MyModel.MyModelSubmodel> value0 = null;
                if (map["submodelMap"] != null)
                {
                    value0 = new Dictionary<string, MyModel.MyModelSubmodel>();
                    foreach (DictionaryEntry item0 in (IDictionary)map["submodelMap"])
                    {
                        value0.Add((string)item0.Key, item0.Value == null ? null : Darabonba.Test.Models.MyModel.MyModelSubmodel.FromMap((IDictionary)item0.Value));
                    }
                }
                model.SubmodelMap = value0;
            }
            return model;
        }
    }

}

