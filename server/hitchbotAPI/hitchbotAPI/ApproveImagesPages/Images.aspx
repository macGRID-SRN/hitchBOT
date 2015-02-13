<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ApproveImagesPages/Shared/PageWithHeader.master"  CodeBehind="Images.aspx.cs"  Inherits="hitchbotAPI.ApproveImagesPages.Images" %>

<asp:Content ID="Content1" ContentPlaceHolderID="StyleContent" runat="server">
    <style type="text/css">
           
    #tableViewImage tbody td {
        max-width:200px;
    }
    #tableViewImage img {
    	padding-top:30px;
    	margin-right:-60px !important;
        width:300px !important;
    }

    .save_btn {
    	margin-top: 40px;
    	margin-bottom:10px;
      margin-right:20px;
	  background: #33cc52;
	  background-image: -webkit-linear-gradient(top, #33cc52, #2fa626);
	  background-image: -moz-linear-gradient(top, #33cc52, #2fa626);
	  background-image: -ms-linear-gradient(top, #33cc52, #2fa626);
	  background-image: -o-linear-gradient(top, #33cc52, #2fa626);
	  background-image: linear-gradient(to bottom, #33cc52, #2fa626);
	  -webkit-border-radius: 14;
	  -moz-border-radius: 14;
	  border-radius: 14px;
	  font-family: Arial;
	  color: #ffffff;
	  font-size: 20px;
	  padding: 5px 10px 5px 10px;
	  text-decoration: none;
	}

	.save_btn:hover {
	  background: #288520;
	  text-decoration: none;
	}

.remove_btn {
    	margin-top: 40px;
	margin-right: 50px;
    	margin-bottom:10px;
  background: #cc3333;
  background-image: -webkit-linear-gradient(top, #cc3333, #a62626);
  background-image: -moz-linear-gradient(top, #cc3333, #a62626);
  background-image: -ms-linear-gradient(top, #cc3333, #a62626);
  background-image: -o-linear-gradient(top, #cc3333, #a62626);
  background-image: linear-gradient(to bottom, #cc3333, #a62626);
  -webkit-border-radius: 14;
  -moz-border-radius: 14;
  border-radius: 14px;
  font-family: Arial;
  color: #ffffff;
  font-size: 20px;
  padding: 5px 10px 5px 10px;
  text-decoration: none;
}

.remove_btn:hover {
  background: #852121;
  text-decoration: none;
}

.table_block {
	border:25px solid #FFF;
	margin-left:150px !important;
	padding:30px;
	background: #EEE;
}

.table_block_inner td {
	max-width:20px !important;
}
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <form id="form1" runat="server">
        <div class="container">
            <h2>Approve/Delete hitchBot Images</h2>
            <h4>Note: Images are never really deleted. Removing them does just removes them from this page, and not from the databn</h4>
    
            <asp:Label ID="Label1" runat="server" Text="" Font-Strikeout="True"></asp:Label>
    
            <asp:Table ID="tableViewImage" runat="server">
            </asp:Table>
        </div>
    </form>
</asp:Content>
