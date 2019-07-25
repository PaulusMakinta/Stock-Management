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

namespace Stock
{
    public partial class Products : Form
    {
        public Products()
        {
            InitializeComponent();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Products_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            LoadData();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-NB4JB3D;Initial Catalog=Stock;Integrated Security=True");

            con.Open();

            bool status = false;
            if(comboBox1.SelectedIndex == 0)
            {
                status = true;
            }
            else
            {
                status = false;
            }

            var sqlQuery = "";

           if(IfProductExists(con,txtProductCode.Text))
           {
                sqlQuery = @"UPDATE [Products] SET [ProductName] = '" + txtProductName.Text + "' ,[ProductStatus] = '" + status + "' WHERE [ProductCode] = '"+ txtProductCode.Text +"'";
           }
            else
            {
              sqlQuery = @"INSERT INTO [Stock].[dbo].[Products],([ProductCode] ,[ProductName] ,[ProductStatus] VALUES
                        ('" + txtProductCode.Text + "','" + txtProductName.Text + "','" + status + "')";
                        
            }


            SqlCommand cmd = new SqlCommand(sqlQuery, con);
            cmd.ExecuteNonQuery();
            con.Close();

            LoadData();


        }

        private bool IfProductExists(SqlConnection con, string productCode)
        {
            SqlDataAdapter sda = new SqlDataAdapter(@"SELECT 1 FROM [Products] WHERE [ProductCode] = '"+ productCode +"'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        public void LoadData()
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-NB4JB3D;Initial Catalog=Stock;Integrated Security=True");
            SqlDataAdapter sda = new SqlDataAdapter(@"SELECT * FROM [Stock].[dbo].[Products]", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            dataGridView1.Rows.Clear();

            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["ProductCode"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["ProductName"].ToString();
                if ((bool)item["ProductStatus"])
                {
                    dataGridView1.Rows[n].Cells[2].Value = "Active";
                }
                else
                {
                    dataGridView1.Rows[n].Cells[2].Value = "Deactive";
                }

            }
        }

       
        private void DataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txtProductCode.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            txtProductName.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            if (dataGridView1.SelectedRows[0].Cells[2].Value.ToString() == "Active")
            {
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                comboBox1.SelectedIndex = 1;
            }
             
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-NB4JB3D;Initial Catalog=Stock;Integrated Security=True");

            var sqlQuery = "";

            if (IfProductExists(con, txtProductCode.Text))
            {
                con.Open();
                sqlQuery = @"DELETE FROM [Products]  WHERE [ProductCode] = '" + txtProductCode.Text + "'";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                MessageBox.Show("Record Do Not Exist.....!");
            }


            

            LoadData();
        }
    }
}
