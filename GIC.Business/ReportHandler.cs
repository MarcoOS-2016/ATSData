using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.ServiceModel;
using System.Runtime.Serialization;
using ATSData;
using ATSData.ATSDataService;
using ATSData.ProductService;
using ATSData.DashboardService;
using ATSData.ATSUtilityService;
using GIC.Common;

namespace GIC.Business
{
    #region --- GIC Context ---
    public class GIC_Context
    {
        private ATSData.ATSDataService.Fullfillmentfacility[] fullfillmentfacilities;
        private ATSData.ProductService.CountryItem[] countryitems;
        private ATSData.ProductService.RegionItem[] regionitems;
        private ATSData.DashboardService.Dashboard[] dashboards;
        private ATSData.ProductService.OnHandByLocation[] onhandbylocation;
        private ATSData.ATSUtilityService.InTransitHistoryResults[] intransithistoryresults;
        private DataTable atsdatatable;

        public ATSData.ATSDataService.Fullfillmentfacility[] FullfillmentFacilities
        {
            get { return fullfillmentfacilities; }
            set { fullfillmentfacilities = value; }
        }

        public ATSData.ProductService.CountryItem[] CountryItems
        {
            get { return countryitems; }
            set { countryitems = value; }
        }

        public ATSData.ProductService.RegionItem[] RegionItems
        {
            get { return regionitems; }
            set { regionitems = value; }
        }

        public ATSData.DashboardService.Dashboard[] Dashboards
        {
            get { return dashboards; }
            set { dashboards = value; }
        }

        public ATSData.ProductService.OnHandByLocation[] OnHandByLocation
        {
            get { return onhandbylocation; }
            set { onhandbylocation = value; }
        }

        public ATSData.ATSUtilityService.InTransitHistoryResults[] InTransitHistoryResults
        {
            get { return intransithistoryresults; }
            set { intransithistoryresults = value; }
        }

        public DataTable ATSDataTable
        {
            get { return atsdatatable; }
            set { atsdatatable = value; }
        }
    }
    #endregion

    public class ReportHandler
    {
        public static GIC_Context GIC_Context = new GIC_Context();
        private string reportname = string.Empty;

        public string ReportName
        {
            set { this.reportname = value; }
            get { return this.reportname; }
        }

        public ReportHandler(string reportName)
        {
            this.reportname = reportName;
        }

        public void Process()
        {
            switch (this.ReportName)
            {
                case "ATS_OnhandByLocation_Report":                
                case "ATS_ONHANDBYLOCATION":
                    GetOnhandByLocationInformation();
                    ExportDataIntoExcelFile(this.ReportName);
                    break;

                case "ATS_Facility_Report":
                    GetFacilityData();
                    //ExportDataIntoExcelFile(this.ReportName);
                    break;

                case "ATS_Country_Report":
                    GetCountriesData();
                    //ExportDataIntoExcelFile(this.ReportName);
                    break;

                case "ATS_Region_Report":
                    GetRegionData();
                    //ExportDataIntoExcelFile(this.ReportName);
                    break;

                case "ATS_OnHand_Report":
                case "ATS_ONHAND":
                    FetchOnHandInventoryData();
                    ExportDataIntoExcelFile(this.ReportName);
                    break;

                case "ATS_In-Transit_Report":
                case "ATS_INTRANSIT":
                    FetchInTransitInventoryData();
                    ExportDataIntoExcelFile(this.ReportName);
                    break;

                case "ATS_Backlog_Order_Report":
                case "ATS_BACKLOG_ORDER":
                    FetchBacklogOrders();
                    ExportDataIntoExcelFile(this.ReportName);
                    break;

                case "ATS_FallOutOrder_Report":
                case "ATS_FallOutOrder":
                    GetFallOutOrders();
                    //ExportDataIntoExcelFile(this.ReportName);
                    break;
            }
        }

        private void GetOnhandByLocationInformation()
        {
            Console.WriteLine(string.Format("[{0}] - Starting to fetch ATS On-hand inventory data with location via calling ATS Web Service...",
                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            MiscUtility.LogHistory("Starting to fetch ATS On-hand inventory data with location via calling ATS Web Service...");

            string countryCodeList = ConfigFileUtility.GetValue("EMEACountryCodeList");

            ATSData.DashboardService.DashboardRequest dashboardrequest = new DashboardRequest();
            dashboardrequest.CountryId = 0;
            dashboardrequest.RegionId = 0;
            dashboardrequest.SalesChannel = 2;
            dashboardrequest.SellAction = 0;
            dashboardrequest.StockStatus = 0;
            dashboardrequest.IsActive = "B";
            dashboardrequest.Type = 0;

            ATSData.DashboardService.DashboardServiceClient dashboardserviceclient = new DashboardServiceClient();
            ATSData.DashboardService.Dashboard[] dashboards = dashboardserviceclient.FetchProducts(dashboardrequest);
            GIC_Context.Dashboards = dashboards;

            ATSData.ProductService.ProductServiceClient client = new ProductServiceClient();
            OnHandByLocationRequest request = new OnHandByLocationRequest();
            //request.ProductCountryId = 19693;
            //request.SKU = "210-ABIH";
            //ATSData.ProductService.OnHandByLocation[] onHandByLocation = client.OnHandByLocationInformation(request);
            //GIC_Context.OnHandByLocation = onHandByLocation;

            DataTable datatable = BuildOnHandWithLocationDataTable();
            DataRow dr = null;

            for (int index = 0; index < dashboards.Length; index++)
            {
                foreach (string countrycode in countryCodeList.Split(','))
                {
                    // Only fetch EMEA on-hand inventory without 0 quantity
                    if (dashboards[index].Country.Equals(countrycode) && dashboards[index].Inventory != 0 )
                    {
                        request.ProductCountryId = dashboards[index].ProductCountryId.Value;
                        request.SKU = dashboards[index].SKU;
                        ATSData.ProductService.OnHandByLocation[] onHandByLocation = client.OnHandByLocationInformation(request);

                        if (onHandByLocation.Length > 0)
                        {
                            for (int indey = 0; indey < onHandByLocation.Length; indey++)
                            {
                                dr = datatable.NewRow();

                                dr["Product Contry ID"] = request.ProductCountryId;
                                dr["Unique Num"] = string.Format("{0}-{1}", onHandByLocation[indey].LocationCode, dashboards[index].FGAId);
                                dr["Country"] = dashboards[index].Country;
                                dr["Sku"] = dashboards[index].SKU;
                                dr["PN"] = dashboards[index].FGAId;
                                dr["Location Code"] = onHandByLocation[indey].LocationCode;
                                dr["Quantity"] = onHandByLocation[indey].OnHandAmount;
                                dr["Source"] = onHandByLocation[indey].Source;
                                dr["Updated Date"] = onHandByLocation[indey].InventoryStatusChangedDateTime;

                                //string text = string.Format("Index: {0}, Region:{1}, Country:{2}, PN:{3}, Location Code:{4}, Quantity:{5}, Source:{6}, Updated Date:{7}, Product Country ID:{8}, Sku:{9}",
                                //    index,
                                //    dashboards[index].RegionId,
                                //    dashboards[index].Country,
                                //    dashboards[index].FGAId,
                                //    onHandByLocation[indey].LocationCode,
                                //    onHandByLocation[indey].OnHandAmount,
                                //    onHandByLocation[indey].Source,
                                //    onHandByLocation[indey].InventoryStatusChangedDateTime,
                                //    dashboards[index].ProductCountryId.Value,
                                //    dashboards[index].SKU);

                                //FileUtility.SaveFile("Log.txt", text);

                                datatable.Rows.Add(dr);
                            }
                        }
                    }
                }
            }

            GIC_Context.ATSDataTable = datatable;

            Console.WriteLine(string.Format("[{0}] - Done!", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            MiscUtility.LogHistory("Done!");
        }

        private void GetFacilityData()
        {
            ATSData.ATSDataService.GOPServiceClient client = new ATSData.ATSDataService.GOPServiceClient();
            ATSData.ATSDataService.FullfillmentfacilityResponse fullFillMentFacilityResponse = client.GetFullFillmentLocations();
            GIC_Context.FullfillmentFacilities = fullFillMentFacilityResponse.Fullfillmentfacility;
        }

        private void GetCountriesData()
        {
            ATSData.ProductService.ProductServiceClient client = new ATSData.ProductService.ProductServiceClient();
            ATSData.ProductService.CountryItem[] countryItems = client.GetAllCountries();
            GIC_Context.CountryItems = countryItems;
        }

        private void GetRegionData()
        {
            ATSData.ProductService.ProductServiceClient client = new ProductServiceClient();
            ATSData.ProductService.RegionItem[] regionItems = client.GetAllRegions();
            GIC_Context.RegionItems = regionItems;
        }

        private void FetchOnHandInventoryData()
        {
            Console.WriteLine(string.Format("[{0}] - Starting to fetch ATS On-hand inventory data via calling ATS Web Service...",
                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            MiscUtility.LogHistory("Starting to fetch ATS On-hand inventory data via calling ATS Web Service...");

            ATSData.DashboardService.DashboardRequest dashboardrequest = new DashboardRequest();
            dashboardrequest.CountryId = 0;
            dashboardrequest.RegionId = 0;
            dashboardrequest.SalesChannel = 2;
            dashboardrequest.SellAction = 0;
            dashboardrequest.StockStatus = 0;
            dashboardrequest.IsActive = "B";
            dashboardrequest.Type = 0;

            ATSData.DashboardService.DashboardServiceClient dashboardserviceclient = new DashboardServiceClient();
            ATSData.DashboardService.Dashboard[] dashboards = dashboardserviceclient.FetchProducts(dashboardrequest);
            GIC_Context.Dashboards = dashboards;

            DataTable datatable = BuildOnHandDataTable();
            DataRow dr = null;

            for (int index = 0; index < dashboards.Length; index++)
            {
                if (dashboards[index].Active.ToString() == "1")
                {                    
                    dr = datatable.NewRow();
                    dr["Brand Name"] = dashboards[index].BrandName;
                    dr["Offer Type"] = dashboards[index].OfferType;

                    if (dashboards[index].SKU.Length == 5 && dashboards[index].FGAId.Length > 5)
                    {
                        dr["PN"] = dashboards[index].SKU;
                        dr["SKU"] = dashboards[index].FGAId;
                    }
                    else
                    {
                        dr["PN"] = dashboards[index].FGAId;
                        dr["SKU"] = dashboards[index].SKU;
                    }

                    dr["Catalog(s)"] = dashboards[index].Catalog;
                    dr["SV"] = dashboards[index].SalesViewVisibility;
                    dr["LT"] = dashboards[index].DisplayLeadTime;
                    dr["ATS"] = dashboards[index].Available;
                    dr["Online"] = dashboards[index].InventoryStatus;
                    dr["Offline"] = dashboards[index].InventoryStatusOffLine;
                    dr["SA Online"] = dashboards[index].SellAction;
                    dr["SA Offline"] = dashboards[index].SellActionOffLine;
                    dr["On Hand"] = dashboards[index].Inventory;
                    dr["Commit"] = dashboards[index].Committed;
                    dr["Checkout"] = dashboards[index].PostCheckout;
                    dr["Backlog"] = Convert.ToInt32(dashboards[index].Committed) + Convert.ToInt32(dashboards[index].PostCheckout);
                    dr["Cart"] = dashboards[index].PreCheckout;
                    dr["Type"] = dashboards[index].IsFGA;
                    dr["State"] = "Active";

                    //dr["ProductCountryId"] = dashboards[index].ProductCountryId;

                    //if (dashboards[index].Active.ToString() == "1")
                    //    dr["State"] = "Active";
                    //else if (dashboards[index].Active.ToString() == "0")
                    //    dr["State"] = "Inactive";

                    datatable.Rows.Add(dr);
                }
            }

            for (int index = 0; index < datatable.Rows.Count; index++)
            {
                if ((datatable.Rows[index]["PN"].ToString().Contains('-') && Convert.ToInt32(datatable.Rows[index]["ATS"]) == 0)
                    || (datatable.Rows[index]["PN"].ToString().Length == 0 && Convert.ToInt32(datatable.Rows[index]["ATS"]) == 0))
                {
                    datatable.Rows[index].Delete();
                    datatable.Rows[index].AcceptChanges();
                }
            }

            GIC_Context.ATSDataTable = datatable;

            Console.WriteLine(string.Format("[{0}] - Done!", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            MiscUtility.LogHistory("Done!");
        }

        private void FetchInTransitInventoryData()
        {
            Console.WriteLine(string.Format("[{0}] - Starting to fetch ATS In-Transit inventory data via calling ATS Web Service...",
                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            MiscUtility.LogHistory("Starting to fetch ATS In-Transit inventory data via calling ATS Web Service..");

            ATSData.ProductService.ProductServiceClient productserviceclient = new ATSData.ProductService.ProductServiceClient();            
            ATSData.ProductService.CountryItem[] countries = productserviceclient.GetAllCountries();

            DataTable datatable = BuildInTransitDataTable();
            DataRow dr = null;            

            for (int index = 0; index < countries.Length; index++)
            {
                //if (countries[index].ID == 12)
                //{
                ATSData.ATSUtilityService.ATSUtilityServiceClient atsutilityserviceclient = new ATSUtilityServiceClient();
                ATSData.ATSUtilityService.InTransitHistoryResults[] inTransitHistoryResults
                    = atsutilityserviceclient.GetInTransitHistory(Convert.ToInt16(countries[index].ID));
                //dataGridView1.DataSource = itemarray;

                for (int rowindex = 0; rowindex < inTransitHistoryResults.Length; rowindex++)
                {
                    dr = datatable.NewRow();
                    
                    dr["Country"] = countries[index].Name;
                    dr["ASNNumber"] = inTransitHistoryResults[rowindex].Assin;
                    dr["EstimatedArrival"] = inTransitHistoryResults[rowindex].EstimatedArrival;
                    dr["InventoryOwner"] = inTransitHistoryResults[rowindex].InventroyOwner;
                    dr["MergeCenter"] = inTransitHistoryResults[rowindex].MergeCenter;
                    dr["PartNumber"] = inTransitHistoryResults[rowindex].Part;
                    dr["Quantity"] = inTransitHistoryResults[rowindex].Quanity;
                    dr["Sku"] = inTransitHistoryResults[rowindex].SKU;
                    dr["ShippingFromFacility"] = inTransitHistoryResults[rowindex].ShippingFromFacility;
                    dr["TransportMode"] = inTransitHistoryResults[rowindex].TransportMode;
                    
                    datatable.Rows.Add(dr);
                }
                //}
            }

            GIC.Business.ReportHandler.GIC_Context.ATSDataTable = datatable;

            Console.WriteLine(string.Format("[{0}] - Done!", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            MiscUtility.LogHistory("Done!");
        }

        private void FetchBacklogOrders()
        {
            Console.WriteLine(string.Format("[{0}] - Starting to fetch ATS Committed Orders via ATS Web Service...",
                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            MiscUtility.LogHistory("Starting to fetch ATS Committed Orders via calling ATS Web Service..");

            string countryCodeList = ConfigFileUtility.GetValue("CountryCodeList");

            ATSData.DashboardService.DashboardRequest dashboardrequest = new DashboardRequest();
            dashboardrequest.CountryId = 0;
            dashboardrequest.RegionId = 0;
            dashboardrequest.SalesChannel = 2;
            dashboardrequest.SellAction = 0;
            dashboardrequest.StockStatus = 0;
            dashboardrequest.IsActive = "B";
            dashboardrequest.Type = 0;
                                    
            ATSData.DashboardService.DashboardServiceClient dashboardserviceclient = new DashboardServiceClient();
            ATSData.DashboardService.Dashboard[] dashboards = dashboardserviceclient.FetchProducts(dashboardrequest);

            DataTable datatable = BuildCommittedOrdersDataTable();
            DataRow dr = null;

            ReservationDetails[] reservationDetails = null;
            FetchReservationDetailsRequest request = new FetchReservationDetailsRequest();

            for (int index = 0; index < dashboards.Length; index++)
            {
                foreach (string countrycode in countryCodeList.Split(','))
                {
                    // Only fetch EMEA on-hand inventory without 0 quantity
                    if (dashboards[index].Country.Equals(countrycode) && dashboards[index].Inventory != 0)
                    {
                        request.ProductCountryId = dashboards[index].ProductCountryId.Value;

                        FetchCommitDetailsResponse response1 = dashboardserviceclient.FetchCommitDetails(request);                        
                        reservationDetails = response1.ReservationDetails;

                        for (int indey = 0; indey < reservationDetails.Length; indey++)
                        {
                            dr = datatable.NewRow();

                            dr["CommittedOn"] = reservationDetails[indey].CommittedOn;
                            dr["CountryName"] = reservationDetails[indey].CountryName;
                            dr["FacilityCode"] = reservationDetails[indey].FacilityCode;
                            dr["IRN"] = reservationDetails[indey].IRN;
                            dr["OrderNumber"] = reservationDetails[indey].OrderNumber;
                            dr["Quantity"] = reservationDetails[indey].Quantity;
                            dr["ReservationID"] = reservationDetails[indey].ReservationID;
                            dr["ReservedOn"] = reservationDetails[indey].ReservedOn;
                            dr["Segment"] = reservationDetails[indey].Segment;
                            dr["TieNumber"] = reservationDetails[indey].TieNumber;
                            dr["ToFulfillCheck"] = reservationDetails[indey].ToFulfillCheck;
                            
                            datatable.Rows.Add(dr);
                        }                        
                    }
                }
            }

            FetchReservationDetailsRequest request2 = new FetchReservationDetailsRequest();

            for (int index = 0; index < dashboards.Length; index++)
            {
                foreach (string countrycode in countryCodeList.Split(','))
                {
                    // Only fetch EMEA on-hand inventory without 0 quantity
                    if (dashboards[index].Country.Equals(countrycode) && dashboards[index].Inventory != 0)
                    {
                        request2.ProductCountryId = dashboards[index].ProductCountryId.Value;

                        FetchPostCheckoutDetailsResponse response2 = dashboardserviceclient.FetchPostCheckoutDetails(request2);
                        reservationDetails = response2.ReservationDetails;

                        for (int indey = 0; indey < reservationDetails.Length; indey++)
                        {
                            dr = datatable.NewRow();

                            dr["CommittedOn"] = reservationDetails[indey].CommittedOn;
                            dr["CountryName"] = reservationDetails[indey].CountryName;
                            dr["FacilityCode"] = reservationDetails[indey].FacilityCode;
                            dr["IRN"] = reservationDetails[indey].IRN;
                            dr["OrderNumber"] = reservationDetails[indey].OrderNumber;
                            dr["Quantity"] = reservationDetails[indey].Quantity;
                            dr["ReservationID"] = reservationDetails[indey].ReservationID;
                            dr["ReservedOn"] = reservationDetails[indey].ReservedOn;
                            dr["Segment"] = reservationDetails[indey].Segment;
                            dr["TieNumber"] = reservationDetails[indey].TieNumber;
                            dr["ToFulfillCheck"] = reservationDetails[indey].ToFulfillCheck;

                            datatable.Rows.Add(dr);
                        }
                    }
                }
            }

            GIC.Business.ReportHandler.GIC_Context.ATSDataTable = datatable;

            Console.WriteLine(string.Format("[{0}] - Done!", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            MiscUtility.LogHistory("Done!");
        }

        private void GetFallOutOrders()
        {
            ATSData.ProductService.ProductServiceClient client = new ProductServiceClient();
            ATSData.ProductService.RegionItem[] regionItems = client.GetAllRegions();

            ATSData.DashboardService.DashboardServiceClient dashboardserviceclient = new DashboardServiceClient();

            DataTable datatable = BuildFallOutOrderDataTable();
            DataRow dr = null;

            for (int index = 0; index < regionItems.Length; index++)
            {
                CommitFallOutCountry[] commitFallOutCountry = dashboardserviceclient.GetCommitFallOutCountry(regionItems[index].ID);

                for (int indey = 0; indey < commitFallOutCountry.Length; indey++)
                {
                    dr = datatable.NewRow();

                    dr["CountryCode"] = commitFallOutCountry[indey].CountryCode;
                    dr["FGA"] = commitFallOutCountry[indey].FGA;
                    dr["OrderNumber"] = commitFallOutCountry[indey].OrderNumber;
                    dr["ProductType"] = commitFallOutCountry[indey].ProductType;
                    dr["Quantity"] = commitFallOutCountry[indey].Quantity;
                    dr["ReservationID"] = commitFallOutCountry[indey].ReservationID;
                    dr["SKU"] = commitFallOutCountry[indey].SKU;
                    dr["TieNumber"] = commitFallOutCountry[indey].TieNumber;
                    dr["UpdateDateTime"] = commitFallOutCountry[indey].UpdateDateTime;

                    datatable.Rows.Add(dr);
                }
            }

            GIC.Business.ReportHandler.GIC_Context.ATSDataTable = datatable;

            Console.WriteLine(string.Format("[{0}] - Done!", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            MiscUtility.LogHistory("Done!");
        }

        #region ----- Export ATS data into an Excel file -----
        private void ExportDataIntoExcelFile(string filename)
        {
            string dayOfWeek = DateTime.Now.DayOfWeek.ToString();
            //string fileName = string.Format("{0}_{1}.xlsx", filename, DateTime.Now.ToString("yyyyMMdd_HHmmss"));
            string fileName = string.Format("{0}_{1}.xlsx", filename, dayOfWeek.Substring(0, 3));
            string fullFileName = Path.Combine(ConfigFileUtility.GetValue("OutputFolder"), fileName);
            string moveToTargetFolder = ConfigFileUtility.GetValue("MoveToTargetFolder");

            if (File.Exists(fullFileName))
                File.Delete(fullFileName);

            Console.WriteLine(string.Format("[{0}] - Starting to export data into report file - {1}...",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), fullFileName));
            MiscUtility.LogHistory(string.Format("Starting to export data into report file - {0}...", fullFileName));

            ExcelFileUtility.SaveExcelFile(fullFileName, GIC_Context.ATSDataTable, false);
            //ExcelFileUtility.ExportDataIntoExcelFile(fullFileName, GIC_Context.ATSDataTable);

            Console.WriteLine(string.Format("[{0}] - Done!", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            MiscUtility.LogHistory("Done!");

            if (moveToTargetFolder.Length != 0)
            {
                Console.WriteLine(string.Format("[{0}] - Starting to move report file to target folder - {1}...",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), moveToTargetFolder));
                MiscUtility.LogHistory(string.Format("Starting to move report file to target folder - {0}...", moveToTargetFolder));

                string newTargetFileName = Path.Combine(ConfigFileUtility.GetValue("MoveToTargetFolder"), fileName);
                if (File.Exists(newTargetFileName))
                    File.Delete(newTargetFileName);

                File.Move(fullFileName, Path.Combine(moveToTargetFolder, fileName));

                Console.WriteLine(string.Format("[{0}] - Done!", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                MiscUtility.LogHistory("Done!");
            }
        }
        #endregion

        #region ----- Build a data table structure of In-Transit data -----
        private DataTable BuildInTransitDataTable()
        {
            DataTable datatable = new DataTable();
            
            DataColumn dc = new DataColumn();
            dc.ColumnName = "Country";
            datatable.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "ASNNumber";
            datatable.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "EstimatedArrival";
            datatable.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "InventoryOwner";
            datatable.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "MergeCenter";
            datatable.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "PartNumber";
            datatable.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "Quantity";
            datatable.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "Sku";
            datatable.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "ShippingFromFacility";
            datatable.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "TransportMode";
            datatable.Columns.Add(dc);

            return datatable;
        }
        #endregion

        #region ----- Build a data table structure of On-hand data -----
        private DataTable BuildOnHandDataTable()
        {
            DataTable datatable = new DataTable();

            DataColumn dc = new DataColumn();
            dc.ColumnName = "Brand Name";
            datatable.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "Offer Type";
            datatable.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "SKU";
            datatable.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "PN";
            datatable.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "Catalog(s)";
            datatable.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "SV";
            datatable.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "LT";
            datatable.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "ATS";
            datatable.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "Online";
            datatable.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "Offline";
            datatable.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "SA Online";
            datatable.Columns.Add(dc);
            dc = new DataColumn();

            dc.ColumnName = "SA Offline";
            datatable.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "On Hand";
            datatable.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "Commit";
            datatable.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "Checkout";
            datatable.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "Backlog";
            datatable.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "Cart";
            datatable.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "Type";
            datatable.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "State";
            datatable.Columns.Add(dc);

            return datatable;
        }
        #endregion

        #region ----- Build a data table structure for On-hand inventory with location information -----
        private DataTable BuildOnHandWithLocationDataTable()
        {
            DataTable datatable = new DataTable();

            DataColumn dc = new DataColumn();
            dc.ColumnName = "Product Contry ID";
            datatable.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "Unique Num";
            datatable.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "Country";
            datatable.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "PN";
            datatable.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "Sku";
            datatable.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "Location Code";
            datatable.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "Quantity";
            datatable.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "Source";
            datatable.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "Updated Date";
            datatable.Columns.Add(dc);

            return datatable;
        }
        #endregion

        #region ----- Build a data table structure to keep Committed orders -----
        private DataTable BuildCommittedOrdersDataTable()
        {
            DataTable datatable = new DataTable();

            DataColumn dc = new DataColumn();
            dc.ColumnName = "CommittedOn";
            datatable.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "CountryName";
            datatable.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "FacilityCode";
            datatable.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "IRN";
            datatable.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "OrderNumber";
            datatable.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "Quantity";
            datatable.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "ReservationID";
            datatable.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "ReservedOn";
            datatable.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "Segment";
            datatable.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "TieNumber";
            datatable.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "ToFulfillCheck";
            datatable.Columns.Add(dc);

            return datatable;
        }
        #endregion

        #region ----- Build a data table structure to keep Fall out orders -----
        private DataTable BuildFallOutOrderDataTable()
        {
            DataTable datatable = new DataTable();

            DataColumn dc = new DataColumn();
            dc.ColumnName = "CountryCode";
            datatable.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "FGA";
            datatable.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "OrderNumber";
            datatable.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "ProductType";
            datatable.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "Quantity";
            datatable.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "ReservationID";
            datatable.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "Sku";
            datatable.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "TieNumber";
            datatable.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "UpdateDateTime";
            datatable.Columns.Add(dc);            

            return datatable;
        }
        #endregion
    }
}
