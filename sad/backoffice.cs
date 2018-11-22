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


        private void backoffice_Load(object sender, EventArgs e) {
            con = new SqlConnection("Data Source=localhost;Initial Catalog=BinCompeteDB;Integrated Security=True;");
            adp = new SqlDataAdapter();
            ds = new DataSet();
            adp.SelectCommand = new SqlCommand("select *from Users",con);

            adp.Fill(ds,"Users");
            dt = ds.Tables["Users"];
            dr = dt.Rows[0];
            dgvCompeticoes.DataSource= ds.Tables["Users"];
            


        }


        
    }
}
