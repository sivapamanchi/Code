<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AddressUpload.aspx.vb" Inherits="VSSA.AddressUpload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Address Upload</title>
    <link href="../Styles/vssa.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js" ></script>    
</head>

<body>
    <style id="cssStyle" type="text/css" media="all">
        @import url('//maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css');

        .isa_success, .isa_warning, .isa_error {
            margin: 10px 0px;
            padding: 12px;
            border-radius: .5em;
            border: 1px solid;
        }

        .isa_success {
            color: #4F8A10;
            background-color: #DFF2BF;
        }

        .isa_warning {
            color: #9F6000;
            background-color: #FEEFB3;
        }

        .isa_error {
            color: #D8000C;
            background-color: #FFBABA;
        }

            .isa_success i, .isa_warning i, .isa_error i {
                margin: 10px 22px;
                font-size: 1em;
                vertical-align: middle;
            }

        .grdErrorList {
            width: 100%;
            background-color: #fff;
            margin: 5px 0 10px 0;
            border: solid 1px #525252;
            border-collapse: collapse;
        }

            .grdErrorList td {
                padding: 2px;
                border: solid 1px #c1c1c1;
                color: #717171;
            }

            .grdErrorList th {
                padding: 4px 2px;
                color: #fff;
                background: #424242 url(../images/grd_head.png) repeat-x top;
                border-left: solid 1px #525252;
                font-size: 0.9em;
            }

            .grdErrorList .alt {
                background: #fcfcfc url(../images/grd_alt.png) repeat-x top;
            }

            .grdErrorList .pgr {
                background: #424242 url(../images/grd_pgr.png) repeat-x top;
            }

                .grdErrorList .pgr table {
                    margin: 5px 0;
                }

                .grdErrorList .pgr td {
                    border-width: 0;
                    padding: 0 6px;
                    border-left: solid 1px #666;
                    font-weight: bold;
                    color: #fff;
                    line-height: 12px;
                }

                .grdErrorList .pgr a {
                    color: #666;
                    text-decoration: none;
                }

                    .grdErrorList .pgr a:hover {
                        color: #000;
                        text-decoration: none;
                    }

        .fileControlStyle {
            background-color: #abcdef;
            border-radius: 3px 3px 3px 3px;
            z-index: 1;
        }

        * {
            box-sizing: border-box;
            -moz-box-sizing: border-box;
            -webkit-box-sizing: border-box;
        }

        .inputBtnSection {
            display: inline-block;
            vertical-align: top;
            font-size: 0;
            font-family: verdana;
            z-index: 1;
        }

        .disableInputField {
            display: inline-block;
            vertical-align: top;
            height: 27px;
            margin: 0;
            font-size: 14px;
            padding: 0 3px;
            z-index: 1;
        }

        .fileUpload {
            position: relative;
            overflow: hidden;
            border: solid 1px gray;
            display: inline-block;
            vertical-align: top;
            z-index: 1;
        }

        .uploadBtn {
            display: inline-block;
            vertical-align: top;
            background: rgba(0,0,0,0.5); /*For modern Browsers*/
            -ms-filter: "progid:DXImageTransform.Microsoft.gradient(GradientType=1, StartColorStr='#7F000000', EndColorStr='#7F000000')"; /* For IE8 */
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=1, StartColorStr='#7F000000', EndColorStr='#7F000000'); /* For IE6,IE7 */
            zoom: 1 !important; /* Trigger hasLayout */
            font-size: 14px;
            padding: 0 10px;
            height: 25px;
            line-height: 22px;
            color: #fff;
            z-index: 1;
        }

        .fileUpload input.upload {
            position: absolute;
            top: 0;
            right: 0;
            margin: 0;
            padding: 0;
            font-size: 20px;
            cursor: pointer;
            opacity: 0;
            filter: alpha(opacity=0);
            z-index: 1;
        }

        #loading-div-background {
            display: none;
            position: fixed;
            top: 0;
            left: 0;
            background: #fff;
            width: 100%;
            height: 100%;
        }

        #loading-div {
            width: 600px;
            height: 450px;
            background-color: #fff;
            border: none;
            text-align: center;
            color: #202020;
            position: absolute;
            left: 50%;
            top: 50%;
            margin-left: -450px;
            margin-top: -250px;
            -webkit-border-radius: 5px;
            -moz-border-radius: 5px;
            border-radius: 5px;
            z-index: 1000;
        }
    </style>
    <script type="text/javascript">

        function uploadBtn_Click(clickEvent) {
            document.getElementById("uploadFile").value = clickEvent.value;
            ShowProgressAnimation();
            document.getElementById("btnUpload").click();
        }
        $(document).ready(function () {
            $("#loading-div-background").css({ opacity: 1.0 });
        });

        function ShowProgressAnimation() {
            $("#loading-div-background").show();
            $("#excelFileUpload").css("display", "none");
            $(".uploadBtn").css("display", "none");
            $("#divInformation").css("display", "none");
        }
    </script>
    <form id="form1" runat="server">
        <div id="loading-div-background">
            <div id="loading-div" class="ui-corner-all">
                <img style="height: 381px; width: 508px; margin: 30px;" src="../../images/loading40.gif" alt="Loading.." />
            </div>
        </div>
        <div>
            <!-- TOP BAR START -->
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td style="vertical-align: top; width: 214">
                        <img height="30" alt="" src="../images/topBarGreyLinesShadow.gif" width="214" style="border: none" />
                    </td>
                    <td style="vertical-align: top; width: 176">
                        <img height="30" alt="" src="../images/topBrGryLinesShdowBckrnd.gif" width="176" style="border: none" />
                    </td>
                    <td style="vertical-align: top; width: 199">
                        <img height="30" alt="" src="../images/topBrGryDots.gif" width="199" style="border: none" />
                    </td>
                    <td style="vertical-align: top; width: 171">
                        <img height="30" alt="" src="../images/topBrGrnDots.gif" width="168" style="border: none" />
                    </td>
                </tr>
                <tr>
                    <td align="center" valign="top" width="214"></td>
                    <td align="center" valign="top" width="176"></td>
                    <td colspan="2" align="right" valign="top">
                        <asp:LinkButton ID="lbAddressUpload" runat="server" Text="Address Upload" CssClass="text"
                            Font-Underline="true" ForeColor="Blue" />
                        |
                        <asp:LinkButton runat="server" ID="lbNewSearch" Text="New Search" CssClass="text"
                            Font-Underline="true" ForeColor="blue" />
                        |
                        <asp:HyperLink NavigateUrl="http://boss" Font-Underline="true" ForeColor="blue" ID="HyperLink2"
                            runat="server" Text="BOSS" CssClass="text"></asp:HyperLink>
                        
                    </td>
                </tr>
            </table>
            <!-- TOP BAR END -->
            <table class="text" border="0" width="6%" cellpadding="0">
                <tr>
                    <td id="banner">
                        <img alt="" src="../images/oss_hdr.gif" />
                    </td>
                </tr>

                <asp:Panel ID="pnlGeneralMessage" runat="server" Visible="false">
                    <asp:Label runat="server" ID="lblGeneralMessage" CssClass="Font10px"></asp:Label>
                </asp:Panel>
            </table>

            <div>
                <fieldset id="divExcelFileUpload" runat="server" style="width: 730px;">
                    <legend>File Upload</legend>
                    <br />
                    <br />

                    <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClientClick="ShowProgressAnimation()" OnClick="btnUpload_Click" Style="display: none;" />
                    <div class="inputBtnSection">
                        <input id="uploadFile" class="disableInputField" placeholder="Choose File" disabled="disabled" />
                        <label class="fileUpload">
                            <%--<input id="uploadBtn" type="file" class="upload"  onchange="uploadBtn_Click(this)"/>--%>
                            <asp:FileUpload ID="excelFileUpload" CssClass="upload" runat="server" onchange="uploadBtn_Click(this)" />&nbsp;
                            <span class="uploadBtn">Upload / Browse File ..</span>
                        </label>
                    </div>
                    <br />
                    <br />
                    <div runat="server" id="divInformation" visible="false">
                        <i runat="server" id="divList"></i>
                    </div>

                    <br />
                    <asp:GridView ID="grdErrorList" runat="server" OnPageIndexChanging="PageIndexChanging" AllowPaging="true" AutoGenerateColumns="true"
                        CssClass="grdErrorList" GridLines="None" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                        EmptyDataText="No Records were Invalid!!" EmptyDataRowStyle-CssClass="Font10px" BorderStyle="Solid" Visible="false">
                    </asp:GridView>


                </fieldset>
                <p>
                    <asp:Label runat="server" ID="LabelErrorMsg" CssClass="Font10px" ForeColor="Red" Visible="false"></asp:Label>
                </p>

            </div>
            <hr align="left" style="text-align: left; width: 760px;" />
            <div>
                <p><a class="textlink" href="/AddressUpload/ExcelFileTemplate/MassUploadTemplate.xlsx" target="_top">Click here</a> for excel format needed for address upload.</p>
                <p><a class="textlink" href="http://team.bxgcorp.com/Collaboration/VSSAAddressUpload/Lists/History/AllItems.aspx" target="_blank">Click here</a> to view history of updated records.</p>
                <p><a class="textlink" href="http://team.bxgcorp.com/Collaboration/VSSAAddressUpload/VSSA_AuditDocs/Forms/AllItems.aspx" target="_blank">Click here</a> to view last uploaded files.</p>
                <asp:HiddenField runat="server" ID="hdn_FileName" />
            </div>
        </div>
    </form>
</body>
</html>
