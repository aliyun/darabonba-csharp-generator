// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Darabonba;
using Darabonba.Utils;

namespace Darabonba.Test.Models
{
    public class MultiLayerModel : Darabonba.Model {
        [NameInMap("Data")]
        [Validation(Required=false)]
        public MultiLayerModelData Data { get; set; }
        public class MultiLayerModelData : Darabonba.Model {
            [NameInMap("Results")]
            [Validation(Required=false)]
            public List<MultiLayerModelDataResults> Results { get; set; }
            public class MultiLayerModelDataResults : Darabonba.Model {
                [NameInMap("TextRectangles")]
                [Validation(Required=false)]
                public MultiLayerModelDataResultsTextRectangles TextRectangles { get; set; }
                public class MultiLayerModelDataResultsTextRectangles : Darabonba.Model {
                    [NameInMap("Top")]
                    [Validation(Required=false)]
                    public int? Top { get; set; }

                    [NameInMap("Width")]
                    [Validation(Required=false)]
                    public int? Width { get; set; }

                    [NameInMap("Height")]
                    [Validation(Required=false)]
                    public int? Height { get; set; }

                    [NameInMap("Angle")]
                    [Validation(Required=false)]
                    public int? Angle { get; set; }

                    [NameInMap("Left")]
                    [Validation(Required=false)]
                    public int? Left { get; set; }

                    public override Dictionary<string, object> ToMapCore(bool noStream = false)
                    {
                        var map = new Dictionary<string, object>();
                        map["Top"] = this.Top;
                        map["Width"] = this.Width;
                        map["Height"] = this.Height;
                        map["Angle"] = this.Angle;
                        map["Left"] = this.Left;
                        return map;
                    }

                    public new static MultiLayerModelDataResultsTextRectangles FromMap(IDictionary map)
                    {
                        if (map == null)
                        {
                            return null;
                        }
                        var model = new MultiLayerModelDataResultsTextRectangles();
                        if (map.Contains("Top"))
                        {
                            model.Top = map["Top"] == null ? (int?)null : System.Convert.ToInt32(map["Top"]);
                        }
                        if (map.Contains("Width"))
                        {
                            model.Width = map["Width"] == null ? (int?)null : System.Convert.ToInt32(map["Width"]);
                        }
                        if (map.Contains("Height"))
                        {
                            model.Height = map["Height"] == null ? (int?)null : System.Convert.ToInt32(map["Height"]);
                        }
                        if (map.Contains("Angle"))
                        {
                            model.Angle = map["Angle"] == null ? (int?)null : System.Convert.ToInt32(map["Angle"]);
                        }
                        if (map.Contains("Left"))
                        {
                            model.Left = map["Left"] == null ? (int?)null : System.Convert.ToInt32(map["Left"]);
                        }
                        return model;
                    }
                }

                [NameInMap("Text")]
                [Validation(Required=false)]
                public string Text { get; set; }

                [NameInMap("Probability")]
                [Validation(Required=false)]
                public float? Probability { get; set; }

                public override Dictionary<string, object> ToMapCore(bool noStream = false)
                {
                    var map = new Dictionary<string, object>();
                    map["TextRectangles"] = this.TextRectangles == null ? null : this.TextRectangles.ToMap();
                    map["Text"] = this.Text;
                    map["Probability"] = this.Probability;
                    return map;
                }

                public new static MultiLayerModelDataResults FromMap(IDictionary map)
                {
                    if (map == null)
                    {
                        return null;
                    }
                    var model = new MultiLayerModelDataResults();
                    if (map.Contains("TextRectangles"))
                    {
                        model.TextRectangles = map["TextRectangles"] == null ? null : MultiLayerModelDataResultsTextRectangles.FromMap((IDictionary)map["TextRectangles"]);
                    }
                    if (map.Contains("Text"))
                    {
                        model.Text = (string)map["Text"];
                    }
                    if (map.Contains("Probability"))
                    {
                        model.Probability = map["Probability"] == null ? (float?)null : System.Convert.ToSingle(map["Probability"]);
                    }
                    return model;
                }
            }

            public override Dictionary<string, object> ToMapCore(bool noStream = false)
            {
                var map = new Dictionary<string, object>();
                {
                    List<object> value0 = null;
                    if (this.Results != null)
                    {
                        value0 = new List<object>();
                        foreach (var item0 in this.Results)
                        {
                            value0.Add(item0 == null ? null : item0.ToMap());
                        }
                    }
                    map["Results"] = value0;
                }
                return map;
            }

            public new static MultiLayerModelData FromMap(IDictionary map)
            {
                if (map == null)
                {
                    return null;
                }
                var model = new MultiLayerModelData();
                if (map.Contains("Results"))
                {
                    List<MultiLayerModelDataResults> value0 = null;
                    if (map["Results"] != null)
                    {
                        value0 = new List<MultiLayerModelDataResults>();
                        foreach (var item0 in (IList)map["Results"])
                        {
                            value0.Add(item0 == null ? null : MultiLayerModelDataResults.FromMap((IDictionary)item0));
                        }
                    }
                    model.Results = value0;
                }
                return model;
            }
        }

        public override Dictionary<string, object> ToMapCore(bool noStream = false)
        {
            var map = new Dictionary<string, object>();
            map["Data"] = this.Data == null ? null : this.Data.ToMap();
            return map;
        }

        public new static MultiLayerModel FromMap(IDictionary map)
        {
            if (map == null)
            {
                return null;
            }
            var model = new MultiLayerModel();
            if (map.Contains("Data"))
            {
                model.Data = map["Data"] == null ? null : MultiLayerModelData.FromMap((IDictionary)map["Data"]);
            }
            return model;
        }
    }

}

