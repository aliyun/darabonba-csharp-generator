// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Darabonba;
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

            }

            [NameInMap("subArray")]
            [Validation(Required=true)]
            public List<Darabonba.Test.Models.Config> SubArray { get; set; }

            [NameInMap("subMutiArray")]
            [Validation(Required=true)]
            public List<List<Darabonba.Test.Models.Config>> SubMutiArray { get; set; }

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

        }

        [NameInMap("array1")]
        [Validation(Required=false)]
        public List<List<string>> Array1 { get; set; }

        [NameInMap("array2")]
        [Validation(Required=false)]
        public List<Darabonba.Test.Models.Config> Array2 { get; set; }

    }

}

