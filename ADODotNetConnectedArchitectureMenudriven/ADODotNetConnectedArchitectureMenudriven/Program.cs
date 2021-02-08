using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADODotNetConnectedArchitectureMenudriven
{
    class InsertRow
    {
        public void MainMenu()
        {
         //   Console.Clear();
            Console.WriteLine("Welcome to OperationDemo");
            Console.WriteLine("----------------------------");

            Console.WriteLine("Option 1: InsertOneRow");
            Console.WriteLine("Option 2: InsertWithParameter");
            Console.WriteLine("Option 3: InsertWithSp");
            Console.WriteLine("Option 4: DeleteRow");
            Console.WriteLine("Option 5: DeleteWithParameter");
            Console.WriteLine("Option 6: DeleteWithSp");
            Console.WriteLine("Option 7: UpdateOneRow");
            Console.WriteLine("Option 8: UpdateWithParameter");
            Console.WriteLine("Option 9: UpdateWithSp");
            Console.WriteLine("Option 10: ShowData");
            Console.WriteLine("Option 11: SearchData");
            Console.WriteLine("Option 12: Exit");
           
            Console.WriteLine("Select the option");
            
            string myOptions;
            myOptions = Console.ReadLine();
            Console.ReadLine();
            switch (myOptions)
            {
                case "1":
                    InsertOneRow();
                    break;

                case "2":
                    InsertWithParameter();
                    break;
                case "3":
                    InsertWithSp();
                    break;
                case "4":
                    DeleteRow();
                    break;
                case "5":
                    DeleteWithParameter();
                    break;
                case "6":
                    DeleteWithSp();
                    break;
                case "7":
                    UpdateOneRow();
                    break;
                case "8":
                    UpdateWithParameter();
                    break;
                case "9":
                    UpdateWithSp();
                    break;
                case "10":
                    ShowData();
                    break;
                case "11":
                    SearchData();
                    break;
              
                case "12":
                    
                    break;
            }

            MainMenu();
        }
        SqlConnection cn = null;
        SqlCommand cmd = null;
        SqlDataReader dr = null;
        public int ShowData()
        {

            try

            {
                Console.WriteLine("Data from table after DML command");
                Console.WriteLine("-----------------");
                cn = new SqlConnection(@"Data Source=LAPTOP-M30FVMGQ;Initial Catalog=WFADotNet;Integrated Security=True");
                cmd = new SqlCommand("select * from employeetable", cn);
                cn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Console.WriteLine($"{dr["empid"]}\t{dr["empname"]}\t{dr["salary"]}\t{dr["Deptno"]}");
                    //Console.WriteLine($"{dr["empname"]}\t{dr["salary"]}\t{dr["Deptno"]}");
                }
                return 0;



            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return 1;
            }
            finally
            {
                cn.Close();
            }
        }
        public int InsertWithParameter()
        {
            try
            {
                Console.WriteLine("Enter Emploee Name");
                var ename = Console.ReadLine();
                Console.WriteLine("enter employee sal");
                var esal = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("enter employee deptid");
                var did = Convert.ToInt32(Console.ReadLine());
                cn = new SqlConnection("Data Source=LAPTOP-M30FVMGQ;Initial Catalog=WFADotNet;Integrated Security=True");
                cmd = new SqlCommand("insert into employeetable values(@ename,@esal,@deptid)", cn);
                cmd.Parameters.Add("@ename", SqlDbType.VarChar, 20).Value = ename;
                cmd.Parameters.Add("@esal", SqlDbType.Float).Value = esal;
                cmd.Parameters.Add("@deptid", SqlDbType.Int).Value = did;
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                ShowData();
                return i;

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return 0;
            }
            finally
            {
                cn.Close();
            }
        }
        public int InsertWithSp()
        {
            try
            {
                /*Console.WriteLine("Enter Employee Id");
                var eid = Console.ReadLine();*/
                Console.WriteLine("enter Employee Name");
                var ename = Console.ReadLine();
                Console.WriteLine("enter Employee Salary");
                var esal = Console.ReadLine();
                Console.WriteLine("enter Dept no");
                var did = Console.ReadLine();
                cn = new SqlConnection("Data Source=LAPTOP-M30FVMGQ;Initial Catalog=WFADotNet;Integrated Security=True");
                cmd = new SqlCommand("sp_InsertEmp", cn);
                cmd.CommandType = CommandType.StoredProcedure;


                // cmd.Parameters.Add("@empid", SqlDbType.Int).Value = eid;
                cmd.Parameters.Add("@empname", SqlDbType.VarChar, 20).Value = ename;
                cmd.Parameters.Add("@esal", SqlDbType.Float).Value = esal;
                cmd.Parameters.Add("@deptid", SqlDbType.Int).Value = did;

                cn.Open();
                int i = cmd.ExecuteNonQuery();
                ShowData();
                return i;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 1;
            }
            finally
            {
                cn.Close();
            }
        }

        public int InsertOneRow()
        {
            try
            {
                Console.WriteLine("Enter Emploee Name");
                var ename = Console.ReadLine();
                Console.WriteLine("enter employee sal");
                var esal = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("enter employee deptid");
                var did = Convert.ToInt32(Console.ReadLine());
                cn = new SqlConnection("Data Source=LAPTOP-M30FVMGQ;Initial Catalog=WFADotNet;Integrated Security=True");
                cmd = new SqlCommand("insert into employee values('" + ename + "'," + esal + "," + did + ")", cn);
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                Console.WriteLine("One row added in table");
                ShowData();
                return i;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
            finally
            {
                cn.Close();
            }
        }

        public int DeleteRow()
        {
            try
            {
                Console.WriteLine("enter employee deptid");
                var eid = Convert.ToInt32(Console.ReadLine());

                cn = new SqlConnection("Data Source=LAPTOP-M30FVMGQ;Initial Catalog=WFADotNet;Integrated Security=True");
                cmd = new SqlCommand("Delete from employeetable where empid=(" + eid + ")", cn);
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                Console.WriteLine("One row deleted in table");
                ShowData();
                return i;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 1;
            }
            finally
            {
                cn.Close();
            }
        }
        public int DeleteWithParameter()
        {
            try
            {
                Console.WriteLine("Enter Employee id");
                var eid = Convert.ToInt32(Console.ReadLine());

                cn = new SqlConnection("Data Source=LAPTOP-M30FVMGQ;Initial Catalog=WFADotNet;Integrated Security=True");
                cmd = new SqlCommand("Delete from employeetable where empid=(" + eid + ")", cn);
                cmd.Parameters.Add("@eid", SqlDbType.VarChar, 20).Value = eid;

                cn.Open();
                int i = cmd.ExecuteNonQuery();
                ShowData();
                return i;

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return 1;
            }
            finally
            {
                cn.Close();
            }
        }
        public int DeleteWithSp()
        {
            try
            {
                Console.WriteLine("enter EmployeId for that u want to Delete details");
                var eid = Console.ReadLine();

                cn = new SqlConnection("Data Source=LAPTOP-M30FVMGQ;Initial Catalog=WFADotNet;Integrated Security=True");
                cmd = new SqlCommand("sp_DeleteEmp", cn);
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add("@empid", SqlDbType.Int).Value = eid;
                /*  cmd.Parameters.Add("@empname", SqlDbType.VarChar, 20).Value = ename;
                  cmd.Parameters.Add("@esal", SqlDbType.Float).Value = esal;
                  cmd.Parameters.Add("@deptid", SqlDbType.Int).Value = did;*/

                cn.Open();
                int i = cmd.ExecuteNonQuery();
                ShowData();
                return i;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 1;
            }
            finally
            {
                cn.Close();
            }
        }


        public int UpdateOneRow()
        {
            try
            {
                Console.WriteLine("enter EmployeId for that u want to updatee datils");
                var eid = Console.ReadLine();
                Console.WriteLine("enter EmployeName");
                var ename = Console.ReadLine();
                Console.WriteLine("enter Employee Salary");
                var esal = Console.ReadLine();
                Console.WriteLine("enter Employe Salary");
                var deptno = Console.ReadLine();
                cn = new SqlConnection("Data Source=LAPTOP-M30FVMGQ;Initial Catalog=WFADotNet;Integrated Security=True");
                cmd = new SqlCommand("Update EmployeeTable set salary=(" + esal + "),empname=('" + ename + "'),deptno=(" + deptno + ") where empid=(" + eid + ")", cn);
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                Console.WriteLine("One row updated to the table:");
                return i;

            }
            catch (Exception e)
            {

                Console.WriteLine($"{e.Message} ");
                return 1;
            }
        }
        public int UpdateWithParameter()
        {
            try
            {
                Console.WriteLine("Enter EmployeId for that you want to update details");
                var eid = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Enter Employee Name");
                var ename = Console.ReadLine();
                Console.WriteLine("Enter Employee sal");
                var esal = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter Employee deptid");
                var did = Convert.ToInt32(Console.ReadLine());
                cn = new SqlConnection("Data Source=LAPTOP-M30FVMGQ;Initial Catalog=WFADotNet;Integrated Security=True");
                cmd = new SqlCommand("Update EmployeeTable set salary=(" + esal + "),empname=('" + ename + "'),deptno=(" + did + ")where empid=(" + eid + ")", cn);
                // cmd.Parameters.Add("@eid", SqlDbType.Int).Value = eid;
                cmd.Parameters.Add("@ename", SqlDbType.VarChar, 20).Value = ename;
                cmd.Parameters.Add("@esal", SqlDbType.Float).Value = esal;
                cmd.Parameters.Add("@did", SqlDbType.Int).Value = did;
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                ShowData();
                return i;

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return 1;
            }
            finally
            {
                cn.Close();
            }
        }

        public int UpdateWithSp()
        {
            try
            {
                Console.WriteLine("enter EmployeId for that u want to update details");
                var eid = Console.ReadLine();
                Console.WriteLine("enter EmployeName");
                var ename = Console.ReadLine();
                Console.WriteLine("enter Employe Salary");
                var esal = Console.ReadLine();
                Console.WriteLine("enter Employe Salary");
                var did = Console.ReadLine();
                cn = new SqlConnection("Data Source=LAPTOP-M30FVMGQ;Initial Catalog=WFADotNet;Integrated Security=True");
                cmd = new SqlCommand("sp_UpdateEmp", cn);
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add("@empid", SqlDbType.Int).Value = eid;
                cmd.Parameters.Add("@empname", SqlDbType.VarChar, 20).Value = ename;
                cmd.Parameters.Add("@esal", SqlDbType.Float).Value = esal;
                cmd.Parameters.Add("@deptid", SqlDbType.Int).Value = did;

                cn.Open();
                int i = cmd.ExecuteNonQuery();
                ShowData();
                return i;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 1;
            }
            finally
            {
                cn.Close();
            }
        }

        public void ShowEmpDetails()
        {
            try
            {
               
                cn = new SqlConnection("Data Source=LAPTOP-M30FVMGQ;Initial Catalog=WFADotNet;Integrated Security=True");
                cmd = new SqlCommand("sp_ShowEmp1", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                Console.WriteLine("Enter an existing Employee id to see the details....");
                var eid = Convert.ToInt32(Console.ReadLine());

                cmd.Parameters.Add("@empid", SqlDbType.Int).Value = eid;

                cn.Open();
                dr = cmd.ExecuteReader();
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {

                        Console.WriteLine($"Emp Name : {dr["empname"].ToString()}");
                        Console.WriteLine($"Salary : { dr["salary"].ToString()}");
                        Console.WriteLine($"DeptName :{dr["deptname"].ToString()}");
                    }

                }
                else
                {
                    Console.WriteLine("NO DATA FOUND......");
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");

            }
            finally
            {
                dr.Close();
                cn.Close();
            }
        }
        public int SearchData()
        {

            try
            {
                Console.WriteLine("Enter EmpName : ");
                var EmpName = Console.ReadLine();

                cn = new SqlConnection("Data Source=LAPTOP-M30FVMGQ;Initial Catalog=WFADotNet;Integrated Security=True");
                cmd = new SqlCommand("SELECT * FROM EmployeeTable WHERE EmpName= @EmpName", cn);

                cmd.Parameters.Add("@EmpName", SqlDbType.VarChar, 20).Value = EmpName;
                cn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Console.WriteLine($"{dr["empname"]}\t{dr["salary"]}\t{dr["Deptno"]}");
                }
                return 0;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                return 1;
            }
            finally
            {
                cn.Close();
            }

        }

    }
    class InsertRowInEmpTable
    {
        static void Main()
        {
            InsertRow ir = new InsertRow();
            //ir.InsertWithParameter();
            // ir.DeleteWithParameter();
            // ir.InsertOneRow();
            // ir.DeleteRow();
            //ir.UpdateWithSp();
            //ir.ShowEmpDetails();
            // ir.UpdateWithParameter();
            //ir.InsertWithSp();
            //  ir.DeleteWithSp();
            ir.MainMenu();
            Console.ReadLine();

        }
    }
}
