using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Jdart.Swiffy.ResponseModels
{
    [DataContract]
    internal class GoogleApiResponse
    {
        [DataMember(Name = "result")]
        public GoogleApiResult Result { get; set; }

        [DataMember(Name = "error")]
        public GoogleApiError Error { get; set; }
    }
}
