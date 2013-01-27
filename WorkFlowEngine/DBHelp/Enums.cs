/********************************************************************************
** Class Name:   Enums
** Author：      spring yang
** Create date： 2012-9-1
** Modify：      spring yang
** Modify Date： 2012-9-25
** Summary：     Enums class
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBHelp
{
    [Serializable]
    public enum SqlSourceType
    {
        Oracle,
        MSSql,
        MySql,
        SQLite
    }
}
