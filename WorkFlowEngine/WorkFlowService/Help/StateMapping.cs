/********************************************************************************
** Class Name:   StateMapping 
** Author：      spring yang
** Create date： 2012-9-1
** Modify：      spring yang
** Modify Date： 2012-9-25
** Summary：     StateMapping class
*********************************************************************************/

using System.Collections.Generic;
using WorkFlowService.DAL;
using WorkFlowService.IDAL;

namespace WorkFlowService.Help
{
    public class StateMapping
    {
        public   List<IStateBase>  StateBasesList;

        private  StateMapping()
        {
            Init();
        }

        public static StateMapping Instance
        {
            get { return new StateMapping(); }
        }

        private void Init()
        {
            StateBasesList = new List<IStateBase>
                                  {
                                      new CommonState(),
                                      new ManageState(),
                                      new DoneState(),
                                      new RefuseState()
                                  };
        }
    }
}
