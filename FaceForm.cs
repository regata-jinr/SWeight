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
using System.Configuration;
using System.Diagnostics;



namespace SWeight
{
    public partial class FaceForm : Form
    {
        private Dictionary<string, string> tabSelects = new Dictionary<string, string>();
        private Dictionary<string, string> tabButtonName = new Dictionary<string, string>();
        private Dictionary<string, DataGridView[]> tabDgvs = new Dictionary<string, DataGridView[]>();
        private SqlConnection con = new SqlConnection();

        private void InitialsSettings()
        {
            con = Connect2DB();
            tabSelects.Add("tabSamples", "select Country_Code, Client_ID, Year, Sample_Set_ID, Sample_Set_Index from NAA_DB.dbo.table_Sample_Set order by year,Sample_Set_ID, Country_Code, Client_ID,  Sample_Set_Index");
            tabSelects.Add("tabStandarts", "select SRM_Set_Name, SRM_Set_Number from NAA_DB.dbo.table_SRM_Set");
            tabSelects.Add("tabMonitors", "select Monitor_Set_Name, Monitor_Set_Number from NAA_DB.dbo.table_Monitor_Set");
            DataGridView[] dgvArray1 = new DataGridView[2];
            dgvArray1[0] = dataGridView_SamplesSet;
            dgvArray1[1] = dataGridView_Samples;
            DataGridView[] dgvArray2 = new DataGridView[2];
            dgvArray2[0] = dataGridView_StandartsSet;
            dgvArray2[1] = dataGridView_Standarts;
            DataGridView[] dgvArray3 = new DataGridView[2];
            dgvArray3[0] = dataGridView_MonitorsSet;
            dgvArray3[1] = dataGridView_Monitors;
            tabDgvs.Add("tabSamples", dgvArray1);
            tabDgvs.Add("tabStandarts", dgvArray2);
            tabDgvs.Add("tabMonitors", dgvArray3);
            tabButtonName.Add("tabSamples", "образеца");
            tabButtonName.Add("tabStandarts", "стандарта");
            tabButtonName.Add("tabMonitors", "монитора");

        }

        public FaceForm()
        {
            InitializeComponent();
            tabs.Selecting += new TabControlCancelEventHandler(tabs_Selecting);
            InitialsSettings();
            DataGridFilling(tabSelects["tabSamples"], dataGridView_SamplesSet);
        }

        private SqlConnection Connect2DB()
        {
            string connetionString = ConfigurationManager.ConnectionStrings[0].ConnectionString;
            try
            {
                SqlConnection cnn = new SqlConnection(connetionString);
                cnn.Open();
                return cnn;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Can not open connection!");
                return null;
            }
        }

        private void DataGridFilling(String select, DataGridView dgv)
        {
            if (con.State == ConnectionState.Closed)
            {
                con = Connect2DB();
            }
            var dataAdapter = new SqlDataAdapter(select, con);
            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dgv.DataSource = ds.Tables[0];
            if (dgv.RowCount == 0) return;
            dgv.CurrentCell = dgv[0, dgv.RowCount - 1];

        }

        void tabs_Selecting(object sender, TabControlCancelEventArgs e)
        {
            TabPage current = (sender as TabControl).SelectedTab;
            DataGridFilling(tabSelects[current.Name], tabDgvs[current.Name][0]);
        }



        private void dataGridView_SamplesSet_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;// get the Row Index
            DataGridViewRow selectedRow = dataGridView_SamplesSet.Rows[index];
            string select = $"select A_Sample_ID, A_Client_Sample_ID, P_Weighting_SLI, P_Weighting_LLI from NAA_DB.dbo.table_Sample where F_Country_Code = '{selectedRow.Cells[0].Value}' and F_Client_ID = '{selectedRow.Cells[1].Value}' and F_Year = '{selectedRow.Cells[2].Value}' and F_Sample_Set_ID = '{selectedRow.Cells[3].Value}' and F_Sample_Set_Index = '{selectedRow.Cells[4].Value}'";
           DataGridFilling(select, dataGridView_Samples);
            if (dataGridView_Samples.RowCount == 0) return;
            dataGridView_Samples.CurrentCell = dataGridView_Samples[0, 0];
           dataGridView_Samples.Columns[0].HeaderText = "номер образца";
           dataGridView_Samples.Columns[1].HeaderText = "клиентский номер образца";
           dataGridView_Samples.Columns[2].HeaderText = "вес, г (КЖИ)";
           dataGridView_Samples.Columns[3].HeaderText = "вес, г(ДЖИ)";
           dataGridView_Samples.Columns[0].ReadOnly = true;
           dataGridView_Samples.Columns[1].ReadOnly = true;
        }

        private void dataGridView_StandartsSet_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;// get the Row Index
            DataGridViewRow selectedRow = dataGridView_StandartsSet.Rows[index];
            string select = $"select SRM_Number, SRM_SLI_Weight, SRM_LLI_Weight from NAA_DB.dbo.table_SRM  where SRM_Set_Name = '{selectedRow.Cells[0].Value}' and SRM_Set_Number = '{selectedRow.Cells[1].Value}'";
            DataGridFilling(select, dataGridView_Standarts);
            if (dataGridView_Standarts.RowCount == 0) return;
            dataGridView_Standarts.CurrentCell = dataGridView_Standarts[0, 0];
            dataGridView_Standarts.Columns[0].HeaderText = "номер стандарта";
            dataGridView_Standarts.Columns[1].HeaderText = "вес, г (КЖИ)";
            dataGridView_Standarts.Columns[2].HeaderText = "вес, г(ДЖИ)";
            dataGridView_Standarts.Columns[0].ReadOnly = true;
        }

        private void dataGridView_MonitorsSet_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;// get the Row Index
            DataGridViewRow selectedRow = dataGridView_MonitorsSet.Rows[index];
            string select = $"select Monitor_Number, Monitor_SLI_Weight, Monitor_LLI_Weight from NAA_DB.dbo.table_Monitor where Monitor_Set_Name = '{selectedRow.Cells[0].Value}' and Monitor_Set_Number = '{selectedRow.Cells[1].Value}'";
            DataGridFilling(select, dataGridView_Monitors);
            if (dataGridView_Monitors.RowCount == 0) return;
            dataGridView_Monitors.CurrentCell = dataGridView_Monitors[0, 0];
            dataGridView_Monitors.Columns[0].HeaderText = "номер монитора";
            dataGridView_Monitors.Columns[1].HeaderText = "вес, г (КЖИ)";
            dataGridView_Monitors.Columns[2].HeaderText = "вес, г(ДЖИ)";
            dataGridView_Monitors.Columns[0].ReadOnly = true;
        }

        private void ButtonReadFromFile_Click(object sender, EventArgs e)
        {

        }

        private void buttonSave2File_Click(object sender, EventArgs e)
        {

        }

        private void buttonAddRow_Click(object sender, EventArgs e)
        {

        }

        private void buttonSave2DB_Click(object sender, EventArgs e)
        {

        }

        private void buttonSaveWeight_Click(object sender, EventArgs e)
        {

        }

    }
}
