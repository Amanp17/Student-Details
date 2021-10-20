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

namespace Student_Details
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            load();
        }

        //Establishing SQL Connection
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-OGH3VOT\\MSSQLSERVER01;Database=FirstDB;Integrated Security=SSPI");
        //Create SQL Command
        SqlCommand cmd;
        SqlDataReader read;
        string id;
        //If the mode is true means add record else update the record
        bool mode=true;
        string sql;

        
        


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //When details are entered then stored in following varaibles..
            string name = txtName.Text;
            string course = txtCourse.Text;
            string fees = txtFees.Text;
            string duration = txtDuration.Text;
            if(txtName.Text.Length==0 || txtFees.Text.Length==0 || txtCourse.Text.Length==0 || txtDuration.Text.Length == 0)
            {
                MessageBox.Show("Please Add Details!!");
                txtName.Focus();
            }
            else if(mode == true )
            {
                //this will add the values at to particular parameters
                sql = "insert into student(stname,course,fees,duration) values(@stname,@course,@fees,@duration)";
                con.Open();  //connection open
                cmd = new SqlCommand(sql, con);  //check that both sql and con works correctly or not
                cmd.Parameters.AddWithValue("@stname", name);
                cmd.Parameters.AddWithValue("@course", course);
                cmd.Parameters.AddWithValue("@fees", fees);
                cmd.Parameters.AddWithValue("@duration", duration);
                
                MessageBox.Show("Record Added!!");
            
                //This will return the number of rows affected after the sql query is processed
                cmd.ExecuteNonQuery();
                //This will clear the text boxes
                txtName.Clear();
                txtCourse.Clear();
                txtFees.Clear();
                txtDuration.Clear();
                //Cursor should focus on Name
                txtName.Focus();



            }
            else
            {
                id = dataGridView1.CurrentRow.Cells[1].Value.ToString();

                sql = "Update student set stname = @stname, course = @course , fees = @fees,duration = @duration where id = @id";
                con.Open();  //connection open
                cmd = new SqlCommand(sql, con);  //check that both sql and con works correctly or not
                cmd.Parameters.AddWithValue("@stname", name);
                cmd.Parameters.AddWithValue("@course", course);
                cmd.Parameters.AddWithValue("@fees", fees);
                cmd.Parameters.AddWithValue("@duration", duration);
                cmd.Parameters.AddWithValue("@id", id);

                MessageBox.Show("Record Updated!!");

                //This will return the number of rows affected after the sql query is processed
                cmd.ExecuteNonQuery();
                //This will clear the text boxes
                txtName.Clear();
                txtCourse.Clear();
                txtFees.Clear();
                txtDuration.Clear();
                //Cursor should focus on Name
                txtName.Focus();
                button1.Text = "Save";
                mode = true;
            }
            try
            {
                sql = "select * from student"; //query for selecting the table from sql database
                cmd = new SqlCommand(sql, con);                    //reading data
                read = cmd.ExecuteReader();
                dataGridView1.Rows.Clear();

                while (read.Read())
                {
                    string no = dataGridView1.Rows.Count.ToString();
                    int number = int.Parse(no);
                    number += 1;

                    dataGridView1.Rows.Add(number, read[0], read[1], read[2], read[3], read[4]);

                }
            }
            catch (Exception y)
            {
                MessageBox.Show(y.Message);
            }
            con.Close();
        }

        //for loading the database table into datagridview
        public void load()
        {

            try
            {
                sql = "select * from student"; //query for selecting the table from sql database
                cmd = new SqlCommand(sql, con);
                con.Open();
                //reading data
                read = cmd.ExecuteReader();
                dataGridView1.Rows.Clear();

                while (read.Read())
                {
                    string no = dataGridView1.Rows.Count.ToString();
                    int number = int.Parse(no);
                    number += 1;

                    dataGridView1.Rows.Add(number,read[0], read[1], read[2], read[3],read[4]);

                }


                con.Close();

            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void getId(string id)
        {
            sql = "select * from student where id = '"+ id +"'";
            con.Open();

            cmd = new SqlCommand(sql, con);

            read = cmd.ExecuteReader();



            while (read.Read())
            {
                txtName.Text = read[1].ToString();
                txtCourse.Text = read[2].ToString();
                txtFees.Text = read[3].ToString();
                txtDuration.Text = read[4].ToString();

            }
            con.Close();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex==dataGridView1.Columns["Edit"].Index && e.RowIndex >= 0)
            {
                mode = false;
                id = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                getId(id);
                button1.Text = "Update";
            }
            else if (e.ColumnIndex == dataGridView1.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                mode = false;
                id = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                sql = "Delete from student where id = @id";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Deleted Successfully");

                try
                {
                    sql = "select * from student"; //query for selecting the table from sql database
                    cmd = new SqlCommand(sql, con);                    //reading data
                    read = cmd.ExecuteReader();
                    dataGridView1.Rows.Clear();

                    while (read.Read())
                    {
                        string no = dataGridView1.Rows.Count.ToString();
                        int number = int.Parse(no);
                        number += 1;

                        dataGridView1.Rows.Add(number, read[0], read[1], read[2], read[3], read[4]);

                    }
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.Message);
                }

                txtName.Clear();
                txtCourse.Clear();
                txtFees.Clear();
                txtDuration.Clear();

                button1.Text = "Save";
                txtName.Focus();
                mode = true;

            }
            con.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            load();
            txtName.Focus();
        }
        


        private void button2_Click(object sender, EventArgs e)
        {
            txtName.Clear();
            txtCourse.Clear();
            txtFees.Clear();
            txtDuration.Clear();
            button1.Text = "Save";
            txtName.Focus();
            mode = true;
        }
    }
}
