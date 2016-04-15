using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GIC.Common;
using GIC.Business;
using ATSData;
using ATSData.ATSDataService;
using ATSData.ProductService;
using ATSData.DashboardService;
using ATSData.ATSUtilityService;

namespace MainForm
{
    public partial class MainForm : Form
    {
        public static DataTable ATSDataTable = null;

        public MainForm()
        {
            InitializeComponent();
        }

        private void getOnhandByLocation_Click(object sender, EventArgs e)
        {
            try
            {
                totalToolStripStatusLabel.Text =
                    string.Format("{0} - Start to fetch ATS On-hand by location via ATS Web Service...", DateTime.Now.ToString());

                StartSynchronizedJob("ATS_OnhandByLocation_Report");
                ATSDataGridView.DataSource = GIC.Business.ReportHandler.GIC_Context.ATSDataTable;

                totalToolStripStatusLabel.Text = string.Format("{0} - Done! Total of items: {1}",
                    DateTime.Now.ToString(),
                    GIC.Business.ReportHandler.GIC_Context.ATSDataTable.Rows.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                        string.Format("Message:{0}, Source:{1}, StackTrack:{2}", ex.Message, ex.Source, ex.StackTrace));
                throw ex;
            }
        }

        private void getFacilityButton_Click(object sender, EventArgs e)
        {
            try
            {
                totalToolStripStatusLabel.Text = 
                    string.Format("{0} - Start to fetch ATS Data via ATS Web Service...", DateTime.Now.ToString());

                StartSynchronizedJob("ATS_Facility_Report");
                ATSDataGridView.DataSource = GIC.Business.ReportHandler.GIC_Context.FullfillmentFacilities;
                
                totalToolStripStatusLabel.Text = string.Format("{0} - Done! Total of items: {1}",
                    DateTime.Now.ToString(),
                    GIC.Business.ReportHandler.GIC_Context.FullfillmentFacilities.Length);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                        string.Format("Message:{0}, Source:{1}, StackTrack:{2}", ex.Message, ex.Source, ex.StackTrace));
                throw ex;
            }
        }

        private void getCountriesButton_Click(object sender, EventArgs e)
        {
            try
            {
                totalToolStripStatusLabel.Text =
                    string.Format("{0} - Start to fetch ATS Data via ATS Web Service...", DateTime.Now.ToString());

                StartSynchronizedJob("ATS_Country_Report");
                ATSDataGridView.DataSource = GIC.Business.ReportHandler.GIC_Context.CountryItems;

                totalToolStripStatusLabel.Text = string.Format("{0} - Done! Total of items: {1}",
                    DateTime.Now.ToString(),
                    GIC.Business.ReportHandler.GIC_Context.CountryItems.Length);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                        string.Format("Message:{0}, Source:{1}, StackTrack:{2}", ex.Message, ex.Source, ex.StackTrace));
                throw ex;
            }
        }

        private void getRegionButton_Click(object sender, EventArgs e)
        {
            try
            {
                totalToolStripStatusLabel.Text =
                    string.Format("{0} - Start to fetch ATS Data via ATS Web Service...", DateTime.Now.ToString());

                StartSynchronizedJob("ATS_Region_Report");
                ATSDataGridView.DataSource = GIC.Business.ReportHandler.GIC_Context.RegionItems;

                totalToolStripStatusLabel.Text = string.Format("{0} - Done! Total of items: {1}",
                    DateTime.Now.ToString(),
                    GIC.Business.ReportHandler.GIC_Context.RegionItems.Length);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                        string.Format("Message:{0}, Source:{1}, StackTrack:{2}", ex.Message, ex.Source, ex.StackTrace));
                throw ex;
            }
        }
                
        private void fetchInTransitDataButton_Click(object sender, EventArgs e)
        {
            try
            {
                totalToolStripStatusLabel.Text =
                    string.Format("{0} - Start to fetch ATS Data via ATS Web Service...", DateTime.Now.ToString());

                StartSynchronizedJob("ATS_In-Transit_Report");
                ATSDataGridView.DataSource = GIC.Business.ReportHandler.GIC_Context.ATSDataTable;

                totalToolStripStatusLabel.Text = string.Format("{0} - Done! Total of items: {1}",
                    DateTime.Now.ToString(),
                    GIC.Business.ReportHandler.GIC_Context.ATSDataTable.Rows.Count);

                MessageBox.Show(
                        string.Format("ATS report is exported into the folder - {0} successful", ConfigFileUtility.GetValue("OutputFolder")),
                        "Export report",
                        MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                        string.Format("Message:{0}, Source:{1}, StackTrack:{2}", ex.Message, ex.Source, ex.StackTrace));
                throw ex;
            }
        }

        private void fetchOnHandDataButton_Click(object sender, EventArgs e)
        {
            try
            {
                totalToolStripStatusLabel.Text =
                    string.Format("{0} - Start to fetch ATS Data via ATS Web Service...", DateTime.Now.ToString());

                StartSynchronizedJob("ATS_OnHand_Report");
                ATSDataGridView.DataSource = GIC.Business.ReportHandler.GIC_Context.ATSDataTable;

                totalToolStripStatusLabel.Text = string.Format("{0} - Done! Total of items: {1}",
                    DateTime.Now.ToString(),
                    GIC.Business.ReportHandler.GIC_Context.ATSDataTable.Rows.Count);

                MessageBox.Show(
                        string.Format("ATS report is exported into the folder - {0} successful", ConfigFileUtility.GetValue("OutputFolder")),
                        "Export report",
                        MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                        string.Format("Message:{0}, Source:{1}, StackTrack:{2}", ex.Message, ex.Source, ex.StackTrace));
                throw ex;
            }
        }

        private void fetchCommittedOrdersButton_Click(object sender, EventArgs e)
        {
            try
            {
                totalToolStripStatusLabel.Text =
                    string.Format("{0} - Start to fetch ATS Data via ATS Web Service...", DateTime.Now.ToString());

                StartSynchronizedJob("ATS_Backlog_Order_Report");
                ATSDataGridView.DataSource = GIC.Business.ReportHandler.GIC_Context.ATSDataTable;

                totalToolStripStatusLabel.Text = string.Format("{0} - Done! Total of items: {1}",
                    DateTime.Now.ToString(),
                    GIC.Business.ReportHandler.GIC_Context.ATSDataTable.Rows.Count);

                MessageBox.Show(
                        string.Format("ATS report is exported into the folder - {0} successful", ConfigFileUtility.GetValue("OutputFolder")),
                        "Export report",
                        MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                        string.Format("Message:{0}, Source:{1}, StackTrack:{2}", ex.Message, ex.Source, ex.StackTrace));
                throw ex;
            }
        }

        private void fetchGetFallOutOrdersButton_Click(object sender, EventArgs e)
        {
            try
            {
                totalToolStripStatusLabel.Text =
                    string.Format("{0} - Start to fetch ATS Data via ATS Web Service...", DateTime.Now.ToString());

                StartSynchronizedJob("ATS_FallOutOrder_Report");
                ATSDataGridView.DataSource = GIC.Business.ReportHandler.GIC_Context.ATSDataTable;

                totalToolStripStatusLabel.Text = string.Format("{0} - Done! Total of items: {1}",
                    DateTime.Now.ToString(),
                    GIC.Business.ReportHandler.GIC_Context.ATSDataTable.Rows.Count);

                MessageBox.Show(
                        string.Format("ATS report is exported into the folder - {0} successful", ConfigFileUtility.GetValue("OutputFolder")),
                        "Export report",
                        MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                        string.Format("Message:{0}, Source:{1}, StackTrack:{2}", ex.Message, ex.Source, ex.StackTrace));
                throw ex;
            }
        }

        #region ------------------------------ Backgroundworker ------------------------------
        private ProgressForm progressform = null;
        private bool IsError = false;

        public void StartSynchronizedJob(object instance)
        {
            // Create a background thread
            BackgroundWorker backgroundworker = new BackgroundWorker();
            backgroundworker.WorkerSupportsCancellation = true;
            backgroundworker.WorkerReportsProgress = true;
            backgroundworker.DoWork += new DoWorkEventHandler(DoWork);
            backgroundworker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(RunWorkerCompleted);
            backgroundworker.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged);

            // Create a progress form on the UI thread
            progressform = new ProgressForm();

            // Kick off the Async thread
            backgroundworker.RunWorkerAsync(instance);

            // Lock up the UI with this modal progress form.
            progressform.ShowDialog(this);
            progressform = null;
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            string functionname = (string)e.Argument;
            ReportHandler handler = new ReportHandler(functionname);
            handler.Process();
        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //progressform.ElapsedTimeLabel.Invoke(
            //    (MethodInvoker)delegate()
            //    {
            //        timer1 = new System.Windows.Forms.Timer();
            //        timer1.Interval = 1000;
            //        timer1.Tick += new EventHandler(TimerEvenProcessor);
            //        timer1.Start();
            //        progressform.progressBar1.Value = Convert.ToInt32(intSecond * (100.0 / 59));
            //    }
            //);
        }

        private void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // The background process is complete. First we should hide the
            // modal Progress Form to unlock the UI. The we need to inspect our
            // response to see if an error occured, a cancel was requested or
            // if we completed succesfully.

            // Hide the Progress Form
            if (progressform != null)
            {
                progressform.Hide();
                progressform = null;
            }

            // Check to see if an error occured in the 
            // background process.
            if (e.Error != null)
            {
                IsError = true;
                MessageBox.Show(e.Error.Message);
                return;
            }

            // Check to see if the background process was cancelled.
            if (e.Cancelled)
            {
                MessageBox.Show("Processing cancelled!", "Cancel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Everything completed normally.
            // process the response using e.Result            
            //MessageBox.Show("Processing is complete!", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        private void testButton_Click(object sender, EventArgs e)
        {
            ReportHandler handler = new ReportHandler("ATS_ONHANDBYLOCATION", "AMER");
            handler.Process();
        }
    }

    #region --- Discard code ---
    //private void getProductInfoButton_Click(object sender, EventArgs e)
    //{
    //ATSData.ProductService.ProductServiceClient client = new ProductServiceClient();
    //ATSData.ProductService.RegionItem[] itemarray = client.GetProductDefaults();
    //dataGridView1.DataSource = itemarray;  
    //}

    //private void onhandByLocationButton_Click(object sender, EventArgs e)
    //{
    //    ATSData.ProductService.ProductServiceClient client = new ProductServiceClient();
    //    ATSData.ProductService.OnHandByLocationRequest request = new OnHandByLocationRequest();
    //    request.ProductCountryId = 12345;
    //    request.SKU = "";
    //    ATSData.ProductService.OnHandByLocation[] itemarray = client.OnHandByLocationInformation(request);
    //}

    //private void fetchOnHandButton_Click(object sender, EventArgs e)
    //{
    //    ATSData.ATSDataService.GOPServiceClient client = new GOPServiceClient();
    //    ATSData.ATSDataService.OnHandRequest onrequest = new OnHandRequest();
    //    onrequest.Identifier = 5616;
    //    ATSData.ATSDataService.FetchOnHandResponse response = client.FetchOnHand(onrequest);
    //    dataGridView1.DataSource = response.OnHandLocationQty;
    //}
    #endregion
}
