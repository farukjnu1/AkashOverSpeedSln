using AkashOverSpeed.DataFactory.BaseFactory;
using AkashOverSpeed.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AkashOverSpeed.Controllers
{
    public class HomeController : Controller
    {
        #region Variables
        string GPSDB = System.Configuration.ConfigurationManager.ConnectionStrings["GPSDB"].ConnectionString;
        Hashtable ht = null;
        private IGenericFactory<VmUser> Generic_Vm = null;
        #endregion

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(VmUser model)
        {
            Generic_Vm = new GenericFactory<VmUser>();
            ht = new Hashtable
            {
                { "Username", model.strUser },
                { "Password", model.strPassword }
            };
            var listUser = Generic_Vm.ExecuteCommandList("dbo.SP_OwnerLogin", "StoredProcedure", ht, GPSDB);
            VmUser oUser = listUser.Count > 0 ? listUser[0] : null;
            if (oUser != null)
            {
                Session["VmUser"] = oUser;
                return RedirectToAction("Index", "Track");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }   
        }

        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("Index", "Home");
        }
        
    }
}