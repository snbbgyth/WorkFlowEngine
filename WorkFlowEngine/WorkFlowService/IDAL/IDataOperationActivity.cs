using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary.Model;

namespace WorkFlowService.IDAL
{
   public interface IDataOperationActivity<T>:ICreateDataTableActivity
   {
       int Insert(T entity);
       int Modify(T entity);
       int DeleteByID(string id);
       List<T> QueryAll();
       T QueryByID(string id);

   }
}
