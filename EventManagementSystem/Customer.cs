using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EventManagementSystem
{
    public class Customer
    {
        [DataMember]
        public int CustId { get; set; }

        [DataMember]
        public string CustName { get; set; }

        [DataMember]
        public string CustPhone { get; set; }
    }
}
