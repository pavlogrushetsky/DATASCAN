using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DATASCAN.Infrastructure.Logging;
using DATASCAN.Infrastructure.Settings;
using DATASCAN.Model;
using DATASCAN.Model.Floutecs;
using DATASCAN.Model.Rocs;
using DATASCAN.Model.Scanning;
using DATASCAN.Properties;
using DATASCAN.Repositories;
using DATASCAN.Services;
using DATASCAN.View.Forms;

namespace DATASCAN.View
{
    public partial class DATASCANForm : Form
    {
        private List<Customer> _customers = new List<Customer>();

        private List<ScanBase> _scans = new List<ScanBase>();

        private string _sqlConnection;  

        public DATASCANForm()
        {
            InitializeComponent();

            GetSettings();

            InitializeConnection();

            UpdateData();          
        }

        private void GetSettings()
        {
            try
            {
                ServerSettings.Get();
            }
            catch (Exception ex)
            {
                Logger.Log(lstMessages, new LogEntry { Message = ex.Message, Status = LogStatus.Error, Type = LogType.System, Timestamp = DateTime.Now });
            }            
        }

        private void InitializeConnection()
        {
            SqlConnectionStringBuilder connection = new SqlConnectionStringBuilder
            {
                DataSource = ServerSettings.ServerName,
                InitialCatalog = ServerSettings.DatabaseName,
                MultipleActiveResultSets = true,
                ConnectTimeout = int.Parse(ServerSettings.ConnectionTimeout)
            };

            if (string.IsNullOrEmpty(ServerSettings.UserName) || string.IsNullOrEmpty(ServerSettings.UserPassword))
            {
                connection.IntegratedSecurity = true;
            }
            else
            {
                connection.IntegratedSecurity = false;
                connection.UserID = ServerSettings.UserName;
                connection.Password = ServerSettings.UserPassword;
            }

            _sqlConnection = connection.ToString();
        }

        private async void UpdateData()
        {
            DataContextService service = new DataContextService();

            bool result = await service.TestConnection(_sqlConnection);

            if (!result)
            {
                Logger.Log(lstMessages, new LogEntry { Message = "Неможливо встановити з'єднання з сервером баз даних. Перевірте налаштування з'єднання", Status = LogStatus.Error, Type = LogType.System, Timestamp = DateTime.Now });
                return;
            }

            try
            {
                using (EntityRepository<Customer> repo = new EntityRepository<Customer>(_sqlConnection))
                {
                    _customers = repo.GetAll()
                        .Include(c => c.Estimators)
                        .Include(c => c.Estimators.Select(e => e.MeasurePoints))
                        .ToList();
                }

                if (!_customers.Any())
                {
                    Logger.Log(lstMessages, new LogEntry { Message = "Дані в базі даних відсутні", Status = LogStatus.Info, Type = LogType.System, Timestamp = DateTime.Now });
                }               

                using (EntityRepository<ScanBase> repo = new EntityRepository<ScanBase>(_sqlConnection))
                {
                    _scans = repo.GetAll()
                        .Include(s => s.Members)
                        .ToList();
                }

                if (!_customers.Any())
                {
                    Logger.Log(lstMessages, new LogEntry { Message = "Групи опитування відсутні", Status = LogStatus.Info, Type = LogType.System, Timestamp = DateTime.Now });
                }

                FillEstimatorsTree();
            }
            catch (Exception ex)
            {
                Logger.Log(lstMessages, new LogEntry { Message = ex.Message, Status = LogStatus.Error, Type = LogType.System, Timestamp = DateTime.Now });
            }
        }

        private void mnuDatabase_Click(object sender, EventArgs e)
        {
            ServerSettingsForm form = new ServerSettingsForm { StartPosition = FormStartPosition.CenterParent };

            DialogResult result = form.ShowDialog();

            if (result == DialogResult.OK)
            {
                Logger.Log(lstMessages, new LogEntry { Message = "Налаштування сервера баз даних були змінені", Status = LogStatus.Info, Type = LogType.System, Timestamp = DateTime.Now });

                InitializeConnection();
                UpdateData();
            }
        }

        private void FillEstimatorsTree()
        {
            trvEstimators.Nodes.Clear();

            if (!_customers.Any())
            {
                return;
            }

            _customers.ForEach(c =>
            {
                TreeNode customerNode = trvEstimators.Nodes.Add(c.Title);
                customerNode.Tag = c;
                customerNode.ImageIndex = c.IsActive ? 0 : 1;
                customerNode.SelectedImageIndex = c.IsActive ? 0 : 1;
                customerNode.ForeColor = c.IsActive ? Color.Black : Color.DarkGray;
                
                ContextMenuStrip customerMenu = new ContextMenuStrip();
                ToolStripMenuItem addFloutecItem = new ToolStripMenuItem("Додати обчислювач ФЛОУТЕК");
                ToolStripMenuItem addRocItem = new ToolStripMenuItem("Додати обчислювач ROC809");
                ToolStripSeparator separatorItem = new ToolStripSeparator();
                ToolStripMenuItem infoCustomerItem = new ToolStripMenuItem("Інформація", Resources.Information);
                ToolStripMenuItem deactivateCustomerItem = new ToolStripMenuItem("Деактивувати", Resources.Deactivate);
                ToolStripMenuItem activateCustomerItem = new ToolStripMenuItem("Активувати", Resources.Activate);
                ToolStripMenuItem deleteCustomerItem = new ToolStripMenuItem("Видалити", Resources.Delete);

                customerMenu.Items.AddRange(new ToolStripItem[]
                {
                    addFloutecItem,
                    addRocItem,
                    separatorItem,
                    infoCustomerItem,
                    c.IsActive ? deactivateCustomerItem : activateCustomerItem,
                    deleteCustomerItem
                });

                customerNode.ContextMenuStrip = customerMenu;

                if (c.Estimators.Any(e => e is Floutec))
                {
                    TreeNode floutecsGroupNode = customerNode.Nodes.Add("FloutecsGroup", "Обчислювачі ФЛОУТЕК");
                    floutecsGroupNode.ImageIndex = 2;
                    floutecsGroupNode.SelectedImageIndex = 2;
                }

                if (c.Estimators.Any(e => e is Roc809))
                {
                    TreeNode rocsGroupNode = customerNode.Nodes.Add("RocsGroup", "Обчислювачі ROC809");
                    rocsGroupNode.ImageIndex = 2;
                    rocsGroupNode.SelectedImageIndex = 2;
                }
            });
        }
    }
}
