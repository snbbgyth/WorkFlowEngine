/********************************************************************************
** Class Name:   InvokeContextModel 
** Author：      spring yang
** Create date： 2013-1-1
** Modify：      spring yang
** Modify Date： 2013-2-17
** Summary：     InvokeContextModel class
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkFlowHandle.Model
{
    public class InvokeContextModel
    {
        public string Name { get; set; }

        public string PartnerLink { get; set; }

        public string PortType { get; set; }

        public string Operation { get; set; }

        public string InputVariable { get; set; }

    }
}
