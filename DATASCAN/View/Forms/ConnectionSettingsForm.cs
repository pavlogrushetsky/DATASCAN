using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DATASCAN.Communication.Serial;
using static System.String;
using Settings = DATASCAN.Infrastructure.Settings.Settings;

// ReSharper disable UnusedParameter.Local

namespace DATASCAN.View.Forms
{
    public partial class ConnectionSettingsForm : Form
    {
        private bool _port1Changed;

        private bool _port2Changed;

        private bool _port3Changed;

        private bool _baudrateChanged;

        private bool _parityChanged;

        private bool _dataBitsChanged;

        private bool _stopBitsChanged;

        private bool _dbfPathChanged;

        private bool _changed;

        private readonly string[] _baudrate = 
        {
            "1200", "1800", "2400", "4800", "7200", "9600",
            "14400", "19200", "38400", "57600", "115200", "128000"
        };

        private readonly string[] _databits =
        {
            "4", "5", "6", "7", "8"
        };

        private const string TITLE = "Налаштування підключення";

        private readonly List<string> _ports;

        public ConnectionSettingsForm()
        {
            InitializeComponent();

            txtDbfPath.Text = Settings.DbfPath;

            _ports = SerialPort.GetPortNames().ToList();
            if (!IsNullOrEmpty(Settings.COMPort1) && !_ports.Contains(Settings.COMPort1))
                _ports.Add(Settings.COMPort1);
            if (!IsNullOrEmpty(Settings.COMPort2) && !_ports.Contains(Settings.COMPort2))
                _ports.Add(Settings.COMPort2);
            if (!IsNullOrEmpty(Settings.COMPort3) && !_ports.Contains(Settings.COMPort3))
                _ports.Add(Settings.COMPort3);
            _ports.Sort((s1, s2) => int.Parse(s1.Substring(3, s1.Length - 3)).CompareTo(int.Parse(s2.Substring(3, s2.Length - 3))));
            _ports.ToList().Insert(0, "");

            cbPort1.Items.AddRange(_ports.ToArray<object>());
            cbPort1.SelectedItem = Settings.COMPort1;
            cbPort1.Enabled = cbPort1.Items.Count > 0;

            cbPort2.Items.AddRange(_ports.Except(new List<string> { Settings.COMPort1 }).ToArray<object>());
            cbPort2.SelectedItem = Settings.COMPort2;
            cbPort2.Enabled = cbPort2.Items.Count > 0;

            cbPort3.Items.AddRange(_ports.Except(new List<string> { Settings.COMPort2 }).ToArray<object>());
            cbPort3.SelectedItem = Settings.COMPort3;
            cbPort3.Enabled = cbPort3.Items.Count > 0;

            EnableTestButton();

            cbParity.Items.AddRange(Enum.GetNames(typeof (Parity)).ToArray<object>());
            cbParity.SelectedItem = Settings.Parity;

            cbDataBits.Items.AddRange(_databits.ToArray<object>());
            cbDataBits.SelectedItem = Settings.DataBits;

            cbStopBits.Items.AddRange(Enum.GetNames(typeof(StopBits)).ToArray<object>());
            cbStopBits.SelectedItem = Settings.StopBits;

            cbBaudrate.Items.AddRange(_baudrate.ToArray<object>());
            cbBaudrate.SelectedItem = Settings.Baudrate;

            btnCancel.Select();
        }

        private void btnDbfPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();

            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                txtDbfPath.Text = dialog.SelectedPath ?? "";
            }
        }

        private void txtDbfPath_TextChanged(object sender, EventArgs e)
        {
            _dbfPathChanged = !txtDbfPath.Text.Equals(Settings.DbfPath);
            SetChanged();
        }

        private void cbPort1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPort1.SelectedItem != null)
            {
                _port1Changed = !cbPort1.SelectedItem.Equals(Settings.COMPort1);
                SetChanged();
            }

            EnableTestButton();

            statusPort1.SetError(cbPort1, "");
        }

        private void cbPort2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPort2.SelectedItem != null)
            {
                _port2Changed = !cbPort2.SelectedItem.Equals(Settings.COMPort2);
                SetChanged();
            }

            EnableTestButton();

            statusPort2.SetError(cbPort2, "");
        }

        private void cbPort3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPort3.SelectedItem != null)
            {
                _port3Changed = !cbPort3.SelectedItem.Equals(Settings.COMPort3);
                SetChanged();
            }

            EnableTestButton();

            statusPort3.SetError(cbPort3, "");
        }

        private void cbBaudrate_SelectedIndexChanged(object sender, EventArgs e)
        {
            _baudrateChanged = !cbBaudrate.SelectedItem.Equals(Settings.Baudrate);
            SetChanged();
            ResetPortsStatuses();
        }

        private void cbParity_SelectedIndexChanged(object sender, EventArgs e)
        {
            _parityChanged = !cbParity.SelectedItem.Equals(Settings.Parity);
            SetChanged();
            ResetPortsStatuses();
        }

        private void cbDataBits_SelectedIndexChanged(object sender, EventArgs e)
        {
            _dataBitsChanged = !cbDataBits.SelectedItem.Equals(Settings.DataBits);
            SetChanged();
            ResetPortsStatuses();
        }

        private void cbStopBits_SelectedIndexChanged(object sender, EventArgs e)
        {
            _stopBitsChanged = !cbStopBits.SelectedItem.Equals(Settings.StopBits);
            SetChanged();
            ResetPortsStatuses();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_changed)
            {
                Settings.COMPort1 = cbPort1.SelectedItem.ToString();
                Settings.COMPort2 = cbPort2.SelectedItem.ToString();
                Settings.COMPort3 = cbPort3.SelectedItem.ToString();
                Settings.Baudrate = cbBaudrate.SelectedItem.ToString();
                Settings.Parity = cbParity.SelectedItem.ToString();
                Settings.StopBits = cbStopBits.SelectedItem.ToString();
                Settings.DataBits = cbDataBits.SelectedItem.ToString();
                Settings.DbfPath = txtDbfPath.Text;

                Settings.Save();

                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void SetChanged()
        {
            _changed = _dbfPathChanged || _baudrateChanged || _dataBitsChanged || _parityChanged || 
                       _port1Changed || _port2Changed || _port3Changed || _stopBitsChanged;

            Text = _changed ? TITLE + " *" : TITLE;            
        }

        private void EnableTestButton()
        {
            btnTestConnection.Enabled = cbPort1.SelectedIndex > 0 ||
                                        cbPort2.SelectedIndex > 0 ||
                                        cbPort3.SelectedIndex > 0;
        }

        private void cbPort1_DropDown(object sender, EventArgs e)
        {
            object item = cbPort1.SelectedItem;
            cbPort1.Items.Clear();            
            cbPort1.Items.AddRange(_ports.Except(new List<object> { cbPort2.SelectedItem, cbPort3.SelectedItem }).ToArray());
            cbPort1.Items.Insert(0, "");
            cbPort1.SelectedItem = item;
        }

        private void cbPort2_DropDown(object sender, EventArgs e)
        {
            object item = cbPort2.SelectedItem;
            cbPort2.Items.Clear();
            cbPort2.Items.AddRange(_ports.Except(new List<object> { cbPort1.SelectedItem, cbPort3.SelectedItem }).ToArray());
            cbPort2.Items.Insert(0, "");
            cbPort2.SelectedItem = item;
        }

        private void cbPort3_DropDown(object sender, EventArgs e)
        {
            object item = cbPort3.SelectedItem;
            cbPort3.Items.Clear();
            cbPort3.Items.AddRange(_ports.Except(new List<object> { cbPort2.SelectedItem, cbPort1.SelectedItem }).ToArray());
            cbPort3.Items.Insert(0, "");
            cbPort3.SelectedItem = item;
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            ResetPortsStatuses();
            string port1 = cbPort1.SelectedItem.ToString();
            string port2 = cbPort2.SelectedItem.ToString();
            string port3 = cbPort3.SelectedItem.ToString();

            if (!IsNullOrEmpty(port1))
                TestConnection(port1, statusPort1, cbPort1);

            if (!IsNullOrEmpty(port2))
                TestConnection(port2, statusPort2, cbPort2);

            if (!IsNullOrEmpty(port3))
                TestConnection(port3, statusPort3, cbPort3);
        }

        private void TestConnection(string port, ErrorProvider stat, ComboBox cb)
        {
            try
            {
                SerialPortFixer.Execute(port);
                using (SerialPort serialPort = new SerialPort
                {
                    PortName = port,
                    BaudRate = int.Parse(cbBaudrate.SelectedItem.ToString()),
                    DataBits = int.Parse(cbDataBits.SelectedItem.ToString()),
                    StopBits = (StopBits) Enum.Parse(typeof (StopBits), (string) cbStopBits.SelectedItem),
                    Parity = (Parity) Enum.Parse(typeof (Parity), (string) cbParity.SelectedItem),
                    Handshake = Handshake.None,
                    ReadTimeout = 500,
                    WriteTimeout = 500                    
                })
                {
                    serialPort.Open();
                    serialPort.WriteLine(@"ATQ0V1E0" + "\r\n");
                    Thread.Sleep(500);
                    string result = serialPort.ReadExisting();
                    if (!result.Contains("OK"))
                    {
                        stat.SetError(cb, "Помилка встановлення з'єднання");
                    }
                }
            }
            catch (Exception)
            {
                stat.SetError(cb, "Помилка встановлення з'єднання");
            }
        }

        private void ResetPortsStatuses()
        {
            statusPort1.SetError(cbPort1, "");
            statusPort2.SetError(cbPort2, "");
            statusPort3.SetError(cbPort3, "");
        }
    }
}
