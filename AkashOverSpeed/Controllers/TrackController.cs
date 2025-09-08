using AkashOverSpeed.DataFactory.BaseFactory;
using AkashOverSpeed.Filters;
using AkashOverSpeed.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AkashOverSpeed.Controllers
{
    public class TrackController : Controller
    {
        #region Variables
        string GPSDB = System.Configuration.ConfigurationManager.ConnectionStrings["GPSDB"].ConnectionString;
        Hashtable ht = null;
        private IGenericFactory<VmTrackLast> Generic_Vm = null;
        #endregion

        // GET: Track
        [MyFilter]
        public ActionResult Index(int? speedLimit)
        {
            var oUser = (VmUser)Session["VmUser"];
            Generic_Vm = new GenericFactory<VmTrackLast>();
            ht = new Hashtable
            {
                { "Username", oUser.strUser },
                { "nOverSpeed", speedLimit }
            };
            var listTrackLast = Generic_Vm.ExecuteCommandList("dbo.SP_OverSpeedByUser", "StoredProcedure", ht, GPSDB);
            return View(listTrackLast);
        }
    }
}