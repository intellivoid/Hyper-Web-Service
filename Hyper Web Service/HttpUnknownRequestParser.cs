﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Intellivoid.HyperWS
{
    internal class HttpUnknownRequestParser : HttpRequestParser
    {
        public HttpUnknownRequestParser(HttpClient client, int contentLength)
            : base(client, contentLength)
        {
            Client.InputStream = new MemoryStream();
        }

        public override void Parse()
        {
            Client.ReadBuffer.CopyToStream(Client.InputStream, ContentLength);

            if (Client.InputStream.Length == ContentLength)
                EndParsing();
        }
    }
}
