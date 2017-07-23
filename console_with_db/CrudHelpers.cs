using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace console_with_db
{
    public static class CrudHelpers
    {
        public delegate void CrudOperation(SqlConnection conn);


        public static void SqlHelper(CrudOperation operation)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Server=JESS_COMPUTER\\SQLEXPRESS;Database=firstDB;Trusted_Connection=true";
                conn.Open();

                operation(conn);
            }
        }

        public static void CreateTask(SqlConnection conn)
        {
            Console.WriteLine("Enter id of Task Assigner: ");
            string AssignerId = Console.ReadLine();

            Console.WriteLine("Enter id of person to complete task: ");
            string TaskerId = Console.ReadLine();

            Console.WriteLine("Please write description");
            string description = Console.ReadLine();

            DateTime dt = DateTime.Now;

            SqlCommand insertCommand = new SqlCommand("INSERT INTO dbo.Tasks (Description, Created, FK_Person_AssignedTO, FK_Person_Assigner) VALUES (@0, @1, @2, @3)", conn);

            insertCommand.Parameters.Add(new SqlParameter("0", description));
            insertCommand.Parameters.Add(new SqlParameter("1", dt));
            insertCommand.Parameters.Add(new SqlParameter("2", TaskerId));
            insertCommand.Parameters.Add(new SqlParameter("3", AssignerId));

            insertCommand.ExecuteNonQuery();

        }

        public static void SendMessage(SqlConnection conn)
        {
            Console.WriteLine("Enter id of sender: ");
            string senderId = Console.ReadLine();

            Console.WriteLine("Enter id of recipient: ");
            string receiverId = Console.ReadLine();

            Console.WriteLine("Please enter message");
            string body = Console.ReadLine();

            SqlCommand insertCommand = new SqlCommand("INSERT INTO dbo.Messages (Body, Sender_Id, Receiver_Id) VALUES (@0, @1, @2)", conn);

            insertCommand.Parameters.Add(new SqlParameter("0", body));
            insertCommand.Parameters.Add(new SqlParameter("1", senderId));
            insertCommand.Parameters.Add(new SqlParameter("2", receiverId));

            insertCommand.ExecuteNonQuery();


        }

        public static void InsertPerson(SqlConnection conn)
        {
            Person person = AddEditPerson();

            SqlCommand insertCommand = new SqlCommand("INSERT INTO dbo.Person (Name, Age, Salaried, BirthDate, Savings) VALUES (@0, @1, @2, @3, @4)", conn);


            insertCommand.Parameters.Add(new SqlParameter("0", person.Name));
            insertCommand.Parameters.Add(new SqlParameter("1", person.Age));
            insertCommand.Parameters.Add(new SqlParameter("2", person.Salaried));
            insertCommand.Parameters.Add(new SqlParameter("3", person.BirthDate));
            insertCommand.Parameters.Add(new SqlParameter("4", person.Savings));

            try
            {
                insertCommand.ExecuteNonQuery();
            }
            catch (System.Data.SqlTypes.SqlTypeException ex)
            {
                Console.WriteLine(ex.Message + " \r\n Please try again");
                InsertPerson(conn);
            }

        }


        public static Person AddEditPerson()
        {
            Person person = new Person();
            try
            {
                Console.WriteLine("Please enter Name");
                person.Name = Console.ReadLine();

                Console.WriteLine("Please enter Age");
                person.Age = Int32.Parse(Console.ReadLine());

                Console.WriteLine("Please enter if Salaried(0 or 1)");
                person.Salaried = Int32.Parse(Console.ReadLine());

                Console.WriteLine("Please enter BirthDate");
                person.BirthDate = DateTime.Parse(Console.ReadLine());

                Console.WriteLine("Please enter savings");
                person.Savings = double.Parse(Console.ReadLine());

            }
            catch(FormatException ) {
                Console.WriteLine("You entered data that was incongruent with expected data type. Try again");
                return AddEditPerson();
            }

            return person;
        }


        public static void DeletePerson(SqlConnection conn)
        {
            Console.WriteLine("Enter id of person you wish to destroy");
            int id = Int32.Parse(Console.ReadLine());

            SqlCommand command = new SqlCommand("DELETE FROM dbo.Person WHERE id = @id", conn);
            command.Parameters.Add(new SqlParameter("id", id));
            command.ExecuteNonQuery();
        }

        public static void ReadPerson(SqlConnection conn)
        {
            Console.WriteLine("Enter id of person you wish to retrieve");
            int id = Int32.Parse(Console.ReadLine());

            SqlCommand command = new SqlCommand("SELECT * FROM dbo.Person WHERE id = @id", conn);

            command.Parameters.Add(new SqlParameter("id", id));

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {

                    Console.WriteLine(String.Format("Id \t | Name \t | Age \t | Salaried \t BirthDate \t | Savings "));
                    Console.WriteLine(String.Format("{0} \t | {1} \t | {2} \t | {3} \t {4} \t | {5} ",
                        reader[0], reader[1], reader[2], reader[3], reader[4], reader[5]));

                    Console.ReadLine();
                }
            }

        }

        public static void ListTasks(SqlConnection conn)
        {
            Console.WriteLine("Enter id of person you for whom you wish to list tasks: ");
            int id = Int32.Parse(Console.ReadLine());
            SqlCommand command = new SqlCommand("SELECT * FROM dbo.Tasks WHERE FK_Person_AssignedTO = @id", conn);
            command.Parameters.Add(new SqlParameter("id", id));
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {

                    Console.WriteLine(String.Format("Description \t | Created \t | Assigned To \t | Assigned By"));
                    Console.WriteLine(String.Format("{0} \t | {1} \t | {2} \t | {3}",
                        reader[0], reader[1], reader[2], reader[3]));

                    Console.ReadLine();
                }
            }

        }

        public static void ListMessages(SqlConnection conn)
        {
            Console.WriteLine("Enter id of person you for whom you wish to list messages: ");
            string id = Console.ReadLine();

            string name = GetNameById(id);

            SqlCommand command = new SqlCommand("SELECT * FROM dbo.Messages WHERE Sender_Id = @id", conn);
            command.Parameters.Add(new SqlParameter("id", id));

            using (SqlDataReader reader = command.ExecuteReader())
            {
                var mystery = reader.Read();

                while (reader.Read())
                {

                    Console.WriteLine(String.Format("Body \t | Sender \t | Receiver \t "));
                    Console.WriteLine(String.Format("{0} \t | {1} \t | {2} \t ",
                        reader[1], name, GetNameById(reader[3].ToString())));

                    
                }
            }

            Console.ReadLine();
        }

        public static void UpdatePerson(SqlConnection conn)
        {
            Console.WriteLine("Enter id of person you wish to update");
            int id = Int32.Parse(Console.ReadLine());

            Person person = AddEditPerson();
                //SqlCommand updateCommand = new SqlCommand("UPDATE dbo.Person SET @column = @value WHERE id = @id", conn);
            SqlCommand updateCommand = new SqlCommand("UPDATE dbo.Person SET Name = @name, Age = @age, Salaried = @salaried, BirthDate = @birthdate, Savings = @savings WHERE Id = @id", conn);
            updateCommand.Parameters.Add(new SqlParameter("name", person.Name));
            updateCommand.Parameters.Add(new SqlParameter("age", person.Age));
            updateCommand.Parameters.Add(new SqlParameter("salaried", person.Salaried));
            updateCommand.Parameters.Add(new SqlParameter("birthdate", person.BirthDate));
            updateCommand.Parameters.Add(new SqlParameter("savings", person.Savings));
            updateCommand.Parameters.Add(new SqlParameter("id", id));

            var commandResult = updateCommand.ExecuteScalar();

        }


        public static string GetNameById(string id)
        {
            string name = "";

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Server=JESS_COMPUTER\\SQLEXPRESS;Database=firstDB;Trusted_Connection=true";
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM dbo.Person WHERE Id = @id", conn);
                command.Parameters.Add(new SqlParameter("id", id));

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    reader.Read();
                    name = reader[1].ToString();
                }


            }

            return name;
        }

    }
}
