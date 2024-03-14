using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace EventManagementSystem
{
    [ServiceContract]
    public interface IVenueService
    {
        [OperationContract]
        void AddVenue(Venue venueDetails);

        [OperationContract]
        void UpdateVenue(Venue venueDetails);

        [OperationContract]
        void DeleteVenue(int venueId);

        [OperationContract]
        List<Venue> GetAllVenues();
    }
}
