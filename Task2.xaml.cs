using DapperHW;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Dapper;

namespace DapperHW
{
    /// <summary>
    /// Interaction logic for Task2.xaml
    /// </summary>
    /// 
    class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int DepId { get; set; }
    }
    class Department
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }
    public partial class Task2 : Window
    {
        static string connStr = ConfigurationManager.ConnectionStrings["PersonDb"].ConnectionString;
        public Task2()
        {
            InitializeComponent();

        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await InsertIntoDepartmentsList();
            await InsertIntoEmployeesList();
        }

        public static async Task InsertIntoDepartmentsList()
        {
            using (IDbConnection conn = new SqlConnection(connStr))
            {
                List<Department> departments = new List<Department>(){
                new Department(){ Id = 1, Country = "Ukraine", City = "Donetsk" },
                new Department(){ Id = 2, Country = "Ukraine", City = "Kyiv" },
                new Department(){ Id = 3, Country = "France", City = "Paris" },
                new Department(){ Id = 4, Country = "UK", City = "London" }
                };

                string insertQuery = @"INSERT INTO Department(Country, City) VALUES (@Country, @City)";
                await conn.ExecuteAsync(insertQuery, departments.ToList());
            }
        }

        public static async Task InsertIntoEmployeesList()
        {
            using (IDbConnection conn = new SqlConnection(connStr))
            {
                List<Employee> employees = new List<Employee>(){
                new Employee(){ Id = 1, FirstName = "Tamara", LastName = "Ivanova", Age = 22, DepId = 2 },
                new Employee(){ Id = 2, FirstName = "Nikita", LastName = "Larin", Age = 33, DepId = 1 },
                new Employee() { Id = 3, FirstName = "Alica", LastName = "Ivanova", Age = 43, DepId = 3 },
                new Employee() { Id = 4, FirstName = "Lida", LastName = "Marusyk", Age = 22, DepId = 2 },
                new Employee() { Id = 5, FirstName = "Lida", LastName = "Voron", Age = 36, DepId = 4 },
                new Employee() { Id = 6, FirstName = "Ivan", LastName = "Kalyta", Age = 22, DepId = 2 },
                new Employee() { Id = 7, FirstName = "Nikita", LastName = " Krotov ", Age = 27,DepId = 4 }
                };

                string insertQuery = @"INSERT INTO Employee(FirstName, LastName, Age, DepId) VALUES (@FirstName, @LastName, @Age, @DepId)";
                await conn.ExecuteAsync(insertQuery, employees.ToList());
            }
        }
        private void FirstTask_Expanded(object sender, RoutedEventArgs e)
        {
            string query = "SELECT FirstName, LastName FROM Employee JOIN Department ON Department.Id = Employee.DepId WHERE Department.Country = 'Ukraine' AND Department.City <> 'Donetsk'";
            using (IDbConnection conn = new SqlConnection(connStr))
            {
                var peoples = conn.Query<Employee>(query).ToList();
                Task1TextBlock.Text = string.Join(Environment.NewLine, peoples.Select(p => $"Name: {p.FirstName}, Surname: {p.LastName}"));
            }
        }

        private void SecondTask_Expanded(object sender, RoutedEventArgs e)
        {
            string query = "SELECT DISTINCT Country FROM Department";
            using (IDbConnection conn = new SqlConnection(connStr))
            {
                var countries = conn.Query<Department>(query).ToList();
                Task2TextBlock.Text = string.Join(Environment.NewLine, countries.Select(p => $"Country: {p.Country}"));
            }
        }

        private void ThirdTask_Expanded(object sender, RoutedEventArgs e)
        {
            string query = "SELECT TOP 3 FirstName, LastName FROM Employee WHERE Age>25";
            using (IDbConnection conn = new SqlConnection(connStr))
            {
                var peoples = conn.Query<Employee>(query).ToList();
                Task3TextBlock.Text = string.Join(Environment.NewLine, peoples.Select(p => $"Name: {p.FirstName}, Surname: {p.LastName}"));
            }
        }

        private void FourthTask_Expanded(object sender, RoutedEventArgs e)
        {
            string query = "SELECT FirstName, LastName, Age FROM Employee JOIN Department ON Department.Id = Employee.DepId WHERE Department.City = 'Kyiv' AND Employee.Age > 23";
            using (IDbConnection conn = new SqlConnection(connStr))
            {
                var peoples = conn.Query<Employee>(query).ToList();
                Task4TextBlock.Text = string.Join(Environment.NewLine, peoples.Select(p => $"Name: {p.FirstName}, Surname: {p.LastName}, Age={p.Age}"));
            }
        }
    }
}
//CREATE TABLE Department
//(
//	Id int NOT NULL PRIMARY KEY IDENTITY(1,1),
//	Country nvarchar(25) NOT NULL,
//    City nvarchar(30) NOT NULL
//);
//GO

//CREATE TABLE [dbo].[Employee]
//(
//    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
//    FirstName nvarchar(20) NOT NULL,
//    LastName nvarchar(20) NOT NULL,
//    Age int NOT NULL CHECK(Age > 18 AND Age <= 65),
//    DepId INT NOT NULL,
//    FOREIGN KEY(DepId) REFERENCES Department(id)
//);