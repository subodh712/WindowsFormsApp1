using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        string img=null;
        OpenFileDialog openFileDialog1;
        SqlConnection connection;
        public Form2()
        {
            InitializeComponent();
            openFileDialog1 = new OpenFileDialog();

            string connetionString = System.Configuration.ConfigurationManager.ConnectionStrings["WindowsFormsApp1.Properties.Settings.TestFormConnectionString"].ConnectionString;
            connection = new SqlConnection(connetionString);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "jpeg|*.jpg|bmp|*.bmp";
            DialogResult res = openFileDialog1.ShowDialog();
            if (res == DialogResult.OK)
            {
                img = openFileDialog1.FileName;
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                FileStream FS = new FileStream(img, FileMode.Open, FileAccess.Read);
                byte[] imgb = new byte[FS.Length];
                FS.Read(imgb, 0, Convert.ToInt32(FS.Length));
                SqlCommand command = new SqlCommand("insert into StudentDetail (Name , FatherName , MotherName , image ) values(@Name,@fName,@mName,@Image)",connection);
                command.Parameters.Add("@Name",SqlDbType.NVarChar).Value = textBox1.Text;
                command.Parameters.Add("@fName", SqlDbType.NVarChar).Value = textBox2.Text;
                command.Parameters.Add("@mName", SqlDbType.NVarChar).Value = textBox3.Text;
                command.Parameters.Add("@Image", SqlDbType.Image).Value = imgb;
                command.ExecuteNonQuery();
                MessageBox.Show("Entry Save Successfully!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                connection.Close();
                this.Close();
            }
        }


        private void validation(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }
    }
}
