﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 fm2 = new Form2();
            fm2.Show();
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 fm3 = new Form3();
            fm3.Show();
        }

        private void exportAllImagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SqlConnection connection;
            string connetionString = System.Configuration.ConfigurationManager.ConnectionStrings["WindowsFormsApp1.Properties.Settings.TestFormConnectionString"].ConnectionString;
            connection = new SqlConnection(connetionString);
            try
            { 
                connection.Open();
                SqlCommand cmd = new SqlCommand("Select * from studentdetail",connection);
                SqlDataReader reader = cmd.ExecuteReader();
                String dir = "D:\\Images\\";
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                while (reader.Read())
                {
                    byte[] img =(byte[])reader["image"];
                    string name = reader["id"].ToString()+".jpeg";
                    FileStream fs;
                    if (!File.Exists(dir + name))
                    {
                         fs = new FileStream(dir + name, FileMode.Create, FileAccess.Write);
                    }
                    else
                    {
                         fs = new FileStream(dir + name, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                    }
                    fs.Write(img, 0, img.Length);
                    fs.Flush();
                    fs.Close();
                }

                MessageBox.Show("Exported all images to "+dir+" Successfully!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}
