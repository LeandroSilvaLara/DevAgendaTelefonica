using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DevAgenda
{
    public partial class frmAgenda : Form
    {

        string conString = "Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=Agenda; Integrated Security=True;";

        SqlConnection sqlCon;
        SqlCommand sqlCmd;
        string Id = "";

        public frmAgenda()
        {
            InitializeComponent();
            sqlCon = new SqlConnection(conString);
            sqlCon.Open();
        }

        private void frmAgenda_Load(object sender, EventArgs e)
        {
            dgvEmp.AutoGenerateColumns = false; // dgvEmp is DataGridView name
            dgvEmp.DataSource = FetchEmpDetails();
        }

        private DataTable FetchEmpDetails()
        {
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            DataTable dtData = new DataTable();
            sqlCmd = new SqlCommand("spContato", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@ActionType", "FetchData");
            SqlDataAdapter sqlSda = new SqlDataAdapter(sqlCmd);
            sqlSda.Fill(dtData);
            return dtData;
        }

        private DataTable FetchEmpRecords(string contatoId)
        {
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            DataTable dtData = new DataTable();
            sqlCmd = new SqlCommand("spContato", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@ActionType", "FetchRecord");
            sqlCmd.Parameters.AddWithValue("@Id", contatoId);
            SqlDataAdapter sqlSda = new SqlDataAdapter(sqlCmd);
            sqlSda.Fill(dtData);
            return dtData;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxEmp.Text))
            {
                MessageBox.Show("Enter Employee Name !!!");
                textBoxEmp.Select();
            }

            else
            {
                try
                {
                    if (sqlCon.State == ConnectionState.Closed)
                    {
                        sqlCon.Open();
                    }
                    DataTable dtData = new DataTable();
                    sqlCmd = new SqlCommand("spContato", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@ActionType", "SaveData");
                    sqlCmd.Parameters.AddWithValue("@Id", Id);
                    sqlCmd.Parameters.AddWithValue("@Name", textBoxEmp.Text);
                    int numRes = sqlCmd.ExecuteNonQuery();
                    if (numRes > 0)
                    {
                        MessageBox.Show("Record Saved Successfully !!!");
                        ClearAllData();
                    }
                    else
                        MessageBox.Show("Please Try Again !!!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:- " + ex.Message);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAllData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Id))
            {
                try
                {
                    if (sqlCon.State == ConnectionState.Closed)
                    {
                        sqlCon.Open();
                    }
                    DataTable dtData = new DataTable();
                    sqlCmd = new SqlCommand("spContato", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@ActionType", "DeleteData");
                    sqlCmd.Parameters.AddWithValue("@Id", Id);
                    int numRes = sqlCmd.ExecuteNonQuery();
                    if (numRes > 0)
                    {
                        MessageBox.Show("Record Deleted Successfully !!!");
                        ClearAllData();
                    }
                    else
                    {
                        MessageBox.Show("Please Try Again !!!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:- " + ex.Message);
                }
            }
        }

        private void dgvEmp_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnSave.Text = "Update";
                Id = dgvEmp.Rows[e.RowIndex].Cells[0].Value.ToString();
                DataTable dtData = FetchEmpRecords(Id);
                if (dtData.Rows.Count > 0)
                {
                    Id = dtData.Rows[0][0].ToString();
                    textBoxEmp.Text = dtData.Rows[0][1].ToString();

                }
                else
                {
                    ClearAllData(); // For clear all control and refresh DataGridView data.
                }
            }
        }
        private void ClearAllData()
        {
            btnSave.Text = "Save";
            textBoxEmp.Text = "";
            Id = "";
            dgvEmp.AutoGenerateColumns = false;
            dgvEmp.DataSource = FetchEmpDetails();
        }

        private void btnCadastro_Click(object sender, EventArgs e)
        {
            frmTelefone tel = new frmTelefone();
            tel.ShowDialog();
        }
    }
}
