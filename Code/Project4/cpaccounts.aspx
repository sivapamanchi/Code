 <%@ Page Language="VB" AutoEventWireup="false" EnableEventValidation="false" Inherits="VSSA.cpaccounts" Codebehind="cpaccounts.aspx.vb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Choice Privileges</title>
    <link href="Styles/vssa.css" rel="stylesheet" type="text/css" />
    <link href="Styles/ChoiceStyleSheet.css" rel="stylesheet" type="text/css" />
   <link href="Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="Styles/AjaxCalendar.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript">
        function CloseWindow() {
            window.close();
        }
    </script>  
    <script type = "text/javascript">
        function GetSelectedRow(lnk) {
            var row = lnk.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var customerId = row.cells[0].innerHTML;
            var city = row.cells[1].getElementsByTagName("input")[0].value;
            alert("RowIndex: " + rowIndex + " ChoicePrivilegeID: " + customerId + " Person.FirstName:" + city);
            return false;
        }
    </script>
     <script type="text/javascript">
         function popWin() {
             window.open('cpConversionConfirm.aspx',
                          'newWin1',
                          'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,width=230,height=450,left=50,top=50'
                          , '')
         }
   </script>

     
     
      <style type="text/css">

    .modalBackground {
        background-color:Gray;
        filter:alpha(opacity=70);
        opacity:0.7;
    }

    .modalPopup {
        background-color:#ffffdd;
        border-width:3px;
        border-style:solid;
        border-color:Gray;
        padding:3px;
        width:350px;
    }

          .TextBoxChoiceAccountTable {
          width:97%;
          border: solid;
          border-width: 2px;

          }


          .MyTable{
    border-collapse: collapse;
    border: 1px;
    width: 100%;
}

       .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
             width: auto;
        }
        .auto-style3 {
            width: auto;
            border:none;
            text-align:center;
        }
        .TableHistory {
            border-collapse: collapse;
            border: 3px;
            width: 800px;
            border-spacing:3px 5px;
            
            
        }
          .trDetails {
              background-color: #cfcfcf;
              color:black;
              text-align:center;
              font-size:9;
            
          }
    
    </style>
   
</head>
<body >

    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>

     
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0" DynamicLayout="true">
    <ProgressTemplate>  
          <div class="overlay">
            <div class="overlayContent">
                <asp:Image ID="Image1" runat="server" ImageUrl="images/icon-search-loading.gif" />
            </div>
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
    <!-- BEGIN HEADER -->   
     <div>
            <!-- TOP BAR START -->
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td style="vertical-align: top; width: 214">
                        <img height="30" alt="" src="images/topBarGreyLinesShadow.gif" width="214" style="border: none" />
                    </td>
                    <td style="vertical-align: top; width: 176">
                        <img height="30" alt="" src="images/topBrGryLinesShdowBckrnd.gif" width="176" style="border: none" />
                    </td>
                    <td style="vertical-align: top; width: 199">
                        <img height="30" alt="" src="images/topBrGryDots.gif" width="199" style="border: none" />
                    </td>
                    <td style="vertical-align: top; width: 171">
                        <img height="30" alt="" src="images/topBrGrnDots.gif" width="168" style="border: none" />
                    </td>
                </tr>
                <tr>
                    <td align="center" valign="top" width="214">
                    </td>
                    <td align="center" valign="top" width="176">
                    </td>
                    <td colspan="2" align="right" valign="top">
                        <asp:LinkButton runat="server" ID="lbNewSearch" Text="New Search" CssClass="text"
                            Font-Underline="true" ForeColor="blue"  PostBackUrl="~/Default.aspx"/>
                        |
                        <asp:HyperLink NavigateUrl="http://boss" Font-Underline="true" ForeColor="blue" ID="HyperLink2"
                            runat="server" Text="BOSS" CssClass="text"></asp:HyperLink>
                        
                    </td>
                </tr>
            </table>
    </div>
    <!--END BEGIN HEADER -->
    <br /><br />
    <!-- ERROR MESSAGE--> 
     <div>
        <asp:Label ID="lblOwnerAccountError" runat="server" Text="" Font-Bold="True" ForeColor="Maroon"></asp:Label>    
    </div>
    <!-- END ERROR MESSAGE --> 
     <div class="clearfix">
            <div id="divSearchResults" runat="server">
                <asp:GridView runat="server" ID="gvSearchResults" AutoGenerateColumns="false" HeaderStyle-BackColor="darkGray"
                    HeaderStyle-CssClass="Font10px" EmptyDataText="Could not find any owner with information entered."
                    EmptyDataRowStyle-CssClass="Font10px" BorderStyle="none" Width="760PX">
                    <Columns>                       
                        <asp:BoundField DataField="ARVACT" HeaderText="ARVACT" ItemStyle-CssClass="Font10px" />
                        <asp:BoundField DataField="Reg" HeaderText="Reg." ItemStyle-CssClass="Font10px" />
                        <asp:BoundField DataField="NameFirst" HeaderText="First Name" ItemStyle-CssClass="Font10px" />
                        <asp:BoundField DataField="NameLast" HeaderText="Last Name" ItemStyle-CssClass="Font10px" />
                        <asp:BoundField DataField="Email" HeaderText="Email Address" ItemStyle-CssClass="Font10px" />
                        <asp:BoundField DataField="PhoneHome" HeaderText="Primary Phone" ItemStyle-CssClass="Font10px" />
                        <asp:BoundField DataField="Address1" HeaderText="Address" ItemStyle-CssClass="Font10px" />
                        <asp:BoundField DataField="City" HeaderText="City" ItemStyle-CssClass="Font10px" />
                        <asp:BoundField DataField="State" HeaderText="State" ItemStyle-CssClass="Font10px" />
                        <asp:BoundField DataField="PostalCode" HeaderText="ZIP" ItemStyle-CssClass="Font10px" />
                        <asp:BoundField DataField="Last4SSN" HeaderText="SSN" ItemStyle-CssClass="Font10px" />
                    </Columns>
                </asp:GridView>
                
            </div>
    </div>
     <br /><br />
     <div id="moreInfo" runat="server">
            <br />
            <asp:Label ID="lblTwsID" runat="server" Text="TSW Person ID: " CssClass="Font10pxBOLD" Visible="False" />
            <asp:Label ID="lbltswPersonID" runat="server" Text='' CssClass="Font10px" Visible="true" /><br />
            <asp:Label ID="Reg" runat="server" Text="Registered: " CssClass="Font10pxBOLD" Visible="False" />
            <asp:Label ID="lblRegistered" runat="server" Text='' CssClass="Font10px" Visible="true" /><br />
            <asp:Label ID="Label1" runat="server" Text="Traveler Plus Expiration: " CssClass="Font10pxBOLD"
                Visible="False" />
            <asp:Label ID="lblTPExp" runat="server" Text='' CssClass="Font10px" Visible="true" /><br />
            <asp:Label ID="label2" runat="server" Text="Collection Code: " CssClass="Font10pxBOLD"
                Visible="false" />
            <asp:Label ID="lblCollCode" runat="server" Text='' CssClass="Font10px" Visible="true" /><br />
             <asp:Label ID="lblPoints" runat="server" Text=" " CssClass="Font10pxBOLD"
                 />
            <asp:Label ID="lblAnnualPoints" runat="server" Text='' CssClass="Font10px" Visible="true" /><br /><br />
            <a href="javascript:window.history.go(<%=Session("HistoryCount")%>)">[Go Back]</a>
        </div>
        <br />
        <br />
     <div id="content" style="width:800px;" >
        
        <div id="left">
          <asp:UpdatePanel ID="UpdPnlAccounts" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                 <table  width="547" class="MyTable">
                    <tr>
                        <td>
                         <asp:GridView ID="GridViewCHCAccounts" runat="server" AutoGenerateColumns="False" BackColor="White" 
                                BorderStyle="None" BorderWidth="0px" CellPadding="2" ForeColor="Black" DataKeyNames="Identifier,ChoicePrivilegeID" 
                                OnRowCommand="GridViewCHCAccounts_RowCommand" OnRowDeleting="GridViewCHCAccounts_RowDeleting" OnRowEditing="GridViewCHCAccounts_RowEditing" OnRowUpdating="GridViewCHCAccounts_RowUpdating" OnRowCancelingEdit="GridViewCHCAccounts_RowCancelingEdit"  showfooter="true" CssClass="gridview-inline">
                                <HeaderStyle height="25px"  HorizontalAlign="center" ForeColor="white" BackColor="#333333">
                                </HeaderStyle>
                                    <rowstyle backcolor="#cfcfcf"  />
                                <alternatingrowstyle backcolor="#efefef"   />
                                <Columns>                                                                                                
                                    <asp:TemplateField HeaderText="Choice Privileges Member #" ControlStyle-Width="140px" >
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TxtEdtChoicePrivilegeID" runat="server" Text='<%# Bind("ChoicePrivilegeID") %>'  BorderStyle="Solid" Height="35" MaxLength="35" OnClientClick = "return GetSelectedRow(this)"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate >
                                            <asp:Label ID="LblChoicePrivilegeID" runat="server" Text='<%# Bind("ChoicePrivilegeID") %>' ItemStyle-Wrap="true" ></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="TxtInsChoicePrivilegeID" runat="server"  CssClass="TextBoxChoiceAccountTable" Text='' MaxLength="35"></asp:TextBox>
                                        </FooterTemplate>
                                        <ControlStyle Font-Size="11px" />
                                        <HeaderStyle Font-Size="11px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="First Name" ControlStyle-Width="70px"  >
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TxtEdtFirstName" runat="server" Text='<%#BindingFirstName %>' BorderStyle="Solid" Height="15" MaxLength="35"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                           <asp:Label ID="LblFirstName" runat="server" Text= '<%#BindingFirstName %>'  style="display:inline;" ></asp:Label>
                                       </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="TxtInsFirstName" runat="server" Text='' CssClass="TextBoxChoiceAccountTable" MaxLength="35"></asp:TextBox>
                                        </FooterTemplate>
                                        <ControlStyle Font-Size="11px"   />
                                        <HeaderStyle Font-Size="11px" />
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last Name" ControlStyle-Width="70px" ItemStyle-Wrap="true">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TxtEdtLastName" runat="server" Text='<%#BindingLastName()%>' BorderStyle="Solid" Height="15" MaxLength="35"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="LblLastName" runat="server" Text= '<%#BindingLastName()%>'  style="display:inline;" ></asp:Label>
                                             <input id="inpHide" type="hidden" runat="server" /> 
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="TxtInsLastName" runat="server" Text=''  CssClass="TextBoxChoiceAccountTable" MaxLength="35"></asp:TextBox>
                                        </FooterTemplate>
                                        <ControlStyle Font-Size="11px" />
                                        <HeaderStyle Font-Size="11px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField  ControlStyle-Width="50px">
                                        <EditItemTemplate >
                                            <asp:LinkButton ID="LinkButtonUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                                Text="Update"></asp:LinkButton>                                            
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                                Text="Edit"></asp:LinkButton>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Button Font-Size="Smaller" ID="btnInsert" runat="Server" Text="Save" CommandName="Insert" CommandArgument="1" UseSubmitBehavior="False" />
                                            
                                        </FooterTemplate>
                                        <ControlStyle Font-Size="11px" />
                                        <HeaderStyle Font-Size="11px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ControlStyle-Width="50px" >
                                       <EditItemTemplate >                                          
                                            <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                                Text="Cancel"></asp:LinkButton>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButtonDelete" runat="server" CausesValidation="False" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete the account?');"
                                                Text="Delete"></asp:LinkButton>
                                                                                                   
                                        </ItemTemplate>
                                            <FooterTemplate>
                                            <asp:Button  Font-Size="Smaller" ID="btnCancel" runat="Server" Text="Cancel" CommandName="Cancel" UseSubmitBehavior="False" />
                                        </FooterTemplate>
                                        <ControlStyle Font-Size="11px"  />
                                        <HeaderStyle Font-Size="11px"  />
                                    </asp:TemplateField>
                                </Columns>                                                                                           
                              
                                <HeaderStyle BackColor="#333333"   />
                                                                                           
                            </asp:GridView>
                          </td>
                    </tr>
                    <tr>
                        <td>
                        <asp:Label ID="LblErrMessage" ForeColor="Red" Font-Size="Smaller" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>   
                    <tr>
                        <td>Not a member? <a href="http://www.choicehotels.com/ires/bluegreen/en-us/CPApplication" target="_blank">Click here</a> to enroll in Choice Privileges.</td>
                    </tr>                                                                           
                 </table>
            </ContentTemplate>
            
          </asp:UpdatePanel>
        <br />
    </div>  
        <div id="right" >
           <asp:UpdatePanel ID="UpdPnlAccountHistory" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" TargetControlID="PnlHistory" CollapseControlID="PanelOlpHdr" ExpandControlID="PanelOlpHdr"
                    Collapsed="true"  TextLabelID="lblText" CollapsedText="Click to Show Choice Privileges Accounts History.." ExpandedText="Click to Hide Choice Privileges Accounts History.."
                    CollapsedSize="0">
                </cc1:CollapsiblePanelExtender>  
                     <asp:Panel ID="PanelOlpHdr" runat="server" Width="547" >
                         <img alt="" src="images/collapse.jpg" /> 
                         <asp:Label ID="lblText" runat="server" Text="Label"></asp:Label><br /><br />

                    </asp:Panel>
                    <asp:Panel ID="PnlHistory" Visible="true" runat="server">
                         <table >
                            <tr>
                                <td>
                               
                                    
                                      <asp:Repeater ID="rptAccountHistory" runat="server" >
                                        <HeaderTemplate>
                                             
                                         <table class="TableHistory" border="3" cellspacing="2">
                                          <tr>
                                            <td class="auto-style3" colspan="3" style="background-color:Gray;">Before</td>
                                            <td class="auto-style3" colspan="3"  style="background-color:Silver;">After</td>
                                            <td class="auto-style3">&nbsp;</td>
                                            <td class="auto-style3">&nbsp;</td>
                                        </tr>
                                            <tr align="center" style="font-size: 9; background-color: #333333;color:white">
                                                <td class="auto-style2">
                                                   Choice Id
                                                </td>
                                                <td  class="auto-style2">
                                                    First Name
                                                </td>
                                                <td class="auto-style2">
                                                   Last Name
                                                </td>
                                                <td class="auto-style2">
                                                    Choice Id
                                                </td>
                                                <td class="auto-style2">
                                                   First Name
                                                </td>
                                                <td class="auto-style2">
                                                    Last Name
                                                </td>
                                                
                                                <td class="auto-style2">
                                                    Updated
                                                </td>
                                                <td class="auto-style2">
                                                   Source
                                                </td>
                                              
                                            </tr>
                                            
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                       
                                        <tr class="trDetails"                           >
                                            <td class="auto-style2">
                                              <asp:Label ID="lblPreChcId" runat="server" Text='<%#IIf(IsDBNull(DataBinder.Eval(Container.DataItem, "PrevChoicePrivilegeID")), "", DataBinder.Eval(Container.DataItem, "PrevChoicePrivilegeID"))%>'> </asp:Label>
                                            </td>
                                            <td class="auto-style2">
                                                
                                                <asp:Label ID="lblPreFname" runat="server"  Text='<%#IIf(IsDBNull(DataBinder.Eval(Container.DataItem, "Person.PrevFirstName")), "", DataBinder.Eval(Container.DataItem, "Person.PrevFirstName"))%>' />
                                            </td>
                                            <td class="auto-style2">
                                                <asp:Label ID="lblPreLname" runat="server" ForeColor="black" Text='<%#IIf(IsDBNull(DataBinder.Eval(Container.DataItem, "Person.PrevLastName")), "", DataBinder.Eval(Container.DataItem, "Person.PrevLastName"))%>'></asp:Label>
                                            </td>
                                            <td class="auto-style2">
                                                <asp:Label ID="lblCurChcId" runat="server" ForeColor="black" Text='<%#IIf(IsDBNull(DataBinder.Eval(Container.DataItem, "ChoicePrivilegeID")), "", DataBinder.Eval(Container.DataItem, "ChoicePrivilegeID"))%>'></asp:Label>
                                            </td>
                                            <td class="auto-style2">
                                                <asp:Label ID="lblCurFname" runat="server" ForeColor="black" Text='<%#IIf(IsDBNull(DataBinder.Eval(Container.DataItem, "Person.FirstName")), "", DataBinder.Eval(Container.DataItem, "Person.FirstName"))%>'></asp:Label>
                                            </td>
                                            <td class="auto-style2">
                                                <asp:Label ID="lblCurLname" runat="server" ForeColor="black" Text='<%#IIf(IsDBNull(DataBinder.Eval(Container.DataItem, "Person.LastName")), "", DataBinder.Eval(Container.DataItem, "Person.LastName"))%>'></asp:Label>
                                            </td>
                                           
                                            <td class="auto-style2">
                                                <asp:Label ID="lblUpdateDate" runat="server" ForeColor="black" Text='<%#IIf(IsDBNull(DataBinder.Eval(Container.DataItem, "MetaData.UpdateDate", "{0:MM/dd/yyyy}")), "", DataBinder.Eval(Container.DataItem, "MetaData.UpdateDate", "{0:MM/dd/yyyy}"))%>'>'></asp:Label>
                                            </td>
                                            <td class="auto-style2">
                                               <asp:Label ID="lblCreatedBy" runat="server" ForeColor="black" Text='<%#IIf(IsDBNull(DataBinder.Eval(Container.DataItem, "MetaData.Requester")), "", DataBinder.Eval(Container.DataItem, "MetaData.Requester"))%>'></asp:Label>
                                            </td>
                                           
                                        </tr>
                                     
                                    </ItemTemplate>
                                     <FooterTemplate>
                                        <tr>
                                            <td valign="middle" align="center" bgcolor="#333333" colspan="9">
                                            </td>
                                        </tr>
                                        </table>
                                    </FooterTemplate>
                                    </asp:Repeater>
                                    <br /><br />
                                    
								</td>
                            </tr>
                        </table>
                    </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
           <asp:UpdatePanel ID="UpdPnlConversion" runat="server" UpdateMode="Conditional">
                <ContentTemplate>  
                <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" runat="server" TargetControlID="PnlChoicePoints" CollapseControlID="PnlConvertPoints" ExpandControlID="PnlConvertPoints"
                    Collapsed="true" TextLabelID="lblConvertPoints" CollapsedText="Click to Show Choice Privileges Convert Points form.." ExpandedText="Click to Hide Choice Privileges Convert Points form.."
                    CollapsedSize="0">
                </cc1:CollapsiblePanelExtender> 

                         <asp:Panel ID="PnlConvertPoints" runat="server" Width="547" >
                         <img src="images/collapse.jpg"  alt=""/> 
                         <asp:Label ID="lblConvertPoints" runat="server" Text="Label"></asp:Label><br /><br />
                    </asp:Panel>

                         <asp:Panel ID="PnlChoicePoints" runat="server" Width="596px" Height="799px">
                            <asp:Panel ID="pnlError" runat="server" Visible ="false">
                                         <table cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td valign="top" height="20px">
                                                        <asp:Image ID="imgAlert" runat="server" ImageUrl="images/alert.gif" Visible="false" />&nbsp;
                                                    </td>
                                                    <td align="left"  valign="top" >
                                                        <asp:Label ID="lblError" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                              </asp:Panel>
                              
                            <div id="chc-points" style="width:547;">
                                
                                        <fieldset id="chbilling" visible="true" runat="server" title="Convert Points">
										<div class="title">
											<span><font><b>*</b></font> Indicates required field</span><h3>Convert Points</h3>
										</div>
										<ol>
                                      
                                            
											<li class="clear">
												<label for="eligiblepoints"><strong><span></span>Eligible Points to Convert:</strong></label>
                                                <asp:Label ID="txteligiblepoints" runat="server" Text=""></asp:Label><span    style="text-align:right;  padding-left:60px;">Not a member? <a href="http://www.choiceprivileges.com/signup/BLUEGALL" target="_blank">Click here </a> to sign up!</span>

											</li>

                                             <li >
												<label for="choiceid1"><strong><span >*</span>Select Owner:</strong></label>
												<asp:DropDownList runat="server" ID="ddlOwner"  TabIndex="2" AutoPostBack="true" >	
											    </asp:DropDownList>&nbsp;&nbsp;<span >**</span>
											</li>   

                                             <panel id="PnlOtherId" runat="server" Visible ="false" >
                                                 <li class="clear">
												    <label for="firstname"><strong><span class="orange"></span>First Name:</strong></label>
                                                   <asp:TextBox ID="txtFirstName" runat="server" size="18" TabIndex="1" MaxLength="35" onkeydown = "return (event.keyCode!=14);"></asp:TextBox> 

											    </li>

                                                <li class="clear">
												    <label for="lastname"><strong><span class="orange"></span>Last Name:</strong></label>
                                                  <asp:TextBox ID="txtLastName" runat="server" size="18" TabIndex="1" MaxLength="35" ></asp:TextBox> 
											    </li>

                                                 <li >
                                                 <label for="choiceid2"><strong><span >*</span>Choice Privileges member #:</strong></label>
                                                  <asp:TextBox ID="txtChoiceId" runat="server" size="18" TabIndex="1" MaxLength="35" ></asp:TextBox> 
                                                </li>
                                            </panel>
                                            <panel id="PnlChoiceId" runat="server" Visible ="true" >
                                            <li >
												<label for="choiceid"><strong><span >*</span>Choice Privileges member #:</strong></label>
												<asp:Label ID="LblChoiceId" runat="server" Text=""></asp:Label>
                                                <%--<asp:DropDownList runat="server" ID="ddlChoiceId"  TabIndex="2"  >	
											    </asp:DropDownList>&nbsp;&nbsp;<span >**</span>--%>
                                               
											</li>
                                            </panel>                                
                                           
											<li>
												<label for="bvcptstochc"><strong><span >*</span>Number of Points you wish to convert:</strong></label>
												<asp:TextBox ID="txtBvcToChcPts" runat="server" size="18"   AutoPostBack="true" TabIndex="3" MaxLength="10"></asp:TextBox>
                                                 
											</li>
                                            <li >
												<label for="chcpts"><strong><span class="orange"></span>Resulting <b>Choice Privileges</b> points:</strong></label>
                                                <asp:Label ID="txtChcPts" runat="server" Text=""></asp:Label>
                                              </li>
                                               
										</ol>
                                        <div  style="text-align:left; padding-top:0px; padding-bottom:5px ">&nbsp;</div>
									</fieldset>
                                    
                                    <div  style="text-align:left; padding-top:5px; padding-bottom:20px "><span >** </span>Note: Name must correspond to the name on your <b>Choice Privileges</b> account.</div>							           
                                    
                                    
                                    
									
									<fieldset visible="true"  runat="server" id="chcredit" title="Credit Card Information">
									<div class="title"><h3>Credit Card Information</h3><span><img alt="" src="images/rent-points-lock.gif"></span></div>
									<ol>
										<li class="clear">
											<label for="payment"><span >*</span><strong>Payment: $</strong></label>
											
                                            <asp:DropDownList ID="ddlAmount" runat="server">
                                            <asp:ListItem Value="50.00" Selected="True" Text="$50.00"></asp:ListItem> 
                                            <asp:ListItem Value="60.00"  Text="$60.00"></asp:ListItem> 
                                            </asp:DropDownList>
                                           
										</li>
                                        <li class="clear">
											<label for="nameNOnCard"><span >*</span><strong>Name of Cardholder:</strong></label>
											<asp:TextBox ID="txtNameOnCard" runat="server"  MaxLength="50"  size="30" ForeColor="Black"  TabIndex="4" onkeydown = "return (event.keyCode!=13);" ></asp:TextBox>
										</li>
										<li class="clear">
											<label for="state"><span >*</span><strong>Card Type:</strong></label>
											<asp:DropDownList runat="server" ID="ddlCCtype"  TabIndex="5">
											<asp:ListItem Value="V" Selected="True" Text="Visa"></asp:ListItem>  
                                            <asp:ListItem Value="M" Text="MasterCard"></asp:ListItem>                                                                              									    		    
										     <asp:ListItem Value="A" Text="AMEX"></asp:ListItem>   
											    <asp:ListItem Value="D" Text="Discover"></asp:ListItem>
											</asp:DropDownList> 
										</li>
										<li class="clear">
											<label for="cardnumber"><span >*</span><strong>Card Number: </strong></label>
											<asp:TextBox ID="txtBxCCNumber" runat="server" size="30" MaxLength="16"  TabIndex="6" onkeydown = "return (event.keyCode!=13);" ></asp:TextBox>
										
										</li>
										<li class="clear">
											<label for="expiredate"><span >*</span><strong>Exp Date:</strong></label>
											<asp:DropDownList ID="ddlExpMonth" runat="server"  TabIndex="7">
                                               <asp:ListItem Value="01">1</asp:ListItem>
                                                        <asp:ListItem Value="02">2</asp:ListItem>
                                                        <asp:ListItem Value="03">3</asp:ListItem>
                                                        <asp:ListItem Value="04">4</asp:ListItem>
                                                        <asp:ListItem Value="05">5</asp:ListItem>
                                                        <asp:ListItem Value="06">6</asp:ListItem>
                                                        <asp:ListItem Value="07">7</asp:ListItem>
                                                        <asp:ListItem Value="08">8</asp:ListItem>
                                                        <asp:ListItem Value="09">9</asp:ListItem>
                                                        <asp:ListItem Value="10">10</asp:ListItem>
                                                        <asp:ListItem Value="11">11</asp:ListItem>
                                                        <asp:ListItem Value="12">12</asp:ListItem>
                                            </asp:DropDownList>
											<asp:DropDownList ID="ddlExpYear" runat="server" TabIndex="8"></asp:DropDownList>
										</li>
                                        <li class="clear">
											<label for="zipcode"><span >*</span><strong>Zip Code: </strong></label>
											<asp:TextBox ID="txtZipCode" runat="server" size="10" MaxLength="10"  TabIndex="9" onkeydown = "return (event.keyCode!=13);" ></asp:TextBox>
                                            <asp:CheckBox ID="ChkUs" runat="server"  TabIndex="9" onkeydown = "return (event.keyCode!=13);" /> Check for International Zip/Postal Code
										
										</li>
										</ol>                                      
                                       <span style = "margin: 0 0 2px 170px; padding: 1px; float: left">
                                           <a class="textlink" href="#" onclick="newWin=window.open('helpBVCCHCTerms.aspx','newWin','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,width=340,height=610,left=100,top=10'); return false;">Terms and Conditions.</a> </span>
										<span style = "margin: 0 0 2px 125px; padding: 8px; float: left">
                                          <strong>This is a final transaction and cannot be cancelled or reversed.</strong>
                                       </span>                                    
                                         
                                        <span style = "margin: 0 0 2px 185px; padding: 1px; float: left"><asp:Button ID="btnCCsubmit" runat="server" Text="" CssClass="tpRenew-Submit" onkeydown = "return (event.keyCode!=13);" /> </span>
                                      
									</fieldset>
                                    
								</div>
                         </asp:Panel>
                    <br />
                    <br />
                         <cc1:ModalPopupExtender runat="server" ID="ModalPopupExtender1"  PopupControlID="pnlOverlay" 
                                BackgroundCssClass="modalBackground"  OkControlID="btnCloseWindow"  OnOkScript="__doPostBack('btnCloseWindow','')"
                                TargetControlID="pnlOverlay">
                        </cc1:ModalPopupExtender>

                        <asp:Panel ID="pnlOverlay" style="display:none;" runat="server" CssClass="modalPopup">
                                    <p> Thank you! Your payment was approved. <b>Your Choice Privileges® points are now available in your Choice Privileges account.</b>. </p>
                                    <p><b>Bluegreen Owner ID:</b>&nbsp;<asp:Label ID="LblOwnerId" runat="server" Text="<%= OwnerArvact %>"></asp:Label></p>
                                    <p><b>Bluegreen Points deducted:</b>&nbsp;<asp:Label ID="LblBvcPoints" runat="server" Text=""></asp:Label></p>
                                    <p> <b>Choice Privileges account number:</b>&nbsp;<asp:Label ID="LblChcId" runat="server" Text=""></asp:Label></p>
                                    <p> <b>Choice Privileges member:</b>&nbsp;<asp:Label ID="LblName" runat="server" Text=""></asp:Label></p>
                                    <p><b>Choice Privileges points deposited:</b>&nbsp; <asp:Label ID="LblChcPoints" runat="server" Text=""></asp:Label></p>
                                    <p><b>Payment Amount:</b>&nbsp;<asp:Label ID="LblPay" runat="server" Text=""></asp:Label> </p>
                                    <p><b>Authorization Number:</b>&nbsp;<asp:Label ID="LblAuthCode" runat="server" Text=""></asp:Label></p>                                  
                                    <p> &nbsp;</p>
                                    
                                    <p align="center" valign="bottom">  <asp:Button ID="btnCloseWindow" runat="server" Text="OK" onclick="btnCloseWindow_Click"  /></p>
                        </asp:Panel>
                                   
                      
                      </div>  
                </ContentTemplate>
                 <Triggers>
                      <asp:AsyncPostBackTrigger ControlID="btnCCsubmit" EventName="Click" />
                      <asp:AsyncPostBackTrigger ControlID="btnCloseWindow" EventName="Click" />
                      <asp:AsyncPostBackTrigger ControlID="txtBvcToChcPts" EventName="TextChanged" />                                       
                 </Triggers>
          </asp:UpdatePanel>
           <asp:UpdatePanel ID="UpdPnlPointsConversionHistory" runat="server" UpdateMode="Conditional">
          <ContentTemplate>
                 <br/>
                <br/> <h3><b> Choice Privilege Points Conversion History</b></h3>                                      
               
                <asp:Panel ID="Panel1" runat="server" Visible="true">
                    <asp:GridView ID="GrdConversion" runat="server" AllowPaging="false" 
                        AllowSorting="false" AutoGenerateColumns="false" CssClass="resortDataw" 
                        Width="547">
                        <HeaderStyle BackColor="#333333" CssClass="resortDataw" ForeColor="white" 
                            HorizontalAlign="center" />
                        <Columns>
                            <asp:BoundField DataField="CreatedDate" dataformatstring="{0:MM/dd/yyyy}" 
                                HeaderText="Transaction Date" ItemStyle-BackColor="#ffffff" 
                                ItemStyle-CssClass="resortData" ItemStyle-Font-Bold="True" 
                                ItemStyle-ForeColor="#666666" ItemStyle-HorizontalAlign="Center" 
                                ReadOnly="True" />
                            <asp:BoundField DataField="ChoiceId" HeaderText="Choice Privileges #" 
                                ItemStyle-BackColor="#ffffff" ItemStyle-CssClass="resortData" 
                                ItemStyle-Font-Bold="True" ItemStyle-ForeColor="#666666" 
                                ItemStyle-HorizontalAlign="Center" ReadOnly="True" />
                            <asp:BoundField DataField="BVCPoints" HeaderText="Bluegreen Vacation Points" 
                                ItemStyle-BackColor="#ffffff" ItemStyle-CssClass="resortData" 
                                ItemStyle-Font-Bold="True" ItemStyle-ForeColor="#666666" 
                                ItemStyle-HorizontalAlign="Center" ReadOnly="True" />
                            <asp:BoundField DataField="ChoicePoints" 
                                HeaderText="Converted &lt;br/&gt; Choice Privileges Points" HtmlEncode="False" 
                                ItemStyle-BackColor="#ffffff" ItemStyle-CssClass="resortData" 
                                ItemStyle-Font-Bold="True" ItemStyle-ForeColor="#666666" 
                                ItemStyle-HorizontalAlign="Center" ReadOnly="True" />
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
                                       
          </ContentTemplate>
         </asp:UpdatePanel>

       
       </div>
    </div>
<br />
<br />
<br />
<br />
    </form>
</body>
</html>

