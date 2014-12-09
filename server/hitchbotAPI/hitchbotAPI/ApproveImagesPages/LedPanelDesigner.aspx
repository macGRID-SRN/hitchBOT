<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LedPanelDesigner.aspx.cs" Inherits="hitchbotAPI.ApproveImagesPages.LedPanelDesigner" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <h3  style="text-align: center">
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="LandingPage.aspx">Back</asp:HyperLink>
            <br />
            Design New Face LED Panels for hitchBOT</h3>
        <div style="height: 100px" ><p style="text-align: center">Upload an Image (png, or jpg only)
            </p></div>
        <div  style="text-align: center">
            <asp:Label ID="lblWarning" runat="server"></asp:Label>
        </div>
    <div  style="text-align: center">
    
        <asp:FileUpload ID="fileUploadImage" runat="server" />
    
    </div>
        <div style="height: 50px"></div>
        <div style="text-align: center">

            <asp:Label ID="Label1" runat="server" Text="Name:" Style="vertical-align: middle;"></asp:Label>

            <asp:TextBox ID="txtImageName" runat="server"></asp:TextBox>

        </div>
        <div style="height: 50px"></div>
        <div  style="text-align: center">

            <asp:Label ID="Label2" runat="server" Text="Description:" ></asp:Label>
            <asp:TextBox ID="txtImageDescription" runat="server" Height="125px" Style="vertical-align: top;"></asp:TextBox>

        </div>
        <div style="height: 50px"></div>
        <div  style="text-align: center" >
           
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
           
        </div>
    </form>
</body>
</html>
