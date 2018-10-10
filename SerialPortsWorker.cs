﻿using System;
using System.Globalization;
using System.Windows.Forms;
using System.IO.Ports;
using System.Diagnostics;
using System.Management;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWeight
{
    class SerialPortsWorker
    {
        private SerialPort port;
        private double weight;

        public SerialPortsWorker()
        {
            try
            {
                string com = FindScales();
                if (com.Equals(""))
                {
                    MessageBox.Show("The scales are not found! Please Check the list of available devices.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                port = new SerialPort(com, 9600, Parity.None, 8, StopBits.One);
                port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
                port.Open();
                //todo: I'm not sure that it's a good ide to use pause here. I should find out how to get only one line form one call.
                System.Threading.Thread.Sleep(1000);
            }
            catch (UnauthorizedAccessException)
            {MessageBox.Show("The scales in the sleep mode or we be not able to connect to it. Try to enable it.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);}
            catch (Exception ex)
            {MessageBox.Show($"Exception has occurred in process of getting the data from scales:\n {ex.ToString()}", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);}
        }

        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
               Match match = Regex.Match(port.ReadLine(), "^.*([0-9]+\\.[0-9]+).*$");
            if (match.Success)
                weight = Convert.ToDouble(match.Groups[1].Value, CultureInfo.InvariantCulture);
            Debug.WriteLine($"Reading weight is {weight}");
            port.Close();
            return;

        }

        public double GetWeight() {return weight;}

        private string FindScales()
        {
            Debug.WriteLine("Port info:");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity where DeviceID  like '%DN02GDZ6A%' ");
            ManagementObject scales = searcher.Get().OfType<ManagementObject>().FirstOrDefault();
            Debug.WriteLine($"Name of weight - {scales["Name"]}");
            if (scales == null) return "";
            return Regex.Match(scales["Name"].ToString(), @"\(([^)]*)\)").Groups[1].Value;
        }
    }
}
