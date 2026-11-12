<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SoapNoteView.ascx.cs" Inherits="QuivusManagerService.CustomeControls.General.SoapNoteView" %>
<%@ Register  TagName="Header" TagPrefix="SoapNote" Src="~/CustomeControls/General/SoapNote/SoapNoteHeader.ascx"  %>
<%@ Register  TagName="PatientView" TagPrefix="SoapNote" Src="~/CustomeControls/General/SoapNote/SoapNotePatient.ascx"  %>
<%@ Register  TagName="TableView" TagPrefix="SoapNote" Src="~/CustomeControls/General/SoapNote/SoapNoteGridView.ascx"  %>
<%@ Register  TagName="SignedBy" TagPrefix="SoapNote" Src="~/CustomeControls/General/SoapNote/SoapNoteSignedBy.ascx"  %>
<%@ Register  TagName="Footer" TagPrefix="SoapNote" Src="~/CustomeControls/General/SoapNote/SoapNoteFooter.ascx"  %>
<style type="text/css">

@media print
{
    #non-printable 
    { 
    display: none; 
    }
    
    .Printable
    { 
        display: block; 
    }
}
@media screen
{
  .Printable
  { 
      display: none; 
  }
}

body span {
font-family:Arial, Helvetica, "Helvetica Neue";
background-color:#FFF;
margin:0 auto;
padding:0;
font-size:11px;
}

#main {
width: 550px;
font-family:Arial, Helvetica, "Helvetica Neue";
font-size:11px;
margin:0 auto;
}

#print {
height:15px;
width:100%;
position:fixed;
z-index:1;
font-size:11px;
font-family:Arial, Helvetica, "Helvetica Neue";
}

#patientInfo
{
text-align: center;
padding:5px;
width:270px;
margin: 0 auto;
}

#header
{
text-align: center;
background-color:#C6D9F1;
padding:5px;
width:270px;
margin: 0 auto;
}

#header span
{
text-align: center;
background-color:#C6D9F1;
}

table
{
margin-left: auto;
margin-right: auto;
text-align: left;
border-width: 1px;
border-spacing: 1px;
border-style: solid;
border-color: gray;
border-collapse: collapse;
background-color: white;
}

td, span
{
padding:5px;
font-style: normal;
font-variant: normal;
font-size: 11px;
line-height: 100%;
word-spacing: normal;
letter-spacing: normal;
text-decoration: none;
text-transform: none;
text-align: left;
text-indent: 0ex;
position:relative;
vertical-align:top;
}

th, span
{
padding:5px;
font-style: normal;
font-variant: normal;
font-size: 13px;
line-height: 100%;
word-spacing: normal;
letter-spacing: normal;
text-decoration:bold;
text-transform: none;
text-align: left;
text-indent: 0ex;
position:relative;
vertical-align:top;
text-align:left;
background-color:#C6D9F1;
}

.name
{
font-weight: normal;/*bold|normal*/
width:15%;

}

.details
{
font-weight: normal;/*bold|normal*/
width: 50%;

}

.notes
{
font-weight: normal;/*bold|normal*/
width:35%;

}

.name_bg
{
    background-color:#BFBFBF;
	font-weight:bold;
}

.notes_bg
{
    background-color:#FFFFCC;
}

.heading
{
font-weight: bold;/*bold|normal*/
font-size:15px;
color:#0000FF;
text-transform:uppercase;
line-height:40px;
}

.FirstCol
{
background-color:#C6D9F1;
font-weight:normal;
}

.SecondCol
{
font-weight:bold;
}

#SignatureInfo
{
text-align: center;
background-color:#C6D9F1;
padding:5px;
width:300px;
margin: 0 auto;
}

#SignatureInfo table
{
text-align: center;
background-color:#C6D9F1;
border:0;
}

#SignatureInfo table tr td
{
border:0;
}

#SignatureInfo span
{
text-align: center;
background-color:#C6D9F1;

}

.font-bold
{
    font-weight:bold;
}

hr 
{
  border: 0;
  width: 100%;
  color: #cccccc;
  background-color: #cccccc;
  height: 1px;
}

</style>

<SoapNote:Header runat="server" ID="header" ></SoapNote:Header>
<SoapNote:PatientView runat="server" ID="patientDetails"></SoapNote:PatientView>
<SoapNote:TableView runat="server" ID="history"></SoapNote:TableView>
<SoapNote:TableView runat="server" ID="subjective"></SoapNote:TableView>
<SoapNote:TableView runat="server" ID="objective"></SoapNote:TableView>
<SoapNote:TableView runat="server" ID="assessment"></SoapNote:TableView>
<SoapNote:TableView runat="server" ID="plan"></SoapNote:TableView>
<SoapNote:SignedBy runat="server" ID="signedBy"></SoapNote:SignedBy>
<SoapNote:Footer runat="server" ID="footer"></SoapNote:Footer>






