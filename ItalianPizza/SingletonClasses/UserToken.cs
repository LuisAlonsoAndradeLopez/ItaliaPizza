using ItalianPizza.DatabaseModel.DatabaseMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItalianPizza.SingletonClasses
{
    public class UserToken
    {
        private static EmployeeSet instance;

        private static readonly object lockObject = new object();

        private UserToken() { }

        public static EmployeeSet GetInstance()
        {
            if (instance == null)
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new EmployeeSet();
                    }
                }
            }
            return instance;
        }
    }
}
