// This file is auto-generated, don't edit it. Thanks.

using Darabonba.Test.Models;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tea;
using Tea.Utils;
using Darabonba.import.Models;
using SourceClient = Darabonba.import.Client;

namespace Darabonba.Test
{
    public interface IClient
    {
        RuntimeObject Complex1(ComplexRequest request, SourceClient client);

        Dictionary<string, object> Complex2(ComplexRequest request, List<string> str, Dictionary<string, string> val, List<List<List<string>>> complexList);

        ComplexRequest Complex3(ComplexRequest request);

        List<string> Hello(Dictionary<string, object> request, List<string> strs);

        string TemplateString();
    }
}

