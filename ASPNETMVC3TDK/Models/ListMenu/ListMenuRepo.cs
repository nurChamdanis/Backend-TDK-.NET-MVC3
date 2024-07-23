using System.Collections.Generic;
using System.Linq;
using Toyota.Common.Web.Platform;
using Toyota.Common.Database;

namespace ASPNETMVC3TDK.Models.ListMenu
{
    public class ListMenuRepo
    {
        #region Singleton

        private static ListMenuRepo instance = null;
        //IDBContext db = DatabaseManager.Instance.GetContext();
        IDBContext db2 = DatabaseManager.Instance.GetContext("SecurityCenter");
        public static ListMenuRepo Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ListMenuRepo();
                }
                return instance;
            }
        }

        #endregion

        public IList<ListMenu> GetListMenu(string USERNAME, string ROLE,string PARENT_ID)
        {
            dynamic args = new
            {
                USERNAME = USERNAME?.Replace("'", "`"),
                ROLE = ROLE?.Replace("'", "`"),
                APPALIAS = ApplicationSettings.Instance.Alias,
                PARENT_ID = PARENT_ID
            };
            IList<ListMenu> Result = db2.Fetch<ListMenu>("Menu/Menu_GetListMenu", args);
            db2.Close();
            return Result;
        }

        public IList<ListMenu> GetListMenuParent(string USERNAME, string ROLE)
        {
            dynamic args = new
            {
                USERNAME = USERNAME?.Replace("'", "`"),
                ROLE = ROLE?.Replace("'", "`"),
                APPALIAS = ApplicationSettings.Instance.Alias
            };
            IList<ListMenu> Result = db2.Fetch<ListMenu>("Menu/Menu_GetListMenuParent", args);
            db2.Close();
            return Result;
        }

        public bool HasChildrenWithin(Menu menuItem)
        {
            if (menuItem.Children.Any())
            {
                return true;
            }

            return false;
        }
    }
}