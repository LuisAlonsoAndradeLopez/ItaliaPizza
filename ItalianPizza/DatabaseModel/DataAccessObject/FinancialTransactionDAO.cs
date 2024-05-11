using ItalianPizza.DatabaseModel.DatabaseMapping;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;

namespace ItalianPizza.DatabaseModel.DataAccessObject
{
    public class FinancialTransactionDAO
    {
        public int AddFinancialTransaction(FinancialTransactionSet financialTransaction)
        {
            int result = 0;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    context.FinancialTransactionSet.Add(financialTransaction);
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

        public List<FinancialTransactionSet> GetFinancialTransactions()
        {
            List<FinancialTransactionSet> financialTransactions = new List<FinancialTransactionSet>();

            using (var context = new ItalianPizzaServerBDEntities())
            {
                financialTransactions = context.FinancialTransactionSet.ToList();
            }

            return financialTransactions;
        }

        public List<FinancialTransactionSet> GetSpecifiedFinancialTransactionsByTransactionTypeAndRealizationDate(string transactionType, DateTime realizationDate)
        {
            List<FinancialTransactionSet> specifiedFinancialTransactions = new List<FinancialTransactionSet>();

            using (var context = new ItalianPizzaServerBDEntities())
            {
                specifiedFinancialTransactions = context.FinancialTransactionSet.Where(ft => ft.Type == transactionType)
                    .Where(ft => ft.FinancialTransactionDate == realizationDate).ToList();
            }

            return specifiedFinancialTransactions;
        }
    }
}
