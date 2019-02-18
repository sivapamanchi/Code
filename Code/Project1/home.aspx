<%@ Reference Control="~/includes/ucmenu.ascx" %>
<%@ Register TagPrefix="includeControlFooter" TagName="footer" Src="~/includes/footer.ascx" %>
<%@ Register TagPrefix="includeSpecials" TagName="Specials" Src="~/includes/Specials.ascx" %>
<%@ Register TagPrefix="includeFeatures" TagName="Features" Src="~/includes/Features.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ucMenu" Src="~/includes/ucMenu.ascx" %>
<%@ Register TagPrefix="includeControlOwnerPalette" TagName="OwnerPalette" Src="~/includes/OwnerPalette.ascx" %>
<%@ Register TagPrefix="traveler" TagName="nav" Src="~/includes/TPleftNav.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" Inherits="BGO.BluegreenOnline.HomeTP"
    SmartNavigation="False" Codebehind="Home.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html dir="LTR">
<head>

    <script type="text/javascript">
<!--
        //v1.7
        // Flash Player Version Detection
        // Detect Client Browser type
        // Copyright 2005-2008 Adobe Systems Incorporated.  All rights reserved.
        var isIE = (navigator.appVersion.indexOf("MSIE") != -1) ? true : false;
        var isWin = (navigator.appVersion.toLowerCase().indexOf("win") != -1) ? true : false;
        var isOpera = (navigator.userAgent.indexOf("Opera") != -1) ? true : false;
        function ControlVersion() {
            var version;
            var axo;
            var e;
            // NOTE : new ActiveXObject(strFoo) throws an exception if strFoo isn't in the registry
            try {
                // version will be set for 7.X or greater players
                axo = new ActiveXObject("ShockwaveFlash.ShockwaveFlash.7");
                version = axo.GetVariable("$version");
            } catch (e) {
            }
            if (!version) {
                try {
                    // version will be set for 6.X players only
                    axo = new ActiveXObject("ShockwaveFlash.ShockwaveFlash.6");

                    // installed player is some revision of 6.0
                    // GetVariable("$version") crashes for versions 6.0.22 through 6.0.29,
                    // so we have to be careful. 

                    // default to the first public version
                    version = "WIN 6,0,21,0";
                    // throws if AllowScripAccess does not exist (introduced in 6.0r47)		
                    axo.AllowScriptAccess = "always";
                    // safe to call for 6.0r47 or greater
                    version = axo.GetVariable("$version");
                } catch (e) {
                }
            }
            if (!version) {
                try {
                    // version will be set for 4.X or 5.X player
                    axo = new ActiveXObject("ShockwaveFlash.ShockwaveFlash.3");
                    version = axo.GetVariable("$version");
                } catch (e) {
                }
            }
            if (!version) {
                try {
                    // version will be set for 3.X player
                    axo = new ActiveXObject("ShockwaveFlash.ShockwaveFlash.3");
                    version = "WIN 3,0,18,0";
                } catch (e) {
                }
            }
            if (!version) {
                try {
                    // version will be set for 2.X player
                    axo = new ActiveXObject("ShockwaveFlash.ShockwaveFlash");
                    version = "WIN 2,0,0,11";
                } catch (e) {
                    version = -1;
                }
            }

            return version;
        }
        // JavaScript helper required to detect Flash Player PlugIn version information
        function GetSwfVer() {
            // NS/Opera version >= 3 check for Flash plugin in plugin array
            var flashVer = -1;

            if (navigator.plugins != null && navigator.plugins.length > 0) {
                if (navigator.plugins["Shockwave Flash 2.0"] || navigator.plugins["Shockwave Flash"]) {
                    var swVer2 = navigator.plugins["Shockwave Flash 2.0"] ? " 2.0" : "";
                    var flashDescription = navigator.plugins["Shockwave Flash" + swVer2].description;
                    var descArray = flashDescription.split(" ");
                    var tempArrayMajor = descArray[2].split(".");
                    var versionMajor = tempArrayMajor[0];
                    var versionMinor = tempArrayMajor[1];
                    var versionRevision = descArray[3];
                    if (versionRevision == "") {
                        versionRevision = descArray[4];
                    }
                    if (versionRevision[0] == "d") {
                        versionRevision = versionRevision.substring(1);
                    } else if (versionRevision[0] == "r") {
                        versionRevision = versionRevision.substring(1);
                        if (versionRevision.indexOf("d") > 0) {
                            versionRevision = versionRevision.substring(0, versionRevision.indexOf("d"));
                        }
                    }
                    var flashVer = versionMajor + "." + versionMinor + "." + versionRevision;
                }
            }
            // MSN/WebTV 2.6 supports Flash 4
            else if (navigator.userAgent.toLowerCase().indexOf("webtv/2.6") != -1) flashVer = 4;
            // WebTV 2.5 supports Flash 3
            else if (navigator.userAgent.toLowerCase().indexOf("webtv/2.5") != -1) flashVer = 3;
            // older WebTV supports Flash 2
            else if (navigator.userAgent.toLowerCase().indexOf("webtv") != -1) flashVer = 2;
            else if (isIE && isWin && !isOpera) {
                flashVer = ControlVersion();
            }
            return flashVer;
        }
        // When called with reqMajorVer, reqMinorVer, reqRevision returns true if that version or greater is available
        function DetectFlashVer(reqMajorVer, reqMinorVer, reqRevision) {
            versionStr = GetSwfVer();
            if (versionStr == -1) {
                return false;
            } else if (versionStr != 0) {
                if (isIE && isWin && !isOpera) {
                    // Given "WIN 2,0,0,11"
                    tempArray = versionStr.split(" "); 	// ["WIN", "2,0,0,11"]
                    tempString = tempArray[1]; 		// "2,0,0,11"
                    versionArray = tempString.split(","); // ['2', '0', '0', '11']
                } else {
                    versionArray = versionStr.split(".");
                }
                var versionMajor = versionArray[0];
                var versionMinor = versionArray[1];
                var versionRevision = versionArray[2];
                // is the major.revision >= requested major.revision AND the minor version >= requested minor
                if (versionMajor > parseFloat(reqMajorVer)) {
                    return true;
                } else if (versionMajor == parseFloat(reqMajorVer)) {
                    if (versionMinor > parseFloat(reqMinorVer))
                        return true;
                    else if (versionMinor == parseFloat(reqMinorVer)) {
                        if (versionRevision >= parseFloat(reqRevision))
                            return true;
                    }
                }
                return false;
            }
        }
        function AC_AddExtension(src, ext) {
            if (src.indexOf('?') != -1)
                return src.replace(/\?/, ext + '?');
            else
                return src + ext;
        }
        function AC_Generateobj(objAttrs, params, embedAttrs) {
            var str = '';
            if (isIE && isWin && !isOpera) {
                str += '<object ';
                for (var i in objAttrs) {
                    str += i + '="' + objAttrs[i] + '" ';
                }
                str += '>';
                for (var i in params) {
                    str += '<param name="' + i + '" value="' + params[i] + '" /> ';
                }
                str += '</object>';
            }
            else {
                str += '<embed ';
                for (var i in embedAttrs) {
                    str += i + '="' + embedAttrs[i] + '" ';
                }
                str += '> </embed>';
            }
            document.write(str);
        }
        function AC_FL_RunContent() {
            var ret =
    AC_GetArgs
    (arguments, ".swf", "movie", "clsid:d27cdb6e-ae6d-11cf-96b8-444553540000"
     , "application/x-shockwave-flash"
    );
            AC_Generateobj(ret.objAttrs, ret.params, ret.embedAttrs);
        }
        function AC_SW_RunContent() {
            var ret =
    AC_GetArgs
    (arguments, ".dcr", "src", "clsid:166B1BCA-3F9C-11CF-8075-444553540000"
     , null
    );
            AC_Generateobj(ret.objAttrs, ret.params, ret.embedAttrs);
        }
        function AC_GetArgs(args, ext, srcParamName, classid, mimeType) {
            var ret = new Object();
            ret.embedAttrs = new Object();
            ret.params = new Object();
            ret.objAttrs = new Object();
            for (var i = 0; i < args.length; i = i + 2) {
                var currArg = args[i].toLowerCase();
                switch (currArg) {
                    case "classid":
                        break;
                    case "pluginspage":
                        ret.embedAttrs[args[i]] = args[i + 1];
                        break;
                    case "src":
                    case "movie":
                        args[i + 1] = AC_AddExtension(args[i + 1], ext);
                        ret.embedAttrs["src"] = args[i + 1];
                        ret.params[srcParamName] = args[i + 1];
                        break;
                    case "onafterupdate":
                    case "onbeforeupdate":
                    case "onblur":
                    case "oncellchange":
                    case "onclick":
                    case "ondblclick":
                    case "ondrag":
                    case "ondragend":
                    case "ondragenter":
                    case "ondragleave":
                    case "ondragover":
                    case "ondrop":
                    case "onfinish":
                    case "onfocus":
                    case "onhelp":
                    case "onmousedown":
                    case "onmouseup":
                    case "onmouseover":
                    case "onmousemove":
                    case "onmouseout":
                    case "onkeypress":
                    case "onkeydown":
                    case "onkeyup":
                    case "onload":
                    case "onlosecapture":
                    case "onpropertychange":
                    case "onreadystatechange":
                    case "onrowsdelete":
                    case "onrowenter":
                    case "onrowexit":
                    case "onrowsinserted":
                    case "onstart":
                    case "onscroll":
                    case "onbeforeeditfocus":
                    case "onactivate":
                    case "onbeforedeactivate":
                    case "ondeactivate":
                    case "type":
                    case "codebase":
                    case "id":
                        ret.objAttrs[args[i]] = args[i + 1];
                        break;
                    case "width":
                    case "height":
                    case "align":
                    case "vspace":
                    case "hspace":
                    case "class":
                    case "title":
                    case "accesskey":
                    case "name":
                    case "tabindex":
                        ret.embedAttrs[args[i]] = ret.objAttrs[args[i]] = args[i + 1];
                        break;
                    default:
                        ret.embedAttrs[args[i]] = ret.params[args[i]] = args[i + 1];
                }
            }
            ret.objAttrs["classid"] = classid;
            if (mimeType) ret.embedAttrs["type"] = mimeType;
            return ret;
        }
// -->
    </script>

    <script type="text/javascript">

        months = new Array("January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December")
        days = new Array(31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31)

        var thisDate = new Date()
        var thisMonth = thisDate.getMonth()
        var thisDay = thisDate.getDate()
        var thisYear = thisDate.getFullYear()

        // Global variables Hotel Search
        var todayDate = new Date();
        var HotelthisMonth = todayDate.getMonth() + 1;
        var HotelthisYear = todayDate.getFullYear();
        var HotelthisDay = todayDate.getDate();

        var writeMonth = HotelthisMonth;
        var writeYear = HotelthisYear;

        var departing_date_selected = false;

    </script>

    <title>Bluegreen Traveler Plus Home</title>
  <link rel="stylesheet" type="text/css" href="/css/owner/owner.css" />
<link rel="stylesheet" type="text/css" href="/css/ucmenu.css" />
<link rel="stylesheet" type="text/css" href="/css/style.css" />


    <script type="text/javascript" src="/scripts/rollover.js"></script>

    <script type="text/javascript" src="/js/otDivCommon.js"></script>

    <script type="text/javascript" src="/js/otMenu.js"></script>

    <!--- Start JavaScript used for Air Quick Search --->

    <script type="text/javascript">
			<!--
        var return_month_index = -1
        var return_day_index = -1
        var return_time_index = -1
        var checkstr = ""
        var CountArrange = 0
        var is_parent_prof = 1
        var indexAirlines = 0
        var numero
        var select_Airlines_text = "Select an airline."
        var no_preference_text = "No preference"

        function onTravelingToResortClicked() {
            var isTravelingToResort = document.AIR_ENTRY_FORM.travelingToResort.checked;
            if (isTravelingToResort == true)
            //If the customer is traveling to a resort then private fares should be shown
            {
                document.AIR_ENTRY_FORM.SO_GL.value = '<?xml version="1.0" encoding="iso-8859-1"?><SO_GL><GLOBAL_LIST mode="complete"><NAME>SO_SINGLE_MULTIPLE_COMMAND_BUILDER</NAME><LIST_ELEMENT><CODE>1</CODE><LIST_VALUE>AIAN7201004000</LIST_VALUE><LIST_VALUE>S</LIST_VALUE></LIST_ELEMENT></GLOBAL_LIST></SO_GL>'
            }
        }

        function GoToAuxWin(n, page) {
            numero = n
            i = page.indexOf("/")
            if (i >= 0) {
                if (page.substring(0, i) == "LangDir") {
                    whole_url = document.URL
                    page = whole_url.substring(0, whole_url.lastIndexOf("/")) + page.substring(i, page.length)
                }
            } else
                page = "/pl/" + page
            listin = open(page, "", "toolbar=no,location=no,directories=no,status=yes,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no,width=320,height=300")
            if (navigator.appName == "Netscape")
                listin.focus()
        }

        function CheckFields(name) {
            for (n = 0; n < document.forms[1].elements.length; n++) {
                if (document.forms[1].elements[n].name == name)
                    indexAirlines = n
            }
        }

        function Lineas() {
            var myelements = document.forms[1].elements
            if (myelements[indexAirlines].value == "00")
                myelements[indexAirlines].value = ""
            if (myelements[indexAirlines + 1].value == "00")
                myelements[indexAirlines + 1].value = ""
            if (myelements[indexAirlines + 2].value == "00")
                myelements[indexAirlines + 2].value = ""
        }

        function ErrorWindow(cadena) {
            var parameter = cadena
            newWindow = window.open("error.aspx", "newWin", "toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=no,resizable=no,width=220,height=400,left=300,top=100")
            if (navigator.appName == "Netscape")
                newWindow.focus()
        }

        function ReturnTrip() {

            var myform = document.forms[0]
            myform.E_MONTH.selectedIndex = myform.B_MONTH.selectedIndex
            myform.E_DAY.selectedIndex = myform.B_DAY.selectedIndex

            myform.E_MONTH.disabled = false
            myform.E_DAY.disabled = false
            myform.E_TIME_TO_PROCESS.disabled = false

            if (myform.B_TIME_TO_PROCESS.value != "ANY") {
                if ((myform.B_TIME_TO_PROCESS.selectedIndex + 1) < myform.E_TIME_TO_PROCESS.length) {
                    myform.E_TIME_TO_PROCESS.selectedIndex = myform.B_TIME_TO_PROCESS.selectedIndex + 1
                }
                else {
                    myform.E_TIME_TO_PROCESS.selectedIndex = 4
                    var selMonth = myform.E_MONTH.value.substr(myform.E_MONTH.value.length - 2) - 1
                    var monthLength = days[selMonth]


                    if (((myform.E_DAY.value * 1) + 1) <= monthLength) {
                        myform.E_DAY.selectedIndex = myform.E_DAY_selectedIndex + 1
                    }
                    else {
                        myform.E_DAY.selectedIndex = 0
                        if ((myform.E_MONTH.selectedIndex + 1) < myform.E_MONTH.length) {
                            myform.E_MONTH.selectedIndex = myform.E_MONTH.selectedIndex + 1
                            ChangeArrivingInfo(selMonth)
                            myform.E_DAY.selectedIndex = 0
                        }
                        else {
                            checkstr += "<li> Your return date will be out of the allowed range - please adjust your departure time.</li>"
                            ErrorWindow(checkstr)
                            return false
                        }
                    }
                }
            }
            else {
                myform.E_TIME_TO_PROCESS.selectedIndex = 0
            }



        }

        function OnewayTrip() {
            var myform = document.forms[0]
            myform.E_MONTH.selectIndex = myform.B_MONTH.selectedIndex
            myform.E_DAY.selectIndex = myform.B_DAY.selectedIndex
            myform.E_TIME_TO_PROCESS.selectedIndex = 0
            myform.E_MONTH.disabled = true
            myform.E_DAY.disabled = true
            myform.E_TIME_TO_PROCESS.disabled = true



        }

        function Check() {
            checkstr = ""
            var myform = document.AIR_ENTRY_FORM

            myform.B_ANY_TIME_1.value = "FALSE"
            myform.B_ANY_TIME_2.value = "FALSE"

            var dYearMonthIndex = myform.B_MONTH.selectedIndex
            var dDayIndex = myform.B_DAY.selectedIndex
            var dYearMonthValue = myform.B_MONTH.options[dYearMonthIndex].value
            var dDayValue = myform.B_DAY.options[dDayIndex].value
            // <fif session.ALLOW_DEPARTURE_TIME>
            var dTimeIndex = myform.B_TIME_TO_PROCESS.selectedIndex
            var dTimeValue = myform.B_TIME_TO_PROCESS.options[dTimeIndex].value
            var dTimeText = myform.B_TIME_TO_PROCESS.options[dTimeIndex].text

            myform.B_ANY_TIME_1.value = "false"
            myform.B_ANY_TIME_2.value = "false"

            var nowThen = ""
            var rnowThen = ""
            var dnowThen = ""
            var departDate = ""
            var returnDate = ""
            var tDate = new Date()
            var tMonth = thisMonth
            var tDay = thisDay
            var tYear = thisYear
            var monthLength = days[tMonth]
            var tHour = tDate.getHours()
            var tMinute = tDate.getMinutes()

            if (tDay + 1 > monthLength) {
                tDate.setDate((tDay + 1) - monthLength)
                if (tMonth < 11) {
                    tDate.setMonth(tMonth + 1)
                }
                else {
                    tDate.setMonth(0)
                    tDate.setFullYear(tYear + 1)
                }
            }
            else {
                tDate.setDate(tDay + 1)
            }
            if (tDate.getMonth() > 8) {
                if (tDate.getDate() < 10) {
                    nowThen = tDate.getFullYear() + "" + (tDate.getMonth() + 1) + "0" + tDate.getDate()
                }
                else {
                    nowThen = tDate.getFullYear() + "" + (tDate.getMonth() + 1) + tDate.getDate()
                }
            }
            else {
                if (tDate.getDate() < 10) {
                    nowThen = tDate.getFullYear() + "0" + (tDate.getMonth() + 1) + "0" + tDate.getDate()
                }
                else {
                    nowThen = tDate.getFullYear() + "0" + (tDate.getMonth() + 1) + tDate.getDate()
                }
            }
            departDate = dYearMonthValue + "" + dDayValue

            if (dTimeValue == "ANY")
            //if(dTimeValue == "0000")
            {
                if (departDate == nowThen) {
                    if (tMinute < 55) {

                        if (tHour < 9) {
                            dTimeValue = "0" + (tHour + 1) + "00"
                        }
                        else {
                            dTimeValue = "" + (tHour + 1) + "00"
                        }

                    }
                    else {
                        if (tHour < 8) {
                            dTimeValue = "0" + (tHour + 2) + "00"
                        }
                        else {
                            dTimeValue = "" + (tHour + 2) + "00"
                        }
                    }
                }
                else {
                    dTimeValue = "0000"
                    myform.B_ANY_TIME_1.value = "true"
                }
            }

            if (dTimeValue == "MORNING") {
                //myform.B_TIME_WINDOW_1.value = "MORNING"
                myform.B_TIME_WINDOW_1.value = 6
                dTimeValue = "0600"
            }

            if (dTimeValue == "AFTERNOON") {
                //myform.B_TIME_WINDOW_1.value = "AFTERNOON"
                myform.B_TIME_WINDOW_1.value = 3
                dTimeValue = "1500"
            }

            if (dTimeValue == "EVENING") {
                //myform.B_TIME_WINDOW_1.value = "EVENING"
                myform.B_TIME_WINDOW_1.value = 3
                dTimeValue = "2100"
            }

            // <felse>
            // var dTimeValue = myform.B_TIME.value
            // </fif>
            myform.B_DATE_1.value = dYearMonthValue + dDayValue + dTimeValue
            // <fif session.allow_return>
            var rYearMonthIndex = myform.E_MONTH.selectedIndex
            var rDayIndex = myform.E_DAY.selectedIndex
            // <fif session.ALLOW_DEPARTURE_TIME>
            var rTimeIndex = myform.E_TIME_TO_PROCESS.selectedIndex
            var rTimeValue = myform.E_TIME_TO_PROCESS.options[rTimeIndex].value
            var rTimeText = myform.E_TIME_TO_PROCESS.options[rTimeIndex].text

            var rYearMonthValue = myform.E_MONTH.options[rYearMonthIndex].value
            var rDayValue = myform.E_DAY.options[rDayIndex].value

            returnDate = rYearMonthValue + "" + rDayValue

            if (rTimeValue == "ANY")
            //if(rTimeValue == "0000")
            {
                if (returnDate == nowThen) {
                    if (tMinute < 55) {

                        if (tHour < 8) {
                            rTimeValue = "0" + (tHour + 2) + "00"
                        }
                        else {
                            rTimeValue = "" + (tHour + 2) + "00"
                        }

                    }
                    else {
                        if (tHour < 7) {
                            rTimeValue = "0" + (tHour + 3) + "00"
                        }
                        else {
                            rTimeValue = "" + (tHour + 3) + "00"
                        }
                    }
                }
                else {
                    myform.B_ANY_TIME_2.value = "true"
                    rTimeValue = "0000"
                }
            }

            if (rTimeValue == "MORNING") {
                //myform.B_TIME_WINDOW_2.value = "MORNING"
                myform.B_TIME_WINDOW_2.value = 6
                rTimeValue = "0600"
            }

            if (rTimeValue == "AFTERNOON") {
                //myform.B_TIME_WINDOW_2.value = "AFTERNOON"
                myform.B_TIME_WINDOW_2.value = 3
                rTimeValue = "1500"
            }

            if (rTimeValue == "EVENING") {
                //myform.B_TIME_WINDOW_2.value = "EVENING"
                myform.B_TIME_WINDOW_2.value = 3
                rTimeValue = "2100"
            }

            // </fif>
            myform.B_DATE_2.value = (rYearMonthValue == "XX" ? "" : rYearMonthValue) + (rDayValue == "XX" ? "" : rDayValue) + (rTimeValue == "XX" ? "" : rTimeValue)


            //check to see that they did not select an earlier month or date within the same month
            if ((rYearMonthValue < dYearMonthValue) || ((rDayValue < dDayValue) && (rYearMonthValue == dYearMonthValue))) {
                checkstr += "<li> Return date must be later than departure date.  Please check your return selection."
            }

            //now let's check to make sure that the user has selected a departure date that is at least 24 hours from one hour
            //from the current time...if we don't capture this and handle here it will get to their generic search page...and who
            //wants that		

            if (myform.B_ANY_TIME_1.value == "true") {
                if (tMinute < 55) {

                    if (tHour < 9) {
                        dnowThen = nowThen + "0" + (tHour + 1) + "00"
                    }
                    else {
                        dnowThen = nowThen + "" + (tHour + 1) + "00"
                    }

                }
                else {
                    if (tHour < 8) {
                        dnowThen = nowThen + "0" + (tHour + 2) + "00"
                    }
                    else {
                        dnowThen = nowThen + "" + (tHour + 2) + "00"
                    }
                }
            }
            else {
                dnowThen = nowThen + "0000"
            }
            if (myform.B_ANY_TIME_2.value == "true") {
                if (tMinute < 55) {

                    if (tHour < 8) {
                        rnowThen = nowThen + "0" + (tHour + 2) + "00"
                    }
                    else {
                        rnowThen = nowThen + "" + (tHour + 2) + "00"
                    }
                }
                else {
                    if (tHour < 7) {
                        rnowThen = nowThen + "0" + (tHour + 3) + "00"
                    }
                    else {
                        rnowThen = nowThen + "" + (tHour + 3) + "00"
                    }
                }
            }
            else {
                rnowThen = nowThen + "0000"
            }

            if (myform.B_DATE_1.value < nowThen)
                checkstr += "<li> Departure date must be at least twenty-four (24) hours from now - please check your departure time selection."
            if (myform.B_DATE_2.value < rnowThen)
                checkstr += "<li> Return date should be at least twenty-four (24) hours from now - please check your return date/time selection"
            if (myform.B_DATE_2.value <= myform.B_DATE_1.value)
                checkstr += "<li> Return date should be later than departure date - please check your return date/time selection"

            // <fif session.allow_oneway>
            // <fif session.COMPLEX_ITINERARY AND session.COMPLEX_ITIN_TYPE EQ "STEP-BY-STEP">
            // if(myform.TRIP_TYPE[2].checked)
            // <felse>
            //				if(myform.TRIP_TYPE.value == "O")
            if (myform.TRIP_TYPE[0].checked == true)
            // </fif>
            // </fif>
            {
                if ((rYearMonthValue == dYearMonthValue) && (rDayValue == dDayValue) && ((dTimeValue == "ANY") || (rTimeValue == "ANY")))
                    checkstr += "<li> When departure and return flights are requested for the same day, you must define specific flight times.</li>"
                else {
                    if ((rYearMonthValue == dYearMonthValue && rDayValue == dDayValue && ((rTimeValue < dTimeValue && dTimeValue < 24) || rTimeValue == 24)) || ((rYearMonthValue == dYearMonthValue) && (rDayValue < dDayValue)) || rYearMonthValue < dYearMonthValue)
                        checkstr += "<li> Return date should be later than departure date.</li>"
                }
            }
            // <fif session.allow_oneway>
            // if(myform.TRIP_TYPE.value == "O"){
            // </fif>
            rYear = rYearMonthValue.substring(0, 4)
            rMonth = rYearMonthValue.substring(4, 6)
            checkstr += CheckDates(rMonth, rDayValue, rYear, 1)
            // <fif session.allow_oneway>
            // }
            // </fif>
            // </fif>

            //				if(myform.TRIP_TYPE.value == "R")
            if (myform.TRIP_TYPE[1].checked == true) {
                if (myform.E_DAY.value == "XX") {
                    checkstr += "<li> Please provide a day for the return trip</li>"
                }
                if (myform.E_MONTH.value == "XX") {
                    checkstr += "<li> Please provide a month for the return trip</li>"
                }
                if (myform.E_TIME_TO_PROCESS.value == "XX") {
                    checkstr += "<li> Please provide a time for the return trip</li>"
                }
            }

            myform.B_LOCATION_2.value = myform.E_LOCATION_1.value
            myform.E_LOCATION_2.value = myform.B_LOCATION_1.value

            dYear = dYearMonthValue.substring(0, 4)
            dMonth = dYearMonthValue.substring(4, 6)
            checkstr += CheckDates(dMonth, dDayValue, dYear, 0)
            if (myform.B_LOCATION_1.type == "text") {
                if (myform.B_LOCATION_1.value == "")
                    checkstr += "<li> Please choose a departure city</li>"
            }
            else {
                if (myform.B_LOCATION_1.options[myform.B_LOCATION_1.selectedIndex].text == "ITYLIST_DESC")
                    checkstr += "<li> Please choose a departure city</li>"
            }
            if (myform.E_LOCATION_1.type == "text") {
                if (myform.E_LOCATION_1.value == "")
                    checkstr += "<li> Please choose an arrival city.</li>"
            }
            else {
                if (myform.E_LOCATION_1.options[myform.E_LOCATION_1.selectedIndex].text == "ITYLIST_DESC")
                    checkstr += "<li> Please choose an arrival city.</li>"
            }

            if (myform.NAME_FIRST.value == "")
                checkstr += "<li> Traveler's first name is required.</li>"

            if (myform.NAME_LAST.value == "")
                checkstr += "<li> Traveler's last name is required</li>"

            //				if(myform.EMAIL.value == "")
            if (myform.CONTACT_POINT_EMAIL_1.value == "")
                checkstr += "<li> Traveler's email is required.</li>"

            //				if(myform.PHONEH.value == "")
            if (myform.CONTACT_POINT_HOME_PHONE.value == "")
                checkstr += "<li> Traveler's phone is required.</li>"

            if (checkstr != "") {
                ErrorWindow(checkstr)
                return false
            }
            return myform.submit();
        }

        function CheckDates(month, day, year, returning) {
            var year = parseInt(year)
            var leap_year = (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0)
            if (!leap_year && month == "02" && (day == "30" || day == "29")) {
                if (returning == 0)
                    return "<li> Departure date is not valid.."
                else
                    return "<li> Return date is not valid.."
            }
            var comp1 = (month == "02" || month == "04" || month == "06" || month == "09" || month == "11")
            var comp2 = (day == "31")
            if (comp1 && comp2) {
                if (returning == 0)
                    return "<li> Departure date is not valid.."
                else
                    return "<li> Return date is not valid.."
            } else
                return ""
        }

        function search() {
            var thisForm = document.forms(0)
            thisForm.EMBEDDED_TRANSACTION.value = "TravelShopperAvailability"
            if (thisForm.TRIP_TYPE[0].checked == true) {
                thisForm.TRIP_TYPE.value = "O"
            }
            else {
                thisForm.TRIP_TYPE.value = "R"
            }
            thisForm.action = ""
            //					thisForm.action = "https://wftc1.e-travel.com/plnext/AAZA/Override.action"

            thisForm.action = "https://wftc1.e-travel.com/plnext/AIEACYLACYL/Override.action"

            if (Check())
                thisForm.submit()
        }

        function ComplexItineraryLink() {
            var myform = document.AIR_ENTRY_FORM;

            //the following code will be bypassed because it is a near
            //duplicate of check() and is unnecessary...this will be removed
            //once it has had a complete shakedown
            if (0 == 1) {

                var dYearMonthIndex = myform.B_MONTH.selectedIndex
                var dDayIndex = myform.B_DAY.selectedIndex
                var dYearMonthValue = myform.B_MONTH.options[dYearMonthIndex].value
                var dDayValue = myform.B_DAY.options[dDayIndex].value
                var dTimeIndex = myform.B_TIME_TO_PROCESS.selectedIndex
                var dTimeValue = myform.B_TIME_TO_PROCESS.options[dTimeIndex].value
                var dTimeText = myform.B_TIME_TO_PROCESS.options[dTimeIndex].text

                if (dTimeValue == "ANY")
                //if(dTimeValue == "0000")
                {
                    myform.B_ANY_TIME_1.value = "TRUE"
                    dTimeValue = "0000"
                }

                if (dTimeValue == "MORNING") {
                    myform.B_TIME_WINDOW_1.value = "MORNING"
                    //myform.B_TIME_WINDOW_1.value = 6
                    dTimeValue = "0600"
                }

                if (dTimeValue == "AFTERNOON") {
                    myform.B_TIME_WINDOW_1.value = "AFTERNOON"
                    //myform.B_TIME_WINDOW_1.value = 3
                    dTimeValue = "1500"
                }

                if (dTimeValue == "EVENING") {
                    myform.B_TIME_WINDOW_1.value = "EVENING"
                    //myform.B_TIME_WINDOW_1.value = 3
                    dTimeValue = "2100"
                }

                myform.B_DATE_1.value = dYearMonthValue + dDayValue + dTimeValue

                var rYearMonthIndex = myform.E_MONTH.selectedIndex
                var rDayIndex = myform.E_DAY.selectedIndex
                var rTimeIndex = myform.E_TIME_TO_PROCESS.selectedIndex
                var rTimeValue = myform.E_TIME_TO_PROCESS.options[rTimeIndex].value
                var rTimeText = myform.E_TIME_TO_PROCESS.options[rTimeIndex].text

                if (rTimeValue == "ANY")
                //if(rTimeValue == "0000")
                {
                    myform.B_ANY_TIME_2.value = "TRUE"
                    rTimeValue = "0000"
                }

                if (rTimeValue == "MORNING") {
                    myform.B_TIME_WINDOW_2.value = "MORNING"
                    //myform.B_TIME_WINDOW_2.value = 6
                    rTimeValue = "0600"
                }

                if (rTimeValue == "AFTERNOON") {
                    myform.B_TIME_WINDOW_2.value = "AFTERNOON"
                    //myform.B_TIME_WINDOW_2.value = 3
                    rTimeValue = "1500"
                }

                if (rTimeValue == "EVENING") {
                    myform.B_TIME_WINDOW_2.value = "EVENING"
                    //myform.B_TIME_WINDOW_2.value = 3
                    rTimeValue = "2100"
                }

                var rYearMonthValue = myform.E_MONTH.options[rYearMonthIndex].value
                var rDayValue = myform.E_DAY.options[rDayIndex].value
                myform.B_DATE_2.value = (rYearMonthValue == "XX" ? "" : rYearMonthValue) + (rDayValue == "XX" ? "" : rDayValue) + (rTimeValue == "XX" ? "" : rTimeValue)

            }
            else {

                myform.B_LOCATION_2.value = myform.E_LOCATION_1.value
                myform.E_LOCATION_2.value = myform.B_LOCATION_1.value

                //myform.TRIP_TYPE.value = "C"
                myform.TRIP_TYPE.value = "M"
                myform.EMBEDDED_TRANSACTION.value = "TravelShopperSearch"
                myform.action = ""
                //			myform.action = "https://wftc1.e-travel.com/plnext/AAZA/Override.action?EMBEDDED_TRANSACTION=TravelShopperSearch&TRIP_TYPE=M"

                // ---------------------------
                // This the address for the acceptance site (DEV and STAGE)for Bluegreen Corp.
                // ---------------------------
                //myform.action = "http://siteacceptance.wftc2.e-travel.com/plnext/AIEACYLACYL/CleanUpSessionPui.action?SITE=ACYLACYL&LANGUAGE=US"

                // ---------------------------
                // This the address for the Production site for Bluegreen Corp.
                // ---------------------------
                myform.action = "http://wftc1.e-travel.com/plnext/AIEACYLACYL/CleanUpSessionPui.action?SITE=ACYLACYL&LANGUAGE=US"

                myform.submit()

            }

        }

        function setETime() {
            var myform = document.forms[0]
            var selIndex = myform.B_TIME_TO_PROCESS.selectedIndex
            var selText = myform.B_TIME_TO_PROCESS.options[selIndex].text

            if (selText == "Any Time") {
                myform.E_TIME_TO_PROCESS.selectedIndex = 0
            }
            else {
                if ((selIndex + 1) < myform.B_TIME_TO_PROCESS.length) {
                    myform.E_TIME_TO_PROCESS.selectedIndex = selIndex + 1
                }
                else {
                    myform.E_TIME_TO_PROCESS.selectedIndex = 4
                    var selMonth = myform.E_MONTH.value.substr(myform.E_MONTH.value.length - 2) - 1
                    var monthLength = days[selMonth]


                    if (((myform.E_DAY.value * 1) + 1) <= monthLength) {
                        myform.E_DAY.selectedIndex = myform.E_DAY_selectedIndex + 1
                    }
                    else {
                        myform.E_DAY.selectedIndex = 0
                        if ((myform.E_MONTH.selectedIndex + 1) < myform.E_MONTH.length) {
                            myform.E_MONTH.selectedIndex = myform.E_MONTH.selectedIndex + 1
                            ChangeArrivingInfo(selMonth)
                            myform.E_DAY.selectedIndex = 0
                        }
                        else {
                            checkstr += "<li> Your return date will be out of the allowed range - please adjust your departure time.</li>"
                            ErrorWindow(checkstr)
                            return false
                        }
                    }
                }
            }
        }


        function DisplayArrangeBy() {
            var ARRANGE_BY_TYPE = ""
            CountArrange = CountArrange + 1
            ARRANGE_BY_TYPE = 2
            if (CountArrange > 1) {
                document.write('<tr class="formBody">')
                document.write('<td class="formBody" colspan="2">')
                document.write('<table border="0" width="100%" cellspacing="0" cellpadding="0" id="000597">')
                document.write('<tr><td bgcolor="#666666"><img src="../images/gif.gif" width="1" height="1"></td></tr>')
                document.write('</table></td></tr>')
                document.write('<tr class="formBody">')
                document.write('<td align="right" class="formBody"><strong>Sort flights by:</strong></td>')
                document.write('<td>')
                document.write('<select name="ARRANGE_BY">')
                document.write('<option selected value="2">')
                document.write("Departure Time")
                document.write('</option>')
                document.write('</select></td></tr>')
            }
            else
                document.write('<input type="hidden" name="ARRANGE_BY" value="' + ARRANGE_BY_TYPE + '">')

        }

			//-->
    </script>

    <!--- End JavaScript used for Air Quick Search --->
    <!--- Start JavaScript used for Car Rental Quick Search --->

    <script type="text/javascript">
			<!--
        var is_parent_prof = 0
        var numero
        var checkstr = ""
        var index = 0
        var select_Car_text = "Select a car company."
        var no_preference_text = "No preference"

        function CheckFields1() {
            for (n = 0; n < document.CAR_ENTRY_FORM.elements.length; n++)
                if (document.CAR_ENTRY_FORM.elements[n].name == "Company1")
                    index = n
            }

            function lineas1() {
                var myelems = document.CAR_ENTRY_FORM.elements

                if (myelems[index + 1 - 1].value == "00")
                    myelems[index + 1 - 1].value = ""

                if (myelems[index + 2 - 1].value == "00")
                    myelems[index + 2 - 1].value = ""

                if (myelems[index + 3 - 1].value == "00")
                    myelems[index + 3 - 1].value = ""

            }

            function GoToAuxWin1(page, number) {
                numero = number
                i = page.indexOf("/")
                if (i >= 0) {
                    if (page.substring(0, i) == "LangDir") {
                        whole_url = document.URL
                        page = whole_url.substring(0, whole_url.lastIndexOf("/")) + page.substring(i, page.length)
                    }
                } else
                    page = "/pl/" + page
                listin = open(page, "", "toolbar=no,location=no,directories=no,status=yes,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no,width=320,height=300")
                if (navigator.appName == "Netscape")
                    listin.focus()
            }

            function errorWindow1(cadena) {
                var parameter = cadena
                newWindow = window.open("error.aspx", "newWin", "toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=no,resizable=no,width=220,height=400,left=300,top=100")
                if (navigator.appName == "Netscape")
                    newWindow.focus()
            }

            function checkdates1(month, day, year, returning) {
                var year = parseInt(year)
                var leap_year = (year % 4 == 0 & year % 100 != 0) || (year % 400 == 0)
                if (month == "02" && (day == "30" || day == "29") && !leap_year) {
                    if (returning == 0)
                        return "<li> Pick-up date is not valid..</li>"
                    else
                        return "<li> Invalid drop-off date.</li>"
                }
                var comp1 = (month == "02" || month == "04" || month == "06" || month == "09" || month == "11")
                var comp2 = (day == "31")
                if (comp1 && comp2) {
                    if (returning == 0)
                        return "<li> Pick-up date is not valid..</li>"
                    else
                        return "<li> Invalid drop-off date.</li>"
                } else
                    return ""
            }

            function checkcar() {
                checkstr = ""

                var myform = document.CAR_ENTRY_FORM
                var dYearMonthIndex = myform.B_MONTH.selectedIndex
                var dDayIndex = myform.B_DAY.selectedIndex
                var dYearMonthValue = myform.B_MONTH.options[dYearMonthIndex].value
                var dDayValue = myform.B_DAY.options[dDayIndex].value
                var dTimeIndex = myform.B_TIME_TO_PROCESS.selectedIndex
                var dTimeValue = myform.B_TIME_TO_PROCESS.options[dTimeIndex].value
                var dTimeText = myform.B_TIME_TO_PROCESS.options[dTimeIndex].text
                myform.B_DATE.value = dYearMonthValue + dDayValue + dTimeValue

                var rYearMonthIndex = myform.E_MONTH.selectedIndex
                var rDayIndex = myform.E_DAY.selectedIndex
                var rTimeIndex = myform.E_TIME_TO_PROCESS.selectedIndex
                var rTimeValue = myform.E_TIME_TO_PROCESS.options[rTimeIndex].value
                var rTimeText = myform.E_TIME_TO_PROCESS.options[rTimeIndex].text
                var rYearMonthValue = myform.E_MONTH.options[rYearMonthIndex].value
                var rDayValue = myform.E_DAY.options[rDayIndex].value

                myform.E_DATE.value = rYearMonthValue + rDayValue + rTimeValue
                myform.E_LOCATION.value = myform.B_LOCATION.value

                var tDate = new Date()
                var tMonth = thisMonth
                var tDay = thisDay
                var tYear = thisYear
                var monthLength = days[tMonth]
                var tHour = tDate.getHours()
                var tMinute = tDate.getMinutes()
                var nowThen = ""
                var dnowThen = ""
                var rnowThen = ""

                if (tDay + 1 > monthLength) {
                    tDate.setDate((tDay + 1) - monthLength)
                    if (tMonth < 11) {
                        tDate.setMonth(tMonth + 1)
                    }
                    else {
                        tDate.setMonth(0)
                        tDate.setFullYear(tYear + 1)
                    }
                }
                else {
                    tDate.setDate(tDay + 1)
                }
                if (tDate.getMonth() > 8) {
                    if (tDate.getDate() < 10) {
                        nowThen = tDate.getFullYear() + "" + (tDate.getMonth() + 1) + "0" + tDate.getDate()
                    }
                    else {
                        nowThen = tDate.getFullYear() + "" + (tDate.getMonth() + 1) + "" + tDate.getDate()
                    }
                }
                else {
                    if (tDay < 10) {
                        nowThen = tDate.getFullYear() + "0" + (tDate.getMonth() + 1) + "0" + tDate.getDate()
                    }
                    else {
                        nowThen = tDate.getFullYear() + "0" + (tDate.getMonth() + 1) + "" + tDate.getDate()
                    }
                }

                if (tMinute < 55) {
                    if (tHour < 9) {
                        dnowThen = nowThen + "0" + (tHour + 1) + "00"
                    }
                    else {
                        dnowThen = nowThen + "" + (tHour + 1) + "00"
                    }
                    if (tHour < 8) {
                        rnowThen = nowThen + "0" + (tHour + 2) + "00"
                    }
                    else {
                        rnowThen = nowThen + "" + (tHour + 2) + "00"
                    }
                }
                else {
                    if (tHour < 8) {
                        dnowThen = nowThen + "0" + (tHour + 2) + "00"
                    }
                    else {
                        dnowThen = nowThen + "" + (tHour + 2) + "00"
                    }
                    if (tHour < 7) {
                        rnowThen = nowThen + "0" + (tHour + 3) + "00"
                    }
                    else {
                        rnowThen = nowThen + "" + (tHour + 3) + "00"
                    }
                }

                var drop_down_list = document.CAR_ENTRY_FORM.B_LOCATION.selectedIndex
                if (document.CAR_ENTRY_FORM.B_LOCATION.value == "" && !drop_down_list)
                    checkstr += "<li> Choose a Pick-up airport.</li>"
                var imonth = document.CAR_ENTRY_FORM.B_MONTH.selectedIndex
                var omonth = document.CAR_ENTRY_FORM.E_MONTH.selectedIndex
                var iday = document.CAR_ENTRY_FORM.B_DAY.selectedIndex
                var oday = document.CAR_ENTRY_FORM.E_DAY.selectedIndex
                var itime = document.CAR_ENTRY_FORM.B_TIME_TO_PROCESS.selectedIndex
                var otime = document.CAR_ENTRY_FORM.E_TIME_TO_PROCESS.selectedIndex
                //if(((omonth == imonth) && (oday == iday) && (otime <= itime) ) || ((omonth == imonth) && (oday < iday)) || (omonth < imonth))
                //checkstr += "<li> Drop-off date should be later than pick-up date.</li>"
                var a = document.CAR_ENTRY_FORM.B_MONTH.options[imonth].value
                var b = document.CAR_ENTRY_FORM.B_DAY.options[iday].value
                year = a.substring(0, 4)
                a = a.substring(4, 6)
                checkstr += checkdates1(a, b, year, 0)
                var a = document.CAR_ENTRY_FORM.E_MONTH.options[omonth].value
                var b = document.CAR_ENTRY_FORM.E_DAY.options[oday].value
                year = a.substring(0, 4)
                a = a.substring(4, 6)
                checkstr += checkdates1(a, b, year, 1)

                //check to see that they did not select an earlier month or date within the same month
                if ((rYearMonthValue < dYearMonthValue) || ((rDayValue < dDayValue) && (rYearMonthValue == dYearMonthValue))) {
                    checkstr += "<li> Return date must be later than departure date.  Please check your return selection."
                }

                //now let's check to make sure that the user has selected a departure date that is at least 24 hours from one hour
                //from the current time...if we don't capture this and handle here it will get to their generic search page...and who
                //wants that			 			

                if (myform.B_DATE.value < dnowThen)
                    checkstr += "<li> Pick-up date must be at least twenty-four (24) hours from now - please check your pick-up date/time selection."
                if (myform.E_DATE.value < rnowThen)
                    checkstr += "<li> Return date must be at least twenty-four (24) hours from now - please check you return date/time selection"
                if (myform.E_DATE.value <= myform.B_DATE.value)
                    checkstr += "<li> Return date must be later than pick-up date - please check your return date/time selection"

                if (checkstr != "") {
                    errorWindow1(checkstr)
                    return false
                } else {

                    var myform = document.CAR_ENTRY_FORM
                    var dYearMonthIndex = myform.B_MONTH.selectedIndex
                    var dDayIndex = myform.B_DAY.selectedIndex
                    var dYearMonthValue = myform.B_MONTH.options[dYearMonthIndex].value
                    var dDayValue = myform.B_DAY.options[dDayIndex].value
                    var dTimeIndex = myform.B_TIME_TO_PROCESS.selectedIndex
                    var dTimeValue = myform.B_TIME_TO_PROCESS.options[dTimeIndex].value
                    var dTimeText = myform.B_TIME_TO_PROCESS.options[dTimeIndex].text
                    myform.B_DATE.value = dYearMonthValue + dDayValue + dTimeValue

                    var rYearMonthIndex = myform.E_MONTH.selectedIndex
                    var rDayIndex = myform.E_DAY.selectedIndex
                    var rTimeIndex = myform.E_TIME_TO_PROCESS.selectedIndex
                    var rTimeValue = myform.E_TIME_TO_PROCESS.options[rTimeIndex].value
                    var rTimeText = myform.E_TIME_TO_PROCESS.options[rTimeIndex].text
                    var rYearMonthValue = myform.E_MONTH.options[rYearMonthIndex].value
                    var rDayValue = myform.E_DAY.options[rDayIndex].value

                    myform.E_DATE.value = rYearMonthValue + rDayValue + rTimeValue
                    //alert(myform.E_DATE.value);
                    return true
                }
            }

            function check2() {
                if (checkcar())
                    document.CAR_ENTRY_FORM.submit()
            }

            function ChangeArrivingInfo(callProc, callPanel) {
                if (callPanel == "AIR") {
                    var thisForm = document.forms(1)
                }
                else {
                    var thisForm = document.forms(2)
                }
                var GoMonth = 0

                var goDate = thisForm.B_MONTH.options[thisForm.B_MONTH.selectedIndex].value + thisForm.B_DAY.options[thisForm.B_DAY.selectedIndex].value
                var comeDate = thisForm.E_MONTH.options[thisForm.E_MONTH.selectedIndex].value + thisForm.E_DAY.options[thisForm.E_DAY.selectedIndex].value
                if (callProc == "ChangeDateDrops" && comeDate < goDate || callProc == "E_MONTH" && comeDate < goDate) {
                    AlignReturnDate()
                    return
                }
                else if (callProc == "ChangeDateDrops" && comeDate >= goDate) {
                    return
                }
                else if (callProc == "E_MONTH" && comeDate >= goDate) {

                    if (callProc == "ChangeDateDrops") {
                        GoMonth = thisForm.B_MONTH.selectedIndex
                        thisForm.E_MONTH.options(GoMonth).selected = true
                    }
                    else {
                        GoMonth = thisForm.E_MONTH.selectedIndex
                    }

                    var ReturnYear = thisForm.E_MONTH.options(GoMonth).value.substr(0, 4)
                    var ReturnMonth = thisForm.E_MONTH.options(GoMonth).value.substr(4, 2)

                    //ReturnMonth is a string - if the value is less than 10 (e.g. "09") we need to strip the leading zero
                    if (ReturnMonth < 10) {
                        ReturnMonth = ReturnMonth.substr(1, 1)
                    }

                    //get the days in the selected month
                    var monthLength = days[ReturnMonth - 1]

                    //from the end of the list backwards delete all the options except for 0
                    for (i1 = (thisForm.E_DAY.options.length - 1); i1 > 0; i1--) {
                        thisForm.E_DAY.options.remove(i1)
                    }

                    //decision point - is the selected month the current calendar month
                    //if yes then start the days drop as of 1 day from today's calendar date...the end of the month
                    //	 is determined by how long the selected month is
                    //otherwise, create the drop as a whole month, the number of days depending upon how long the
                    //	 selected month is
                    if (ReturnMonth != (thisMonth + 1)) {
                        thisForm.E_DAY.options(0).value = "01"
                        thisForm.E_DAY.options(0).innerText = 1
                        thisForm.E_DAY.options(0).selected = true
                        if (ReturnMonth == 2) {
                            //if it is February check for leap year...non-leap years always have a remainder when divided by 4
                            //	 the ceil function will round up to the nearest integer causing the two sides to be unequal
                            //	 and, therefore, a non-leap year
                            if ((ReturnYear / 4) == Math.ceil(ReturnYear / 4)) {
                                monthLength = 29
                            }
                            else {
                                monthLength = 28
                            }
                        }
                        //create the rest of the month
                        for (i1 = 1; i1 < monthLength; i1++) {
                            var newOption = document.createElement("OPTION")
                            thisForm.E_DAY.options.add(newOption, i1)
                            if ((i1 + 1) < 10) {
                                thisForm.E_DAY.options(i1).value = "0" + (i1 + 1)
                            }
                            else {
                                thisForm.E_DAY.options(i1).value = i1 + 1
                            }
                            thisForm.E_DAY.options(i1).innerText = i1 + 1
                        }
                    }
                    else {
                        //if 1 day from today is next month then get the values for the next month
                        //	 and set the first day to that date
                        //otherwise, the beginning day for this drop is 1 day from today 

                        if (thisDay + 1 > monthLength) {
                            firstDay = (thisDay + 1 - monthLength)
                            //if it's December then set the month to January
                            if (ReturnMonth < 12) {
                                ReturnMonth = ReturnMonth + 1
                            }
                            else {
                                ReturnMonth = 1
                                ReturnYear = ReturnYear + 1
                            }
                            monthLength = days[ReturnMonth - 1]
                        }
                        else {
                            firstDay = thisDay + 1
                        }
                        if (ReturnMonth == 2) {
                            if ((ReturnYear / 4) == Math.ceil(ReturnYear / 4)) {
                                monthLength = 29
                            }
                            else {
                                monthLength = 28
                            }
                        }
                        //set the value of the first day in the list to be 4 days from now
                        if (firstDay < 10) {
                            thisForm.E_DAY.options(0).value = "0" + firstDay
                        }
                        else {
                            thisForm.E_DAY.options(0).value = firstDay
                        }
                        thisForm.E_DAY.options(0).innerText = firstDay
                        thisForm.E_DAY.options(0).selected = true

                        //create the rest of the days drop		
                        for (i1 = 1; i1 < (monthLength - (firstDay - 1)); i1++) {
                            var newOption = document.createElement("OPTION")
                            thisForm.E_DAY.options.add(newOption, i1)
                            if ((firstDay + i1) < 10) {
                                thisForm.E_DAY.options(i1).value = "0" + (firstDay + i1)
                            }
                            else {
                                thisForm.E_DAY.options(i1).value = firstDay + i1
                            }
                            thisForm.E_DAY.options(i1).innerText = firstDay + i1
                        }
                    }
                }
            }

            function ChangeDateDrops(callPanel) {
                if (callPanel == "AIR") {
                    var thisForm = document.forms(1)
                }
                else {
                    var thisForm = document.forms(2)
                }
                var GoMonth = thisForm.B_MONTH.options(thisForm.B_MONTH.selectedIndex).value.substr(4, 2)
                var GoYear = thisForm.B_MONTH.options(thisForm.B_MONTH.selectedIndex).value.substr(0, 4)

                //strip the zero if necessary
                if (GoMonth < 10) {
                    GoMonth = GoMonth.substr(1, 1)
                }

                //get the number of days of the selected month
                var monthLength = days[GoMonth - 1]

                //delete all but one of the original days list starting from the end backwards
                for (i1 = (thisForm.B_DAY.options.length - 1); i1 > 0; i1--) {
                    thisForm.B_DAY.options.remove(i1)
                }

                //decision point - is the selected month the current calendar month
                //if yes then start the days drop as of 1 day from today's calendar date...the end of the month
                //	 is determined by how long the selected month is
                //otherwise, create the drop as a whole month, the number of days depending upon how long the
                //	 selected month is
                if (GoMonth != (thisMonth + 1)) {
                    //set the remaining option to the beginning of the month
                    thisForm.B_DAY.options(0).value = "01"
                    thisForm.B_DAY.options(0).innerText = 1
                    thisForm.B_DAY.options(0).selected = true
                    if (GoMonth == 2) {
                        //if it is February check for leap year...non-leap years always have a remainder when divided by 4
                        //	 the ceil function will round up to the nearest integer causing the two sides to be unequal
                        //	 and, therefore, a non-leap year
                        if ((GoYear / 4) == Math.ceil(GoYear / 4)) {
                            monthLength = 29
                        }
                        else {
                            monthLength = 28
                        }
                    }
                    //now fill the days drop
                    for (i1 = 1; i1 < (monthLength); i1++) {
                        var newOption = document.createElement("OPTION")
                        thisForm.B_DAY.options.add(newOption, i1)

                        if ((i1 + 1) < 10) {
                            thisForm.B_DAY.options(i1).value = "0" + (i1 + 1)
                        }
                        else {
                            thisForm.B_DAY.options(i1).value = i1 + 1
                        }

                        thisForm.B_DAY.options(i1).innerText = i1 + 1
                    }
                }
                else {
                    //if 1 day from today is next month then get the values for the next month
                    //	 and set the first day to that date
                    //otherwise, the beginning day for this drop is 1 day from today 

                    if (thisDay + 1 > monthLength) {
                        firstDay = (thisDay + 1 - monthLength)
                        //if it's December then set the month to January
                        if (GoMonth < 12) {
                            GoMonth = GoMonth + 1
                        }
                        else {
                            GoMonth = 1
                            GoYear = GoYear + 1
                        }
                        monthLength = days[GoMonth - 1]
                    }
                    else {
                        firstDay = thisDay + 1
                    }
                    if (GoMonth == 2) {
                        if ((GoYear / 4) == Math.ceil(GoYear / 4)) {
                            monthLength = 29
                        }
                        else {
                            monthLength = 28
                        }
                    }
                    //set the remaining option to four days from now
                    if (firstDay < 10) {
                        thisForm.B_DAY.options(0).value = "0" + firstDay
                    }
                    else {
                        thisForm.B_DAY.options(0).value = "" + firstDay
                    }
                    thisForm.B_DAY.options(0).innerText = firstDay
                    thisForm.B_DAY.options(0).selected = true

                    //create the rest of the days drop			 
                    for (i1 = 1; i1 < (monthLength - (firstDay - 1)); i1++) {
                        var newOption = document.createElement("OPTION")
                        thisForm.B_DAY.options.add(newOption, i1)

                        if ((firstDay + i1) < 10) {
                            thisForm.B_DAY.options(i1).value = "0" + (firstDay + i1)
                        }
                        else {
                            thisForm.B_DAY.options(i1).value = firstDay + i1
                        }
                        thisForm.B_DAY.options(i1).innerText = firstDay + i1
                    }
                }
                //now go take care of the return drops 
                ChangeArrivingInfo("ChangeDateDrops", callPanel)
            }

            function AlignReturnDate() {
                var goDate = thisForm.B_MONTH.options[thisForm.B_MONTH.selectedIndex].value + "" + thisForm.B_DAY.options[thisForm.B_DAY.selectedIndex].value
                var comeDate = thisForm.E_MONTH.options[thisForm.E_MONTH.selectedIndex].value + "" + thisForm.E_DAY.options[thisForm.E_DAY.selectedIndex].value
                if (comeDate < goDate) {
                    thisForm.E_MONTH.selectedIndex = thisForm.B_MONTH.selectedIndex
                    if (thisForm.E_DAY.length != thisForm.B_DAY.length) {
                        for (i1 = (thisForm.E_DAY.length - 1); i1 > 0; i1--) {
                            thisForm.E_DAY.options.remove(i1)
                        }
                        thisForm.E_DAY.options[0].value = thisForm.B_DAY.options[0].value
                        thisForm.E_DAY.options[0].innerText = thisForm.B_DAY.options[0].innerText
                        for (i1 = 1; i1 <= (thisForm.B_DAY.length - 1); i1++) {
                            var newOption = document.createElement("OPTION")
                            thisForm.E_DAY.options.add(newOption, i1)
                            thisForm.E_DAY.options[i1].value = thisForm.B_DAY.options[i1].value
                            thisForm.E_DAY.options[i1].innerText = thisForm.B_DAY.options[i1].innerText
                        }
                        thisForm.E_DAY.selectedIndex = thisForm.B_DAY.selectedIndex
                    }
                    else {
                        thisForm.E_DAY.selectedIndex = thisForm.B_DAY.selectedIndex
                    }
                }
            }

	//-->
    </script>


    <!--- End JavaScript used for Car Rental Quick Search --->
    <!--- Start JavaScript used for Hotel Quick Search --->

    <script type="text/javascript">
			<!--
        // Called to validate mandatory information
        function CheckHotelForm() {
            var myform = document.Hotel_Search //forms[0]
            var dYearMonthIndex = myform.B_MONTH.selectedIndex
            var dDayIndex = myform.B_DAY.selectedIndex
            var dYearMonthValue = myform.B_MONTH.options[dYearMonthIndex].value
            var dDayValue = myform.B_DAY.options[dDayIndex].value

            myform.B_DATE.value = dYearMonthValue + dDayValue + "1200"

            var rYearMonthIndex = myform.E_MONTH.selectedIndex
            var rDayIndex = myform.E_DAY.selectedIndex

            var rYearMonthValue = myform.E_MONTH.options[rYearMonthIndex].value
            var rDayValue = myform.E_DAY.options[rDayIndex].value
            myform.E_DATE.value = rYearMonthValue + rDayValue + "2359"

            return true;
        }

        // Update return date so it is after outbound date.
        function HotelChangeReturnDate() {
            var myform = document.Hotel_Search //forms[0]
            var selectedMonth
            var IsShortMonth
            var btnRetDate = true

            selectedMonth = myform.B_MONTH.options[myform.B_MONTH.selectedIndex].value.charAt(4) + myform.B_MONTH.options[myform.B_MONTH.selectedIndex].value.charAt(5)
            IsShortMonth = (selectedMonth == "02" || selectedMonth == "04" || selectedMonth == "06" || selectedMonth == "09" || selectedMonth == "11")

            if (btnRetDate && departing_date_selected) {
                if (myform.B_MONTH.selectedIndex > myform.E_MONTH.selectedIndex || myform.B_MONTH.options[myform.B_MONTH.selectedIndex].value == "XX") {
                    if (((myform.E_DAY.options[myform.E_DAY.selectedIndex].value == 31) && (IsShortMonth)) || ((myform.E_DAY.options[myform.E_DAY.selectedIndex].value > 28) && (selectedMonth == "02"))) {
                        myform.E_MONTH.selectedIndex = myform.B_MONTH.selectedIndex + 1
                        myform.E_DAY.selectedIndex = 0
                    }
                    else {
                        myform.E_MONTH.selectedIndex = myform.B_MONTH.selectedIndex
                    }
                }
                if ((myform.B_DAY.selectedIndex >= myform.E_DAY.selectedIndex || myform.E_DAY.options[myform.E_DAY.selectedIndex].value == "XX") && (myform.E_MONTH.selectedIndex == myform.B_MONTH.selectedIndex)) {
                    if (((myform.B_DAY.options[myform.B_DAY.selectedIndex].value > 29) && (IsShortMonth)) || (myform.B_DAY.options[myform.B_DAY.selectedIndex].value == 31) || ((myform.B_DAY.options[myform.B_DAY.selectedIndex].value > 27) && (myform.B_MONTH.selectedIndex == 6))) {
                        myform.E_MONTH.selectedIndex = myform.B_MONTH.selectedIndex + 1
                        myform.E_DAY.selectedIndex = 0
                    }
                    else {
                        myform.E_DAY.selectedIndex = myform.B_DAY.selectedIndex + 1
                    }
                }
            }
            departing_date_selected = false
        }

        function SetDate() {
            var myform = document.Hotel_Search; //forms[0];
            var dYearMonthValue = myform.B_MONTH.options[myform.B_MONTH.selectedIndex].value
            var dDayValue = myform.B_DAY.options[myform.B_DAY.selectedIndex].value
            myform.B_CAL_DATE_1.value = dYearMonthValue.substr(0, 4) + '-' + dYearMonthValue.substr(4, 2) + '-' + dDayValue

            var rYearMonthValue = myform.E_MONTH.options[myform.E_MONTH.selectedIndex].value
            var rDayValue = myform.E_DAY.options[myform.E_DAY.selectedIndex].value
            myform.B_CAL_DATE_2.value = rYearMonthValue.substr(0, 4) + '-' + rYearMonthValue.substr(4, 2) + '-' + rDayValue
        }

        function UpdateDate(bOutbound) {
            var MonthYear;
            var Day;
            var myform = document.Hotel_Search;
            var CalendarDate;
            if (bOutbound) {
                // format is 'YYYYMMDD'
                CalendarDate = myform.B_CAL_DATE_1.value;
                MonthYear = CalendarDate.substr(0, 6)
                Day = CalendarDate.substr(6, 2);
                // Now I need to set the dates in the drop lists.
                for (n = 0; n < myform.B_MONTH.length; n++) {
                    if (myform.B_MONTH.options[n].value == MonthYear)
                        myform.B_MONTH.selectedIndex = n;
                }
                for (n = 0; n < myform.B_DAY.length; n++) {
                    if (myform.B_DAY.options[n].value == Day)
                        myform.B_DAY.selectedIndex = n;
                }

                // And now I need to update the Return date!
                departing_date_selected = true;
                HotelChangeReturnDate();
            }
            else {
                // format is 'YYYYMMDD'
                CalendarDate = myform.B_CAL_DATE_2.value;
                MonthYear = CalendarDate.substr(0, 6)
                Day = CalendarDate.substr(6, 2);
                // Now I need to set the dates in the drop lists.
                for (n = 0; n < myform.E_MONTH.length; n++) {
                    if (myform.E_MONTH.options[n].value == MonthYear)
                        myform.E_MONTH.selectedIndex = n;
                }
                for (n = 0; n < myform.E_DAY.length; n++) {
                    if (myform.E_DAY.options[n].value == Day)
                        myform.E_DAY.selectedIndex = n;
                }
            }
        }
//-->
    </script>

    <style type="text/css">
        .style1
        {
            width: 100px;
        }
    </style>
</head>
<body leftmargin="0" topmargin="0" marginheight="0" marginwidth="0" onload="init();">
    <uc1:ucMenu ID="UcMenu1" runat="server"></uc1:ucMenu>

    <script type="text/javascript">
        var bClearDeparting = true;
        var bClearArriving = true;
        var bClearPickUp = true;

        function clearField(inputField) {
            if (bClearDeparting && (inputField.name == "B_LOCATION_1")) {
                inputField.value = "";
                bClearDeparting = false;
            }

            if (bClearArriving && (inputField.name == "E_LOCATION_1")) {
                inputField.value = "";
                bClearArriving = false;
            }

            if (bClearPickUp && (inputField.name == "B_LOCATION")) {
                inputField.value = "";
                bClearPickUp = false;
            }
        }
    </script>

    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <%If Session("IsTravelerPlusEmployee") = "TRUE" Then%>
            <td valign="top" width="50%">
                <img height="10" src="images/blank.gif" width="10" border="0">
            </td>
            <%Else%>
            <td valign="top" width="50%">
                <img height="10" src="images/blank.gif" width="10" border="0">
            </td>
            <%End If%>
            <td valign="top" width="728">
                <!-- header bar -->
                <!-- end header bar -->
                <table cellspacing="0" cellpadding="0" width="740" border="0">
                    <%If Session("IsTravelerPlusEmployee") <> "TRUE" Then%>
                    <!-- Added new code to account for new ucmenu adjustments -->
                    <tr>
                        <td colspan="4">
                            &nbsp;
                        </td>
                    </tr>
                    <%End If%>
                    <tr>
                        <td valign="top" width="10" bgcolor="#ffffff">
                            <img src="images/blank.gif" width="10">
                        </td>
                        <td valign="top" width="185" bgcolor="#ffffff">
                            <table class="submenu" cellspacing="0" cellpadding="0" width="185" border="0">
                                <tr>
                                    <td>
                                        <traveler:nav ID="lNav" runat="server" />
                                    </td>
                                </tr>
                                <!-- /5 sub -->
                            </table>
                            <br>
                            <!--<a href="RvCamping.aspx" target="_self"><IMG src="images/c2c_banner.jpg" border="0"></a><br><br>-->
                                                    
                        <%If Session("IsTravelerPlusEmployee") = "TRUE" Then%>
                            <br>
                            <a href="/TravelerPlus/employee/default.aspx" target="_self">Sign out</a>
                            
                        <%Else%>
                            <script language="JavaScript1.2" type="text/javascript">
									var slideshow_width='180px' //SET IMAGE WIDTH
									var slideshow_height='150px' //SET IMAGE HEIGHT
									var pause=3000 //SET PAUSE BETWEEN SLIDE (3000=3 seconds)
									var employee = "<%= Session("IsTravelerPlusEmployee")%>"
									var gycVisible="<%=Session("gycBanner") %>"
									var fadeimages=new Array()
									//SET 1) IMAGE PATHS, 2) optional link, 3), optional link target:
								if (employee){
								    fadeimages[0] = ["~/TravelerPlus/owner/images/OA_generic_180x150.jpg", '~/TravelerPlus/owner/ownerAdventures.aspx', ""] //update for new banner 12-01-13 DRomero
								    fadeimages[1] = ["~/TravelerPlus/owner/images/CoastToCoast_banner.jpg", '~/TravelerPlus/owner/RvCamping.aspx', ""] //C2C
								    fadeimages[2] = ["~/TravelerPlus/owner/images/hotweeksBanner.jpg", '~/TravelerPlus/owner/hotweekssearch.aspx', ""]
								    fadeimages[3] = ["~/TravelerPlus/owner/images/worldhotelsbanner_new.jpg ", "~/TravelerPlus/owner/HotelPointStays.aspx", "_self"] //World Hotels Resorts - WORLDHOTELS
									fadeimages[4] = ["~/TravelerPlus/owner/images/oa-down-under.jpg", 'https://bluegreenowner.com/travelerplus/owner/ownerAdventures.aspx>', "_self"] //Printable Coupons - Entertainment - Interval International
									fadeimages[5] = ["~/TravelerPlus/owner/images/TRA100733_1.jpg", 'http://travelerplus.condodirect.com/web/cs?a=5&loginID=<%= Session("OwnerNumber")%>&phoneNumber=<%= Session("OwnerHomePhone")%>', "_self"] 
								    fadeimages[6] = ["~/TravelerPlus/images/ask-bluegreen-180x150.jpg", 'https://bluegreenvacations.force.com/CustomerCommunity/s/', "_blank"] //C2C
								//if purchase date greater tha 7/1/2006 show GYC Banner
								if(gycVisible=="True")
									{
								    fadeimages[7] = ["~/TravelerPlus/owner/images/YachtBanner.jpg", "~/TravelerPlus/owner/gyc.aspx", ""] //Grand Yacht Club Banner
								    fadeimages[8] = ["~/TravelerPlus/owner/images/sc-banner-02.gif", '~/TravelerPlus/owner/selectconnections.aspx', ""]//selectConnectionBanner_1
								    fadeimages[9] = ["~/TravelerPlus/owner/images/sc-banner-01.gif", '~/TravelerPlus/owner/selectconnections.aspx', ""]//selectConnectionBanner_2
									 
									} 
								}
								else{
								    fadeimages[0] = ["~/TravelerPlus/owner/images/OA_generic_180x150.jpg", '~/TravelerPlus/owner/ownerAdventures.aspx', ""]
								    fadeimages[1] = ["~/TravelerPlus/owner/images/CoastToCoast_banner.jpg", '~/TravelerPlus/owner/RvCamping.aspx', ""] //C2C
								    fadeimages[2] = ["~/TravelerPlus/owner/images/hotweeksBanner.jpg", '~/TravelerPlus/owner/hotweekssearch.aspx', ""]
								    fadeimages[3] = ["~/TravelerPlus/owner/images/worldhotelsbanner_new.jpg ", "~/TravelerPlus/owner/HotelPointStays.aspx", "_self"] //World Hotels Resorts - WORLDHOTELS
									 fadeimages[4] = ["~/TravelerPlus/owner/images/oa-down-under.jpg", 'https://bluegreenowner.com/travelerplus/owner/ownerAdventures.aspx>', "_self"] //Printable Coupons - Entertainment - Interval International
									 fadeimages[5] = ["~/TravelerPlus/owner/images/TRA100733_1.jpg", 'http://travelerplus.condodirect.com/web/cs?a=5&loginID=<%= Session("OwnerNumber")%>&phoneNumber=<%= Session("OwnerHomePhone")%>', "_self"] 
								    fadeimages[6] = ["~/TravelerPlus/images/ask-bluegreen-180x150.jpg", 'https://bluegreenvacations.force.com/CustomerCommunity/s/', "_blank"] //C2C
								//if purchase date greater tha 7/1/2006 show GYC Banner
									if(gycVisible=="True")
									{
									    fadeimages[7] = ["~/TravelerPlus/owner/images/YachtBanner.jpg", "~/TravelerPlus/owner/gyc.aspx", ""] //Grand Yacht Club Banner
									    fadeimages[8] = ["~/TravelerPlus/owner/images/sc-banner-02.gif", '~/TravelerPlus/owner/selectconnections.aspx', ""]//selectConnectionBanner_1
									    fadeimages[9] = ["~/TravelerPlus/owner/images/sc-banner-01.gif", '~/TravelerPlus/owner/selectconnections.aspx', ""]//selectConnectionBanner_2
									}
								}
							////NO need to edit beyond here/////////////
														
								var preloadedimages=new Array()
								for (p=0;p<fadeimages.length;p++){
								preloadedimages[p]=new Image()
								preloadedimages[p].src=fadeimages[p][0]
								}

								var ie4=document.all
								var dom=document.getElementById

								if (ie4||dom)
								document.write('<div style="position:relative;width:'+slideshow_width+';height:'+slideshow_height+';overflow:hidden"><div  id="canvas0" style="position:absolute;width:'+slideshow_width+';height:'+slideshow_height+';top:0;left:0;filter:alpha(opacity=10);-moz-opacity:10"></div><div id="canvas1" style="position:absolute;width:'+slideshow_width+';height:'+slideshow_height+';top:0;left:0;filter:alpha(opacity=10);-moz-opacity:10;visibility: hidden"></div></div>')
								else
								document.write('<img name="defaultslide" src="'+fadeimages[0][0]+'">')

								var curpos=10
								var degree=10
								var curcanvas="canvas0"
								var curimageindex=0
								var nextimageindex=1

								function fadepic(){
								if (curpos<100){
								curpos+=10
								if (tempobj.filters)
								tempobj.filters.alpha.opacity=curpos
								else if (tempobj.style.MozOpacity)
								tempobj.style.MozOpacity=curpos/101
								}
								else{
								clearInterval(dropslide)
								nextcanvas=(curcanvas=="canvas0")? "canvas0" : "canvas1"
								tempobj=ie4? eval("document.all."+nextcanvas) : document.getElementById(nextcanvas)
								tempobj.innerHTML=insertimage(nextimageindex)
								nextimageindex=(nextimageindex<fadeimages.length-1)? nextimageindex+1 : 0
								var tempobj2=ie4? eval("document.all."+nextcanvas) : document.getElementById(nextcanvas)
								tempobj2.style.visibility="hidden"
								setTimeout("rotateimage()",pause)
								}
								}

								function insertimage(i){
								var tempcontainer=fadeimages[i][1]!=""? '<a href="'+fadeimages[i][1]+'" target="'+fadeimages[i][2]+'">' : ""
								tempcontainer+='<img src="'+fadeimages[i][0]+'" border="0">'
								tempcontainer=fadeimages[i][1]!=""? tempcontainer+'</a>' : tempcontainer
								return tempcontainer
								}

								function rotateimage(){
								if (ie4||dom){
								resetit(curcanvas)
								var crossobj=tempobj=ie4? eval("document.all."+curcanvas) : document.getElementById(curcanvas)
								crossobj.style.zIndex++
								tempobj.style.visibility="visible"
								var temp='setInterval("fadepic()",50)'
								dropslide=eval(temp)
								curcanvas=(curcanvas=="canvas0")? "canvas1" : "canvas0"
								}
								else
								document.images.defaultslide.src=fadeimages[curimageindex][0]
								curimageindex=(curimageindex<fadeimages.length-1)? curimageindex+1 : 0
								}

								function resetit(what){
								curpos=10
								var crossobj=ie4? eval("document.all."+what) : document.getElementById(what)
								if (crossobj.filters)
								crossobj.filters.alpha.opacity=curpos
								else if (crossobj.style.MozOpacity)
								crossobj.style.MozOpacity=curpos/101
								}

								function startit(){
								var crossobj=ie4? eval("document.all."+curcanvas) : document.getElementById(curcanvas)
								crossobj.innerHTML=insertimage(curimageindex)
								rotateimage()
								}

								if (ie4||dom)
								window.onload=startit
								else
								setInterval("rotateimage()",pause)

                            </script>
                        <%End If%>
                        <td valign="top" align="left" width="497">
                            <table cellspacing="0" cellpadding="0" width="497" border="0">
                                <tr>
                                    <td class="bcrumb" width="340" valign="top">
                                        <a class="bcrumblink" href='<%= Session("securedURL") %>default.aspx'>Bluegreen</a>
											/ <a class="bcrumblink" href="/TravelerPlus/owner/home.aspx">Owners</a>
												/ <a class="bcrumblink" href="home.aspx">Traveler Plus</a> / Home
                                    </td>
                                </tr>
                                <tr>
                                    <td id="page-title">
                                        <h2 id="travelerplus-home">
                                            Welcome to Bluegreen Traveler Plus&trade;</h2>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
										<br />
                                        <object id="flash" classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" width="497" height="175">
                                            <param name="movie" value="ownerAdventures_4.swf" />
                                            <param name="quality" value="high" />
                                            <param name="wmode" value="opaque" />
                                            <param name="swfversion" value="6.0.65.0" />
                                            <!-- This param tag prompts users with Flash Player 6.0 r65 and higher to download the latest version of Flash Player. Delete it if you don’t want users to see the prompt. -->
                                            <!-- Next object tag is for non-IE browsers. So hide it from IE using IECC. -->
                                            <!--[if !IE]>-->
                                            <object type="application/x-shockwave-flash" data="ownerAdventures_4.swf" width="497" height="175">
                                                <!--<![endif]-->
                                                <param name="quality" value="high" />
                                                <param name="wmode" value="opaque" />
                                                <param name="swfversion" value="6.0.65.0" />
                                            </object>
                                    </td> 
                                </tr>
                                <tr>
                                    <td align="left">
                                        <p>
                                            <%If Session("IsTravelerPlusEmployee") <> "TRUE" Then%>
                                            <br />
                                            Do you want more opportunities to use your Bluegreen Vacation Points (&quot;Points&quot;)?
                                            Save money and discover all you can do with our <a class="textlink" href="/TravelerPlus/owner/pointsmartCashsmart.aspx#ps">
                                                <font color="#0069B9">PointSmart</font></a><img src="/TravelerPlus/owner/images/sm.gif" width="11" height="12">
                                            benefits. Want to save your Points? Use cash or credit to enjoy our popular <a class="textlink"
                                                href="/TravelerPlus/owner/pointsmartCashsmart.aspx#cs"><font color="#1EAC4B">CashSmart</font></a>
                                                <img src="/TravelerPlus/owner/images/sm.gif" width="11" height="12" alt="" />
                                            benefits.
                                            <%End If %>
                                        </p>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="white-space: nowrap;">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <form name="TravelPanel" runat="server" id="Form1">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="23%">
                                                    <span style="white-space: nowrap;">
                                                        <asp:ImageButton ID="ImgBtnQuickSearchAir" runat="server" ImageUrl="./images/quicksearchAir_off.gif"
                                                            ImageAlign="left"></asp:ImageButton>
                                                    </span>
                                                </td>
                                                <td width="24%">
                                                    <span style="white-space: nowrap;">
                                                        <asp:ImageButton ID="ImgBtnQuickSearchCar" runat="server" ImageUrl="./images/quicksearchCar.gif"
                                                            ImageAlign="Left"></asp:ImageButton>
                                                    </span>
                                                </td>
                                                <td width="24%">
                                                    <span style="white-space: nowrap;">
                                                        <asp:ImageButton ID="ImgBtnQuickSearchHotel" runat="server" ImageUrl="./images/quicksearchHotels_off.gif"
                                                            ImageAlign="Left"></asp:ImageButton>
                                                    </span>
                                                </td>
                                                <td width="27%"> 
                                                    <a href="/TravelerPlus/owner/cruises.aspx">
                                                        <img src="/TravelerPlus/owner/images/quicksearchCruises_off.gif" 
                                                        alt="Cruises" width="126" height="28" border="0"
                                                            align="left" style="border-width: 0px;" /></a>
                                                </td>
                                            </tr>
                                        </table>
                                        </form>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="bottom" colspan="3">
                                        <!-- for Production Environement -->
                                        <form action="http://wftc1.e-travel.com/plnext/AIEACYLACYL/TravelShopperAvailability.action"
                                        method="post" name="AIR_ENTRY_FORM" onsubmit="return Check();" target="new_window">

                                        <!-- for test (DEV and STG) Environement -->
<%--                                        <form action='http://siteacceptance.wftc1.e-travel.com/plnext/AIEACYLACYL/TravelShopperAvailability.action'
                                        method='POST' name='AIR_ENTRY_FORM' onsubmit='return Check()' target='new_window'>
--%>                      
                                        <input type="hidden" name="LANGUAGE" value="US" />
                                        <input type="hidden" name="SITE" value="ACYLACYL" />
                                        <input type="hidden" name="AIR_CABIN" value="E" />
                                        <input type="hidden" name="TRIP_TYPE" value="R" />
                                        <input type="hidden" name="TRAVELLER_TYPE_1" value="ADT" />
                                        <input type="hidden" name="TRAVELLER_TYPE_2" value="" />
                                        <input type="hidden" name="TRAVELLER_TYPE_3" value="" />
                                        <input type="hidden" name="TRAVELLER_TYPE_4" value="" />
                                        <input type="hidden" name="TRAVELLER_TYPE_5" value="" />
                                        <input type="hidden" name="B_ANY_TIME_1" value="FALSE" />
                                        <input type="hidden" name="B_ANY_TIME_2" value="FALSE" />
                                        <input type="hidden" name="B_DATE_1" value="" />
                                        <input type="hidden" name="B_DATE_2" value="" />
                                        <input type="hidden" name="PRODUCT_TYPE_1" value="STANDARD_AIR" />
                                        <input type="hidden" name="DISTANCE_UNIT" value="M" />
                                        <input type="hidden" name="B_TIME_WINDOW_1" value="" />
                                        <input type="hidden" name="B_TIME_WINDOW_2" value="" />
                                        <input type="hidden" name="MINUS_DATE_RANGE_1" value="" />
                                        <input type="hidden" name="MINUS_DATE_RANGE_2" value="" />
                                        <input type="hidden" name="PLUS_DATE_RANGE_1" value="" />
                                        <input type="hidden" name="PLUS_DATE_RANGE_2" value="" />
                                        <input type="hidden" name="PLTG_FROMPAGE" value="MPSEARCH" />
                                        <input type="hidden" name="TRIP_FLOW" value="YES" />
                                        <input type="hidden" name="B_CAL_DATE_1" value="" />
                                        <input type="hidden" name="B_CAL_DATE_2" value="" />
                                        <input type="hidden" name="SEARCH_PAGE" value="MP" />
                                        <input type="hidden" name="FIELD_ADT_NUMBER" value="1" />
                                        <input type="hidden" name="AIR_MAX_CONNECTIONS" value="4" />
                                        <%--<%If Session("IsTravelerPlusEmployee") = "TRUE" Then%>
									<input type="hidden" name="SOSCODE" value='<%= Session("EmployeeID")%>'>
									<%Else%>
									<input type="hidden" name="SOSCODE" value='<%= Session("OwnerNumber")%>'>
									<%End If%>--%>
                                        <input type='hidden' name='PAYMENT_TYPE' value='CON' />
                                        <input type='hidden' name='DELIVERY_TYPE' value='ETCKT' />
                                        <input type='hidden' name='SESSION_ID' />
                                        <input type='hidden' name='SO_GL' value='<?xml version="1.0" encoding="iso-8859-1"?><SO_GL><GLOBAL_LIST mode="complete"><NAME>SO_SINGLE_MULTIPLE_COMMAND_BUILDER</NAME><LIST_ELEMENT><CODE>1</CODE><LIST_VALUE>AIAN7201004000</LIST_VALUE><LIST_VALUE>S</LIST_VALUE></LIST_ELEMENT></GLOBAL_LIST></SO_GL>' />
                                        <asp:Panel ID="PnlAir" runat="server" Height="166px" Width="498px" Visible="True">
                                            <div align="left">
                                                <table height="166" cellspacing="0" cellpadding="0" width="499" border="0" ms_2d_layout="TRUE">
                                                    <tr valign="top">
                                                        <td width="5" height="133" background="images/dots.gif">
                                                            &nbsp;
                                                        </td>
                                                        <td width="487" height="133" background="images/reservBoxBkgd3.gif">
                                                            <table cellspacing="0" cellpadding="0" width="487" border="0">
                                                                <tr>
                                                                    <td align="right" colspan="7">
                                                                    </td>
                                                                </tr>
                                                                <tr height="22">
                                                                    <td width="12">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td width="70" valign="bottom" align="left">
                                                                        <input type="radio" name="TRIP_TYPE" value="O" onclick="OnewayTrip()">One-way
                                                                    </td>
                                                                    <td width="80" valign="bottom" align="left">
                                                                        <input type="radio" name="TRIP_TYPE" value="R" onclick="ReturnTrip()" checked>Round
                                                                        Trip
                                                                    </td>
                                                                    <td valign="bottom" width="80">
                                                                   </td>
                                                                    <td width="60">
                                                                        Name:
                                                                    </td>
                                                                    <%If Session("IsTravelerPlusEmployee") = "TRUE" Then%>
                                                                    <td align="left" colspan="2">
                                                                        <input tabindex="9" name="NAME_FIRST" type="text" value='<%= Trim(Session("EmployeeFirstName"))%>'
                                                                            size="15">&nbsp;
                                                                        <input tabindex="10" name="NAME_LAST" type="text" value='<%= Session("EmployeeLastName")%>'
                                                                            size="15">
                                                                    </td>
                                                                    <%Else%>
                                                                    <td align="left" colspan="2">
                                                                        <input tabindex="9" name="NAME_FIRST" type="text" value='<%= Trim(Session("OwnerNameFirst"))%>'
                                                                            size="15">&nbsp;
                                                                        <input tabindex="10" name="NAME_LAST" type="text" value='<%= Session("OwnerNameLast")%>'
                                                                            size="15">
                                                                    </td>
                                                                    <%End If%>
                                                                </tr>
                                                                <tr height="22">
                                                                    <td width="12">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td width="70">
                                                                        From:
                                                                    </td>
                                                                    <td colspan="2" align="left">

                                                                        <script language="JavaScript">
																				<!--
                                                                            if (navigator.appName == "Netscape")
                                                                                document.write('<input type="text" value="City name or airport code" tabindex="1" name="B_LOCATION_1" size="24" align="top" onFocus="clearField(this)">');
                                                                            else
                                                                                document.write('<input type="text" value="City name or airport code" tabindex="1" name="B_LOCATION_1" size="24" align="top" onFocus="clearField(this)">');
																				//-->
                                                                        </script>

                                                                    </td>
                                                                    <td>
                                                                        Email:
                                                                    </td>
                                                                    <%If Session("IsTravelerPlusEmployee") = "TRUE" Then%>
                                                                    <td colspan="2" align="left">
                                                                        <%--<input tabindex="11" type="text" name="EMAIL" value='<%= Session("EmployeeEmail")%>' size="38">--%>
                                                                        <input tabindex="11" type="text" name="CONTACT_POINT_EMAIL_1" size="35" value='<%= Session("EmployeeEmail")%>' />
                                                                    </td>
                                                                    <%Else%>
                                                                    <td colspan="2" align="left">
                                                                        <%--<input tabindex="11" type="text" name="EMAIL" value='<%= Session("OwnerEmailAddress")%>' size="38">--%>
                                                                        <input type="text" tabindex="11" name="CONTACT_POINT_EMAIL_1" size="35" />
                                                                    </td>
                                                                    <%End If%>
                                                                </tr>
                                                                <tr height="22">
                                                                    <td width="12">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        <select name="B_MONTH" id="aB_MONTH" tabindex="2" onchange='ChangeDateDrops("AIR")'>

                                                                            <script type="text/javascript">
                                                                                var thisForm = document.forms(1)
                                                                                var bMonth = thisMonth
                                                                                var bDay = thisDay
                                                                                var bYear = thisYear
                                                                                var bDate = new Date()
                                                                                var monthLength = days[bMonth]

                                                                                if (bDay + 1 > monthLength) {
                                                                                    if (bMonth < 11) {
                                                                                        bMonth = bMonth + 1
                                                                                    }
                                                                                    else {
                                                                                        bMonth = 0
                                                                                        bYear = bYear + 1
                                                                                    }
                                                                                }
                                                                                for (i1 = 1; i1 < 12; i1++) {
                                                                                    if (bMonth < 9) {
                                                                                        document.write('<option value="' + bYear + '0' + (bMonth + 1) + '">' + months[bMonth].slice(0, 3) + ' ' + bYear + '</option>')
                                                                                    }
                                                                                    else {
                                                                                        document.write('<option value="' + bYear + (bMonth + 1) + '">' + months[bMonth].slice(0, 3) + ' ' + bYear + '</option>')
                                                                                    }
                                                                                    bMonth = bMonth + 1
                                                                                    if (bMonth > 11) {
                                                                                        bMonth = 0
                                                                                        bYear = bYear + 1
                                                                                    }
                                                                                    bDate.setMonth(bMonth)
                                                                                    bDate.setFullYear(bYear)
                                                                                }
                                                                                thisForm.B_MONTH.options(0).selected = true																	
                                                                            </script>

                                                                        </select>
                                                                    </td>
                                                                    <td colspan="2">
                                                                        <select name="B_DAY" id="aB_DAY" tabindex="3" onchange='AlignReturnDate("AIR")'>

                                                                            <script type="text/javascript">
                                                                                var firstDay = 0
                                                                                var thisForm = document.forms(1)
                                                                                var bMonth = thisMonth
                                                                                var bDate = new Date()
                                                                                var bDay = thisDay
                                                                                var bYear = thisYear
                                                                                var monthLength = days[bMonth]

                                                                                if (bDay + 1 > monthLength) {
                                                                                    firstDay = (bDay + 1 - monthLength)
                                                                                    if (bMonth < 11) {
                                                                                        bMonth = bMonth + 1
                                                                                    }
                                                                                    else {
                                                                                        bMonth = 0
                                                                                        bYear = bYear + 1
                                                                                    }
                                                                                    monthLength = days[bMonth]
                                                                                }
                                                                                else {
                                                                                    firstDay = bDay + 1
                                                                                }

                                                                                if (bMonth == 1) {
                                                                                    if ((bYear / 4) == Math.ceil(bYear / 4)) {
                                                                                        monthLength = 29
                                                                                    }
                                                                                    else {
                                                                                        monthLength = 28
                                                                                    }
                                                                                }
                                                                                for (i1 = firstDay; i1 <= monthLength; i1++) {
                                                                                    if (i1 < 10) {
                                                                                        document.write('<option value="0' + i1 + '">' + i1 + '</option>')
                                                                                    }
                                                                                    else {
                                                                                        document.write('<option value="' + i1 + '">' + i1 + '</option>')
                                                                                    }
                                                                                }
                                                                                thisForm.B_DAY.options(0).selected = true
                                                                            </script>

                                                                        </select>
                                                                        <select name="B_TIME_TO_PROCESS" id="B_TIME_TO_PROCESS" tabindex="4" onchange="setETime">
                                                                            <option selected = "selected" value="ANY">Any Time</option>
                                                                            <option value="MORNING">Morning</option>
                                                                            <option value="AFTERNOON">Afternoon</option>
                                                                            <option value="EVENING">Evening</option>
                                                                            <option value="0000">12:00 AM</option>
                                                                            <option value="0100">1:00 AM</option>
                                                                            <option value="0200">2:00 AM</option>
                                                                            <option value="0300">3:00 AM</option>
                                                                            <option value="0400">4:00 AM</option>
                                                                            <option value="0500">5:00 AM</option>
                                                                            <option value="0600">6:00 AM</option>
                                                                            <option value="0700">7:00 AM</option>
                                                                            <option value="0800">8:00 AM</option>
                                                                            <option value="0900">9:00 AM</option>
                                                                            <option value="1000">10:00 AM</option>
                                                                            <option value="1100">11:00 AM</option>
                                                                            <option value="1200">12:00 PM</option>
                                                                            <option value="1300">1:00 PM</option>
                                                                            <option value="1400">2:00 PM</option>
                                                                            <option value="1500">3:00 PM</option>
                                                                            <option value="1600">4:00 PM</option>
                                                                            <option value="1700">5:00 PM</option>
                                                                            <option value="1800">6:00 PM</option>
                                                                            <option value="1900">7:00 PM</option>
                                                                            <option value="2000">8:00 PM</option>
                                                                            <option value="2100">9:00 PM</option>
                                                                            <option value="2200">10:00 PM</option>
                                                                            <option value="2300">11:00 PM</option>
                                                                        </select>
                                                                    </td>
                                                                    <td>
                                                                        Phone:
                                                                    </td>
                                                                    <%If Session("IsTravelerPlusEmployee") = "TRUE" Then%>
                                                                    <td>
                                                                        <%--<input tabindex="12" type="text" name="PHONEH" value='<%= Session("EmployeePhone")%>' size="12">--%>
                                                                        <input tabindex="12" type="text" name="CONTACT_POINT_HOME_PHONE" value='<%= Session("EmployeePhone") %>'
                                                                            size="10" />
                                                                    </td>
                                                                    <%Else%>
                                                                    <td>
                                                                        <%--<input tabindex="12" type="text" name="PHONEH" value='<%= Session("OwnerHomePhone")%>' size="12">--%>
                                                                        <input tabindex="12" type="text" name="CONTACT_POINT_HOME_PHONE" size="10" />
                                                                    </td>
                                                                    <%End If%>
                                                                    <td>
                                                                        <a href="/TravelerPlus/owner/airlineSearch.aspx">Add Travelers</a>
                                                                    </td>
                                                                </tr>
                                                                <tr height="22">
                                                                    <td width="12">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td width="70">
                                                                        To:
                                                                    </td>
                                                                    <td colspan="2" align="left">

                                                                        <script type="text/javascript">
																					<!--
                                                                            if (navigator.appName == "Netscape")
                                                                                document.write('<input type="text" value="City name or airport code" tabindex="5" name="E_LOCATION_1" size="24" align="top" onFocus="clearField(this)">');
                                                                            else
                                                                                document.write('<input type="text" value="City name or airport code" tabindex="5" name="E_LOCATION_1" size="24" align="top" onFocus="clearField(this)">');
																					//-->
                                                                        </script>

                                                                    </td>
                                                                    <td>
                                                                        Traveler:
                                                                    </td>
                                                                    <td valign="top">
                                                                        <select name="TRAVELLER_TYPE_1" tabindex="13">
                                                                            <option selected ="selected" value="ADT">Adult</option>
                                                                            <option value="CHD">Child (2-11)</option>
                                                                        </select>
                                                                    </td>
                                                                    <td valign="top">
                                                                        infant?
                                                                        <input type="checkbox" name="HAS_INFANT_1" value="TRUE" />
                                                                    </td>
                                                                </tr>
                                                                <tr height="22">
                                                                    <td width="12">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        <select name="E_MONTH" id="aE_MONTH" tabindex="6" onchange='ChangeArrivingInfo("E_MONTH", "AIR")'>

                                                                            <script type="text/javascript">
                                                                                var eMonth = thisMonth
                                                                                var eYear = thisYear
                                                                                var eMonth = thisMonth
                                                                                var eDay = thisDay
                                                                                var eDate = new Date()
                                                                                var thisForm = document.forms(1)
                                                                                var monthLength = days[eMonth]

                                                                                if (eDay + 1 > monthLength) {
                                                                                    if (eMonth < 11) {
                                                                                        eMonth = eMonth + 1
                                                                                    }
                                                                                    else {
                                                                                        eMonth = 0
                                                                                        eYear = eYear + 1
                                                                                    }
                                                                                }
                                                                                for (i1 = 1; i1 < 12; i1++) {
                                                                                    if (eMonth < 9) {
                                                                                        document.write('<option value="' + eYear + '0' + (eMonth + 1) + '">' + months[eMonth].slice(0, 3) + ' ' + eYear + '</option>')
                                                                                    }
                                                                                    else {
                                                                                        document.write('<option value="' + eYear + (eMonth + 1) + '">' + months[eMonth].slice(0, 3) + ' ' + eYear + '</option>')
                                                                                    }
                                                                                    eMonth = eMonth + 1
                                                                                    if (eMonth > 11) {
                                                                                        eMonth = 0
                                                                                        eYear = eYear + 1
                                                                                    }
                                                                                    eDate.setMonth(eMonth)
                                                                                    eDate.setFullYear(eYear)
                                                                                }
                                                                                thisForm.E_MONTH.options(0).selected = true																	
                                                                            </script>

                                                                        </select>
                                                                    </td>
                                                                    <td colspan="2">
                                                                        <select name="E_DAY" id="aE_DAY" tabindex="7">

                                                                            <script type="text/javascript">
                                                                                var firstDay = 0
                                                                                var eMonth = thisMonth
                                                                                var eYear = thisYear
                                                                                var eDay = thisDay
                                                                                var eDate = new Date()
                                                                                var thisForm = document.forms(1)
                                                                                var monthLength = days[eMonth]

                                                                                if (eDay + 1 > monthLength) {
                                                                                    firstDay = (eDay + 1 - monthLength)
                                                                                    if (eMonth < 11) {
                                                                                        eMonth = eMonth + 1
                                                                                    }
                                                                                    else {
                                                                                        eMonth = 0
                                                                                        eYear = eYear + 1
                                                                                    }
                                                                                    monthLength = days[eMonth]
                                                                                }
                                                                                else {
                                                                                    firstDay = eDay + 1
                                                                                }

                                                                                if (eMonth == 1) {
                                                                                    if ((eYear / 4) == Math.ceil(eYear / 4)) {
                                                                                        monthLength = 29
                                                                                    }
                                                                                    else {
                                                                                        monthLength = 28
                                                                                    }
                                                                                }
                                                                                for (i1 = firstDay; i1 <= monthLength; i1++) {
                                                                                    if (i1 < 10) {
                                                                                        document.write('<option value="0' + i1 + '">' + i1 + '</option>')
                                                                                    }
                                                                                    else {
                                                                                        document.write('<option value="' + i1 + '">' + i1 + '</option>')
                                                                                    }
                                                                                }
                                                                                thisForm.E_DAY.options(0).selected = true
                                                                            </script>

                                                                        </select>
                                                                        <select name="E_TIME_TO_PROCESS" id="E_TIME_TO_PROCESS" tabindex="8">
                                                                            <option selected = "selected" value="ANY">Any Time</option>
                                                                            <option value="MORNING">Morning</option>
                                                                            <option value="AFTERNOON">Afternoon</option>
                                                                            <option value="EVENING">Evening</option>
                                                                            <option value="0000">12:00 AM</option>
                                                                            <option value="0100">1:00 AM</option>
                                                                            <option value="0200">2:00 AM</option>
                                                                            <option value="0300">3:00 AM</option>
                                                                            <option value="0400">4:00 AM</option>
                                                                            <option value="0500">5:00 AM</option>
                                                                            <option value="0600">6:00 AM</option>
                                                                            <option value="0700">7:00 AM</option>
                                                                            <option value="0800">8:00 AM</option>
                                                                            <option value="0900">9:00 AM</option>
                                                                            <option value="1000">10:00 AM</option>
                                                                            <option value="1100">11:00 AM</option>
                                                                            <option value="1200">12:00 PM</option>
                                                                            <option value="1300">1:00 PM</option>
                                                                            <option value="1400">2:00 PM</option>
                                                                            <option value="1500">3:00 PM</option>
                                                                            <option value="1600">4:00 PM</option>
                                                                            <option value="1700">5:00 PM</option>
                                                                            <option value="1800">6:00 PM</option>
                                                                            <option value="1900">7:00 PM</option>
                                                                            <option value="2000">8:00 PM</option>
                                                                            <option value="2100">9:00 PM</option>
                                                                            <option value="2200">10:00 PM</option>
                                                                            <option value="2300">11:00 PM</option>
                                                                        </select>
                                                                    </td>
                                                                    <td>
                                                                        Search by:
                                                                    </td>
                                                                    <td>
                                                                        <select name="SEARCH_BY" class="forminputdrop" tabindex="15">
                                                                            <option value="2" selected>fare</option>
                                                                            <option value="1">schedule</option>
                                                                        </select>
                                                                    </td>
                                                                    <td align="center">
                                                                        <input name="image" type="image" style="{border: 0px; }" tabindex="16" src="/TravelerPlus/owner/images/search_btn.gif"
                                                                            alt="Search" width="40" height="20" border="0" target="_top">
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td width="8" height="133" background="images/dotsright.gif">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" valign="top">
                                                            <img src="/TravelerPlus/owner/images/leftdots.gif" align="left" alt=""/><img src="/TravelerPlus/owner/images/rightdots.gif" align="right"
                                                                border="0" alt="" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </asp:Panel>
                                        </form>
                                        <!-- for test (DEV and STG) Environement -->
<%--                                        <form action='http://siteacceptance.wftc1.e-travel.com/plnext/AIEACYLACYL/CarAvailability.action'
                                        method='post' name='CAR_ENTRY_FORM' onsubmit='return checkcar()' target='new_window'>
--%>
                                        <!-- for Production Environement -->
                                        <form name='CAR_ENTRY_FORM' action='http://wftc1.e-travel.com/plnext/AIEACYLACYL/CarAvailability.action'
                                        method='post' onsubmit='return checkcar()' target='new_window'>
                                        <input type="hidden" value="US" name="LANGUAGE" />
                                        <input type="hidden" value="ACYLACYL" name="SITE" />
                                        <input type="hidden" value="" name="B_DATE" />
                                        <input type="hidden" value="" name="E_DATE" />
                                        <input type="hidden" value="" name="E_LOCATION" />
                                        <input type="hidden" name="PLTG_FROMPAGE" value="CARSEARCH" />
                                        <input type="hidden" name="DISPLAY_TYPE" value="M" />
                                        <input type="hidden" name="TRIP_FLOW" value="YES" />
                                        <input type="hidden" name="B_CAL_DATE_1" value="" />
                                        <input type="hidden" name="B_CAL_DATE_2" value="" />
                                        <asp:Panel ID="PnlCar" runat="server" Height="133px" Width="498px" Visible="False">
                                            <div align="left">
                                                <table height="133" cellspacing="0" cellpadding="0" width="499" border="0" ms_2d_layout="TRUE">
                                                    <tr valign="top">
                                                        <td width="5" height="133" background="images/dots.gif">
                                                            &nbsp;
                                                        </td>
                                                        <td width="487" height="133" background="images/reservBoxBkgd3.gif">
                                                            <table cellspacing="0" cellpadding="0" width="487" border="0">
                                                                <tr>
                                                                    <td align="right" colspan="7">
                                                                    </td>
                                                                </tr>
                                                                <tr height="22">
                                                                    <td width="12">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td width="40">
                                                                        Location:
                                                                    </td>
                                                                    <td colspan="2" align="left">
                                                                        <input tabindex="1" type="text" name="B_LOCATION" class="forminput" value="City name or airport code"
                                                                            size="24" onfocus="clearField(this)" />
                                                                    </td>
                                                                    <td>
                                                                        Car Class:
                                                                    </td>
                                                                    <td colspan="2" align="left">
                                                                        <select name="CLASS" tabindex="11">
                                                                            <option selected = "selected" value="*">No preference</option>
                                                                            <option value="C">Compact</option>
                                                                            <option value="E">Economy</option>
                                                                            <option value="F">Full size</option>
                                                                            <option value="I">Intermediate</option>
                                                                            <option value="L">Luxury</option>
                                                                            <option value="M">Mini</option>
                                                                            <option value="P">Premium</option>
                                                                            <option value="S">Standard</option>
                                                                            <option value="X">Special</option>
                                                                        </select>
                                                                    </td>
                                                                </tr>
                                                                <tr height="22">
                                                                    <td width="12">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        Pick-up:
                                                                    </td>
                                                                    <td>
                                                                        <select name="B_MONTH" id="cB_MONTH" onchange='ChangeDateDrops("CAR")' tabindex="2">

                                                                            <script type="text/javascript">
                                                                                var thisForm = document.forms(2)
                                                                                var bMonth = thisMonth
                                                                                var bYear = thisYear
                                                                                var bDate = new Date()
                                                                                var bDay = thisDay
                                                                                var monthLength = days[bMonth]

                                                                                if (bDay + 1 > monthLength) {
                                                                                    if (bMonth < 11) {
                                                                                        bMonth = bMonth + 1
                                                                                    }
                                                                                    else {
                                                                                        bMonth = 0
                                                                                        bYear = bYear + 1
                                                                                    }
                                                                                }
                                                                                for (i1 = 1; i1 < 12; i1++) {
                                                                                    if (bMonth < 9) {
                                                                                        document.write('<option value="' + bYear + '0' + (bMonth + 1) + '">' + months[bMonth].slice(0, 3) + ' ' + bYear + '</option>')
                                                                                    }
                                                                                    else {
                                                                                        document.write('<option value="' + bYear + (bMonth + 1) + '">' + months[bMonth].slice(0, 3) + ' ' + bYear + '</option>')
                                                                                    }
                                                                                    bMonth = bMonth + 1
                                                                                    if (bMonth > 11) {
                                                                                        bMonth = 0
                                                                                        bYear = bYear + 1
                                                                                    }
                                                                                    bDate.setMonth(bMonth)
                                                                                    bDate.setFullYear(bYear)
                                                                                }
                                                                                thisForm.B_MONTH.options(0).selected = true																	
                                                                            </script>

                                                                        </select>
                                                                    </td>
                                                                    <td>
                                                                        <select name="B_DAY" id="cB_DAY" onchange='AlignReturnDate("AIR")' tabindex="3">

                                                                            <script type="text/javascript">
                                                                                var firstDay = 0
                                                                                var thisForm = document.forms(2)
                                                                                var bMonth = thisMonth
                                                                                var bDay = thisDay
                                                                                var bYear = thisYear
                                                                                var bDate = new Date()
                                                                                var monthLength = days[bMonth]

                                                                                if (bDay + 1 > monthLength) {
                                                                                    firstDay = (bDay + 1 - monthLength)
                                                                                    if (bMonth < 11) {
                                                                                        bMonth = bMonth + 1
                                                                                    }
                                                                                    else {
                                                                                        bMonth = 0
                                                                                        bYear = bYear + 1
                                                                                    }
                                                                                    monthLength = days[bMonth]
                                                                                }
                                                                                else {
                                                                                    firstDay = bDay + 1
                                                                                }
                                                                                if (bMonth == 1) {
                                                                                    if ((bYear / 4) == Math.ceil(bYear / 4)) {
                                                                                        monthLength = 29
                                                                                    }
                                                                                    else {
                                                                                        monthLength = 28
                                                                                    }
                                                                                }
                                                                                for (i1 = firstDay; i1 <= monthLength; i1++) {
                                                                                    if (i1 < 10) {
                                                                                        document.write('<option value="0' + i1 + '">' + i1 + '</option>')
                                                                                    }
                                                                                    else {
                                                                                        document.write('<option value="' + i1 + '">' + i1 + '</option>')
                                                                                    }
                                                                                }
                                                                                thisForm.B_DAY.options(0).selected = true
                                                                            </script>

                                                                        </select>
                                                                        <select name="B_TIME_TO_PROCESS" id="cB_TIME_TO_PROCESS" tabindex="4">
                                                                            <option value="2400">12:00 AM</option>
                                                                            <option value="0100">1:00 AM</option>
                                                                            <option value="0200">2:00 AM</option>
                                                                            <option value="0300">3:00 AM</option>
                                                                            <option value="0400">4:00 AM</option>
                                                                            <option value="0500">5:00 AM</option>
                                                                            <option value="0600">6:00 AM</option>
                                                                            <option value="0700">7:00 AM</option>
                                                                            <option value="0800">8:00 AM</option>
                                                                            <option value="0900">9:00 AM</option>
                                                                            <option value="1000">10:00 AM</option>
                                                                            <option value="1100">11:00 AM</option>
                                                                            <option value="1200">12:00 PM</option>
                                                                            <option value="1300">1:00 PM</option>
                                                                            <option value="1400">2:00 PM</option>
                                                                            <option value="1500">3:00 PM</option>
                                                                            <option value="1600">4:00 PM</option>
                                                                            <option value="1700">5:00 PM</option>
                                                                            <option value="1800">6:00 PM</option>
                                                                            <option value="1900">7:00 PM</option>
                                                                            <option value="2000">8:00 PM</option>
                                                                            <option value="2100">9:00 PM</option>
                                                                            <option value="2200">10:00 PM</option>
                                                                            <option value="2300">11:00 PM</option>

                                                                            <script type="text/javascript">
                                                                                var timeSelect = document.getElementById("cB_TIME_TO_PROCESS")
                                                                                var tDate = new Date()
                                                                                var tHour = tDate.getHours()
                                                                                if (tHour < 9) {
                                                                                    tHour = "0" + (tHour + 1) + "00"
                                                                                }
                                                                                else {
                                                                                    tHour = "" + (tHour + 1) + "00"
                                                                                }
                                                                                for (i1 = 0; i1 < timeSelect.length; i1++) {
                                                                                    var x = timeSelect.options[i1].value
                                                                                    if (timeSelect.options[i1].value == tHour) {
                                                                                        timeSelect.selectedIndex = i1
                                                                                    }
                                                                                }
                                                                            </script>

                                                                        </select>
                                                                    </td>
                                                                    <td>
                                                                        Car Type:
                                                                    </td>
                                                                    <td colspan="2">
                                                                        <select name="TYPE" tabindex="12">
                                                                            <option selected = "selected"  value="*">No preference</option>
                                                                            <option value="B">2 door car</option>
                                                                            <option value="C">2 door/4 door</option>
                                                                            <option value="D">4 door car</option>
                                                                            <option value="F">4 wheel drive</option>
                                                                            <option value="J">All Terrain</option>
                                                                            <option value="K">Truck</option>
                                                                            <option value="L">Limousine</option>
                                                                            <option value="P">Pickup</option>
                                                                            <option value="R">Recreation</option>
                                                                            <option value="S">Sports car</option>
                                                                            <option value="T">Convertible</option>
                                                                            <option value="V">Van</option>
                                                                            <option value="W">Wagon</option>
                                                                            <option value="X">Special</option>
                                                                        </select>
                                                                    </td>
                                                                </tr>
                                                                <tr height="22">
                                                                    <td width="12">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        Drop-off:
                                                                    </td>
                                                                    <td>
                                                                        <select name="E_MONTH" id="cE_MONTH" onchange='ChangeArrivingInfo("E_MONTH", "CAR")'
                                                                            tabindex="6">

                                                                            <script type="text/javascript">
                                                                                var eMonth = thisMonth
                                                                                var eYear = thisYear
                                                                                var eDay = thisDay
                                                                                var eDate = new Date()
                                                                                var thisForm = document.forms(2)
                                                                                var monthLength = days[eMonth]

                                                                                if (eDay + 1 > monthLength) {
                                                                                    if (eMonth < 11) {
                                                                                        eMonth = eMonth + 1
                                                                                    }
                                                                                    else {
                                                                                        eMonth = 0
                                                                                        eYear = eYear + 1
                                                                                    }
                                                                                }
                                                                                for (i1 = 1; i1 < 12; i1++) {
                                                                                    if (eMonth < 9) {
                                                                                        document.write('<option value="' + eYear + '0' + (eMonth + 1) + '">' + months[eMonth].slice(0, 3) + ' ' + eYear + '</option>')
                                                                                    }
                                                                                    else {
                                                                                        document.write('<option value="' + eYear + (eMonth + 1) + '">' + months[eMonth].slice(0, 3) + ' ' + eYear + '</option>')
                                                                                    }
                                                                                    eMonth = eMonth + 1
                                                                                    if (eMonth > 11) {
                                                                                        eMonth = 0
                                                                                        eYear = eYear + 1
                                                                                    }
                                                                                    eDate.setMonth(eMonth)
                                                                                    eDate.setFullYear(eYear)
                                                                                }
                                                                                thisForm.E_MONTH.options(0).selected = true																	
                                                                            </script>

                                                                        </select>
                                                                    </td>
                                                                    <td>
                                                                        <select name="E_DAY" id="cE_DAY" tabindex="7">

                                                                            <script type="text/javascript">
                                                                                var firstDay = 0
                                                                                var eMonth = thisMonth
                                                                                var eYear = thisYear
                                                                                var eDay = thisDay
                                                                                var eDate = new Date()
                                                                                var thisForm = document.forms(2)
                                                                                var monthLength = days[eMonth]

                                                                                if (eDay + 1 > monthLength) {
                                                                                    firstDay = (eDay + 1 - monthLength)
                                                                                    if (eMonth < 11) {
                                                                                        eMonth = eMonth + 1
                                                                                    }
                                                                                    else {
                                                                                        eMonth = 0
                                                                                        eYear = eYear + 1
                                                                                    }
                                                                                    monthLength = days[eMonth]
                                                                                }
                                                                                else {
                                                                                    firstDay = eDay + 1
                                                                                }
                                                                                if (eMonth == 1) {
                                                                                    if ((eYear / 4) == Math.ceil(eYear / 4)) {
                                                                                        monthLength = 29
                                                                                    }
                                                                                    else {
                                                                                        monthLength = 28
                                                                                    }
                                                                                }
                                                                                for (i1 = firstDay; i1 <= monthLength; i1++) {
                                                                                    if (i1 < 10) {
                                                                                        document.write('<option value="0' + i1 + '">' + i1 + '</option>')
                                                                                    }
                                                                                    else {
                                                                                        document.write('<option value="' + i1 + '">' + i1 + '</option>')
                                                                                    }
                                                                                }
                                                                                thisForm.E_DAY.options(0).selected = true
                                                                            </script>

                                                                        </select>
                                                                        <select name="E_TIME_TO_PROCESS" id="cE_TIME_TO_PROCESS" tabindex="8">
                                                                            <option value="2400">12:00 AM</option>
                                                                            <option value="0100">1:00 AM</option>
                                                                            <option value="0200">2:00 AM</option>
                                                                            <option value="0300">3:00 AM</option>
                                                                            <option value="0400">4:00 AM</option>
                                                                            <option value="0500">5:00 AM</option>
                                                                            <option value="0600">6:00 AM</option>
                                                                            <option value="0700">7:00 AM</option>
                                                                            <option value="0800">8:00 AM</option>
                                                                            <option value="0900">9:00 AM</option>
                                                                            <option value="1000">10:00 AM</option>
                                                                            <option value="1100">11:00 AM</option>
                                                                            <option value="1200">12:00 PM</option>
                                                                            <option value="1300">1:00 PM</option>
                                                                            <option value="1400">2:00 PM</option>
                                                                            <option value="1500">3:00 PM</option>
                                                                            <option value="1600">4:00 PM</option>
                                                                            <option value="1700">5:00 PM</option>
                                                                            <option value="1800">6:00 PM</option>
                                                                            <option value="1900">7:00 PM</option>
                                                                            <option value="2000">8:00 PM</option>
                                                                            <option value="2100">9:00 PM</option>
                                                                            <option value="2200">10:00 PM</option>
                                                                            <option value="2300">11:00 PM</option>

                                                                            <script type="text/javascript">
                                                                                var timeSelect = document.getElementById("cE_TIME_TO_PROCESS")
                                                                                var tDate = new Date()
                                                                                var tHour = tDate.getHours()
                                                                                if (tHour < 8) {
                                                                                    tHour = "0" + (tHour + 2) + "00"
                                                                                }
                                                                                else {
                                                                                    tHour = "" + (tHour + 2) + "00"
                                                                                }
                                                                                for (i1 = 0; i1 < timeSelect.length; i1++) {
                                                                                    var x = timeSelect.options[i1].value
                                                                                    if (timeSelect.options[i1].value == tHour) {
                                                                                        timeSelect.selectedIndex = i1
                                                                                    }
                                                                                }
                                                                            </script>

                                                                        </select>
                                                                    </td>
                                                                    <td>
                                                                        Transmission:
                                                                    </td>
                                                                    <td colspan="2">
                                                                        <select name="TRANSMISSION" class="forminputdrop" tabindex="15">
                                                                            <option selected = "selected" value="*">No preference</option>
                                                                            <option value="A">Automatic</option>
                                                                            <option value="M">Manual</option>
                                                                        </select>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="12">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td colspan="3" align="left">
                                                                        <a href="/TravelerPlus/owner/carrentalSearch.aspx" target="_self">Advanced Search Options</a>
                                                                    </td>
                                                                    <td>
                                                                        Air conditioning:
                                                                    </td>
                                                                    <td colspan="2">
                                                                        <select name="AIR_CONDITIONING" tabindex="16">
                                                                            <option selected = "selected" value="*">No preference</option>
                                                                            <option value="N">No air conditioning</option>
                                                                            <option value="R">Air conditioning</option>
                                                                        </select>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="7" align="right" valign="bottom" height="30">
                                                                        <input name="image" type="image" style="{border: 0px; }" tabindex="17" src="images/search_btn.gif"
                                                                            alt="Search" width="40" height="20" border="0" onclick="check2()" target="_top" />&nbsp;&nbsp;&nbsp;
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td width="8" height="133" background="images/dotsright.gif">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" valign="top">
                                                            <img src="/TravelerPlus/owner/images/leftdots.gif" align="left" alt="" /><img src="/TravelerPlus/owner/images/rightdots.gif" align="right" alt="" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </asp:Panel>
                                        </form>
                                        <!-- for test (DEV and STG) Environement -->
<%--                                    <form name="Hotel_Search"  action="http://siteacceptance.wftc1.e-travel.com/plnext/AIEACYLACYL/HotelAvailabilityGeneric.action"
                                        onsubmit="return CheckHotelForm()" method="post" target="new_window">
--%>  
                                        <!-- for Production Environement -->
                                        <form name="Hotel_Search"  action="http://wftc1.e-travel.com/plnext/AIEACYLACYL/HotelAvailabilityGeneric.action" 
                                        onsubmit="return CheckHotelForm()" method="post" target="new_window">

                                        <input type="hidden" value="US" name="LANGUAGE" />
                                        <input type="hidden" value="ACYLACYL" name="SITE" />
                                        <input type="hidden" value="0" name="LAST_HOTEL_INDEX" />
                                        <input type="hidden" value="" name="B_DATE" />
                                        <input type="hidden" value="" name="E_DATE" />
                                        <input type="hidden" value="1" name="AVAILABILITY_TYPE" />
                                        <input type="hidden" value="1" name="WHERE" />
                                        <input type="hidden" value="" name="B_LOCATION" />
                                        <input type="hidden" value="P" name="SORT_CRITERIA" />
                                        <input type="hidden" name="PLTG_FROMPAGE" value="HOTS" />
                                        <input type="hidden" name="TRIP_FLOW" value="YES" />
                                        <input type="hidden" name="B_CAL_DATE_1" value="" />
                                        <input type="hidden" name="B_CAL_DATE_2" value="" />
                                        <input type="hidden" name="NUMBER_OF_HOTELS" value="100" />
                                        <asp:Panel ID="pnlHotel" runat="server" Height="136px" Width="498px" Visible="False">
                                            <div align="left">
                                                <table height="133" cellspacing="0" cellpadding="0" width="499" border="0" ms_2d_layout="TRUE">
                                                    <tr valign="top">
                                                        <td width="5" height="133" background="images/dots.gif">
                                                            &nbsp;
                                                        </td>
                                                        <td width="487" height="133" background="images/reservBoxBkgd3.gif">
                                                            <table cellspacing="0" cellpadding="0" width="487" border="0">
                                                                <tr>
                                                                    <td align="right" colspan="7">
                                                                    </td>
                                                                </tr>
                                                                <tr height="22">
                                                                    <td width="12">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td class="style1">
                                                                        City:
                                                                    </td>
                                                                    <td colspan="2" align="left">
                                                                        <input type="text" name="B_LOCATION" value="City name" size="24"
                                                                            onfocus="clearField(this)">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="12">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td class="style1">
                                                                        Country:
                                                                    </td>
                                                                    <td colspan="2" align="left">
                                                                        <select name="COUNTRY_CODE">
                                                                            <option value="">Select country</option>
                                                                            <option value="AF">Afghanistan</option>
                                                                            <option value="AL">Albania</option>
                                                                            <option value="DZ">Algeria</option>
                                                                            <option value="AS">American Samoa</option>
                                                                            <option value="AD">Andorra</option>
                                                                            <option value="AO">Angola</option>
                                                                            <option value="AI">Anguilla</option>
                                                                            <option value="AQ">Antarctica</option>
                                                                            <option value="AG">Antigua &amp; Barbuda</option>
                                                                            <option value="AR">Argentina</option>
                                                                            <option value="AM">Armenia</option>
                                                                            <option value="AW">Aruba</option>
                                                                            <option value="AU">Australia</option>
                                                                            <option value="AT">Austria</option>
                                                                            <option value="AZ">Azerbaijan</option>
                                                                            <option value="BS">Bahamas</option>
                                                                            <option value="BH">Bahrain</option>
                                                                            <option value="BD">Bangladesh</option>
                                                                            <option value="BB">Barbados</option>
                                                                            <option value="BY">Belarus (Belorussia)</option>
                                                                            <option value="BE">Belgium</option>
                                                                            <option value="BZ">Belize</option>
                                                                            <option value="BJ">Benin</option>
                                                                            <option value="BM">Bermuda</option>
                                                                            <option value="BT">Bhutan</option>
                                                                            <option value="BO">Bolivia</option>
                                                                            <option value="BA">Bosnia-Herzegovina</option>
                                                                            <option value="BW">Botswana</option>
                                                                            <option value="BV">Bouvet Islands</option>
                                                                            <option value="BR">Brazil</option>
                                                                            <option value="IO">British Indian Ocean Territory</option>
                                                                            <option value="VG">British Virgin Islands</option>
                                                                            <option value="BN">Brunei Darussalam</option>
                                                                            <option value="BG">Bulgaria</option>
                                                                            <option value="BF">Burkina Faso</option>
                                                                            <option value="BI">Burundi</option>
                                                                            <option value="KH">Cambodia</option>
                                                                            <option value="CM">Cameroon</option>
                                                                            <option value="CA">Canada</option>
                                                                            <option value="CV">Cape Verde</option>
                                                                            <option value="KY">Cayman Islands</option>
                                                                            <option value="CF">Central African Republic</option>
                                                                            <option value="TD">Chad</option>
                                                                            <option value="CL">Chile</option>
                                                                            <option value="CN">China</option>
                                                                            <option value="CX">Christmas Islands</option>
                                                                            <option value="CC">Cocos (Keeling) Island</option>
                                                                            <option value="CO">Colombia</option>
                                                                            <option value="KM">Comoros</option>
                                                                            <option value="CG">Congo</option>
                                                                            <option value="CD">Congo (Rep. Dem.)</option>
                                                                            <option value="CK">Cook Islands</option>
                                                                            <option value="CR">Costa Rica</option>
                                                                            <option value="HR">Croatia</option>
                                                                            <option value="CU">Cuba</option>
                                                                            <option value="CY">Cyprus</option>
                                                                            <option value="CZ">Czech Republic</option>
                                                                            <option value="DK">Denmark</option>
                                                                            <option value="DJ">Djibouti</option>
                                                                            <option value="DO">Dominican Republic</option>
                                                                            <option value="DM">Dominicana</option>
                                                                            <option value="EC">Ecuador</option>
                                                                            <option value="EG">Egypt</option>
                                                                            <option value="SV">El Salvador</option>
                                                                            <option value="GQ">Equatorial Guinea</option>
                                                                            <option value="ER">Eritrea</option>
                                                                            <option value="EE">Estonia</option>
                                                                            <option value="ET">Ethiopia</option>
                                                                            <option value="FK">Falkland Islands</option>
                                                                            <option value="FO">Faroe Islands</option>
                                                                            <option value="FJ">Fiji Islands</option>
                                                                            <option value="FI">Finland</option>
                                                                            <option value="FR">France</option>
                                                                            <option value="GF">French Guiana</option>
                                                                            <option value="PF">French Polynesia</option>
                                                                            <option value="GA">Gabon</option>
                                                                            <option value="GM">Gambia</option>
                                                                            <option value="GE">Georgia</option>
                                                                            <option value="DE">Germany</option>
                                                                            <option value="GH">Ghana</option>
                                                                            <option value="GI">Gibraltar</option>
                                                                            <option value="GR">Greece</option>
                                                                            <option value="GL">Greenland</option>
                                                                            <option value="GD">Grenada</option>
                                                                            <option value="GP">Guadeloupe</option>
                                                                            <option value="GU">Guam</option>
                                                                            <option value="GT">Guatemala</option>
                                                                            <option value="GN">Guinea</option>
                                                                            <option value="GW">Guinea-Bissau</option>
                                                                            <option value="GY">Guyana</option>
                                                                            <option value="HT">Haiti</option>
                                                                            <option value="HM">Heard &amp; Mcdonald Islands</option>
                                                                            <option value="HN">Honduras</option>
                                                                            <option value="HK">Hongkong</option>
                                                                            <option value="HU">Hungary</option>
                                                                            <option value="IS">Iceland</option>
                                                                            <option value="IN">India</option>
                                                                            <option value="ID">Indonesia</option>
                                                                            <option value="IR">Iran</option>
                                                                            <option value="IQ">Iraq</option>
                                                                            <option value="IE">Ireland</option>
                                                                            <option value="IL">Israel</option>
                                                                            <option value="IT">Italy</option>
                                                                            <option value="CI">Ivory Coast</option>
                                                                            <option value="JM">Jamaica</option>
                                                                            <option value="JP">Japan</option>
                                                                            <option value="JO">Jordan</option>
                                                                            <option value="KZ">Kazakhstan</option>
                                                                            <option value="KE">Kenya</option>
                                                                            <option value="KI">Kiribati</option>
                                                                            <option value="KP">Korea (Democratic People&#039;s Republic Of)</option>
                                                                            <option value="KR">Korea (Republic Of)</option>
                                                                            <option value="KW">Kuwait</option>
                                                                            <option value="KG">Kyrgyzstan</option>
                                                                            <option value="LA">Lao People&#039;s Democratic Republic</option>
                                                                            <option value="LV">Latvia</option>
                                                                            <option value="LB">Lebanon</option>
                                                                            <option value="LS">Lesotho</option>
                                                                            <option value="LR">Liberia</option>
                                                                            <option value="LY">Libyan Arab Jamahiriya</option>
                                                                            <option value="LI">Liechtenstein</option>
                                                                            <option value="LT">Lithuania</option>
                                                                            <option value="LU">Luxembourg</option>
                                                                            <option value="MO">Macau</option>
                                                                            <option value="MK">Macedonia</option>
                                                                            <option value="MG">Madagascar</option>
                                                                            <option value="MW">Malawi</option>
                                                                            <option value="MY">Malaysia</option>
                                                                            <option value="MV">Maldives</option>
                                                                            <option value="ML">Mali</option>
                                                                            <option value="MT">Malta</option>
                                                                            <option value="MH">Marshall Islands</option>
                                                                            <option value="MQ">Martinique</option>
                                                                            <option value="MR">Mauritania</option>
                                                                            <option value="MU">Mauritius</option>
                                                                            <option value="YT">Mayotte</option>
                                                                            <option value="MX">Mexico</option>
                                                                            <option value="FM">Micronesia</option>
                                                                            <option value="MD">Moldova</option>
                                                                            <option value="MC">Monaco</option>
                                                                            <option value="MN">Mongolia</option>
                                                                            <option value="MS">Montserrat</option>
                                                                            <option value="MA">Morocco</option>
                                                                            <option value="MZ">Mozambique</option>
                                                                            <option value="MM">Myanmar</option>
                                                                            <option value="NA">Namibia</option>
                                                                            <option value="NR">Nauru</option>
                                                                            <option value="NP">Nepal</option>
                                                                            <option value="NL">Netherlands</option>
                                                                            <option value="AN">Netherlands Antilles</option>
                                                                            <option value="NC">New Caledonia</option>
                                                                            <option value="NZ">New Zealand</option>
                                                                            <option value="NI">Nicaragua</option>
                                                                            <option value="NE">Niger</option>
                                                                            <option value="NG">Nigeria</option>
                                                                            <option value="NU">Niue</option>
                                                                            <option value="NF">Norfolk Islands</option>
                                                                            <option value="MP">Northern Mariana Islands</option>
                                                                            <option value="NO">Norway</option>
                                                                            <option value="OM">Oman</option>
                                                                            <option value="PK">Pakistan</option>
                                                                            <option value="PW">Palau</option>
                                                                            <option value="PS">Palestinian Occ. Territories</option>
                                                                            <option value="PA">Panama</option>
                                                                            <option value="PG">Papua New Guinea</option>
                                                                            <option value="PY">Paraguay</option>
                                                                            <option value="PE">Peru</option>
                                                                            <option value="PH">Philippines</option>
                                                                            <option value="PL">Poland</option>
                                                                            <option value="PT">Portugal</option>
                                                                            <option value="PR">Puerto Rico</option>
                                                                            <option value="QA">Qatar</option>
                                                                            <option value="RE">Reunion</option>
                                                                            <option value="RO">Romania</option>
                                                                            <option value="RW">Ruanda</option>
                                                                            <option value="RU">Russian Federation</option>
                                                                            <option value="LC">Saint Lucia</option>
                                                                            <option value="WS">Samoa</option>
                                                                            <option value="SM">San Marino</option>
                                                                            <option value="ST">Sao Tome &amp; Principe</option>
                                                                            <option value="SA">Saudi Arabia</option>
                                                                            <option value="SN">Senegal</option>
                                                                            <option value="CS">Serbia Montenegro</option>
                                                                            <option value="SC">Seychelles</option>
                                                                            <option value="SL">Sierra Leone</option>
                                                                            <option value="SG">Singapore</option>
                                                                            <option value="SK">Slovakia</option>
                                                                            <option value="SI">Slovenia</option>
                                                                            <option value="SB">Solomon Islands</option>
                                                                            <option value="SO">Somalia</option>
                                                                            <option value="ZA">South Africa</option>
                                                                            <option value="ES">Spain</option>
                                                                            <option value="LK">Sri Lanka</option>
                                                                            <option value="SH">St. Helena</option>
                                                                            <option value="KN">St. Kitts and Nevis</option>
                                                                            <option value="PM">St. Pierre &amp; Miquelon</option>
                                                                            <option value="VC">St. Vincent &amp; the Grenadines</option>
                                                                            <option value="SD">Sudan</option>
                                                                            <option value="SR">Suriname</option>
                                                                            <option value="SJ">Svalbard &amp; Jan Mayen Islands</option>
                                                                            <option value="SZ">Swaziland</option>
                                                                            <option value="SE">Sweden</option>
                                                                            <option value="CH">Switzerland</option>
                                                                            <option value="SY">Syrian Arab Republic</option>
                                                                            <option value="TW">Taiwan</option>
                                                                            <option value="TJ">Tajikistan</option>
                                                                            <option value="TZ">Tanzania</option>
                                                                            <option value="TH">Thailand</option>
                                                                            <option value="TL">Timor Leste</option>
                                                                            <option value="TG">Togo</option>
                                                                            <option value="TK">Tokelau</option>
                                                                            <option value="TO">Tonga</option>
                                                                            <option value="TT">Trinidad and Tobago</option>
                                                                            <option value="TN">Tunisia</option>
                                                                            <option value="TR">Turkey</option>
                                                                            <option value="TM">Turkmenistan</option>
                                                                            <option value="TC">Turks &amp; Caicos Islands</option>
                                                                            <option value="TV">Tuvalu</option>
                                                                            <option value="UM">U.S. Minor Outlaying Islands</option>
                                                                            <option value="UG">Uganda</option>
                                                                            <option value="UA">Ukraine</option>
                                                                            <option value="AE">United Arab Emirates</option>
                                                                            <option value="GB">United Kingdom</option>
                                                                            <option value="UY">Uruguay</option>
                                                                            <option value="US" selected>USA</option>
                                                                            <option value="UZ">Uzbekistan</option>
                                                                            <option value="VU">Vanuatu</option>
                                                                            <option value="VA">Vatican City State</option>
                                                                            <option value="VE">Venezuela</option>
                                                                            <option value="VN">Viet Nam</option>
                                                                            <option value="VI">Virgin Islands (US)</option>
                                                                            <option value="WF">Wallis &amp; Futuna Islands</option>
                                                                            <option value="YE">Yemen</option>
                                                                            <option value="ZM">Zambia</option>
                                                                            <option value="ZW">Zimbabwe</option>
                                                                        </select>
                                                                    </td>
                                                                </tr>
                                                                <tr height="22">
                                                                    <td width="12">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td class="style1">
                                                                        Check-in Date:
                                                                    </td>
                                                                    <td colspan="2">
                                                                        <select name="B_MONTH" onchange="departing_date_selected = true;HotelChangeReturnDate()">

                                                                            <script type="text/javascript">
                                                                                writeMonth = HotelthisMonth;
                                                                                writeYear = HotelthisYear;
                                                                                for (n = 0; n < 10; n++) {
                                                                                    if (writeMonth < 10)
                                                                                        document.write('<option value="' + writeYear + '0' + writeMonth + '">' + months[writeMonth - 1] + ' ' + writeYear + '</option>');
                                                                                    else
                                                                                        document.write('<option value="' + writeYear + writeMonth + '">' + months[writeMonth - 1] + ' ' + writeYear + '</option>');
                                                                                    if (writeMonth == 12) {
                                                                                        writeYear++;
                                                                                        writeMonth = 1;
                                                                                    }
                                                                                    else
                                                                                        writeMonth++;
                                                                                }
                                                                            </script>

                                                                        </select>
                                                                        <select name="B_DAY" onchange="departing_date_selected = true;HotelChangeReturnDate()">

                                                                            <script type="text/javascript">
                                                                                var str;
                                                                                for (n = 1; n <= 31; n++) {
                                                                                    if (n < 10)
                                                                                        str = "0" + n;
                                                                                    else
                                                                                        str = n;
                                                                                    if (str == HotelthisDay)
                                                                                        document.write('<option selected value="' + str + '">' + str + '</option>');
                                                                                    else
                                                                                        document.write('<option value="' + str + '">' + str + '</option>');
                                                                                }
                                                                            </script>

                                                                        </select>
                                                                    </td>
                                                                </tr>
                                                                <tr height="22">
                                                                    <td width="12">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td class="style1">
                                                                        Check-out Date:
                                                                    </td>
                                                                    <td colspan="2">
                                                                        <select name="E_MONTH">

                                                                            <script type="text/javascript">
                                                                                writeMonth = HotelthisMonth;
                                                                                writeYear = HotelthisYear;
                                                                                for (n = 0; n < 10; n++) {
                                                                                    if (writeMonth < 10)
                                                                                        document.write('<option value="' + writeYear + '0' + writeMonth + '">' + months[writeMonth - 1] + ' ' + writeYear + '</option>');
                                                                                    else
                                                                                        document.write('<option value="' + writeYear + writeMonth + '">' + months[writeMonth - 1] + ' ' + writeYear + '</option>');
                                                                                    if (writeMonth == 12) {
                                                                                        writeYear++;
                                                                                        writeMonth = 1;
                                                                                    }
                                                                                    else
                                                                                        writeMonth++;
                                                                                }
                                                                            </script>

                                                                        </select>
                                                                        <select name="E_DAY">

                                                                            <script type="text/javascript">
                                                                                var str;
                                                                                for (n = 1; n <= 31; n++) {
                                                                                    if (n < 10)
                                                                                        str = "0" + n;
                                                                                    else
                                                                                        str = n;
                                                                                    if (str == HotelthisDay)
                                                                                        document.write('<option selected value="' + str + '">' + str + '</option>');
                                                                                    else
                                                                                        document.write('<option value="' + str + '">' + str + '</option>');
                                                                                }
                                                                            </script>

                                                                        </select>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="12">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td class="style1">
                                                                        Number of guests:
                                                                    </td>
                                                                    <td colspan="2">
                                                                        <select name="OCCUPANCY">
                                                                            <option selected="selected" value="1">1</option>
                                                                            <option value="2">2 or more</option>
                                                                        </select>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4" align="right" valign="bottom">
                                                                        <input name="image" type="image" style="{border: 0px; }" tabindex="17" src="/TravelerPlus/owner/images/search_btn.gif"
                                                                            alt="Search" width="40" height="20" border="0" target="_top" />&nbsp;&nbsp;&nbsp;
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td width="8" height="133" background="images/dotsright.gif">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" valign="top">
                                                            <img src="/TravelerPlus/owner/images/leftdots.gif" align="left" alt="" /><img src="/TravelerPlus/owner/images/rightdots.gif" align="right" alt="" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </asp:Panel>
                                        </form>
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td colspan="3">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <table class="barTitle" cellspacing="0" cellpadding="0" width="100%" background="../images/smBarBkgd.gif"
                                            border="0">
                                            <tr>
                                                <td width="100%" height="15" align="center">FEATURED SPECIALS
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" width="240">
                                        <includeSpecials:Specials ID="Specials1" runat="server"></includeSpecials:Specials>
                                    </td>
                                    <td>
                                        <img src="images/blank.gif" width="9" alt="" />
                                    </td>
                                    <td valign="top">
                                        <includeFeatures:Features ID="Features1" runat="server"></includeFeatures:Features>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <!-- footer -->
                                        <includeControlFooter:footer ID="footer" runat="server"></includeControlFooter:footer>
                                        <!-- end footer -->
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <script type="text/javascript"> 
<!--
        function init() {
            departing_date_selected = true;
            ChangeReturnDate();
        }
//-->
    </script>

</body>
</html>
