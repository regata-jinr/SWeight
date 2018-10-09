//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.IO;
using Microsoft.VisualBasic.FileIO;
//using System.Diagnostics;
using System.Collections;

namespace SWeight
{
    class CSVParser
    {
        public static DataTable CSV2DataTable(string path)
        {
            int cnt = 0;
            var dt = new DataTable();
            DataColumn dc = new DataColumn("num", typeof(string));
            dt.Columns.Add(dc);
            dc = new DataColumn("ind", typeof(string));
            dt.Columns.Add(dc);
            dc = new DataColumn("sli,g", typeof(string));
            dt.Columns.Add(dc);
            dc = new DataColumn("lli,g", typeof(string));
            dt.Columns.Add(dc);
            if (!File.Exists(path) || Path.GetExtension(path) != ".ves")
            {
                MessageBox.Show($"Файл не существует или не правильное расширение - ({Path.GetExtension(path)})", "Error!" ,MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            //TODO: should I use here convertation to UTF (or more smart checks)
            using (TextFieldParser parser = new TextFieldParser(path, System.Text.Encoding.GetEncoding("windows-1251")))
            {
                parser.Delimiters = new string[] { "\t" };
                while (true)
                {
                    string[] parts = parser.ReadFields();
                    if (parts == null)
                        break;
                    if (parts.Length != 1)
                    {
                        if (cnt > 1) // skip header
                        {
                            DataRow dr = dt.NewRow();
                            for (var i = 0; i < 4; ++i)
                                dr[i] = parts[i];
                            dt.Rows.Add(dr);
                        }
                        cnt++;
                    }
                }
            }
            return dt;
        }

        public static void DataGridView2CSV(DataGridView dgv,ArrayList header, string path, string AddToNum="")
        {
            StreamWriter sW = new StreamWriter(path);
            string delim = "\t";
            string val = "";
            foreach (string head in header)
                sW.WriteLine(head);

            for (int row = 0; row < dgv.RowCount; ++row)
            {
                string lines = (row+1).ToString("D2");
                //we save only first three columns, so instead dgv.ColumnCount we will use just 3
                for (int col = 0; col < 3; ++col)
                {
                    //if (col == 1 && dgv.ColumnCount == 4) continue;
                    if (col == 0)
                        delim += AddToNum;
                    val = dgv.Rows[row].Cells[col].Value.ToString();
                    if (string.IsNullOrEmpty(val))
                        val = "0";
                    lines += (string.IsNullOrEmpty(lines) ? "" : delim) + val;
                    if (col == 0)
                        delim = "\t";

                }
                sW.WriteLine(lines);
            }
            sW.Close();
            MessageBox.Show("Сохранение завершено!");
        }
    }
}