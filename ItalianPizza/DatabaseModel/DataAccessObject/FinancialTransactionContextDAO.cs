using ItalianPizza.DatabaseModel.DatabaseMapping;
using System.Collections.Generic;
using System.Linq;

namespace ItalianPizza.DatabaseModel.DataAccessObject
{
    public class FinancialTransactionContextDAO
    {
        
        public List<FinancialTransactionContextSet> GetAllFinancialTransactionContexts()
        {
            List<FinancialTransactionContextSet> financialTransactionContexts = new List<FinancialTransactionContextSet>();
        
            using (var context = new ItalianPizzaServerBDEntities())
            {
                financialTransactionContexts = context.FinancialTransactionContextSet.ToList();
            }
        
            return financialTransactionContexts;
        }


        public FinancialTransactionContextSet GetFinancialTransactionContextById(int financialTransactionContextId)
        {
            FinancialTransactionContextSet financialTransactionContext = new FinancialTransactionContextSet();

            using (var context = new ItalianPizzaServerBDEntities())
            {
                financialTransactionContext = context.FinancialTransactionContextSet.Where(ftc => ftc.Id == financialTransactionContextId).FirstOrDefault();
            }

            return financialTransactionContext;
        }

        public FinancialTransactionContextSet GetFinancialTransactionContextByName(string financialTransactionContextName)
        {
            FinancialTransactionContextSet financialTransactionContext = new FinancialTransactionContextSet();

            using (var context = new ItalianPizzaServerBDEntities())
            {
                financialTransactionContext = context.FinancialTransactionContextSet.Where(ftc => ftc.Context == financialTransactionContextName).FirstOrDefault();
            }

            return financialTransactionContext;
        }
    }
}
