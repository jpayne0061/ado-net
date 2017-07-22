using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace console_with_db
{
    class Program
    {
        static void Main(string[] args)
        {
            int operation = 100;

            while (operation != 0)
            {
                Console.WriteLine("What operation would you like to perform? \r\n");
                Console.WriteLine("Add Person: 1 \r\n Read Person: 2 \r\n Update Person: 3 \r\n Delete Person: 4 ");

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
                }

            }


        }

        //ERRORS I ENCOUNTERED:
        //did not open connection
        //entered full path of database instead of just the name(login falied)
        //wrong table name


    }
}
