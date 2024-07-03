// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Darabonba;

namespace Darabonba.Test.Models
{
    public class MultiLayerModel : DaraModel {
        [NameInMap("Data")]
        [Validation(Required=false)]
        public MultiLayerModelData Data { get; set; }
        public class MultiLayerModelData : DaraModel {
            [NameInMap("Results")]
            [Validation(Required=false)]
            public List<MultiLayerModelDataResults> Results { get; set; }
            public class MultiLayerModelDataResults : DaraModel {
                [NameInMap("TextRectangles")]
                [Validation(Required=false)]
                public MultiLayerModelDataResultsTextRectangles TextRectangles { get; set; }
                public class MultiLayerModelDataResultsTextRectangles : DaraModel {
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

                    public MultiLayerModelDataResultsTextRectangles Copy()
                    {
                        MultiLayerModelDataResultsTextRectangles copy = FromMap(ToMap());
                        return copy;
                    }

                    public MultiLayerModelDataResultsTextRectangles CopyWithoutStream()
                    {
                        MultiLayerModelDataResultsTextRectangles copy = FromMap(ToMap(true));
                        return copy;
                    }

                    public Dictionary<string, object> ToMap(bool noStream = false)
                    {
                        var map = new Dictionary<string, object>();
                        if (Top != null)
                        {
                            map["Top"] = Top;
                        }

                        if (Width != null)
                        {
                            map["Width"] = Width;
                        }

                        if (Height != null)
                        {
                            map["Height"] = Height;
                        }

                        if (Angle != null)
                        {
                            map["Angle"] = Angle;
                        }

                        if (Left != null)
                        {
                            map["Left"] = Left;
                        }

                        return map;
                    }

                    public static MultiLayerModelDataResultsTextRectangles FromMap(Dictionary<string, object> map)
                    {
                        var model = new MultiLayerModelDataResultsTextRectangles();
                        if (map.ContainsKey("Top"))
                        {
                            model.Top = (int?)map["Top"];
                        }

                        if (map.ContainsKey("Width"))
                        {
                            model.Width = (int?)map["Width"];
                        }

                        if (map.ContainsKey("Height"))
                        {
                            model.Height = (int?)map["Height"];
                        }

                        if (map.ContainsKey("Angle"))
                        {
                            model.Angle = (int?)map["Angle"];
                        }

                        if (map.ContainsKey("Left"))
                        {
                            model.Left = (int?)map["Left"];
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

                public MultiLayerModelDataResults Copy()
                {
                    MultiLayerModelDataResults copy = FromMap(ToMap());
                    return copy;
                }

                public MultiLayerModelDataResults CopyWithoutStream()
                {
                    MultiLayerModelDataResults copy = FromMap(ToMap(true));
                    return copy;
                }

                public Dictionary<string, object> ToMap(bool noStream = false)
                {
                    var map = new Dictionary<string, object>();
                    if (TextRectangles != null)
                    {
                        map["TextRectangles"] = TextRectangles != null ? TextRectangles.ToMap(noStream) : null;
                    }

                    if (Text != null)
                    {
                        map["Text"] = Text;
                    }

                    if (Probability != null)
                    {
                        map["Probability"] = Probability;
                    }

                    return map;
                }

                public static MultiLayerModelDataResults FromMap(Dictionary<string, object> map)
                {
                    var model = new MultiLayerModelDataResults();
                    if (map.ContainsKey("TextRectangles"))
                    {
                        if (map["TextRectangles"] != null)
                        {
                            var temp = (Dictionary<string, object>)map["TextRectangles"];
                            model.TextRectangles = MultiLayerModelDataResultsTextRectangles.FromMap(temp);
                        }
                    }

                    if (map.ContainsKey("Text"))
                    {
                        model.Text = (string)map["Text"];
                    }

                    if (map.ContainsKey("Probability"))
                    {
                        model.Probability = (float?)map["Probability"];
                    }

                    return model;
                }
            }

            public MultiLayerModelData Copy()
            {
                MultiLayerModelData copy = FromMap(ToMap());
                return copy;
            }

            public MultiLayerModelData CopyWithoutStream()
            {
                MultiLayerModelData copy = FromMap(ToMap(true));
                return copy;
            }

            public Dictionary<string, object> ToMap(bool noStream = false)
            {
                var map = new Dictionary<string, object>();
                if (Results != null)
                {
                    var list1 = new List<Dictionary<string, object>>();
                    int n1 = 0;
                    foreach (var item1 in Results) 
                    {
                        list1[n1++] = item1 != null ? item1.ToMap(noStream) : null;
                    }
                    map["Results"] = list1;
                }

                return map;
            }

            public static MultiLayerModelData FromMap(Dictionary<string, object> map)
            {
                var model = new MultiLayerModelData();
                if (map.ContainsKey("Results"))
                {
                    var list1 = map["Results"] as List<Dictionary<string, object>>;
                    if (list1 != null && list1.Count > 0)
                    {
                        int n1 = 0;
                        var modelList1 = new List<MultiLayerModelDataResults>();
                        foreach(var item1 in list1)
                        {
                            if (item1 != null)
                            {
                                var temp = (Dictionary<string, object>)item1;
                                modelList1[n1++] = MultiLayerModelDataResults.FromMap(temp);
                            }
                        }
                        model.Results = modelList1;
                    }
                }

                return model;
            }
        }

        public MultiLayerModel Copy()
        {
            MultiLayerModel copy = FromMap(ToMap());
            return copy;
        }

        public MultiLayerModel CopyWithoutStream()
        {
            MultiLayerModel copy = FromMap(ToMap(true));
            return copy;
        }

        public Dictionary<string, object> ToMap(bool noStream = false)
        {
            var map = new Dictionary<string, object>();
            if (Data != null)
            {
                map["Data"] = Data != null ? Data.ToMap(noStream) : null;
            }

            return map;
        }

        public static MultiLayerModel FromMap(Dictionary<string, object> map)
        {
            var model = new MultiLayerModel();
            if (map.ContainsKey("Data"))
            {
                if (map["Data"] != null)
                {
                    var temp = (Dictionary<string, object>)map["Data"];
                    model.Data = MultiLayerModelData.FromMap(temp);
                }
            }

            return model;
        }
    }

}

