using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace CRUD_operations
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-P3FFI96\SQLEXPRESS;Initial Catalog=CRUDoperations;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False");
		public int StudentID;

		private void button1_Click(object sender, EventArgs e)
		{
			if(IsValid())
            {
				SqlCommand cmd = new SqlCommand("INSERT INTO StudentTable VALUES (@Name, @Email, @RollNumber , @Address , @Mobile)", con);
				cmd.CommandType = CommandType.Text;
				cmd.Parameters.AddWithValue("@Name", txtStudentName.Text);
				cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
				cmd.Parameters.AddWithValue("@RollNumber", txtRollNumber.Text);
				cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
				cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text);

				con.Open();
				cmd.ExecuteNonQuery();
				con.Close();

				MessageBox.Show("New student is succesfully saved in the database", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

				GetStudentsRecord();
				ResetFormControls();

			}
		}

        private bool IsValid()
        {
            if(txtStudentName.Text == string.Empty)
            {
				MessageBox.Show(" Student Name is required", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
            }

			return true;
        }

        private void label1_Click(object sender, EventArgs e)
		{

		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{

		}

		private void textBox5_TextChanged(object sender, EventArgs e)
		{

		}

		private void Form1_Load(object sender, EventArgs e)
		{
			GetStudentsRecord();
		}

		private void GetStudentsRecord()
		{
			
			SqlCommand cmd = new SqlCommand("Select * from StudentTable", con);
			DataTable dt = new DataTable();

			con.Open();

			SqlDataReader sdr = cmd.ExecuteReader();
			dt.Load(sdr);
			con.Close();

			StudentRecordDataGridView.DataSource = dt;

		}

        private void button4_Click(object sender, EventArgs e)
        {
            ResetFormControls();
        }

        private void ResetFormControls()
        {
			StudentID = 0;
            txtStudentName.Clear();
            txtEmail.Clear();
            txtRollNumber.Clear();
            txtAddress.Clear();
            txtMobile.Clear();

			txtStudentName.Focus();
        }

        private void StudentRecordDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void StudentRecordDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
			StudentID = Convert.ToInt32(StudentRecordDataGridView.SelectedRows[0].Cells[0].Value);
			txtStudentName.Text = StudentRecordDataGridView.SelectedRows[0].Cells[1].Value.ToString();
			txtEmail.Text = StudentRecordDataGridView.SelectedRows[0].Cells[2].Value.ToString();
		    txtRollNumber.Text = StudentRecordDataGridView.SelectedRows[0].Cells[3].Value.ToString();
			txtAddress.Text = StudentRecordDataGridView.SelectedRows[0].Cells[4].Value.ToString();
			txtMobile.Text = StudentRecordDataGridView.SelectedRows[0].Cells[5].Value.ToString();



		}

        private void button2_Click(object sender, EventArgs e)
        {
			if(StudentID > 0)
            {
				SqlCommand cmd = new SqlCommand("UPDATE StudentTable SET Name = @Name, Email = @Email, RollNumber = @RollNumber , Address = @Address , Mobile = @Mobile WHERE StudentID = @ID", con);
				cmd.CommandType = CommandType.Text;
				cmd.Parameters.AddWithValue("@Name", txtStudentName.Text);
				cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
				cmd.Parameters.AddWithValue("@RollNumber", txtRollNumber.Text);
				cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
				cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text);
				cmd.Parameters.AddWithValue("@ID", this.StudentID);

				con.Open();
				cmd.ExecuteNonQuery();
				con.Close();

				MessageBox.Show("Student Information is updated succesfully", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);

				GetStudentsRecord();
				ResetFormControls();
			}
			else
            {
				MessageBox.Show("Please select a student to update his information", "Select?", MessageBoxButtons.OK, MessageBoxIcon.Error);

			}
        }

        private void button3_Click(object sender, EventArgs e)
        {
			if(StudentID > 0)
            {
				SqlCommand cmd = new SqlCommand("DELETE FROM StudentTable WHERE StudentID = @ID", con);
				cmd.CommandType = CommandType.Text;
				cmd.Parameters.AddWithValue("@ID", this.StudentID);

				con.Open();
				cmd.ExecuteNonQuery();
				con.Close();

				MessageBox.Show("Student is deleted from the system ", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

				GetStudentsRecord();
				ResetFormControls();
			}
            else
            {
				MessageBox.Show("Please select a student to delete", "Select?", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
				
        }
    }
}
