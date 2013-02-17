/********************************************************************************
** Class Name:   IDataOperationActivity 
** Author：      Spring Yang
** Create date： 2012-9-1
** Modify：      Spring Yang
** Modify Date： 2012-9-25
** Summary：     IDataOperationActivity class
*********************************************************************************/

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
