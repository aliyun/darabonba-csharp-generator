using System.Collections;
using System.Collections.Generic;

// An independently implemented dependency verifies calls across SDK boundaries.
namespace Darabonba.import
{
    public class Client { }
}
namespace Darabonba.import.Models
{
    public class Request : Darabonba.Model
    {
        public string Accesskey { get; set; }
        public string Region { get; set; }
        public string ReflectionOnly { get { throw new System.Exception("Imported model was reflected"); } }
        public static int FromMapCalls;

        public override Dictionary<string, object> ToMapCore(bool noStream = false)
        {
            return new Dictionary<string, object> { { "accesskey", Accesskey }, { "region", Region } };
        }

        public new static Request FromMap(IDictionary map)
        {
            FromMapCalls++;
            if (map == null) return null;
            var model = new Request();
            if (map.Contains("accesskey")) model.Accesskey = (string)map["accesskey"];
            if (map.Contains("region")) model.Region = (string)map["region"];
            return model;
        }
    }
}
