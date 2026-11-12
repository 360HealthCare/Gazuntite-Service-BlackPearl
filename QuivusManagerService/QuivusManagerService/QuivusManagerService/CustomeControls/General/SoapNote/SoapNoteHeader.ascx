<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SoapNoteHeader.ascx.cs" Inherits="QuivusManagerService.CustomeControls.General.SoapNote.SoapNoteHeader" %>
<style type="text/css">
    .style1
    {
        width: 263px;
        height: 381px;
    }
</style>
<img id="logo" runat="server" height="136" width="126" alt="pLogo"
        style="float:left;"/>
<div id="header" >

        <%--ImageUrl="../../Images/GeaterLogo3.jpg"--%>
<%--<asp:Label ID="lblClinicName" runat="server" Text="" 
    style="font-weight: 700; font-family: Arial, Helvetica, sans-serif; font-size: small; text-align: center; left:5px" ></asp:Label>
<br />--%>
<asp:Label ID="lblClinicAddress" runat="server" Text=""></asp:Label>
<br />
<asp:Label ID="lblClinicPhone" runat="server" Text=""></asp:Label>
<br />
<asp:Label ID="lblClinicFax" runat="server" Text=""></asp:Label>

</div>
<br />


