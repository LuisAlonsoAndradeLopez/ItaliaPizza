using ItalianPizza.DatabaseModel.DatabaseMapping;

namespace ItalianPizza.SingletonClasses
{
    public class UserToken
    {
        private static EmployeeSet employeeInstance;

        private static readonly object lockObject = new object();

        private UserToken() { }

        public static void GetInstance(EmployeeSet employee)
        {
            lock (lockObject)
            {
                employeeInstance = employee;
            }
        }

        public static int GetEmployeeID()
        {
            return employeeInstance.Id;
        }

        public static EmployeePositionSet GetEmployeePosition()
        {
            return employeeInstance.EmployeePositionSet;
        }
    }
}
