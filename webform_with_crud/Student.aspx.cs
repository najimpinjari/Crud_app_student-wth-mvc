using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace webform_with_crud
{
    public partial class Student : System.Web.UI.Page
    {
        private string connectionstring = ConfigurationManager.ConnectionStrings["Myconnectionstring"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        protected void BtnAdd_click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Students (Name, Gender, Fees) VALUES (@Name, @Gender, @Fees)", connection))
                {
                    cmd.Parameters.AddWithValue("@Name", textname.Text);
                    cmd.Parameters.AddWithValue("@Gender", ddlgender.SelectedValue);
                    cmd.Parameters.AddWithValue("@Fees", Convert.ToInt32(textfees.Text));

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }

            BindGrid();
            ClearFields();
        }

        private void ClearFields()
        {
            textname.Text = "";
            ddlgender.SelectedIndex = 0;
            textfees.Text = "";
        }

        protected void BindGrid()
        {
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Students", connection))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        gvstudent.DataSource = dt;
                        gvstudent.DataBind();
                    }
                }
            }
        }

        protected void gvStudents_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Get the row index from the CommandArgument
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvstudent.Rows[rowIndex];

            // Retrieve the Student ID from the first cell (assuming ID is the first column)
            int studentId = Convert.ToInt32(row.Cells[0].Text);

            if (e.CommandName == "Edit")
            {
                EditStudent(studentId);
            }
            else if (e.CommandName == "Delete")
            {
                DeleteStudent(studentId);
            }
        }

        protected void EditStudent(int studentId)
        {
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Students WHERE Id = @Id", connection))
                {
                    cmd.Parameters.AddWithValue("@Id", studentId);
                    connection.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        textname.Text = reader["Name"].ToString();
                        ddlgender.SelectedValue = reader["Gender"].ToString();
                        textfees.Text = reader["Fees"].ToString();
                    }
                    connection.Close();
                }
            }
        }

        private void DeleteStudent(int studentId)
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Students WHERE Id = @Id", con))
                {
                    cmd.Parameters.AddWithValue("@Id", studentId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            BindGrid();
        }
    }
}
