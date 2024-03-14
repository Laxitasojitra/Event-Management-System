using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace EventManagementSystem
{
    [DataContract]
    public class Venue
    {
        [DataMember]
        public int VId { get; set; }

        [DataMember]
        public string VName { get; set; }

        [DataMember]
        public int VCapacity { get; set; }

        [DataMember]
        public string VAddress { get; set; }

        [DataMember]
        public string VManager { get; set; }

        [DataMember]
        public string VPhone { get; set; }
    }
}

