using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace StudentsProfileApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection connection = new SqlConnection(@"Data Source=LAPTOP-BIA7GPHB\SQLEXPRESS;Initial Catalog=Student_db;Integrated Security=True");
        public int Student_Id;

        private void Form1_Load(object sender, EventArgs e)
        {
            GetStudentsRecord();
        }

        private void GetStudentsRecord()
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Student_TB",connection);
            DataTable dataTable = new DataTable();

            connection.Open();
            SqlDataReader sqlDataReader = command.ExecuteReader();
            dataTable.Load(sqlDataReader);
            connection.Close();
            StudentRecordDataGridView.DataSource = dataTable;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (IsVAlid())
            {
                SqlCommand command = new SqlCommand("INSERT INTO Student_TB VALUES (@Name, @Father_Name, @Roll_Number, @Address, @Mobile)",connection);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@Name", txtStudentName.Text);
                command.Parameters.AddWithValue("@Father_Name", txtFatherName.Text);
                command.Parameters.AddWithValue("@Roll_Number", txtRollNumber.Text);
                command.Parameters.AddWithValue("@Address", txtAddress.Text);
                command.Parameters.AddWithValue("@Mobile", txtMobile.Text);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                //MessageBox.Show("Student added successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GetStudentsRecord();
                ResetFormControls();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ResetFormControls();
        }

        private void ResetFormControls()
        {
            Student_Id = 0;
            txtStudentName.Clear();
            txtFatherName.Clear();
            txtRollNumber.Clear();
            txtAddress.Clear();
            txtMobile.Clear();

            txtStudentName.Focus();
        }

        private bool IsVAlid()
        {
            if (txtStudentName.Text == string.Empty)
            {
                MessageBox.Show("Student Name is required", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void StudentRecordDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Student_Id = Convert.ToInt32(StudentRecordDataGridView.SelectedRows[0].Cells[0].Value);
            txtStudentName.Text = StudentRecordDataGridView.SelectedRows[0].Cells[1].Value.ToString();
            txtFatherName.Text = StudentRecordDataGridView.SelectedRows[0].Cells[2].Value.ToString();
            txtRollNumber.Text = StudentRecordDataGridView.SelectedRows[0].Cells[3].Value.ToString();
            txtAddress.Text = StudentRecordDataGridView.SelectedRows[0].Cells[4].Value.ToString();
            txtMobile.Text = StudentRecordDataGridView.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Student_Id > 0)
            {
                SqlCommand command = new SqlCommand("UPDATE Student_TB SET Name = @Name, Father_Name = @Father_Name, Roll_Number = @Roll_Number, Address = @Address, Mobile = @Mobile WHERE Student_Id = @Id", connection);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@Name", txtStudentName.Text);
                command.Parameters.AddWithValue("@Father_Name", txtFatherName.Text);
                command.Parameters.AddWithValue("@Roll_Number", txtRollNumber.Text);
                command.Parameters.AddWithValue("@Address", txtAddress.Text);
                command.Parameters.AddWithValue("@Mobile", txtMobile.Text);
                command.Parameters.AddWithValue("@Id", this.Student_Id);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                MessageBox.Show("Student Information Updated successfully", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GetStudentsRecord();
                ResetFormControls();
            }
            else 
            {
                MessageBox.Show("Please select a student for update!", "Select?", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (Student_Id > 0)
            {
                SqlCommand command = new SqlCommand("DELETE FROM Student_TB WHERE Student_Id = @Id", connection);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@Id", this.Student_Id);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                MessageBox.Show("Student Information deleted successfully", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GetStudentsRecord();
                ResetFormControls();
            }
            else
            {
                MessageBox.Show("Please select a student for update!", "Select?", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
