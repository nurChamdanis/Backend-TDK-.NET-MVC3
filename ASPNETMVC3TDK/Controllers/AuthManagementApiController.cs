using System;
using System.Linq;
using System.Web.Mvc;
using Toyota.Common.Credential;
using Toyota.Common.Web.Platform;
using ASPNETMVC3TDK.Shared;
using ASPNETMVC3TDK.Models;
using ASPNETMVC3TDK.Models.AuthManagement;

namespace ASPNETMVC3TDK.Controllers
{
	public class AuthManagementApiController : PageController
	{
		AuthManagementRepo repo = new AuthManagementRepo();
		DataTables datatable = new DataTables();

		#region Get List Application
		[HttpPost]
		//[ValidateAntiForgeryToken]
		public JsonResult GetListApplications()
		{
			try
			{
				var listApplication = repo.GetListApplications();

				return Json(listApplication, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				return Json("Error: " + ex.Message, JsonRequestBehavior.AllowGet);
			}
		}
		#endregion

		#region Get List Function
		[HttpPost]
		//[ValidateAntiForgeryToken]
		public JsonResult GetListFunction()
		{
			try
			{
				var ROLE = Request.Form.GetValues("ROLE").FirstOrDefault();
				var listFunction = repo.GetListFunction(ROLE);

				return Json(listFunction, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				return Json("Error: " + ex.Message, JsonRequestBehavior.AllowGet);
			}
		}
		#endregion

		#region Get List Feature
		[HttpPost]
		//[ValidateAntiForgeryToken]
		public JsonResult GetListFeature()
		{
			try
			{
				var ROLE = Request.Form.GetValues("ROLE").FirstOrDefault();
				var FUNCTION = Request.Form.GetValues("FUNCTION").FirstOrDefault();
				var listFeature = repo.GetListFeature(ROLE, FUNCTION);

				return Json(listFeature, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				return Json("Error: " + ex.Message, JsonRequestBehavior.AllowGet);
			}
		}
		#endregion

		#region Assign Function
		[HttpPost]
		[ValidateAntiForgeryToken]
		public JsonResult AssignFunction(AuthManagement m)
		{
			User user = Lookup.Get<User>();

			return Json(repo.AssignFunction(m, user.Username), JsonRequestBehavior.AllowGet);
		}
		#endregion

		#region Assign Feature
		[HttpPost]
		[ValidateAntiForgeryToken]
		public JsonResult AssignFeature(AuthManagement m)
		{
			User user = Lookup.Get<User>();

			return Json(repo.AssignFeature(m, user.Username), JsonRequestBehavior.AllowGet);
		}
		#endregion

		#region Delete Function
		[HttpPost]
		[ValidateAntiForgeryToken]
		public JsonResult DeleteFunction(FormCollection form)
		{
			User user = Lookup.Get<User>();
			return Json(repo.DeleteFunction(form), JsonRequestBehavior.AllowGet);
		}
		#endregion

		#region Delete Feature
		[HttpPost]
		[ValidateAntiForgeryToken]
		public JsonResult DeleteFeature(FormCollection form)
		{
			User user = Lookup.Get<User>();
			return Json(repo.DeleteFeature(form), JsonRequestBehavior.AllowGet);
		}
		#endregion

		#region Get Datatable Module
		[HttpPost]
		[ValidateAntiForgeryToken]
		public JsonResult GetDatatables()
		{
			User user = Lookup.Get<User>();
			var draw = Request.Form.GetValues("draw").FirstOrDefault();
			datatable.draw = draw != null ? Convert.ToInt32(draw) : 0;
			try
			{
				var start = Request.Form.GetValues("start").FirstOrDefault();
				var length = Request.Form.GetValues("length").FirstOrDefault();
				var order_type = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
				var idx_column = Request.Form.GetValues("order[0][column]").FirstOrDefault();
				var order_name = Request.Form.GetValues("columns[" + idx_column + "][name]").FirstOrDefault();

				//Param
				var ID = Request.Form.GetValues("ID").FirstOrDefault();
				var NAME = Request.Form.GetValues("NAME").FirstOrDefault();

				//Paging Size (10,20,50,100)    
				int pageSize = length != null ? Convert.ToInt32(length) : 0;
				int skip = start != null ? Convert.ToInt32(start) : 0;

				datatable.data = repo.GetDataAuthManagement(skip, pageSize, order_name, order_type, ID, NAME);
				datatable.recordsFiltered = repo.GetDataAuthManagementCount(ID, NAME);
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

		#region Get Function Datatable Module
		[HttpPost]
		[ValidateAntiForgeryToken]
		public JsonResult GetFunctionDatatables()
		{
			User user = Lookup.Get<User>();
			var draw = Request.Form.GetValues("draw").FirstOrDefault();
			datatable.draw = draw != null ? Convert.ToInt32(draw) : 0;
			try
			{
				var start = Request.Form.GetValues("start").FirstOrDefault();
				var length = Request.Form.GetValues("length").FirstOrDefault();
				var order_type = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
				var idx_column = Request.Form.GetValues("order[0][column]").FirstOrDefault();
				var order_name = Request.Form.GetValues("columns[" + idx_column + "][name]").FirstOrDefault();

				//Param
				var ID = Request.Form.GetValues("ID").FirstOrDefault();
				var NAME = Request.Form.GetValues("NAME").FirstOrDefault();
				var ROLE = Request.Form.GetValues("ROLE").FirstOrDefault();

				//Paging Size (10,20,50,100)    
				int pageSize = length != null ? Convert.ToInt32(length) : 0;
				int skip = start != null ? Convert.ToInt32(start) : 0;

				datatable.data = repo.GetDataFunctionAuthManagement(skip, pageSize, order_name, order_type, ID, NAME, ROLE);
				datatable.recordsFiltered = repo.GetDataFunctionAuthManagementCount(ID, NAME, ROLE);
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

		#region Get Feature Datatable Module
		[HttpPost]
		[ValidateAntiForgeryToken]
		public JsonResult GetFeatureDatatables()
		{
			User user = Lookup.Get<User>();
			var draw = Request.Form.GetValues("draw").FirstOrDefault();
			datatable.draw = draw != null ? Convert.ToInt32(draw) : 0;
			try
			{
				var start = Request.Form.GetValues("start").FirstOrDefault();
				var length = Request.Form.GetValues("length").FirstOrDefault();
				var order_type = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
				var idx_column = Request.Form.GetValues("order[0][column]").FirstOrDefault();
				var order_name = Request.Form.GetValues("columns[" + idx_column + "][name]").FirstOrDefault();

				//Param
				var ID = Request.Form.GetValues("ID").FirstOrDefault();
				var NAME = Request.Form.GetValues("NAME").FirstOrDefault();
				var ROLE = Request.Form.GetValues("ROLE").FirstOrDefault();
				var FUNCTION = Request.Form.GetValues("FUNCTION").FirstOrDefault();

				//Paging Size (10,20,50,100)    
				int pageSize = length != null ? Convert.ToInt32(length) : 0;
				int skip = start != null ? Convert.ToInt32(start) : 0;

				datatable.data = repo.GetDataFeatureAuthManagement(skip, pageSize, order_name, order_type, ID, NAME, ROLE, FUNCTION);
				datatable.recordsFiltered = repo.GetDataFeatureAuthManagementCount(ID, NAME, ROLE, FUNCTION);
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


	}
}
