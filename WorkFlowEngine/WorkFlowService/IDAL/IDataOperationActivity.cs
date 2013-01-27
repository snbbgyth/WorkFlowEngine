/********************************************************************************
** Class Name:   IDataOperationActivity 
** Author：      spring yang
** Create date： 2012-9-1
** Modify：      spring yang
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
