using ItalianPizza.DatabaseModel.DatabaseMapping;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;

namespace ItalianPizza.DatabaseModel.DataAccessObject
{
    public class IncomeFinancialTransactionDAO
    {
        public int AddIncomeFinancialTransaction(IncomeFinancialTransactionSet incomeFinancialTransaction)
        {
            int result = 0;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    context.IncomeFinancialTransactionSet.Add(incomeFinancialTransaction);
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

        public List<IncomeFinancialTransactionSet> GetIncomeFinancialTransactions()
        {
            List<IncomeFinancialTransactionSet> incomeFinancialTransactions = new List<IncomeFinancialTransactionSet>();

            using (var context = new ItalianPizzaServerBDEntities())
            {
                incomeFinancialTransactions = context.IncomeFinancialTransactionSet.ToList();
            }

            return incomeFinancialTransactions;
        }
    }
}
