using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Bet.WebAppSample.Options;
using Bet.WebAppSample.Services;

using Microsoft.Extensions.Options;

namespace Bet.WebAppSample
{
#pragma warning disable SA1300 // Element should begin with upper-case letter
    public partial class _Default : Page
#pragma warning restore SA1300 // Element should begin with upper-case letter
    {
        private readonly OptionsService _optionsService;
        private readonly IOptionsSnapshot<AppOptions> _options;

        public _Default(OptionsService optionsService, IOptionsSnapshot<AppOptions> options)
        {
            _optionsService = optionsService;
            _options = options;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblOptionsTextValue.Text = _optionsService.GetValue();
            lblOptionsMessage.Text = _options.Value.Message;
        }
    }
}
