<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SoapNotePatient.ascx.cs" Inherits="QuivusManagerService.CustomeControls.General.SoapNote.SoapNotePatient" %>
<div id="patientInfo">
    <table cellspacing="0" rules="all" border="1" id="patientTable">
        <tr>
            <th><asp:Label ID="Label1" CssClass="FirstCol" runat="server" Text="Patient Name:" ></asp:Label></th>
            <td><asp:Label ID="lblPatientName" CssClass="SecondCol" runat="server" Text="" ></asp:Label></td>
        </tr>
        <tr>
            <th><asp:Label ID="Label2" CssClass="FirstCol" runat="server" Text="Date of Birth:" ></asp:Label></th>
            <td><asp:Label ID="lblDOB" CssClass="SecondCol" runat="server" Text="" ></asp:Label></td>
        </tr>

         <tr>
            <th><asp:Label ID="Label3" CssClass="FirstCol" runat="server" Text="Date of Service:" ></asp:Label></th>
            <td><asp:Label ID="lblDOService" CssClass="SecondCol" runat="server" Text="" ></asp:Label></td>
        </tr>
        <tr>
            <th><asp:Label ID="Label4" CssClass="FirstCol" runat="server" Text="Primary Care Provider:" ></asp:Label></th>
            <td><asp:Label ID="lblProviderName" CssClass="SecondCol" runat="server" Text="" ></asp:Label></td>
        </tr>
    </table>
</div>