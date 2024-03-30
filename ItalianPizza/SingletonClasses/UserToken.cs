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
        private static Employee instance;

        private static readonly object lockObject = new object();

        private UserToken() { }

        public static Employee GetInstance()
        {
            if (instance == null)
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new Employee();
                    }
                }
            }
            return instance;
        }
    }
}
