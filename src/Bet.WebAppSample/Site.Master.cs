using System;
using System.Web.UI;

using Bet.AspNet.FeatureManagement;

using Microsoft.FeatureManagement;

namespace Bet.WebAppSample
{
    public partial class SiteMaster : MasterPage
    {
        private readonly IFeatureManagerSnapshot _featureSnapshot;

        public SiteMaster(IFeatureManagerSnapshot featureSnapshot)
        {
            _featureSnapshot = featureSnapshot ?? throw new ArgumentNullException(nameof(featureSnapshot));
        }

        public bool IsBeta { get; set; }

        public bool IsAlpha { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            IsBeta = _featureSnapshot.IsEnabledAsync(nameof(FeatureReleaseFlags.Beta)).GetAwaiter().GetResult();
            IsAlpha = _featureSnapshot.IsEnabledAsync(nameof(FeatureReleaseFlags.Alpha)).GetAwaiter().GetResult();
        }
    }
}
