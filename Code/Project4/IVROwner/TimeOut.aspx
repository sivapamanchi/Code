<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TimeOut.aspx.vb" Inherits="VSSA.TimeOut" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <link href="../Styles/vssa.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        td {
            padding: 3px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>

        <table style="width: 100%; margin: auto">
            <tr>
                <td style="width: 100%;">
                    <div style="width: 100%; font-size: 30px; text-align: center; padding-bottom: 15px; padding-top: 10px">Bluegreen Owner Info</div>
                </td>
            </tr>
            <tr>
                <td style="width: 100%;">
                    <div style="width: 100%; font-size: 15px;">
                        <asp:Label ID="lblError" runat="server" ForeColor="Red" Text=""></asp:Label>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="width: 100%;">

                    <table style="border: 1px solid; width: 40%; margin-bottom: 20px">
                        <tr>
                            <td>
                                <div class="Font14pxBOLD">
                                    Incoming Call Information
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table style="width: 100%;">
                                    <tr>
                                        <td class="Font12pxBOLD" style="width: 40%">Captured </td>
                                        <td class="Font12pxNormal">
                                            <asp:Label ID="lblCaptured" runat="server" Text="Unavailable"></asp:Label>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td class="Font12pxBOLD">Searched </td>
                                        <td class="Font12pxNormal">
                                            <asp:Label ID="lblSearched" runat="server" Text="Unavailable"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table style="width: 100%;">
                                    <tr>
                                        <td class="Font12pxBOLD" style="width: 40%">Disposition </td>
                                        <td class="Font12pxNormal">
                                            <asp:Label ID="lblDisposition" runat="server" Text=""></asp:Label>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td class="Font12pxBOLD">Transfer Point</td>
                                        <td class="Font12pxNormal">
                                            <asp:Label ID="lblTransferTxt" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <asp:Panel ID="pnlMenu" runat="server">

                            <tr>
                                <td>
                                    <div class="Font14pxBOLD">
                                        Requested Menu Options
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td class="Font12pxNormal">
                                                <asp:Label ID="lblMenuOption" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </asp:Panel>
                    </table>
                </td>
            </tr>

            <tr>
                <td style="width: 100%;">

                    <asp:GridView runat="server" ID="gvSearchResults" AutoGenerateColumns="false" GridLines="None" CellSpacing="1"
                        HeaderStyle-CssClass="gridhdr" AlternatingRowStyle-CssClass="gridrowalt" RowStyle-CssClass="gridrow"
                        EmptyDataText="Could not find any owner with information entered." EmptyDataRowStyle-ForeColor="Red" Width="1200px">
                        <Columns>
                            <asp:TemplateField HeaderText="ARVACT" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="lbARVACT" Text='<%# DataBinder.Eval(Container, "DataItem.ARVACT") %>' CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ARVACT")%>' CommandName="redirect"
                                        CausesValidation="false"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Reg" HeaderText="Reg." HeaderStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="NameFirst" HeaderText="First Name" HeaderStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="NameLast" HeaderText="Last Name" HeaderStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="Email" HeaderText="Email" HeaderStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="PhoneHome" HeaderText="Primary Phone" HeaderStyle-HorizontalAlign="Left"  />
                            <asp:BoundField DataField="Address1" HeaderText="Address" HeaderStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="City" HeaderText="City" HeaderStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="State" HeaderText="State" HeaderStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="PostalCode" HeaderText="ZIP" HeaderStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="Last4SSN" HeaderText="SSN" HeaderStyle-HorizontalAlign="Left" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
