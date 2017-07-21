using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Data;
//using ePayment;

namespace iPrint
{
    class MySQLConnection
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        //Constructor
        public MySQLConnection()
        {
            Initialize();
        }
        //Initialize values
        private void Initialize()
        {
            server = "localhost";
            database = "iPrint3";//C:\\ProgramData\\MySQL\\MySQL Server 5.7\\data\\order\\
            uid = "root";//root
            password = "Nosheenbl1$";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            /*server = "iprintltd.com";//"195.154.134.187"; //
            database = "iprintlt_iPrint";
            uid = "iprintlt_root";
            password = "Nosheenbl1$";
            string connectionString;
            connectionString = "SERVER=" + server + "; PORT = 3306 ;" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            //mycon = new MySqlConnection(connectionString);*/

            connection = new MySqlConnection(connectionString);
        }

        //open connection to database
        private bool OpenConnection()
        {
            try
            {                
                connection.Open();
               // MessageBox.Show("Connection success");
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                MessageBox.Show("Cannot connect to server.  Contact administrator: Unknown Error");

                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public bool RemoteConnect()
        {
            if (this.OpenConnection())
                return true;

            else
                return false;

        }

        public void RemoteDiconnect()
        {
            this.OpenConnection();
        }
        //Insert statement
        public void SaveTransactionStatus(String Transaction_TransactionTrackingId, String Transaction_MerchantReference, String Transaction_CustumerFName, String Transaction_Customer_LName, String Transaction_Tel, String Transaction_Status)
        {

            DateTime theDate = DateTime.Now;
            String date = theDate.ToString("yyyy-MM-dd");
            String time = theDate.ToString("H:mm:ss");
            String comma = "','";

            String query1 = "INSERT INTO Transaction ( Transaction_Date, Transaction_Time, Transaction_TransactionTrackingId, Transaction_MerchantReference, Transaction_CustumerFName, Transaction_Customer_LName, Transaction_Tel, Transaction_Status, Transaction_Station_Station_Id)";
            String query2 = " VALUES ('" + date + comma + time + comma + Transaction_TransactionTrackingId + comma + Transaction_MerchantReference + comma + Transaction_CustumerFName + comma + Transaction_Customer_LName + comma + Transaction_Tel + comma + Transaction_Status + comma + clsGlobalVariables.Transaction_Station_Station_Id + "')";

            String query = query1 + query2;

            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                // MySqlCommand cmd = new MySqlCommand(query, connection);

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = query;
                //"INSERT INTO ORDERS ( ORDER_DATE, ORDER_TYPE) VALUES ('" + date + "',@ORDERTYPE)";
                //cmd.Prepare();
                //cmd.Parameters.AddWithValue("@ORDERTYPE", "S");

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }
        
        public void SaveTransactionDetails(String merchantReference, int standardPhoto, int passportPhoto)
        {           
            String comma = "','";
            String query1 = "INSERT INTO TransDetails ( TransDetails_Transaction_Transaction_MerchantReference, TransDetails_Standard, TransDetails_Passport, TransDetails_ProfitStandard, TransDetails_ProfitPPort)";
            String query2 = " VALUES ('" + merchantReference + comma + standardPhoto + comma + passportPhoto + comma + "0" + comma + "0" + "')";

            String query = query1 + query2;

            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor             

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = query;

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        public void GetPrintingPrices()
        {   

            //open connection
            if (this.OpenConnection() == true)
            {
                
                //create command and assign the query and connection from the constructor             

                MySqlCommand cmd = new MySqlCommand("PrintingPrice");

                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;

               // var rank = new MySqlParameter("@StandardPrice", MySqlDbType.Decimal) { Direction = ParameterDirection.Output };
              //  cmd.Parameters.Add(rank);

              //  var rank2 = new MySqlParameter("@PPortPrice", MySqlDbType.Decimal) { Direction = ParameterDirection.Output };
              //  cmd.Parameters.Add(rank2);

                cmd.Parameters.Add("@StandardPrice", MySqlDbType.Decimal);
                cmd.Parameters["@StandardPrice"].Direction = ParameterDirection.Output;

                cmd.Parameters.Add("@PPortPrice", MySqlDbType.Decimal);
                cmd.Parameters["@PPortPrice"].Direction = ParameterDirection.Output;
                
                
                //cmd.Prepare();
                //Execute command
                cmd.ExecuteNonQuery();
                clsGlobalVariables.StandardPhotoPrice = (Decimal)cmd.Parameters["@StandardPrice"].Value;
                clsGlobalVariables.PassportPhoroPrice = (Decimal)cmd.Parameters["@PPortPrice"].Value;

                //close connection
                this.CloseConnection();
            }
        }     


        //Update statement
        public void Update()
        {
            string query = "UPDATE ORDERS SET name='Joe', age='22' WHERE name='John Smith'";

            //Open connection
            if (this.OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        //Delete statement
        public void Delete()
        {
            string query = "DELETE FROM ORDERS WHERE name='John Smith'";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        //Select statement
        public List<string>[] Select()
        {
            string query = "SELECT * FROM ORDERS";

            //Create a list to store the result
            List<string>[] list = new List<string>[3];
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();

            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    list[0].Add(dataReader["ORDER_NO"] + "");
                    list[1].Add(dataReader["ORDER_DATE"] + "");
                    list[2].Add(dataReader["ORDER_TYPE"] + "");
                }

                //Console.WriteLine(rdr.GetInt32(0) + ": " + rdr.GetString(1));
                //dataReader
                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return list;
            }
            else
            {
                return list;
            }
        }

        //Count statement
        public int Count()
        {
            string query = "SELECT Count(*) FROM ORDERS";
            int Count = -1;

            //Open Connection
            if (this.OpenConnection() == true)
            {
                //Create Mysql Command
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //ExecuteScalar will return one value
                Count = int.Parse(cmd.ExecuteScalar() + "");

                //close Connection
                this.CloseConnection();

                return Count;
            }
            else
            {
                return Count;
            }
        }

        //Backup
        public void Backup()
        {
            try
            {
                DateTime Time = DateTime.Now;
                int year = Time.Year;
                int month = Time.Month;
                int day = Time.Day;
                int hour = Time.Hour;
                int minute = Time.Minute;
                int second = Time.Second;
                int millisecond = Time.Millisecond;

                //Save file to C:\a\ with the current date as a filename
                string path;
                path = "C:\\a\\MySqlBackup" + year + "-" + month + "-" + day +
            "-" + hour + "-" + minute + "-" + second + "-" + millisecond + ".sql";
                StreamWriter file = new StreamWriter(path);


                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = @"C:\Program Files\MySQL\MySQL Server 5.7\bin\mysqldump";
                psi.RedirectStandardInput = false;
                psi.RedirectStandardOutput = true;
                psi.Arguments = string.Format(@"-u{0} -p{1} -h{2} {3}",
                    uid, password, server, database);
                psi.UseShellExecute = false;

                Process process = Process.Start(psi);

                string output;
                output = process.StandardOutput.ReadToEnd();
                file.WriteLine(output);
                process.WaitForExit();
                file.Close();
                process.Close();
            }
            catch (IOException ex)
            {
                clsGlobalFunctions.ErrorLog(ex); //Log Error
                MessageBox.Show("Error , unable to backup!");
            }
        }

        //Restore
        public void Restore()
        {
            try
            {
                //Read file from C:\
                string path;
                path = "C:\\a\\MySqlBackup2015-6-18-8-40-7-107.sql";
                StreamReader file = new StreamReader(path);
                string input = file.ReadToEnd();
                file.Close();

                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = @"C:\Program Files\MySQL\MySQL Server 5.7\bin\mysql";
                psi.RedirectStandardInput = true;
                psi.RedirectStandardOutput = false;
                psi.Arguments = string.Format(@"-u{0} -p{1} -h{2} {3}", uid, password, server, database);
                psi.UseShellExecute = false;


                Process process = Process.Start(psi);
                process.StandardInput.WriteLine(input);
                process.StandardInput.Close();
                process.WaitForExit();
                process.Close();
            }
            catch (IOException ex)
            {
                clsGlobalFunctions.ErrorLog(ex); //Log Error
                MessageBox.Show("Error , unable to Restore!");
            }
        }

        public void Read()
        {
            //Open connection
            if (this.OpenConnection() == true)
            {
                string stm = "SELECT * FROM ORDERS";
                MySqlDataAdapter da = new MySqlDataAdapter(stm, connection);

                DataSet ds = new DataSet();

                da.Fill(ds, "orders");
                DataTable dt = ds.Tables["orders"];

                dt.WriteXml("c:\\a\\orders.xml");
               

                foreach (DataRow row in dt.Rows)
                {
                    foreach (DataColumn col in dt.Columns)
                    {
                        Console.WriteLine(row[col]);
                    }

                    Console.WriteLine("".PadLeft(20, '='));
                }

                //close Connection
                this.CloseConnection();
            }
        }

        public DataTable ReturnData()
        {
           // DataGrid dg = null;            
            MySqlDataAdapter da = null;        
            DataSet ds = null;

           string stm = "SELECT * FROM ORDERS";

            if (this.OpenConnection() == true)
            {                
                ds = new DataSet();
                da = new MySqlDataAdapter(stm, connection);
                da.Fill(ds, "orders");

                //dg.DataSource = ds.Tables["orders"];
                this.CloseConnection();                               
            }

            return ds.Tables["orders"]; 
            
        }

        public void CommitTransaction()
        {
           
            MySqlConnection conn = null;
            MySqlTransaction tr = null;

          if (this.OpenConnection() == true)
          { 
            try
            {
                
                tr = conn.BeginTransaction();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.Transaction = tr;

                cmd.CommandText = "UPDATE ORDERS SET Name='Leo Tolstoy' WHERE Id=1";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "UPDATE ORDERS SET Title='War and Peace' WHERE Id=1";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "UPDATE ORDERS SET Titl='Anna Karenina' WHERE Id=2";
                cmd.ExecuteNonQuery();

                tr.Commit();

            }
            catch (MySqlException ex)
            {
                try
                {
                    tr.Rollback();

                }
                catch (MySqlException ex1)
                {
                    Console.WriteLine("Error: {0}", ex1.ToString());
                }

                Console.WriteLine("Error: {0}", ex.ToString());

            }
          }
          //close Connection
          this.CloseConnection();
            
        }
    }
}
