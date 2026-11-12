<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SoapNoteGridView.ascx.cs" Inherits="QuivusManagerService.CustomeControls.General.SoapNote.SoapNoteGridView" %>
<asp:Label ID="Label1" runat="server" CssClass="heading"></asp:Label>
<br />
<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  >
    <Columns>
        <asp:BoundField DataField="Name" HeaderText="Section" ItemStyle-CssClass="name name_bg"
            SortExpression="Name"  />
        <asp:BoundField DataField="Particulars" HeaderText="Details" ItemStyle-CssClass="details"
            SortExpression="Particulars" />
        <asp:BoundField DataField="Notes" HeaderText="Notes" ItemStyle-CssClass="notes notes_bg"
            SortExpression="Notes" />
    </Columns>
</asp:GridView>
<br />