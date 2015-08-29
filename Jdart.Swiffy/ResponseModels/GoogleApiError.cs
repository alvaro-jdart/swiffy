using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Jdart.Swiffy.ResponseModels
{
    [DataContract]
    internal class GoogleApiError
    {
        [DataMember(Name = "message")]
        public string Message { get; set; }
    }
}
