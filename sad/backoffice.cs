using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//novos sql server
using System.Data.SqlClient;

namespace sad {
    public partial class backoffice : Form {
        public backoffice() {
            InitializeComponent();
        }

        //inicializar coneçao sql server
        SqlConnection con;
        SqlDataAdapter adp;
        SqlCommand cb;

        DataSet ds;
        DataTable dt;
        DataRow dr;


        public void backoffice_Load() {
            //matriz é prenchida com login retornoado da parte do SQL
            DataTable dt = new DataTable();
            //Dentro das chavetas é indicado o código de localização da BD e o dados de acesso
            using (SqlConnection sqlConn = new SqlConnection("Data Source=TOSHIBA\\SQLEXPRESS;Initial Catalog=BinCompeteDB;Integrated Security=True;")) {
            //using (SqlConnection sqlConn = new SqlConnection("Data Source=localhost;Initial Catalog=BinCompeteDB;Integrated Security=True;")) { 
                //nome da procedure onde o programa irá buscar os dados de acesso à aplicação
                string sql = "sp_Users";
                //cria a con à BD usando procedure que irá aceder e passar os valores das caixas de texto e o caminho para a BD
                using (SqlCommand sqlCmd = new SqlCommand(sql, sqlConn)) {

                    //
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                   
                    sqlConn.Open();
                    using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCmd)) {
                        sqlAdapter.Fill(dt);
                    }
                    dgvCompeticoes.DataSource = dt;
                }
            }

            foreach (DataRow row in dt.Rows) {
                string verCompeticoes = row["LoginName"].ToString();
            }
        }

        private void dgvCompeticoes_CellContentClick(object sender, DataGridViewCellEventArgs e) {

        }

        private void button2_Click(object sender, EventArgs e) {
            editar_comp editar_comp1 = new editar_comp();
            editar_comp1.editar_comp_Load();
            editar_comp1.ShowDialog();
            this.Visible = false;

        }
    }
}
