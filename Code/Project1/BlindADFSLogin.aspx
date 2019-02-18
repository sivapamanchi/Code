﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BlindADFSLogin.aspx.vb" Inherits="BGO.BlindADFSLogin" %>

<script type="text/javascript">
    //<![CDATA[
    function LoginErrors() { this.userNameFormatError = 'Enter your user ID in the format \u0026quot;domain\\user\u0026quot; or \u0026quot;user@domain\u0026quot;.'; this.passwordEmpty = 'Enter your password.'; }

    function InputUtil(errTextElementID, errDisplayElementID) {

        if (!errTextElementID) errTextElementID = 'errorText';
        if (!errDisplayElementID) errDisplayElementID = 'error';

        this.hasFocus = false;
        this.errLabel = document.getElementById(errTextElementID);
        this.errDisplay = document.getElementById(errDisplayElementID);
    };
    InputUtil.prototype.canDisplayError = function () {
        return this.errLabel && this.errDisplay;
    }
    InputUtil.prototype.checkError = function () {
        if (!this.canDisplayError) {
            throw new Error('Error element not present');
        }
        if (this.errLabel && this.errLabel.innerHTML) {
            this.errDisplay.style.display = '';
            var cause = this.errLabel.getAttribute('for');
            if (cause) {
                var causeNode = document.getElementById(cause);
                if (causeNode && causeNode.value) {
                    causeNode.focus();
                    this.hasFocus = true;
                }
            }
        }
        else {
            this.errDisplay.style.display = 'none';
        }
    };
    InputUtil.prototype.setInitialFocus = function (input) {
        if (this.hasFocus) return;
        var node = document.getElementById(input);
        if (node) {
            if ((/^\s*$/).test(node.value)) {
                node.focus();
                this.hasFocus = true;
            }
        }
    };
    InputUtil.prototype.setError = function (input, errorMsg) {
        if (!this.canDisplayError) {
            throw new Error('Error element not present');
        }
        input.focus();

        if (errorMsg) {
            this.errLabel.innerHTML = errorMsg;
        }
        this.errLabel.setAttribute('for', input.id);
        this.errDisplay.style.display = '';
    };
    InputUtil.makePlaceholder = function (input) {
        var ua = navigator.userAgent;

        if (ua != null &&
            (ua.match(/MSIE 9.0/) != null ||
                ua.match(/MSIE 8.0/) != null ||
                ua.match(/MSIE 7.0/) != null)) {
            var node = document.getElementById(input);
            if (node) {
                var placeholder = node.getAttribute("placeholder");
                if (placeholder != null && placeholder != '') {
                    var label = document.createElement('input');
                    label.type = "text";
                    label.value = placeholder;
                    label.readOnly = true;
                    label.style.position = 'absolute';
                    label.style.borderColor = 'transparent';
                    label.className = node.className + ' hint';
                    label.tabIndex = -1;
                    label.onfocus = function () { this.nextSibling.focus(); };

                    node.style.position = 'relative';
                    node.parentNode.style.position = 'relative';
                    node.parentNode.insertBefore(label, node);
                    node.onkeyup = function () { InputUtil.showHint(this); };
                    node.onblur = function () { InputUtil.showHint(this); };
                    node.style.background = 'transparent';

                    node.setAttribute("placeholder", "");
                    InputUtil.showHint(node);
                }
            }
        }
    };
    InputUtil.focus = function (inputField) {
        var node = document.getElementById(inputField);
        if (node) node.focus();
    };
    InputUtil.hasClass = function (node, clsName) {
        return node.className.match(new RegExp('(\\s|^)' + clsName + '(\\s|$)'));
    };
    InputUtil.addClass = function (node, clsName) {
        if (!this.hasClass(node, clsName)) node.className += " " + clsName;
    };
    InputUtil.removeClass = function (node, clsName) {
        if (this.hasClass(node, clsName)) {
            var reg = new RegExp('(\\s|^)' + clsName + '(\\s|$)');
            node.className = node.className.replace(reg, ' ');
        }
    };
    InputUtil.showHint = function (node, gotFocus) {
        if (node.value && node.value != '') {
            node.previousSibling.style.display = 'none';
        }
        else {
            node.previousSibling.style.display = '';
        }
    };

    function Login() {
    }

    Login.userNameInput = 'userNameInput';
    Login.passwordInput = 'passwordInput';

    Login.initialize = function () {

        var u = new InputUtil();

        u.checkError();
        u.setInitialFocus(Login.userNameInput);
        u.setInitialFocus(Login.passwordInput);
    } ();

    Login.submitLoginRequest = function () {
        var u = new InputUtil();
        //var e = new LoginErrors();

        //var userName = document.getElementById(Login.userNameInput);
        //var password = document.getElementById(Login.passwordInput);

        //if (!userName.value || !userName.value.match('[@\\\\]')) {
        //    u.setError(userName, e.userNameFormatError);
        //    return false;
        //}

        //if (!password.value) {
        //    u.setError(password, e.passwordEmpty);
        //    return false;
        //}

        document.forms['loginForm'].submit();
        return false;
    };

    InputUtil.makePlaceholder(Login.userNameInput);
    InputUtil.makePlaceholder(Login.passwordInput);
    </script>

<script type="text/javascript">
//<![CDATA[
    // Copyright (c) Microsoft Corporation.  All rights reserved.

    // This file contains several workarounds on inconsistent browser behaviors that administrators may customize.
    "use strict";

    // iPhone email friendly keyboard does not include "\" key, use regular keyboard instead.
    // Note change input type does not work on all versions of all browsers.
    if (navigator.userAgent.match(/iPhone/i) != null) {
        var emails = document.querySelectorAll("input[type='email']");
        if (emails) {
            for (var i = 0; i < emails.length; i++) {
                emails[i].type = 'text';
            }
        }
    }

    // In the CSS file we set the ms-viewport to be consistent with the device dimensions, 
    // which is necessary for correct functionality of immersive IE. 
    // However, for Windows 8 phone we need to reset the ms-viewport's dimension to its original
    // values (auto), otherwise the viewport dimensions will be wrong for Windows 8 phone.
    // Windows 8 phone has agent string 'IEMobile 10.0'
    if (navigator.userAgent.match(/IEMobile\/10\.0/)) {
        var msViewportStyle = document.createElement("style");
        msViewportStyle.appendChild(
            document.createTextNode(
                "@-ms-viewport{width:auto!important}"
            )
        );
        msViewportStyle.appendChild(
            document.createTextNode(
                "@-ms-viewport{height:auto!important}"
            )
        );
        document.getElementsByTagName("head")[0].appendChild(msViewportStyle);
    }

    // If the innerWidth is defined, use it as the viewport width.
    if (window.innerWidth && window.outerWidth && window.innerWidth !== window.outerWidth) {
        var viewport = document.querySelector("meta[name=viewport]");
        viewport.setAttribute('content', 'width=' + window.innerWidth + 'px; initial-scale=1.0; maximum-scale=1.0');
    }

    // Gets the current style of a specific property for a specific element.
    function getStyle(element, styleProp) {
        var propStyle = null;

        if (element && element.currentStyle) {
            propStyle = element.currentStyle[styleProp];
        }
        else if (element && window.getComputedStyle) {
            propStyle = document.defaultView.getComputedStyle(element, null).getPropertyValue(styleProp);
        }

        return propStyle;
    }

    // The script below is used for downloading the illustration image 
    // only when the branding is displaying. This script work together
    // with the code in PageBase.cs that sets the html inline style
    // containing the class 'illustrationClass' with the background image.
    var computeLoadIllustration = function () {
        var branding = document.getElementById("branding");
        var brandingDisplay = getStyle(branding, "display");
        var brandingWrapperDisplay = getStyle(document.getElementById("brandingWrapper"), "display");

        if (brandingDisplay && brandingDisplay !== "none" &&
            brandingWrapperDisplay && brandingWrapperDisplay !== "none") {
            var newClass = "illustrationClass";

            if (branding.classList && branding.classList.add) {
                branding.classList.add(newClass);
            } else if (branding.className !== undefined) {
                branding.className += " " + newClass;
            }
            if (window.removeEventListener) {
                window.removeEventListener('load', computeLoadIllustration, false);
                window.removeEventListener('resize', computeLoadIllustration, false);
            }
            else if (window.detachEvent) {
                window.detachEvent('onload', computeLoadIllustration);
                window.detachEvent('onresize', computeLoadIllustration);
            }
        }
    };

    if (window.addEventListener) {
        window.addEventListener('resize', computeLoadIllustration, false);
        window.addEventListener('load', computeLoadIllustration, false);
    }
    else if (window.attachEvent) {
        window.attachEvent('onresize', computeLoadIllustration);
        window.attachEvent('onload', computeLoadIllustration);
    }
    //]]>
</script>