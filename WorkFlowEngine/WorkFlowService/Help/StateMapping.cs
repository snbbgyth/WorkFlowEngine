using System.Collections.Generic;
using WorkFlowService.DAL;
using WorkFlowService.IDAL;

namespace WorkFlowService.Help
{
    public class StateMapping
    {
        public   List<IStateBase>  IStateBasesList;

        private  StateMapping()
        {
            Init();
        }

        public static StateMapping Instance
        {
            get { return new StateMapping(); }
        }

        private  void Init()
        {
            IStateBasesList = new List<IStateBase>
                                  {
                                      new CommonState(),
                                      new ManageState(),
                                      new DoneState(),
                                      new RefuseState()

                                  };
        }
    }
}
