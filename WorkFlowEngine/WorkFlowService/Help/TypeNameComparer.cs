using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkFlowService.Help
{
    public class TypeNameComparer : IComparer<Type>
    {
        public int Compare(Type t1, Type t2)
        {
            if (t1.Namespace.Length > t2.Namespace.Length)
            {
                return 1;
            }

            if (t1.Namespace.Length < t2.Namespace.Length)
            {
                return -1;
            }

            return String.Compare(t1.Namespace, t2.Namespace, StringComparison.Ordinal);
        }
    }
}
