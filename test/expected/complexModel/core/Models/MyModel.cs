// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Darabonba;
using Darabonba.Utils;
using SourceClient = Darabonba.import.Client;

namespace Darabonba.Test.Models
{
    public class MyModel : Darabonba.Model {
        [NameInMap("model")]
        [Validation(Required=true)]
        public MyModelModel Model { get; set; }
        public class MyModelModel : Darabonba.Model {
            [NameInMap("str")]
            [Validation(Required=true)]
            public string Str { get; set; }

            [NameInMap("model")]
            [Validation(Required=true)]
            public MyModelModelModel Model { get; set; }
            public class MyModelModelModel : Darabonba.Model {
                [NameInMap("str")]
                [Validation(Required=true)]
                public string Str { get; set; }

                public override Dictionary<string, object> ToMapCore(bool noStream = false)
                {
                    var map = new Dictionary<string, object>();
                    map["str"] = this.Str;
                    return map;
                }

                public new static MyModelModelModel FromMap(IDictionary map)
                {
                    if (map == null)
                    {
                        return null;
                    }
                    var model = new MyModelModelModel();
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

            public new static MyModelModel FromMap(IDictionary map)
            {
                if (map == null)
                {
                    return null;
                }
                var model = new MyModelModel();
                if (map.Contains("str"))
                {
                    model.Str = (string)map["str"];
                }
                if (map.Contains("model"))
                {
                    model.Model = map["model"] == null ? null : MyModelModelModel.FromMap((IDictionary)map["model"]);
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
        public class MyModelSubmodel : Darabonba.Model {
            [NameInMap("stringfield")]
            [Validation(Required=true)]
            public string Stringfield { get; set; }

            [NameInMap("model")]
            [Validation(Required=true)]
            public MyModelSubmodelModel Model { get; set; }
            public class MyModelSubmodelModel : Darabonba.Model {
                [NameInMap("str")]
                [Validation(Required=true)]
                public string Str { get; set; }

                public override Dictionary<string, object> ToMapCore(bool noStream = false)
                {
                    var map = new Dictionary<string, object>();
                    map["str"] = this.Str;
                    return map;
                }

                public new static MyModelSubmodelModel FromMap(IDictionary map)
                {
                    if (map == null)
                    {
                        return null;
                    }
                    var model = new MyModelSubmodelModel();
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
                map["stringfield"] = this.Stringfield;
                map["model"] = this.Model == null ? null : this.Model.ToMap();
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
                if (map.Contains("model"))
                {
                    model.Model = map["model"] == null ? null : MyModelSubmodelModel.FromMap((IDictionary)map["model"]);
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
        public class MyModelSubarraymodel : Darabonba.Model {
            public override Dictionary<string, object> ToMapCore(bool noStream = false)
            {
                var map = new Dictionary<string, object>();
                return map;
            }

            public new static MyModelSubarraymodel FromMap(IDictionary map)
            {
                if (map == null)
                {
                    return null;
                }
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
        public List<List<Darabonba.import.Models.Request>> Ssubmmarray { get; set; }

        [NameInMap("maparray")]
        [Validation(Required=true)]
        public List<Dictionary<string, object>> Maparray { get; set; }

        [NameInMap("mapsubmarray")]
        [Validation(Required=true)]
        public List<Dictionary<string, SourceClient>> Mapsubmarray { get; set; }

        [NameInMap("moduleModelMap")]
        [Validation(Required=true)]
        public Dictionary<string, Darabonba.import.Models.Request> ModuleModelMap { get; set; }

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
        public Darabonba.Request Request { get; set; }

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
        public sbyte? Int8Field { get; set; }

        [NameInMap("int16Field")]
        [Validation(Required=true)]
        public short? Int16Field { get; set; }

        [NameInMap("int32Field")]
        [Validation(Required=true)]
        public int? Int32Field { get; set; }

        [NameInMap("int64Field")]
        [Validation(Required=true)]
        public long? Int64Field { get; set; }

        [NameInMap("uint8Field")]
        [Validation(Required=true)]
        public byte? Uint8Field { get; set; }

        [NameInMap("uint16Field")]
        [Validation(Required=true)]
        public ushort? Uint16Field { get; set; }

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

        public override Dictionary<string, object> ToMapCore(bool noStream = false)
        {
            var map = new Dictionary<string, object>();
            map["model"] = this.Model == null ? null : this.Model.ToMap();
            map["stringfield"] = this.Stringfield;
            map["bytesfield"] = this.Bytesfield;
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
                List<object> value0 = null;
                if (this.Subarraymodel != null)
                {
                    value0 = new List<object>();
                    foreach (var item0 in this.Subarraymodel)
                    {
                        value0.Add(item0 == null ? null : item0.ToMap());
                    }
                }
                map["subarraymodel"] = value0;
            }
            {
                List<object> value0 = null;
                if (this.Subarray != null)
                {
                    value0 = new List<object>();
                    foreach (var item0 in this.Subarray)
                    {
                        value0.Add(item0 == null ? null : item0.ToMap());
                    }
                }
                map["subarray"] = value0;
            }
            {
                List<object> value0 = null;
                if (this.Ssubarray != null)
                {
                    value0 = new List<object>();
                    foreach (var item0 in this.Ssubarray)
                    {
                        List<object> value1 = null;
                        if (item0 != null)
                        {
                            value1 = new List<object>();
                            foreach (var item1 in item0)
                            {
                                List<object> value2 = null;
                                if (item1 != null)
                                {
                                    value2 = new List<object>();
                                    foreach (var item2 in item1)
                                    {
                                        value2.Add(item2 == null ? null : item2.ToMap());
                                    }
                                }
                                value1.Add(value2);
                            }
                        }
                        value0.Add(value1);
                    }
                }
                map["ssubarray"] = value0;
            }
            {
                List<object> value0 = null;
                if (this.Ssubmarray != null)
                {
                    value0 = new List<object>();
                    foreach (var item0 in this.Ssubmarray)
                    {
                        List<object> value1 = null;
                        if (item0 != null)
                        {
                            value1 = new List<object>();
                            foreach (var item1 in item0)
                            {
                                value1.Add(item1);
                            }
                        }
                        value0.Add(value1);
                    }
                }
                map["ssubmarray"] = value0;
            }
            {
                List<object> value0 = null;
                if (this.Ssubmmarray != null)
                {
                    value0 = new List<object>();
                    foreach (var item0 in this.Ssubmmarray)
                    {
                        List<object> value1 = null;
                        if (item0 != null)
                        {
                            value1 = new List<object>();
                            foreach (var item1 in item0)
                            {
                                value1.Add(item1 == null ? null : item1.ToMap());
                            }
                        }
                        value0.Add(value1);
                    }
                }
                map["ssubmmarray"] = value0;
            }
            {
                List<object> value0 = null;
                if (this.Maparray != null)
                {
                    value0 = new List<object>();
                    foreach (var item0 in this.Maparray)
                    {
                        Dictionary<string, object> value1 = null;
                        if (item0 != null)
                        {
                            value1 = new Dictionary<string, object>();
                            foreach (var item1 in item0)
                            {
                                value1.Add(item1.Key, item1.Value);
                            }
                        }
                        value0.Add(value1);
                    }
                }
                map["maparray"] = value0;
            }
            {
                List<object> value0 = null;
                if (this.Mapsubmarray != null)
                {
                    value0 = new List<object>();
                    foreach (var item0 in this.Mapsubmarray)
                    {
                        Dictionary<string, object> value1 = null;
                        if (item0 != null)
                        {
                            value1 = new Dictionary<string, object>();
                            foreach (var item1 in item0)
                            {
                                value1.Add(item1.Key, item1.Value);
                            }
                        }
                        value0.Add(value1);
                    }
                }
                map["mapsubmarray"] = value0;
            }
            {
                Dictionary<string, object> value0 = null;
                if (this.ModuleModelMap != null)
                {
                    value0 = new Dictionary<string, object>();
                    foreach (var item0 in this.ModuleModelMap)
                    {
                        value0.Add(item0.Key, item0.Value == null ? null : item0.Value.ToMap());
                    }
                }
                map["moduleModelMap"] = value0;
            }
            {
                Dictionary<string, object> value0 = null;
                if (this.SubModelMap != null)
                {
                    value0 = new Dictionary<string, object>();
                    foreach (var item0 in this.SubModelMap)
                    {
                        value0.Add(item0.Key, item0.Value == null ? null : item0.Value.ToMap());
                    }
                }
                map["subModelMap"] = value0;
            }
            {
                Dictionary<string, object> value0 = null;
                if (this.ModelMap != null)
                {
                    value0 = new Dictionary<string, object>();
                    foreach (var item0 in this.ModelMap)
                    {
                        value0.Add(item0.Key, item0.Value == null ? null : item0.Value.ToMap());
                    }
                }
                map["modelMap"] = value0;
            }
            {
                Dictionary<string, object> value0 = null;
                if (this.ModuleMap != null)
                {
                    value0 = new Dictionary<string, object>();
                    foreach (var item0 in this.ModuleMap)
                    {
                        value0.Add(item0.Key, item0.Value);
                    }
                }
                map["moduleMap"] = value0;
            }
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
            map["existModel"] = this.ExistModel == null ? null : this.ExistModel.ToMap();
            map["request"] = this.Request == null ? null : this.Request.ToMap();
            {
                List<object> value0 = null;
                if (this.ComplexList != null)
                {
                    value0 = new List<object>();
                    foreach (var item0 in this.ComplexList)
                    {
                        List<object> value1 = null;
                        if (item0 != null)
                        {
                            value1 = new List<object>();
                            foreach (var item1 in item0)
                            {
                                value1.Add(item1);
                            }
                        }
                        value0.Add(value1);
                    }
                }
                map["complexList"] = value0;
            }
            map["numberfield"] = this.Numberfield;
            map["integerField"] = this.IntegerField;
            map["floatField"] = this.FloatField;
            map["doubleField"] = this.DoubleField;
            map["longField"] = this.LongField;
            map["ulongField"] = this.UlongField;
            map["int8Field"] = this.Int8Field;
            map["int16Field"] = this.Int16Field;
            map["int32Field"] = this.Int32Field;
            map["int64Field"] = this.Int64Field;
            map["uint8Field"] = this.Uint8Field;
            map["uint16Field"] = this.Uint16Field;
            map["uint32Field"] = this.Uint32Field;
            map["uint64Field"] = this.Uint64Field;
            map["link"] = this.Link;
            return map;
        }

        public new static MyModel FromMap(IDictionary map)
        {
            if (map == null)
            {
                return null;
            }
            var model = new MyModel();
            if (map.Contains("model"))
            {
                model.Model = map["model"] == null ? null : MyModelModel.FromMap((IDictionary)map["model"]);
            }
            if (map.Contains("stringfield"))
            {
                model.Stringfield = (string)map["stringfield"];
            }
            if (map.Contains("bytesfield"))
            {
                model.Bytesfield = (byte[])map["bytesfield"];
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
            if (map.Contains("mapModel"))
            {
                Dictionary<string, M> value0 = null;
                if (map["mapModel"] != null)
                {
                    value0 = new Dictionary<string, M>();
                    foreach (DictionaryEntry item0 in (IDictionary)map["mapModel"])
                    {
                        value0.Add((string)item0.Key, item0.Value == null ? null : Darabonba.Test.Models.M.FromMap((IDictionary)item0.Value));
                    }
                }
                model.MapModel = value0;
            }
            if (map.Contains("subarraymodel"))
            {
                List<MyModelSubarraymodel> value0 = null;
                if (map["subarraymodel"] != null)
                {
                    value0 = new List<MyModelSubarraymodel>();
                    foreach (var item0 in (IList)map["subarraymodel"])
                    {
                        value0.Add(item0 == null ? null : MyModelSubarraymodel.FromMap((IDictionary)item0));
                    }
                }
                model.Subarraymodel = value0;
            }
            if (map.Contains("subarray"))
            {
                List<M> value0 = null;
                if (map["subarray"] != null)
                {
                    value0 = new List<M>();
                    foreach (var item0 in (IList)map["subarray"])
                    {
                        value0.Add(item0 == null ? null : Darabonba.Test.Models.M.FromMap((IDictionary)item0));
                    }
                }
                model.Subarray = value0;
            }
            if (map.Contains("ssubarray"))
            {
                List<List<List<M>>> value0 = null;
                if (map["ssubarray"] != null)
                {
                    value0 = new List<List<List<M>>>();
                    foreach (var item0 in (IList)map["ssubarray"])
                    {
                        List<List<M>> value1 = null;
                        if (item0 != null)
                        {
                            value1 = new List<List<M>>();
                            foreach (var item1 in (IList)item0)
                            {
                                List<M> value2 = null;
                                if (item1 != null)
                                {
                                    value2 = new List<M>();
                                    foreach (var item2 in (IList)item1)
                                    {
                                        value2.Add(item2 == null ? null : Darabonba.Test.Models.M.FromMap((IDictionary)item2));
                                    }
                                }
                                value1.Add(value2);
                            }
                        }
                        value0.Add(value1);
                    }
                }
                model.Ssubarray = value0;
            }
            if (map.Contains("ssubmarray"))
            {
                List<List<SourceClient>> value0 = null;
                if (map["ssubmarray"] != null)
                {
                    value0 = new List<List<SourceClient>>();
                    foreach (var item0 in (IList)map["ssubmarray"])
                    {
                        List<SourceClient> value1 = null;
                        if (item0 != null)
                        {
                            value1 = new List<SourceClient>();
                            foreach (var item1 in (IList)item0)
                            {
                                value1.Add((SourceClient)item1);
                            }
                        }
                        value0.Add(value1);
                    }
                }
                model.Ssubmarray = value0;
            }
            if (map.Contains("ssubmmarray"))
            {
                List<List<Darabonba.import.Models.Request>> value0 = null;
                if (map["ssubmmarray"] != null)
                {
                    value0 = new List<List<Darabonba.import.Models.Request>>();
                    foreach (var item0 in (IList)map["ssubmmarray"])
                    {
                        List<Darabonba.import.Models.Request> value1 = null;
                        if (item0 != null)
                        {
                            value1 = new List<Darabonba.import.Models.Request>();
                            foreach (var item1 in (IList)item0)
                            {
                                value1.Add(item1 == null ? null : Darabonba.import.Models.Request.FromMap((IDictionary)item1));
                            }
                        }
                        value0.Add(value1);
                    }
                }
                model.Ssubmmarray = value0;
            }
            if (map.Contains("maparray"))
            {
                List<Dictionary<string, object>> value0 = null;
                if (map["maparray"] != null)
                {
                    value0 = new List<Dictionary<string, object>>();
                    foreach (var item0 in (IList)map["maparray"])
                    {
                        Dictionary<string, object> value1 = null;
                        if (item0 != null)
                        {
                            value1 = new Dictionary<string, object>();
                            foreach (DictionaryEntry item1 in (IDictionary)item0)
                            {
                                value1.Add((string)item1.Key, (object)item1.Value);
                            }
                        }
                        value0.Add(value1);
                    }
                }
                model.Maparray = value0;
            }
            if (map.Contains("mapsubmarray"))
            {
                List<Dictionary<string, SourceClient>> value0 = null;
                if (map["mapsubmarray"] != null)
                {
                    value0 = new List<Dictionary<string, SourceClient>>();
                    foreach (var item0 in (IList)map["mapsubmarray"])
                    {
                        Dictionary<string, SourceClient> value1 = null;
                        if (item0 != null)
                        {
                            value1 = new Dictionary<string, SourceClient>();
                            foreach (DictionaryEntry item1 in (IDictionary)item0)
                            {
                                value1.Add((string)item1.Key, (SourceClient)item1.Value);
                            }
                        }
                        value0.Add(value1);
                    }
                }
                model.Mapsubmarray = value0;
            }
            if (map.Contains("moduleModelMap"))
            {
                Dictionary<string, Darabonba.import.Models.Request> value0 = null;
                if (map["moduleModelMap"] != null)
                {
                    value0 = new Dictionary<string, Darabonba.import.Models.Request>();
                    foreach (DictionaryEntry item0 in (IDictionary)map["moduleModelMap"])
                    {
                        value0.Add((string)item0.Key, item0.Value == null ? null : Darabonba.import.Models.Request.FromMap((IDictionary)item0.Value));
                    }
                }
                model.ModuleModelMap = value0;
            }
            if (map.Contains("subModelMap"))
            {
                Dictionary<string, M.MSubM> value0 = null;
                if (map["subModelMap"] != null)
                {
                    value0 = new Dictionary<string, M.MSubM>();
                    foreach (DictionaryEntry item0 in (IDictionary)map["subModelMap"])
                    {
                        value0.Add((string)item0.Key, item0.Value == null ? null : Darabonba.Test.Models.M.MSubM.FromMap((IDictionary)item0.Value));
                    }
                }
                model.SubModelMap = value0;
            }
            if (map.Contains("modelMap"))
            {
                Dictionary<string, M> value0 = null;
                if (map["modelMap"] != null)
                {
                    value0 = new Dictionary<string, M>();
                    foreach (DictionaryEntry item0 in (IDictionary)map["modelMap"])
                    {
                        value0.Add((string)item0.Key, item0.Value == null ? null : Darabonba.Test.Models.M.FromMap((IDictionary)item0.Value));
                    }
                }
                model.ModelMap = value0;
            }
            if (map.Contains("moduleMap"))
            {
                Dictionary<string, SourceClient> value0 = null;
                if (map["moduleMap"] != null)
                {
                    value0 = new Dictionary<string, SourceClient>();
                    foreach (DictionaryEntry item0 in (IDictionary)map["moduleMap"])
                    {
                        value0.Add((string)item0.Key, (SourceClient)item0.Value);
                    }
                }
                model.ModuleMap = value0;
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
            if (map.Contains("readable"))
            {
                model.Readable = (Stream)map["readable"];
            }
            if (map.Contains("writable"))
            {
                model.Writable = (Stream)map["writable"];
            }
            if (map.Contains("existModel"))
            {
                model.ExistModel = map["existModel"] == null ? null : Darabonba.Test.Models.M.FromMap((IDictionary)map["existModel"]);
            }
            if (map.Contains("request"))
            {
                model.Request = map["request"] == null ? null : Darabonba.Request.FromMap((IDictionary)map["request"]);
            }
            if (map.Contains("complexList"))
            {
                List<List<string>> value0 = null;
                if (map["complexList"] != null)
                {
                    value0 = new List<List<string>>();
                    foreach (var item0 in (IList)map["complexList"])
                    {
                        List<string> value1 = null;
                        if (item0 != null)
                        {
                            value1 = new List<string>();
                            foreach (var item1 in (IList)item0)
                            {
                                value1.Add((string)item1);
                            }
                        }
                        value0.Add(value1);
                    }
                }
                model.ComplexList = value0;
            }
            if (map.Contains("numberfield"))
            {
                model.Numberfield = map["numberfield"] == null ? (int?)null : System.Convert.ToInt32(map["numberfield"]);
            }
            if (map.Contains("integerField"))
            {
                model.IntegerField = map["integerField"] == null ? (int?)null : System.Convert.ToInt32(map["integerField"]);
            }
            if (map.Contains("floatField"))
            {
                model.FloatField = map["floatField"] == null ? (float?)null : System.Convert.ToSingle(map["floatField"]);
            }
            if (map.Contains("doubleField"))
            {
                model.DoubleField = map["doubleField"] == null ? (double?)null : System.Convert.ToDouble(map["doubleField"]);
            }
            if (map.Contains("longField"))
            {
                model.LongField = map["longField"] == null ? (long?)null : System.Convert.ToInt64(map["longField"]);
            }
            if (map.Contains("ulongField"))
            {
                model.UlongField = map["ulongField"] == null ? (ulong?)null : System.Convert.ToUInt64(map["ulongField"]);
            }
            if (map.Contains("int8Field"))
            {
                model.Int8Field = map["int8Field"] == null ? (sbyte?)null : System.Convert.ToSByte(map["int8Field"]);
            }
            if (map.Contains("int16Field"))
            {
                model.Int16Field = map["int16Field"] == null ? (short?)null : System.Convert.ToInt16(map["int16Field"]);
            }
            if (map.Contains("int32Field"))
            {
                model.Int32Field = map["int32Field"] == null ? (int?)null : System.Convert.ToInt32(map["int32Field"]);
            }
            if (map.Contains("int64Field"))
            {
                model.Int64Field = map["int64Field"] == null ? (long?)null : System.Convert.ToInt64(map["int64Field"]);
            }
            if (map.Contains("uint8Field"))
            {
                model.Uint8Field = map["uint8Field"] == null ? (byte?)null : System.Convert.ToByte(map["uint8Field"]);
            }
            if (map.Contains("uint16Field"))
            {
                model.Uint16Field = map["uint16Field"] == null ? (ushort?)null : System.Convert.ToUInt16(map["uint16Field"]);
            }
            if (map.Contains("uint32Field"))
            {
                model.Uint32Field = map["uint32Field"] == null ? (uint?)null : System.Convert.ToUInt32(map["uint32Field"]);
            }
            if (map.Contains("uint64Field"))
            {
                model.Uint64Field = map["uint64Field"] == null ? (ulong?)null : System.Convert.ToUInt64(map["uint64Field"]);
            }
            if (map.Contains("link"))
            {
                model.Link = (string)map["link"];
            }
            return model;
        }
    }

}

