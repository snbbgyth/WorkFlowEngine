using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLibrary.IDAL;

namespace WorkFlowService.IDAL
{
   public  interface IDataOperationDAL
   {

       int Insert<T>(T entity) where T : ITableModel;
       int Modify<T>(T entity);
       int Remove<T>(string id);
       T QueryByID<T>(string id);
       IList<T> QueryAll<T>();
   }
}
