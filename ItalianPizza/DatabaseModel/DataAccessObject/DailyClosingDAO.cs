using ItalianPizza.DatabaseModel.DatabaseMapping;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Windows.Documents;

namespace ItalianPizza.DatabaseModel.DataAccessObject
{
    public class DailyClosingDAO
    {
        public int GetMaximumDailyClosingID()
        {
            int maximumDailyClosingID = 0;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    maximumDailyClosingID = context.DailyClosingSet.Max(dailyClosing => (int?)dailyClosing.Id) ?? 0;
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

            return maximumDailyClosingID;
        }

        public List<IncomeFinancialTransactionSet> GetDailyIncomes()
        {
            List<IncomeFinancialTransactionSet> dailyIncomes = new List<IncomeFinancialTransactionSet>();
            int currentDailyClosingID = GetMaximumDailyClosingID();

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    dailyIncomes = context.IncomeFinancialTransactionSet
                        .Where(income => income.DailyClosingId == currentDailyClosingID)
                        .ToList();
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

            return dailyIncomes;
        }

        public List<WithDrawFinancialTransactionSet> GetDailyWithdrawals()
        {
            List<WithDrawFinancialTransactionSet> dailyWithdrawals = new List<WithDrawFinancialTransactionSet>();
            int currentDailyClosingID = GetMaximumDailyClosingID();

            try
            {

                using (var context = new ItalianPizzaServerBDEntities())
                {
                    dailyWithdrawals = context.WithDrawFinancialTransactionSet
                        .Where(withdrawal => withdrawal.DailyClosingId == currentDailyClosingID)
                        .ToList();
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

            return dailyWithdrawals;
        }


        public int RegisterDailyClosing(DailyClosingSet dailyClosing)
        {
            int dailyClosingID = 0;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    context.DailyClosingSet.Add(dailyClosing);
                    context.SaveChanges();
                    dailyClosingID = dailyClosing.Id;
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

            return dailyClosingID;
        }

        public DailyClosingSet GetCurrentDailyClosing()
        {
            DailyClosingSet currentDailyClosing = new DailyClosingSet();

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                     currentDailyClosing = context.DailyClosingSet
                                 .OrderByDescending(dailyClosing => dailyClosing.Id)
                                 .FirstOrDefault();

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

            return currentDailyClosing;
        }

    }
}
