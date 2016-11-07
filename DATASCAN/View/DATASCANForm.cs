using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DATASCAN.Communication.Clients;
using DATASCAN.Core.Entities;
using DATASCAN.Core.Entities.Common;
using DATASCAN.Core.Entities.Floutecs;
using DATASCAN.Core.Entities.Rocs;
using DATASCAN.Core.Entities.Scanning;
using DATASCAN.DataAccess.Services;
using DATASCAN.Infrastructure.Logging;
using DATASCAN.Properties;
using DATASCAN.Scanners;
using DATASCAN.View.Common;
using DATASCAN.View.Extensions;
using DATASCAN.View.Forms;
using Settings = DATASCAN.Infrastructure.Settings.Settings;

namespace DATASCAN.View
{
    public partial class DATASCANForm : Form
    {
        #region Fields & constructor

        private string _sqlConnection;

        private readonly DataContextService _contextService;
        private readonly EntitiesService<EntityBase> _entitiesService;
        private readonly EstimatorsService _estimatorsService;
        private readonly CustomersService _customersService;
        private readonly GroupsService _groupsService;
        private readonly MeasurePointsService _pointsService;
        private readonly PeriodicScansService _periodicScansService;
        private readonly ScheduledScansService _scheduledScansService;
        private readonly ScanMembersService _scanMembersService;

        private readonly RocScanner _rocScanner;
        private readonly FloutecScanner _floutecScanner;

        private List<EstimatorBase> _estimators = new List<EstimatorBase>();
        private List<Customer> _customers = new List<Customer>();
        private List<EstimatorsGroup> _groups = new List<EstimatorsGroup>();
        private List<MeasurePointBase> _points = new List<MeasurePointBase>();
        private List<PeriodicScan> _periodicScans = new List<PeriodicScan>();
        private List<ScheduledScan> _scheduledScans = new List<ScheduledScan>();

        private readonly GprsClient _gprsClient;

        private const int SCAN_PERIOD_MS = 5000;

        public DATASCANForm()
        {
            InitializeComponent();              
            InitializeEstimatorsTree();   
            InitializeScansTree();  
            InitializeStatusStrip();

            GetSettings();

            InitializeConnection();

            _gprsClient = new GprsClient();
            InitializeGprsClient();

            _contextService = new DataContextService(_sqlConnection);
            _entitiesService = new EntitiesService<EntityBase>(_sqlConnection);
            _estimatorsService = new EstimatorsService(_sqlConnection);
            _customersService = new CustomersService(_sqlConnection);
            _groupsService = new GroupsService(_sqlConnection);
            _pointsService = new MeasurePointsService(_sqlConnection);
            _periodicScansService = new PeriodicScansService(_sqlConnection);
            _scheduledScansService = new ScheduledScansService(_sqlConnection);
            _scanMembersService = new ScanMembersService(_sqlConnection);

            _rocScanner = new RocScanner(lstMessages, _gprsClient);
            _floutecScanner = new FloutecScanner(lstMessages);
           
            UpdateData(true).ConfigureAwait(false);

            var timer = new Timer { Interval = SCAN_PERIOD_MS };
            timer.Tick += Timer_Tick;
            timer.Start();
        }        

        #endregion

        #region Setup

        private void GetSettings()
        {
            try
            {
                Settings.Get();
            }
            catch (Exception ex)
            {
                LogException(ex.Message);
            }            
        }

        private void InitializeConnection()
        {
            var connection = new SqlConnectionStringBuilder
            {
                DataSource = Settings.ServerName,
                InitialCatalog = Settings.DatabaseName,
                MultipleActiveResultSets = true,
                ConnectTimeout = int.Parse(Settings.ConnectionTimeout)
            };

            if (string.IsNullOrEmpty(Settings.UserName) || string.IsNullOrEmpty(Settings.UserPassword))
            {
                connection.IntegratedSecurity = true;
            }
            else
            {
                connection.IntegratedSecurity = false;
                connection.UserID = Settings.UserName;
                connection.Password = Settings.UserPassword;
            }

            _sqlConnection = connection.ToString();
        }

        private void InitializeGprsClient()
        {
            var ports = new List<string>();

            if (!string.IsNullOrEmpty(Settings.COMPort1))
                ports.Add(Settings.COMPort1);
            if (!string.IsNullOrEmpty(Settings.COMPort2))
                ports.Add(Settings.COMPort2);
            if (!string.IsNullOrEmpty(Settings.COMPort3))
                ports.Add(Settings.COMPort3);           

            _gprsClient.Baudrate = int.Parse(Settings.Baudrate);
            _gprsClient.DataBits = int.Parse(Settings.DataBits);
            _gprsClient.StopBits = (StopBits)Enum.Parse(typeof(StopBits), Settings.StopBits);
            _gprsClient.Parity = (Parity)Enum.Parse(typeof(Parity), Settings.Parity);
            _gprsClient.ReadDelay = int.Parse(Settings.ReadDelay);
            _gprsClient.WriteDelay = int.Parse(Settings.WriteDelay);
            _gprsClient.Timeout = int.Parse(Settings.Timeout);
            _gprsClient.Retries = int.Parse(Settings.Retries); 
            _gprsClient.WaitingTime = int.Parse(Settings.WaitingTime);
            _gprsClient.Ports = ports;
        }

        private async Task UpdateData(bool initialize)
        {
            status.Items[0].Visible = true;
            status.Items[2].Visible = true;

            var connected = await _contextService.TestConnection(initialize, ex => LogException(ex.Message));

            status.Items[0].Visible = false;
            status.Items[2].Visible = false;

            if (connected)
            {
                status.Items[0].Visible = true;
                status.Items[1].Visible = true;

                _estimators = await _estimatorsService.GetAll(null, ex => LogException(ex.Message), e => e.OrderBy(o => o.Id), e => e.Customer, e => e.Group, e => e.MeasurePoints);
                _customers = await _customersService.GetAll(null, ex => LogException(ex.Message), c => c.OrderBy(o => o.Title));
                _groups = await _groupsService.GetAll(null, ex => LogException(ex.Message), g => g.OrderBy(o => o.Name));
                _points = await _pointsService.GetAll(null, ex => LogException(ex.Message), p => p.OrderBy(o => o.Id));
                _periodicScans = await _periodicScansService.GetAll(null, ex => LogException(ex.Message), null, s => s.Members);
                _scheduledScans = await _scheduledScansService.GetAll(null, ex => LogException(ex.Message), null, s => s.Members, s => s.Periods);

                FillEstimatorsTree();
                FillScansTree();

                status.Items[0].Visible = false;
                status.Items[1].Visible = false;
            }
            else
            {
                trvEstimators.Nodes.Clear(); 
                trvScans.Nodes.Clear();               
            }

            trvEstimators.ContextMenuStrip.Items[0].Enabled = connected;
            trvEstimators.ContextMenuStrip.Items[1].Enabled = connected;
            trvEstimators.ContextMenuStrip.Items[2].Enabled = connected;
            trvEstimators.ContextMenuStrip.Items[3].Enabled = connected;

            trvScans.ContextMenuStrip.Items[0].Enabled = connected;
            trvScans.ContextMenuStrip.Items[1].Enabled = connected;
        }

        #endregion

        #region Components initialization

        private ContextMenuStrip EstimatorsContextMenu()
        {
            var estimatorsMenu = new ContextMenuStrip();

            estimatorsMenu.Items.AddRange(new ToolStripItem[]
            {
                new ToolStripMenuItem(Resources.AddCustomerMsg, null, EditCustomerMenu_Click),
                new ToolStripMenuItem(Resources.AddEstimatorsGroupMsg, null, EditGroupMenu_Click),
                new ToolStripMenuItem(Resources.AddFloutecMsg, null, EditFloutecMenu_Click),
                new ToolStripMenuItem(Resources.AddRocMsg, null, EditRocMenu_Click),
                new ToolStripSeparator(),
                new ToolStripMenuItem(Resources.UpdateMsg, Resources.Refresh, RefreshMenu_Click)
            });

            estimatorsMenu.Opening += EstimatorsContextMenu_Opening;

            return estimatorsMenu;
        }

        private ContextMenuStrip EstimatorContextMenu(EstimatorBase estimator)
        {
            var estimatorMenu = new ContextMenuStrip();

            estimatorMenu.Items.AddRange(new ToolStripItem[]
            {
                estimator is Floutec ? new ToolStripMenuItem(Resources.AddMeasureLineMsg, null, EditLineMenu_Click) : new ToolStripMenuItem(Resources.AddMeasurePointMsg, null, EditPointMenu_Click),
                new ToolStripSeparator(),
                estimator is Floutec ? new ToolStripMenuItem(Resources.SettingsMsg, Resources.Settings, EditFloutecMenu_Click) : new ToolStripMenuItem(Resources.SettingsMsg, Resources.Settings, EditRocMenu_Click),
                new ToolStripSeparator(),
                estimator.IsActive ? new ToolStripMenuItem(Resources.DeactivateMsg, Resources.Deactivate, DeactivateMenu_Click) : new ToolStripMenuItem(Resources.ActivateMsg, Resources.Activate, ActivateMenu_Click),
                new ToolStripMenuItem(Resources.DeleteMsg, Resources.Delete, DeleteEstimatorMenu_Click)
            });

            estimatorMenu.Opening += EstimatorsContextMenu_Opening;

            return estimatorMenu;
        }

        private ContextMenuStrip MeasurePointContextMenu(EstimatorBase estimator, MeasurePointBase point)
        {
            var pointMenu = new ContextMenuStrip();

            pointMenu.Items.AddRange(new ToolStripItem[]
            {
                estimator is Floutec ? new ToolStripMenuItem(Resources.SettingsMsg, Resources.Settings, EditLineMenu_Click) : new ToolStripMenuItem(Resources.SettingsMsg, Resources.Settings, EditPointMenu_Click),
                new ToolStripSeparator(),
                point.IsActive ? new ToolStripMenuItem(Resources.DeactivateMsg, Resources.Deactivate, DeactivateMenu_Click) : new ToolStripMenuItem(Resources.ActivateMsg, Resources.Activate, ActivateMenu_Click),
                new ToolStripMenuItem(Resources.DeleteMsg, Resources.Delete, DeletePointMenu_Click)
            });

            pointMenu.Opening += EstimatorsContextMenu_Opening;

            return pointMenu;
        }

        private ContextMenuStrip CustomerContextMenu(Customer customer)
        {
            var customerMenu = new ContextMenuStrip();

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

            customerMenu.Opening += EstimatorsContextMenu_Opening;

            return customerMenu;
        }

        private ContextMenuStrip GroupContextMenu(EstimatorsGroup group)
        {
            var groupMenu = new ContextMenuStrip();

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

            groupMenu.Opening += EstimatorsContextMenu_Opening;

            return groupMenu;
        }

        private ContextMenuStrip ScansContextMenu()
        {
            var scansMenu = new ContextMenuStrip();

            scansMenu.Items.AddRange(new ToolStripItem[]
            {
                new ToolStripMenuItem(Resources.AddPeriodicScan, null, EditPeriodicScan_Click),
                new ToolStripMenuItem(Resources.AddScheduledScan, null, EditScheduledScan_Click),
                new ToolStripSeparator(),
                new ToolStripMenuItem(Resources.UpdateMsg, Resources.Refresh, RefreshMenu_Click)
            });

            scansMenu.Opening += ScansContextMenu_Opening;

            return scansMenu;
        }

        private ContextMenuStrip PeriodicScansContextMenu()
        {
            var periodicScansMenu = new ContextMenuStrip();

            periodicScansMenu.Items.AddRange(new ToolStripItem[]
            {
                new ToolStripMenuItem(Resources.AddPeriodicScan, null, EditPeriodicScan_Click),
            });

            return periodicScansMenu;
        }

        private ContextMenuStrip ScheduledScansContextMenu()
        {
            var scheduledScansMenu = new ContextMenuStrip();

            scheduledScansMenu.Items.AddRange(new ToolStripItem[]
            {
                new ToolStripMenuItem(Resources.AddScheduledScan, null, EditScheduledScan_Click),
            });

            return scheduledScansMenu;
        }

        private ContextMenuStrip PeriodicScanContextMenu(PeriodicScan scan)
        {
            var scanMenu = new ContextMenuStrip();

            scanMenu.Items.AddRange(new ToolStripItem[]
            {
                new ToolStripMenuItem(Resources.SettingsMsg, Resources.Settings, EditPeriodicScan_Click),
                new ToolStripSeparator(),
                scan.IsActive ? new ToolStripMenuItem(Resources.DeactivateMsg, Resources.Deactivate, DeactivateScanMenu_Click) : new ToolStripMenuItem(Resources.ActivateMsg, Resources.Activate, ActivateScanMenu_Click),
                new ToolStripMenuItem(Resources.DeleteMsg, Resources.Delete, DeletePeriodicScanMenu_Click)
            });

            scanMenu.Opening += ScansContextMenu_Opening;

            return scanMenu;
        }

        private ContextMenuStrip ScheduledScanContextMenu(ScheduledScan scan)
        {
            var scanMenu = new ContextMenuStrip();

            scanMenu.Items.AddRange(new ToolStripItem[]
            {
                new ToolStripMenuItem(Resources.SettingsMsg, Resources.Settings, EditScheduledScan_Click),
                new ToolStripSeparator(),
                scan.IsActive ? new ToolStripMenuItem(Resources.DeactivateMsg, Resources.Deactivate, DeactivateScanMenu_Click) : new ToolStripMenuItem(Resources.ActivateMsg, Resources.Activate, ActivateScanMenu_Click),
                new ToolStripMenuItem(Resources.DeleteMsg, Resources.Delete, DeleteScheduledScanMenu_Click)
            });

            scanMenu.Opening += ScansContextMenu_Opening;

            return scanMenu;
        }

        public ContextMenuStrip ScanMemberContextMenu()
        {
            var memberMenu = new ContextMenuStrip();

            memberMenu.Items.AddRange(new ToolStripItem[]
            {
                new ToolStripMenuItem(Resources.SettingsMsg, Resources.Settings, MemberSettingsMenu_Click),
                new ToolStripSeparator(),
                new ToolStripMenuItem(Resources.DeleteFromScanMsg, Resources.Delete, DeleteMemberMenu_Click)
            });

            memberMenu.Opening += ScansContextMenu_Opening;

            return memberMenu;
        }

        private void InitializeEstimatorsTree()
        {
            trvEstimators.ContextMenuStrip = EstimatorsContextMenu();
            trvEstimators.ShowNodeToolTips = true;
            trvEstimators.AllowDrop = true;
            trvEstimators.ItemDrag += TrvEstimators_ItemDrag;
            trvEstimators.DragEnter += TrvEstimators_DragEnter;
            trvEstimators.DragDrop += TrvEstimators_DragDrop;
        }

        private void InitializeScansTree()
        {
            trvScans.ContextMenuStrip = ScansContextMenu();
            trvScans.ShowNodeToolTips = true;
            trvScans.AllowDrop = true;
            trvScans.ItemDrag += TrvEstimators_ItemDrag;
            trvScans.DragEnter += TrvEstimators_DragEnter;
            trvScans.DragDrop += TrvEstimators_DragDrop;
        }

        private void InitializeStatusStrip()
        {
            var progress = new PictureBox { Image = Resources.Progress };
            status.Items.Add(progress.Image);
            status.Items.Add("Виконується запит до бази даних ...");
            status.Items.Add("Встановлюється з'єднання з сервером баз даних ...");
            status.Items[0].Visible = false;
            status.Items[1].Visible = false;
            status.Items[2].Visible = false;
        }

        #endregion

        #region Filling estimators tree

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
                var customerNode = trvEstimators.Nodes.Add(customer.Title);
                customerNode.ForeColor = customer.IsActive ? Color.Black : Color.Red;
                customerNode.Tag = customer;
                customerNode.ImageIndex = 0;
                customerNode.SelectedImageIndex = 0;
                customerNode.ContextMenuStrip = CustomerContextMenu(customer);
                customerNode.ToolTipText = customer.Info();

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
                var groupNode = customerNode?.Nodes.Add(group.Name) ?? trvEstimators.Nodes.Add(group.Name);
                groupNode.ForeColor = group.IsActive ? Color.Black : Color.Red;
                groupNode.Tag = group;
                groupNode.ImageIndex = 2;
                groupNode.SelectedImageIndex = 2;
                groupNode.ContextMenuStrip = GroupContextMenu(group);
                groupNode.ToolTipText = group.Info();

                FillEstimators(groupNode);
            });
        }

        private void FillEstimators(TreeNode parentNode = null)
        {
            var estimators = new List<EstimatorBase>();

            if (parentNode != null)
            {
                if (parentNode.Tag is EstimatorsGroup)
                {
                    var group = parentNode.Tag as EstimatorsGroup;
                    estimators = _estimators.Where(e => e.GroupId == group.Id).ToList();
                }
                else if (parentNode.Tag is Customer)
                {
                    var customer = parentNode.Tag as Customer;
                    estimators = _estimators.Where(e => e.CustomerId == customer.Id && !e.GroupId.HasValue).ToList();
                }
            }
            else
            {
                estimators = _estimators.Where(e => !e.GroupId.HasValue && !e.CustomerId.HasValue).ToList();
            }

            estimators.ForEach(estimator =>
            {
                var nodeTitle = estimator.NodeTitle();

                var estimatorNode = parentNode?.Nodes.Add(nodeTitle) ?? trvEstimators.Nodes.Add(nodeTitle);
                estimatorNode.ForeColor = estimator.IsActive ? Color.Black : Color.Red;
                estimatorNode.Tag = estimator;
                estimatorNode.ImageIndex = 3;
                estimatorNode.SelectedImageIndex = 3;
                estimatorNode.ContextMenuStrip = EstimatorContextMenu(estimator);
                estimatorNode.ToolTipText = estimator is Floutec ? ((Floutec)estimator).Info() : ((Roc809)estimator).Info();

                FillPoints(estimatorNode);
            });
        }

        private void FillPoints(TreeNode estimatorNode)
        {
            if (estimatorNode?.Tag is EstimatorBase)
            {
                var estimator = estimatorNode.Tag as EstimatorBase;

                _points.Where(p => p.EstimatorId == estimator.Id).ToList().ForEach(point =>
                {
                    var pointNode = estimatorNode.Nodes.Add(point.NodeTitle());

                    pointNode.ForeColor = point.IsActive ? Color.Black : Color.Red;
                    pointNode.Tag = point;
                    pointNode.ImageIndex = 4;
                    pointNode.SelectedImageIndex = 4;
                    pointNode.ContextMenuStrip = MeasurePointContextMenu(estimator, point);
                    pointNode.ToolTipText = point is FloutecMeasureLine ? ((FloutecMeasureLine)point).Info() : ((Roc809MeasurePoint)point).Info();
                });
            }
        }

        #endregion

        #region Filling scans tree

        private void FillScansTree()
        {
            var savedExpansionState = trvScans.Nodes.GetExpansionState();

            trvScans.BeginUpdate();
            trvScans.Nodes.Clear();

            if (_periodicScans.Any())
            {
                var periodicScansNode = trvScans.Nodes.Add("Періодичні опитування");
                periodicScansNode.ImageIndex = 0;
                periodicScansNode.SelectedImageIndex = 0;
                periodicScansNode.ContextMenuStrip = PeriodicScansContextMenu();

                FillPeriodicScans(periodicScansNode);
            }

            if (_scheduledScans.Any())
            {
                var scheduledScansNode = trvScans.Nodes.Add("Опитування за графіком");
                scheduledScansNode.ImageIndex = 1;
                scheduledScansNode.SelectedImageIndex = 1;
                scheduledScansNode.ContextMenuStrip = ScheduledScansContextMenu();

                FillScheduledScans(scheduledScansNode);
            }

            trvScans.Nodes.SetExpansionState(savedExpansionState);
            trvScans.EndUpdate();
        }        

        private void FillPeriodicScans(TreeNode scansGroup)
        {
            _periodicScans.ForEach(scan =>
            {
                var scanNode = scansGroup.Nodes.Add(scan.NodeTitle());
                scanNode.ForeColor = scan.IsActive ? Color.Black : Color.Red;
                scanNode.Tag = scan;
                scanNode.ImageIndex = 2;
                scanNode.SelectedImageIndex = 2;
                scanNode.ContextMenuStrip = PeriodicScanContextMenu(scan);
                scanNode.ToolTipText = scan.Info();

                FillMembers(scanNode);
            });
        }

        private void FillScheduledScans(TreeNode scansGroup)
        {
            _scheduledScans.ForEach(scan =>
            {
                var scanNode = scansGroup.Nodes.Add(scan.NodeTitle());
                scanNode.ForeColor = scan.IsActive ? Color.Black : Color.Red;
                scanNode.Tag = scan;
                scanNode.ImageIndex = 2;
                scanNode.SelectedImageIndex = 2;
                scanNode.ContextMenuStrip = ScheduledScanContextMenu(scan);
                scanNode.ToolTipText = scan.Info();

                FillMembers(scanNode);
            });
        }

        private void FillMembers(TreeNode scanNode)
        {
            var scan = scanNode.Tag as ScanBase;

            scan?.Members.ToList().ForEach(member =>
            {
                var estimator = _estimators.Find(e => e.Id == member.EstimatorId);

                var memberNode = scanNode.Nodes.Add(estimator.NodeTitle());
                memberNode.ForeColor = estimator.IsActive ? Color.Black : Color.Red;
                memberNode.Tag = member;
                memberNode.ImageIndex = 3;
                memberNode.SelectedImageIndex = 3;
                memberNode.ContextMenuStrip = ScanMemberContextMenu();
            });
        }

        #endregion

        #region Estimators tree event handlers

        private void EstimatorsContextMenu_Opening(object sender, CancelEventArgs e)
        {
            var nodeAtMousePosition = trvEstimators.GetNodeAt(trvEstimators.PointToClient(MousePosition));

            var selectedNode = trvEstimators.SelectedNode;

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

        private async void EditCustomerMenu_Click(object sender, EventArgs e)
        {
            var menuItem = sender as ToolStripMenuItem;

            var node = trvEstimators.SelectedNode;

            var customer = node?.Tag as Customer;

            var form = new EditCustomerForm
            {
                StartPosition = FormStartPosition.CenterParent,
                IsEdit = menuItem != null && menuItem.Text.Equals(Resources.InformationMsg),
                Customer = customer
            };

            var result = form.ShowDialog();

            if (result != DialogResult.OK || form.Customer == null) return;

            customer = form.Customer;

            if (form.IsEdit)
            {
                await _customersService.Update(customer, () =>
                {
                    Logger.Log(lstMessages, new LogEntry { Message = $"Дані замовника змінено: {customer}", Status = LogStatus.Info, Type = LogType.System, Timestamp = DateTime.Now });
                }, ex => LogException(ex.Message));
            }
            else
            {
                await _customersService.Insert(customer, () =>
                {
                    Logger.Log(lstMessages, new LogEntry { Message = $"Додано замовника: {customer}", Status = LogStatus.Info, Type = LogType.System, Timestamp = DateTime.Now });
                }, ex => LogException(ex.Message));
            }

            await UpdateData(false);
        }

        private async void EditGroupMenu_Click(object sender, EventArgs e)
        {
            var menuItem = sender as ToolStripMenuItem;

            var node = trvEstimators.SelectedNode;

            var customer = node?.Tag as Customer;

            var group = node?.Tag as EstimatorsGroup;

            var form = new EditEstimatorsGroupForm
            {
                StartPosition = FormStartPosition.CenterParent,
                IsEdit = menuItem != null && menuItem.Text.Equals(Resources.InformationMsg),
                Group = group
            };

            var result = form.ShowDialog();

            if (result == DialogResult.OK && form.Group != null)
            {
                group = form.Group;

                if (form.IsEdit)
                {
                    await _groupsService.Update(group, () =>
                    {
                        Logger.Log(lstMessages, new LogEntry { Message = $"Дані групи обчислювачів змінено: {group}", Status = LogStatus.Info, Type = LogType.System, Timestamp = DateTime.Now });
                    }, ex => LogException(ex.Message));
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
                    }, ex => LogException(ex.Message));
                }

                await UpdateData(false);
            }
        }

        private async void EditFloutecMenu_Click(object sender, EventArgs e)
        {
            var menuItem = sender as ToolStripMenuItem;

            var node = trvEstimators.SelectedNode;

            var customer = node?.Tag as Customer;

            var group = node?.Tag as EstimatorsGroup;

            var floutec = node?.Tag as Floutec;

            var form = new EditFloutecForm
            {
                StartPosition = FormStartPosition.CenterParent,
                IsEdit = menuItem != null && menuItem.Text.Equals(Resources.SettingsMsg),
                Floutec = floutec
            };

            form.Addresses = !form.IsEdit ? _estimators.OfType<Floutec>().Select(f => f.Address).ToList() : _estimators.OfType<Floutec>().Except(new[] { floutec }).Select(f => f.Address).ToList();

            var result = form.ShowDialog();

            if (result != DialogResult.OK || form.Floutec == null) return;

            floutec = form.Floutec;

            if (form.IsEdit)
            {
                await _estimatorsService.Update(floutec, () =>
                {
                    Logger.Log(lstMessages, new LogEntry { Message = $"Дані обчислювача ФЛОУТЕК змінено: {floutec}", Status = LogStatus.Info, Type = LogType.System, Timestamp = DateTime.Now });
                }, ex => LogException(ex.Message));
            }
            else
            {
                if (customer != null)
                {
                    floutec.CustomerId = customer.Id;
                }
                else if (@group != null)
                {
                    floutec.GroupId = @group.Id;
                    floutec.CustomerId = @group.CustomerId;
                }

                await _estimatorsService.Insert(floutec, () =>
                {
                    Logger.Log(lstMessages, new LogEntry { Message = $"Додано обчислювач ФЛОУТЕК: {floutec}", Status = LogStatus.Info, Type = LogType.System, Timestamp = DateTime.Now });
                }, ex => LogException(ex.Message));
            }

            await UpdateData(false);
        }

        private async void EditRocMenu_Click(object sender, EventArgs e)
        {
            var menuItem = sender as ToolStripMenuItem;

            var node = trvEstimators.SelectedNode;

            var customer = node?.Tag as Customer;

            var group = node?.Tag as EstimatorsGroup;

            var roc = node?.Tag as Roc809;

            var form = new EditRocForm
            {
                StartPosition = FormStartPosition.CenterParent,
                IsEdit = menuItem != null && menuItem.Text.Equals(Resources.SettingsMsg),
                Roc = roc
            };

            var result = form.ShowDialog();

            if (result != DialogResult.OK || form.Roc == null) return;

            roc = form.Roc;

            if (form.IsEdit)
            {
                await _estimatorsService.Update(roc, () =>
                {
                    Logger.Log(lstMessages, new LogEntry { Message = $"Дані обчислювача ROC809 змінено: {roc}", Status = LogStatus.Info, Type = LogType.System, Timestamp = DateTime.Now });
                }, ex => LogException(ex.Message));
            }
            else
            {
                if (customer != null)
                {
                    roc.CustomerId = customer.Id;
                }
                else if (@group != null)
                {
                    roc.GroupId = @group.Id;
                    roc.CustomerId = @group.CustomerId;
                }

                await _estimatorsService.Insert(roc, () =>
                {
                    Logger.Log(lstMessages, new LogEntry { Message = $"Додано обчислювач ROC809: {roc}", Status = LogStatus.Info, Type = LogType.System, Timestamp = DateTime.Now });
                }, ex => LogException(ex.Message));
            }

            await UpdateData(false);
        }

        private async void EditPointMenu_Click(object sender, EventArgs e)
        {

            var menuItem = sender as ToolStripMenuItem;

            var node = trvEstimators.SelectedNode;

            var roc = node?.Tag as Roc809;

            var point = node?.Tag as Roc809MeasurePoint;

            var form = new EditRocPointForm
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
                    .Select(g => new KeyValuePair<int, List<int>>(g.Key, g.Except(new List<Roc809MeasurePoint> { point }).Select(p => p.Number).ToList()))
                    .ToDictionary(d => d.Key, d => d.Value);

            }
            else if (roc != null)
            {
                form.Numbers = roc.MeasurePoints.Cast<Roc809MeasurePoint>()
                    .GroupBy(p => p.HistSegment)
                    .Select(g => new KeyValuePair<int, List<int>>(g.Key, g.Select(p => p.Number).ToList()))
                    .ToDictionary(d => d.Key, d => d.Value);
            }

            var result = form.ShowDialog();

            if (result != DialogResult.OK || form.Point == null) return;

            point = form.Point;

            if (form.IsEdit)
            {
                await _pointsService.Update(point, () =>
                {
                    Logger.Log(lstMessages, new LogEntry { Message = $"Дані вимірювальної точки обчислювача ROC809 змінено: {point}", Status = LogStatus.Info, Type = LogType.System, Timestamp = DateTime.Now });
                }, ex => LogException(ex.Message));
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
                }, ex => LogException(ex.Message));
            }

            await UpdateData(false);
        }

        private async void EditLineMenu_Click(object sender, EventArgs e)
        {
            var menuItem = sender as ToolStripMenuItem;

            var node = trvEstimators.SelectedNode;

            var floutec = node?.Tag as Floutec;

            var line = node?.Tag as FloutecMeasureLine;

            var form = new EditFloutecLineForm
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
                    .Except(new List<int> { line.Number })
                    .ToList();
            }
            else if (floutec != null)
            {
                form.Numbers = floutec.MeasurePoints.Cast<FloutecMeasureLine>()
                    .Select(l => l.Number)
                    .ToList();
            }

            var result = form.ShowDialog();

            if (result != DialogResult.OK || form.Line == null) return;

            line = form.Line;

            if (form.IsEdit)
            {
                await _pointsService.Update(line, () =>
                {
                    Logger.Log(lstMessages, new LogEntry { Message = $"Дані вимірювальної нитки обчислювача ФЛОУТЕК змінено: {line}", Status = LogStatus.Info, Type = LogType.System, Timestamp = DateTime.Now });
                }, ex => LogException(ex.Message));
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
                }, ex => LogException(ex.Message));
            }

            await UpdateData(false);
        }

        private async void DeactivateMenu_Click(object sender, EventArgs e)
        {
            var node = trvEstimators.SelectedNode;

            var entity = node?.Tag as EntityBase;

            if (entity == null) return;

            entity.IsActive = false;

            await _entitiesService.Update(entity, null, ex => LogException(ex.Message));

            await UpdateData(false);
        }

        private async void ActivateMenu_Click(object sender, EventArgs e)
        {
            var node = trvEstimators.SelectedNode;

            var entity = node?.Tag as EntityBase;

            if (entity == null) return;

            entity.IsActive = true;

            await _entitiesService.Update(entity, null, ex => LogException(ex.Message));

            await UpdateData(false);
        }

        private async void DeletePointMenu_Click(object sender, EventArgs e)
        {
            var node = trvEstimators.SelectedNode;

            var point = node?.Tag as MeasurePointBase;

            if (point == null) return;

            DialogResult result = MessageBox.Show($"Ви дійсно бажаєте видалити вимірювальну точку {point} з бази даних без можливості відновлення?", "Видалення", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

            if (result != DialogResult.Yes) return;

            await _pointsService.Delete(point, () =>
            {
                Logger.Log(lstMessages, new LogEntry { Message = $"Вимірювальну точку видалено: {point}", Status = LogStatus.Info, Type = LogType.System, Timestamp = DateTime.Now });
            }, ex => LogException(ex.Message));

            await UpdateData(false);
        }

        private async void DeleteCustomerMenu_Click(object sender, EventArgs e)
        {
            var node = trvEstimators.SelectedNode;

            var customer = node?.Tag as Customer;

            if (customer == null) return;

            DialogResult result = MessageBox.Show($"Ви дійсно бажаєте видалити замовника {customer} з бази даних без можливості відновлення?", "Видалення", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

            if (result != DialogResult.Yes) return;

            await _customersService.Delete(customer.Id, () =>
            {
                Logger.Log(lstMessages, new LogEntry { Message = $"Замовника видалено: {customer}", Status = LogStatus.Info, Type = LogType.System, Timestamp = DateTime.Now });
            }, ex => LogException(ex.Message));

            await UpdateData(false);
        }

        private async void DeleteGroupMenu_Click(object sender, EventArgs e)
        {
            var node = trvEstimators.SelectedNode;

            var group = node?.Tag as EstimatorsGroup;

            if (@group == null) return;

            DialogResult result = MessageBox.Show($"Ви дійсно бажаєте видалити групу обчислювачів {@group} з бази даних без можливості відновлення?", "Видалення", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

            if (result != DialogResult.Yes) return;

            await _groupsService.Delete(@group.Id, () =>
            {
                Logger.Log(lstMessages, new LogEntry { Message = $"Групу обчислювачів видалено: {@group}", Status = LogStatus.Info, Type = LogType.System, Timestamp = DateTime.Now });
            }, ex => LogException(ex.Message));

            await UpdateData(false);
        }

        private async void DeleteEstimatorMenu_Click(object sender, EventArgs e)
        {
            var node = trvEstimators.SelectedNode;

            var estimator = node?.Tag as EstimatorBase;

            if (estimator == null) return;

            var result = MessageBox.Show($"Ви дійсно бажаєте видалити обчислювач {estimator} з бази даних без можливості відновлення?", "Видалення", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

            if (result != DialogResult.Yes) return;

            await _estimatorsService.Delete(estimator, () =>
            {
                Logger.Log(lstMessages, new LogEntry { Message = $"Обчислювач видалено: {estimator}", Status = LogStatus.Info, Type = LogType.System, Timestamp = DateTime.Now });
            }, ex => LogException(ex.Message));

            await UpdateData(false);
        }

        #endregion

        #region Scans tree event handlers

        private void ScansContextMenu_Opening(object sender, CancelEventArgs e)
        {
            var nodeAtMousePosition = trvScans.GetNodeAt(trvScans.PointToClient(MousePosition));

            var selectedNode = trvScans.SelectedNode;

            if (nodeAtMousePosition != null)
            {
                if (nodeAtMousePosition != selectedNode)
                    trvScans.SelectedNode = nodeAtMousePosition;
            }
            else
            {
                trvScans.SelectedNode = null;
            }
        }

        private async void ActivateScanMenu_Click(object sender, EventArgs e)
        {
            var node = trvScans.SelectedNode;

            var entity = node?.Tag as EntityBase;

            if (entity == null) return;

            entity.IsActive = true;
            await _entitiesService.Update(entity, null, ex => LogException(ex.Message));
            await UpdateData(false);
        }

        private async void DeactivateScanMenu_Click(object sender, EventArgs e)
        {
            var node = trvScans.SelectedNode;

            var entity = node?.Tag as EntityBase;

            if (entity == null) return;

            entity.IsActive = false;
            await _entitiesService.Update(entity, null, ex => LogException(ex.Message));
            await UpdateData(false);
        }

        private async void EditPeriodicScan_Click(object sender, EventArgs e)
        {
            var menuItem = sender as ToolStripMenuItem;

            var node = trvScans.SelectedNode;

            var scan = node?.Tag as PeriodicScan;

            var form = new EditPeriodicScanForm
            {
                StartPosition = FormStartPosition.CenterParent,
                IsEdit = menuItem != null && menuItem.Text.Equals(Resources.SettingsMsg),
                Scan = scan
            };

            var result = form.ShowDialog();

            if (result != DialogResult.OK || form.Scan == null) return;

            scan = form.Scan;

            if (form.IsEdit)
            {
                await _periodicScansService.Update(scan, () =>
                {
                    Logger.Log(lstMessages, new LogEntry { Message = $"Дані періодичного опитування змінено: {scan}", Status = LogStatus.Info, Type = LogType.System, Timestamp = DateTime.Now });
                }, ex => LogException(ex.Message));
            }
            else
            {
                await _periodicScansService.Insert(scan, () =>
                {
                    Logger.Log(lstMessages, new LogEntry { Message = $"Додано періодичне опитування: {scan}", Status = LogStatus.Info, Type = LogType.System, Timestamp = DateTime.Now });
                }, ex => LogException(ex.Message));
            }

            await UpdateData(false);
        }

        private async void EditScheduledScan_Click(object sender, EventArgs e)
        {
            var menuItem = sender as ToolStripMenuItem;

            var node = trvScans.SelectedNode;

            var scan = node?.Tag as ScheduledScan;

            var form = new EditScheduledScanForm
            {
                StartPosition = FormStartPosition.CenterParent,
                IsEdit = menuItem != null && menuItem.Text.Equals(Resources.SettingsMsg),
                Scan = scan
            };

            var result = form.ShowDialog();

            if (result != DialogResult.OK || form.Scan == null) return;

            scan = form.Scan;

            if (form.IsEdit)
            {
                await _scheduledScansService.Update(scan, () =>
                {
                    Logger.Log(lstMessages, new LogEntry { Message = $"Дані опитування за графіком змінено: {scan}", Status = LogStatus.Info, Type = LogType.System, Timestamp = DateTime.Now });
                }, ex => LogException(ex.Message));
            }
            else
            {
                await _scheduledScansService.Insert(scan, () =>
                {
                    Logger.Log(lstMessages, new LogEntry { Message = $"Додано опитування за графіком: {scan}", Status = LogStatus.Info, Type = LogType.System, Timestamp = DateTime.Now });
                }, ex => LogException(ex.Message));
            }

            await UpdateData(false);
        }

        private async void DeletePeriodicScanMenu_Click(object sender, EventArgs e)
        {
            var node = trvScans.SelectedNode;

            var scan = node?.Tag as PeriodicScan;

            if (scan == null) return;

            var result = MessageBox.Show($"Ви дійсно бажаєте видалити періодичне опитування {scan} з бази даних без можливості відновлення?", "Видалення", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

            if (result != DialogResult.Yes) return;

            await _periodicScansService.Delete(scan.Id, () =>
            {
                Logger.Log(lstMessages, new LogEntry { Message = $"Періодичне опитування видалено: {scan}", Status = LogStatus.Info, Type = LogType.System, Timestamp = DateTime.Now });
            }, ex => LogException(ex.Message));

            await UpdateData(false);
        }

        private async void DeleteScheduledScanMenu_Click(object sender, EventArgs e)
        {
            var node = trvScans.SelectedNode;

            var scan = node?.Tag as ScheduledScan;

            if (scan == null) return;

            var result = MessageBox.Show($"Ви дійсно бажаєте видалити опитування за графіком {scan} з бази даних без можливості відновлення?", "Видалення", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

            if (result != DialogResult.Yes) return;

            await _scheduledScansService.Delete(scan, () =>
            {
                Logger.Log(lstMessages, new LogEntry { Message = $"Опитування за графіком видалено: {scan}", Status = LogStatus.Info, Type = LogType.System, Timestamp = DateTime.Now });
            }, ex => LogException(ex.Message));

            await UpdateData(false);
        }

        private async void MemberSettingsMenu_Click(object sender, EventArgs e)
        {
            var node = trvScans.SelectedNode;

            var member = node?.Tag as ScanMemberBase;

            Form form;

            if (member is RocScanMember)
            {
                form = new EditRocScanMemberForm
                {
                    StartPosition = FormStartPosition.CenterParent,
                    IsEdit = true,
                    Member = member as RocScanMember,
                    ExistentMembers = GetRocMembers()
                };
            }
            else
            {
                form = new EditFloutecScanMemberForm
                {
                    StartPosition = FormStartPosition.CenterParent,
                    IsEdit = true,
                    Member = member as FloutecScanMember,
                    ExistentMembers = GetFloutecMembers()
                };                
            }

            var result = form.ShowDialog();

            if (result != DialogResult.OK) return;

            member = member is RocScanMember ? (ScanMemberBase)((EditRocScanMemberForm)form).Member : ((EditFloutecScanMemberForm)form).Member;

            await _scanMembersService.Update(member, () =>
            {
                Logger.Log(lstMessages, new LogEntry { Message = $"Дані періодичного опитування змінено: {member}", Status = LogStatus.Info, Type = LogType.System, Timestamp = DateTime.Now });
            }, ex => LogException(ex.Message));

            await UpdateData(false);
        }

        private async void DeleteMemberMenu_Click(object sender, EventArgs e)
        {
            var node = trvScans.SelectedNode;

            var member = node?.Tag as ScanMemberBase;

            if (member == null) return;

            var result = MessageBox.Show($"Ви дійсно бажаєте видалити опитування обчислювача {member} з бази даних без можливості відновлення?", "Видалення", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

            if (result != DialogResult.Yes) return;

            await _scanMembersService.Delete(member.Id, () =>
            {
                Logger.Log(lstMessages, new LogEntry { Message = $"Опитування обчислювача видалено: {member}", Status = LogStatus.Info, Type = LogType.System, Timestamp = DateTime.Now });
            }, ex => LogException(ex.Message));

            await UpdateData(false);
        }

        #endregion        

        #region Form event handlers

        private async void mnuDatabase_Click(object sender, EventArgs e)
        {
            var form = new ServerSettingsForm { StartPosition = FormStartPosition.CenterParent };

            var result = form.ShowDialog();

            if (result != DialogResult.OK) return;

            Logger.Log(lstMessages, new LogEntry { Message = "Налаштування сервера баз даних змінено", Status = LogStatus.Info, Type = LogType.System, Timestamp = DateTime.Now });

            InitializeConnection();
            await UpdateData(false);
        }

        private void mnuConnection_Click(object sender, EventArgs e)
        {
            var form = new ConnectionSettingsForm {StartPosition = FormStartPosition.CenterParent};
            var result = form.ShowDialog();

            if (result == DialogResult.OK)
            {
                Logger.Log(lstMessages, new LogEntry { Message = "Налаштування підключення змінено", Status = LogStatus.Info, Type = LogType.System, Timestamp = DateTime.Now });
            }

            InitializeGprsClient();
        }

        private async void RefreshMenu_Click(object sender, EventArgs e)
        {
            await UpdateData(false);
        }

        private void mnuExpand_Click(object sender, EventArgs e)
        {
            ShowMe();
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show(
                                "Ви дійсно бажаєте вийти з програми?",
                                "Вихід",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Warning,
                                MessageBoxDefaultButton.Button2);

            if (confirmResult != DialogResult.Yes)
                return;

            notifyIcon.Visible = false;
            notifyIcon.Icon = null;
            notifyIcon.Dispose();

            _gprsClient.Dispose();

            Application.Exit();
        }

        private void DATASCANForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing)
                return;

            e.Cancel = true;
            Hide();
        }

        #endregion                

        #region Drag and Drop

        private void TrvEstimators_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Button != MouseButtons.Left || e.Item == null) return;

            var node = e.Item as TreeNode;

            if (node == null) return;

            var entity = node.Tag as EntityBase;

            if (entity is EstimatorsGroup || entity is EstimatorBase)
            {
                trvEstimators.DoDragDrop(e.Item, DragDropEffects.Move);
            }
        }

        private void TrvEstimators_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private async void TrvEstimators_DragDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("System.Windows.Forms.TreeNode", false)) return;

            var pt = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
            var destNode = ((TreeView)sender).GetNodeAt(pt);
            var newNode = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");

            if ((TreeView)sender == newNode.TreeView)
            {
                var estimatorsGroup = newNode.Tag as EstimatorsGroup;
                var estimator = newNode.Tag as EstimatorBase;
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
                            var customer = destNode.Tag as Customer;
                            estimatorsGroup.CustomerId = customer.Id;
                            estimatorsGroup.Customer = customer;
                        }

                        await _groupsService.Update(estimatorsGroup, () =>
                        {
                            Logger.Log(lstMessages, new LogEntry { Message = destNode == null ? $"Групу обчислювачів {estimatorsGroup} відкріплено від замовника" 
                                : $"Групу обчислювачів {estimatorsGroup} закріплено за замовником {estimatorsGroup.Customer}",
                                Status = LogStatus.Info, Type = LogType.System, Timestamp = DateTime.Now });
                        }, ex => LogException(ex.Message));
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
                            message = $"Обчислювач {estimator} відкріплено від замовників та груп";
                        }
                        else
                        {
                            var group = destNode.Tag as EstimatorsGroup;
                            var customer = destNode.Tag as Customer;
                            if (@group != null)
                            {
                                estimator.GroupId = @group.Id;
                                estimator.Group = @group;
                                estimator.CustomerId = @group.CustomerId;
                                estimator.Customer = _customers.SingleOrDefault(c => c.Id == @group.CustomerId);
                                message = $"Обчислювач {estimator} закріплено за групою {@group}";
                            }
                            else
                            {
                                estimator.GroupId = null;
                                estimator.Group = null;
                                estimator.CustomerId = customer.Id;
                                estimator.Customer = customer;
                                message = $"Обчислювач {estimator} закріплено за замовником {customer}";
                            }
                        }

                        await _estimatorsService.Update(estimator, () =>
                        {
                            Logger.Log(lstMessages, new LogEntry { Message = message, Status = LogStatus.Info, Type = LogType.System, Timestamp = DateTime.Now });
                        }, ex => LogException(ex.Message));
                    }
                }

                await UpdateData(false);
            }
            else
            {
                var estimator = newNode.Tag as EstimatorBase;

                if (estimator == null || destNode == null) return;

                if (!(destNode.Tag is PeriodicScan) && !(destNode.Tag is ScheduledScan)) return;

                var scan = (ScanBase)destNode.Tag;

                if (estimator is Floutec)
                {                    
                    var form = new EditFloutecScanMemberForm
                    {
                        StartPosition = FormStartPosition.CenterParent,
                        IsEdit = false,
                        ExistentMembers = GetFloutecMembers(),
                        ScanBaseId = scan.Id,
                        EstimatorId = estimator.Id
                    };

                    var result = form.ShowDialog();

                    if (result == DialogResult.OK)
                    {                        
                        ScanMemberBase member = form.Member;

                        await _scanMembersService.Insert(member, () =>
                        {
                            Logger.Log(lstMessages, new LogEntry { Message = $"Обчислювач {estimator} додано до опитування {scan}", Status = LogStatus.Info, Type = LogType.System, Timestamp = DateTime.Now });
                        }, ex => LogException(ex.Message));
                    }
                }
                else if (estimator is Roc809)
                {
                    var form = new EditRocScanMemberForm
                    {
                        StartPosition = FormStartPosition.CenterParent,
                        IsEdit = false,
                        ExistentMembers = GetRocMembers(),
                        ScanBaseId = scan.Id,
                        EstimatorId = estimator.Id
                    };

                    var result = form.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        ScanMemberBase member = form.Member;

                        await _scanMembersService.Insert(member, () =>
                        {
                            Logger.Log(lstMessages, new LogEntry { Message = $"Обчислювач {estimator} додано до опитування {scan}", Status = LogStatus.Info, Type = LogType.System, Timestamp = DateTime.Now });
                        }, ex => LogException(ex.Message));
                    }
                }

                await UpdateData(false);
            }
        }

        #endregion

        #region Helping methods

        private void LogException(string message)
        {
            Logger.Log(lstMessages, new LogEntry { Message = message, Status = LogStatus.Error, Type = LogType.System, Timestamp = DateTime.Now });
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == NativeMethods.WM_SHOWME)
            {
                ShowMe();
            }
            base.WndProc(ref m);
        }

        private void ShowMe()
        {
            Show();

            if (WindowState == FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Normal;
            }
        }

        private List<FloutecScanMember> GetFloutecMembers()
        {
            var members = new List<FloutecScanMember>();
            members.AddRange(_periodicScans.SelectMany(s => s.Members.OfType<FloutecScanMember>()));
            members.AddRange(_scheduledScans.SelectMany(s => s.Members.OfType<FloutecScanMember>()));

            return members;
        }

        private List<RocScanMember> GetRocMembers()
        {
            var members = new List<RocScanMember>();
            members.AddRange(_periodicScans.SelectMany(s => s.Members.OfType<RocScanMember>()));
            members.AddRange(_scheduledScans.SelectMany(s => s.Members.OfType<RocScanMember>()));

            return members;
        }

        #endregion

        #region Scanning

        private async void Timer_Tick(object sender, EventArgs e)
        {
            var scansToProcess = new List<ScanBase>();
            scansToProcess.AddRange(GetPeriodicScansToProcess());
            scansToProcess.AddRange(GetScheduledScansToProcess());

            var floutecMembers = scansToProcess.SelectMany(s => s.Members)
                .Where(m => m is FloutecScanMember && m.IsActive && _estimators.Single(f => f.Id == m.EstimatorId).IsActive);
            var rocMembers = scansToProcess.SelectMany(s => s.Members)
                .Where(m => m is RocScanMember && m.IsActive && _estimators.Single(r => r.Id == m.EstimatorId).IsActive);

            _floutecScanner.Process(_sqlConnection, floutecMembers, _estimators);
            _rocScanner.Process(_sqlConnection, rocMembers, _estimators);

            scansToProcess.ForEach(scan =>
            {
                scan.DateLastScanned = DateTime.Now;                
            });            

            await _entitiesService.Update(scansToProcess.OfType<EntityBase>().ToList(), null, ex => LogException(ex.Message));

            if (trvScans.Nodes.Count > 0)
            {
                foreach (TreeNode node in trvScans.Nodes[0].Nodes)
                {
                    var scan = node.Tag as PeriodicScan;
                    node.ToolTipText = scan.Info();
                }
            }

            if (trvScans.Nodes.Count > 1)
            {
                foreach (TreeNode node in trvScans.Nodes[1].Nodes)
                {
                    var scan = node.Tag as ScheduledScan;
                    node.ToolTipText = scan.Info();
                }
            }
        }

        private IEnumerable<PeriodicScan> GetPeriodicScansToProcess()
        {
            return _periodicScans.Where(s => s.IsActive 
                && (!s.DateLastScanned.HasValue 
                || s.DateLastScanned.Value.AddMinutes(!s.PeriodType ? s.Period : s.Period * 60).CompareTo(DateTime.Now) < 0));
        }

        private IEnumerable<ScheduledScan> GetScheduledScansToProcess()
        {
            var prev = DateTime.Now.TimeOfDay.Subtract(new TimeSpan(0, 0, 0, 0, SCAN_PERIOD_MS / 2));
            var next = DateTime.Now.TimeOfDay.Add(new TimeSpan(0, 0, 0, 0, SCAN_PERIOD_MS / 2));

            return _scheduledScans.Where(s => s.IsActive
                && s.Periods.Select(p => p.Period)
                    .Any(p => p > prev && p < next));
        }

        #endregion        
    }
}
