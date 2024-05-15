using ItalianPizza.DatabaseModel.DatabaseMapping;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;

namespace ItalianPizza.DatabaseModel.DataAccessObject
{
    public class WithDrawFinancialTransactionDAO
    {
        public int AddWithDrawFinancialTransaction(WithDrawFinancialTransactionSet withDrawFinancialTransaction)
        {
            int result = 0;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    context.WithDrawFinancialTransactionSet.Add(withDrawFinancialTransaction);
                    context.SaveChanges();
                }

                result = 1;
            }
            catch (EntityException ex)
            {
                throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("Operación no válida al acceder a la base de datos.", ex);
            }

            return result;
        }

        public List<WithDrawFinancialTransactionSet> GetWithDrawFinancialTransactions()
        {
            List<WithDrawFinancialTransactionSet> withDrawFinancialTransactions = new List<WithDrawFinancialTransactionSet>();

            using (var context = new ItalianPizzaServerBDEntities())
            {
                withDrawFinancialTransactions = context.WithDrawFinancialTransactionSet.ToList();
            }

            return withDrawFinancialTransactions;
        }
    }
}
