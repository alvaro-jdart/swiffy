using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Jdart.Swiffy.ResponseModels
{
    [DataContract]
    internal class SwiffyResponse
    {
        [DataMember(Name = "output")]
        public string Output { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "version")]
        public string Version { get; set; }
    }
}
