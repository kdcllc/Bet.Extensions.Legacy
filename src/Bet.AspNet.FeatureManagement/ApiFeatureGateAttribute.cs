using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

using Bet.AspNet.DependencyInjection.Legacy;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;

namespace Bet.AspNet.FeatureManagement
{
    /// <summary>
    /// An attribute that can be placed on MVC actions to require all or any of a set of features to be enabled.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class ApiFeatureGateAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiFeatureGateAttribute"/> class.
        /// Creates an attribute that will gate actions unless all the provided feature(s) are enabled.
        /// </summary>
        /// <param name="features">The names of the features that the attribute will represent.</param>
        public ApiFeatureGateAttribute(params string[] features)
            : this(RequirementType.All, features)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiFeatureGateAttribute"/> class.
        /// Creates an attribute that can be used to gate actions. The gate can be configured to require all or any of the provided feature(s) to pass.
        /// </summary>
        /// <param name="requirementType">Specifies whether all or any of the provided features should be enabled in order to pass.</param>
        /// <param name="features">The names of the features that the attribute will represent.</param>
        public ApiFeatureGateAttribute(RequirementType requirementType, params string[] features)
        {
            if (features == null || features.Length == 0)
            {
                throw new ArgumentNullException(nameof(features));
            }

            Features = features;

            RequirementType = requirementType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiFeatureGateAttribute"/> class.
        /// Creates an attribute that will gate actions unless all the provided feature(s) are enabled.
        /// </summary>
        /// <param name="features">A set of enums representing the features that the attribute will represent.</param>
        public ApiFeatureGateAttribute(params object[] features)
            : this(RequirementType.All, features)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiFeatureGateAttribute"/> class.
        /// Creates an attribute that can be used to gate actions. The gate can be configured to require all or any of the provided feature(s) to pass.
        /// </summary>
        /// <param name="requirementType">Specifies whether all or any of the provided features should be enabled in order to pass.</param>
        /// <param name="features">A set of enums representing the features that the attribute will represent.</param>
        public ApiFeatureGateAttribute(RequirementType requirementType, params object[] features)
        {
            if (features == null || features.Length == 0)
            {
                throw new ArgumentNullException(nameof(features));
            }

            var fs = new List<string>();

            foreach (var feature in features)
            {
                var type = feature.GetType();

                if (!type.IsEnum)
                {
                    // invalid
                    throw new ArgumentException("The provided features must be enums.", nameof(features));
                }

                fs.Add(Enum.GetName(feature.GetType(), feature));
            }

            Features = fs;

            RequirementType = requirementType;
        }

        /// <summary>
        /// The name of the features that the feature attribute will activate for.
        /// </summary>
        public IEnumerable<string> Features { get; }

        /// <summary>
        /// Controls whether any or all features in <see cref="Features"/> should be enabled to pass.
        /// </summary>
        public RequirementType RequirementType { get; }

        /// <summary>
        /// Performs controller action pre-procesing to ensure that at least one of the specified features are enabled.
        /// </summary>
        /// <param name="filterContext">The context of the MVC action.</param>
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            if (HttpContext.Current.Items[Constants.ServiceScopeType] is IServiceScope scope)
            {
                var fm = scope.ServiceProvider.GetRequiredService<IFeatureManagerSnapshot>();

                // Enabled state is determined by either 'any' or 'all' features being enabled.
                var enabled = RequirementType == RequirementType.All ?
                                 Features.All(feature => fm.IsEnabledAsync(feature).GetAwaiter().GetResult()) :
                                 Features.Any(feature => fm.IsEnabledAsync(feature).GetAwaiter().GetResult());

                if (!enabled)
                {
                    filterContext.Response = filterContext
                        .Request
                        .CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, "Endpoint doesn't exist");
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
