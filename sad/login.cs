using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sad
{
    public partial class login : Form
    {
        public class MyUser
        {
            public string Name { get; set; }
            public bool Admin { get; set; }
        }

        public login()
        {
            InitializeComponent();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void login_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            DataTable dt = new DataTable();//matriz é prenchida com login - pass - admin
            using (SqlConnection sqlConn = new SqlConnection("Data Source=WSP-CSPSPO71;Initial Catalog=BinCompeteDB;Integrated Security=True;"))
            {
                string sql = "sp_login";
                using (SqlCommand sqlCmd = new SqlCommand(sql, sqlConn))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@LoginName", txtName.Text);
                    sqlCmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                    sqlConn.Open();
                    using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCmd))
                    {
                        sqlAdapter.Fill(dt);
                    }
                }
            }

            var InfoObj = (from rw in dt.AsEnumerable()
                           select new MyUser()
                           {
                               Name = Convert.ToString(rw["LoginName"]),
                               Admin = Convert.ToBoolean(rw["Administrator"])
                           });

        }
    }
}
