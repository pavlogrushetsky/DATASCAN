using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Windows.Forms;
using Settings = DATASCAN.Infrastructure.Settings.Settings;

// ReSharper disable UnusedParameter.Local

namespace DATASCAN.View.Forms
{
    public partial class ConnectionSettingsForm : Form
    {
        private bool _portsChanged;

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

            var addPortTooltip = new ToolTip();
            addPortTooltip.SetToolTip(btnAddPort, "Додати порт");

            var removePortTooltip = new ToolTip();
            removePortTooltip.SetToolTip(btnRemovePort, "Видалити порт");

            _ports = SerialPort.GetPortNames().ToList();            
            _ports.Sort((s1, s2) => int.Parse(s1.Substring(3, s1.Length - 3)).CompareTo(int.Parse(s2.Substring(3, s2.Length - 3))));

            Settings.COMPorts.ForEach(port =>
            {
                lstPorts.Items.Add(port, port, null);
            });
            lstPorts.ListViewItemSorter = new ListViewItemComparer();
            lstPorts.Sort();

            _ports.ForEach(port =>
            {
                if (lstPorts.Items.ContainsKey(port))
                    return;

                cbPorts.Items.Add(port);
            });
            cbPorts.Items.Insert(0, "");
            cbPorts.Enabled = cbPorts.Items.Count > 1;

            btnAddPort.Enabled = cbPorts.SelectedIndex > 0;
            btnRemovePort.Enabled = false;

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
                Settings.COMPorts.Clear();
                lstPorts.Items.OfType<ListViewItem>().ToList().ForEach(item =>
                {
                    Settings.COMPorts.Add(item.Text);
                });

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
                       _portsChanged || _stopBitsChanged || _retriesChanged || _writeDelayChanged || 
                       _readDelayChanged || _timeoutChanged || _waitingTimeChanged;

            Text = _changed ? TITLE + " *" : TITLE;            
        }

        private void cbPorts_DropDown(object sender, EventArgs e)
        {
            var item = cbPorts.SelectedItem;
            cbPorts.Items.Clear();                   
            _ports.ForEach(port =>
            {
                if (lstPorts.Items.ContainsKey(port))
                    return;

                cbPorts.Items.Add(port);
            });
            cbPorts.Items.Insert(0, "");
            cbPorts.SelectedItem = item;
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

        private void btnAddPort_Click(object sender, EventArgs e)
        {
            lstPorts.Items.Add(cbPorts.SelectedItem.ToString(), cbPorts.SelectedItem.ToString(), null);
            lstPorts.Sort();
            _portsChanged = true;
            SetChanged();

            cbPorts.Items.Clear();
            _ports.ForEach(port =>
            {
                if (lstPorts.Items.ContainsKey(port))
                    return;

                cbPorts.Items.Add(port);
            });
            cbPorts.Items.Insert(0, "");
            cbPorts.SelectedIndex = 0;

            cbPorts.Enabled = cbPorts.Items.Count > 1;
        }

        private void btnRemovePort_Click(object sender, EventArgs e)
        {
            lstPorts.Items.Remove(lstPorts.SelectedItems[0]);
            _portsChanged = true;
            SetChanged();

            cbPorts.Items.Clear();
            _ports.ForEach(port =>
            {
                if (lstPorts.Items.ContainsKey(port))
                    return;

                cbPorts.Items.Add(port);
            });
            cbPorts.Items.Insert(0, "");
            cbPorts.SelectedIndex = 0;

            cbPorts.Enabled = cbPorts.Items.Count > 1;
        }

        private void lstPorts_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRemovePort.Enabled = lstPorts.SelectedItems.Count > 0;
        }

        private void cbPorts_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnAddPort.Enabled = cbPorts.SelectedIndex > 0;
        }

        private class ListViewItemComparer : IComparer
        {
            public int Compare(object x, object y)
            {
                return
                    int.Parse(((ListViewItem) x).Text.Substring(3, ((ListViewItem) x).Text.Length - 3))
                        .CompareTo(int.Parse(((ListViewItem) y).Text.Substring(3, ((ListViewItem) y).Text.Length - 3)));
            }
        }
    }
}
