using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Windows.Forms;
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

        private bool _retriesChanged;

        private bool _timeoutChanged;

        private bool _writeDelayChanged;

        private bool _readDelayChanged;

        private bool _waitingTimeChanged;

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

            cbParity.Items.AddRange(Enum.GetNames(typeof (Parity)).ToArray<object>());
            cbParity.SelectedItem = Settings.Parity;

            cbDataBits.Items.AddRange(_databits.ToArray<object>());
            cbDataBits.SelectedItem = Settings.DataBits;

            cbStopBits.Items.AddRange(Enum.GetNames(typeof(StopBits)).ToArray<object>());
            cbStopBits.SelectedItem = Settings.StopBits;

            cbBaudrate.Items.AddRange(_baudrate.ToArray<object>());
            cbBaudrate.SelectedItem = Settings.Baudrate;

            numRetries.Value = int.Parse(Settings.Retries);
            numWriteDelay.Value = int.Parse(Settings.WriteDelay);
            numReadDelay.Value = int.Parse(Settings.ReadDelay);
            numTimeout.Value = int.Parse(Settings.Timeout);
            numWaitingTime.Value = int.Parse(Settings.WaitingTime);

            btnCancel.Select();
        }

        private void btnDbfPath_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();

            var result = dialog.ShowDialog();

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
        }

        private void cbPort2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPort2.SelectedItem == null) return;
            _port2Changed = !cbPort2.SelectedItem.Equals(Settings.COMPort2);
            SetChanged();
        }

        private void cbPort3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPort3.SelectedItem == null) return;
            _port3Changed = !cbPort3.SelectedItem.Equals(Settings.COMPort3);
            SetChanged();
        }

        private void cbBaudrate_SelectedIndexChanged(object sender, EventArgs e)
        {
            _baudrateChanged = !cbBaudrate.SelectedItem.Equals(Settings.Baudrate);
            SetChanged();
        }

        private void cbParity_SelectedIndexChanged(object sender, EventArgs e)
        {
            _parityChanged = !cbParity.SelectedItem.Equals(Settings.Parity);
            SetChanged();
        }

        private void cbDataBits_SelectedIndexChanged(object sender, EventArgs e)
        {
            _dataBitsChanged = !cbDataBits.SelectedItem.Equals(Settings.DataBits);
            SetChanged();
        }

        private void cbStopBits_SelectedIndexChanged(object sender, EventArgs e)
        {
            _stopBitsChanged = !cbStopBits.SelectedItem.Equals(Settings.StopBits);
            SetChanged();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_changed)
            {
                Settings.COMPort1 = cbPort1.SelectedItem?.ToString();
                Settings.COMPort2 = cbPort2.SelectedItem?.ToString();
                Settings.COMPort3 = cbPort3.SelectedItem?.ToString();
                Settings.Baudrate = cbBaudrate.SelectedItem?.ToString();
                Settings.Parity = cbParity.SelectedItem?.ToString();
                Settings.StopBits = cbStopBits.SelectedItem?.ToString();
                Settings.DataBits = cbDataBits.SelectedItem?.ToString();
                Settings.Retries = numRetries.Value.ToString(CultureInfo.InvariantCulture);
                Settings.WriteDelay = numWriteDelay.Value.ToString(CultureInfo.InvariantCulture);
                Settings.ReadDelay = numReadDelay.Value.ToString(CultureInfo.InvariantCulture);
                Settings.Timeout = numTimeout.Value.ToString(CultureInfo.InvariantCulture);
                Settings.WaitingTime = numWaitingTime.Value.ToString(CultureInfo.InvariantCulture);
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
                       _port1Changed || _port2Changed || _port3Changed || _stopBitsChanged ||
                       _retriesChanged || _writeDelayChanged || _readDelayChanged || _timeoutChanged ||
                       _waitingTimeChanged;

            Text = _changed ? TITLE + " *" : TITLE;            
        }

        private void cbPort1_DropDown(object sender, EventArgs e)
        {
            var item = cbPort1.SelectedItem;
            cbPort1.Items.Clear();            
            cbPort1.Items.AddRange(_ports.Except(new List<object> { cbPort2.SelectedItem, cbPort3.SelectedItem }).ToArray());
            cbPort1.Items.Insert(0, "");
            cbPort1.SelectedItem = item;
        }

        private void cbPort2_DropDown(object sender, EventArgs e)
        {
            var item = cbPort2.SelectedItem;
            cbPort2.Items.Clear();
            cbPort2.Items.AddRange(_ports.Except(new List<object> { cbPort1.SelectedItem, cbPort3.SelectedItem }).ToArray());
            cbPort2.Items.Insert(0, "");
            cbPort2.SelectedItem = item;
        }

        private void cbPort3_DropDown(object sender, EventArgs e)
        {
            var item = cbPort3.SelectedItem;
            cbPort3.Items.Clear();
            cbPort3.Items.AddRange(_ports.Except(new List<object> { cbPort2.SelectedItem, cbPort1.SelectedItem }).ToArray());
            cbPort3.Items.Insert(0, "");
            cbPort3.SelectedItem = item;
        }

        private void numRetries_ValueChanged(object sender, EventArgs e)
        {
            _retriesChanged = !numRetries.Value.Equals(int.Parse(Settings.Retries));
            SetChanged();
        }

        private void numWriteDelay_ValueChanged(object sender, EventArgs e)
        {
            _writeDelayChanged = !numWriteDelay.Value.Equals(int.Parse(Settings.WriteDelay));
            SetChanged();
        }

        private void numReadDelay_ValueChanged(object sender, EventArgs e)
        {
            _readDelayChanged = !numReadDelay.Value.Equals(int.Parse(Settings.ReadDelay));
            SetChanged();
        }

        private void numTimeout_ValueChanged(object sender, EventArgs e)
        {
            _timeoutChanged = !numTimeout.Value.Equals(int.Parse(Settings.Timeout));
            SetChanged();
        }

        private void numWaitingTime_ValueChanged(object sender, EventArgs e)
        {
            _waitingTimeChanged = !numWaitingTime.Value.Equals(int.Parse(Settings.WaitingTime));
            SetChanged();
        }
    }
}
