using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkFlowService.Help;

namespace WorkFlowService.IDAL
{
    public interface IUserOperationActivity
    {
        string CreateUser(string userName, string password);

        string AssignOperationRole(string userName, string workflowState, ActivityState operationActivity);



    }
}
