using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Toyota.Common.Credential;
using Toyota.Common.Web.Platform;
using ASPNETMVC3TDK.Shared;
using ASPNETMVC3TDK.Models.LogMonitoring;
using ASPNETMVC3TDK.Models;
using Toyoya.Common.Validation;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms.VisualStyles;
using NPOI.HSSF.UserModel;
using System.IO;
using NPOI.SS.UserModel;
using VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment;
using static Azure.Core.HttpHeader;
using System.Web;
using NPOI.XSSF.UserModel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using NPOI.SS.Formula.Functions;
using static NPOI.SS.Formula.Functions.LinearRegressionFunction;
using System.Data;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using System.Drawing.Printing;
using System.Web.UI.WebControls;

namespace ASPNETMVC3TDK.Controllers
{
	// ============== UNCOMMENT JIKA INGIN MENGGUNAKAN UNIT TEST ====================
	/*public class LogMonitoringApiController : Controller*/
	// ============== END UNCOMMENT JIKA INGIN MENGGUNAKAN UNIT TEST =================

	// ============== COMMENT JIKA TIDAK INGIN MENGGUNAKAN UNIT TEST ====================
	public class LogMonitoringApiController : PageController
	// ============== END COMMENT JIKA TIDAK INGIN MENGGUNAKAN UNIT TEST =================

	{
		private static LogMonitoringApiController instance = null;

		LogMonitoringRepo repo = new LogMonitoringRepo();
		DataTables datatable = new DataTables();

		public static LogMonitoringApiController Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new LogMonitoringApiController();
				}
				return instance;
			}

		}

		#region Get Datatable Module
		[HttpPost]
		[ValidateAntiForgeryToken]
		public JsonResult GetDatatables()
		{
			var draw = Request.Form.GetValues("draw").FirstOrDefault();
			datatable.draw = draw != null ? Convert.ToInt32(draw) : 0;

			try
			{
				var start = Request.Form.GetValues("start").FirstOrDefault();
				var length = Request.Form.GetValues("length").FirstOrDefault();
				var orderType = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
				var idxColumn = Request.Form.GetValues("order[0][column]").FirstOrDefault();
				var orderName = Request.Form.GetValues("columns[" + idxColumn + "][name]").FirstOrDefault();

				//Param
				var processDt = Request.Form.GetValues("PROCESS_DT").FirstOrDefault();
				var endDt = Request.Form.GetValues("END_DT").FirstOrDefault();
				var userId = Request.Form.GetValues("USER_ID").FirstOrDefault();
				var processId = Request.Form.GetValues("PROCESS_ID").FirstOrDefault();
				var moduleId = Request.Form.GetValues("MODULE_ID").FirstOrDefault();
				var functionId = Request.Form.GetValues("FUNCTION_ID").FirstOrDefault();
				var processSts = Request.Form.GetValues("PROCESS_STATUS").FirstOrDefault();

				//Paging Size (10,20,50,100)    
				int pageSize = length != null ? Convert.ToInt32(length) : 0;
				int skip = start != null ? Convert.ToInt32(start) : 0;

				datatable.data = repo.GetDataLog("row", skip, pageSize, processDt, endDt, userId, processId, moduleId, functionId, processSts, orderName, orderType);
				var recordFiltered = repo.GetDataLog("count", skip, pageSize, processDt, endDt, userId, processId, moduleId, functionId, processSts, orderName, orderType)[0].CNT;
				datatable.recordsFiltered = int.Parse(recordFiltered);
				datatable.recordsTotal = datatable.recordsFiltered;

				//Returning Json Data    
				return Json(datatable);
			}
			catch (Exception err)
			{
				datatable.data = new { err.InnerException, err.Message };
				datatable.status = false;
				return Json(datatable);
			}
		}
		#endregion

		#region Get Log Detail
		public JsonResult GetLogDetail()
		{
			var draw = Request.Form.GetValues("draw").FirstOrDefault();
			datatable.draw = draw != null ? Convert.ToInt32(draw) : 0;

			try
			{
				var start = Request.Form.GetValues("start").FirstOrDefault();
				var length = Request.Form.GetValues("length").FirstOrDefault();
				var orderType = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
				var idxColumn = Request.Form.GetValues("order[0][column]").FirstOrDefault();
				var orderName = Request.Form.GetValues("columns[" + idxColumn + "][name]").FirstOrDefault();

				var processId = Request.Form.GetValues("PROCESS_ID").FirstOrDefault();
				var messageType = Request.Form.GetValues("MESSAGE_TYPE").FirstOrDefault();

				//Paging Size (10,20,50,100)    
				int pageSize = length != null ? Convert.ToInt32(length) : 0;
				int skip = start != null ? Convert.ToInt32(start) : 0;

				datatable.data = repo.GetDataLogDetail("row", skip, pageSize, processId, messageType, orderName, orderType);
				var recordFiltered = repo.GetDataLogDetail("count", skip, pageSize, processId, messageType, orderName, orderType)[0].CNT;
				datatable.recordsFiltered = int.Parse(recordFiltered);
				datatable.recordsTotal = datatable.recordsFiltered;

				return Json(datatable);
			}
			catch (Exception ex)
			{
				return Json("Error: " + ex.Message, JsonRequestBehavior.AllowGet);
			}
		}

		#endregion

		#region Get Data Options
		[HttpPost]
		public JsonResult GetDataOptions(string PARAM_1, string PARAM_2, string PARAM_3)
		{
			try
			{

				PARAM_1 = CommonHelper.parseEmptyString(PARAM_1);
				PARAM_2 = CommonHelper.parseEmptyString(PARAM_2);
				PARAM_3 = CommonHelper.parseEmptyString(PARAM_3);

				var listSystem = repo.GetDataOptions(PARAM_1, PARAM_2, PARAM_3);

				return Json(listSystem, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				return Json("Error: " + ex.Message, JsonRequestBehavior.AllowGet);
			}
		}
		#endregion

		#region Download Excel
		[HttpPost]
		[ValidateAntiForgeryToken]
		public JsonResult DownloadData()
		{
			try
			{
				var columnOrder = Request.Form.GetValues("COLUMN_ORDER").FirstOrDefault();
				var typeOrder = Request.Form.GetValues("TYPE_ORDER").FirstOrDefault();

				//Param
				var processDt = Request.Form.GetValues("PROCESS_DT").FirstOrDefault();
				var endDt = Request.Form.GetValues("END_DT").FirstOrDefault();
				var userId = Request.Form.GetValues("USER_ID").FirstOrDefault();
				var processId = Request.Form.GetValues("PROCESS_ID").FirstOrDefault();
				var moduleId = Request.Form.GetValues("MODULE_ID").FirstOrDefault();
				var functionId = Request.Form.GetValues("FUNCTION_ID").FirstOrDefault();
				var processSts = Request.Form.GetValues("PROCESS_STATUS").FirstOrDefault();

				var listData = repo.GetDataLog("download", 0, -1, processDt, endDt, userId, processId, moduleId, functionId, processSts, columnOrder, typeOrder);
				int listDataCount = int.Parse(repo.GetDataLog("count", 0, -1, processDt, endDt, userId, processId, moduleId, functionId, processSts, columnOrder, typeOrder)[0].CNT);

				string fileName = GenerateExcel(listDataCount, processDt, endDt, userId, processId, moduleId, functionId, processSts, columnOrder, typeOrder);
				string pathFile = HttpContext.Request.MapPath("~/Content/download/Report_Log/Header/");
				string pathString = "/Content/download/Report_Log/Header/";

				ResultMessage res = new ResultMessage();
				if (!string.IsNullOrEmpty(fileName) && System.IO.File.Exists(pathFile + fileName))
				{
					res.result = "true";
					res.msgInfo = "success";
					res.msg = pathString;
					res.extraAttribute = fileName;
				}
				else
				{
					res.result = "false";
					res.msgInfo = "failed";
					res.msg = "File not existsss";
					res.extraAttribute = "";

				}
				return Json(res, JsonRequestBehavior.AllowGet);
			}
			catch (Exception err)
			{
				ResultMessage res = new ResultMessage();
				res.result = "false";
				res.msgInfo = "failed";
				res.msg = "File not exists";
				res.extraAttribute = "";

				return Json(res, JsonRequestBehavior.AllowGet);
			}
		}
		#endregion

		private string GenerateExcel(int dataCount, string processDt, string endDt, string userId, string processId, string moduleId, string functionId, string processSts, string columnOrder, string typeOrder)
		{
			string filesTMp = AppDomain.CurrentDomain.BaseDirectory + "Content/download/MASTER_DOWNLOAD.xls";
			if (System.IO.File.Exists(filesTMp))
			{
				IRow Hrow;
				int row;
				HSSFWorkbook tmpWorkbook;

				using (FileStream fs = new FileStream(filesTMp, FileMode.Open, FileAccess.Read))
				{
					tmpWorkbook = new HSSFWorkbook(fs);
					fs.Close();
				}

				DateTime now = DateTime.Now; 
				string formattedDateTime = now.ToString("ddmmyyyy_HHmmss");
				string fileName = "LOG_MONITORING_HEADER_" + formattedDateTime + ".xls";

				HSSFWorkbook destWorkbook;
				destWorkbook = new HSSFWorkbook();

				HSSFSheet sheetLog = tmpWorkbook.CreateSheet("Log Header Monitoring") as HSSFSheet;
				sheetLog = tmpWorkbook.GetSheet("Log Header Monitoring") as HSSFSheet;

				if (sheetLog != null)
				{
					sheetLog.CopyTo(destWorkbook, "Log Header Monitoring", true, true);
				}

				ICellStyle S_BORDER_ALL = destWorkbook.CreateCellStyle();
				S_BORDER_ALL.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
				S_BORDER_ALL.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
				S_BORDER_ALL.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
				S_BORDER_ALL.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;

				ICellStyle S_BORDER_ALL_CENTER = destWorkbook.CreateCellStyle();
				S_BORDER_ALL_CENTER.Alignment = HorizontalAlignment.Center;
				S_BORDER_ALL_CENTER.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
				S_BORDER_ALL_CENTER.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
				S_BORDER_ALL_CENTER.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
				S_BORDER_ALL_CENTER.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;

				ICellStyle styleReportTitle = destWorkbook.CreateCellStyle();
				styleReportTitle.VerticalAlignment = VerticalAlignment.Center;
				styleReportTitle.Alignment = HorizontalAlignment.Center;
				styleReportTitle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
				styleReportTitle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
				styleReportTitle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
				styleReportTitle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
				styleReportTitle.FillForegroundColor = IndexedColors.Grey25Percent.Index;
				styleReportTitle.FillPattern = FillPattern.SolidForeground;
				IFont fontReportTitle = destWorkbook.CreateFont();
				fontReportTitle.FontHeightInPoints = 10; // Ukuran font
				fontReportTitle.Boldweight = (short)FontBoldWeight.Bold; // Atur font sebagai bold
				styleReportTitle.SetFont(fontReportTitle);

				#region data log header
				row = 1;
				sheetLog = destWorkbook.GetSheet("Log Header Monitoring") as HSSFSheet;

				Hrow = sheetLog.CreateRow(0);
				Hrow.CreateCell(0).SetCellValue("NO.");
				Hrow.CreateCell(1).SetCellValue("PROCESS ID");
				Hrow.CreateCell(2).SetCellValue("PROCESS DATE");
				Hrow.CreateCell(3).SetCellValue("MODULE");
				Hrow.CreateCell(4).SetCellValue("FUNCTION");
				Hrow.CreateCell(5).SetCellValue("PROCESS STATUS");
				Hrow.CreateCell(6).SetCellValue("USER_ID");
				Hrow.CreateCell(7).SetCellValue("END DATE");
				Hrow.CreateCell(8).SetCellValue("REMARK");
				Hrow.GetCell(0).CellStyle = styleReportTitle;
				Hrow.GetCell(1).CellStyle = styleReportTitle;
				Hrow.GetCell(2).CellStyle = styleReportTitle;
				Hrow.GetCell(3).CellStyle = styleReportTitle;
				Hrow.GetCell(4).CellStyle = styleReportTitle;
				Hrow.GetCell(5).CellStyle = styleReportTitle;
				Hrow.GetCell(6).CellStyle = styleReportTitle;
				Hrow.GetCell(7).CellStyle = styleReportTitle;
				Hrow.GetCell(8).CellStyle = styleReportTitle;

				sheetLog.AutoSizeColumn(0);
				sheetLog.AutoSizeColumn(1);
				sheetLog.AutoSizeColumn(2);
				sheetLog.AutoSizeColumn(3);
				sheetLog.AutoSizeColumn(4);
				sheetLog.AutoSizeColumn(5);
				sheetLog.AutoSizeColumn(6);
				sheetLog.AutoSizeColumn(7);
				sheetLog.AutoSizeColumn(8);

				if (dataCount > 0)
				{
					IList<LogH> listData = repo.GetDataLog("download", 0, -1, processDt, endDt, userId, processId, moduleId, functionId, processSts, columnOrder, typeOrder);
					foreach (var item in listData.Select((value, j) => new { j, value }))
					{
						var value = item.value;
						var index = item.j;
						Hrow = sheetLog.CreateRow(row + index);
						Hrow.CreateCell(0).SetCellValue(index + 1);
						Hrow.CreateCell(1).SetCellValue(value.PROCESS_ID);
						Hrow.CreateCell(2).SetCellValue(value.PROCESS_DT);
						Hrow.CreateCell(3).SetCellValue(value.MODULE_NAME);
						Hrow.CreateCell(4).SetCellValue(value.FUNCTION_NAME);
						Hrow.CreateCell(5).SetCellValue(value.PROCESS_STS);
						Hrow.CreateCell(6).SetCellValue(value.USER_ID);
						Hrow.CreateCell(7).SetCellValue(value.END_DT);
						Hrow.CreateCell(8).SetCellValue(value.REMARKS);
						Hrow.GetCell(0).CellStyle = S_BORDER_ALL_CENTER;
						Hrow.GetCell(1).CellStyle = S_BORDER_ALL;
						Hrow.GetCell(2).CellStyle = S_BORDER_ALL_CENTER;
						Hrow.GetCell(3).CellStyle = S_BORDER_ALL;
						Hrow.GetCell(4).CellStyle = S_BORDER_ALL;
						Hrow.GetCell(5).CellStyle = S_BORDER_ALL;
						Hrow.GetCell(6).CellStyle = S_BORDER_ALL;
						Hrow.GetCell(7).CellStyle = S_BORDER_ALL_CENTER;
						Hrow.GetCell(8).CellStyle = S_BORDER_ALL;


						sheetLog.AutoSizeColumn(0);
						sheetLog.AutoSizeColumn(1);
						sheetLog.AutoSizeColumn(2);
						sheetLog.AutoSizeColumn(3);
						sheetLog.AutoSizeColumn(4);
						sheetLog.AutoSizeColumn(5);
						sheetLog.AutoSizeColumn(6);
						sheetLog.AutoSizeColumn(7);
						sheetLog.AutoSizeColumn(8);
					}
				}
				else
				{
					Hrow = sheetLog.CreateRow(row);
					Hrow.CreateCell(0).CellStyle = S_BORDER_ALL_CENTER;
					Hrow.CreateCell(1).CellStyle = S_BORDER_ALL_CENTER;
					Hrow.CreateCell(2).CellStyle = S_BORDER_ALL_CENTER;
					Hrow.CreateCell(3).CellStyle = S_BORDER_ALL_CENTER;
					Hrow.CreateCell(4).CellStyle = S_BORDER_ALL_CENTER;
					Hrow.CreateCell(5).CellStyle = S_BORDER_ALL_CENTER;
					Hrow.CreateCell(6).CellStyle = S_BORDER_ALL_CENTER;
					Hrow.CreateCell(7).CellStyle = S_BORDER_ALL_CENTER;
					Hrow.CreateCell(8).CellStyle = S_BORDER_ALL_CENTER;
					Hrow.GetCell(0).SetCellValue("No data available");
					sheetLog.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(row, row, 0, 8));
				}
				#endregion data log header

				string filesDest = AppDomain.CurrentDomain.BaseDirectory + "Content/download/Report_Log/Header/" + fileName;

				System.IO.DirectoryInfo di = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "Content/download/Report_Log/Header");
				foreach (FileInfo file in di.GetFiles())
				{
					file.Delete();
				}
				using (FileStream fs = new FileStream(filesDest, FileMode.Create, FileAccess.Write))
				{
					destWorkbook.Write(fs);
					fs.Close();
				}
				return fileName;
			}
			return "";
		}

		#region Download Excel Detail
		[HttpPost]
		[ValidateAntiForgeryToken]
		public JsonResult DownloadDataDetail()
		{
			try
			{
				var orderName = Request.Form.GetValues("COLUMN_ORDER").FirstOrDefault();
				var orderType = Request.Form.GetValues("TYPE_ORDER").FirstOrDefault();

				//Param
				var processId = Request.Form.GetValues("PROCESS_ID").FirstOrDefault();
				var messageType = Request.Form.GetValues("MESSAGE_TYPE").FirstOrDefault();

				var listData = repo.GetDataLogDetail("download", 0, -1, processId, messageType, orderName, orderType);
				int listDataCount = int.Parse(repo.GetDataLogDetail("count", 0, -1, processId, messageType, orderName, orderType)[0].CNT);

				string fileName = GenerateExcelDetail(listDataCount, processId, messageType, orderName, orderType);
				string pathFile = HttpContext.Request.MapPath("~/Content/download/Report_Log/Detail/");
				string pathString = "/Content/download/Report_Log/Detail/";

				ResultMessage res = new ResultMessage();
				if (!string.IsNullOrEmpty(fileName) && System.IO.File.Exists(pathFile + fileName))
				{
					res.result = "true";
					res.msgInfo = "success";
					res.msg = pathString;
					res.extraAttribute = fileName;
				}
				else
				{
					res.result = "false";
					res.msgInfo = "failed";
					res.msg = "File not existsss";
					res.extraAttribute = "";

				}
				return Json(res, JsonRequestBehavior.AllowGet);
			}
			catch (Exception err)
			{
				ResultMessage res = new ResultMessage();
				res.result = "false";
				res.msgInfo = "failed";
				res.msg = "File not exists";
				res.extraAttribute = "";

				return Json(res, JsonRequestBehavior.AllowGet);
			}
		}
		#endregion
		private string GenerateExcelDetail(int dataCount, string processId, string messageType, string orderName, string orderType)
		{
			string filesTMp = AppDomain.CurrentDomain.BaseDirectory + "Content/download/MASTER_DOWNLOAD.xls";
			if (System.IO.File.Exists(filesTMp))
			{
				IRow Hrow;
				int row;
				HSSFWorkbook tmpWorkbook;

				using (FileStream fs = new FileStream(filesTMp, FileMode.Open, FileAccess.Read))
				{
					tmpWorkbook = new HSSFWorkbook(fs);
					fs.Close();
				}

				DateTime now = DateTime.Now;
				string formattedDateTime = now.ToString("ddmmyyyy_HHmmss");
				string fileName = "LOG_MONITORING_DETAIL_" + formattedDateTime + ".xls";

				HSSFWorkbook destWorkbook;
				destWorkbook = new HSSFWorkbook();

				HSSFSheet sheetLog = tmpWorkbook.CreateSheet("Log Detail Monitoring " + processId) as HSSFSheet;
				sheetLog = tmpWorkbook.GetSheet("Log Detail Monitoring " + processId) as HSSFSheet;

				if (sheetLog != null)
				{
					sheetLog.CopyTo(destWorkbook, "Log Detail Monitoring " + processId, true, true);
				}

				ICellStyle S_BORDER_ALL = destWorkbook.CreateCellStyle();
				S_BORDER_ALL.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
				S_BORDER_ALL.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
				S_BORDER_ALL.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
				S_BORDER_ALL.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;

				ICellStyle S_BORDER_ALL_CENTER = destWorkbook.CreateCellStyle();
				S_BORDER_ALL_CENTER.Alignment = HorizontalAlignment.Center;
				S_BORDER_ALL_CENTER.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
				S_BORDER_ALL_CENTER.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
				S_BORDER_ALL_CENTER.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
				S_BORDER_ALL_CENTER.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;

				ICellStyle styleReportTitle = destWorkbook.CreateCellStyle();
				styleReportTitle.VerticalAlignment = VerticalAlignment.Center;
				styleReportTitle.Alignment = HorizontalAlignment.Center;
				styleReportTitle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
				styleReportTitle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
				styleReportTitle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
				styleReportTitle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
				styleReportTitle.FillForegroundColor = IndexedColors.Grey25Percent.Index;
				styleReportTitle.FillPattern = FillPattern.SolidForeground;
				IFont fontReportTitle = destWorkbook.CreateFont();
				fontReportTitle.FontHeightInPoints = 10; // Ukuran font
				fontReportTitle.Boldweight = (short)FontBoldWeight.Bold; // Atur font sebagai bold
				styleReportTitle.SetFont(fontReportTitle);

				#region data log detail
				row = 3;
				sheetLog = destWorkbook.CreateSheet("Log Detail Monitoring " + processId) as HSSFSheet;

				Hrow = sheetLog.CreateRow(0);
				Hrow.CreateCell(0).SetCellValue("PROCESS ID");
				Hrow.CreateCell(1).SetCellValue(processId);
				Hrow.GetCell(0).CellStyle = styleReportTitle;
				Hrow.GetCell(1).CellStyle = S_BORDER_ALL_CENTER;

				Hrow = sheetLog.CreateRow(2);
				Hrow.CreateCell(0).SetCellValue("SEQ NO.");
				Hrow.CreateCell(1).SetCellValue("MESSAGE DATE TIME");
				Hrow.CreateCell(2).SetCellValue("LOCATION");
				Hrow.CreateCell(3).SetCellValue("MESSAGE TYPE");
				Hrow.CreateCell(4).SetCellValue("MESSAGE ID");
				Hrow.CreateCell(5).SetCellValue("MESSAGE DETAIL");
				Hrow.GetCell(0).CellStyle = styleReportTitle;
				Hrow.GetCell(1).CellStyle = styleReportTitle;
				Hrow.GetCell(2).CellStyle = styleReportTitle;
				Hrow.GetCell(3).CellStyle = styleReportTitle;
				Hrow.GetCell(4).CellStyle = styleReportTitle;
				Hrow.GetCell(5).CellStyle = styleReportTitle;

				if (dataCount > 0)
				{
					IList<LogD> listData = repo.GetDataLogDetail("download", 0, -1, processId, messageType, orderName, orderType);
					foreach (var item in listData.Select((value, j) => new { j, value }))
					{
						var value = item.value;
						var index = item.j;
						Hrow = sheetLog.CreateRow(row + index);
						Hrow.CreateCell(0).SetCellValue(value.SEQ_NO);
						Hrow.CreateCell(1).SetCellValue(value.ERR_DT);
						Hrow.CreateCell(2).SetCellValue(value.ERR_LOCATION);
						Hrow.CreateCell(3).SetCellValue(value.MSG_TYPE);
						Hrow.CreateCell(4).SetCellValue(value.MSG_ID);
						Hrow.CreateCell(5).SetCellValue(value.ERR_MESSAGE);
						Hrow.GetCell(0).CellStyle = S_BORDER_ALL_CENTER;
						Hrow.GetCell(1).CellStyle = S_BORDER_ALL_CENTER;
						Hrow.GetCell(2).CellStyle = S_BORDER_ALL;
						Hrow.GetCell(3).CellStyle = S_BORDER_ALL;
						Hrow.GetCell(4).CellStyle = S_BORDER_ALL_CENTER;
						Hrow.GetCell(5).CellStyle = S_BORDER_ALL;
					}
				}
				else
				{
					Hrow = sheetLog.CreateRow(row);
					Hrow.CreateCell(0).CellStyle = S_BORDER_ALL_CENTER;
					Hrow.CreateCell(1).CellStyle = S_BORDER_ALL_CENTER;
					Hrow.CreateCell(2).CellStyle = S_BORDER_ALL_CENTER;
					Hrow.CreateCell(3).CellStyle = S_BORDER_ALL_CENTER;
					Hrow.CreateCell(4).CellStyle = S_BORDER_ALL_CENTER;
					Hrow.CreateCell(5).CellStyle = S_BORDER_ALL_CENTER;
					Hrow.GetCell(0).SetCellValue("No data available");
					sheetLog.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(row, row, 0, 5));
				}

				sheetLog.AutoSizeColumn(0);
				sheetLog.AutoSizeColumn(1);
				sheetLog.AutoSizeColumn(2);
				sheetLog.AutoSizeColumn(3);
				sheetLog.AutoSizeColumn(4);
				sheetLog.AutoSizeColumn(5);
				#endregion data log detail

				string filesDest = AppDomain.CurrentDomain.BaseDirectory + "Content/download/Report_Log/Detail/" + fileName;

				System.IO.DirectoryInfo di = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "Content/download/Report_Log/Detail");
				foreach (FileInfo file in di.GetFiles())
				{
					file.Delete();
				}
				using (FileStream fs = new FileStream(filesDest, FileMode.Create, FileAccess.Write))
				{
					destWorkbook.Write(fs);
					fs.Close();
				}
				return fileName;
			}
			return "";
		}
	}
}
