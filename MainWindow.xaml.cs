using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Data.SqlClient;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Dapper;
using System.Net.WebSockets;

namespace DapperHW
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static string connStr = ConfigurationManager.ConnectionStrings["PersonDb"].ConnectionString;
        public MainWindow()
        {
            InitializeComponent();
            //InsertIntoPersonsList();
        }

        public static void InsertIntoPersonsList()
        {
            using (IDbConnection conn = new SqlConnection(connStr)) 
            { 
                List<Person> persons = new List<Person>()
                {
                    new Person(){ Name = "Andrey", Age = 24, City = "Kyiv" },
                    new Person(){ Name = "Liza", Age = 18, City = "Kryvyi Rih" },
                    new Person(){ Name = "Oleg", Age = 15, City = "London" },
                    new Person(){ Name = "Sergey", Age = 55, City = "Kyiv" },
                    new Person(){ Name = "Sergey", Age = 32, City = "Kyiv" }
                };
                string insertQuery = @"INSERT INTO Person(Name, Age, City) VALUES (@Name, @Age, @City)";
                conn.Execute(insertQuery, persons.ToList());
            } 
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            string query = "SELECT * FROM Person WHERE Age>25";
            using (IDbConnection conn = new SqlConnection(connStr))
            {
                var persons = conn.Query<Person>(query).ToList();
                Results.Text=string.Join(Environment.NewLine, persons.Select(p => $"Name: {p.Name}, Age: {p.Age}, City: {p.City}"));
            }
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            string query = "SELECT * FROM Person WHERE City!='Kyiv'";
            using (IDbConnection conn = new SqlConnection(connStr))
            {
                var persons = conn.Query<Person>(query).ToList();
                Results.Text = string.Join(Environment.NewLine, persons.Select(p => $"Name: {p.Name}, Age: {p.Age}, City: {p.City}"));
            }
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            string query = "SELECT Name FROM Person WHERE City='Kyiv'";
            using (IDbConnection conn = new SqlConnection(connStr))
            {
                var persons = conn.Query<Person>(query).ToList();
                Results.Text = string.Join(Environment.NewLine, persons.Select(p => $"Name: {p.Name}"));
            }
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            string query = "SELECT * FROM Person WHERE Name='Sergey' AND Age>35";
            using (IDbConnection conn = new SqlConnection(connStr))
            {
                var persons = conn.Query<Person>(query).ToList();
                Results.Text = string.Join(Environment.NewLine, persons.Select(p => $"Name: {p.Name}, Age: {p.Age}, City: {p.City}"));
            }
        }

        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            string query = "SELECT * FROM Person WHERE City='Kryvyi Rih'";
            using (IDbConnection conn = new SqlConnection(connStr))
            {
                var persons = conn.Query<Person>(query).ToList();
                Results.Text = string.Join(Environment.NewLine, persons.Select(p => $"Name: {p.Name}, Age: {p.Age}, City: {p.City}"));
            }
        }

        private void Button6_Click(object sender, RoutedEventArgs e)
        {
            Task2 task2 = new Task2();
            task2.Show();
        }
    }
}
