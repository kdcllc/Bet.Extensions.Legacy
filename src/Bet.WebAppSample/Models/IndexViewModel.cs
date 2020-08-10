using Bet.WebAppSample.Options;

using Microsoft.FeatureManagement;

namespace Bet.WebAppSample.Models
{
    public class IndexViewModel : BaseViewModel
    {
        public IndexViewModel(IFeatureManagerSnapshot featureManager) : base(featureManager)
        {
        }

        public AppOptions Options { get; set; }
    }
}
