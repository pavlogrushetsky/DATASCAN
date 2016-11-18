using System;
using System.Windows.Forms;
using DATASCAN.Communication.Clients;
using DATASCAN.Communication.Common;

namespace DATASCAN.View.Forms
{
    public partial class ModemDiagnosticsForm : Form
    {
        private delegate void EventArgsDelegate(object sender, EventArgs e);

        private readonly GprsClient _gprsClient;

        public ModemDiagnosticsForm(GprsClient gprsClient)
        {
            InitializeComponent();

            _gprsClient = gprsClient;

            _gprsClient.StatusChanged += GprsClient_StatusChanged;
        }

        private void GprsClient_StatusChanged(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventArgsDelegate(GprsClient_StatusChanged), sender, e);
            }
            else
            {
                UpdateStatus(_gprsClient.Status);
            }
        }

        private void UpdateStatus(ModemLogEntry status)
        {
            if (lstModemMessages == null)
                return;

            var item = new ListViewItem(new[]
                {
                    "",
                    status.Timestamp.ToString("dd.MM.yyyy HH:mm:ss"),
                    status.Port,
                    status.Message
                });

            switch (status.Status)
            {
                case ModemStatus.CALLING:
                    item.ImageIndex = 0;
                    item.StateImageIndex = 0;
                    break;
                case ModemStatus.CONNECTED:
                    item.ImageIndex = 1;
                    item.StateImageIndex = 1;
                    break;
                case ModemStatus.DISCONNECTED:
                    item.ImageIndex = 2;
                    item.StateImageIndex = 2;
                    break;
                case ModemStatus.ENDCALL:
                    item.ImageIndex = 3;
                    item.StateImageIndex = 3;
                    break;
                case ModemStatus.ERROR:
                    item.ImageIndex = 4;
                    item.StateImageIndex = 4;
                    break;
                case ModemStatus.RECEIVE:
                    item.ImageIndex = 5;
                    item.StateImageIndex = 5;
                    break;
                case ModemStatus.SEND:
                    item.ImageIndex = 6;
                    item.StateImageIndex = 6;
                    break;
                case ModemStatus.WAIT:
                    item.ImageIndex = 7;
                    item.StateImageIndex = 7;
                    break;
                case ModemStatus.INFO:
                    break;
            }            

            lstModemMessages.BeginUpdate();
            lstModemMessages.Items.Add(item);
            lstModemMessages.Items[lstModemMessages.Items.Count - 1].EnsureVisible();
            lstModemMessages.EndUpdate();
        }

        private void mnuClear_Click(object sender, EventArgs e)
        {
            lstModemMessages.Items.Clear();
        }
    }
}
