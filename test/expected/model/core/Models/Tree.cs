// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Darabonba;
using Darabonba.Utils;

namespace Darabonba.Test.Models
{
    public class Tree : Darabonba.Model {
        [NameInMap("name")]
        [Validation(Required=true)]
        public string Name { get; set; }

        [NameInMap("children")]
        [Validation(Required=true)]
        public List<Tree> Children { get; set; }

        [NameInMap("branches")]
        [Validation(Required=true)]
        public Dictionary<string, Tree> Branches { get; set; }

        [NameInMap("next")]
        [Validation(Required=true)]
        public Tree Next { get; set; }

        public override Dictionary<string, object> ToMapCore(bool noStream = false)
        {
            var map = new Dictionary<string, object>();
            map["name"] = this.Name;
            {
                List<object> value0 = null;
                if (this.Children != null)
                {
                    value0 = new List<object>();
                    foreach (var item0 in this.Children)
                    {
                        value0.Add(item0 == null ? null : item0.ToMap());
                    }
                }
                map["children"] = value0;
            }
            {
                Dictionary<string, object> value0 = null;
                if (this.Branches != null)
                {
                    value0 = new Dictionary<string, object>();
                    foreach (var item0 in this.Branches)
                    {
                        value0.Add(item0.Key, item0.Value == null ? null : item0.Value.ToMap());
                    }
                }
                map["branches"] = value0;
            }
            map["next"] = this.Next == null ? null : this.Next.ToMap();
            return map;
        }

        public new static Tree FromMap(IDictionary map)
        {
            if (map == null)
            {
                return null;
            }
            var model = new Tree();
            if (map.Contains("name"))
            {
                model.Name = (string)map["name"];
            }
            if (map.Contains("children"))
            {
                List<Tree> value0 = null;
                if (map["children"] != null)
                {
                    value0 = new List<Tree>();
                    foreach (var item0 in (IList)map["children"])
                    {
                        value0.Add(item0 == null ? null : Darabonba.Test.Models.Tree.FromMap((IDictionary)item0));
                    }
                }
                model.Children = value0;
            }
            if (map.Contains("branches"))
            {
                Dictionary<string, Tree> value0 = null;
                if (map["branches"] != null)
                {
                    value0 = new Dictionary<string, Tree>();
                    foreach (DictionaryEntry item0 in (IDictionary)map["branches"])
                    {
                        value0.Add((string)item0.Key, item0.Value == null ? null : Darabonba.Test.Models.Tree.FromMap((IDictionary)item0.Value));
                    }
                }
                model.Branches = value0;
            }
            if (map.Contains("next"))
            {
                model.Next = map["next"] == null ? null : Darabonba.Test.Models.Tree.FromMap((IDictionary)map["next"]);
            }
            return model;
        }
    }

}

