using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFlowService.IDAL
{
   public  interface IDataOperationDAL
   {
       void InitDataBase();
       int Insert<T>(T entity);
       int Modify<T>(T entity);
       int Remove<T>(string id);
       T QueryByID<T>(string id);
       IList<T> QueryAll<T>();
   }
}
