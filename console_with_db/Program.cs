using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace console_with_db
{
    //write python script to scrape page and store each row
    //store each row as json object
    //use streamreader to store objects into sql server
    




    class Program
    {
        static void Main(string[] args)
        {
            int operation = 100;

            while (operation != 0)
            {
                Console.WriteLine("What operation would you like to perform? \r\n");
                Console.WriteLine("Add Person: 1 \r\n Read Person: 2 \r\n Update Person: 3 \r\n Delete Person: 4 \r\n Assign Task: 5 \r\n List Tasks: 6 \r\n Send Message: 7 \r\n List Messages: 8");

                operation = Int32.Parse(Console.ReadLine());

                switch (operation)
                {
                    case 1:
                        CrudHelpers.SqlHelper(CrudHelpers.InsertPerson);
                        break;
                    case 2:
                        CrudHelpers.SqlHelper(CrudHelpers.ReadPerson);
                        break;
                    case 3:
                        CrudHelpers.SqlHelper(CrudHelpers.UpdatePerson);
                        break;
                    case 4:
                        CrudHelpers.SqlHelper(CrudHelpers.DeletePerson);
                        break;
                    case 5:
                        CrudHelpers.SqlHelper(CrudHelpers.CreateTask);
                        break;
                    case 6:
                        CrudHelpers.SqlHelper(CrudHelpers.ListTasks);
                        break;
                    case 7:
                        CrudHelpers.SqlHelper(CrudHelpers.SendMessage);
                        break;
                    case 8:
                        CrudHelpers.SqlHelper(CrudHelpers.ListMessages);
                        break;
                    case 9:
                        CrudHelpers.SqlHelper(CrudHelpers.ListSavingsOverN);
                        break;
                    case 10:
                        CrudHelpers.SqlHelper(CrudHelpers.AssignPersonToOffender);
                        break;
                    case 11:
                        CrudHelpers.SqlHelper(CrudHelpers.AssignPersonToOffender);
                        break;
                    case 12:
                        CrudHelpers.SqlHelper(CrudHelpers.ListAssociatedOffenders);
                        break;
                }

            }


        }

        //ERRORS I ENCOUNTERED:
        //did not open connection
        //entered full path of database instead of just the name(login falied)
        //wrong table name


    }
}
