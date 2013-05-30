using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkFlowService.Model;

namespace WorkFlowService.IDAL
{
    public interface IRelationDAL:IDataOperationActivity<RelationModel>
    {
        RelationModel QueryByChildNodeIDAndParentNodeIDAndType(string childNodeID, string parentNodeID, int type);
        IList<RelationModel> QueryByChildNodeIDAndType(string childNodeID, int type);
        IList<RelationModel> QueryByParentNodeIDAndType(string parentNodeID, int type);
        int DeleteByChildNodeIDAndType(string childNodeID, int type);
        int DeleteByParentNodeIDAndType(string parentNodeID, int type);
        int DeleteByChildNodeIDAndParentNodeIDAndType(string childNodeID, string parentNodeID, int type);
    }
}
