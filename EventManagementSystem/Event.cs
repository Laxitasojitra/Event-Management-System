using System;
using System.Runtime.Serialization;

namespace EventManagementSystem
{
    [DataContract]
    public class Event
    {
        [DataMember]
        public int EvId { get; set; }

        [DataMember]
        public string EvName { get; set; }

        [DataMember]
        public DateTime EvDate { get; set; }

        [DataMember]
        public int VenueId { get; set; }

        [DataMember]
        public string EvVenue { get; set; }

        [DataMember]
        public int EvDuration { get; set; }

        [DataMember]
        public int EvCusId { get; set; }

        [DataMember]
        public string EvCustName { get; set; }

        [DataMember]
        public string EvStatus { get; set; }
    }
}
