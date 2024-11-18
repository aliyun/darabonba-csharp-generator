// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Darabonba;
using SourceClient = Darabonba.import.Client;

namespace Darabonba.Test.Models
{
    public class ComplexRequest : Model {
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
        public class ComplexRequestHeader : Model {
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
            public class ComplexRequestHeaderListSub : Model {
                [NameInMap("listSubItemName")]
                [Validation(Required=true)]
                public string ListSubItem { get; set; }

                [NameInMap("listSubItemSubName")]
                [Validation(Required=true)]
                public Darabonba.Test.Models.Config ListSubItemSub { get; set; }

                public ComplexRequestHeaderListSub Copy()
                {
                    ComplexRequestHeaderListSub copy = FromMap(ToMap());
                    return copy;
                }

                public ComplexRequestHeaderListSub CopyWithoutStream()
                {
                    ComplexRequestHeaderListSub copy = FromMap(ToMap(true));
                    return copy;
                }

                public Dictionary<string, object> ToMap(bool noStream = false)
                {
                    var map = new Dictionary<string, object>();
                    if (ListSubItem != null)
                    {
                        map["listSubItemName"] = ListSubItem;
                    }

                    if (ListSubItemSub != null)
                    {
                        map["listSubItemSubName"] = ListSubItemSub != null ? ListSubItemSub.ToMap(noStream) : null;
                    }

                    return map;
                }

                public static ComplexRequestHeaderListSub FromMap(Dictionary<string, object> map)
                {
                    var model = new ComplexRequestHeaderListSub();
                    if (map.ContainsKey("listSubItemName"))
                    {
                        model.ListSubItem = (string)map["listSubItemName"];
                    }

                    if (map.ContainsKey("listSubItemSubName"))
                    {
                        var temp = (Dictionary<string, object>)map["listSubItemSubName"];
                        model.ListSubItemSub = Darabonba.Test.Models.Config.FromMap(temp);
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
            public class ComplexRequestHeaderSubModel : Model {
                [NameInMap("subModelStr")]
                [Validation(Required=true)]
                public string SubModelStr { get; set; }

                public ComplexRequestHeaderSubModel Copy()
                {
                    ComplexRequestHeaderSubModel copy = FromMap(ToMap());
                    return copy;
                }

                public ComplexRequestHeaderSubModel CopyWithoutStream()
                {
                    ComplexRequestHeaderSubModel copy = FromMap(ToMap(true));
                    return copy;
                }

                public Dictionary<string, object> ToMap(bool noStream = false)
                {
                    var map = new Dictionary<string, object>();
                    if (SubModelStr != null)
                    {
                        map["subModelStr"] = SubModelStr;
                    }

                    return map;
                }

                public static ComplexRequestHeaderSubModel FromMap(Dictionary<string, object> map)
                {
                    var model = new ComplexRequestHeaderSubModel();
                    if (map.ContainsKey("subModelStr"))
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

            public ComplexRequestHeader Copy()
            {
                ComplexRequestHeader copy = FromMap(ToMap());
                return copy;
            }

            public ComplexRequestHeader CopyWithoutStream()
            {
                ComplexRequestHeader copy = FromMap(ToMap(true));
                return copy;
            }

            public Dictionary<string, object> ToMap(bool noStream = false)
            {
                var map = new Dictionary<string, object>();
                if (Content != null)
                {
                    map["Content"] = Content;
                }

                if (ListSub != null)
                {
                    var list1 = new List<Dictionary<string, object>>();
                    int n1 = 0;
                    foreach (var item1 in ListSub) 
                    {
                        list1[n1++] = item1 != null ? item1.ToMap(noStream) : null;
                    }
                    map["listSub"] = list1;
                }

                if (ListStr != null)
                {
                    var list1 = new List<string>();
                    int n1 = 0;
                    foreach (var item1 in ListStr) 
                    {
                        list1[n1++] = item1;
                    }
                    map["listStr"] = list1;
                }

                if (SourceClient != null)
                {
                    map["sourceClient"] = SourceClient;
                }

                if (SourceConfig != null)
                {
                    map["sourceConfig"] = SourceConfig != null ? SourceConfig.ToMap(noStream) : null;
                }

                if (SubModel != null)
                {
                    map["subModel"] = SubModel != null ? SubModel.ToMap(noStream) : null;
                }

                if (SubArray != null)
                {
                    var list1 = new List<Dictionary<string, object>>();
                    int n1 = 0;
                    foreach (var item1 in SubArray) 
                    {
                        list1[n1++] = item1 != null ? item1.ToMap(noStream) : null;
                    }
                    map["subArray"] = list1;
                }

                if (SubMutiArray != null)
                {
                    var list1 = new List<List<Dictionary<string, object>>>();
                    int n1 = 0;
                    foreach (var item1 in SubMutiArray) 
                    {
                        var list2 = new List<Dictionary<string, object>>();
                        int n2 = 0;
                        foreach (var item2 in item1) 
                        {
                            list2[n2++] = item2 != null ? item2.ToMap(noStream) : null;
                        }
                        list1[n1++] = list2;
                    }
                    map["subMutiArray"] = list1;
                }

                return map;
            }

            public static ComplexRequestHeader FromMap(Dictionary<string, object> map)
            {
                var model = new ComplexRequestHeader();
                if (map.ContainsKey("Content"))
                {
                    model.Content = (string)map["Content"];
                }

                if (map.ContainsKey("listSub"))
                {
                    var list1 = map["listSub"] as List<Dictionary<string, object>>;
                    if (list1 != null && list1.Count > 0)
                    {
                        int n1 = 0;
                        var modelList1 = new List<ComplexRequestHeaderListSub>();
                        foreach(var item1 in list1)
                        {
                            if (item1 != null)
                            {
                                var temp = (Dictionary<string, object>)item1;
                                modelList1[n1++] = ComplexRequestHeaderListSub.FromMap(temp);
                            }
                        }
                        model.ListSub = modelList1;
                    }
                }

                if (map.ContainsKey("listStr"))
                {
                    var list1 = map["listStr"] as List<string>;
                    if (list1 != null && list1.Count > 0)
                    {
                        int n1 = 0;
                        var modelList1 = new List<string>();
                        foreach(var item1 in list1)
                        {
                            modelList1[n1++] = (string)item1;
                        }
                        model.ListStr = modelList1;
                    }
                }

                if (map.ContainsKey("sourceClient"))
                {
                    model.SourceClient = (SourceClient)map["sourceClient"];
                }

                if (map.ContainsKey("sourceConfig"))
                {
                    model.SourceConfig = Darabonba.import.Models.Config.FromMap(map["sourceConfig"]);
                }

                if (map.ContainsKey("subModel"))
                {
                    if (map["subModel"] != null)
                    {
                        var temp = (Dictionary<string, object>)map["subModel"];
                        model.SubModel = ComplexRequestHeaderSubModel.FromMap(temp);
                    }
                }

                if (map.ContainsKey("subArray"))
                {
                    var list1 = map["subArray"] as List<Dictionary<string, object>>;
                    if (list1 != null && list1.Count > 0)
                    {
                        int n1 = 0;
                        var modelList1 = new List<Darabonba.Test.Models.Config>();
                        foreach(var item1 in list1)
                        {
                            modelList1[n1++] = Darabonba.Test.Models.Config.FromMap(item1);
                        }
                        model.SubArray = modelList1;
                    }
                }

                if (map.ContainsKey("subMutiArray"))
                {
                    var list1 = map["subMutiArray"] as List<List<Dictionary<string, object>>>;
                    if (list1 != null && list1.Count > 0)
                    {
                        int n1 = 0;
                        var modelList1 = new List<List<Darabonba.Test.Models.Config>>();
                        foreach(var item1 in list1)
                        {
                            var list2 = item1;
                            if (list2 != null && list2.Count > 0)
                            {
                                int n2 = 0;
                                var modelList2 = new List<Darabonba.Test.Models.Config>();
                                foreach(var item2 in list2)
                                {
                                    modelList2[n2++] = Darabonba.Test.Models.Config.FromMap(item2);
                                }
                                modelList1[n1++] = modelList2;
                            }
                        }
                        model.SubMutiArray = modelList1;
                    }
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
        public class ComplexRequestPart : Model {
            /// <summary>
            /// <para>PartNumber</para>
            /// </summary>
            [NameInMap("PartNumber")]
            [Validation(Required=false)]
            public string PartNumber { get; set; }

            public ComplexRequestPart Copy()
            {
                ComplexRequestPart copy = FromMap(ToMap());
                return copy;
            }

            public ComplexRequestPart CopyWithoutStream()
            {
                ComplexRequestPart copy = FromMap(ToMap(true));
                return copy;
            }

            public Dictionary<string, object> ToMap(bool noStream = false)
            {
                var map = new Dictionary<string, object>();
                if (PartNumber != null)
                {
                    map["PartNumber"] = PartNumber;
                }

                return map;
            }

            public static ComplexRequestPart FromMap(Dictionary<string, object> map)
            {
                var model = new ComplexRequestPart();
                if (map.ContainsKey("PartNumber"))
                {
                    model.PartNumber = (string)map["PartNumber"];
                }

                return model;
            }
        }

        [NameInMap("configs")]
        [Validation(Required=true)]
        public ComplexRequestConfigs Configs { get; set; }
        public class ComplexRequestConfigs : Model {
            [NameInMap("key")]
            [Validation(Required=true)]
            public string Key { get; set; }

            [NameInMap("value")]
            [Validation(Required=true)]
            public List<string> Value { get; set; }

            [NameInMap("extra")]
            [Validation(Required=true)]
            public Dictionary<string, string> Extra { get; set; }

            public ComplexRequestConfigs Copy()
            {
                ComplexRequestConfigs copy = FromMap(ToMap());
                return copy;
            }

            public ComplexRequestConfigs CopyWithoutStream()
            {
                ComplexRequestConfigs copy = FromMap(ToMap(true));
                return copy;
            }

            public Dictionary<string, object> ToMap(bool noStream = false)
            {
                var map = new Dictionary<string, object>();
                if (Key != null)
                {
                    map["key"] = Key;
                }

                if (Value != null)
                {
                    var list1 = new List<string>();
                    int n1 = 0;
                    foreach (var item1 in Value) 
                    {
                        list1[n1++] = item1;
                    }
                    map["value"] = list1;
                }

                if (Extra != null)
                {
                    var dict = new Dictionary<string, string>();
                    foreach (var item1 in Extra) 
                    {
                        dict[item1.Key] = item1.Value;
                    }
                    map["extra"] = dict;
                }

                return map;
            }

            public static ComplexRequestConfigs FromMap(Dictionary<string, object> map)
            {
                var model = new ComplexRequestConfigs();
                if (map.ContainsKey("key"))
                {
                    model.Key = (string)map["key"];
                }

                if (map.ContainsKey("value"))
                {
                    var list1 = map["value"] as List<string>;
                    if (list1 != null && list1.Count > 0)
                    {
                        int n1 = 0;
                        var modelList1 = new List<string>();
                        foreach(var item1 in list1)
                        {
                            modelList1[n1++] = (string)item1;
                        }
                        model.Value = modelList1;
                    }
                }

                if (map.ContainsKey("extra"))
                {
                    var dict = map["extra"] as Dictionary<string, string>;
                    if (dict != null && dict.Count > 0)
                    {
                        var modelMap1 = new Dictionary<string, string>();
                        foreach (KeyValuePair<string, string> entry1 in dict)
                        {
                            modelMap1[entry1.Key] = (string)entry1.Value;
                        }
                        model.Extra = modelMap1;
                    }
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
        public class ComplexRequestArray : Model {
            [NameInMap("type")]
            [Validation(Required=false)]
            public string Type { get; set; }

            [NameInMap("link")]
            [Validation(Required=false)]
            public string Link { get; set; }

            [NameInMap("text")]
            [Validation(Required=false)]
            public string Text { get; set; }

            public ComplexRequestArray Copy()
            {
                ComplexRequestArray copy = FromMap(ToMap());
                return copy;
            }

            public ComplexRequestArray CopyWithoutStream()
            {
                ComplexRequestArray copy = FromMap(ToMap(true));
                return copy;
            }

            public Dictionary<string, object> ToMap(bool noStream = false)
            {
                var map = new Dictionary<string, object>();
                if (Type != null)
                {
                    map["type"] = Type;
                }

                if (Link != null)
                {
                    map["link"] = Link;
                }

                if (Text != null)
                {
                    map["text"] = Text;
                }

                return map;
            }

            public static ComplexRequestArray FromMap(Dictionary<string, object> map)
            {
                var model = new ComplexRequestArray();
                if (map.ContainsKey("type"))
                {
                    model.Type = (string)map["type"];
                }

                if (map.ContainsKey("link"))
                {
                    model.Link = (string)map["link"];
                }

                if (map.ContainsKey("text"))
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

        public ComplexRequest Copy()
        {
            ComplexRequest copy = FromMap(ToMap());
            return copy;
        }

        public ComplexRequest CopyWithoutStream()
        {
            ComplexRequest copy = FromMap(ToMap(true));
            return copy;
        }

        public Dictionary<string, object> ToMap(bool noStream = false)
        {
            var map = new Dictionary<string, object>();
            if (DuplicatName != null)
            {
                map["duplicatName"] = DuplicatName != null ? DuplicatName.ToMap(noStream) : null;
            }

            if (AccessKey != null)
            {
                map["accessKey"] = AccessKey;
            }

            if (Body != null)
            {
                map["Body"] = Body;
            }

            if (Strs != null)
            {
                var list1 = new List<string>();
                int n1 = 0;
                foreach (var item1 in Strs) 
                {
                    list1[n1++] = item1;
                }
                map["Strs"] = list1;
            }

            if (Header != null)
            {
                map["header"] = Header != null ? Header.ToMap(noStream) : null;
            }

            if (Num != null)
            {
                map["num"] = Num;
            }

            if (Client != null)
            {
                map["client"] = Client;
            }

            if (Part != null)
            {
                var list1 = new List<Dictionary<string, object>>();
                int n1 = 0;
                foreach (var item1 in Part) 
                {
                    list1[n1++] = item1 != null ? item1.ToMap(noStream) : null;
                }
                map["Part"] = list1;
            }

            if (Configs != null)
            {
                map["configs"] = Configs != null ? Configs.ToMap(noStream) : null;
            }

            if (Dict != null)
            {
                var dict = new Dictionary<string, object>();
                foreach (var item1 in Dict) 
                {
                    dict[item1.Key] = item1.Value;
                }
                map["dict"] = dict;
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

            if (Array != null)
            {
                var list1 = new List<List<Dictionary<string, object>>>();
                int n1 = 0;
                foreach (var item1 in Array) 
                {
                    var list2 = new List<Dictionary<string, object>>();
                    int n2 = 0;
                    foreach (var item2 in item1) 
                    {
                        list2[n2++] = item2 != null ? item2.ToMap(noStream) : null;
                    }
                    list1[n1++] = list2;
                }
                map["array"] = list1;
            }

            if (Array1 != null)
            {
                var list1 = new List<List<string>>();
                int n1 = 0;
                foreach (var item1 in Array1) 
                {
                    var list2 = new List<string>();
                    int n2 = 0;
                    foreach (var item2 in item1) 
                    {
                        list2[n2++] = item2;
                    }
                    list1[n1++] = list2;
                }
                map["array1"] = list1;
            }

            if (Array2 != null)
            {
                var list1 = new List<Dictionary<string, object>>();
                int n1 = 0;
                foreach (var item1 in Array2) 
                {
                    list1[n1++] = item1 != null ? item1.ToMap(noStream) : null;
                }
                map["array2"] = list1;
            }

            return map;
        }

        public static ComplexRequest FromMap(Dictionary<string, object> map)
        {
            var model = new ComplexRequest();
            if (map.ContainsKey("duplicatName"))
            {
                model.DuplicatName = Darabonba.import.Models.ComplexRequest.FromMap(map["duplicatName"]);
            }

            if (map.ContainsKey("accessKey"))
            {
                model.AccessKey = (string)map["accessKey"];
            }

            if (map.ContainsKey("Body"))
            {
                model.Body = (Stream)map["Body"];
            }

            if (map.ContainsKey("Strs"))
            {
                var list1 = map["Strs"] as List<string>;
                if (list1 != null && list1.Count > 0)
                {
                    int n1 = 0;
                    var modelList1 = new List<string>();
                    foreach(var item1 in list1)
                    {
                        modelList1[n1++] = (string)item1;
                    }
                    model.Strs = modelList1;
                }
            }

            if (map.ContainsKey("header"))
            {
                if (map["header"] != null)
                {
                    var temp = (Dictionary<string, object>)map["header"];
                    model.Header = ComplexRequestHeader.FromMap(temp);
                }
            }

            if (map.ContainsKey("num"))
            {
                model.Num = (int?)map["num"];
            }

            if (map.ContainsKey("client"))
            {
                model.Client = (SourceClient)map["client"];
            }

            if (map.ContainsKey("Part"))
            {
                var list1 = map["Part"] as List<Dictionary<string, object>>;
                if (list1 != null && list1.Count > 0)
                {
                    int n1 = 0;
                    var modelList1 = new List<ComplexRequestPart>();
                    foreach(var item1 in list1)
                    {
                        if (item1 != null)
                        {
                            var temp = (Dictionary<string, object>)item1;
                            modelList1[n1++] = ComplexRequestPart.FromMap(temp);
                        }
                    }
                    model.Part = modelList1;
                }
            }

            if (map.ContainsKey("configs"))
            {
                if (map["configs"] != null)
                {
                    var temp = (Dictionary<string, object>)map["configs"];
                    model.Configs = ComplexRequestConfigs.FromMap(temp);
                }
            }

            if (map.ContainsKey("dict"))
            {
                var dict = map["dict"] as Dictionary<string, object>;
                if (dict != null && dict.Count > 0)
                {
                    var modelMap1 = new Dictionary<string, object>();
                    foreach (KeyValuePair<string, object> entry1 in dict)
                    {
                        modelMap1[entry1.Key] = (object)entry1.Value;
                    }
                    model.Dict = modelMap1;
                }
            }

            if (map.ContainsKey("submodelMap"))
            {
                var dict = map["submodelMap"] as Dictionary<string, Dictionary<string, object>>;
                if (dict != null && dict.Count > 0)
                {
                    var modelMap1 = new Dictionary<string, Darabonba.import.Models.Config>();
                    foreach (KeyValuePair<string, Dictionary<string, object>> entry1 in dict)
                    {
                        modelMap1[entry1.Key] = Darabonba.import.Models.Config.FromMap(entry1.Value);
                    }
                    model.SubmodelMap = modelMap1;
                }
            }

            if (map.ContainsKey("array"))
            {
                var list1 = map["array"] as List<List<Dictionary<string, object>>>;
                if (list1 != null && list1.Count > 0)
                {
                    int n1 = 0;
                    var modelList1 = new List<List<ComplexRequestArray>>();
                    foreach(var item1 in list1)
                    {
                        var list2 = item1;
                        if (list2 != null && list2.Count > 0)
                        {
                            int n2 = 0;
                            var modelList2 = new List<ComplexRequestArray>();
                            foreach(var item2 in list2)
                            {
                                if (item2 != null)
                                {
                                    var temp = (Dictionary<string, object>)item2;
                                    modelList2[n2++] = ComplexRequestArray.FromMap(temp);
                                }
                            }
                            modelList1[n1++] = modelList2;
                        }
                    }
                    model.Array = modelList1;
                }
            }

            if (map.ContainsKey("array1"))
            {
                var list1 = map["array1"] as List<List<string>>;
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
                    model.Array1 = modelList1;
                }
            }

            if (map.ContainsKey("array2"))
            {
                var list1 = map["array2"] as List<Dictionary<string, object>>;
                if (list1 != null && list1.Count > 0)
                {
                    int n1 = 0;
                    var modelList1 = new List<Darabonba.Test.Models.Config>();
                    foreach(var item1 in list1)
                    {
                        modelList1[n1++] = Darabonba.Test.Models.Config.FromMap(item1);
                    }
                    model.Array2 = modelList1;
                }
            }

            return model;
        }
    }

}

