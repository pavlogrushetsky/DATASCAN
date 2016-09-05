using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using DATASCAN.Infrastructure.Logging;
using DATASCAN.Infrastructure.Settings;
using DATASCAN.Model;
using DATASCAN.Model.Floutecs;
using DATASCAN.Model.Rocs;
using DATASCAN.Model.Scanning;
using DATASCAN.Repositories;

namespace DATASCAN.View
{
    public partial class DATASCANForm : Form
    {
        private List<Customer> _customers;

        private List<Floutec> _floutecs;

        private List<Roc809> _rocs;

        private List<FloutecMeasureLine> _floutecLines;

        private List<Roc809MeasurePoint> _rocPoints;

        private List<PeriodicScan> _periodicScans;

        private List<ScheduledScan> _scheduledScans;

        private List<ScanMember> _scanMembers;

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
                ConnectTimeout = 10
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

        private void UpdateData()
        {
            try
            {
                using (EntityRepository<Customer> repo = new EntityRepository<Customer>(_sqlConnection))
                {
                    _customers = new List<Customer>();
                    _customers = repo.GetAll().ToList();

                    if (!_customers.Any())
                    {
                        Logger.Log(lstMessages, new LogEntry { Message = "Дані в базі даних відсутні", Status = LogStatus.Info, Type = LogType.System, Timestamp = DateTime.Now });
                        return;
                    }

                    _floutecs = new List<Floutec>();
                    _customers.ForEach(c =>
                    {
                        _floutecs.AddRange(c.Estimators.OfType<Floutec>().OrderBy(o => o.Address).ToList());
                    });

                    _rocs = new List<Roc809>();
                    _customers.ForEach(c =>
                    {
                        _rocs.AddRange(c.Estimators.OfType<Roc809>().OrderBy(o => o.Address).ToList());
                    });

                    _floutecLines = new List<FloutecMeasureLine>();
                    _floutecs.ForEach(f =>
                    {
                        _floutecLines.AddRange(f.MeasurePoints.OfType<FloutecMeasureLine>().OrderBy(o => o.Number).ToList());
                    });

                    _rocPoints = new List<Roc809MeasurePoint>();
                    _rocs.ForEach(r =>
                    {
                        _rocPoints.AddRange(r.MeasurePoints.OfType<Roc809MeasurePoint>().OrderBy(o => o.Number).ToList());
                    });
                }

                using (EntityRepository<ScanBase> repo = new EntityRepository<ScanBase>(_sqlConnection))
                {
                    _periodicScans = new List<PeriodicScan>();
                    _scheduledScans = new List<ScheduledScan>();

                    _periodicScans = repo.GetAll().OfType<PeriodicScan>().ToList();
                    _scheduledScans = repo.GetAll().OfType<ScheduledScan>().ToList();

                    _scanMembers = new List<ScanMember>();

                    _periodicScans.ForEach(s =>
                    {
                        _scanMembers.AddRange(s.Members);
                    });

                    _scheduledScans.ForEach(s =>
                    {
                        _scanMembers.AddRange(s.Members);
                    });
                }
            }
            catch (Exception ex)
            {
                Logger.Log(lstMessages, new LogEntry { Message = ex.Message, Status = LogStatus.Error, Type = LogType.System, Timestamp = DateTime.Now });
            }
        }
    }
}
