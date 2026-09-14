using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Darabonba;
using Darabonba.Test;
using Darabonba.Test.Models;

class ModelMappingCheck
{
    class ReflectionProbe : LowerModel
    {
        public string Extra
        {
            get { throw new Exception("ToMap inspected an undeclared property through reflection"); }
        }
    }

    static void Check(bool condition, string message)
    {
        if (!condition) throw new Exception(message);
    }

    static void Equal(object expected, object actual)
    {
        if (expected == null || actual == null)
        {
            Check(expected == actual, "Null changed during conversion");
        }
        else if (expected is IDictionary)
        {
            var left = (IDictionary)expected;
            var right = actual as IDictionary;
            Check(right != null && left.Count == right.Count, "Map shape changed");
            foreach (DictionaryEntry entry in left)
            {
                Check(right.Contains(entry.Key), "Missing key: " + entry.Key);
                Equal(entry.Value, right[entry.Key]);
            }
        }
        else if (expected is IList)
        {
            var left = (IList)expected;
            var right = actual as IList;
            Check(right != null && left.Count == right.Count, "List shape changed");
            for (int i = 0; i < left.Count; i++) Equal(left[i], right[i]);
        }
        else
        {
            Check(expected.Equals(actual), "Value changed: " + expected + " / " + actual);
        }
    }

    static void Main()
    {
        foreach (string culture in new[] { "en-US", "fr-FR" })
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(culture);
            RunChecks();
        }
    }

    static void RunChecks()
    {
        Check((string)new ReflectionProbe { Stringfield = "direct" }.ToMap()["stringfield"] == "direct", "Generated ToMap was not called");
        Darabonba.Model baseModel = new ReflectionProbe { Stringfield = "base" };
        Check((string)baseModel.ToMap()["stringfield"] == "base", "Base model did not dispatch to generated ToMap");
        Darabonba.Model nullModel = null;
        Check(nullModel.ToMap() == null, "Null extension method compatibility changed");
        var eventChild = BuiltinChild.FromMap(new Dictionary<string, object> { { "data", "event" }, { "retry", 4L }, { "name", "child" } });
        Check(eventChild.Retry == 4 && (string)eventChild.ToMap()["data"] == "event" && (string)eventChild.ToMap()["name"] == "child", "Builtin model inheritance failed");
        Darabonba.Request requestChild = RequestChild.FromMap(new Dictionary<string, object> { { "port", 443L }, { "count", 2L }, { "method", "POST" } });
        Check(requestChild.Port == 443 && (int)requestChild.ToMap()["count"] == 2, "Builtin request inheritance failed");
        var child = new Dictionary<string, object> { { "stringfield", "child" } };
        var bytes = new byte[] { 0, 128, 255 };
        var input = new Dictionary<string, object>
        {
            { "nestedLists", new object[] { new object[] { new object[] { child, null }, new object[0], null }, null } },
            { "listMaps", new object[] { new Dictionary<string, object> { { "child", child }, { "null", null } }, null } },
            { "mapLists", new Dictionary<string, object> { { "children", new object[] { child, null } }, { "empty", new object[0] }, { "null", null } } },
            { "nestedMaps", new Dictionary<string, object> { { "children", new Dictionary<string, object> { { "child", child } } }, { "empty", new Dictionary<string, object>() }, { "null", null } } },
            { "numbers", new object[] { 42L, "7", null } },
            { "booleans", new Dictionary<string, object> { { "true", true }, { "false", "false" }, { "null", null } } },
            { "bytes", bytes },
            { "int8Value", -128L }, { "uint8Value", 255L },
            { "int16Value", -32768L }, { "uint16Value", 65535L },
            { "int32Value", -2147483648L }, { "uint32Value", 4294967295L },
            { "int64Value", long.MinValue }, { "uint64Value", ulong.MaxValue },
            { "floatValue", 1.25d }, { "doubleValue", 1.5d }
        };
        input["mapListMaps"] = new Dictionary<string, object> { { "outer", new object[] { new Dictionary<string, object> { { "inner", new object[] { child, null } } }, null } } };
        input["listMapLists"] = new object[] { new Dictionary<string, object> { { "inner", new object[] { new object[] { child, null }, new object[0], null } } }, null };
        var model = Mapping.FromMap(input);
        Check(model.MapListMaps["outer"][0]["inner"][0].Stringfield == "child" && model.MapListMaps["outer"][1] == null, "Map/list/map/list conversion failed");
        Check(model.ListMapLists[0]["inner"][0][0].Stringfield == "child" && model.ListMapLists[0]["inner"][1].Count == 0, "List/map/list/list conversion failed");
        Check(model.NestedLists[0][0][0].Stringfield == "child", "Nested model was not converted");
        Check(model.NestedLists[0][0][1] == null && model.NestedLists[0][1].Count == 0 && model.NestedLists[0][2] == null, "Nested null/empty list was lost");
        Check(model.MapLists["empty"].Count == 0 && model.MapLists["null"] == null, "Empty/null map value was lost");
        Check(model.Numbers[0] == 42 && model.Numbers[1] == 7 && model.Numbers[2] == null, "Nullable numeric conversion failed");
        Check(model.Booleans["true"] == true && model.Booleans["false"] == false && model.Booleans["null"] == null, "Boolean conversion failed");
        Check(model.Int8Value == sbyte.MinValue && model.Uint8Value == byte.MaxValue, "8-bit conversion failed");
        Check(model.Int16Value == short.MinValue && model.Uint16Value == ushort.MaxValue, "16-bit conversion failed");
        Check(model.Int32Value == int.MinValue && model.Uint32Value == uint.MaxValue, "32-bit conversion failed");
        Check(model.Int64Value == long.MinValue && model.Uint64Value == ulong.MaxValue, "64-bit conversion failed");
        Check(model.FloatValue == 1.25f && model.DoubleValue == 1.5d, "Floating-point conversion failed");
        var map = model.ToMap();
        Equal(map, Mapping.FromMap(map).ToMap());
        Check(!Object.ReferenceEquals(map["mapLists"], model.MapLists), "ToMap must copy collections");
        Equal(bytes, map["bytes"]);
        Check(Mapping.FromMap(null) == null, "Null input must return null");
        var empty = Mapping.FromMap(new Dictionary<string, object> { { "numbers", new List<object>() }, { "booleans", new Dictionary<string, string>() } });
        Check(empty.Numbers.Count == 0 && empty.Booleans.Count == 0, "Empty collections must not become null");

        var stream = new MemoryStream(bytes);
        var simple = Client.ConvertModel(new Dictionary<string, object>
        {
            { "realName", "renamed" }, { "stringfield", "12" }, { "numberfield", 42L },
            { "mapfield", new Dictionary<string, string> { { "header", "value" }, { "null", null } } },
            { "submodel", child }, { "readable", stream },
            { "unknown", "ignored" }
        });
        Check(simple.Name == "renamed" && simple.Stringfield == "12" && simple.Numberfield == 42, "API return/field conversion failed");
        Check(simple.Submodel.Stringfield == "child" && simple.Mapfield["header"] == "value", "Typed map conversion failed");
        Check(Object.ReferenceEquals(simple.Readable, stream), "FromMap must preserve streams");
        Check(!simple.ToMap().ContainsKey("readable") && !simple.ToMap().ContainsKey("unknown"), "Stream or unknown field was serialized");
        Check(simple.ToMap().ContainsKey("m") && simple.ToMap()["m"] == null, "Null field must be preserved");
        simple.Object = new Dictionary<string, object> { { "model", new LowerModel { Stringfield = "dynamic" } }, { "headers", new Dictionary<string, string> { { "key", "value" } } } };
        var dynamicValue = simple.Object["model"];
        Check(Object.ReferenceEquals(((IDictionary)simple.ToMap()["object"])["model"], dynamicValue), "Any values must pass through unchanged");
        simple.M = new ReflectionProbe { Stringfield = "typed-base" };
        Check((string)((IDictionary)simple.ToMap()["m"])["stringfield"] == "typed-base", "$Model field used reflection");
        var derived = Extend.FromMap(new Dictionary<string, object> { { "name", "parent" }, { "size", 3L } });
        Parent parent = derived;
        Check(derived.Name == "parent" && derived.Size == 3 && (int)parent.ToMap()["size"] == 3, "Inheritance lost fields");
        Equal(derived.ToMap(), Extend.FromMap(derived.ToMap()).ToMap());
        Check(Mapping.FromMap(new Dictionary<string, object> { { "doubleValue", (1.5d).ToString() } }).DoubleValue == 1.5d, "Culture-sensitive numeric conversion changed");
        foreach (var value in new object[] { new Dictionary<string, object> { { "nested", new List<object> { 1L, null } } }, new List<object> { "text", 2L } })
        {
            var stringInput = new Dictionary<string, object> { { "stringfield", value } };
            bool invalid = false;
            try { LowerModel.FromMap(stringInput); }
            catch (InvalidCastException) { invalid = true; }
            Check(invalid, "String fields must not serialize arbitrary collections");
        }
        bool overflow = false;
        try { Mapping.FromMap(new Dictionary<string, object> { { "uint8Value", 256L } }); }
        catch (OverflowException) { overflow = true; }
        Check(overflow, "Numeric overflow must not be swallowed");
        var inherited = Extend.FromMap(new Dictionary<string, object> { { "details", new Dictionary<string, object> { { "value", "inherited" } } } });
        Check(inherited.Details.Value == "inherited", "Inherited anonymous model was not converted");
        Equal(inherited.ToMap(), Extend.FromMap(inherited.ToMap()).ToMap());

        var tree = new Tree { Name = "leaf", Children = new List<Tree>(), Branches = new Dictionary<string, Tree>() };
        for (int i = 0; i < 64; i++)
        {
            tree = new Tree
            {
                Name = "node-" + i,
                Children = i % 3 == 0 ? new List<Tree> { tree, null } : null,
                Branches = i % 3 == 1 ? new Dictionary<string, Tree> { { "child", tree }, { "null", null } } : null,
                Next = i % 3 == 2 ? tree : null
            };
        }
        var treeMap = tree.ToMap();
        var treeCopy = Tree.FromMap(treeMap);
        Equal(treeMap, treeCopy.ToMap());
        Check(!Object.ReferenceEquals(tree, treeCopy), "FromMap reused the original model");
        int depth = 0;
        for (var cursor = treeCopy; cursor.Name != "leaf"; depth++)
        {
            cursor = cursor.Next ?? (cursor.Children != null ? cursor.Children[0] : cursor.Branches["child"]);
        }
        Check(depth == 64, "Self-referencing model lost recursive levels");

        var inline = InlineLists.FromMap(new Dictionary<string, object>
        {
            { "values", new object[] { new object[] { new Dictionary<string, object> { { "name", "inline" }, { "children", new object[] { treeMap, null } } }, null }, new object[0], null } }
        });
        Check(inline.Values[0][0].Name == "inline" && inline.Values[0][0].Children[0].Name == "node-63", "Nested anonymous model conversion failed");
        Check(inline.Values[0][1] == null && inline.Values[1].Count == 0 && inline.Values[2] == null, "Anonymous model list lost null/empty values");
        Equal(inline.ToMap(), InlineLists.FromMap(inline.ToMap()).ToMap());
        var mutual = new Left { Right = new Right { Left = new Left() } };
        var mutualCopy = Left.FromMap(mutual.ToMap());
        Check(mutualCopy.Right.Left != null && mutualCopy.Right.Left.Right == null, "Mutually recursive models were truncated");
        Equal(mutual.ToMap(), mutualCopy.ToMap());
        Console.WriteLine("Model mapping checks passed (nested collections, anonymous models, inheritance, 64-level recursion and mutual recursion).");
    }
}
