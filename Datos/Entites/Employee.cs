using DataAccess.Repositories;
using Datos.Cache;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entites
{
    public class Employee:MasterRepository
    {
        int id;
        string user;
        string name;
        string mail;
        string pass;

        public int Id { get => id; set => id = value; }
        public string User { get => user; set => user = value; }
        public string Name { get => name; set => name = value; }
        public string Mail { get => mail; set => mail = value; }
        public string Pass { get => pass; set => pass = value; }

        public Employee()
        {
            this.id = Id;
            this.user = User;
            this.name = Name;
            this.Mail = mail;
            this.pass = Pass;
        }
        private void AddEmployee()
        {
            string transaction = "RegisterEmployee";
            param = new List<SqlParameter>();
            param.Add(new SqlParameter("@username", user));
            param.Add(new SqlParameter("@name", name));
            param.Add(new SqlParameter("@email", mail));
            param.Add(new SqlParameter("@password", pass));
            ExecuteNonQuery(transaction);
        }
        public string Save()
        {
            string msj = null;
            try
            {
                AddEmployee();
                msj = "Employee save";
            }catch(Exception)
            {
                msj = "Fail to save";
            }
            return msj;
        }
        public bool LoginEmployee(string usr, string pass)
        {
            using(var conn = GetConnection())
            {
                conn.Open();
                using(var command = new SqlCommand())
                {
                    command.Connection = conn;
                    command.CommandText = "select * from EMPLOYEE where username = @username and CONVERT(NVARCHAR(MAX),DECRYPTBYPASSPHRASE('PASSWORD',password)) = @password";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@username", usr);
                    command.Parameters.AddWithValue("@password", pass);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            CacheEmployee.id_e = reader.GetInt32(0);
                            CacheEmployee.usr = reader.GetString(1);
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
    }
}
