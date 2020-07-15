using System.Web.Mvc;

using Bet.AspNet.FeatureManagement;

namespace Bet.WebAppSample.Controllers
{
    public class FeatureManagementController : Controller
    {
        // GET: FeatureManagement
        [FeatureGate(RequirementType.All, FeatureReleaseFlags.Beta, FeatureReleaseFlags.Alpha)]
        public ActionResult Index()
        {
            return View();
        }
    }
}
