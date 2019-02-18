<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CustomerProfile.aspx.vb" Inherits="VSSA.CustomerProfile" %>

<html>
<head>
<title>IVR Customer Profile</title>
<meta http-equiv="X-UA-Compatible" content="IE=edge" />
</head>
<body>

<script>
    Element.prototype.remove = function () {
        this.parentElement.removeChild(this);
    }
    NodeList.prototype.remove = HTMLCollection.prototype.remove = function () {
        for (var i = this.length - 1; i >= 0; i--) {
            if (this[i] && this[i].parentElement) {
                this[i].parentElement.removeChild(this[i]);
            }
        }
    }




    var iframe = document.createElement('iframe');
    var currentUrl = window.location.href;
    var newURL = currentUrl.replace('CustomerProfile', 'CustomerProfileMain');
    var timeoutMilliseconds = 10000;
    var redirectFunction = iFrameTimedout;
    var timerVar = setTimeout(redirectFunction, timeoutMilliseconds);
    iframe.onload = iFrameLoaded;
    timeoutURL = currentUrl.replace('CustomerProfile', 'SearchCustomer');
    iframe.width = '100%';
    iframe.height = '768px';
    iframe.src = newURL;
    iframe.id = 'customerMainIFrame';
    document.body.appendChild(iframe);

    function iFrameLoaded() {
        clearTimeout(timerVar);
        autoResize('customerMainIFrame');
    }

    function iFrameTimedout() {
        //document.removeChild('customerMainIFrame');

        document.getElementById('customerMainIFrame').remove();
        alert("Page load has exceeded allowed timeout");
        var indexOfKey = timeoutURL.search("UUI=");
        var newRedirectURL = timeoutURL.substring(0, indexOfKey + 4) + "________________" + timeoutURL.substring(indexOfKey + 20, 9999);


        var newTimedOutURL = timeoutURL.indexOf()

        //newRedirectURL = newRedirectURL.replace('stg.vssa','vssa');
        window.location = newRedirectURL;
    }

    function autoResize(id) {
        var newheight;
        var newwidth;
        if (document.getElementById) {
            newheight = document.getElementById(id).contentWindow.document.body.scrollHeight;
            newwidth = document.getElementById(id).contentWindow.document.body.scrollWidth;
        }
        document.getElementById(id).height = (newheight) + "px";
        document.getElementById(id).width = (newwidth) + "px";
    }

</script>
</body>
</html>

