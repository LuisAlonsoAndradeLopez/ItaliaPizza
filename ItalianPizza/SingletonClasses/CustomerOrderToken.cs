using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItalianPizza.SingletonClasses
{
    public class CustomerOrderToken
    {
        public static bool InProcess = false;
        public static bool TransactionInProcess()
        {
            return InProcess;
        }

        public static void ActivateTransaction()
        {
            InProcess = true;
        }

        public static void DeactivateTransaction()
        {
            InProcess = false;
        }
    }
}
