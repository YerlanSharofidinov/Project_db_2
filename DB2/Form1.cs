using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace DB2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        DataTable dt = new DataTable("t_schedule");
        private void tabPage1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["cn"].ConnectionString))
                {
                    if (cn.State == ConnectionState.Closed) 
                        cn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter("select * from t_schedule", cn))
                    {
                        da.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }
                }
            }
            catch (Exception ex){
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet1.T_SCHEDULE' table. You can move, or remove it, as needed.
            this.t_SCHEDULETableAdapter.Fill(this.dataSet1.T_SCHEDULE);

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13) {
                DataView dv = dt.DefaultView;
                dv.RowFilter = string.Format("select * from t_schedule where emp_id like '"
                    +textBox1.Text+"%'", textBox1.Text);
                dataGridView1.DataSource = dv.ToTable();
            }
        }
    }
}
