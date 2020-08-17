using System;
using System.Web.UI;

using Bet.WebAppSample.Options;
using Bet.WebAppSample.Services;

using Microsoft.Extensions.Options;

namespace Bet.WebAppSample
{
#pragma warning disable SA1300 // Element should begin with upper-case letter
    public partial class _Default : Page
#pragma warning restore SA1300 // Element should begin with upper-case letter
    {
        private readonly ConfigurationService _optionsService;
        private readonly IOptionsSnapshot<AppOptions> _options;

        public _Default(ConfigurationService optionsService, IOptionsSnapshot<AppOptions> options)
        {
            _optionsService = optionsService;
            _options = options;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblOptionsTextValue.Text = $"Text: {_options.Value.TextValue}; Changed:{_optionsService.Referesh()}";
            lblOptionsMessage.Text = _options.Value.Message;
        }
    }
}
