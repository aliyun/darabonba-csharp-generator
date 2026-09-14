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
    public class ComplexRequest : Darabonba.Model {
        [NameInMap("duplicatName")]
        [Validation(Required=true)]
        public Darabonba.import.Models.ComplexRequest DuplicatName { get; set; }

        [NameInMap("accessKey")]
        [Validation(Required=true)]
        public string AccessKey { get; set; }

        /// <summary>
        /// <para>Body</para>
        /// 
        /// <b>Example:</b>
        /// <para>Body</para>
        /// </summary>
        [NameInMap("Body")]
        [Validation(Required=true)]
        public Stream Body { get; set; }

        /// <summary>
        /// <para>Strs</para>
        /// 
        /// <b>Example:</b>
        /// <para>Strs</para>
        /// </summary>
        [NameInMap("Strs")]
        [Validation(Required=true)]
        public List<string> Strs { get; set; }

        /// <summary>
        /// <para>header</para>
        /// </summary>
        [NameInMap("header")]
        [Validation(Required=true)]
        public ComplexRequestHeader Header { get; set; }
        public class ComplexRequestHeader : Darabonba.Model {
            /// <summary>
            /// <para>The ID of the security group to which you want to assign the instance. Instances in the same security group can communicate with each other. The maximum number of instances that a security group can contain depends on the type of the security group. For more information, see the &quot;Security group limits&quot; section in <a href="https://help.aliyun.com/document_detail/25412.html#SecurityGroupQuota">Limits</a>.</para>
            /// <remarks>
            /// <para>Notice:  The network type of the new instance must be the same as that of the security group specified by the <c>SecurityGroupId</c> parameter. For example, if the specified security group is of the VPC type, the new instance is also of the VPC type and you must specify <c>VSwitchId</c>.</para>
            /// </remarks>
            /// <para>If you do not use <c>LaunchTemplateId</c> or <c>LaunchTemplateName</c> to specify a launch template, you must specify SecurityGroupId. Take note of the following items:</para>
            /// <list type="bullet">
            /// <item><description>You can set <c>SecurityGroupId</c> to specify a single security group or set <c>SecurityGroupIds.N</c> to specify one or more security groups. However, you cannot specify both <c>SecurityGroupId</c> and <c>SecurityGroupIds.N</c>.</description></item>
            /// <item><description>If <c>NetworkInterface.N.InstanceType</c> is set to <c>Primary</c>, you cannot specify <c>SecurityGroupId</c> or <c>SecurityGroupIds.N</c> but can specify <c>NetworkInterface.N.SecurityGroupId</c> or <c>NetworkInterface.N.SecurityGroupIds.N</c>.</description></item>
            /// </list>
            /// 
            /// <b>Example:</b>
            /// <para>The name of the region.</para>
            /// 
            /// <b>check if is blank:</b>
            /// <c>true</c>
            /// 
            /// <b>if can be null:</b>
            /// <c>true</c>
            /// 
            /// <b>if is sensitive:</b>
            /// <c>true</c>
            /// </summary>
            [NameInMap("Content")]
            [Validation(Required=true)]
            public string Content { get; set; }

            [NameInMap("listSub")]
            [Validation(Required=true)]
            public List<ComplexRequestHeaderListSub> ListSub { get; set; }
            public class ComplexRequestHeaderListSub : Darabonba.Model {
                [NameInMap("listSubItemName")]
                [Validation(Required=true)]
                public string ListSubItem { get; set; }

                [NameInMap("listSubItemSubName")]
                [Validation(Required=true)]
                public Darabonba.Test.Models.Config ListSubItemSub { get; set; }

                public override Dictionary<string, object> ToMapCore(bool noStream = false)
                {
                    var map = new Dictionary<string, object>();
                    map["listSubItemName"] = this.ListSubItem;
                    map["listSubItemSubName"] = this.ListSubItemSub == null ? null : this.ListSubItemSub.ToMap();
                    return map;
                }

                public new static ComplexRequestHeaderListSub FromMap(IDictionary map)
                {
                    if (map == null)
                    {
                        return null;
                    }
                    var model = new ComplexRequestHeaderListSub();
                    if (map.Contains("listSubItemName"))
                    {
                        model.ListSubItem = (string)map["listSubItemName"];
                    }
                    if (map.Contains("listSubItemSubName"))
                    {
                        model.ListSubItemSub = map["listSubItemSubName"] == null ? null : Darabonba.Test.Models.Config.FromMap((IDictionary)map["listSubItemSubName"]);
                    }
                    return model;
                }
            }

            [NameInMap("listStr")]
            [Validation(Required=true)]
            public List<string> ListStr { get; set; }

            [NameInMap("sourceClient")]
            [Validation(Required=true)]
            public SourceClient SourceClient { get; set; }

            [NameInMap("sourceConfig")]
            [Validation(Required=true)]
            public Darabonba.import.Models.Config SourceConfig { get; set; }

            [NameInMap("subModel")]
            [Validation(Required=true)]
            public ComplexRequestHeaderSubModel SubModel { get; set; }
            public class ComplexRequestHeaderSubModel : Darabonba.Model {
                [NameInMap("subModelStr")]
                [Validation(Required=true)]
                public string SubModelStr { get; set; }

                public override Dictionary<string, object> ToMapCore(bool noStream = false)
                {
                    var map = new Dictionary<string, object>();
                    map["subModelStr"] = this.SubModelStr;
                    return map;
                }

                public new static ComplexRequestHeaderSubModel FromMap(IDictionary map)
                {
                    if (map == null)
                    {
                        return null;
                    }
                    var model = new ComplexRequestHeaderSubModel();
                    if (map.Contains("subModelStr"))
                    {
                        model.SubModelStr = (string)map["subModelStr"];
                    }
                    return model;
                }
            }

            [NameInMap("subArray")]
            [Validation(Required=true)]
            public List<Darabonba.Test.Models.Config> SubArray { get; set; }

            [NameInMap("subMutiArray")]
            [Validation(Required=true)]
            public List<List<Darabonba.Test.Models.Config>> SubMutiArray { get; set; }

            public override Dictionary<string, object> ToMapCore(bool noStream = false)
            {
                var map = new Dictionary<string, object>();
                map["Content"] = this.Content;
                {
                    List<object> value0 = null;
                    if (this.ListSub != null)
                    {
                        value0 = new List<object>();
                        foreach (var item0 in this.ListSub)
                        {
                            value0.Add(item0 == null ? null : item0.ToMap());
                        }
                    }
                    map["listSub"] = value0;
                }
                {
                    List<object> value0 = null;
                    if (this.ListStr != null)
                    {
                        value0 = new List<object>();
                        foreach (var item0 in this.ListStr)
                        {
                            value0.Add(item0);
                        }
                    }
                    map["listStr"] = value0;
                }
                map["sourceClient"] = this.SourceClient;
                map["sourceConfig"] = this.SourceConfig == null ? null : this.SourceConfig.ToMap();
                map["subModel"] = this.SubModel == null ? null : this.SubModel.ToMap();
                {
                    List<object> value0 = null;
                    if (this.SubArray != null)
                    {
                        value0 = new List<object>();
                        foreach (var item0 in this.SubArray)
                        {
                            value0.Add(item0 == null ? null : item0.ToMap());
                        }
                    }
                    map["subArray"] = value0;
                }
                {
                    List<object> value0 = null;
                    if (this.SubMutiArray != null)
                    {
                        value0 = new List<object>();
                        foreach (var item0 in this.SubMutiArray)
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
                    map["subMutiArray"] = value0;
                }
                return map;
            }

            public new static ComplexRequestHeader FromMap(IDictionary map)
            {
                if (map == null)
                {
                    return null;
                }
                var model = new ComplexRequestHeader();
                if (map.Contains("Content"))
                {
                    model.Content = (string)map["Content"];
                }
                if (map.Contains("listSub"))
                {
                    List<ComplexRequestHeaderListSub> value0 = null;
                    if (map["listSub"] != null)
                    {
                        value0 = new List<ComplexRequestHeaderListSub>();
                        foreach (var item0 in (IList)map["listSub"])
                        {
                            value0.Add(item0 == null ? null : ComplexRequestHeaderListSub.FromMap((IDictionary)item0));
                        }
                    }
                    model.ListSub = value0;
                }
                if (map.Contains("listStr"))
                {
                    List<string> value0 = null;
                    if (map["listStr"] != null)
                    {
                        value0 = new List<string>();
                        foreach (var item0 in (IList)map["listStr"])
                        {
                            value0.Add((string)item0);
                        }
                    }
                    model.ListStr = value0;
                }
                if (map.Contains("sourceClient"))
                {
                    model.SourceClient = (SourceClient)map["sourceClient"];
                }
                if (map.Contains("sourceConfig"))
                {
                    model.SourceConfig = map["sourceConfig"] == null ? null : Darabonba.import.Models.Config.FromMap((IDictionary)map["sourceConfig"]);
                }
                if (map.Contains("subModel"))
                {
                    model.SubModel = map["subModel"] == null ? null : ComplexRequestHeaderSubModel.FromMap((IDictionary)map["subModel"]);
                }
                if (map.Contains("subArray"))
                {
                    List<Darabonba.Test.Models.Config> value0 = null;
                    if (map["subArray"] != null)
                    {
                        value0 = new List<Darabonba.Test.Models.Config>();
                        foreach (var item0 in (IList)map["subArray"])
                        {
                            value0.Add(item0 == null ? null : Darabonba.Test.Models.Config.FromMap((IDictionary)item0));
                        }
                    }
                    model.SubArray = value0;
                }
                if (map.Contains("subMutiArray"))
                {
                    List<List<Darabonba.Test.Models.Config>> value0 = null;
                    if (map["subMutiArray"] != null)
                    {
                        value0 = new List<List<Darabonba.Test.Models.Config>>();
                        foreach (var item0 in (IList)map["subMutiArray"])
                        {
                            List<Darabonba.Test.Models.Config> value1 = null;
                            if (item0 != null)
                            {
                                value1 = new List<Darabonba.Test.Models.Config>();
                                foreach (var item1 in (IList)item0)
                                {
                                    value1.Add(item1 == null ? null : Darabonba.Test.Models.Config.FromMap((IDictionary)item1));
                                }
                            }
                            value0.Add(value1);
                        }
                    }
                    model.SubMutiArray = value0;
                }
                return model;
            }
        }

        [NameInMap("num")]
        [Validation(Required=true)]
        public int? Num { get; set; }

        [NameInMap("client")]
        [Validation(Required=true)]
        public SourceClient Client { get; set; }

        /// <term><b>Obsolete</b></term>
        /// 
        /// <summary>
        /// <para>Part</para>
        /// </summary>
        [NameInMap("Part")]
        [Validation(Required=false)]
        [Obsolete]
        public List<ComplexRequestPart> Part { get; set; }
        public class ComplexRequestPart : Darabonba.Model {
            /// <summary>
            /// <para>PartNumber</para>
            /// </summary>
            [NameInMap("PartNumber")]
            [Validation(Required=false)]
            public string PartNumber { get; set; }

            public override Dictionary<string, object> ToMapCore(bool noStream = false)
            {
                var map = new Dictionary<string, object>();
                map["PartNumber"] = this.PartNumber;
                return map;
            }

            public new static ComplexRequestPart FromMap(IDictionary map)
            {
                if (map == null)
                {
                    return null;
                }
                var model = new ComplexRequestPart();
                if (map.Contains("PartNumber"))
                {
                    model.PartNumber = (string)map["PartNumber"];
                }
                return model;
            }
        }

        [NameInMap("configs")]
        [Validation(Required=true)]
        public ComplexRequestConfigs Configs { get; set; }
        public class ComplexRequestConfigs : Darabonba.Model {
            [NameInMap("key")]
            [Validation(Required=true)]
            public string Key { get; set; }

            [NameInMap("value")]
            [Validation(Required=true)]
            public List<string> Value { get; set; }

            [NameInMap("extra")]
            [Validation(Required=true)]
            public Dictionary<string, string> Extra { get; set; }

            public override Dictionary<string, object> ToMapCore(bool noStream = false)
            {
                var map = new Dictionary<string, object>();
                map["key"] = this.Key;
                {
                    List<object> value0 = null;
                    if (this.Value != null)
                    {
                        value0 = new List<object>();
                        foreach (var item0 in this.Value)
                        {
                            value0.Add(item0);
                        }
                    }
                    map["value"] = value0;
                }
                {
                    Dictionary<string, object> value0 = null;
                    if (this.Extra != null)
                    {
                        value0 = new Dictionary<string, object>();
                        foreach (var item0 in this.Extra)
                        {
                            value0.Add(item0.Key, item0.Value);
                        }
                    }
                    map["extra"] = value0;
                }
                return map;
            }

            public new static ComplexRequestConfigs FromMap(IDictionary map)
            {
                if (map == null)
                {
                    return null;
                }
                var model = new ComplexRequestConfigs();
                if (map.Contains("key"))
                {
                    model.Key = (string)map["key"];
                }
                if (map.Contains("value"))
                {
                    List<string> value0 = null;
                    if (map["value"] != null)
                    {
                        value0 = new List<string>();
                        foreach (var item0 in (IList)map["value"])
                        {
                            value0.Add((string)item0);
                        }
                    }
                    model.Value = value0;
                }
                if (map.Contains("extra"))
                {
                    Dictionary<string, string> value0 = null;
                    if (map["extra"] != null)
                    {
                        value0 = new Dictionary<string, string>();
                        foreach (DictionaryEntry item0 in (IDictionary)map["extra"])
                        {
                            value0.Add((string)item0.Key, (string)item0.Value);
                        }
                    }
                    model.Extra = value0;
                }
                return model;
            }
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
        public class ComplexRequestArray : Darabonba.Model {
            [NameInMap("type")]
            [Validation(Required=false)]
            public string Type { get; set; }

            [NameInMap("link")]
            [Validation(Required=false)]
            public string Link { get; set; }

            [NameInMap("text")]
            [Validation(Required=false)]
            public string Text { get; set; }

            public override Dictionary<string, object> ToMapCore(bool noStream = false)
            {
                var map = new Dictionary<string, object>();
                map["type"] = this.Type;
                map["link"] = this.Link;
                map["text"] = this.Text;
                return map;
            }

            public new static ComplexRequestArray FromMap(IDictionary map)
            {
                if (map == null)
                {
                    return null;
                }
                var model = new ComplexRequestArray();
                if (map.Contains("type"))
                {
                    model.Type = (string)map["type"];
                }
                if (map.Contains("link"))
                {
                    model.Link = (string)map["link"];
                }
                if (map.Contains("text"))
                {
                    model.Text = (string)map["text"];
                }
                return model;
            }
        }

        [NameInMap("array1")]
        [Validation(Required=false)]
        public List<List<string>> Array1 { get; set; }

        [NameInMap("array2")]
        [Validation(Required=false)]
        public List<Darabonba.Test.Models.Config> Array2 { get; set; }

        public override Dictionary<string, object> ToMapCore(bool noStream = false)
        {
            var map = new Dictionary<string, object>();
            map["duplicatName"] = this.DuplicatName == null ? null : this.DuplicatName.ToMap();
            map["accessKey"] = this.AccessKey;
            {
                List<object> value0 = null;
                if (this.Strs != null)
                {
                    value0 = new List<object>();
                    foreach (var item0 in this.Strs)
                    {
                        value0.Add(item0);
                    }
                }
                map["Strs"] = value0;
            }
            map["header"] = this.Header == null ? null : this.Header.ToMap();
            map["num"] = this.Num;
            map["client"] = this.Client;
            {
                List<object> value0 = null;
                if (this.Part != null)
                {
                    value0 = new List<object>();
                    foreach (var item0 in this.Part)
                    {
                        value0.Add(item0 == null ? null : item0.ToMap());
                    }
                }
                map["Part"] = value0;
            }
            map["configs"] = this.Configs == null ? null : this.Configs.ToMap();
            {
                Dictionary<string, object> value0 = null;
                if (this.Dict != null)
                {
                    value0 = new Dictionary<string, object>();
                    foreach (var item0 in this.Dict)
                    {
                        value0.Add(item0.Key, item0.Value);
                    }
                }
                map["dict"] = value0;
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
            {
                List<object> value0 = null;
                if (this.Array != null)
                {
                    value0 = new List<object>();
                    foreach (var item0 in this.Array)
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
                map["array"] = value0;
            }
            {
                List<object> value0 = null;
                if (this.Array1 != null)
                {
                    value0 = new List<object>();
                    foreach (var item0 in this.Array1)
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
                map["array1"] = value0;
            }
            {
                List<object> value0 = null;
                if (this.Array2 != null)
                {
                    value0 = new List<object>();
                    foreach (var item0 in this.Array2)
                    {
                        value0.Add(item0 == null ? null : item0.ToMap());
                    }
                }
                map["array2"] = value0;
            }
            return map;
        }

        public new static ComplexRequest FromMap(IDictionary map)
        {
            if (map == null)
            {
                return null;
            }
            var model = new ComplexRequest();
            if (map.Contains("duplicatName"))
            {
                model.DuplicatName = map["duplicatName"] == null ? null : Darabonba.import.Models.ComplexRequest.FromMap((IDictionary)map["duplicatName"]);
            }
            if (map.Contains("accessKey"))
            {
                model.AccessKey = (string)map["accessKey"];
            }
            if (map.Contains("Body"))
            {
                model.Body = (Stream)map["Body"];
            }
            if (map.Contains("Strs"))
            {
                List<string> value0 = null;
                if (map["Strs"] != null)
                {
                    value0 = new List<string>();
                    foreach (var item0 in (IList)map["Strs"])
                    {
                        value0.Add((string)item0);
                    }
                }
                model.Strs = value0;
            }
            if (map.Contains("header"))
            {
                model.Header = map["header"] == null ? null : ComplexRequestHeader.FromMap((IDictionary)map["header"]);
            }
            if (map.Contains("num"))
            {
                model.Num = map["num"] == null ? (int?)null : System.Convert.ToInt32(map["num"]);
            }
            if (map.Contains("client"))
            {
                model.Client = (SourceClient)map["client"];
            }
            if (map.Contains("Part"))
            {
                List<ComplexRequestPart> value0 = null;
                if (map["Part"] != null)
                {
                    value0 = new List<ComplexRequestPart>();
                    foreach (var item0 in (IList)map["Part"])
                    {
                        value0.Add(item0 == null ? null : ComplexRequestPart.FromMap((IDictionary)item0));
                    }
                }
                model.Part = value0;
            }
            if (map.Contains("configs"))
            {
                model.Configs = map["configs"] == null ? null : ComplexRequestConfigs.FromMap((IDictionary)map["configs"]);
            }
            if (map.Contains("dict"))
            {
                Dictionary<string, object> value0 = null;
                if (map["dict"] != null)
                {
                    value0 = new Dictionary<string, object>();
                    foreach (DictionaryEntry item0 in (IDictionary)map["dict"])
                    {
                        value0.Add((string)item0.Key, (object)item0.Value);
                    }
                }
                model.Dict = value0;
            }
            if (map.Contains("submodelMap"))
            {
                Dictionary<string, Darabonba.import.Models.Config> value0 = null;
                if (map["submodelMap"] != null)
                {
                    value0 = new Dictionary<string, Darabonba.import.Models.Config>();
                    foreach (DictionaryEntry item0 in (IDictionary)map["submodelMap"])
                    {
                        value0.Add((string)item0.Key, item0.Value == null ? null : Darabonba.import.Models.Config.FromMap((IDictionary)item0.Value));
                    }
                }
                model.SubmodelMap = value0;
            }
            if (map.Contains("array"))
            {
                List<List<ComplexRequestArray>> value0 = null;
                if (map["array"] != null)
                {
                    value0 = new List<List<ComplexRequestArray>>();
                    foreach (var item0 in (IList)map["array"])
                    {
                        List<ComplexRequestArray> value1 = null;
                        if (item0 != null)
                        {
                            value1 = new List<ComplexRequestArray>();
                            foreach (var item1 in (IList)item0)
                            {
                                value1.Add(item1 == null ? null : ComplexRequestArray.FromMap((IDictionary)item1));
                            }
                        }
                        value0.Add(value1);
                    }
                }
                model.Array = value0;
            }
            if (map.Contains("array1"))
            {
                List<List<string>> value0 = null;
                if (map["array1"] != null)
                {
                    value0 = new List<List<string>>();
                    foreach (var item0 in (IList)map["array1"])
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
                model.Array1 = value0;
            }
            if (map.Contains("array2"))
            {
                List<Darabonba.Test.Models.Config> value0 = null;
                if (map["array2"] != null)
                {
                    value0 = new List<Darabonba.Test.Models.Config>();
                    foreach (var item0 in (IList)map["array2"])
                    {
                        value0.Add(item0 == null ? null : Darabonba.Test.Models.Config.FromMap((IDictionary)item0));
                    }
                }
                model.Array2 = value0;
            }
            return model;
        }
    }

}

