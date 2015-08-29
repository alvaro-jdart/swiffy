using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Jdart.Swiffy.Helpers
{
    internal class PushStreamContent : HttpContent
    {
        private readonly Func<Stream, Task> _pushAction;

        public PushStreamContent(Func<Stream, Task> pushAction)
        {
            _pushAction = pushAction;
        }

        protected override async Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            await _pushAction(stream);
        }

        protected override bool TryComputeLength(out long length)
        {
            length = -1;
            return false;
        }
    }
}
