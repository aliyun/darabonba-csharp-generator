// This file is auto-generated, don't edit it. Thanks.

using System;
using System.Collections.Generic;
using System.IO;

using Tea;

namespace Darabonba.Test.Models
{
    public class ComplexRequest : TeaModel {
        [NameInMap("accessKey")]
        [Validation(Required=true)]
        public string AccessKey { get; set; }

        /// <summary>
        /// Body
        /// </summary>
        [NameInMap("Body")]
        [Validation(Required=true)]
        public Stream Body { get; set; }

        /// <summary>
        /// Strs
        /// </summary>
        [NameInMap("Strs")]
        [Validation(Required=true)]
        public List<string> Strs { get; set; }

        /// <summary>
        /// header
        /// </summary>
        [NameInMap("header")]
        [Validation(Required=true)]
        public ComplexRequestHeader Header { get; set; }
        public class ComplexRequestHeader : TeaModel {
            /// <summary>
            /// Body
            /// </summary>
            [NameInMap("Content")]
            [Validation(Required=true)]
            public string Content { get; set; }

            [NameInMap("listSub")]
            [Validation(Required=true)]
            public List<ComplexRequestHeaderListSub> ListSub { get; set; }
            public class ComplexRequestHeaderListSub : TeaModel {
                [NameInMap("listSubItemName")]
                [Validation(Required=true)]
                public string ListSubItem { get; set; }

                [NameInMap("listSubItemSubName")]
                [Validation(Required=true)]
                public Config ListSubItemSub { get; set; }

            }

            [NameInMap("listStr")]
            [Validation(Required=true)]
            public List<string> ListStr { get; set; }

            [NameInMap("sourceClient")]
            [Validation(Required=true)]
            public Darabonba.import.Client SourceClient { get; set; }

            [NameInMap("sourceConfig")]
            [Validation(Required=true)]
            public Darabonba.import.Models.Config SourceConfig { get; set; }

            [NameInMap("subModel")]
            [Validation(Required=true)]
            public ComplexRequestHeaderSubModel SubModel { get; set; }
            public class ComplexRequestHeaderSubModel : TeaModel {
                [NameInMap("subModelStr")]
                [Validation(Required=true)]
                public string SubModelStr { get; set; }

            }

            [NameInMap("subArray")]
            [Validation(Required=true)]
            public List<Config> SubArray { get; set; }

            [NameInMap("subMutiArray")]
            [Validation(Required=true)]
            public List<List<Config>> SubMutiArray { get; set; }

        }

        [NameInMap("num")]
        [Validation(Required=true)]
        public int? Num { get; set; }

        [NameInMap("client")]
        [Validation(Required=true)]
        public Darabonba.import.Client Client { get; set; }

        /// <summary>
        /// Part
        /// </summary>
        [NameInMap("Part")]
        [Validation(Required=false)]
        [Obsolete]
        public List<ComplexRequestPart> Part { get; set; }
        public class ComplexRequestPart : TeaModel {
            /// <summary>
            /// PartNumber
            /// </summary>
            [NameInMap("PartNumber")]
            [Validation(Required=false)]
            public string PartNumber { get; set; }

        }

        [NameInMap("configs")]
        [Validation(Required=true)]
        public ComplexRequestConfigs Configs { get; set; }
        public class ComplexRequestConfigs : TeaModel {
            [NameInMap("key")]
            [Validation(Required=true)]
            public string Key { get; set; }

            [NameInMap("value")]
            [Validation(Required=true)]
            public List<string> Value { get; set; }

            [NameInMap("extra")]
            [Validation(Required=true)]
            public Dictionary<string, string> Extra { get; set; }

        }

        [NameInMap("dict")]
        [Validation(Required=true)]
        public Dictionary<string, object> Dict { get; set; }

        [NameInMap("submodelMap")]
        [Validation(Required=true)]
        public Dictionary<string, Darabonba.import.Models.Config> SubmodelMap { get; set; }

        [NameInMap("array")]
        [Validation(Required=false)]
        public List<List<ComplexRequestArray>> Array { get; set; }
        public class ComplexRequestArray : TeaModel {
            [NameInMap("type")]
            [Validation(Required=false)]
            public string Type { get; set; }

            [NameInMap("link")]
            [Validation(Required=false)]
            public string Link { get; set; }

            [NameInMap("text")]
            [Validation(Required=false)]
            public string Text { get; set; }

        }

        [NameInMap("array1")]
        [Validation(Required=false)]
        public List<List<string>> Array1 { get; set; }

        [NameInMap("array2")]
        [Validation(Required=false)]
        public List<Config> Array2 { get; set; }

    }

}
