using ItalianPizza.DatabaseModel.DatabaseMapping;
using System.Collections.Generic;
using System.Linq;

namespace ItalianPizza.DatabaseModel.DataAccessObject
{
    public class FinancialTransactionIncomeContextDAO
    {

        public List<FinancialTransactionIncomeContextSet> GetAllFinancialTransactionIncomeContexts()
        {
            List<FinancialTransactionIncomeContextSet> financialTransactionIncomeContexts = new List<FinancialTransactionIncomeContextSet>();

            using (var context = new ItalianPizzaServerBDEntities())
            {
                financialTransactionIncomeContexts = context.FinancialTransactionIncomeContextSet.ToList();
            }

            return financialTransactionIncomeContexts;
        }


        public FinancialTransactionIncomeContextSet GetFinancialTransactionIncomeContextById(int financialTransactionIncomeContextId)
        {
            FinancialTransactionIncomeContextSet financialTransactionIncomeContext = new FinancialTransactionIncomeContextSet();

            using (var context = new ItalianPizzaServerBDEntities())
            {
                financialTransactionIncomeContext = context.FinancialTransactionIncomeContextSet.Where(ftic => ftic.Id == financialTransactionIncomeContextId).FirstOrDefault();
            }

            return financialTransactionIncomeContext;
        }

        public FinancialTransactionIncomeContextSet GetFinancialTransactionIncomeContextByName(string financialTransactionIncomeContextName)
        {
            FinancialTransactionIncomeContextSet financialTransactionIncomeContext = new FinancialTransactionIncomeContextSet();

            using (var context = new ItalianPizzaServerBDEntities())
            {
                financialTransactionIncomeContext = context.FinancialTransactionIncomeContextSet.Where(ftic => ftic.Context == financialTransactionIncomeContextName).FirstOrDefault();
            }

            return financialTransactionIncomeContext;
        }
    }
}
