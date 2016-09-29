using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DATASCAN.Infrastructure.Logging;
using DATASCAN.Infrastructure.Settings;
using DATASCAN.Model;
using DATASCAN.Model.Common;
using DATASCAN.Model.Floutecs;
using DATASCAN.Model.Rocs;
using DATASCAN.Model.Scanning;
using DATASCAN.Properties;
using DATASCAN.Services;
using DATASCAN.View.Extensions;
using DATASCAN.View.Forms;

namespace DATASCAN.View
{
    public partial class DATASCANForm : Form
    {
        #region Services

        private readonly DataContextService _contextService;
        private readonly EntitiesService<EntityBase> _entitiesService; 
        private readonly EstimatorsService _estimatorsService;
        private readonly CustomersService _customersService;
        private readonly GroupsService _groupsService;
        private readonly MeasurePointsService _pointsService;
        private readonly EntitiesService<ScanBase> _scansService;

        #endregion

        #region Entities collections

        private List<EstimatorBase> _estimators = new List<EstimatorBase>();        
        private List<Customer> _customers = new List<Customer>();               
        private List<EstimatorsGroup> _groups = new List<EstimatorsGroup>();                
        private List<MeasurePointBase> _points = new List<MeasurePointBase>();        
        private List<ScanBase> _scans = new List<ScanBase>();

        #endregion

        #region Fields & constructor

        private string _sqlConnection;  

        public DATASCANForm()
        {
            InitializeComponent();            

            GetSettings();

            InitializeConnection();

            _contextService = new DataContextService(_sqlConnection);
            _entitiesService = new EntitiesService<EntityBase>(_sqlConnection);
            _estimatorsService = new EstimatorsService(_sqlConnection);
            _customersService = new CustomersService(_sqlConnection);
            _groupsService = new GroupsService(_sqlConnection);
            _pointsService = new MeasurePointsService(_sqlConnection);
            _scansService = new EntitiesService<ScanBase>(_sqlConnection);

            UpdateData().ConfigureAwait(false);

            ContextMenuStrip estimatorsMenu = new ContextMenuStrip();

            estimatorsMenu.Items.AddRange(new ToolStripItem[]
            {
                new ToolStripMenuItem(Resources.AddCustomerMsg, null, EditCustomerMenu_Click),
                new ToolStripMenuItem(Resources.AddEstimatorsGroupMsg, null, EditGroupMenu_Click),
                new ToolStripMenuItem(Resources.AddFloutecMsg, null, EditFloutecMenu_Click),
                new ToolStripMenuItem(Resources.AddRocMsg, null, EditRocMenu_Click),
                new ToolStripSeparator(), 
                new ToolStripMenuItem(Resources.UpdateMsg, Resources.Refresh, RefreshMenu_Click)
            });

            estimatorsMenu.Opening += ContextMenu_Opening;

            trvEstimators.ContextMenuStrip = estimatorsMenu;

            trvEstimators.AllowDrop = true;
            trvEstimators.ItemDrag += TrvEstimators_ItemDrag;
            trvEstimators.DragEnter += TrvEstimators_DragEnter;
            trvEstimators.DragDrop += TrvEstimators_DragDrop;
        }

        #endregion

        #region Setup

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

        private async Task UpdateData()
        {
            bool connected = await _contextService.TestConnection(ex =>
            {
                Logger.Log(lstMessages, new LogEntry { Message = ex.Message, Status = LogStatus.Error, Type = LogType.System, Timestamp = DateTime.Now });
            });

            if (connected)
            {
                _estimators = await _estimatorsService.GetAll(null, ex =>
                {
                    Logger.Log(lstMessages, new LogEntry { Message = ex.Message, Status = LogStatus.Error, Type = LogType.System, Timestamp = DateTime.Now });
                }, e => e.OrderBy(o => o.Id), e => e.Customer, e => e.Group, e => e.MeasurePoints);

                _customers = await _customersService.GetAll(null, ex =>
                {
                    Logger.Log(lstMessages, new LogEntry { Message = ex.Message, Status = LogStatus.Error, Type = LogType.System, Timestamp = DateTime.Now });
                }, c => c.OrderBy(o => o.Title));

                _groups = await _groupsService.GetAll(null, ex =>
                {
                    Logger.Log(lstMessages, new LogEntry { Message = ex.Message, Status = LogStatus.Error, Type = LogType.System, Timestamp = DateTime.Now });
                }, g => g.OrderBy(o => o.Name));

                _points = await _pointsService.GetAll(null, ex =>
                {
                    Logger.Log(lstMessages, new LogEntry { Message = ex.Message, Status = LogStatus.Error, Type = LogType.System, Timestamp = DateTime.Now });
                }, p => p.OrderBy(o => o.Id));

                _scans = await _scansService.GetAll(null, ex =>
                {
                    Logger.Log(lstMessages, new LogEntry { Message = ex.Message, Status = LogStatus.Error, Type = LogType.System, Timestamp = DateTime.Now });
                }, null, s => s.Members);

                FillEstimatorsTree();
            }
            else
            {
                trvEstimators.Nodes.Clear();                
            }

            trvEstimators.ContextMenuStrip.Items[0].Enabled = connected;
            trvEstimators.ContextMenuStrip.Items[1].Enabled = connected;
            trvEstimators.ContextMenuStrip.Items[2].Enabled = connected;
            trvEstimators.ContextMenuStrip.Items[3].Enabled = connected;
        }

        #endregion

        #region Estimators tree filling

        private void FillEstimatorsTree()
        {
            var savedExpansionState = trvEstimators.Nodes.GetExpansionState();

            trvEstimators.BeginUpdate();

            trvEstimators.Nodes.Clear();

            FillCustomers();

            FillGroups();

            FillEstimators();

            trvEstimators.Nodes.SetExpansionState(savedExpansionState);

            trvEstimators.EndUpdate();                     
        }

        private void FillCustomers()
        {
            _customers.ForEach(customer =>
            {
                TreeNode customerNode = trvEstimators.Nodes.Add(customer.Title);
                customerNode.ForeColor = customer.IsActive ? Color.Black : Color.Red;
                customerNode.Tag = customer;
                customerNode.ImageIndex = 0;
                customerNode.SelectedImageIndex = 0;

                ContextMenuStrip customerMenu = new ContextMenuStrip();

                customerMenu.Items.AddRange(new ToolStripItem[]
                {
                    new ToolStripMenuItem(Resources.AddEstimatorsGroupMsg, null, EditGroupMenu_Click),
                    new ToolStripMenuItem(Resources.AddFloutecMsg, null, EditFloutecMenu_Click),
                    new ToolStripMenuItem(Resources.AddRocMsg, null, EditRocMenu_Click),
                    new ToolStripSeparator(),
                    new ToolStripMenuItem(Resources.InformationMsg, Resources.Information, EditCustomerMenu_Click),
                    new ToolStripSeparator(), 
                    customer.IsActive ? new ToolStripMenuItem(Resources.DeactivateMsg, Resources.Deactivate, DeactivateMenu_Click) : new ToolStripMenuItem(Resources.ActivateMsg, Resources.Activate, ActivateMenu_Click),
                    new ToolStripMenuItem(Resources.DeleteMsg, Resources.Delete, DeleteCustomerMenu_Click)
                });

                customerMenu.Opening += ContextMenu_Opening;

                customerNode.ContextMenuStrip = customerMenu;

                FillGroups(customerNode);
                FillEstimators(customerNode);
            });
        }       

        private void FillGroups(TreeNode customerNode = null)
        {
            List<EstimatorsGroup> groups;
            Customer customer;

            if (customerNode == null)
            {
                groups = _groups.Where(g => !g.CustomerId.HasValue).ToList();
            }
            else
            {
                customer = customerNode.Tag as Customer;
                groups = _groups.Where(g => customer != null && g.CustomerId == customer.Id).ToList();
            }

            groups.ForEach(group =>
            {
                TreeNode groupNode = customerNode?.Nodes.Add(group.Name) ?? trvEstimators.Nodes.Add(group.Name);

                groupNode.ForeColor = group.IsActive ? Color.Black : Color.Red;
                groupNode.Tag = group;
                groupNode.ImageIndex = 2;
                groupNode.SelectedImageIndex = 2;

                ContextMenuStrip groupMenu = new ContextMenuStrip();

                groupMenu.Items.AddRange(new ToolStripItem[]
                {
                    new ToolStripMenuItem(Resources.AddFloutecMsg, null, EditFloutecMenu_Click),
                    new ToolStripMenuItem(Resources.AddRocMsg, null, EditRocMenu_Click),
                    new ToolStripSeparator(),
                    new ToolStripMenuItem(Resources.InformationMsg, Resources.Information, EditGroupMenu_Click),
                    new ToolStripSeparator(),
                    group.IsActive ? new ToolStripMenuItem(Resources.DeactivateMsg, Resources.Deactivate, DeactivateMenu_Click) : new ToolStripMenuItem(Resources.ActivateMsg, Resources.Activate, ActivateMenu_Click),
                    new ToolStripMenuItem(Resources.DeleteMsg, Resources.Delete, DeleteGroupMenu_Click)
                });

                groupMenu.Opening += ContextMenu_Opening;

                groupNode.ContextMenuStrip = groupMenu;

                FillEstimators(groupNode);
            });
        }

        private void FillEstimators(TreeNode parentNode = null)
        {
            List<EstimatorBase> estimators = new List<EstimatorBase>();

            if (parentNode != null)
            {
                if (parentNode.Tag is EstimatorsGroup)
                {
                    EstimatorsGroup group = parentNode.Tag as EstimatorsGroup;
                    estimators = _estimators.Where(e => e.GroupId == group.Id).ToList();
                }
                else if (parentNode.Tag is Customer)
                {
                    Customer customer = parentNode.Tag as Customer;
                    estimators = _estimators.Where(e => e.CustomerId == customer.Id && !e.GroupId.HasValue).ToList();
                }
            }
            else
            {
                estimators = _estimators.Where(e => !e.GroupId.HasValue && !e.CustomerId.HasValue).ToList();
            }

            estimators.ForEach(estimator =>
            {
                Floutec floutec = estimator as Floutec;
                Roc809 roc = estimator as Roc809;

                string nodeTitle = string.Empty;

                if (floutec != null)
                {
                    nodeTitle = $"{floutec.Name} (ФЛОУТЕК, Адреса = {floutec.Address})";
                }
                else if (roc != null)
                {
                    nodeTitle = $"{roc.Name} (ROC, Адреса = {roc.Address})";
                }

                TreeNode estimatorNode = parentNode?.Nodes.Add(nodeTitle) ?? trvEstimators.Nodes.Add(nodeTitle);

                estimatorNode.ForeColor = estimator.IsActive ? Color.Black : Color.Red;
                estimatorNode.Tag = estimator;
                estimatorNode.ImageIndex = 3;
                estimatorNode.SelectedImageIndex = 3;

                ContextMenuStrip estimatorMenu = new ContextMenuStrip();

                estimatorMenu.Items.AddRange(new ToolStripItem[]
                {
                    estimator is Floutec ? new ToolStripMenuItem(Resources.AddMeasureLineMsg, null, EditLineMenu_Click) : new ToolStripMenuItem(Resources.AddMeasurePointMsg, null, EditPointMenu_Click),
                    new ToolStripSeparator(),
                    estimator is Floutec ? new ToolStripMenuItem(Resources.SettingsMsg, Resources.Settings, EditFloutecMenu_Click) : new ToolStripMenuItem(Resources.SettingsMsg, Resources.Settings, EditRocMenu_Click),
                    new ToolStripSeparator(),
                    estimator.IsActive ? new ToolStripMenuItem(Resources.DeactivateMsg, Resources.Deactivate, DeactivateMenu_Click) : new ToolStripMenuItem(Resources.ActivateMsg, Resources.Activate, ActivateMenu_Click),
                    new ToolStripMenuItem(Resources.DeleteMsg, Resources.Delete, DeleteEstimatorMenu_Click)
                });

                estimatorMenu.Opening += ContextMenu_Opening;

                estimatorNode.ContextMenuStrip = estimatorMenu;

                FillPoints(estimatorNode);
            });
        }

        private void FillPoints(TreeNode estimatorNode)
        {
            if (estimatorNode != null && estimatorNode.Tag is EstimatorBase)
            {
                EstimatorBase estimator = estimatorNode.Tag as EstimatorBase;

                _points.Where(p => p.EstimatorId == estimator.Id).ToList().ForEach(point =>
                {
                    TreeNode pointNode = null;

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
                        pointNode.ForeColor = point.IsActive ? Color.Black : Color.Red;
                        pointNode.Tag = point;
                        pointNode.ImageIndex = 4;
                        pointNode.SelectedImageIndex = 4;

                        ContextMenuStrip pointMenu = new ContextMenuStrip();

                        pointMenu.Items.AddRange(new ToolStripItem[]
                        {
                            estimator is Floutec ? new ToolStripMenuItem(Resources.SettingsMsg, Resources.Settings, EditLineMenu_Click) : new ToolStripMenuItem(Resources.SettingsMsg, Resources.Settings, EditPointMenu_Click),
                            new ToolStripSeparator(),
                            point.IsActive ? new ToolStripMenuItem(Resources.DeactivateMsg, Resources.Deactivate, DeactivateMenu_Click) : new ToolStripMenuItem(Resources.ActivateMsg, Resources.Activate, ActivateMenu_Click),
                            new ToolStripMenuItem(Resources.DeleteMsg, Resources.Delete, DeletePointMenu_Click)
                        });

                        pointMenu.Opening += ContextMenu_Opening;

                        pointNode.ContextMenuStrip = pointMenu;
                    }
                });
            }            
        }

        #endregion

        private void ContextMenu_Opening(object sender, CancelEventArgs e)
        {
            TreeNode nodeAtMousePosition = trvEstimators.GetNodeAt(trvEstimators.PointToClient(MousePosition));

            TreeNode selectedNode = trvEstimators.SelectedNode;

            if (nodeAtMousePosition != null)
            {
                if (nodeAtMousePosition != selectedNode)
                    trvEstimators.SelectedNode = nodeAtMousePosition;
            }
            else
            {
                trvEstimators.SelectedNode = null;
            }
        }

        private async void mnuDatabase_Click(object sender, EventArgs e)
        {
            ServerSettingsForm form = new ServerSettingsForm { StartPosition = FormStartPosition.CenterParent };

            DialogResult result = form.ShowDialog();

            if (result == DialogResult.OK)
            {
                Logger.Log(lstMessages, new LogEntry { Message = "Налаштування сервера баз даних змінено", Status = LogStatus.Info, Type = LogType.System, Timestamp = DateTime.Now });

                InitializeConnection();
                await UpdateData();
            }
        }

        private async void EditCustomerMenu_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;

            TreeNode node = trvEstimators.SelectedNode;

            Customer customer = node?.Tag as Customer;

            EditCustomerForm form = new EditCustomerForm
            {
                StartPosition = FormStartPosition.CenterParent,
                IsEdit = menuItem != null && menuItem.Text.Equals(Resources.InformationMsg),
                Customer = customer
            };

            DialogResult result = form.ShowDialog();

            if (result == DialogResult.OK && form.Customer != null)
            {
                customer = form.Customer;

                if (form.IsEdit)
                {                        
                    await _customersService.Update(customer, () =>
                    {
                        Logger.Log(lstMessages, new LogEntry { Message = $"Дані замовника змінено: {customer}", Status = LogStatus.Info, Type = LogType.System, Timestamp = DateTime.Now });
                    }, ex =>
                    {
                        Logger.Log(lstMessages, new LogEntry { Message = ex.Message, Status = LogStatus.Error, Type = LogType.System, Timestamp = DateTime.Now });
                    });
                }
                else
                {
                    await _customersService.Insert(customer, () =>
                    {
                        Logger.Log(lstMessages, new LogEntry { Message = $"Додано замовника: {customer}", Status = LogStatus.Info, Type = LogType.System, Timestamp = DateTime.Now });
                    }, ex =>
                    {
                        Logger.Log(lstMessages, new LogEntry { Message = ex.Message, Status = LogStatus.Error, Type = LogType.System, Timestamp = DateTime.Now });
                    });
                }

                await UpdateData();
            }
        }

        private async void EditGroupMenu_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;

            TreeNode node = trvEstimators.SelectedNode;

            Customer customer = node?.Tag as Customer;

            EstimatorsGroup group = node?.Tag as EstimatorsGroup;

            EditEstimatorsGroupForm form = new EditEstimatorsGroupForm
            {
                StartPosition = FormStartPosition.CenterParent,
                IsEdit = menuItem != null && menuItem.Text.Equals(Resources.InformationMsg),
                Group = group
            };

            DialogResult result = form.ShowDialog();

            if (result == DialogResult.OK && form.Group != null)
            {
                group = form.Group;

                if (form.IsEdit)
                {
                    await _groupsService.Update(group, () =>
                    {
                        Logger.Log(lstMessages, new LogEntry { Message = $"Дані групи обчислювачів змінено: {group}", Status = LogStatus.Info, Type = LogType.System, Timestamp = DateTime.Now });
                    }, ex =>
                    {
                        Logger.Log(lstMessages, new LogEntry { Message = ex.Message, Status = LogStatus.Error, Type = LogType.System, Timestamp = DateTime.Now });
                    });
                }
                else
                {
                    if (customer != null)
                    {
                        group.CustomerId = customer.Id;                            
                    }

                    await _groupsService.Insert(group, () =>
                    {
                        Logger.Log(lstMessages, new LogEntry { Message = $"Додано групу обчислювачів: {group}", Status = LogStatus.Info, Type = LogType.System, Timestamp = DateTime.Now });
                    }, ex =>
                    {
                        Logger.Log(lstMessages, new LogEntry { Message = ex.Message, Status = LogStatus.Error, Type = LogType.System, Timestamp = DateTime.Now });
                    });
                }

                await UpdateData();
            }
        }

        private async void RefreshMenu_Click(object sender, EventArgs e)
        {
            await UpdateData();
        }

        private async void EditFloutecMenu_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;

            TreeNode node = trvEstimators.SelectedNode;

            Customer customer = node?.Tag as Customer;

            EstimatorsGroup group = node?.Tag as EstimatorsGroup;

            Floutec floutec = node?.Tag as Floutec;

            EditFloutecForm form = new EditFloutecForm
            {
                StartPosition = FormStartPosition.CenterParent,
                IsEdit = menuItem != null && menuItem.Text.Equals(Resources.SettingsMsg),
                Floutec = floutec
            };

            DialogResult result = form.ShowDialog();

            if (result == DialogResult.OK && form.Floutec != null)
            {
                floutec = form.Floutec;

                if (form.IsEdit)
                {
                    await _estimatorsService.Update(floutec, () =>
                    {
                        Logger.Log(lstMessages, new LogEntry { Message = $"Дані обчислювача ФЛОУТЕК змінено: {floutec}", Status = LogStatus.Info, Type = LogType.System, Timestamp = DateTime.Now });
                    }, ex =>
                    {
                        Logger.Log(lstMessages, new LogEntry { Message = ex.Message, Status = LogStatus.Error, Type = LogType.System, Timestamp = DateTime.Now });
                    });
                }
                else
                {
                    if (customer != null)
                    {
                        floutec.CustomerId = customer.Id;
                    }
                    else if (group != null)
                    {
                        floutec.GroupId = group.Id;
                        floutec.CustomerId = group.CustomerId;
                    }

                    await _estimatorsService.Insert(floutec, () =>
                    {
                        Logger.Log(lstMessages, new LogEntry { Message = $"Додано обчислювач ФЛОУТЕК: {floutec}", Status = LogStatus.Info, Type = LogType.System, Timestamp = DateTime.Now });
                    }, ex =>
                    {
                        Logger.Log(lstMessages, new LogEntry { Message = ex.Message, Status = LogStatus.Error, Type = LogType.System, Timestamp = DateTime.Now });
                    });
                }

                await UpdateData();
            }         
        }

        private async void EditRocMenu_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;

            TreeNode node = trvEstimators.SelectedNode;

            Customer customer = node?.Tag as Customer;

            EstimatorsGroup group = node?.Tag as EstimatorsGroup;

            Roc809 roc = node?.Tag as Roc809;

            EditRocForm form = new EditRocForm
            {
                StartPosition = FormStartPosition.CenterParent,
                IsEdit = menuItem != null && menuItem.Text.Equals(Resources.SettingsMsg),
                Roc = roc
            };

            DialogResult result = form.ShowDialog();

            if (result == DialogResult.OK && form.Roc != null)
            {
                roc = form.Roc;

                if (form.IsEdit)
                {
                    await _estimatorsService.Update(roc, () =>
                    {
                        Logger.Log(lstMessages, new LogEntry { Message = $"Дані обчислювача ROC809 змінено: {roc}", Status = LogStatus.Info, Type = LogType.System, Timestamp = DateTime.Now });
                    }, ex =>
                    {
                        Logger.Log(lstMessages, new LogEntry { Message = ex.Message, Status = LogStatus.Error, Type = LogType.System, Timestamp = DateTime.Now });
                    });
                }
                else
                {
                    if (customer != null)
                    {
                        roc.CustomerId = customer.Id;
                    }
                    else if (group != null)
                    {
                        roc.GroupId = group.Id;
                        roc.CustomerId = group.CustomerId;
                    }

                    await _estimatorsService.Insert(roc, () =>
                    {
                        Logger.Log(lstMessages, new LogEntry { Message = $"Додано обчислювач ROC809: {roc}", Status = LogStatus.Info, Type = LogType.System, Timestamp = DateTime.Now });
                    }, ex =>
                    {
                        Logger.Log(lstMessages, new LogEntry { Message = ex.Message, Status = LogStatus.Error, Type = LogType.System, Timestamp = DateTime.Now });
                    });
                }

                await UpdateData();
            }
        }

        private async void EditPointMenu_Click(object sender, EventArgs e)
        {

            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;

            TreeNode node = trvEstimators.SelectedNode;

            Roc809 roc = node?.Tag as Roc809;

            Roc809MeasurePoint point = node?.Tag as Roc809MeasurePoint;

            EditRocPointForm form = new EditRocPointForm
            {
                StartPosition = FormStartPosition.CenterParent,
                IsEdit = menuItem != null && menuItem.Text.Equals(Resources.SettingsMsg),
                Point = point
            };

            if (point != null)
            {
                form.Numbers = _estimators
                    .Where(est => est is Roc809)
                    .Cast<Roc809>()
                    .Single(r => r.Id == point.EstimatorId)
                    .MeasurePoints.Cast<Roc809MeasurePoint>()
                    .GroupBy(p => p.HistSegment)
                    .Select(g => new KeyValuePair<int, List<int>>(g.Key, g.Except(new List<Roc809MeasurePoint> {point}).Select(p => p.Number).ToList()))
                    .ToDictionary(d => d.Key, d => d.Value);

            }
            else if (roc != null)
            {
                form.Numbers = roc.MeasurePoints.Cast<Roc809MeasurePoint>()
                    .GroupBy(p => p.HistSegment)
                    .Select(g => new KeyValuePair<int, List<int>>(g.Key, g.Select(p => p.Number).ToList()))
                    .ToDictionary(d => d.Key, d => d.Value);
            }

            DialogResult result = form.ShowDialog();

            if (result == DialogResult.OK && form.Point != null)
            {
                point = form.Point;

                if (form.IsEdit)
                {
                    await _pointsService.Update(point, () =>
                    {
                        Logger.Log(lstMessages, new LogEntry { Message = $"Дані вимірювальної точки обчислювача ROC809 змінено: {point}", Status = LogStatus.Info, Type = LogType.System, Timestamp = DateTime.Now });
                    }, ex =>
                    {
                        Logger.Log(lstMessages, new LogEntry { Message = ex.Message, Status = LogStatus.Error, Type = LogType.System, Timestamp = DateTime.Now });
                    });
                }
                else
                {
                    if (roc != null)
                    {
                        point.EstimatorId = roc.Id;
                    }

                    await _pointsService.Insert(point, () =>
                    {
                        Logger.Log(lstMessages, new LogEntry { Message = $"Додано вимірювальну точку обчислювача ROC809: {point}", Status = LogStatus.Info, Type = LogType.System, Timestamp = DateTime.Now });
                    }, ex =>
                    {
                        Logger.Log(lstMessages, new LogEntry { Message = ex.Message, Status = LogStatus.Error, Type = LogType.System, Timestamp = DateTime.Now });
                    });
                }

                await UpdateData();
            }
        }

        private async void EditLineMenu_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;

            TreeNode node = trvEstimators.SelectedNode;

            Floutec floutec = node?.Tag as Floutec;

            FloutecMeasureLine line = node?.Tag as FloutecMeasureLine;

            EditFloutecLineForm form = new EditFloutecLineForm
            {
                StartPosition = FormStartPosition.CenterParent,
                IsEdit = menuItem != null && menuItem.Text.Equals(Resources.SettingsMsg),
                Line = line
            };

            if (line != null)
            {
                form.Numbers = _estimators
                    .Where(est => est is Floutec)
                    .Cast<Floutec>()
                    .Single(f => f.Id == line.EstimatorId)
                    .MeasurePoints.Cast<FloutecMeasureLine>()
                    .Select(l => l.Number)
                    .Except(new List<int> {line.Number})
                    .ToList();
            }
            else if (floutec != null)
            {
                form.Numbers = floutec.MeasurePoints.Cast<FloutecMeasureLine>()
                    .Select(l => l.Number)
                    .ToList();
            }

            DialogResult result = form.ShowDialog();

            if (result == DialogResult.OK && form.Line != null)
            {
                line = form.Line;

                if (form.IsEdit)
                {
                    await _pointsService.Update(line, () =>
                    {
                        Logger.Log(lstMessages, new LogEntry { Message = $"Дані вимірювальної нитки обчислювача ФЛОУТЕК змінено: {line}", Status = LogStatus.Info, Type = LogType.System, Timestamp = DateTime.Now });
                    }, ex =>
                    {
                        Logger.Log(lstMessages, new LogEntry { Message = ex.Message, Status = LogStatus.Error, Type = LogType.System, Timestamp = DateTime.Now });
                    });
                }
                else
                {
                    if (floutec != null)
                    {
                        line.EstimatorId = floutec.Id;
                    }

                    await _pointsService.Insert(line, () =>
                    {
                        Logger.Log(lstMessages, new LogEntry { Message = $"Додано вимірювальну нитку обчислювача ФЛОУТЕК: {line}", Status = LogStatus.Info, Type = LogType.System, Timestamp = DateTime.Now });
                    }, ex =>
                    {
                        Logger.Log(lstMessages, new LogEntry { Message = ex.Message, Status = LogStatus.Error, Type = LogType.System, Timestamp = DateTime.Now });
                    });
                }

                await UpdateData();
            }
        }

        private async void DeactivateMenu_Click(object sender, EventArgs e)
        {
            TreeNode node = trvEstimators.SelectedNode;

            EntityBase entity = node?.Tag as EntityBase;

            if (entity != null)
            {
                entity.IsActive = false;
                await _entitiesService.Update(entity, null, ex =>
                {
                    Logger.Log(lstMessages, new LogEntry { Message = ex.Message, Status = LogStatus.Error, Type = LogType.System, Timestamp = DateTime.Now });
                });

                await UpdateData();
            }
        }

        private async void ActivateMenu_Click(object sender, EventArgs e)
        {
            TreeNode node = trvEstimators.SelectedNode;

            EntityBase entity = node?.Tag as EntityBase;

            if (entity != null)
            {
                entity.IsActive = true;
                await _entitiesService.Update(entity, null, ex =>
                {
                    Logger.Log(lstMessages, new LogEntry { Message = ex.Message, Status = LogStatus.Error, Type = LogType.System, Timestamp = DateTime.Now });
                });

                await UpdateData();
            }
        }

        private async void DeletePointMenu_Click(object sender, EventArgs e)
        {
            TreeNode node = trvEstimators.SelectedNode;

            MeasurePointBase point = node?.Tag as MeasurePointBase;

            if (point != null)
            {
                DialogResult result = MessageBox.Show($"Ви дійсно бажаєте видалити вимірювальну точку {point} з бази даних без можливості відновлення?", "Видалення", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                if (result == DialogResult.Yes)
                {
                    await _pointsService.Delete(point, () =>
                    {
                        Logger.Log(lstMessages, new LogEntry { Message = $"Вимірювальну точку видалено: {point}", Status = LogStatus.Info, Type = LogType.System, Timestamp = DateTime.Now });
                    }, ex =>
                    {
                        Logger.Log(lstMessages, new LogEntry { Message = ex.Message, Status = LogStatus.Error, Type = LogType.System, Timestamp = DateTime.Now });
                    });

                    await UpdateData();
                }
            }
        }

        private async void DeleteCustomerMenu_Click(object sender, EventArgs e)
        {
            TreeNode node = trvEstimators.SelectedNode;

            Customer customer = node?.Tag as Customer;

            if (customer != null)
            {
                DialogResult result = MessageBox.Show($"Ви дійсно бажаєте видалити замовника {customer} з бази даних без можливості відновлення?", "Видалення", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                if (result == DialogResult.Yes)
                {
                    await _customersService.Delete(customer.Id, () =>
                    {
                        Logger.Log(lstMessages, new LogEntry { Message = $"Замовника видалено: {customer}", Status = LogStatus.Info, Type = LogType.System, Timestamp = DateTime.Now });
                    }, ex =>
                    {
                        Logger.Log(lstMessages, new LogEntry { Message = ex.Message, Status = LogStatus.Error, Type = LogType.System, Timestamp = DateTime.Now });
                    });

                    await UpdateData();
                }
            }
        }

        private async void DeleteGroupMenu_Click(object sender, EventArgs e)
        {
            TreeNode node = trvEstimators.SelectedNode;

            EstimatorsGroup group = node?.Tag as EstimatorsGroup;

            if (group != null)
            {
                DialogResult result = MessageBox.Show($"Ви дійсно бажаєте видалити групу обчислювачів {group} з бази даних без можливості відновлення?", "Видалення", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                if (result == DialogResult.Yes)
                {
                    await _groupsService.Delete(group.Id, () =>
                    {
                        Logger.Log(lstMessages, new LogEntry { Message = $"Групу обчислювачів видалено: {group}", Status = LogStatus.Info, Type = LogType.System, Timestamp = DateTime.Now });
                    }, ex =>
                    {
                        Logger.Log(lstMessages, new LogEntry { Message = ex.Message, Status = LogStatus.Error, Type = LogType.System, Timestamp = DateTime.Now });
                    });

                    await UpdateData();
                }
            }
        }

        private async void DeleteEstimatorMenu_Click(object sender, EventArgs e)
        {
            TreeNode node = trvEstimators.SelectedNode;

            EstimatorBase estimator = node?.Tag as EstimatorBase;

            if (estimator != null)
            {
                DialogResult result = MessageBox.Show($"Ви дійсно бажаєте видалити обчислювач {estimator} з бази даних без можливості відновлення?", "Видалення", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                if (result == DialogResult.Yes)
                {
                    await _estimatorsService.Delete(estimator, () =>
                    {
                        Logger.Log(lstMessages, new LogEntry { Message = $"Обчислювач видалено: {estimator}", Status = LogStatus.Info, Type = LogType.System, Timestamp = DateTime.Now });
                    }, ex =>
                    {
                        Logger.Log(lstMessages, new LogEntry { Message = ex.Message, Status = LogStatus.Error, Type = LogType.System, Timestamp = DateTime.Now });
                    });

                    await UpdateData();
                }
            }
        }

        #region Drag and Drop

        private void TrvEstimators_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Item != null)
            {
                TreeNode node = e.Item as TreeNode;

                if (node != null)
                {
                    EntityBase entity = node.Tag as EntityBase;

                    if (entity is EstimatorsGroup || entity is EstimatorBase)
                    {
                        trvEstimators.DoDragDrop(e.Item, DragDropEffects.Move);
                    }
                }               
            }
        }

        private void TrvEstimators_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private async void TrvEstimators_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode", false))
            {
                Point pt = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
                TreeNode destNode = ((TreeView)sender).GetNodeAt(pt);
                TreeNode newNode = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");

                if ((TreeView)sender == newNode.TreeView)
                {
                    EstimatorsGroup estimatorsGroup = newNode.Tag as EstimatorsGroup;
                    EstimatorBase estimator = newNode.Tag as EstimatorBase;
                    if (estimatorsGroup != null)
                    {
                        if (destNode == null || destNode.Tag is Customer)
                        {
                            if (destNode == null)
                            {
                                estimatorsGroup.CustomerId = null;
                                estimatorsGroup.Customer = null;
                            }
                            else
                            {
                                Customer customer = destNode.Tag as Customer;
                                estimatorsGroup.CustomerId = customer.Id;
                                estimatorsGroup.Customer = customer;
                            }

                            await _groupsService.Update(estimatorsGroup, () =>
                            {
                                Logger.Log(lstMessages, new LogEntry { Message = destNode == null ? $"Групу обчислювачів з Id={estimatorsGroup.Id} та назвою '{estimatorsGroup.Name}' відкріплено від замовника" : $"Групу обчислювачів з Id={estimatorsGroup.Id} та назвою '{estimatorsGroup.Name}' закріплено за замовником з Id={estimatorsGroup.Customer.Id} та назвою '{estimatorsGroup.Customer.Title}'", Status = LogStatus.Info, Type = LogType.System, Timestamp = DateTime.Now });
                            }, ex =>
                            {
                                Logger.Log(lstMessages, new LogEntry { Message = ex.Message, Status = LogStatus.Error, Type = LogType.System, Timestamp = DateTime.Now });
                            });
                        }
                    }
                    else if (estimator != null)
                    {
                        string message;

                        if (destNode == null || destNode.Tag is EstimatorsGroup || destNode.Tag is Customer)
                        {
                            if (destNode == null)
                            {
                                estimator.CustomerId = null;
                                estimator.Customer = null;
                                estimator.GroupId = null;
                                estimator.Group = null;
                                message = $"Обчислювач з Id={estimator.Id} та назвою '{estimator.Name}' відкріплено від замовників та груп";
                            }
                            else
                            {
                                EstimatorsGroup group = destNode.Tag as EstimatorsGroup;
                                Customer customer = destNode.Tag as Customer;
                                if (group != null)
                                {
                                    estimator.GroupId = group.Id;
                                    estimator.Group = group;
                                    estimator.CustomerId = group.CustomerId;
                                    estimator.Customer = _customers.SingleOrDefault(c => c.Id == group.CustomerId);
                                    message = $"Обчислювач з Id={estimator.Id} та назвою '{estimator.Name}' закріплено за групою з Id={group.Id} та назвою '{group.Name}'";
                                }
                                else
                                {
                                    estimator.GroupId = null;
                                    estimator.Group = null;
                                    estimator.CustomerId = customer.Id;
                                    estimator.Customer = customer;
                                    message = $"Обчислювач з Id={estimator.Id} та назвою '{estimator.Name}' закріплено за замовником з Id={customer.Id} та назвою '{customer.Title}'";
                                }
                            }

                            await _estimatorsService.Update(estimator, () =>
                            {
                                Logger.Log(lstMessages, new LogEntry { Message = message, Status = LogStatus.Info, Type = LogType.System, Timestamp = DateTime.Now });
                            }, ex =>
                            {
                                Logger.Log(lstMessages, new LogEntry { Message = ex.Message, Status = LogStatus.Error, Type = LogType.System, Timestamp = DateTime.Now });
                            });
                        }
                    }

                    await UpdateData();
                }
            }
        }

        #endregion

    }
}
