<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Bet.WebAppSample._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Bet.Extensions.Legacy</h1>
        <p class="lead">Migrating from ASP.NET WebForms, MVC4 and WebApi2 to AspNetCore with incremental approach.</p>
        <p><a href="https://github.com/kdcllc/Bet.Extensions.Legacy" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>
    </div>
    <div class="row">
        <div class="col-md-6">
            <div class="alert alert-success" role="alert">
                <asp:Label ID="lblOptionsTextValue" CssClass="" runat="server" Text="Options Value from AppOptions"></asp:Label>
            </div>
        </div>

        <div class="col-md-6">
            <div class="alert alert-success" role="alert">
                <asp:Label ID="lblOptionsMessage" runat="server" Text="Options Value from AppOptions"></asp:Label>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>FeatureManagement</h2>
            <p>
                Bet.AspNet.FeatureManagement using with
                ASP.NET WebForms, MVC4 and WebApi2 support for Microsoft.FeatureManagement.AspNetCore

            </p>
            <p>
                <a class="btn btn-default" href="https://github.com/kdcllc/Bet.Extensions.Legacy/tree/master/src/Bet.AspNet.FeatureManagement/">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Hosting</h2>
            <p>
                Bet.AspNet.LegacyHosting using with
                ASP.NET WebForms, MVC4 and WebApi2 support for Microsoft.Extensions.Hosting.

            </p>
            <p>
                <a class="btn btn-default" href="https://github.com/kdcllc/Bet.Extensions.Legacy/tree/master/src/Bet.AspNet.LegacyHosting/">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>AzureAppConfiguration</h2>
            <p>
                Bet.AspNet.LegacyHosting.AzureAppConfiguration using with
                ASP.NET WebForms, MVC4 and WebApi2 support for Microsoft.Extensions.Configuration.AzureAppConfiguration

            </p>
            <p>
                <a class="btn btn-default" href="ttps://github.com/kdcllc/Bet.Extensions.Legacy/tree/master/src/Bet.AspNet.LegacyHosting.AzureAppConfiguration">Learn more &raquo;</a>
            </p>
        </div>
    </div>

</asp:Content>
