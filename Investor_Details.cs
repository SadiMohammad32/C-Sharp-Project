﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace Project_Login
{
    public partial class Investor_Details : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;

        public Investor_Details()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Investor i1 = new Investor();
            i1.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            HomePage hm = new HomePage();
            hm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select Image";
            ofd.Filter = "All Files *.* | *.*";
            //ofd.ShowDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(ofd.FileName);
            }
        }



        private void button4_Click(object sender, EventArgs e)
        {
            //textBox1.Clear();
            //textBox2.Clear();
            //textBox3.Clear();
            //textBox4.Clear();
            //textBox5.Clear();
            //pictureBox1.Image = Properties.Resources.default_image;
            if (textBox1.Text == "")
            {
                MessageBox.Show("Fill the information", "Failed..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Focus();
            }

            else if (textBox2.Text == "")
            {
                MessageBox.Show("Fill the information", "Failed..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Focus();
            }
           
            else if (textBox4.Text == "")
            {
                MessageBox.Show("Fill the information", "Failed..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox4.Focus();
            }
            else if (textBox5.Text == "")
            {
                MessageBox.Show("Fill the information", "Failed..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox5.Focus();
            }

            else
            {
                if (textBox1.Text == textBox1.Text)
                {
                    SqlConnection con = new SqlConnection(cs);
                    string query = "UPDATE INVEST_DETAILS SET US_NAME=@USER,CONTACT=@CONTACT, ADDRES=@add, BANK_ID=@bac, PICTURE=@img WHERE NID='" + HomePage.nid + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@USER", textBox1.Text);
                    cmd.Parameters.AddWithValue("@CONTACT", textBox2.Text);
                   // cmd.Parameters.AddWithValue("@nid", textBox3.Text);
                    cmd.Parameters.AddWithValue("@add", textBox4.Text);
                    cmd.Parameters.AddWithValue("@bac", textBox5.Text);
                    cmd.Parameters.AddWithValue("@img", savepic());

                    con.Open();
                    int a = cmd.ExecuteNonQuery();
                    if (a > 0)
                    {
                        MessageBox.Show("UPDATE SUCCESSFULLY", "Successful..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Hide();
                        HomePage hp = new HomePage();
                        hp.Show();
                    }
                    else
                    {
                        MessageBox.Show("User Not Matched", "Failed..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    // con.Close();
                }
                else
                {
                    MessageBox.Show("User Not Matched", "Failed..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Clear();
                }

            }
        }

        private byte[] savepic()
        {
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
            return ms.GetBuffer();

        }

        private void Investor_Details_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "SELECT * FROM INVEST_DETAILS WHERE US_NAME='" + HomePage.user + "'";

            SqlCommand cmd = new SqlCommand(query, con);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows == true)
            {
                if (dr.Read())
                {
                    HomePage.user = dr.GetValue(1).ToString();
                    HomePage.contract = dr.GetValue(4).ToString();
                    HomePage.bankacc = dr.GetValue(8).ToString();
                    HomePage.address = dr.GetValue(6).ToString();

                    HomePage.image = ((byte[])dr.GetValue(7));

                    textBox1.Text = HomePage.user;
                    textBox2.Text = HomePage.contract;
                    textBox5.Text = HomePage.bankacc;
                    textBox4.Text = HomePage.address;

                    pictureBox1.Image = GetPhoto((byte[])HomePage.image);
                    con.Close();
                }

            }
        }

        private Image GetPhoto(byte[] photo)
        {
            MemoryStream ms = new MemoryStream(photo);
            return Image.FromStream(ms);
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox4.Clear();
            textBox5.Clear();
            pictureBox1.Image = Properties.Resources.default_image;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
