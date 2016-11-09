using System;
using System.Windows.Forms;
using DATASCAN.Communication.Clients;

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
            gprsClient.StatusChanged += GprsClient_StatusChanged;
        }

        private void GprsClient_StatusChanged(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventArgsDelegate(GprsClient_StatusChanged), sender, e);
                return;
            }

            lstModemMessages.Items.Add(_gprsClient.Status);
        }
    }
}
