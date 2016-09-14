using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DATASCAN.Infrastructure.Logging;
using DATASCAN.Infrastructure.Settings;
using DATASCAN.Model;
using DATASCAN.Model.Common;
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

            estimatorsMenu.Items.AddRange(new ToolStripItem[]
            {
                new ToolStripMenuItem("Додати замовника", null, EditCustomerMenu_Click),
                new ToolStripMenuItem("Додати групу обчислювачів", null, EditGroupMenu_Click),
                new ToolStripMenuItem("Додати обчислювач ФЛОУТЕК", null, AddFloutecMenu_Click),
                new ToolStripMenuItem("Додати обчислювач ROC809", null, AddRocMenu_Click)
            });

            estimatorsMenu.Opening += ContextMenu_Opening;

            trvEstimators.ContextMenuStrip = estimatorsMenu;

            trvEstimators.AllowDrop = true;
            trvEstimators.ItemDrag += TrvEstimators_ItemDrag;
            trvEstimators.DragEnter += TrvEstimators_DragEnter;
            trvEstimators.DragDrop += TrvEstimators_DragDrop;
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

                using (EntityRepository<ScanBase> repo = new EntityRepository<ScanBase>(_sqlConnection))
                {
                    _scans = repo.GetAll()
                        .Include(s => s.Members)
                        .ToList();
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

            FillCustomers();

            FillGroups();

            FillEstimators();  
            
            trvEstimators.ExpandAll();          
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
                    new ToolStripMenuItem("Додати групу обчислювачів", null, EditGroupMenu_Click),
                    new ToolStripMenuItem("Додати обчислювач ФЛОУТЕК", null, AddFloutecMenu_Click),
                    new ToolStripMenuItem("Додати обчислювач ROC809", null, AddRocMenu_Click),
                    new ToolStripSeparator(),
                    new ToolStripMenuItem("Інформація", Resources.Information, EditCustomerMenu_Click),
                    new ToolStripSeparator(), 
                    customer.IsActive ? new ToolStripMenuItem("Деактивувати", Resources.Deactivate, DeactivateMenu_Click) : new ToolStripMenuItem("Активувати", Resources.Activate, ActivateMenu_Click),
                    new ToolStripMenuItem("Видалити", Resources.Delete, DeleteMenu_Click)
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
                    new ToolStripMenuItem("Додати обчислювач ФЛОУТЕК", null, AddFloutecMenu_Click),
                    new ToolStripMenuItem("Додати обчислювач ROC809", null, AddRocMenu_Click),
                    new ToolStripSeparator(),
                    new ToolStripMenuItem("Інформація", Resources.Information, EditGroupMenu_Click),
                    new ToolStripSeparator(),
                    group.IsActive ? new ToolStripMenuItem("Деактивувати", Resources.Deactivate, DeactivateMenu_Click) : new ToolStripMenuItem("Активувати", Resources.Activate, ActivateMenu_Click),
                    new ToolStripMenuItem("Видалити", Resources.Delete, DeleteMenu_Click)
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
                    estimator is Floutec ? new ToolStripMenuItem("Додати нитку вимірювання", null, AddPointMenu_Click) : new ToolStripMenuItem("Додати точку вимірювання", null, AddPointMenu_Click),
                    new ToolStripSeparator(),
                    new ToolStripMenuItem("Налаштування", Resources.Settings, EstimatorSettingsMenu_Click),
                    new ToolStripSeparator(),
                    estimator.IsActive ? new ToolStripMenuItem("Деактивувати", Resources.Deactivate, DeactivateMenu_Click) : new ToolStripMenuItem("Активувати", Resources.Activate, ActivateMenu_Click),
                    new ToolStripMenuItem("Видалити", Resources.Delete, DeleteMenu_Click)
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
                            new ToolStripMenuItem("Налаштування", Resources.Settings, PointSettingsMenu_Click),
                            new ToolStripSeparator(),
                            point.IsActive ? new ToolStripMenuItem("Деактивувати", Resources.Deactivate, DeactivateMenu_Click) : new ToolStripMenuItem("Активувати", Resources.Activate, ActivateMenu_Click),
                            new ToolStripMenuItem("Видалити", Resources.Delete, DeleteMenu_Click)
                        });

                        pointMenu.Opening += ContextMenu_Opening;

                        pointNode.ContextMenuStrip = pointMenu;
                    }
                });
            }            
        }

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

        private void EditCustomerMenu_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;

            TreeNode node = trvEstimators.SelectedNode;

            Customer customer = node?.Tag as Customer;

            EditCustomerForm form = new EditCustomerForm
            {
                StartPosition = FormStartPosition.CenterParent,
                IsEdit = menuItem != null && menuItem.Text.Equals("Інформація"),
                Customer = customer
            };

            DialogResult result = form.ShowDialog();

            if (result == DialogResult.OK && form.Customer != null)
            {
                using (EntityRepository<Customer> repo = new EntityRepository<Customer>(_sqlConnection))
                {
                    if (form.IsEdit)
                    {
                        customer = form.Customer;
                        customer.DateModified = DateTime.Now;
                        repo.Update(customer);
                    }
                    else
                    {
                        repo.Insert(form.Customer);
                    }
                }

                UpdateData();
            }
        }

        private void EditGroupMenu_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;

            TreeNode node = trvEstimators.SelectedNode;

            Customer customer = node?.Tag as Customer;

            EstimatorsGroup group = node?.Tag as EstimatorsGroup;

            EditEstimatorsGroupForm form = new EditEstimatorsGroupForm
            {
                StartPosition = FormStartPosition.CenterParent,
                IsEdit = menuItem != null && menuItem.Text.Equals("Інформація"),
                Group = group
            };

            DialogResult result = form.ShowDialog();

            if (result == DialogResult.OK && form.Group != null)
            {
                using (EntityRepository<EstimatorsGroup> repo = new EntityRepository<EstimatorsGroup>(_sqlConnection))
                {
                    group = form.Group;

                    if (form.IsEdit)
                    {                        
                        group.DateModified = DateTime.Now;
                        repo.Update(group);
                    }
                    else
                    {
                        if (customer != null)
                        {
                            group.CustomerId = customer.Id;                            
                        }

                        repo.Insert(group);
                    }
                }

                UpdateData();
            }
        }

        private void AddFloutecMenu_Click(object sender, EventArgs e)
        {

        }

        private void AddRocMenu_Click(object sender, EventArgs e)
        {

        }

        private void AddPointMenu_Click(object sender, EventArgs e)
        {

        }

        private void EstimatorSettingsMenu_Click(object sender, EventArgs e)
        {

        }

        private void PointSettingsMenu_Click(object sender, EventArgs e)
        {

        }

        private void DeactivateMenu_Click(object sender, EventArgs e)
        {
            TreeNode node = trvEstimators.SelectedNode;

            EntityBase entity = node?.Tag as EntityBase;

            if (entity != null)
            { 
                using (EntityRepository<EntityBase> repo = new EntityRepository<EntityBase>(_sqlConnection))
                {
                    entity.IsActive = false;
                    repo.Update(entity);
                }

                UpdateData();
            }
        }

        private void ActivateMenu_Click(object sender, EventArgs e)
        {
            TreeNode node = trvEstimators.SelectedNode;

            EntityBase entity = node?.Tag as EntityBase;

            if (entity != null)
            {
                using (EntityRepository<EntityBase> repo = new EntityRepository<EntityBase>(_sqlConnection))
                {
                    entity.IsActive = true;
                    repo.Update(entity);
                }

                UpdateData();
            }
        }

        private void DeleteMenu_Click(object sender, EventArgs e)
        {

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

        private void TrvEstimators_DragDrop(object sender, DragEventArgs e)
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
                        if (destNode == null)
                        {
                            estimatorsGroup.CustomerId = null;
                            estimatorsGroup.Customer = null;
                        }
                        else
                        {
                            Customer customer = destNode.Tag as Customer;
                            if (customer != null)
                            {
                                estimatorsGroup.CustomerId = customer.Id;
                                estimatorsGroup.Customer = customer;
                            }
                        }

                        using (EntityRepository<EstimatorsGroup> repo = new EntityRepository<EstimatorsGroup>(_sqlConnection))
                        {
                            repo.Update(estimatorsGroup);
                        }
                    }
                    else if (newNode.Tag is EstimatorBase)
                    {
                        if (destNode == null)
                        {
                            estimator.CustomerId = null;
                            estimator.Customer = null;
                            estimator.GroupId = null;
                            estimator.Group = null;
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
                            }
                            else if (customer != null)
                            {
                                estimator.GroupId = null;
                                estimator.Group = null;
                                estimator.CustomerId = customer.Id;
                                estimator.Customer = customer;
                            }
                        }

                        using (EntityRepository<EstimatorBase> repo = new EntityRepository<EstimatorBase>(_sqlConnection))
                        {
                            repo.Update(estimator);
                        }
                    }

                    UpdateData();
                }
            }
        }

        #endregion

    }
}
