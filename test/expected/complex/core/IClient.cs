// This file is auto-generated, don't edit it. Thanks.

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using Tea;
using Tea.Utils;

using Darabonba.Test.Models;

namespace Darabonba.Test
{
    public interface IClient 
    {
        Darabonba.import.Models.RuntimeObject Complex1(ComplexRequest request, Darabonba.import.Client client);

        Dictionary<string, object> Complex2(ComplexRequest request, List<string> str, Dictionary<string, string> val, List<List<List<string>>> complexList);

        ComplexRequest Complex3(ComplexRequest request);

        List<string> Hello(Dictionary<string, object> request, List<string> strs);

        string TemplateString();
   }
}
