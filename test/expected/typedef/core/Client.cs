using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Darabonba;
using Darabonba.Utils;
using System.Net.Http;
using Tea;
using System.Net.Http.Headers;
using OSSClient = Darabonba.import.Client;
using Darabonba.Test.Models;

namespace Darabonba.Test
{
    public class Client 
    {
        protected HttpRequestMessage _vid;
        protected TeaModel _model;

        public Client(HttpRequestMessage request, TeaModel model_)
        {
            this._vid = request;
            this._model = model_;
        }


        public void Main(HttpRequestMessage test1, HttpRequestHeaders test2, TeaModel test3)
        {
            OSSClient oss = new OSSClient(test1);
            M m = new M
            {
                A = test1,
                B = test2,
            };
            this._vid = test1;
            this._model = test3;
        }


        public HttpResponseMessage TestHttpRequest(HttpRequestMessage req)
        {
            return TestHttpRequestWith("test", req);
        }

        public async Task<HttpResponseMessage> TestHttpRequestAsync(HttpRequestMessage req)
        {
            return TestHttpRequestWith("test", req);
        }

        public static HttpResponseMessage TestHttpRequestWith(string method, HttpRequestMessage req)
        {
            throw new NotImplementedException();
        }

        public static HttpResponseMessage TestHttpHeader(string method, HttpRequestHeaders headers)
        {
            throw new NotImplementedException();
        }

    }
}

