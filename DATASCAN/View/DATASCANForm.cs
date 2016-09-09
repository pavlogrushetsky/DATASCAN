using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
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
        private List<EstimatorBase> _estimators = new List<EstimatorBase>();

        private List<Customer> _customers = new List<Customer>();
        
        private List<EstimatorsGroup> _groups = new List<EstimatorsGroup>();
        
        private List<MeasurePointBase> _points = new List<MeasurePointBase>();   

        private List<ScanBase> _scans = new List<ScanBase>();

        private string _sqlConnection;  

        public DATASCANForm()
        {
            InitializeComponent();

            GetSettings();

            InitializeConnection();

            UpdateData();
            
            ContextMenuStrip estimatorsMenu = new ContextMenuStrip();
            ToolStripMenuItem addCustomerMenu = new ToolStripMenuItem("Додати замовника");
            ToolStripMenuItem addGroupMenu = new ToolStripMenuItem("Додати групу обчислювачів");
            ToolStripMenuItem addFloutecMenu = new ToolStripMenuItem("Додати обчислювач ФЛОУТЕК");
            ToolStripMenuItem addRocMenu = new ToolStripMenuItem("Додати обчислювач ROC809");

            estimatorsMenu.Items.AddRange(new ToolStripItem[]
            {
                addCustomerMenu,
                addGroupMenu,
                addFloutecMenu,
                addRocMenu
            });

            trvEstimators.ContextMenuStrip = estimatorsMenu;
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
                using (EntityRepository<EstimatorBase> repo = new EntityRepository<EstimatorBase>(_sqlConnection))
                {
                    _estimators = repo.GetAll()
                        .Include(c => c.Customer)
                        .Include(c => c.Group)
                        .Include(c => c.MeasurePoints)
                        .OrderBy(o => o.Id)
                        .ToList();
                }

                using (EntityRepository<Customer> repo = new EntityRepository<Customer>(_sqlConnection))
                {
                    _customers = repo.GetAll().OrderBy(o => o.Title).ToList();
                }

                using (EntityRepository<EstimatorsGroup> repo = new EntityRepository<EstimatorsGroup>(_sqlConnection))
                {
                    _groups = repo.GetAll().OrderBy(o => o.Name).ToList();
                }

                using (EntityRepository<MeasurePointBase> repo = new EntityRepository<MeasurePointBase>(_sqlConnection))
                {
                    _points = repo.GetAll().OrderBy(o => o.Id).ToList();
                }

                if (!_estimators.Any())
                {
                    Logger.Log(lstMessages, new LogEntry { Message = "Дані обчислювачів в базі даних відсутні", Status = LogStatus.Info, Type = LogType.System, Timestamp = DateTime.Now });
                }

                using (EntityRepository<ScanBase> repo = new EntityRepository<ScanBase>(_sqlConnection))
                {
                    _scans = repo.GetAll()
                        .Include(s => s.Members)
                        .ToList();
                }

                if (!_scans.Any())
                {
                    Logger.Log(lstMessages, new LogEntry { Message = "Дані груп опитування в базі даних відсутні", Status = LogStatus.Info, Type = LogType.System, Timestamp = DateTime.Now });
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

            _customers.ForEach(customer =>
            {
                TreeNode customerNode = trvEstimators.Nodes.Add(customer.Title);
                customerNode.Tag = customer;                               
                customerNode.ImageIndex = 0;
                customerNode.SelectedImageIndex = 0;

                ContextMenuStrip customerMenu = new ContextMenuStrip();
                ToolStripMenuItem addGroupToCustomerMenu = new ToolStripMenuItem("Додати групу обчислювачів");
                ToolStripMenuItem addFloutecToCustomerMenu = new ToolStripMenuItem("Додати обчислювач ФЛОУТЕК");
                ToolStripMenuItem addRocToCustomerMenu = new ToolStripMenuItem("Додати обчислювач ROC809");              
                ToolStripMenuItem customerInfoMenu = new ToolStripMenuItem("Інформація", Resources.Information);
                ToolStripMenuItem deactivateCustomerMenu = new ToolStripMenuItem("Деактивувати", Resources.Deactivate);
                ToolStripMenuItem activateCustomerMenu = new ToolStripMenuItem("Активувати", Resources.Activate);
                ToolStripMenuItem deleteCustomerMenu = new ToolStripMenuItem("Видалити", Resources.Delete);
                ToolStripSeparator customerSeparator1 = new ToolStripSeparator();
                ToolStripSeparator customerSeparator2 = new ToolStripSeparator();

                customerMenu.Items.AddRange(new ToolStripItem[]
                {
                    addGroupToCustomerMenu,
                    addFloutecToCustomerMenu,
                    addRocToCustomerMenu,
                    customerSeparator1,
                    customerInfoMenu,
                    customerSeparator2,
                    customer.IsActive ? deactivateCustomerMenu : activateCustomerMenu,
                    deleteCustomerMenu
                });

                customerNode.ContextMenuStrip = customerMenu;

                _groups.Where(g => g.CustomerId == customer.Id).ToList().ForEach(group =>
                {
                    TreeNode groupNode = customerNode.Nodes.Add(group.Name);
                    groupNode.Tag = group;
                    groupNode.ImageIndex = 2;
                    groupNode.SelectedImageIndex = 2;

                    ContextMenuStrip groupMenu = new ContextMenuStrip();
                    ToolStripMenuItem addFloutecToGroupMenu = new ToolStripMenuItem("Додати обчислювач ФЛОУТЕК");
                    ToolStripMenuItem addRocToGroupMenu = new ToolStripMenuItem("Додати обчислювач ROC809");
                    ToolStripMenuItem groupInfoMenu = new ToolStripMenuItem("Інформація", Resources.Information);
                    ToolStripMenuItem deactivateGroupMenu = new ToolStripMenuItem("Деактивувати", Resources.Deactivate);
                    ToolStripMenuItem activateGroupMenu = new ToolStripMenuItem("Активувати", Resources.Activate);
                    ToolStripMenuItem deleteGroupMenu = new ToolStripMenuItem("Видалити", Resources.Delete);
                    ToolStripSeparator groupSeparator1 = new ToolStripSeparator();
                    ToolStripSeparator groupSeparator2 = new ToolStripSeparator();

                    groupMenu.Items.AddRange(new ToolStripItem[]
                    {
                        addFloutecToGroupMenu,
                        addRocToGroupMenu,
                        groupSeparator1,
                        groupInfoMenu,
                        groupSeparator2,
                        group.IsActive ? deactivateGroupMenu : activateGroupMenu,
                        deleteGroupMenu
                    });

                    groupNode.ContextMenuStrip = groupMenu;

                    _estimators.Where(e => e.GroupId == group.Id).ToList().ForEach(estimator =>
                    {
                        TreeNode estimatorNode = null;
                        if (estimator is Floutec)
                        {
                            Floutec floutec = estimator as Floutec;
                            estimatorNode = groupNode.Nodes.Add($"{floutec.Name} (ФЛОУТЕК, Адреса = {floutec.Address})");
                        }
                        else if (estimator is Roc809)
                        {
                            Roc809 roc = estimator as Roc809;
                            estimatorNode = groupNode.Nodes.Add($"{roc.Name} (Адреса = {roc.Address})");
                        }

                        if (estimatorNode != null)
                        {
                            estimatorNode.Tag = estimator;
                            estimatorNode.ImageIndex = 3;
                            estimatorNode.SelectedImageIndex = 3;

                            ContextMenuStrip estimatorMenu = new ContextMenuStrip();
                            ToolStripMenuItem estimatorSettingsMenu = new ToolStripMenuItem("Налаштування", Resources.Settings);
                            ToolStripMenuItem deactivateEstimatorMenu = new ToolStripMenuItem("Деактивувати", Resources.Deactivate);
                            ToolStripMenuItem activateEstimatorMenu = new ToolStripMenuItem("Активувати", Resources.Activate);
                            ToolStripMenuItem deleteEstimatorMenu = new ToolStripMenuItem("Видалити", Resources.Delete);
                            ToolStripSeparator estimatorSeparator = new ToolStripSeparator();

                            estimatorMenu.Items.AddRange(new ToolStripItem[]
                            {
                                estimatorSettingsMenu,
                                estimatorSeparator,
                                group.IsActive ? deactivateEstimatorMenu : activateEstimatorMenu,
                                deleteEstimatorMenu
                            });

                            estimatorNode.ContextMenuStrip = estimatorMenu;
                        }

                        _points.Where(p => p.EstimatorId == estimator.Id).ToList().ForEach(point =>
                        {
                            TreeNode pointNode = null;
                            if (estimatorNode != null)
                            {
                                if (point is FloutecMeasureLine)
                                {
                                    FloutecMeasureLine floutecLine = point as FloutecMeasureLine;
                                    pointNode = estimatorNode.Nodes.Add($"{floutecLine.Number} {floutecLine.Name}");
                                }
                                else if (point is Roc809MeasurePoint)
                                {
                                    Roc809MeasurePoint rocPoint = point as Roc809MeasurePoint;
                                    pointNode = estimatorNode.Nodes.Add($"{rocPoint.Number} {rocPoint.Name} (Сегмент = {rocPoint.HistSegment})");
                                }

                                if (pointNode != null)
                                {
                                    pointNode.Tag = point;
                                    pointNode.ImageIndex = 4;
                                    pointNode.SelectedImageIndex = 4;

                                    ContextMenuStrip pointMenu = new ContextMenuStrip();
                                    ToolStripMenuItem pointSettingsMenu = new ToolStripMenuItem("Налаштування", Resources.Settings);
                                    ToolStripMenuItem deactivatePointMenu = new ToolStripMenuItem("Деактивувати", Resources.Deactivate);
                                    ToolStripMenuItem activatePointMenu = new ToolStripMenuItem("Активувати", Resources.Activate);
                                    ToolStripMenuItem deletePointMenu = new ToolStripMenuItem("Видалити", Resources.Delete);
                                    ToolStripSeparator pointSeparator = new ToolStripSeparator();

                                    pointMenu.Items.AddRange(new ToolStripItem[]
                                    {
                                        pointSettingsMenu,
                                        pointSeparator,
                                        group.IsActive ? deactivatePointMenu : activatePointMenu,
                                        deletePointMenu
                                    });

                                    pointNode.ContextMenuStrip = pointMenu;
                                }
                            }
                        });
                    });
                });
            });
        }
    }
}
