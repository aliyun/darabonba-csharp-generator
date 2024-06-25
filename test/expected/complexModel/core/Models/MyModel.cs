// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Tea;
using SourceClient = Darabonba.import.Client;
using Darabonba.import.Models;

namespace Darabonba.Test.Models
{
    public class MyModel : TeaModel {
        [NameInMap("model")]
        [Validation(Required=true)]
        public MyModelModel Model { get; set; }
        public class MyModelModel : TeaModel {
            [NameInMap("str")]
            [Validation(Required=true)]
            public string Str { get; set; }

            [NameInMap("model")]
            [Validation(Required=true)]
            public MyModelModelModel Model { get; set; }
            public class MyModelModelModel : TeaModel {
                [NameInMap("str")]
                [Validation(Required=true)]
                public string Str { get; set; }

                public new MyModelModelModel Copy()
                {
                    MyModelModelModel copy = FromMap(ToMap());
                    return copy;
                }

                public new MyModelModelModel CopyWithoutStream()
                {
                    MyModelModelModel copy = FromMap(ToMap(true));
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

                public static new MyModelModelModel FromMap(Dictionary<string, object> map)
                {
                    var model = new MyModelModelModel();
                    if (map.ContainsKey("str"))
                    {
                        model.Str = (string)map["str"];
                    }

                    return model;
                }
            }

            public new MyModelModel Copy()
            {
                MyModelModel copy = FromMap(ToMap());
                return copy;
            }

            public new MyModelModel CopyWithoutStream()
            {
                MyModelModel copy = FromMap(ToMap(true));
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

            public static new MyModelModel FromMap(Dictionary<string, object> map)
            {
                var model = new MyModelModel();
                if (map.ContainsKey("str"))
                {
                    model.Str = (string)map["str"];
                }

                if (map.ContainsKey("model"))
                {
                    if (map["model"] != null)
                    {
                        var temp = (Dictionary<string, object>)map["model"];
                        model.Model = MyModelModelModel.FromMap(temp);
                    }
                }

                return model;
            }
        }

        [NameInMap("stringfield")]
        [Validation(Required=true)]
        public string Stringfield { get; set; }

        [NameInMap("bytesfield")]
        [Validation(Required=true)]
        public byte[] Bytesfield { get; set; }

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
        public class MyModelSubmodel : TeaModel {
            [NameInMap("stringfield")]
            [Validation(Required=true)]
            public string Stringfield { get; set; }

            [NameInMap("model")]
            [Validation(Required=true)]
            public MyModelSubmodelModel Model { get; set; }
            public class MyModelSubmodelModel : TeaModel {
                [NameInMap("str")]
                [Validation(Required=true)]
                public string Str { get; set; }

                public new MyModelSubmodelModel Copy()
                {
                    MyModelSubmodelModel copy = FromMap(ToMap());
                    return copy;
                }

                public new MyModelSubmodelModel CopyWithoutStream()
                {
                    MyModelSubmodelModel copy = FromMap(ToMap(true));
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

                public static new MyModelSubmodelModel FromMap(Dictionary<string, object> map)
                {
                    var model = new MyModelSubmodelModel();
                    if (map.ContainsKey("str"))
                    {
                        model.Str = (string)map["str"];
                    }

                    return model;
                }
            }

            public new MyModelSubmodel Copy()
            {
                MyModelSubmodel copy = FromMap(ToMap());
                return copy;
            }

            public new MyModelSubmodel CopyWithoutStream()
            {
                MyModelSubmodel copy = FromMap(ToMap(true));
                return copy;
            }

            public new void Validate()
            {
                TeaModel.ValidateRequired("Stringfield", Stringfield, true);
                if (Model != null) {
                    Model.Validate();
                }
                TeaModel.ValidateRequired("Model", Model, true);
                base.Validate();
            }

            public new Dictionary<string, object> ToMap(bool noStream = false)
            {
                var map = new Dictionary<string, object>();
                if (Stringfield != null)
                {
                    map["stringfield"] = Stringfield;
                }

                if (Model != null)
                {
                    map["model"] = Model != null ? Model.ToMap(noStream) : null;
                }

                return map;
            }

            public static new MyModelSubmodel FromMap(Dictionary<string, object> map)
            {
                var model = new MyModelSubmodel();
                if (map.ContainsKey("stringfield"))
                {
                    model.Stringfield = (string)map["stringfield"];
                }

                if (map.ContainsKey("model"))
                {
                    if (map["model"] != null)
                    {
                        var temp = (Dictionary<string, object>)map["model"];
                        model.Model = MyModelSubmodelModel.FromMap(temp);
                    }
                }

                return model;
            }
        }

        [NameInMap("submodelMap")]
        [Validation(Required=true)]
        public Dictionary<string, MyModel.MyModelSubmodel> SubmodelMap { get; set; }

        [NameInMap("mapModel")]
        [Validation(Required=true)]
        public Dictionary<string, M> MapModel { get; set; }

        [NameInMap("subarraymodel")]
        [Validation(Required=true)]
        public List<MyModelSubarraymodel> Subarraymodel { get; set; }
        public class MyModelSubarraymodel : TeaModel {
            public new MyModelSubarraymodel Copy()
            {
                MyModelSubarraymodel copy = FromMap(ToMap());
                return copy;
            }

            public new MyModelSubarraymodel CopyWithoutStream()
            {
                MyModelSubarraymodel copy = FromMap(ToMap(true));
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

            public static new MyModelSubarraymodel FromMap(Dictionary<string, object> map)
            {
                var model = new MyModelSubarraymodel();
                return model;
            }
        }

        [NameInMap("subarray")]
        [Validation(Required=true)]
        public List<M> Subarray { get; set; }

        [NameInMap("ssubarray")]
        [Validation(Required=true)]
        public List<List<List<M>>> Ssubarray { get; set; }

        [NameInMap("ssubmarray")]
        [Validation(Required=true)]
        public List<List<SourceClient>> Ssubmarray { get; set; }

        [NameInMap("ssubmmarray")]
        [Validation(Required=true)]
        public List<List<Request>> Ssubmmarray { get; set; }

        [NameInMap("maparray")]
        [Validation(Required=true)]
        public List<Dictionary<string, object>> Maparray { get; set; }

        [NameInMap("mapsubmarray")]
        [Validation(Required=true)]
        public List<Dictionary<string, SourceClient>> Mapsubmarray { get; set; }

        [NameInMap("moduleModelMap")]
        [Validation(Required=true)]
        public Dictionary<string, Request> ModuleModelMap { get; set; }

        [NameInMap("subModelMap")]
        [Validation(Required=true)]
        public Dictionary<string, M.MSubM> SubModelMap { get; set; }

        [NameInMap("modelMap")]
        [Validation(Required=true)]
        public Dictionary<string, M> ModelMap { get; set; }

        [NameInMap("moduleMap")]
        [Validation(Required=true)]
        public Dictionary<string, SourceClient> ModuleMap { get; set; }

        [NameInMap("object")]
        [Validation(Required=true)]
        public Dictionary<string, object> Object { get; set; }

        [NameInMap("readable")]
        [Validation(Required=true)]
        public Stream Readable { get; set; }

        [NameInMap("writable")]
        [Validation(Required=true)]
        public Stream Writable { get; set; }

        [NameInMap("existModel")]
        [Validation(Required=true)]
        public M ExistModel { get; set; }

        [NameInMap("request")]
        [Validation(Required=true)]
        public TeaRequest Request { get; set; }

        [NameInMap("complexList")]
        [Validation(Required=true)]
        public List<List<string>> ComplexList { get; set; }

        [NameInMap("numberfield")]
        [Validation(Required=true)]
        public int? Numberfield { get; set; }

        [NameInMap("integerField")]
        [Validation(Required=true)]
        public int? IntegerField { get; set; }

        [NameInMap("floatField")]
        [Validation(Required=true)]
        public float? FloatField { get; set; }

        [NameInMap("doubleField")]
        [Validation(Required=true)]
        public double? DoubleField { get; set; }

        [NameInMap("longField")]
        [Validation(Required=true)]
        public long? LongField { get; set; }

        [NameInMap("ulongField")]
        [Validation(Required=true)]
        public ulong? UlongField { get; set; }

        [NameInMap("int8Field")]
        [Validation(Required=true)]
        public int? Int8Field { get; set; }

        [NameInMap("int16Field")]
        [Validation(Required=true)]
        public int? Int16Field { get; set; }

        [NameInMap("int32Field")]
        [Validation(Required=true)]
        public int? Int32Field { get; set; }

        [NameInMap("int64Field")]
        [Validation(Required=true)]
        public long? Int64Field { get; set; }

        [NameInMap("uint8Field")]
        [Validation(Required=true)]
        public uint? Uint8Field { get; set; }

        [NameInMap("uint16Field")]
        [Validation(Required=true)]
        public uint? Uint16Field { get; set; }

        [NameInMap("uint32Field")]
        [Validation(Required=true)]
        public uint? Uint32Field { get; set; }

        [NameInMap("uint64Field")]
        [Validation(Required=true)]
        public ulong? Uint64Field { get; set; }

        /// <summary>
        /// <b>Example:</b>
        /// <para>http://<em>/</em>.png</para>
        /// </summary>
        [NameInMap("link")]
        [Validation(Required=false)]
        public string Link { get; set; }

        public new MyModel Copy()
        {
            MyModel copy = FromMap(ToMap());
            return copy;
        }

        public new MyModel CopyWithoutStream()
        {
            MyModel copy = FromMap(ToMap(true));
            return copy;
        }

        public new void Validate()
        {
            if (Model != null) {
                Model.Validate();
            }
            TeaModel.ValidateRequired("Model", Model, true);
            TeaModel.ValidateRequired("Stringfield", Stringfield, true);
            TeaModel.ValidateRequired("Bytesfield", Bytesfield, true);
            if (Stringarrayfield is IList) {
                TeaModel.ValidateArray(Stringarrayfield);
            }
            TeaModel.ValidateRequired("Stringarrayfield", Stringarrayfield, true);
            if (Mapfield is IDictionary) {
                TeaModel.ValidateMap(Mapfield);
            }
            TeaModel.ValidateRequired("Mapfield", Mapfield, true);
            TeaModel.ValidateRequired("Name", Name, true);
            if (Submodel != null) {
                Submodel.Validate();
            }
            TeaModel.ValidateRequired("Submodel", Submodel, true);
            if (SubmodelMap is IDictionary) {
                TeaModel.ValidateMap(SubmodelMap);
            }
            TeaModel.ValidateRequired("SubmodelMap", SubmodelMap, true);
            if (MapModel is IDictionary) {
                TeaModel.ValidateMap(MapModel);
            }
            TeaModel.ValidateRequired("MapModel", MapModel, true);
            if (Subarraymodel is IList) {
                TeaModel.ValidateArray(Subarraymodel);
            }
            TeaModel.ValidateRequired("Subarraymodel", Subarraymodel, true);
            if (Subarray is IList) {
                TeaModel.ValidateArray(Subarray);
            }
            TeaModel.ValidateRequired("Subarray", Subarray, true);
            if (Ssubarray is IList) {
                TeaModel.ValidateArray(Ssubarray);
            }
            TeaModel.ValidateRequired("Ssubarray", Ssubarray, true);
            if (Ssubmarray is IList) {
                TeaModel.ValidateArray(Ssubmarray);
            }
            TeaModel.ValidateRequired("Ssubmarray", Ssubmarray, true);
            if (Ssubmmarray is IList) {
                TeaModel.ValidateArray(Ssubmmarray);
            }
            TeaModel.ValidateRequired("Ssubmmarray", Ssubmmarray, true);
            if (Maparray is IList) {
                TeaModel.ValidateArray(Maparray);
            }
            TeaModel.ValidateRequired("Maparray", Maparray, true);
            if (Mapsubmarray is IList) {
                TeaModel.ValidateArray(Mapsubmarray);
            }
            TeaModel.ValidateRequired("Mapsubmarray", Mapsubmarray, true);
            if (ModuleModelMap is IDictionary) {
                TeaModel.ValidateMap(ModuleModelMap);
            }
            TeaModel.ValidateRequired("ModuleModelMap", ModuleModelMap, true);
            if (SubModelMap is IDictionary) {
                TeaModel.ValidateMap(SubModelMap);
            }
            TeaModel.ValidateRequired("SubModelMap", SubModelMap, true);
            if (ModelMap is IDictionary) {
                TeaModel.ValidateMap(ModelMap);
            }
            TeaModel.ValidateRequired("ModelMap", ModelMap, true);
            if (ModuleMap is IDictionary) {
                TeaModel.ValidateMap(ModuleMap);
            }
            TeaModel.ValidateRequired("ModuleMap", ModuleMap, true);
            TeaModel.ValidateRequired("Object", Object, true);
            TeaModel.ValidateRequired("Readable", Readable, true);
            TeaModel.ValidateRequired("Writable", Writable, true);
            if (ExistModel != null) {
                ExistModel.Validate();
            }
            TeaModel.ValidateRequired("ExistModel", ExistModel, true);
            TeaModel.ValidateRequired("Request", Request, true);
            if (ComplexList is IList) {
                TeaModel.ValidateArray(ComplexList);
            }
            TeaModel.ValidateRequired("ComplexList", ComplexList, true);
            TeaModel.ValidateRequired("Numberfield", Numberfield, true);
            TeaModel.ValidateRequired("IntegerField", IntegerField, true);
            TeaModel.ValidateRequired("FloatField", FloatField, true);
            TeaModel.ValidateRequired("DoubleField", DoubleField, true);
            TeaModel.ValidateRequired("LongField", LongField, true);
            TeaModel.ValidateRequired("UlongField", UlongField, true);
            TeaModel.ValidateRequired("Int8Field", Int8Field, true);
            TeaModel.ValidateRequired("Int16Field", Int16Field, true);
            TeaModel.ValidateRequired("Int32Field", Int32Field, true);
            TeaModel.ValidateRequired("Int64Field", Int64Field, true);
            TeaModel.ValidateRequired("Uint8Field", Uint8Field, true);
            TeaModel.ValidateRequired("Uint16Field", Uint16Field, true);
            TeaModel.ValidateRequired("Uint32Field", Uint32Field, true);
            TeaModel.ValidateRequired("Uint64Field", Uint64Field, true);
            base.Validate();
        }

        public new Dictionary<string, object> ToMap(bool noStream = false)
        {
            var map = new Dictionary<string, object>();
            if (Model != null)
            {
                map["model"] = Model != null ? Model.ToMap(noStream) : null;
            }

            if (Stringfield != null)
            {
                map["stringfield"] = Stringfield;
            }

            if (Bytesfield != null)
            {
                map["bytesfield"] = Bytesfield;
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

            if (SubmodelMap != null)
            {
                var dict = new Dictionary<string, Dictionary<string, object>>();
                foreach (var item1 in SubmodelMap) 
                {
                    dict[item1.Key] = item1.Value != null ? item1.Value.ToMap(noStream) : null;
                }
                map["submodelMap"] = dict;
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

            if (Subarraymodel != null)
            {
                var list1 = new List<Dictionary<string, object>>();
                int n1 = 0;
                foreach (var item1 in Subarraymodel) 
                {
                    list1[n1++] = item1 != null ? item1.ToMap(noStream) : null;
                }
                map["subarraymodel"] = list1;
            }

            if (Subarray != null)
            {
                var list1 = new List<Dictionary<string, object>>();
                int n1 = 0;
                foreach (var item1 in Subarray) 
                {
                    list1[n1++] = item1 != null ? item1.ToMap(noStream) : null;
                }
                map["subarray"] = list1;
            }

            if (Ssubarray != null)
            {
                var list1 = new List<List<List<Dictionary<string, object>>>>();
                int n1 = 0;
                foreach (var item1 in Ssubarray) 
                {
                    var list2 = new List<List<Dictionary<string, object>>>();
                    int n2 = 0;
                    foreach (var item2 in item1) 
                    {
                        var list3 = new List<Dictionary<string, object>>();
                        int n3 = 0;
                        foreach (var item3 in item2) 
                        {
                            list3[n3++] = item3 != null ? item3.ToMap(noStream) : null;
                        }
                        list2[n2++] = list3;
                    }
                    list1[n1++] = list2;
                }
                map["ssubarray"] = list1;
            }

            if (Ssubmarray != null)
            {
                var list1 = new List<List<Dictionary<string, object>>>();
                int n1 = 0;
                foreach (var item1 in Ssubmarray) 
                {
                    var list2 = new List<Dictionary<string, object>>();
                    int n2 = 0;
                    foreach (var item2 in item1) 
                    {
                        list2[n2++] = item2;
                    }
                    list1[n1++] = list2;
                }
                map["ssubmarray"] = list1;
            }

            if (Ssubmmarray != null)
            {
                var list1 = new List<List<Dictionary<string, object>>>();
                int n1 = 0;
                foreach (var item1 in Ssubmmarray) 
                {
                    var list2 = new List<Dictionary<string, object>>();
                    int n2 = 0;
                    foreach (var item2 in item1) 
                    {
                        list2[n2++] = item2 != null ? item2.ToMap(noStream) : null;
                    }
                    list1[n1++] = list2;
                }
                map["ssubmmarray"] = list1;
            }

            if (Maparray != null)
            {
                var list1 = new List<Dictionary<string, object>>();
                int n1 = 0;
                foreach (var item1 in Maparray) 
                {
                    var dict = new Dictionary<string, object>();
                    foreach (var item2 in item1) 
                    {
                        dict[item2.Key] = item2.Value;
                    }
                    list1[n1++] = dict;
                }
                map["maparray"] = list1;
            }

            if (Mapsubmarray != null)
            {
                var list1 = new List<Dictionary<string, Dictionary<string, object>>>();
                int n1 = 0;
                foreach (var item1 in Mapsubmarray) 
                {
                    var dict = new Dictionary<string, Dictionary<string, object>>();
                    foreach (var item2 in item1) 
                    {
                        dict[item2.Key] = item2.Value;
                    }
                    list1[n1++] = dict;
                }
                map["mapsubmarray"] = list1;
            }

            if (ModuleModelMap != null)
            {
                var dict = new Dictionary<string, Dictionary<string, object>>();
                foreach (var item1 in ModuleModelMap) 
                {
                    dict[item1.Key] = item1.Value != null ? item1.Value.ToMap(noStream) : null;
                }
                map["moduleModelMap"] = dict;
            }

            if (SubModelMap != null)
            {
                var dict = new Dictionary<string, Dictionary<string, object>>();
                foreach (var item1 in SubModelMap) 
                {
                    dict[item1.Key] = item1.Value != null ? item1.Value.ToMap(noStream) : null;
                }
                map["subModelMap"] = dict;
            }

            if (ModelMap != null)
            {
                var dict = new Dictionary<string, Dictionary<string, object>>();
                foreach (var item1 in ModelMap) 
                {
                    dict[item1.Key] = item1.Value != null ? item1.Value.ToMap(noStream) : null;
                }
                map["modelMap"] = dict;
            }

            if (ModuleMap != null)
            {
                var dict = new Dictionary<string, Dictionary<string, object>>();
                foreach (var item1 in ModuleMap) 
                {
                    dict[item1.Key] = item1.Value;
                }
                map["moduleMap"] = dict;
            }

            if (Object != null)
            {
                map["object"] = Object;
            }

            if (Readable != null)
            {
                map["readable"] = Readable;
            }

            if (Writable != null)
            {
                map["writable"] = Writable;
            }

            if (ExistModel != null)
            {
                map["existModel"] = ExistModel != null ? ExistModel.ToMap(noStream) : null;
            }

            if (Request != null)
            {
                map["request"] = Request != null ? Request.ToMap(noStream) : null;
            }

            if (ComplexList != null)
            {
                var list1 = new List<List<string>>();
                int n1 = 0;
                foreach (var item1 in ComplexList) 
                {
                    var list2 = new List<string>();
                    int n2 = 0;
                    foreach (var item2 in item1) 
                    {
                        list2[n2++] = item2;
                    }
                    list1[n1++] = list2;
                }
                map["complexList"] = list1;
            }

            if (Numberfield != null)
            {
                map["numberfield"] = Numberfield;
            }

            if (IntegerField != null)
            {
                map["integerField"] = IntegerField;
            }

            if (FloatField != null)
            {
                map["floatField"] = FloatField;
            }

            if (DoubleField != null)
            {
                map["doubleField"] = DoubleField;
            }

            if (LongField != null)
            {
                map["longField"] = LongField;
            }

            if (UlongField != null)
            {
                map["ulongField"] = UlongField;
            }

            if (Int8Field != null)
            {
                map["int8Field"] = Int8Field;
            }

            if (Int16Field != null)
            {
                map["int16Field"] = Int16Field;
            }

            if (Int32Field != null)
            {
                map["int32Field"] = Int32Field;
            }

            if (Int64Field != null)
            {
                map["int64Field"] = Int64Field;
            }

            if (Uint8Field != null)
            {
                map["uint8Field"] = Uint8Field;
            }

            if (Uint16Field != null)
            {
                map["uint16Field"] = Uint16Field;
            }

            if (Uint32Field != null)
            {
                map["uint32Field"] = Uint32Field;
            }

            if (Uint64Field != null)
            {
                map["uint64Field"] = Uint64Field;
            }

            if (Link != null)
            {
                map["link"] = Link;
            }

            return map;
        }

        public static new MyModel FromMap(Dictionary<string, object> map)
        {
            var model = new MyModel();
            if (map.ContainsKey("model"))
            {
                if (map["model"] != null)
                {
                    var temp = (Dictionary<string, object>)map["model"];
                    model.Model = MyModelModel.FromMap(temp);
                }
            }

            if (map.ContainsKey("stringfield"))
            {
                model.Stringfield = (string)map["stringfield"];
            }

            if (map.ContainsKey("bytesfield"))
            {
                model.Bytesfield = (byte[])map["bytesfield"];
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
                var dict = map["Mapfield"] as Dictionary<string, string>;
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

            if (map.ContainsKey("submodelMap"))
            {
                var dict = map["SubmodelMap"] as Dictionary<string, Dictionary<string, object>>;
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

            if (map.ContainsKey("mapModel"))
            {
                var dict = map["MapModel"] as Dictionary<string, Dictionary<string, object>>;
                if (dict != null && dict.Count > 0)
                {
                    var modelMap1 = new Dictionary<string, M>();
                    foreach (KeyValuePair<string, Dictionary<string, object>> entry1 in dict)
                    {
                        modelMap1[entry1.Key] = M.FromMap(entry1.Value);
                    }
                    model.MapModel = modelMap1;
                }
            }

            if (map.ContainsKey("subarraymodel"))
            {
                var list1 = map["subarraymodel"] as List<Dictionary<string, object>>;
                if (list1 != null && list1.Count > 0)
                {
                    int n1 = 0;
                    var modelList1 = new List<MyModelSubarraymodel>();
                    foreach(var item1 in list1)
                    {
                        if (item1 != null)
                        {
                            var temp = (Dictionary<string, object>)item1;
                            modelList1[n1++] = MyModelSubarraymodel.FromMap(temp);
                        }
                    }
                    model.Subarraymodel = modelList1;
                }
            }

            if (map.ContainsKey("subarray"))
            {
                var list1 = map["subarray"] as List<Dictionary<string, object>>;
                if (list1 != null && list1.Count > 0)
                {
                    int n1 = 0;
                    var modelList1 = new List<M>();
                    foreach(var item1 in list1)
                    {
                        modelList1[n1++] = M.FromMap(item1);
                    }
                    model.Subarray = modelList1;
                }
            }

            if (map.ContainsKey("ssubarray"))
            {
                var list1 = map["ssubarray"] as List<List<List<Dictionary<string, object>>>>;
                if (list1 != null && list1.Count > 0)
                {
                    int n1 = 0;
                    var modelList1 = new List<List<List<M>>>();
                    foreach(var item1 in list1)
                    {
                        var list2 = item1;
                        if (list2 != null && list2.Count > 0)
                        {
                            int n2 = 0;
                            var modelList2 = new List<List<M>>();
                            foreach(var item2 in list2)
                            {
                                var list3 = item2;
                                if (list3 != null && list3.Count > 0)
                                {
                                    int n3 = 0;
                                    var modelList3 = new List<M>();
                                    foreach(var item3 in list3)
                                    {
                                        modelList3[n3++] = M.FromMap(item3);
                                    }
                                    modelList2[n2++] = modelList3;
                                }
                            }
                            modelList1[n1++] = modelList2;
                        }
                    }
                    model.Ssubarray = modelList1;
                }
            }

            if (map.ContainsKey("ssubmarray"))
            {
                var list1 = map["ssubmarray"] as List<List<Dictionary<string, object>>>;
                if (list1 != null && list1.Count > 0)
                {
                    int n1 = 0;
                    var modelList1 = new List<List<SourceClient>>();
                    foreach(var item1 in list1)
                    {
                        var list2 = item1;
                        if (list2 != null && list2.Count > 0)
                        {
                            int n2 = 0;
                            var modelList2 = new List<SourceClient>();
                            foreach(var item2 in list2)
                            {
                                modelList2[n2++] = (SourceClient)item2;
                            }
                            modelList1[n1++] = modelList2;
                        }
                    }
                    model.Ssubmarray = modelList1;
                }
            }

            if (map.ContainsKey("ssubmmarray"))
            {
                var list1 = map["ssubmmarray"] as List<List<Dictionary<string, object>>>;
                if (list1 != null && list1.Count > 0)
                {
                    int n1 = 0;
                    var modelList1 = new List<List<Request>>();
                    foreach(var item1 in list1)
                    {
                        var list2 = item1;
                        if (list2 != null && list2.Count > 0)
                        {
                            int n2 = 0;
                            var modelList2 = new List<Request>();
                            foreach(var item2 in list2)
                            {
                                modelList2[n2++] = Request.FromMap(item2);
                            }
                            modelList1[n1++] = modelList2;
                        }
                    }
                    model.Ssubmmarray = modelList1;
                }
            }

            if (map.ContainsKey("maparray"))
            {
                var list1 = map["maparray"] as List<Dictionary<string, object>>;
                if (list1 != null && list1.Count > 0)
                {
                    int n1 = 0;
                    var modelList1 = new List<Dictionary<string, object>>();
                    foreach(var item1 in list1)
                    {
                        var dict = map["Maparray"] as Dictionary<string, object>;
                        if (dict != null && dict.Count > 0)
                        {
                            var modelMap2 = new Dictionary<string, object>();
                            foreach (KeyValuePair<string, object> entry2 in dict)
                            {
                                modelMap2[entry2.Key] = (object)entry2.Value;
                            }
                            modelList1[n1++] = modelMap2;
                        }
                    }
                    model.Maparray = modelList1;
                }
            }

            if (map.ContainsKey("mapsubmarray"))
            {
                var list1 = map["mapsubmarray"] as List<Dictionary<string, Dictionary<string, object>>>;
                if (list1 != null && list1.Count > 0)
                {
                    int n1 = 0;
                    var modelList1 = new List<Dictionary<string, SourceClient>>();
                    foreach(var item1 in list1)
                    {
                        var dict = map["Mapsubmarray"] as Dictionary<string, SourceClient>;
                        if (dict != null && dict.Count > 0)
                        {
                            var modelMap2 = new Dictionary<string, SourceClient>();
                            foreach (KeyValuePair<string, SourceClient> entry2 in dict)
                            {
                                modelMap2[entry2.Key] = (SourceClient)entry2.Value;
                            }
                            modelList1[n1++] = modelMap2;
                        }
                    }
                    model.Mapsubmarray = modelList1;
                }
            }

            if (map.ContainsKey("moduleModelMap"))
            {
                var dict = map["ModuleModelMap"] as Dictionary<string, Dictionary<string, object>>;
                if (dict != null && dict.Count > 0)
                {
                    var modelMap1 = new Dictionary<string, Request>();
                    foreach (KeyValuePair<string, Dictionary<string, object>> entry1 in dict)
                    {
                        modelMap1[entry1.Key] = Request.FromMap(entry1.Value);
                    }
                    model.ModuleModelMap = modelMap1;
                }
            }

            if (map.ContainsKey("subModelMap"))
            {
                var dict = map["SubModelMap"] as Dictionary<string, Dictionary<string, object>>;
                if (dict != null && dict.Count > 0)
                {
                    var modelMap1 = new Dictionary<string, MyModelSubM>();
                    foreach (KeyValuePair<string, Dictionary<string, object>> entry1 in dict)
                    {
                        modelMap1[entry1.Key] = MyModelSubM.FromMap(entry1.Value);
                    }
                    model.SubModelMap = modelMap1;
                }
            }

            if (map.ContainsKey("modelMap"))
            {
                var dict = map["ModelMap"] as Dictionary<string, Dictionary<string, object>>;
                if (dict != null && dict.Count > 0)
                {
                    var modelMap1 = new Dictionary<string, M>();
                    foreach (KeyValuePair<string, Dictionary<string, object>> entry1 in dict)
                    {
                        modelMap1[entry1.Key] = M.FromMap(entry1.Value);
                    }
                    model.ModelMap = modelMap1;
                }
            }

            if (map.ContainsKey("moduleMap"))
            {
                var dict = map["ModuleMap"] as Dictionary<string, SourceClient>;
                if (dict != null && dict.Count > 0)
                {
                    var modelMap1 = new Dictionary<string, SourceClient>();
                    foreach (KeyValuePair<string, SourceClient> entry1 in dict)
                    {
                        modelMap1[entry1.Key] = (SourceClient)entry1.Value;
                    }
                    model.ModuleMap = modelMap1;
                }
            }

            if (map.ContainsKey("object"))
            {
                model.Object = (Dictionary<string, object>)map["object"];
            }

            if (map.ContainsKey("readable"))
            {
                model.Readable = (Stream)map["readable"];
            }

            if (map.ContainsKey("writable"))
            {
                model.Writable = (Stream)map["writable"];
            }

            if (map.ContainsKey("existModel"))
            {
                var temp = (Dictionary<string, object>)map["existModel"];
                model.ExistModel = M.FromMap(temp);
            }

            if (map.ContainsKey("request"))
            {
                model.Request = TeaRequest.FromMap(map["request"]);
            }

            if (map.ContainsKey("complexList"))
            {
                var list1 = map["complexList"] as List<List<string>>;
                if (list1 != null && list1.Count > 0)
                {
                    int n1 = 0;
                    var modelList1 = new List<List<string>>();
                    foreach(var item1 in list1)
                    {
                        var list2 = item1;
                        if (list2 != null && list2.Count > 0)
                        {
                            int n2 = 0;
                            var modelList2 = new List<string>();
                            foreach(var item2 in list2)
                            {
                                modelList2[n2++] = (string)item2;
                            }
                            modelList1[n1++] = modelList2;
                        }
                    }
                    model.ComplexList = modelList1;
                }
            }

            if (map.ContainsKey("numberfield"))
            {
                model.Numberfield = (int?)map["numberfield"];
            }

            if (map.ContainsKey("integerField"))
            {
                model.IntegerField = (int?)map["integerField"];
            }

            if (map.ContainsKey("floatField"))
            {
                model.FloatField = (float?)map["floatField"];
            }

            if (map.ContainsKey("doubleField"))
            {
                model.DoubleField = (double?)map["doubleField"];
            }

            if (map.ContainsKey("longField"))
            {
                model.LongField = (long?)map["longField"];
            }

            if (map.ContainsKey("ulongField"))
            {
                model.UlongField = (ulong?)map["ulongField"];
            }

            if (map.ContainsKey("int8Field"))
            {
                model.Int8Field = (int?)map["int8Field"];
            }

            if (map.ContainsKey("int16Field"))
            {
                model.Int16Field = (int?)map["int16Field"];
            }

            if (map.ContainsKey("int32Field"))
            {
                model.Int32Field = (int?)map["int32Field"];
            }

            if (map.ContainsKey("int64Field"))
            {
                model.Int64Field = (long?)map["int64Field"];
            }

            if (map.ContainsKey("uint8Field"))
            {
                model.Uint8Field = (uint?)map["uint8Field"];
            }

            if (map.ContainsKey("uint16Field"))
            {
                model.Uint16Field = (uint?)map["uint16Field"];
            }

            if (map.ContainsKey("uint32Field"))
            {
                model.Uint32Field = (uint?)map["uint32Field"];
            }

            if (map.ContainsKey("uint64Field"))
            {
                model.Uint64Field = (ulong?)map["uint64Field"];
            }

            if (map.ContainsKey("link"))
            {
                model.Link = (string)map["link"];
            }

            return model;
        }
    }

}

