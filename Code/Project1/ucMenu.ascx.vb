Public Class ucMenu
    Inherits System.Web.UI.UserControl

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents menuBar As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents subMenuBar As System.Web.UI.HtmlControls.HtmlGenericControl

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
		InitializeComponent()
    End Sub

#End Region
	Public m_ForeColor As String = ""
	Public m_BGColor As String = ""
	Public m_BGMainColor As String = ""
	Private m_iStartLeft As Integer = 0
	Private m_iStartTop As Integer = 0
	Private m_iMenuWidth As Integer = 110
    Private m_sSubMenu As String '= "<label isMenu=""1"" onmouseover=""Highlight(event,this,#MENUTYPE#)"" onmouseout=""UnHighlight(event,this,#MENUTYPE#)"" #TITLE# style=""position:absolute;Font-Family:Verdana;Font-Size:7.2pt;letter-spacing:-1;line-height:88%;left:#LP#px;top:#TP#px;text-align:left;width:#MENU_WIDTH#px;height:18px;background-Color:#BGCOLOR#;"" #REFID# onclick=""#FUNC#('#MENUCOMMAND#')"">#IMG##MENULABEL#</label>"
    Private m_sDivMenu As String = "<div id=""#ID#"" isMenu=""1"" style=""visibility:hidden;position:absolute;left:#LP#px;top:#TP#px;width:190px;"">" & vbCrLf & "%s" & "</div>"
    Private m_arrSubMenu() As System.Web.UI.HtmlControls.HtmlGenericControl
    Private m_arrMenuItems() As cMenuObj
    Const mnuWidth = "#MENU_WIDTH#"
    Const mnuType = "#MENUTYPE#"
	Const mnuRefID = "#REFID#"
    Const mnuID = "#ID#"
    Const mnuLP = "#LP#"
    Const mnuimg = "#IMG#"
    Const mnuTP = "#TP#"
    Const mnuTitle = "#TITLE#"
    Const mnuBGColor = "#BGCOLOR#"
    Const mnuForeColor = "#FORECOLOR#"
    Const mnuCommand = "#MENUCOMMAND#"
    Const mnuLabel = "#MENULABEL#"
    Const mnuFunc = "#FUNC#"
	Private Sub InitMenuLabel()
		Dim bc As HttpBrowserCapabilities
		bc = Request.Browser
		Select Case bc.Browser
			Case "MOZILLA", "FIREFOX", "NETSCAPE", "Gecko"
                m_sSubMenu = "<label isMenu=""1"" onmouseover=""Highlight(event,this,#MENUTYPE#)"" onmouseout=""UnHighlight(event,this,#MENUTYPE#)"" #TITLE# style=""position:absolute;Font-Family:Verdana;Font-Size:7.2pt;font-stretch:extra-condensed;line-height:88%;left:#LP#px;top:#TP#px;text-align:left;width:#MENU_WIDTH#px;height:18px;background-Color:#BGCOLOR#;"" #REFID# onclick=""#FUNC#('#MENUCOMMAND#')"">#IMG##MENULABEL#</label>"
			Case "OPERA"
                m_sSubMenu = "<label isMenu=""1"" onmouseover=""Highlight(event,this,#MENUTYPE#)"" onmouseout=""UnHighlight(event,this,#MENUTYPE#)"" #TITLE# style=""position:absolute;Font-Family:Verdana;Font-Size:7.2pt;letter-spacing:-1;line-height:88%;left:#LP#px;top:#TP#px;text-align:left;width:#MENU_WIDTH#px;height:18px;background-Color:#BGCOLOR#;"" #REFID# onclick=""#FUNC#('#MENUCOMMAND#')"">#IMG##MENULABEL#</label>"
			Case "SAFARI"
                m_sSubMenu = "<label isMenu=""1"" onmouseover=""Highlight(event,this,#MENUTYPE#)"" onmouseout=""UnHighlight(event,this,#MENUTYPE#)"" #TITLE# style=""position:absolute;Font-Family:Verdana;Font-Size:7.2pt;letter-spacing:-2;line-height:88%;left:#LP#px;top:#TP#px;text-align:left;width:#MENU_WIDTH#px;height:18px;background-Color:#BGCOLOR#;"" #REFID# onclick=""#FUNC#('#MENUCOMMAND#')"">#IMG##MENULABEL#</label>"
			Case Else
                m_sSubMenu = "<label isMenu=""1"" onmouseover=""Highlight(event,this,#MENUTYPE#)"" onmouseout=""UnHighlight(event,this,#MENUTYPE#)"" #TITLE# style=""position:absolute;Font-Family:Verdana;Font-Size:7.2pt;letter-spacing:-1;line-height:88%;left:#LP#px;top:#TP#px;text-align:left;width:#MENU_WIDTH#px;height:18px;background-Color:#BGCOLOR#;"" #REFID# onclick=""#FUNC#('#MENUCOMMAND#')"">#IMG##MENULABEL#</label>"
		End Select
	End Sub
	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		If (Page.IsPostBack) Then
			m_BGColor = ViewState("m_BGColor")
			m_BGMainColor = ViewState("m_BGMainColor")
			m_ForeColor = ViewState("m_ForeColor")
			'm_iStartLeft = ViewState("m_iStartLeft")
			'm_iStartTop = ViewState("m_iStartTop")
			menuBar.Style.Clear()
		End If
	End Sub
	Public Sub FinalizeMenus()
		If (IsNothing(m_arrMenuItems)) Then Exit Sub
		If (Not IsNothing(m_iStartLeft)) Then
			If (IsNumeric(m_iStartLeft)) Then
				menuBar.Style.Add("left", m_iStartLeft & "px")
				menuBar.Style.Add("position", "absolute")
			End If
		End If
		If (Not IsNothing(m_iStartTop)) Then
			If (IsNumeric(m_iStartTop)) Then
				menuBar.Style.Add("top", m_iStartTop & "px")
			End If
		End If
		'CHECK TO SEE THAT INDEX IS WITHIN SUB MENU BOUNDS
		Dim I, J As Integer
		For I = 0 To m_arrMenuItems.GetLength(0) - 1
			Dim subMenu As cMenuObj = m_arrMenuItems(I)
			If (Not IsNothing(subMenu)) Then
				Dim sDiv As String = CreateDiv(subMenu.sId, (I * m_iMenuWidth), 18)
				sDiv = sDiv.Replace("%s", "")
				sDiv = sDiv.Replace("</div>", "")
				subMenuBar.Controls.Add(New LiteralControl(sDiv))
				If (Not IsNothing(subMenu.sElements)) Then
					For J = 0 To subMenu.sElements.GetLength(0) - 1
						subMenuBar.Controls.Add(New LiteralControl(vbTab))
						subMenuBar.Controls.Add(New LiteralControl(subMenu.sElements(J)))
						subMenuBar.Controls.Add(New LiteralControl(vbCrLf))
					Next
				End If
				subMenuBar.Controls.Add(New LiteralControl("</div>"))
				subMenuBar.Controls.Add(New LiteralControl(vbCrLf))
			End If
		Next
	End Sub
	Public Sub CreateTopLabels(ByVal arrMenuItems As Object, ByVal sBGColor As String, ByVal sForeColor As String)
		InitMenuLabel()
		If (Not IsNothing(arrMenuItems)) Then
			Dim I, J As Integer
			Dim tmpStr As String
			Dim Left As Integer = 0
			Dim Top As Integer = 0
			J = arrMenuItems.Length - 1
			ReDim m_arrMenuItems(J)
			For I = 0 To J
				tmpStr = m_sSubMenu.Replace(mnuLP, Left)
				tmpStr = tmpStr.Replace(mnuTitle, "")
				tmpStr = tmpStr.Replace(mnuimg, "")
				tmpStr = tmpStr.Replace(mnuType, 1)
				tmpStr = tmpStr.Replace(mnuTP, Top)
				tmpStr = tmpStr.Replace(mnuBGColor, sBGColor)
				tmpStr = tmpStr.Replace(mnuWidth, m_iMenuWidth)
				Dim sToken As String() = Split(arrMenuItems(I), ",")
				If (IsArray(sToken)) Then
					Dim sLabel As String = sToken(0)
					If (IsNothing(sLabel)) Then sLabel = ""
					If (sLabel.Length > 30 And sLabel.IndexOf("&") = -1) Then
						tmpStr = tmpStr.Replace(mnuLabel, sLabel.Substring(0, 30) & "...")
					Else
						tmpStr = tmpStr.Replace(mnuLabel, sLabel)
					End If
					tmpStr = tmpStr.Replace(mnuTitle, "title=""" & sLabel & """")
					If (sToken.Length >= 2) Then
						If (sToken(1).StartsWith("#")) Then
							Dim mnuObj As New cMenuObj
							Dim s As String = sToken(1)
							s = s.Replace(" ", "")
							s = s.Replace("#", "")
							mnuObj.iLeft = Left
							mnuObj.iTop = Top
							mnuObj.sId = Me.UniqueID & "_" & s
							tmpStr = tmpStr.Replace(mnuRefID, "menuid=" & Me.UniqueID & "_" & s)
							If (Not IsNothing(sToken(2))) Then
								tmpStr = tmpStr.Replace(mnuCommand, sToken(2))
								tmpStr = tmpStr.Replace(mnuFunc, "onCommand")
							Else
								tmpStr = tmpStr.Replace(mnuCommand, Me.UniqueID & "_" & s)
								tmpStr = tmpStr.Replace(mnuFunc, "ShowMenu")
							End If
							m_arrMenuItems(I) = mnuObj
						Else
							tmpStr = tmpStr.Replace(mnuRefID, "")
							tmpStr = tmpStr.Replace(mnuCommand, sToken(1))
							tmpStr = tmpStr.Replace(mnuFunc, "onCommand")
							m_arrMenuItems(I) = Nothing
						End If
					End If
				End If
				Left += m_iMenuWidth
				menuBar.Controls.Add(New LiteralControl(tmpStr & vbCrLf))
			Next
		End If

	End Sub
	Private Function CreateDiv(ByVal sId As String, ByVal iLeft As Integer, ByVal iTop As Integer) As String
		Dim divCtrl As String = m_sDivMenu
		divCtrl = divCtrl.Replace(mnuID, sId)
		divCtrl = divCtrl.Replace(mnuLP, iLeft)
		divCtrl = divCtrl.Replace(mnuTP, iTop)
		Return divCtrl
	End Function
	Public Function CreateSubDiv(ByVal iIdx As Integer, ByVal arrMenuItems As Object, ByVal sBGColor As String, ByVal sForeColor As String) As Integer()
		Dim arrSubMenu(0) As Integer
		'CHECK TO SEE THAT THERE ARE SUB MENU ITEMS
		If (IsNothing(m_arrMenuItems)) Then Exit Function
		'CHECK TO SEE THAT INDEX IS WITHIN SUB MENU BOUNDS
		If (iIdx > m_arrMenuItems.GetLength(0)) Then Exit Function
		Dim subMenu As cMenuObj = m_arrMenuItems(iIdx)

		If (Not IsNothing(arrMenuItems) And Not IsNothing(subMenu)) Then
			Dim I, J As Integer
			Dim tmpStr As String
			Dim Left As Integer = 0
			Left = m_iMenuWidth * iIdx
			Dim Top As Integer = 0
			For I = 0 To arrMenuItems.Length - 1
				tmpStr = m_sSubMenu.Replace(mnuLP, 0)
				tmpStr = tmpStr.Replace(mnuType, 0)
				tmpStr = tmpStr.Replace(mnuTP, Top)
				tmpStr = tmpStr.Replace(mnuBGColor, sBGColor)
				tmpStr = tmpStr.Replace(mnuWidth, 190)
				Dim sToken = Split(arrMenuItems(I), ",")
				If (IsArray(sToken)) Then
					Dim sLabel As String = sToken(0)
					If (IsNothing(sLabel)) Then sLabel = ""
					If (sLabel.Length > 25 And sLabel.IndexOf("&") = -1) Then
						tmpStr = tmpStr.Replace(mnuLabel, sLabel.Substring(0, 25) & "...")
					Else
						tmpStr = tmpStr.Replace(mnuLabel, sLabel)
					End If
					tmpStr = tmpStr.Replace(mnuTitle, "title=""" & sLabel & """")
					If (sToken.Length >= 2) Then
						If (sToken(1).StartsWith("#")) Then
							Dim s As String = sToken(1)
							s = s.Replace(" ", "")
							s = s.Replace("#", "")
							tmpStr = tmpStr.Replace(mnuLabel, sToken(0))
							tmpStr = tmpStr.Replace(mnuRefID, "menuid=" & Me.UniqueID & "_" & s)
							If (Not IsNothing(sToken(2))) Then
								tmpStr = tmpStr.Replace(mnuCommand, sToken(2))
								tmpStr = tmpStr.Replace(mnuFunc, "onCommand")
							Else
								tmpStr = tmpStr.Replace(mnuCommand, Me.UniqueID & "_" & s)
								tmpStr = tmpStr.Replace(mnuFunc, "ShowMenu")
							End If
							tmpStr = tmpStr.Replace(mnuimg, "<img align=absmiddle src=images/Submnu.gif>&nbsp;&nbsp;")
							ReDim Preserve arrSubMenu(J)
							arrSubMenu(J) = I
							ReDim Preserve subMenu.sElements(I)
							subMenu.sElements(I) = tmpStr & vbCrLf & CreateDiv(Me.UniqueID & "_" & s, 190, 0)
							J += 1
						Else
							tmpStr = tmpStr.Replace(mnuLabel, sToken(0))
							tmpStr = tmpStr.Replace(mnuRefID, "")
							tmpStr = tmpStr.Replace(mnuCommand, sToken(1))
							tmpStr = tmpStr.Replace(mnuFunc, "onCommand")
							tmpStr = tmpStr.Replace(mnuimg, "")
							ReDim Preserve subMenu.sElements(I)
							subMenu.sElements(I) = tmpStr
						End If
					End If
					Top += 18
				End If
			Next
			m_arrMenuItems(iIdx) = subMenu
		End If
		Return arrSubMenu
	End Function
	Public Sub CreateSideDiv(ByVal iIdx As Integer, ByVal subIdx As Integer, ByVal arrMenuItems As Object, ByVal sBGColor As String, ByVal sForeColor As String)
		Dim subMenu As cMenuObj = m_arrMenuItems(iIdx)
		If (Not IsNothing(arrMenuItems) And Not IsNothing(subMenu)) Then
			Dim subDiv As String
			If (IsNothing(subMenu.sElements(subIdx))) Then
				Exit Sub
			Else
				subDiv = subMenu.sElements(subIdx)
			End If

			Dim I As Integer
			Dim tmpStr As String
            Dim finalStr As String = ""
			Dim Left As Integer = 0
			Dim Top As Integer = 0
			For I = 0 To arrMenuItems.Length - 1
				tmpStr = m_sSubMenu.Replace(mnuLP, Left)
				tmpStr = tmpStr.Replace(mnuimg, "")
				tmpStr = tmpStr.Replace(mnuType, 0)
				tmpStr = tmpStr.Replace(mnuTP, Top)
				tmpStr = tmpStr.Replace(mnuBGColor, sBGColor)
				tmpStr = tmpStr.Replace(mnuWidth, 190)
				Dim sToken = Split(arrMenuItems(I), ",")
				If (IsArray(sToken)) Then
					If (sToken.Length = 2) Then
						tmpStr = tmpStr.Replace(mnuLabel, sToken(0))
						tmpStr = tmpStr.Replace(mnuRefID, "")
						tmpStr = tmpStr.Replace(mnuCommand, sToken(1))
						tmpStr = tmpStr.Replace(mnuFunc, "onCommand")
						finalStr += tmpStr + vbCrLf
						Top += 18
					End If
				End If
			Next
			subMenu.sElements(subIdx) = subDiv.Replace("%s", finalStr)
			m_arrMenuItems(iIdx) = subMenu
		End If
	End Sub
	Property SetMainBGColor()
		Get
			Return m_BGMainColor
		End Get
		Set(ByVal Value)
			m_BGMainColor = Value
			ViewState("m_BGMainColor") = m_BGMainColor
		End Set
	End Property
	Property SetBGColor()
		Get
			Return m_BGColor
		End Get
		Set(ByVal Value)
			m_BGColor = Value
			ViewState("m_BGColor") = m_BGColor
		End Set
	End Property
	Property SetForeColor()
		Get
			Return m_ForeColor
		End Get
		Set(ByVal Value)
			m_ForeColor = Value
			ViewState("m_ForeColor") = m_ForeColor

		End Set
	End Property
	Property SetStartLeft()
		Get
			Return m_iStartLeft
		End Get
		Set(ByVal Value)
			m_iStartLeft = Value
			ViewState("m_iStartLeft") = m_iStartLeft
		End Set
	End Property
	Property SetStartTop()
		Get
			Return m_iStartTop
		End Get
		Set(ByVal Value)
			m_iStartTop = Value
			ViewState("m_iStartTop") = m_iStartTop
		End Set
	End Property
	Property SetMenuWidth()
		Get
			Return m_iMenuWidth
		End Get
		Set(ByVal Value)
			m_iMenuWidth = Value
			ViewState("m_iMenuWidth") = m_iMenuWidth
		End Set
	End Property
	Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.PreRender
	End Sub
End Class
Class cMenuObj
	Public sElements() As String
	Public sId As String
	Public iTop As Integer
	Public iLeft As Integer
	Public iType As Integer
End Class