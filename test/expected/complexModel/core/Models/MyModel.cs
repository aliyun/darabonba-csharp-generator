// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Darabonba;
using SourceClient = Darabonba.import.Client;
using Darabonba.import.Models;

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

    }

}

