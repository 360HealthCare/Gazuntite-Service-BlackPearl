<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SoapNoteSignedBy.ascx.cs" Inherits="QuivusManagerService.CustomeControls.General.SoapNote.SoapNoteSignedBy" %>
<div id="SignatureInfo">
    <table cellspacing="0" rules="all" border="1" id="patientTable">
        <tr>
            <td><asp:Label ID="Label1" runat="server" Text="Electronically Signed by:" ></asp:Label></td>
            <td><asp:Label ID="lblProviderName" runat="server" Text="" CssClass="font-bold" ></asp:Label></td>
        </tr>
        <tr>
            <td><asp:Label ID="lblCC" runat="server" Text="CC:" style="text-align: right" Width="100%" ></asp:Label></td>
            <td><asp:Label ID="lblCCProviders" runat="server" Text="" CssClass="font-bold" ></asp:Label></td>
        </tr>
    </table>
</div>
<br />