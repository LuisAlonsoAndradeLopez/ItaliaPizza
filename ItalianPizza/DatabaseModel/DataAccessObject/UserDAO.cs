using ItalianPizza.DatabaseModel.DatabaseMapping;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ItalianPizza.DatabaseModel.DataAccessObject
{
    public class UserDAO
    {
        public UserDAO() { }

        public List<Empleado> GetAllEmployees()
        {
            List<Empleado> employees = new List<Empleado>();

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    employees = context.Empleado.ToList();
                }

            }
            catch (EntityException ex)
            {
                throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("Operación no válida al acceder a la base de datos.", ex);
            }

            return employees;
        }

        public int RegisterUser(Cuenta account, Empleado employee)
        {
            int result = 0;
            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    context.Cuenta.Add(account);
                    context.Empleado.Add(employee);
                    result = context.SaveChanges();
                }

            }
            catch (EntityException ex)
            {
                throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
            }
            return result;
        }

    }

        public class RandomNumberGenerator
        {
            private static readonly RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

            public static int GenerateRandomNumber(int min, int max)
            {
                byte[] randomNumber = new byte[4];
                rng.GetBytes(randomNumber);

                int generatedNumber = BitConverter.ToInt32(randomNumber, 0);

                return new Random(generatedNumber).Next(min, max);
            }
        }
    
}
