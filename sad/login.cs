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
using System.Security.Cryptography;

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
        public static class SHA {
            //https://codeshare.co.uk/blog/sha-256-and-sha-512-hash-examples/
            public static string GenerateSHA256String(string inputString) {
                SHA256 sha256 = SHA256Managed.Create();
                byte[] bytes = Encoding.UTF8.GetBytes(inputString);
                byte[] hash = sha256.ComputeHash(bytes);
                return GetStringFromHash(hash);
            }

            public static string GenerateSHA512String(string inputString) {
                SHA512 sha512 = SHA512Managed.Create();
                byte[] bytes = Encoding.UTF8.GetBytes(inputString);
                byte[] hash = sha512.ComputeHash(bytes);
                return GetStringFromHash(hash);
            }

            private static string GetStringFromHash(byte[] hash) {
                StringBuilder result = new StringBuilder();
                for (int i = 0; i < hash.Length; i++) {
                    result.Append(hash[i].ToString("X2"));
                }
                return result.ToString();
            }
        }


        private void panel3_Paint(object sender, PaintEventArgs e)
        {


        }

        private void login_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            
            //matriz é prenchida com login retornoado da parte do SQL
            DataTable dt = new DataTable();
            //Dentro das chavetas é indicado o código de localização da BD e o dados de acesso
            using (SqlConnection sqlConn = new SqlConnection("Data Source=TOSHIBA\\SQLEXPRESS;Initial Catalog=BinCompeteDB;Integrated Security=True;")) {
            //using (SqlConnection sqlConn = new SqlConnection("Data Source=localhost;Initial Catalog=BinCompeteDB;Integrated Security=True;")) { 
                //nome da procedure onde o programa irá buscar os dados de acesso à aplicação
                string sql = "sp_login";
                //cria a con à BD usando procedure que irá aceder e passar os valores das caixas de texto e o caminho para a BD
                using (SqlCommand sqlCmd = new SqlCommand(sql, sqlConn))
                {
                    
                    //
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

            foreach (DataRow row in dt.Rows)
            {
                string Login = row["LoginName"].ToString();
                string Admin = row["UsersID"].ToString();

            }
            //var InfoObj = (from rw in dt.AsEnumerable()
            //               select new MyUser()
            //               {
            //                   Name = Convert.ToString(rw["LoginName"]),
            //                   Admin = Convert.ToBoolean(rw["Administrator"])
            //               });
            

            //************************************ Falta o and login
            if (rdbUtilizador.Checked==false ) {
                backoffice abrirBackoffice = new backoffice();
                abrirBackoffice.backoffice_Load();
                abrirBackoffice.ShowDialog();
                this.Visible = false;

            }
           
            
        }

        private void rdb_CheckedChanged(object sender, EventArgs e) {
            
            if (rdbUtilizador.Checked) {
                rdbUtilizador.Checked = true;

            }
            else {
                rdbUtilizador.Checked = false;
            }


        }

        private void txtName_TextChanged(object sender, EventArgs e) {

        }
    }
}
