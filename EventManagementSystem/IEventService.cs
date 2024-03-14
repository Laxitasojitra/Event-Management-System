using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace EventManagementSystem
{
    [ServiceContract]
    public interface IEventService
    {
            [OperationContract]
            void AddEvent(Event eventDetails);

            [OperationContract]
            void UpdateEvent(Event eventDetails);

            [OperationContract]
            void DeleteEvent(int eventId);

            [OperationContract]
            List<Event> GetAllEvents();
        }
    }

