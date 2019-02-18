<%@ Page Language="VB" AutoEventWireup="false" Inherits="VSSA.ReservationPrint" Codebehind="ReservationPrint.aspx.vb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Print Reservation Confirmation </title>
    <link href="../css/vssa.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="frmSearch" runat="server">
		<!-- BEGIN HEADER -->
		<table style=" margin-left:auto; margin-right:auto;" id="" cellSpacing="0" cellPadding="0" width="" border="0"> 
		
			<tr >
				<td align="center" vAlign="top" width="214"><IMG height="30" alt="" src="../images/topBarGreyLinesShadow.gif" width="214" border="0"></td>
				<td align="center" vAlign="top" width="176"><IMG height="30" alt="" src="../images/topBrGryLinesShdowBckrnd.gif" width="176" border="0"></td>
				<td align="center" vAlign="top" width="199"><IMG height="30" alt="" src="../images/topBrGryDots.gif" width="199" border="0"></td>
				<td align="center" vAlign="top" width="171"><IMG height="30" alt="" src="../images/topBrGrnDots.gif" width="168" border="0"></td>
			</tr>
			<tr>
				<td colspan="4" style="height:20px;" >
				</td>
			 </tr>
			 
		</table>
		<!--END HEADER -->
  <!--Begin Body-->
  <table id="tblMainContainer" cellpadding="0" cellspacing="0" style="margin-left: auto;
	  margin-right: auto;">
	<tr>
         <td id="tblMainColMarginLeft" style="width: 140px;">
         </td> 
	    <!--BEGIN Search Panel -->
	    <td valign="top" background="../images/reservBoxBkgd2.gif" height="100" style="width: 495px">
			<table id="tblInnerControlContainer" cellSpacing="0" cellPadding="0">
				<tr id="rowError" runat="server" visible="false">
					<td></td>
					<td valign="top" align=right>
						<asp:Image ID="imgError" runat="server" ImageUrl="../images/alert.gif" />
					</td>
					<td align="left" valign="bottom" >
						<asp:Label ID="lstErrors"  runat="server" CssClass="text" ForeColor="red"></asp:Label>
					</td>
				</tr>	
				<tr id="tblInnerMarginTop">
					<td>
					
				    </td>	
				   <td colspan="2" align="center"  style="height:50px;">
				   <br />
				   <asp:Label Font-Size="Small" ID="lblTitle" runat="server" CssClass="Font10pxBOLD" Text="Print Reservation Confirmations"></asp:Label></td>
				   <br />
				</tr>		
				
				<tr>
					<td  valign="top">
						&nbsp;
					</td>
					<td colspan="2" align="center" >
						<br />
						<asp:ImageButton AlternateText="Search owner reservations" ID="btnSubmit" runat="server" ImageUrl="../images/search_btn.gif"/>&nbsp; 
						
						<br />
						<br />
					</td>
				</tr>
			 </table> 
	     </td>
	     <!--End Search Panel-->
	<td id ="tblMaincolmarginRight" style="width: 140px;">&nbsp;</td> 
	</tr>
      <tr><td><br /></td></tr>
	<tr>
		<td colspan="3" align="center">
			<asp:Repeater ID="rptReservations" runat="server">
				<HeaderTemplate>
                    <table border="1"  style="border-width:thin;" cellpadding="0" visible="true" cellspacing="0" width="650px">
						<tr width="650px">
							<td colspan="5"  align="left" style="background-color:Black;">
								<asp:Label ID="Label1" ForeColor="white" runat="server" CssClass="Font10pxBOLD" Text="Pending Reservations"></asp:Label>
						     </td> 
						     <td  style="background-color:Black;" align="right">
								<asp:LinkButton ForeColor="white" runat="server" ID="LinkButton1" CommandName="selectAll" Text="Select All" CssClass="Font10pxBOLD"></asp:LinkButton>
						     </td>
						</tr>
                        </table>
					<table border="1"  style="border-width:thin;" cellpadding="0" visible="true" cellspacing="0" width="650px">
						
					     <tr class="orange">
						  <td >
							<span class="textBlack">Confirmation Number</span>
					       </td>
						  <td>
							  <span class="textBlack">Resort Name</span>
						  </td>
						  <td>
							  <span class="textBlack">Check In Date</span>
						  </td>	
						  <td>
							  <span class="textBlack">Check Out Date</span>	
						  </td>	
						  <td>
							  <span class="textBlack">Reservation Type</span>
						  </td>
                             <td>
							  <span class="textBlack">Created Date</span>
						  </td>	
						  <td>
							  <span class="textBlack">Print Confirmation</span>
						  </td>	
					</tr>
				</HeaderTemplate>
				<ItemTemplate>
					<tr>
						<td>
							<asp:Label ID="lblConfirmationNumber" Text="N/A" runat="server" CssClass="text"></asp:Label>
						</td>
						<td>
							<asp:Label ID="lblResortName" runat="server" Text="N/A" CssClass="text"></asp:Label>
						</td>
						<td>
							<asp:Label ID="lblCheckinDate" runat="server" Text="N/A"  CssClass="text"></asp:Label>
						</td>
						<td>
							<asp:Label ID="lblCheckoutDate" runat="server" CssClass="text" Text="N/A"></asp:Label>
						</td>
						<td>
							<asp:Label ID="lblReservationType" runat="server" CssClass="text" Text="N/A"></asp:Label>
						</td>
                        <td>
							<asp:Label ID="lblCreatedDate" runat="server" CssClass="text" Text="N/A"></asp:Label>
						</td>
						<td>
							<asp:CheckBox ID="chkPrint" runat="server"></asp:CheckBox>
						</td>
				       	
					</tr>
				</ItemTemplate>
				<AlternatingItemTemplate>
					<tr class="orange">
						<td>
							<asp:Label ID="lblConfirmationNumber" runat="server" CssClass="textBlack" Text="N/A"></asp:Label>
						</td>
						<td>
							<asp:Label ID="lblResortName" runat="server" CssClass="textBlack" Text="N/A"></asp:Label>
						</td>
						<td>
							<asp:Label ID="lblCheckinDate" runat="server" CssClass="textBlack" Text="N/A"></asp:Label>
						</td>
						<td>
							<asp:Label ID="lblCheckoutDate" runat="server" CssClass="textBlack" Text="N/A"></asp:Label>
						</td>
						<td>
							<asp:Label ID="lblReservationType" runat="server" CssClass="textBlack" Text="N/A"></asp:Label>
						</td>
                          <td>
							<asp:Label ID="lblCreatedDate" runat="server" CssClass="text" Text="N/A"></asp:Label>
						</td>
						<td>
							<asp:CheckBox ID="chkPrint" runat="server" />
						</td>
				</AlternatingItemTemplate>
				<FooterTemplate>
				   </table>
				</FooterTemplate>
			</asp:Repeater>
			<br />
			<asp:ImageButton ID="btnPrinConfirmations" Visible="false" ImageUrl="../images/submitBtn.gif" runat="server" />
		</td>
	</tr>	
  </table>
 <!--END Body-->
 </form>
</body>
</html>
