<%@ Page Language="C#" MasterPageFile="~/User/UserPage.master" AutoEventWireup="true" CodeFile="Default3.aspx.cs" Inherits="Default3" Title="无标题页" %>

<%@ Register src="User/CertificateGenerate.ascx" tagname="CertificateGenerate" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <uc1:CertificateGenerate ID="CertificateGenerate1" runat="server" />
</asp:Content>

