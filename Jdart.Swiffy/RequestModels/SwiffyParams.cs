using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Jdart.Swiffy.RequestModels
{
    [DataContract]
    internal class SwiffyParams
    {
        [DataMember(Name = "client")]
        public string Client { get; set; }

        [DataMember(Name = "input")]
        public string Input { get; set; }
    }
}
