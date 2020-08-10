using Bet.AspNet.FeatureManagement;

using Microsoft.FeatureManagement;

namespace Bet.WebAppSample.Models
{
    public class BaseViewModel
    {
        public BaseViewModel(IFeatureManagerSnapshot featureManager)
        {
            IsApiEnabled = featureManager.IsEnabledAsync(nameof(FeatureReleaseFlags.Alpha)).GetAwaiter().GetResult();
            IsMvcViewEnabled = featureManager.IsEnabledAsync(nameof(FeatureReleaseFlags.Beta)).GetAwaiter().GetResult();
        }

        public bool IsApiEnabled { get; set; }

        public bool IsMvcViewEnabled { get; set; }
    }
}
