// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Darabonba;
using Darabonba.Utils;

namespace Darabonba.Test.Models
{
    public class InlineLists : Darabonba.Model {
        [NameInMap("values")]
        [Validation(Required=true)]
        public List<List<InlineListsValues>> Values { get; set; }
        public class InlineListsValues : Darabonba.Model {
            [NameInMap("name")]
            [Validation(Required=true)]
            public string Name { get; set; }

            [NameInMap("children")]
            [Validation(Required=true)]
            public List<Tree> Children { get; set; }

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
                return map;
            }

            public new static InlineListsValues FromMap(IDictionary map)
            {
                if (map == null)
                {
                    return null;
                }
                var model = new InlineListsValues();
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
                return model;
            }
        }

        public override Dictionary<string, object> ToMapCore(bool noStream = false)
        {
            var map = new Dictionary<string, object>();
            {
                List<object> value0 = null;
                if (this.Values != null)
                {
                    value0 = new List<object>();
                    foreach (var item0 in this.Values)
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
                map["values"] = value0;
            }
            return map;
        }

        public new static InlineLists FromMap(IDictionary map)
        {
            if (map == null)
            {
                return null;
            }
            var model = new InlineLists();
            if (map.Contains("values"))
            {
                List<List<InlineListsValues>> value0 = null;
                if (map["values"] != null)
                {
                    value0 = new List<List<InlineListsValues>>();
                    foreach (var item0 in (IList)map["values"])
                    {
                        List<InlineListsValues> value1 = null;
                        if (item0 != null)
                        {
                            value1 = new List<InlineListsValues>();
                            foreach (var item1 in (IList)item0)
                            {
                                value1.Add(item1 == null ? null : InlineListsValues.FromMap((IDictionary)item1));
                            }
                        }
                        value0.Add(value1);
                    }
                }
                model.Values = value0;
            }
            return model;
        }
    }

}

