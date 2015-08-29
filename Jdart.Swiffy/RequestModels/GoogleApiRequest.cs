using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Jdart.Swiffy.RequestModels
{
    [DataContract]
    internal class GoogleApiRequest
    {
        [DataMember(Name = "apiVersion")]
        public string ApiVersion { get; set; }

        [DataMember(Name = "method")]
        public string Method { get; set; }

        [DataMember(Name = "params")]
        public SwiffyParams Params { get; set; }
    }
}
