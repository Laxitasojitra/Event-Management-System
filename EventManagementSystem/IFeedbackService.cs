using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace EventManagementSystem
{
    [ServiceContract]
    public interface IFeedbackService
    {
        [OperationContract]
        void AddFeedback(Feedback feedbackDetails);
                        
        [OperationContract]
        void DeleteFeedback(int feedbackNumber);

        [OperationContract]
        List<Feedback> GetAllFeedbacks();
    }

}
