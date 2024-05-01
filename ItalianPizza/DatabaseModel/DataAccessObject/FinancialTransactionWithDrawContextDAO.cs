using ItalianPizza.DatabaseModel.DatabaseMapping;
using System.Collections.Generic;
using System.Linq;

namespace ItalianPizza.DatabaseModel.DataAccessObject
{
    public class FinancialTransactionWithDrawContextDAO
    {
        
        public List<FinancialTransactionWithDrawContextSet> GetAllFinancialTransactionWithDrawContexts()
        {
            List<FinancialTransactionWithDrawContextSet> financialTransactionWithDrawContexts = new List<FinancialTransactionWithDrawContextSet>();
        
            using (var context = new ItalianPizzaServerBDEntities())
            {
                financialTransactionWithDrawContexts = context.FinancialTransactionWithDrawContextSet.ToList();
            }
        
            return financialTransactionWithDrawContexts;
        }


        public FinancialTransactionWithDrawContextSet GetFinancialTransactionWithDrawContextById(int financialTransactionWithDrawContextId)
        {
            FinancialTransactionWithDrawContextSet financialTransactionWithDrawContext = new FinancialTransactionWithDrawContextSet();

            using (var context = new ItalianPizzaServerBDEntities())
            {
                financialTransactionWithDrawContext = context.FinancialTransactionWithDrawContextSet.Where(ftwc => ftwc.Id == financialTransactionWithDrawContextId).FirstOrDefault();
            }

            return financialTransactionWithDrawContext;
        }

        public FinancialTransactionWithDrawContextSet GetFinancialTransactionWithDrawContextByName(string financialTransactionWithDrawContextName)
        {
            FinancialTransactionWithDrawContextSet financialTransactionWithDrawContext = new FinancialTransactionWithDrawContextSet();

            using (var context = new ItalianPizzaServerBDEntities())
            {
                financialTransactionWithDrawContext = context.FinancialTransactionWithDrawContextSet.Where(ftwc => ftwc.Context == financialTransactionWithDrawContextName).FirstOrDefault();
            }

            return financialTransactionWithDrawContext;
        }
    }
}
