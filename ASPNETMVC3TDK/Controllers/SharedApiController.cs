using System;
using System.Linq;
using System.Web.Mvc;
using Toyota.Common.Credential;
using Toyota.Common.Web.Platform;
using ASPNETMVC3TDK.Shared;
using ASPNETMVC3TDK.Models;
using ASPNETMVC3TDK.Models.Shared;
using NPOI.SS.UserModel;
using NPOI.SS.Util;

namespace ASPNETMVC3TDK.Controllers
{
	public class SharedApiController : PageController
	{

		SharedRepo repo = new SharedRepo();

		#region Get Message
		[HttpPost]
		public JsonResult GetMessage(string MESSAGE_ID, string PARAM_1, string PARAM_2, string PARAM_3, string PARAM_4)
		{
			var message = repo.GetMessage(MESSAGE_ID, PARAM_1, PARAM_2, PARAM_3, PARAM_4);
			return Json(new { message }, JsonRequestBehavior.AllowGet);
		}
		#endregion

		public void ExcelMergeCells(ISheet sheet, int row, int rowEnd, int colStart, int colEnd, string value, ICellStyle style, int height = 0)
		{
			IRow dataRow = sheet.GetRow(row);
			if (dataRow == null)
				dataRow = sheet.CreateRow(row);

			if (height > 0)
			{
				dataRow.HeightInPoints = height;
			}

			ICell cell = dataRow.CreateCell(colStart);
			cell.SetCellValue(value);

			CellRangeAddress mergedRegion = new CellRangeAddress(row, rowEnd, colStart, colEnd);
			sheet.AddMergedRegion(mergedRegion);

			ExcelApplyStyleToMergedCells(sheet, mergedRegion, style);
		}

		public static void ExcelApplyStyleToMergedCells(ISheet sheet, CellRangeAddress mergeRange, ICellStyle style)
		{
			for (int i = mergeRange.FirstRow; i <= mergeRange.LastRow; i++)
			{
				IRow row = sheet.GetRow(i);
				if (row == null)
				{
					row = sheet.CreateRow(i);
				}
				if (row != null)
				{
					for (int j = mergeRange.FirstColumn; j <= mergeRange.LastColumn; j++)
					{
						ICell cell = row.GetCell(j);
						if (cell == null)
						{
							cell = row.CreateCell(j);
						}
						cell.CellStyle = style;
					}
				}
			}
		}
	}
}
