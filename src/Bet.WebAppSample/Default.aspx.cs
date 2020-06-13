using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Bet.WebAppSample.Services;

namespace Bet.WebAppSample
{
#pragma warning disable SA1300 // Element should begin with upper-case letter
    public partial class _Default : Page
#pragma warning restore SA1300 // Element should begin with upper-case letter
    {
        private readonly FeedService _feedService;

        public _Default(FeedService feedService)
        {
            _feedService = feedService;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblFeed.Text = _feedService.GetValue();
        }
    }
}
