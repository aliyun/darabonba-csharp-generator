// This file is auto-generated, don't edit it. Thanks.

using System;
using System.Collections.Generic;
using System.IO;

using Tea;

namespace Darabonba.Test.Models
{
    public class MultiLayerModel : TeaModel {
        [NameInMap("Data")]
        [Validation(Required=false)]
        public MultiLayerModelData Data { get; set; }
        public class MultiLayerModelData : TeaModel {
            [NameInMap("Results")]
            [Validation(Required=false)]
            public List<MultiLayerModelDataResults> Results { get; set; }
            public class MultiLayerModelDataResults : TeaModel {
                [NameInMap("TextRectangles")]
                [Validation(Required=false)]
                public MultiLayerModelDataResultsTextRectangles TextRectangles { get; set; }
                public class MultiLayerModelDataResultsTextRectangles : TeaModel {
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

                }

                [NameInMap("Text")]
                [Validation(Required=false)]
                public string Text { get; set; }

                [NameInMap("Probability")]
                [Validation(Required=false)]
                public float? Probability { get; set; }

            }

        }

    }

}
