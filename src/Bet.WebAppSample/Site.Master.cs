using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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

        protected void Page_Load(object sender, EventArgs e)
        {
            var feature = FeatureReleaseFlags.Beta.ToString();
            IsBeta = _featureSnapshot.IsEnabledAsync(feature).GetAwaiter().GetResult();
        }
    }
}
