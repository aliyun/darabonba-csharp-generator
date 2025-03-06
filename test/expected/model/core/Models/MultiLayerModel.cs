// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Darabonba;

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

