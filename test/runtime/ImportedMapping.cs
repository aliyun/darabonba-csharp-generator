using System;
using System.Collections;
using System.Collections.Generic;
using Darabonba;
using Darabonba.Test;
using Darabonba.Test.Models;
using ImportedRequest = Darabonba.import.Models.Request;

class ImportedMappingCheck
{
    static void Check(bool condition, string message)
    {
        if (!condition) throw new Exception(message);
    }

    static void Main()
    {
        var item = new Dictionary<string, object> { { "accesskey", "key" }, { "region", "region" } };
        ImportedRequest.FromMapCalls = 0;
        var model = MyModel.FromMap(new Dictionary<string, object>
        {
            { "ssubmmarray", new object[] { new object[] { item, null }, new object[0], null } },
            { "moduleModelMap", new Dictionary<string, object> { { "item", item }, { "null", null } } }
        });
        Check(ImportedRequest.FromMapCalls == 2, "Imported FromMap was not called");
        Check(model.Ssubmmarray[0][0].Accesskey == "key" && model.Ssubmmarray[1].Count == 0 && model.Ssubmmarray[2] == null, "Imported nested list conversion failed");
        var map = model.ToMap();
        var nested = (IDictionary)((IList)((IList)map["ssubmmarray"])[0])[0];
        Check((string)nested["accesskey"] == "key", "Imported ToMap was not called");
        Check(MyModel.FromMap(map).ModuleModelMap["item"].Region == "region", "Imported round trip failed");
        Check(Client.ImportedModel(item).Accesskey == "key", "Imported API return did not use FromMap");
        var inherited = ImportedGrandchild.FromMap(new Dictionary<string, object>
        {
            { "accesskey", "inherited" }, { "region", "region" }, { "count", 3L }, { "values", new object[] { item, null } }
        });
        Darabonba.Model baseModel = inherited;
        var inheritedMap = baseModel.ToMap();
        Check((string)inheritedMap["accesskey"] == "inherited" && (int)inheritedMap["count"] == 3, "Inherited fields were lost");
        var copy = ImportedGrandchild.FromMap(inheritedMap);
        Check(copy.Values[0].Region == "region" && copy.Values[1] == null && copy.Accesskey == "inherited", "External inheritance round trip failed");
        var exceptionModel = Darabonba.Test.Exceptions.MainFileException.MainFileExceptionModel.FromMap(new Dictionary<string, object>
        {
            { "str", "outer" }, { "model", new Dictionary<string, object> { { "str", "inner" } } }
        });
        Check(exceptionModel.Model.Str == "inner" && (string)((IDictionary)exceptionModel.ToMap()["model"])["str"] == "inner", "Anonymous exception model conversion failed");
        Console.WriteLine("Imported model checks passed (direct calls, API returns, nested collections and external inheritance).");
    }
}
