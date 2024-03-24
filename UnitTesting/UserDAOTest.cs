using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItalianPizza.DatabaseModel.DataAccessObject;
using ItalianPizza.DatabaseModel.DatabaseMapping;

namespace UnitTesting
{

    [TestClass]
    public class UserDAOTest
    {
        private static UserDAO userDAO;
        private static Empleado employeeToRegist;
        private static Empleado employeeAlreadyRegistered;
        private static Empleado employeeToGet;
        private static Cuenta accountToRegist;
        private static Cuenta accountAlreadyRegistered;
        private static Cuenta accountToGet;
        private static int validValue = 1;
        private static int invalidValue = -1;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            userDAO = new UserDAO();
            employeeToRegist = new Empleado()
            {
                Nombre = "Test",
                Apellido = "Test",
                Direccion = "Test",
                Telefono = "Test",
                Correo = "Test",
                FechaNacimiento = "Test",
                FechaContratacion = "Test",
                Puesto = "Test",
                Salario = "Test",
                Activo = "Test"
            };
            employeeAlreadyRegistered = new Empleado()
            {
                Nombre = "Test",
                Apellido = "Test",
                Direccion = "Test",
                Telefono = "Test",
                Correo = "Test",
                FechaNacimiento = "Test",
                FechaContratacion = "Test",
                Puesto = "Test",
                Salario = "Test",
                Activo = "Test"
            };
            employeeToGet = new Empleado()
            {
                Nombre = "Test",
                Apellido = "Test",
                Direccion = "Test",
                Telefono = "Test",
                Correo = "Test",
                FechaNacimiento = "Test",
                FechaContratacion = "Test",
                Puesto = "Test",
                Salario = "Test",
                Activo = "Test"
            };
            accountToRegist = new Cuenta()
            {
                Usuario = "Test",
                Contrasena = "Test",
                Activo = "Test"
            };
            accountAlreadyRegistered = new Cuenta()
            {
                Usuario = "Test",
                Contrasena = "Test",
                Activo = "Test"
            };
            accountToGet = new Cuenta()
            {
                Usuario = "Test",
                Contrasena = "Test",
                Activo = "Test"
            };
        }

        [TestCleanup]
        public void TestCleanup()
        {
            using(var scope = new TransactionScope())
            {
                scope.Complete();
            }
        }
    }
}
