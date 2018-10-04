//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using System.Diagnostics;

namespace SWeight
{
    class CSVParser
    {
        public static DataTable CSV2DataTable(string path)
        {
            var dt = new DataTable();
            
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
                    {
                        break;
                    }
                    Debug.WriteLine($"{parts[0]}");
                }
            }

            return dt;
        }
    }
}
