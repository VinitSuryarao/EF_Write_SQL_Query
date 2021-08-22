using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Database_First
{
    public class ClientData
    {
        public int n_ClientNo { get; set; }
        public string s_ClientCode { get; set; }
    }

    class ExecuteRawQuery
    {
        static void Main(string[] args)
        {
            MDSEntities db = new MDSEntities(); // Object of context class

            // ********* SQL Query For Entity Type **********
            // Simple Select Query
            IEnumerable<tb_Client> clientList = db.tb_Client.SqlQuery("select * from tb_Client");

            foreach (var item in clientList)
            {
                Console.WriteLine("{0}\t{1}\t{2}\n", item.n_ClientNo, item.s_ClientCode, item.s_ClientName);
            }

            // Select Query with Parameter
            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@Pn_ClientNo";
            p1.Value = 2;
            p1.SqlDbType = System.Data.SqlDbType.Int;

            SqlParameter p2 = new SqlParameter();
            p2.ParameterName = "@Ps_ClientCode";
            p2.Value = "CCN0502";
            p2.SqlDbType = System.Data.SqlDbType.NChar;
            p2.Size = 50;

            IEnumerable<tb_Client> paramClientList = db.tb_Client.SqlQuery("select * from tb_Client " +
                "where n_ClientNo=@Pn_ClientNo and s_ClientCode=@Ps_ClientCode", p1, p2);

            foreach (var item in paramClientList)
            {
                Console.WriteLine("{0}\t{1}\t{2}\n", item.n_ClientNo, item.s_ClientCode, item.s_ClientName);
            }

            //// ********* SQL Query For Non Entity Type **********

            var clientdata = db.Database.SqlQuery<ClientData>("select n_ClientNo, s_ClientCode from tb_Client");

            foreach (ClientData item in clientdata)
            {
                Console.WriteLine("{0}\t{1}\n", item.n_ClientNo, item.s_ClientCode);
            }

            //// ********* Non Query Commands **********

            int result = db.Database.ExecuteSqlCommand("UPDATE tb_Client SET s_ClientName='CCN0504 First CCN0504 Middle CCN0504Last' " +
                "WHERE s_ClientCode ='CCN0504'");

            if (result==1)
                Console.WriteLine("Updated Successfull");
            else
                Console.WriteLine("Updation Failed");


            Console.ReadLine();
        }
    }
}
