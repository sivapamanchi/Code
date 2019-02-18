using System.Collections.Generic;
using System.Configuration;

namespace BGModern.Models
{
    public class SiteTopNavModel
    {
        #region Properties
        public string Title { get; set; }
        public string SiteName { get; set; }
        public string ParentUrl { get; set; }

        public List<NavigationItemModel> MainNavigation { get; set; }

        public bool AllAccountsFromSecondaryMarketing { get; set; }
        public bool IsPremierOwner { get; set; }
        public bool IsSampler { get; set; }
        public bool IsTravelerPlusEmployee { get; set; }
        public bool IsTPExpired { get; set; }
        public string ContractType { get; set; }
        public string HomeProject { get; set; }
        public string NavigatorType { get; set; }
        public string ShowGuestProfile { get; set; }
        public string OwnerLevel { get; set; }

        // Home
        public bool EnableHome { get; set; }
        public bool EnableTutorialsAndWebinars { get; set; } // if NavigatorType != "Sampler"
        public bool EnableBluegreenMemories { get; set; } // if TPExpired or NavigatorType != "Travelerplus"
        public bool EnableOwnerUpdate { get; set; } // if NavigatorType != TP employee
        public bool EnableOurResorts { get; set; } // if NavigatorType != "Fixed"

        // Reservations
        public bool EnablePointsReservations { get; set; } // if NavigatorType != "Fixed" and not TP employee
        public bool EnableBonusTimeReservations { get; set; } // if NavigatorType == "Owner" or "Travelerplus" or ("Sampler" and homeproject = 52)
        public bool EnableCalculatePoints { get; set; } // if NavigatorType != "Fixed"
        public bool EnableTravelServices { get; set; } // if NavigatorType != "Sampler" or (NavigatorType = "Sampler" and homeproject = "52")
        public bool EnablePremierWaitList { get; set; } // if NavigatorType = "PremierOwner"
        public bool EnableReservationsOurResorts { get; set; } // if NavigatorType != "Sampler"
        public bool EnableResMenuResReminder { get; set; }// if NavigatorType == Owner or (NavigatorType == "Sampler" and homeproject = 52)
        public bool EnableChoiceHotels { get; set; } // if NavigatorType != "Sampler"

        // Payments
        public bool EnablePayments { get; set; }
        public bool EnablePrePayment { get; set; } // if InstalPayEligible != 1 and (InstallStatus == "IP" or InstallStatus == "IS" or InstallStatus == "IC")
        public bool EnableViewInstallmentPlan { get; set; } // if InstalPayEligible = 1 and InstallStatus == "IP"
        public bool EnableChangeInstallPlanCC { get; set; } // if (InstalPayEligible = 1 and InstallStatus == "IS") or InstallStatus == "IC"
        public bool EnablePayInstallPmts { get; set; } // if InstalPayEligible = 1 and (InstallStatus == "IP" or InstallStatus == "IS" or InstallStatus == "IC")

        // My Account
        public bool EnableMyAccount { get; set; }
        public bool EnableGoGreen { get; set; } // if NavigatorType != "Sampler"
        public bool EnableMyAccountBluegreenPremier { get; set; } // if NavigatorType != ("Sampler" or "Fixed") and ShowGuestProfile != "V" 
        public bool EnableFreeStayCertificate { get; set; } // if ShowGuestProfile = P or G
        public bool EnableChoicePrivileges { get; set; }
        public bool EnableMyPoints { get; set; } // if NavigatorType != "Fixed" or "Travelerplus"

        // Bluegreen Rewards
        public bool EnableBluegreenRewards { get; set; }
        public bool EnableSendOffersToFriends { get; set; } // if (NavigatorType = "Travelerplus" && tpnav) or NavigatorType = "Fixed" or NavigatorType = "Pending"
        public bool EnableRegisterFriends { get; set; } // if (NavigatorType = "Travelerplus" && tpnav) or NavigatorType = "Fixed" or NavigatorType = "Pending"
        public bool EnableReferralParties { get; set; } // if (NavigatorType = "Travelerplus" && !tpnav)

        // Traveler Plus - TP employee (Session["IsTravelerPlusEmployee"] == "TRUE")
        public bool TravelerPlusGoesNowhere { get; set; } // some owner types (e.g. pending) should do nothing when the top-level TP menu item is clicked
        public bool EnableTravelerPlus { get; set; } // if NavigatorType = "Travelerplus" or is TP employee (Session["IsTravelerPlusEmployee"] == "TRUE")
        public int TravelerPlusLevel { get; set; }
        public bool EnableRenewTravelerPlus { get; set; } // TPRenewLink (what is this?)

        // Traveler Plus - All Others
        public bool EnablePointSmart { get; set; } // if NavigatorType = "Travelerplus and !TPexpired
        public bool EnableDirectExchange { get; set; } // if TrvelerPlusLevel > 1
        public bool EnableOutdoorSportsman { get; set; } // if TrvelerPlusLevel >= 2
        public bool EnableGolfCard { get; set; } // if TrvelerPlusLevel >= 3
        public bool EnableSkiAndSnowboard { get; set; } // if TrvelerPlusLevel >= 3
        public bool EnableEverydayServices { get; set; } // if TrvelerPlusLevel >= 3

        // Using the Club
        public bool EnableVacationClub { get; set; }
        public bool EnableVacationClubFixedMenu { get; set; }
        public bool EnableBluegreenPremier { get; set; }

        ///////////////////////////////////////
        // Misc. Owner type-specific URL values
        ///////////////////////////////////////
        public string HomeURL { get; set; }
        public string MyPointsURL { get; set; }
        public string CruisesURL { get; set; }
        public string TravelerPlusURL { get; set; }
        public string UsingTheClubURL { get; set; }
        public string HelpFAQs { get; set; }
        public string ContactUs { get; set; }
        public string ReservationTravelServicesURL { get; set; }
        public string CashSmartTravelServicesURL { get; set; }

        ////////////////////////////////////////////
        // OWNER TYPE-SPECIFIC "Using the Club" URLs
        ////////////////////////////////////////////

        // "Fixed" owners
        public string BluegreenVacationClub { get; set; }
        public string HowItWorks { get; set; }
        public string OwnerTestimonials { get; set; }
        public string OurResorts { get; set; }
        public string ResortLocator { get; set; }
        public string TellMeMore { get; set; }
        // Everyone else
        public string ClubOverview { get; set; } // if Session["OwnerContractType"] != "TRADITIONAL" and NavigatorType != "Sampler"
        public string TutorialsAndWebinars { get; set; } // if Session["OwnerContractType"] != "TRADITIONAL" and NavigatorType != "Sampler"
        public string QuickStartGuide { get; set; } // if Session["OwnerContractType"] != "TRADITIONAL" and NavigatorType != "Sampler"
        public string HowPointsWork { get; set; } // if Session["OwnerContractType"] != "TRADITIONAL" and NavigatorType != "Sampler"
        public string pointsProtectionPlan { get; set; } // if Session["OwnerContractType"] != "TRADITIONAL" and NavigatorType != "Sampler"
        public string ReservationGuidelines { get; set; } // if Session["OwnerContractType"] != "TRADITIONAL" and NavigatorType != "Sampler"
        public string MakingExchanges { get; set; } // if Session["OwnerContractType"] != "TRADITIONAL" and NavigatorType != "Sampler"
        public string BluegreenPremier { get; set; } // if Session["OwnerContractType"] != "TRADITIONAL" and NavigatorType != "Sampler" and ShowGuestProfile = "V"
        public string BeforeYouGo { get; set; }
        #endregion


        //Owner Details for Top Menu

        public int PointsTotal { get; set; }
        public double PaymentBalance { get; set; }
        public string OwnerExpiration { get; set; }
        public string SiteTopMenuURL { get; set; }
        public string InstallStatus { get; set; }
        public bool TPRenewLink { get; set; }

        public SiteTopNavModel()
        {
            HomeURL = "/BGModern/home"; // certain owner types use a different URL for home
            CruisesURL = "/owner/cruises.aspx"; // if the NavigatorType is not an "owner, depending upon the TPExpired value, this URL will change
            TravelerPlusURL = "/owner/vcTravelerPlus.aspx"; // certain owner types use a different URL for TP
            TravelerPlusGoesNowhere = false; // default to false. only Pending owner types should do nothing when the top level TP menu is clicked
            ReservationTravelServicesURL = "/owner/TravelServices.aspx";
            CashSmartTravelServicesURL = "/TravelerPlus/owner/TravelServices.aspx";


            SiteTopMenuURL = ConfigurationManager.AppSettings["bxgwebMenuPath"];

            UsingTheClubURL = ConfigurationManager.AppSettings["UsingTheClub_Owner"];
            if (UsingTheClubURL.IndexOf("~") == 0)
            {
                UsingTheClubURL = UsingTheClubURL.Substring(1);
            }
            ClubOverview = ConfigurationManager.AppSettings["ClubOverview"];
            if (ClubOverview.IndexOf("~") == 0)
            {
                ClubOverview = ClubOverview.Substring(1);
            }
            TutorialsAndWebinars = ConfigurationManager.AppSettings["TutorialsAndWebinarsUTC"];
            if (TutorialsAndWebinars.IndexOf("~") == 0)
            {
                TutorialsAndWebinars = TutorialsAndWebinars.Substring(1);
            }
            QuickStartGuide = ConfigurationManager.AppSettings["QuickStartGuide"];
            if (QuickStartGuide.IndexOf("~") == 0)
            {
                QuickStartGuide = QuickStartGuide.Substring(1);
            }
            HowPointsWork = ConfigurationManager.AppSettings["HowPointsWork"];
            if (HowPointsWork.IndexOf("~") == 0)
            {
                HowPointsWork = HowPointsWork.Substring(1);
            }
            pointsProtectionPlan = ConfigurationManager.AppSettings["PointsProtectionPlan"];
            if (pointsProtectionPlan.IndexOf("~") == 0)
            {
                pointsProtectionPlan = pointsProtectionPlan.Substring(1);
            }
            ReservationGuidelines = ConfigurationManager.AppSettings["ReservationGuidelines"];
            if (ReservationGuidelines.IndexOf("~") == 0)
            {
                ReservationGuidelines = ReservationGuidelines.Substring(1);
            }
            MakingExchanges = ConfigurationManager.AppSettings["MakingExchanges"];
            if (MakingExchanges.IndexOf("~") == 0)
            {
                MakingExchanges = MakingExchanges.Substring(1);
            }
            BluegreenPremier = ConfigurationManager.AppSettings["BluegreenPremier"];
            if (BluegreenPremier.IndexOf("~") == 0)
            {
                BluegreenPremier = BluegreenPremier.Substring(1);
            }
            BeforeYouGo = ConfigurationManager.AppSettings["BeforeYouGo"];
            if (BeforeYouGo.IndexOf("~") == 0)
            {
                BeforeYouGo = BeforeYouGo.Substring(1);
            }
            HelpFAQs = ConfigurationManager.AppSettings["HelpFaq"];
            if (HelpFAQs.IndexOf("~") == 0)
            {
                HelpFAQs = HelpFAQs.Substring(1);
            }
            ContactUs = ConfigurationManager.AppSettings["ContactUs"];
            if (ContactUs.IndexOf("~") == 0)
            {
                ContactUs = ContactUs.Substring(1);
            }


        }
    }
}