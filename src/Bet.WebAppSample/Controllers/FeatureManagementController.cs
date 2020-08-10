using System.Web.Mvc;

using Bet.AspNet.FeatureManagement;
using Bet.WebAppSample.Models;
using Bet.WebAppSample.Options;
using Bet.WebAppSample.Services;

using Microsoft.Extensions.Options;
using Microsoft.FeatureManagement;

namespace Bet.WebAppSample.Controllers
{
    public class FeatureManagementController : Controller
    {
        private readonly ConfigurationService _optionsService;
        private readonly IFeatureManagerSnapshot _featureManager;
        private AppOptions _options;

        public FeatureManagementController(
            IOptionsMonitor<AppOptions> optionsMonitor,
            ConfigurationService optionsService,
            IFeatureManagerSnapshot featureManager)
        {
            _options = optionsMonitor.CurrentValue;
            optionsMonitor.OnChange(n => _options = n);

            _optionsService = optionsService;

            _featureManager = featureManager ?? throw new System.ArgumentNullException(nameof(featureManager));
        }

        // GET: FeatureManagement
        [FeatureGate(RequirementType.All, FeatureReleaseFlags.Beta, FeatureReleaseFlags.Alpha)]
        public ActionResult Index()
        {
            ViewData["Message"] = _optionsService.Referesh();

            var model = new IndexViewModel(_featureManager)
            {
                Options = _options,
            };

            return View(model);
        }

        public ActionResult Beta()
        {
            ViewBag.Message = "Your application description page.";

            var model = new BaseViewModel(_featureManager);

            return View(model);
        }
    }
}
