using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace WorkflowSetting.Help
{
    public class ItemModel
    {
        public ItemModel()
        {
 
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public ICommand EditCommand { get; set; }
    }
}
