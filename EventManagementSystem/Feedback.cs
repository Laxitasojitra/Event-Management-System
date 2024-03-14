using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EventManagementSystem
{
    [DataContract]
    public class Feedback
    {
        [DataMember]
        public int FbNum { get; set; }

        [DataMember]
        public int EvId { get; set; }

        [DataMember]
        public string EvName { get; set; }

        [DataMember]
        public int Venue { get; set; }

        [DataMember]
        public int Punctuality { get; set; }

        [DataMember]
        public int Hospitality { get; set; }

        [DataMember]
        public int Overall { get; set; }
    }
}
