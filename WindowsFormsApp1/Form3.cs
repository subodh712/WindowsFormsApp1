using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        SqlConnection connection;
        public Form3()
        {
            InitializeComponent();
            string connetionString = "Data Source=DESK\\SQLEXPRESS;Initial Catalog=TestForm;Trusted_Connection=true";
            connection = new SqlConnection(connetionString);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!textBox1.Text.Equals(""))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("select * from studentdetail where name = @param",connection);
                    cmd.Parameters.Add("@param", SqlDbType.NVarChar).Value = textBox1.Text;
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        MemoryStream ms = new MemoryStream((byte[])dt.Rows[0]["image"]);
                        label5.Text = dt.Rows[0]["Name"].ToString();
                        label6.Text = dt.Rows[0]["FatherName"].ToString();
                        label7.Text = dt.Rows[0]["MotherName"].ToString();
                        pictureBox1.Image = Image.FromStream(ms);
                        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                        pictureBox1.Refresh();
                    }
                    else
                        MessageBox.Show("No Record Found!!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    connection.Close();
                }
            }
            else
                MessageBox.Show("Enter the Name!!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
