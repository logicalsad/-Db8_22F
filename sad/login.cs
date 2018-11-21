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
            //matriz é prenchida com login retornoado da parte do SQL
            DataTable dt = new DataTable();
            //Dentro das chavetas é indicado o código de localização da BD e o dados de acesso
            using (SqlConnection sqlConn = new SqlConnection("Data Source=localhost;Initial Catalog=BinCompeteDB;Integrated Security=True;"))
            {
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
                string Admin = row["Administrator"].ToString();

            }
            //var InfoObj = (from rw in dt.AsEnumerable()
            //               select new MyUser()
            //               {
            //                   Name = Convert.ToString(rw["LoginName"]),
            //                   Admin = Convert.ToBoolean(rw["Administrator"])
            //               });

        }
    }
}
