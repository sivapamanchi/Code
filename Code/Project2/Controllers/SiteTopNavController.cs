using BGModern.HtmlExtensions;
using BGModern.Mappers;
using BGModern.Models;
using BGO.BluegreenOnline;
using BGO.OwnerWS;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.Mvc;

namespace BGModern.Controllers
{
    public enum otMenuType
    {
        mnuAlone = 0,
        mnuSubMenu = 1
    };

    public enum otDisplayType
    {
        mnuText = 0,
        mnuImage = 1
    };
    public enum otNavType
    {
        locationNav = 0,
        popupNav = 1
    };

    // TODO: refactor this to Umbraco content
    public class Travelerpluslevel
    {
        //Constants for TravelerPlus Tiers
        public const String None  = "";
        public const String TravelerPlusLevel = "1";
        public const String TravelerPlusPlusLevel = "2";
        public const String TravelerPlus3 = "3";
    }

    public class cInitMenu
    {
		public otMenuType mnuType;
		public otDisplayType mnuDisplay;
		public otNavType mnuNav;
		public String sTitle;
		public String sCommand;
		public String sUrl;
		public String sOverImg;
		public String sOutImg;
		public int iWidth;
		public int iHeight;
    }


    /// <summary>
    /// This class is responsible for building the top navigation menu. It takes into account the current owner's type and will include only the menu items that owner type is permitted to view.
    /// </summary>
    public class SiteTopNavController : SurfaceController
    {
        #region Constants
        const String mnuWidth = "#MENU_WIDTH#";
        const String mnuType = "#MENUTYPE#";
        const String mnuRefID = "#REFID#";
        const String mnuID = "#ID#";
        const String mnuLP = "#LP#";
        const String mnuImg = "#IMG#";
        const String mnuFontSize = "#FONTSIZE#";
        const String mnuTP = "#TP#";
        const String mnuTitle = "#TITLE#";
        const String mnuHeight = "#MENUHEIGHT#";
        const String mnuBGColor = "#BGCOLOR#";
        const String mnuForeColor = "#FORECOLOR#";
        const String mnuCommand = "#MENUCOMMAND#";
        const String mnuLabel = "#MENULABEL#";
        const String mnuFunc = "#FUNC#";
        const String mnuPopWidth = "#WIDTH#";
        const String mnuPopHeight = "#HEIGHT#";
        const String mnuPopMaker = ",#WIDTH#,#HEIGHT#";
        const String mnuSubMenuWidth = "#SUBMENU_WIDTH#";
        const String mnuEventAndTitle = "isMenu=\"1\" onmouseover=\"Highlight(event,this,#MENUTYPE#)\" onmouseout=\"UnHighlight(event,this,#MENUTYPE#)\" #TITLE# ";
        const String mnuSpecial = "#SPECIAL_STUFF#";
        #endregion

        #region Variables
        public String srvRoot;
        public Boolean tpnav;
        public Boolean isEncoreRewards;
        public String sSSLURL = System.Configuration.ConfigurationManager.AppSettings["bxgwebSecureURL"];
        public Boolean TPexpired;
        public String sNavType = "owner";
        //public String homeproject;
        public BGO.OwnerWS.Owner bxgOwner = null;
        public Boolean MktCampaign;
        public int TrvelerPlusLevel;
        public String BWGift;
        public Boolean InstalPayEligible;
        public String InstallStatus;
        //public String ShowGuestProfile;
        //public Boolean TPRenewLink;
        public Boolean bTravelerPlusPlus;
        private String m_sDivMenu = "<div id=\"#ID#\" isMenu=\"1\" style=\"visibility:hidden;position:absolute;left:#LP#px;top:#TP#px;width:#SUBMENU_WIDTH#px;\">" + System.Environment.NewLine + "%s" + "</div>";
        private System.Web.UI.HtmlControls.HtmlGenericControl[] m_arrSubMenu;
        private cMenuObj[] m_arrMenuItems;
        private String m_BGMainColor;
        private String m_BGColor;
        private String m_ForeColor;
        private int m_iStartLeft = 5;
        private int m_iStartTop = 10;
        private int m_iMenuWidth = 0;
        private int m_iMenuHeight = 22; 
        private SiteTopNavModel model = null;
        #endregion

        #region Properties
        public String SetMainBGColor
        {
            get { return m_BGMainColor; }

            set
            {
                m_BGMainColor = value;
                //ViewState("m_BGMainColor") = m_BGMainColor}
            }
        }

        public String SetBGColor
        {
            get { return m_BGColor; }

            set
            {
                m_BGColor = value;
                //ViewState("m_BGColor") = m_BGColor
            }
        }

        public String SetForeColor
        {
            get { return m_ForeColor; }

            set
            {
                m_ForeColor = value;
                //ViewState("m_ForeColor") = m_ForeColor
            }
        }

        public int SetStartLeft
        {
            get { return m_iStartLeft; }

            set
            {
                m_iStartLeft = value;
                //ViewState("m_iStartLeft") = m_iStartLeft
            }
        }

        public int SetStartTop
        {
            get { return m_iStartTop; }

            set
            {
                m_iStartTop = value;
                //ViewState("m_iStartTop") = m_iStartTop
            }
        }

        public int SetMenuWidth
        {
            get { return m_iMenuWidth; }

            set
            {
                m_iMenuWidth = value;
                //ViewState("m_iMenuWidth") = m_iMenuWidth
            }
        }

        public int SetMenuHeight
        {
            get { return m_iMenuHeight; }

            set
            {
                m_iMenuHeight = value;
                //ViewState("m_iMenuHeight") = m_iMenuHeight
            }
        } 
        #endregion

        //
        // GET: /SiteTopNav/
        public ActionResult Index()
        {
            model = new SiteTopNavModel();

            #region legacy code
            bxgOwner = (Owner)Session["BXGOwner"];
            TPexpired = false;
            model.TPRenewLink = false;

            //if (IsPostBack) {
            //m_BGColor = ViewState("m_BGColor")
            //m_BGMainColor = ViewState("m_BGMainColor")
            //m_ForeColor = ViewState("m_ForeColor")
            //m_iMenuHeight = ViewState("m_iMenuHeight")
            //}

            IdentifyNavigatorType();

            TravelerplusLandscape();

            //getFullPath();
            #endregion

            SetMenuItemControlValues(model);

            var root = CurrentPage.AncestorOrSelf(1);

            model.SiteName = root.GetPropertyValue<string>("siteName");
            if (string.IsNullOrWhiteSpace(model.SiteName))
            {
                model.SiteName = root.Name;
            }

            model.Title = CurrentPage.GetPropertyValue<string>("title");
            if (string.IsNullOrWhiteSpace(model.Title))
            {
                model.Title = CurrentPage.Name;
            }

            model.MainNavigation = mapNavigation(root).ToList();

            //return PartialView("ResponsiveNavBar", model);
            return PartialView("SiteTopNav", model);
        }

        public ActionResult SitecoreIndex()
        {
            model = new SiteTopNavModel();

            #region legacy code
            bxgOwner = (Owner)Session["BXGOwner"];
            TPexpired = false;
            model.TPRenewLink= false;

           

            IdentifyNavigatorType();

            TravelerplusLandscape();

            CheckForPointsUpdate(bxgOwner);
            //getFullPath();
            #endregion

            SetMenuItemControlValues(model);

            var root = CurrentPage.AncestorOrSelf(1);

            model.SiteName = root.GetPropertyValue<string>("siteName");
            if (string.IsNullOrWhiteSpace(model.SiteName))
            {
                model.SiteName = root.Name;
            }

            model.Title = CurrentPage.GetPropertyValue<string>("title");
            if (string.IsNullOrWhiteSpace(model.Title))
            {
                model.Title = CurrentPage.Name;
            }

            model.MainNavigation = mapNavigation(root).ToList();

            // Set Owner Details To Model 
            model.PointsTotal = bxgOwner.PointsTotal;
            model.PaymentBalance = bxgOwner.PaymentBalance;

            model.InstallStatus = "NULL";
            if (bxgOwner.InstallmentPlan[0].InstallmentStatus != null)
            {
                model.InstallStatus = bxgOwner.InstallmentPlan[0].InstallmentStatus.Replace(" ","")=="" ? "NULL" : bxgOwner.InstallmentPlan[0].InstallmentStatus ;
               
            }
            //return PartialView("ResponsiveNavBar", model);
            return PartialView("SitecoreTopNav", model);
        }

        private IEnumerable<NavigationItemModel> mapNavigation(IPublishedContent content)
        {
            // TODO : modify the code to limit children to items available to the current owner type
            return from c in content.Children
                   where c.IsVisible()
                   // Use the mapper to map items:
                   select NavigationItemMappers.Map(
                   new NavigationItemModel()
                   {
                       // Map children the same way as we have already mapped parents:
                       Children = mapNavigation(c)
                   }, c, CurrentPage);
        }

        // Specify menu items to be hidden or shown based on owner type
        private void SetMenuItemControlValues(SiteTopNavModel model)
        {
            // Home
            model.EnableHome = (model.NavigatorType != "Pending" && model.NavigatorType != "Corporate" && model.NavigatorType != "Fixed" && model.NavigatorType != "Flex");
            model.EnableTutorialsAndWebinars = model.NavigatorType != "Sampler";
            model.EnableBluegreenMemories = TPexpired && model.NavigatorType != "Travelerplus";
            model.EnableOwnerUpdate = !model.IsTravelerPlusEmployee;
            if(model.IsTravelerPlusEmployee)
            {
                model.HomeURL = "/TravelerPlus/owner/home.aspx";
            }
            if (model.NavigatorType == "Fixed")
            {
                model.HomeURL = "/owner/homefixed.aspx";
            }
            if (model.NavigatorType == "explore")
            {
                model.HomeURL = "/explore/home.aspx";
            }

            // Reservations
            model.EnablePointsReservations = ((!model.IsTravelerPlusEmployee && model.NavigatorType != "Fixed") || (model.IsPremierOwner && model.NavigatorType != "Fixed"));
            model.EnableBonusTimeReservations = model.NavigatorType == "owner" || model.NavigatorType == "Travelerplus" || (model.NavigatorType == "Sampler" && model.HomeProject == "52");
            model.EnableCalculatePoints = model.NavigatorType != "Fixed";
            model.EnableReservationsOurResorts = model.NavigatorType == "Fixed";
            model.EnableTravelServices = (model.NavigatorType != "Sampler" || (model.NavigatorType == "Sampler" && model.HomeProject == "52"));
            model.EnableResMenuResReminder = ((model.NavigatorType != "Sampler" && model.NavigatorType != "Fixed" && model.NavigatorType != "Flex") || (model.NavigatorType == "Sampler" && model.HomeProject == "52") || (model.IsPremierOwner && model.NavigatorType != "Fixed"));
            model.EnableChoiceHotels = model.NavigatorType != "Sampler" || model.IsPremierOwner;
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Setting the URL for the Reservations...Travel Services menu item. It is ordered so that the conditions are mutually exclusive and FIFO (in other words, if an account is Sampler and somehow 
            // TravelerPlusEmployee (should never happen, but...), the first match takes precedence.
            if (model.NavigatorType == "owner" || model.NavigatorType == "Sampler")
            {
                model.ReservationTravelServicesURL = "/owner/TravelServices.aspx";
            }
            else if (model.IsTravelerPlusEmployee)
            {
                if (model.IsTPExpired)
                {
                    model.ReservationTravelServicesURL = "/TravelerPlus/owner/TravelServices.aspx";
                }
            }
            else if(model.IsPremierOwner || model.NavigatorType == "Travelerplus" || model.NavigatorType == "Fixed")
            {
                model.ReservationTravelServicesURL = "/owner/TravelServices.aspx";
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            // My Account
            model.EnableMyAccount = true;
            model.EnableGoGreen = (!model.IsTravelerPlusEmployee && model.NavigatorType != "Sampler");
            model.EnableMyAccountBluegreenPremier = (!(model.NavigatorType == "Sampler" || model.NavigatorType == "Fixed") && model.ShowGuestProfile != "V");
            model.EnableFreeStayCertificate = model.ShowGuestProfile == "P" || model.ShowGuestProfile == "G";
            model.EnableChoicePrivileges = (model.NavigatorType == "owner" || model.NavigatorType == "Sampler" || model.IsPremierOwner || model.NavigatorType == "Travelerplus") && model.ContractType != "Sampler";
            model.EnableMyPoints = (model.NavigatorType != "Fixed" && model.NavigatorType != "Flex" && !model.IsTravelerPlusEmployee);
            model.MyPointsURL = (Session["IsFixedFlexOrTraditionalOwner"] is bool && (bool)Session["IsFixedFlexOrTraditionalOwner"]) ? "/owner/ownerAccount.aspx" : CustomHtmlHelpers.GetFullSitePath(null) + "/my-points";

            // Payments
            model.EnablePayments = true;
            model.EnablePrePayment = (!model.IsTravelerPlusEmployee && !InstalPayEligible);
            model.EnableViewInstallmentPlan = InstalPayEligible;
            model.EnableChangeInstallPlanCC = InstalPayEligible && (InstallStatus == "IP");
            
            // Traveler Plus
            model.EnableTravelerPlus = ((model.NavigatorType == "Travelerplus" || model.IsPremierOwner) && !TPexpired);
            model.TravelerPlusLevel = TrvelerPlusLevel;
            model.EnableRenewTravelerPlus =model.TPRenewLink;
            /////////////////////////////////////////////////////////////////////////////
            // Setting the URL for when owner clicks on top level Traveler Plus menu item
            if (model.NavigatorType == "owner" || model.NavigatorType == "Sampler" || model.NavigatorType == "Fixed")
            {
                model.TravelerPlusURL = "/owner/vcTravelerPlus.aspx";
            }
            else if(model.IsTravelerPlusEmployee)
            {
                model.TravelerPlusURL = "/TravelerPlus/owner/home.aspx";
            }
            else if (model.IsPremierOwner || model.NavigatorType == "Travelerplus")
            {
                if(bxgOwner.User[0].AllAccountsComesFromSecondaryMarketing && TPexpired)
                {
                    model.TravelerPlusURL = "/owner/vcTravelerPlus.aspx";
                }
                else 
                {
                    model.TravelerPlusURL = "/TravelerPlus/owner/home.aspx";
                }
            }
            /////////////////////////////////////////////////////////////////////////////

            // Bluegreen Rewards
            model.EnableBluegreenRewards = (!model.IsTravelerPlusEmployee && model.NavigatorType != "Sampler");
            model.EnableRegisterFriends = (model.NavigatorType == "Travelerplus" && tpnav) || model.NavigatorType == "Fixed" || model.NavigatorType == "Pending";
            model.EnableSendOffersToFriends = (model.NavigatorType == "Travelerplus" && tpnav) || model.NavigatorType == "Fixed" || model.NavigatorType == "Pending";
            model.EnableReferralParties = (model.NavigatorType == "Travelerplus" && !tpnav);

            // Using the Club
            model.EnableVacationClub = model.ContractType.ToUpper() != "TRADITIONAL" && model.NavigatorType != "Sampler";
            model.EnableVacationClubFixedMenu = (model.NavigatorType == "Fixed");
            model.EnableBluegreenPremier = model.ShowGuestProfile == "V" && model.NavigatorType != "Sampler" && model.NavigatorType != "Fixed" && model.ContractType.ToUpper() != "TRADITIONAL" && !model.IsTravelerPlusEmployee;

            if(model.ContractType == "Vacation Club")
            {
                model.UsingTheClubURL = ConfigurationManager.AppSettings["UsingTheClub_VCOwner"];
            }
            else if(model.NavigatorType == "Sampler")
            {
                if(model.HomeProject == "52")
                {
                    model.UsingTheClubURL = ConfigurationManager.AppSettings["UsingTheClub_Sampler24"];
                }
                else
                {
                    model.UsingTheClubURL = ConfigurationManager.AppSettings["UsingTheClub_Sampler"];
                }
            }
            if (model.UsingTheClubURL.IndexOf("~") == 0)
            {
                model.UsingTheClubURL = model.UsingTheClubURL.Substring(1);
            }

            if (model.EnableBluegreenPremier)
            {
                model.TutorialsAndWebinars = ConfigurationManager.AppSettings["TutorialsAndWebinars"];
            }
            else
            {
                model.TutorialsAndWebinars = ConfigurationManager.AppSettings["TutorialsAndWebinarsUTC"]; ;
            }
            if (model.TutorialsAndWebinars.IndexOf("~") == 0)
            {
                model.TutorialsAndWebinars = model.TutorialsAndWebinars.Substring(1);
            }

            if (model.EnableVacationClubFixedMenu)
            {
                model.BluegreenVacationClub = "/owner/vacationclub.aspx";
                model.HowItWorks = "/owner/vcHowItWorks.aspx";
                model.OwnerTestimonials = "/owner/vcTestimonials.aspx";
                model.OurResorts = "/BGModern/our-resorts";
                model.TellMeMore = "/owner/learnMore.aspx";
                model.HelpFAQs = "/owner/faq.aspx";
                model.ContactUs = "/owner/contact.aspx";
            }

            //if (model.NavigatorType == "Sampler")
            //{
            //    if (model.HomeProject == "51")
            //    {
            //        model.TravelerPlusURL = "/owner/clubuse-valuesampler.aspx";
            //    }
            //    else if (model.HomeProject == "52")
            //    {
            //        model.TravelerPlusURL = "/owner/clubuse-sampler24.aspx";
            //    }
            //}
        }

        private void IdentifyNavigatorType()
        {
            
            bxgOwner = (BGO.OwnerWS.Owner)HttpContext.Session["BXGOwner"];
            
            
            #if STANDALONE
            model.ShowGuestProfile = "";
            model.HomeProject = "";
            model.ContractType = "Vacation Club";
            model.AllAccountsFromSecondaryMarketing = false;
            #else
            model.ShowGuestProfile = ((BGO.BluegreenOnline.Bluegreenowner)HttpContext.Session["BluegreenownerForUmbraco"]).OwnerMembershipType;
            if (Session["OwnerContractType"] !=null)
            {
                model.ContractType = Session["OwnerContractType"].ToString();
            }           
            #endif

            model.AllAccountsFromSecondaryMarketing = bxgOwner.User[0].AllAccountsComesFromSecondaryMarketing;
            model.HomeProject = bxgOwner.User[0].HomeProject;
           
            
            if ( Server.MapPath("").ToLower().EndsWith("owner") )
            {
                model.NavigatorType = "owner";
            }

            if(model.ContractType == "Vacation Club" )
            {
                model.NavigatorType = "owner";
            }

            if (model.ContractType == "Sampler")
            {
                model.NavigatorType = "Sampler";
            }

            if (Session["IsTravelerPlusEmployee"] != null && Session["IsTravelerPlusEmployee"].ToString() == "TRUE")
            {
                model.IsTravelerPlusEmployee = true;
            }

            if (Session["ispremierOwner"] != null && Session["ispremierOwner"].ToString().Trim().ToLower() == "true")
            {
                model.IsPremierOwner = true;
            }

            if ((model.IsTravelerPlusEmployee || 
                (HttpContext.Session["IsTravelerPlusOwner"] != null && HttpContext.Session["IsTravelerPlusOwner"].ToString() == "TRUE") ||
                (HttpContext.Session["IsTravelerPlusEligible"] != null  && HttpContext.Session["IsTravelerPlusEligible"].ToString() == "TRUE")) && model.ContractType == "Vacation Club") 
            {
                model.NavigatorType = "Travelerplus";
            }

            if (Session["PendingOwner"] != null && Session["PendingOwner"].ToString() == "TRUE")
            {
                model.NavigatorType = "Pending";
                model.TravelerPlusGoesNowhere = true;
            }

            if (Session["siteNavjs"] != null && Session["siteNavjs"].ToString() == "ownerNVC_data")
            {
                model.NavigatorType = "Fixed";
            }
            if (model.HomeProject != null)
            {
                if (model.HomeProject == "52")
                {
                    model.NavigatorType = "Sampler24";

                }
            }
        }

        private String getFullPath()
        {
            String protocol;

            if (Server.MapPath("").ToLower().EndsWith("explore"))  model.NavigatorType = "explore";
            if (Server.MapPath("").ToLower().EndsWith("corporate")) model.NavigatorType = "corporate";
            if (Server.MapPath("").ToLower().Contains("travelerplus"))  tpnav = true;
            if (Server.MapPath("").ToLower().Contains("encorerewards"))  isEncoreRewards = true;

            // Old server paths don't apply to MVC site
            //if (!Server.MapPath("").ToLower().EndsWith("explore") && !Server.MapPath("").ToLower().EndsWith("corporate") && !Server.MapPath("").ToLower().Contains("travelerplus") && !Server.MapPath("").ToLower().Contains("owner") && !Server.MapPath("").ToLower().Contains("encorerewards") && !Server.MapPath("").ToLower().Contains("bonustime")) 
            //{
            //    sNavType = "";
            //}

            //set srvRoot to get full path of host to point to central images directory
            //for example image path would be set to 'http://MACHINE_NAME
            //imgPath = "http://" & HttpContext.Current.Server.MachineName

            protocol = Request.ServerVariables["SERVER_PORT_SECURE"];

            if ((protocol == null || protocol == "0")) 
            {
                protocol = "http://";
            }
            else 
            {
                protocol = "https://";
            }

            srvRoot = String.Format("{0}{1}", protocol, Request.ServerVariables["SERVER_NAME"]);

            return srvRoot;
        }

        private void DefineMarketingCampaignFlag()
        {
            //The flag from BxgOwner replaced the previous logical where a call to SQL to obtain owner's accounts. Second loop through all owner's account verify certain date condition 
            //and third, unfortunately, ucmenu is rendering every single time a screen change happening.
            //Since the flag is already set it will speed up the render process.
            HttpContext.Session["showMC"] = bxgOwner.User[0].AtLeastOneAccountQualify4MarketCampaig;
            MktCampaign = bxgOwner.User[0].AtLeastOneAccountQualify4MarketCampaig;
        }

        private void TravelerplusLandscape()
        {
            if (bxgOwner != null) {
                TrvelerPlusLevel = Convert.ToInt32(bxgOwner.TravelerPlusMembership.TPLevel);
                // TODO: fix the following 
#if STANDALONE 
                BWGift = "";
#else
                BWGift = ((BGO.BluegreenOnline.Bluegreenowner)HttpContext.Session["BluegreenownerForUmbraco"]).BlueWhiteGift;
#endif
                InstallStatus = bxgOwner.InstallmentPlan[0].InstallmentStatus;
                InstalPayEligible = (bxgOwner.InstallmentPlan[0].InstallmentPaymentEligible == "1" && (InstallStatus == "IP" || InstallStatus =="IS" || InstallStatus == "IC")); //BGO uses string value instead of boolean. we are using boolean.

                //verify owner TP renew status and owner level to display renew link from top nav
                try{
                    Boolean Tpexpdatecheck = false;

                    //verify tp auto renew and cc exp date to show the renew  link
                    if (bxgOwner.TravelerPlusMembership.IsTravelerPlusAutoRenew == true && bxgOwner.TravelerPlusMembership.TPCCExp.Trim().Length > 1) {

                        //validate the cc expiration date
                        int explen = bxgOwner.TravelerPlusMembership.TPCCExp.Length;
                        String ccexpdateMonth = bxgOwner.TravelerPlusMembership.TPCCExp.Substring(0, explen - 2);
                        if (explen == 3) 
                        {
                            ccexpdateMonth = String.Format("{0}{1}", "0", ccexpdateMonth);
                        }

                        String ccexpdateYear = bxgOwner.TravelerPlusMembership.TPCCExp.Substring(explen - 2, 2);
                        DateTime dtTemp = new DateTime(Convert.ToInt32(ccexpdateYear), Convert.ToInt32(ccexpdateMonth), 1);
                        int lastDayOfMonth = GetLastDay(dtTemp);
                        DateTime ccexpdate = new DateTime(Convert.ToInt32(ccexpdateYear), Convert.ToInt32(ccexpdateMonth), lastDayOfMonth);
                        DateTime tpexpFirstdayoftheMonth = GetFirstDay(DateTime.Parse(bxgOwner.TravelerPlusMembership.TPExpiration));

                        //check the 2 monoth expiration window
                        DateTime now = DateTime.Now;
                        DateTime tpexpwindow = now.AddMonths(2);
                        int lastdaytpexpwindow  = GetLastDay(new DateTime(tpexpwindow.Year, tpexpwindow.Month, 1));
                        
                        DateTime tpexpdte = new DateTime(tpexpwindow.Year, tpexpwindow.Month, Convert.ToInt32(lastdaytpexpwindow));
                        
                        if (DateTime.Compare(ccexpdate, tpexpFirstdayoftheMonth) < 0 && DateTime.Compare(DateTime.Parse(bxgOwner.TravelerPlusMembership.TPExpiration.ToString()), tpexpdte) <= 0) 
                        {
                            Tpexpdatecheck = true;
                        }
                    }
                    if (bxgOwner.OwnerExpiration != null)
                    {
                        string iDate = bxgOwner.OwnerExpiration.ToString();
                        DateTime oDate = Convert.ToDateTime(iDate);

                        if (model.NavigatorType == "Travelerplus" && DateTime.Today.AddDays(1) <= oDate)
                            model.OwnerExpiration = oDate.ToString("d");
                        else
                            model.OwnerExpiration = "NULL";
                    }
                    else
                    {
                        model.OwnerExpiration = "NULL";
                    }

                   
                    if (Tpexpdatecheck == true)
                    {
                        model.TPRenewLink = true;
                    }
                }
             
                catch //(Exception ex)
                {

                }

                if (bxgOwner.TravelerPlusMembership.TPLevel == Travelerpluslevel.TravelerPlusPlusLevel) {

                    bTravelerPlusPlus = true;
                }

                if ((HttpContext.Session["IsTravelerPlusOwner"] != null && HttpContext.Session["IsTravelerPlusOwner"].ToString() == "TRUE") ||
                    (HttpContext.Session["IsTravelerPlusEligible"] != null && HttpContext.Session["IsTravelerPlusEligible"].ToString() == "TRUE") || bTravelerPlusPlus == true)
                {
                    try
                    {
                        DateTime now = DateTime.Now;
                        DateTime expiry = DateTime.Parse(HttpContext.Session["Expires"].ToString());
                        DateTime ccc = expiry.AddDays(1);
                        DateTime yy = now;
                        
                        if (DateTime.Compare(ccc, now) < 0) {

                            TPexpired = true;
                            model.IsTPExpired = true;
                        }
                    }
                    catch //(Exception ex)
                    {

                    }
                }
                if ((model.NavigatorType == "Travelerplus"))
                {
                    if ((model.IsTPExpired == false))
                    {
                        if (Session["IsTravelerPlusEmployee"] != null && Session["IsTravelerPlusEmployee"].ToString() == "TRUE")
                        {
                            model.TPRenewLink = false;
                        }
                        else
                        {
                            if (((bxgOwner.TravelerPlusMembership.IsTravelerPlusAutoRenew == true) | (bxgOwner.membershipLevel == "P")))
                            {
                                model.TPRenewLink = false;
                            }
                            else
                            {
                                if ((bxgOwner.User[0].AllAccountsComesFromSecondaryMarketing == false))
                                {
                                    model.TPRenewLink = true;
                                }
                                else
                                {
                                    model.TPRenewLink = false;
                                }
                            }
                        }
                    }
                }
                DefineMarketingCampaignFlag();
            }
        }

        public DateTime GetFirstDay(DateTime aDate)
        {
            DateTime dteFirstDayNextMonth;

            dteFirstDayNextMonth = new DateTime(aDate.Year, aDate.Month + 1, 1);
            return dteFirstDayNextMonth.AddDays(-1);
        }

        private int GetLastDay(DateTime aDate)
        {
            DateTime dteFirstDayNextMonth;

            dteFirstDayNextMonth = new DateTime(aDate.Year, aDate.Month + 1, 1);
            return dteFirstDayNextMonth.AddDays(-1).Day;
        }

        public cInitMenu InitMenuObject(otMenuType mnuType, otDisplayType mnuDisplay, String sMenuTitle, String sUrl, String sOverImg, String sOutImg, otNavType iNavType, String sCommand = "", int iWidth = 78, int iHeight = 22)
        {
            cInitMenu im = new cInitMenu();
            return im;
        }

        public void CrOwnerMenu(){ }

        public void CrTPOwnerMenu(){}

        public void CrPendingOwnerMenu() { }

        public void FinalizeMenus(){}

        public void CreateTopLabels(cInitMenu[] arrMenuItems, String sBGColor, String sForeColor, Boolean fCreateRebar = false){}

        private String CreateDiv(String sId, int iLeft, int iTop) 
        {
            String divCtrl = m_sDivMenu;

            return divCtrl;
        }

        int CreateSubDiv(int iIdx , cInitMenu[] arrMenuItems, String sBGColor, String sForeColor)
        {
            return 0;
        }

        public void CreateSideDiv(int iIdx, int subIdx, Object arrMenuItems, String sBGColor, String sForeColor)
        {
            cMenuObj subMenu = m_arrMenuItems[iIdx];
        }

        private void CheckForPointsUpdate(Owner owner)
        {
            try
            {
                bool isPointsUpdated = HttpContext.Session["isPointsUpdated"] == null ? false : bool.Parse(HttpContext.Session["isPointsUpdated"].ToString());

                if (isPointsUpdated == true)
                {
                    OwnerWS1SoapClient OwnerServiceProxy = new OwnerWS1SoapClient();
                    BGO.OwnerWS.Points ownerTotalPoints = new BGO.OwnerWS.Points();
                    ownerTotalPoints = OwnerServiceProxy.getTotalPoints(owner.Arvact);
                    
                    if (NeedsUpdatedOwnerPointValues(owner, ownerTotalPoints))
                    {
                        owner.PointsTotal = ownerTotalPoints.PointsTotal;
                        owner.PointsTotalAnnual = ownerTotalPoints.PointsTotalAnnual;
                        owner.PointsTotalRestricted = ownerTotalPoints.PointsTotalRestricted;
                        owner.PointsTotalSaved = ownerTotalPoints.PointsTotalSaved;
                        owner.PointsTotalFuture = ownerTotalPoints.PointsTotalFuture;

                        NewPointsHandler(owner);
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        private bool NeedsUpdatedOwnerPointValues(BGO.OwnerWS.Owner OldOwner, BGO.OwnerWS.Points NewOwnerPoints)
        {
            bool needsUpdated = false;

            if (OldOwner.PointsTotal != NewOwnerPoints.PointsTotal)
                needsUpdated = true;
            if (OldOwner.PointsTotalAnnual != NewOwnerPoints.PointsTotalAnnual)
                needsUpdated = true;
            if (OldOwner.PointsTotalRestricted != NewOwnerPoints.PointsTotalRestricted)
                needsUpdated = true;
            if (OldOwner.PointsTotalSaved != NewOwnerPoints.PointsTotalSaved)
                needsUpdated = true;
            if (OldOwner.PointsTotalFuture != NewOwnerPoints.PointsTotalFuture)
                needsUpdated = true;

            return needsUpdated;
        }

        private void NewPointsHandler(BGO.OwnerWS.Owner bxgOwner)
        {
            try
            {
                HttpContext.Session["isPointsUpdated"] = false;

                //Populate the Owner object and Session variable.
                BGO.BluegreenOnline.Bluegreenowner.CurrentOwner.OwnerAnnualPoints = bxgOwner.PointsTotalAnnual.ToString();
                BGO.BluegreenOnline.Bluegreenowner.CurrentOwner.OwnerSavedPoints = bxgOwner.PointsTotalSaved.ToString();
                BGO.BluegreenOnline.Bluegreenowner.CurrentOwner.OwnerRestrictedPoints = bxgOwner.PointsTotalRestricted.ToString();
                BGO.BluegreenOnline.Bluegreenowner.CurrentOwner.OwnerTotalPoints = bxgOwner.PointsTotal.ToString();
                BGO.BluegreenOnline.Bluegreenowner.CurrentOwner.OwnerPaymentBalance = bxgOwner.PaymentBalance.ToString();

                Session["BXGOwner"] = bxgOwner;
            }
            catch (Exception ex)
            {

            }
        }

    }
}


