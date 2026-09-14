// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Darabonba;
using Darabonba.Utils;

namespace Darabonba.Test.Models
{
    public class Mapping : Darabonba.Model {
        [NameInMap("nestedLists")]
        [Validation(Required=true)]
        public List<List<List<LowerModel>>> NestedLists { get; set; }

        [NameInMap("listMaps")]
        [Validation(Required=true)]
        public List<Dictionary<string, LowerModel>> ListMaps { get; set; }

        [NameInMap("mapLists")]
        [Validation(Required=true)]
        public Dictionary<string, List<LowerModel>> MapLists { get; set; }

        [NameInMap("nestedMaps")]
        [Validation(Required=true)]
        public Dictionary<string, Dictionary<string, LowerModel>> NestedMaps { get; set; }

        [NameInMap("mapListMaps")]
        [Validation(Required=true)]
        public Dictionary<string, List<Dictionary<string, List<LowerModel>>>> MapListMaps { get; set; }

        [NameInMap("listMapLists")]
        [Validation(Required=true)]
        public List<Dictionary<string, List<List<LowerModel>>>> ListMapLists { get; set; }

        [NameInMap("numbers")]
        [Validation(Required=true)]
        public List<int?> Numbers { get; set; }

        [NameInMap("booleans")]
        [Validation(Required=true)]
        public Dictionary<string, bool?> Booleans { get; set; }

        [NameInMap("bytes")]
        [Validation(Required=true)]
        public byte[] Bytes { get; set; }

        [NameInMap("int8Value")]
        [Validation(Required=true)]
        public sbyte? Int8Value { get; set; }

        [NameInMap("uint8Value")]
        [Validation(Required=true)]
        public byte? Uint8Value { get; set; }

        [NameInMap("int16Value")]
        [Validation(Required=true)]
        public short? Int16Value { get; set; }

        [NameInMap("uint16Value")]
        [Validation(Required=true)]
        public ushort? Uint16Value { get; set; }

        [NameInMap("int32Value")]
        [Validation(Required=true)]
        public int? Int32Value { get; set; }

        [NameInMap("uint32Value")]
        [Validation(Required=true)]
        public uint? Uint32Value { get; set; }

        [NameInMap("int64Value")]
        [Validation(Required=true)]
        public long? Int64Value { get; set; }

        [NameInMap("uint64Value")]
        [Validation(Required=true)]
        public ulong? Uint64Value { get; set; }

        [NameInMap("floatValue")]
        [Validation(Required=true)]
        public float? FloatValue { get; set; }

        [NameInMap("doubleValue")]
        [Validation(Required=true)]
        public double? DoubleValue { get; set; }

        public override Dictionary<string, object> ToMapCore(bool noStream = false)
        {
            var map = new Dictionary<string, object>();
            {
                List<object> value0 = null;
                if (this.NestedLists != null)
                {
                    value0 = new List<object>();
                    foreach (var item0 in this.NestedLists)
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
                map["nestedLists"] = value0;
            }
            {
                List<object> value0 = null;
                if (this.ListMaps != null)
                {
                    value0 = new List<object>();
                    foreach (var item0 in this.ListMaps)
                    {
                        Dictionary<string, object> value1 = null;
                        if (item0 != null)
                        {
                            value1 = new Dictionary<string, object>();
                            foreach (var item1 in item0)
                            {
                                value1.Add(item1.Key, item1.Value == null ? null : item1.Value.ToMap());
                            }
                        }
                        value0.Add(value1);
                    }
                }
                map["listMaps"] = value0;
            }
            {
                Dictionary<string, object> value0 = null;
                if (this.MapLists != null)
                {
                    value0 = new Dictionary<string, object>();
                    foreach (var item0 in this.MapLists)
                    {
                        List<object> value1 = null;
                        if (item0.Value != null)
                        {
                            value1 = new List<object>();
                            foreach (var item1 in item0.Value)
                            {
                                value1.Add(item1 == null ? null : item1.ToMap());
                            }
                        }
                        value0.Add(item0.Key, value1);
                    }
                }
                map["mapLists"] = value0;
            }
            {
                Dictionary<string, object> value0 = null;
                if (this.NestedMaps != null)
                {
                    value0 = new Dictionary<string, object>();
                    foreach (var item0 in this.NestedMaps)
                    {
                        Dictionary<string, object> value1 = null;
                        if (item0.Value != null)
                        {
                            value1 = new Dictionary<string, object>();
                            foreach (var item1 in item0.Value)
                            {
                                value1.Add(item1.Key, item1.Value == null ? null : item1.Value.ToMap());
                            }
                        }
                        value0.Add(item0.Key, value1);
                    }
                }
                map["nestedMaps"] = value0;
            }
            {
                Dictionary<string, object> value0 = null;
                if (this.MapListMaps != null)
                {
                    value0 = new Dictionary<string, object>();
                    foreach (var item0 in this.MapListMaps)
                    {
                        List<object> value1 = null;
                        if (item0.Value != null)
                        {
                            value1 = new List<object>();
                            foreach (var item1 in item0.Value)
                            {
                                Dictionary<string, object> value2 = null;
                                if (item1 != null)
                                {
                                    value2 = new Dictionary<string, object>();
                                    foreach (var item2 in item1)
                                    {
                                        List<object> value3 = null;
                                        if (item2.Value != null)
                                        {
                                            value3 = new List<object>();
                                            foreach (var item3 in item2.Value)
                                            {
                                                value3.Add(item3 == null ? null : item3.ToMap());
                                            }
                                        }
                                        value2.Add(item2.Key, value3);
                                    }
                                }
                                value1.Add(value2);
                            }
                        }
                        value0.Add(item0.Key, value1);
                    }
                }
                map["mapListMaps"] = value0;
            }
            {
                List<object> value0 = null;
                if (this.ListMapLists != null)
                {
                    value0 = new List<object>();
                    foreach (var item0 in this.ListMapLists)
                    {
                        Dictionary<string, object> value1 = null;
                        if (item0 != null)
                        {
                            value1 = new Dictionary<string, object>();
                            foreach (var item1 in item0)
                            {
                                List<object> value2 = null;
                                if (item1.Value != null)
                                {
                                    value2 = new List<object>();
                                    foreach (var item2 in item1.Value)
                                    {
                                        List<object> value3 = null;
                                        if (item2 != null)
                                        {
                                            value3 = new List<object>();
                                            foreach (var item3 in item2)
                                            {
                                                value3.Add(item3 == null ? null : item3.ToMap());
                                            }
                                        }
                                        value2.Add(value3);
                                    }
                                }
                                value1.Add(item1.Key, value2);
                            }
                        }
                        value0.Add(value1);
                    }
                }
                map["listMapLists"] = value0;
            }
            {
                List<object> value0 = null;
                if (this.Numbers != null)
                {
                    value0 = new List<object>();
                    foreach (var item0 in this.Numbers)
                    {
                        value0.Add(item0);
                    }
                }
                map["numbers"] = value0;
            }
            {
                Dictionary<string, object> value0 = null;
                if (this.Booleans != null)
                {
                    value0 = new Dictionary<string, object>();
                    foreach (var item0 in this.Booleans)
                    {
                        value0.Add(item0.Key, item0.Value);
                    }
                }
                map["booleans"] = value0;
            }
            map["bytes"] = this.Bytes;
            map["int8Value"] = this.Int8Value;
            map["uint8Value"] = this.Uint8Value;
            map["int16Value"] = this.Int16Value;
            map["uint16Value"] = this.Uint16Value;
            map["int32Value"] = this.Int32Value;
            map["uint32Value"] = this.Uint32Value;
            map["int64Value"] = this.Int64Value;
            map["uint64Value"] = this.Uint64Value;
            map["floatValue"] = this.FloatValue;
            map["doubleValue"] = this.DoubleValue;
            return map;
        }

        public new static Mapping FromMap(IDictionary map)
        {
            if (map == null)
            {
                return null;
            }
            var model = new Mapping();
            if (map.Contains("nestedLists"))
            {
                List<List<List<LowerModel>>> value0 = null;
                if (map["nestedLists"] != null)
                {
                    value0 = new List<List<List<LowerModel>>>();
                    foreach (var item0 in (IList)map["nestedLists"])
                    {
                        List<List<LowerModel>> value1 = null;
                        if (item0 != null)
                        {
                            value1 = new List<List<LowerModel>>();
                            foreach (var item1 in (IList)item0)
                            {
                                List<LowerModel> value2 = null;
                                if (item1 != null)
                                {
                                    value2 = new List<LowerModel>();
                                    foreach (var item2 in (IList)item1)
                                    {
                                        value2.Add(item2 == null ? null : Darabonba.Test.Models.LowerModel.FromMap((IDictionary)item2));
                                    }
                                }
                                value1.Add(value2);
                            }
                        }
                        value0.Add(value1);
                    }
                }
                model.NestedLists = value0;
            }
            if (map.Contains("listMaps"))
            {
                List<Dictionary<string, LowerModel>> value0 = null;
                if (map["listMaps"] != null)
                {
                    value0 = new List<Dictionary<string, LowerModel>>();
                    foreach (var item0 in (IList)map["listMaps"])
                    {
                        Dictionary<string, LowerModel> value1 = null;
                        if (item0 != null)
                        {
                            value1 = new Dictionary<string, LowerModel>();
                            foreach (DictionaryEntry item1 in (IDictionary)item0)
                            {
                                value1.Add((string)item1.Key, item1.Value == null ? null : Darabonba.Test.Models.LowerModel.FromMap((IDictionary)item1.Value));
                            }
                        }
                        value0.Add(value1);
                    }
                }
                model.ListMaps = value0;
            }
            if (map.Contains("mapLists"))
            {
                Dictionary<string, List<LowerModel>> value0 = null;
                if (map["mapLists"] != null)
                {
                    value0 = new Dictionary<string, List<LowerModel>>();
                    foreach (DictionaryEntry item0 in (IDictionary)map["mapLists"])
                    {
                        List<LowerModel> value1 = null;
                        if (item0.Value != null)
                        {
                            value1 = new List<LowerModel>();
                            foreach (var item1 in (IList)item0.Value)
                            {
                                value1.Add(item1 == null ? null : Darabonba.Test.Models.LowerModel.FromMap((IDictionary)item1));
                            }
                        }
                        value0.Add((string)item0.Key, value1);
                    }
                }
                model.MapLists = value0;
            }
            if (map.Contains("nestedMaps"))
            {
                Dictionary<string, Dictionary<string, LowerModel>> value0 = null;
                if (map["nestedMaps"] != null)
                {
                    value0 = new Dictionary<string, Dictionary<string, LowerModel>>();
                    foreach (DictionaryEntry item0 in (IDictionary)map["nestedMaps"])
                    {
                        Dictionary<string, LowerModel> value1 = null;
                        if (item0.Value != null)
                        {
                            value1 = new Dictionary<string, LowerModel>();
                            foreach (DictionaryEntry item1 in (IDictionary)item0.Value)
                            {
                                value1.Add((string)item1.Key, item1.Value == null ? null : Darabonba.Test.Models.LowerModel.FromMap((IDictionary)item1.Value));
                            }
                        }
                        value0.Add((string)item0.Key, value1);
                    }
                }
                model.NestedMaps = value0;
            }
            if (map.Contains("mapListMaps"))
            {
                Dictionary<string, List<Dictionary<string, List<LowerModel>>>> value0 = null;
                if (map["mapListMaps"] != null)
                {
                    value0 = new Dictionary<string, List<Dictionary<string, List<LowerModel>>>>();
                    foreach (DictionaryEntry item0 in (IDictionary)map["mapListMaps"])
                    {
                        List<Dictionary<string, List<LowerModel>>> value1 = null;
                        if (item0.Value != null)
                        {
                            value1 = new List<Dictionary<string, List<LowerModel>>>();
                            foreach (var item1 in (IList)item0.Value)
                            {
                                Dictionary<string, List<LowerModel>> value2 = null;
                                if (item1 != null)
                                {
                                    value2 = new Dictionary<string, List<LowerModel>>();
                                    foreach (DictionaryEntry item2 in (IDictionary)item1)
                                    {
                                        List<LowerModel> value3 = null;
                                        if (item2.Value != null)
                                        {
                                            value3 = new List<LowerModel>();
                                            foreach (var item3 in (IList)item2.Value)
                                            {
                                                value3.Add(item3 == null ? null : Darabonba.Test.Models.LowerModel.FromMap((IDictionary)item3));
                                            }
                                        }
                                        value2.Add((string)item2.Key, value3);
                                    }
                                }
                                value1.Add(value2);
                            }
                        }
                        value0.Add((string)item0.Key, value1);
                    }
                }
                model.MapListMaps = value0;
            }
            if (map.Contains("listMapLists"))
            {
                List<Dictionary<string, List<List<LowerModel>>>> value0 = null;
                if (map["listMapLists"] != null)
                {
                    value0 = new List<Dictionary<string, List<List<LowerModel>>>>();
                    foreach (var item0 in (IList)map["listMapLists"])
                    {
                        Dictionary<string, List<List<LowerModel>>> value1 = null;
                        if (item0 != null)
                        {
                            value1 = new Dictionary<string, List<List<LowerModel>>>();
                            foreach (DictionaryEntry item1 in (IDictionary)item0)
                            {
                                List<List<LowerModel>> value2 = null;
                                if (item1.Value != null)
                                {
                                    value2 = new List<List<LowerModel>>();
                                    foreach (var item2 in (IList)item1.Value)
                                    {
                                        List<LowerModel> value3 = null;
                                        if (item2 != null)
                                        {
                                            value3 = new List<LowerModel>();
                                            foreach (var item3 in (IList)item2)
                                            {
                                                value3.Add(item3 == null ? null : Darabonba.Test.Models.LowerModel.FromMap((IDictionary)item3));
                                            }
                                        }
                                        value2.Add(value3);
                                    }
                                }
                                value1.Add((string)item1.Key, value2);
                            }
                        }
                        value0.Add(value1);
                    }
                }
                model.ListMapLists = value0;
            }
            if (map.Contains("numbers"))
            {
                List<int?> value0 = null;
                if (map["numbers"] != null)
                {
                    value0 = new List<int?>();
                    foreach (var item0 in (IList)map["numbers"])
                    {
                        value0.Add(item0 == null ? (int?)null : System.Convert.ToInt32(item0));
                    }
                }
                model.Numbers = value0;
            }
            if (map.Contains("booleans"))
            {
                Dictionary<string, bool?> value0 = null;
                if (map["booleans"] != null)
                {
                    value0 = new Dictionary<string, bool?>();
                    foreach (DictionaryEntry item0 in (IDictionary)map["booleans"])
                    {
                        value0.Add((string)item0.Key, item0.Value == null ? (bool?)null : System.Convert.ToBoolean(item0.Value));
                    }
                }
                model.Booleans = value0;
            }
            if (map.Contains("bytes"))
            {
                model.Bytes = (byte[])map["bytes"];
            }
            if (map.Contains("int8Value"))
            {
                model.Int8Value = map["int8Value"] == null ? (sbyte?)null : System.Convert.ToSByte(map["int8Value"]);
            }
            if (map.Contains("uint8Value"))
            {
                model.Uint8Value = map["uint8Value"] == null ? (byte?)null : System.Convert.ToByte(map["uint8Value"]);
            }
            if (map.Contains("int16Value"))
            {
                model.Int16Value = map["int16Value"] == null ? (short?)null : System.Convert.ToInt16(map["int16Value"]);
            }
            if (map.Contains("uint16Value"))
            {
                model.Uint16Value = map["uint16Value"] == null ? (ushort?)null : System.Convert.ToUInt16(map["uint16Value"]);
            }
            if (map.Contains("int32Value"))
            {
                model.Int32Value = map["int32Value"] == null ? (int?)null : System.Convert.ToInt32(map["int32Value"]);
            }
            if (map.Contains("uint32Value"))
            {
                model.Uint32Value = map["uint32Value"] == null ? (uint?)null : System.Convert.ToUInt32(map["uint32Value"]);
            }
            if (map.Contains("int64Value"))
            {
                model.Int64Value = map["int64Value"] == null ? (long?)null : System.Convert.ToInt64(map["int64Value"]);
            }
            if (map.Contains("uint64Value"))
            {
                model.Uint64Value = map["uint64Value"] == null ? (ulong?)null : System.Convert.ToUInt64(map["uint64Value"]);
            }
            if (map.Contains("floatValue"))
            {
                model.FloatValue = map["floatValue"] == null ? (float?)null : System.Convert.ToSingle(map["floatValue"]);
            }
            if (map.Contains("doubleValue"))
            {
                model.DoubleValue = map["doubleValue"] == null ? (double?)null : System.Convert.ToDouble(map["doubleValue"]);
            }
            return model;
        }
    }

}

