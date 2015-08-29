using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Jdart.Swiffy.ResponseModels
{
    [DataContract]
    internal class GoogleApiResult
    {
        [DataMember(Name = "response")]
        public SwiffyResponse Response { get; set; }
    }
}
