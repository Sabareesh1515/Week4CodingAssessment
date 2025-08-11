using Microsoft.Data.SqlClient;

namespace ADONetDemo
{
    internal class Program
    {
        static int Menu()
        {
            Console.WriteLine("\n--- Employee Management ---");
            Console.WriteLine("1. View All Employees");
            Console.WriteLine("2. Add Employee");
            Console.WriteLine("3. Delete Employee");
            Console.WriteLine("4. Edit Employee Name");
            Console.WriteLine("Enter your choice:");
            int ch = int.Parse(Console.ReadLine());
            return ch;
        }

        static void Main(string[] args)
        {
            string choice = "y";
            while (choice.ToLower() == "y")
            {
                int c = Menu();
                switch (c)
                {
                    case 1: ViewEmployees(); break;
                    case 2: AddEmployee(); break;
                    case 3: DeleteEmployee(); break;
                    case 4: EditEmployee(); break;
                    default: Console.WriteLine("Invalid choice"); break;
                }
                Console.WriteLine("Repeat? (y/n): ");
                choice = Console.ReadLine();
            }
        }

        static string connectionString = @"server=SABAREESH_S;database=demo;integrated security=true;TrustServerCertificate=true";

        static void ViewEmployees()
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM Employee";
            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                Console.WriteLine("ID\tName\tManager\tType");
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["EmployeeID"]}\t{reader["Name"]}\t{reader["ReportingManager"]}\t{reader["EmployeeType"]}");
                }
            }
            else
            {
                Console.WriteLine("No records found.");
            }
            reader.Close();
        }

        static void AddEmployee()
        {
            Console.Write("Enter name: ");
            string name = Console.ReadLine();
            Console.Write("Enter reporting manager: ");
            string manager = Console.ReadLine();
            Console.Write("Enter employee type (Contract/Payroll): ");
            string type = Console.ReadLine().ToLower();

            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand insertEmp = new SqlCommand("INSERT INTO Employee (Name, ReportingManager, EmployeeType) OUTPUT INSERTED.EmployeeID VALUES (@name, @manager, @type)", connection);
            insertEmp.Parameters.AddWithValue("@name", name);
            insertEmp.Parameters.AddWithValue("@manager", manager);
            insertEmp.Parameters.AddWithValue("@type", type);
            int empId = (int)insertEmp.ExecuteScalar();

            if (type == "contract")
            {
                Console.Write("Enter contract date (yyyy-mm-dd): ");
                DateTime contractDate = DateTime.Parse(Console.ReadLine());
                Console.Write("Enter duration in months: ");
                int duration = int.Parse(Console.ReadLine());
                Console.Write("Enter charges: ");
                decimal charges = decimal.Parse(Console.ReadLine());

                SqlCommand insertContract = new SqlCommand("INSERT INTO ContractEmployee (EmployeeID, ContractDate, DurationInMonths, Charges) VALUES (@id, @date, @duration, @charges)", connection);
                insertContract.Parameters.AddWithValue("@id", empId);
                insertContract.Parameters.AddWithValue("@date", contractDate);
                insertContract.Parameters.AddWithValue("@duration", duration);
                insertContract.Parameters.AddWithValue("@charges", charges);
                insertContract.ExecuteNonQuery();
            }
            else if (type == "payroll")
            {
                Console.Write("Enter joining date (yyyy-mm-dd): ");
                DateTime joiningDate = DateTime.Parse(Console.ReadLine());
                Console.Write("Enter experience in years: ");
                int exp = int.Parse(Console.ReadLine());
                Console.Write("Enter basic salary: ");
                decimal basic = decimal.Parse(Console.ReadLine());

                decimal da = 0, hra = 0, pf = 0;
                if (exp > 10)
                {
                    da = basic * 0.10M; hra = basic * 0.085M; pf = 6200;
                }
                else if (exp > 7)
                {
                    da = basic * 0.07M; hra = basic * 0.065M; pf = 4100;
                }
                else if (exp > 5)
                {
                    da = basic * 0.041M; hra = basic * 0.038M; pf = 1800;
                }
                else
                {
                    da = basic * 0.019M; hra = basic * 0.02M; pf = 1200;
                }

                decimal net = basic + da + hra - pf;

                SqlCommand insertPayroll = new SqlCommand(@"INSERT INTO PayrollEmployee (EmployeeID, JoiningDate, ExperienceYears, BasicSalary, DA, HRA, PF, NetSalary) 
                                                            VALUES (@id, @joinDate, @exp, @basic, @da, @hra, @pf, @net)", connection);
                insertPayroll.Parameters.AddWithValue("@id", empId);
                insertPayroll.Parameters.AddWithValue("@joinDate", joiningDate);
                insertPayroll.Parameters.AddWithValue("@exp", exp);
                insertPayroll.Parameters.AddWithValue("@basic", basic);
                insertPayroll.Parameters.AddWithValue("@da", da);
                insertPayroll.Parameters.AddWithValue("@hra", hra);
                insertPayroll.Parameters.AddWithValue("@pf", pf);
                insertPayroll.Parameters.AddWithValue("@net", net);
                insertPayroll.ExecuteNonQuery();
            }
            Console.WriteLine("Employee added successfully.");
        }

        static void DeleteEmployee()
        {
            Console.Write("Enter employee ID to delete: ");
            int id = int.Parse(Console.ReadLine());

            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();


            SqlCommand deleteContract = new SqlCommand("DELETE FROM ContractEmployee WHERE EmployeeID=@id", connection);
            deleteContract.Parameters.AddWithValue("@id", id);
            deleteContract.ExecuteNonQuery();

            SqlCommand deletePayroll = new SqlCommand("DELETE FROM PayrollEmployee WHERE EmployeeID=@id", connection);
            deletePayroll.Parameters.AddWithValue("@id", id);
            deletePayroll.ExecuteNonQuery();


            SqlCommand deleteEmp = new SqlCommand("DELETE FROM Employee WHERE EmployeeID=@id", connection);
            deleteEmp.Parameters.AddWithValue("@id", id);
            int rows = deleteEmp.ExecuteNonQuery();

            if (rows > 0)
                Console.WriteLine("Record deleted.");
            else
                Console.WriteLine("Employee not found.");
        }

        static void EditEmployee()
        {
            Console.Write("Enter employee ID to edit: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Enter new name: ");
            string name = Console.ReadLine();

            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand update = new SqlCommand("UPDATE Employee SET Name=@name WHERE EmployeeID=@id", connection);
            update.Parameters.AddWithValue("@name", name);
            update.Parameters.AddWithValue("@id", id);
            int rows = update.ExecuteNonQuery();

            if (rows > 0)
                Console.WriteLine("Record updated.");
            else
                Console.WriteLine("Employee not found.");
        }
    }
}

