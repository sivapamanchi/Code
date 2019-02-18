<%@ Reference Control="~/includes/ucmenu.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="BluegreenOnline.ownerAdventures" Codebehind="ownerAdventures.aspx.vb" %>
<%@ Register TagPrefix="includeControlFooter" TagName="footer" Src="~/includes/footer.ascx" %>
<%@ Register TagPrefix="includeControlTPMenu" TagName="TPMenus" Src="~/TravelerPlus/includes/tpDropDownMenu.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ucMenu" Src="~/includes/ucMenu.ascx" %>
<%@ Register TagPrefix="traveler" TagName="nav" Src="~/includes/TPleftNav.ascx" %>
<%@ Register TagPrefix="includeControlBannerRotation" TagName="BannerRotation" Src="~/TravelerPlus/includes/bannerRotation.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//Dtd HTML 4.01 transitional//EN" "http://www.w3.org/tr/html4/loose.dtd">
<html dir="ltr">
<head>
<title>Bluegreen Traveler Plus Owner Adventures</title>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<link rel="stylesheet" type="text/css" href="/css/owner/owner.css" />
<link rel="stylesheet" type="text/css" href="/css/ucmenu.css" />
<link rel="stylesheet" type="text/css" href="/css/style.css" />


<script language="javaSCRIPT" src="/SCRIPTs/rollover.js"></script>
<script language="javascript" src="/js/otdivCommon.js"></script>
<script language="javascript" src="/js/otMenu.js"></script>
	</head>
<body leftMargin="0" topMargin="0" marginheight="0"	marginwidth="0" onload="doIT()">
<a name="top" id="top"></a>
<%If Session("IstravelerPlusEmployee") = "trUE" Then%>
<%Else%>		
	<uc1:ucmenu id="UcMenu1" runat="server"></uc1:ucmenu>
<%End If%>
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
	<%If Session("IstravelerPlusEmployee") = "trUE" Then%>
		<td vAlign="top" width="50%"><IMG height="10" src="/images/blank.gif" width="10" border="0"></td>
	<%Else%>
		<td vAlign="top" width="50%"><IMG height="10" src="/images/blank.gif" width="10" border="0"></td>
	<%End If%>
		<td vAlign="top" width="728">
			<table cellSpacing="0" cellPadding="0" width="740" border="0">
			<%If Session("IstravelerPlusEmployee") = "trUE" Then%>
			<%Else%>
<!-- Added new code to account for new ucmenu adjustments -->
				<tr>
					<td colspan="4">&nbsp;</td>
				</tr>
			<%End If%>
				<tr>
					<td vAlign="top" width="10" bgColor="#ffffff"><IMG src="/images/blank.gif" width="10"></td>
					<td vAlign="top" width="185" bgColor="#ffffff">
						<table class="submenu" cellspacing="0" cellpadding="0" width="185" border="0">
							<tr>
								<td><traveler:nav ID="lNav" runat ="server" /></td>
							</tr>
						</table>
						<br />
						<br />
						<includeControlBannerRotation:BannerRotation ID="BannerRotation" runat="server">
						</includeControlBannerRotation:BannerRotation>
					</td>
				<%If Session("IstravelerPlusEmployee") = "trUE" Then%>
					<td vAlign="top" align="right" width="64" bgColor="#ffffff"><IMG src="/images/blank.gif" width="50"></td>
				<%Else%>
					<td vAlign="top" align="right" width="64" bgColor="#ffffff"><IMG src="/images/blank.gif" width="50"></td>
				<%End If%>
					<td vAlign="top" align="left" width="493">
						<table cellSpacing="0" cellPadding="0" width="495" border="0">
							<tr>
								<td class="bcrumb" width="340" valign="top">
									<a class="bcrumblink" href='<%= Session("UnsecuredURL") %>default.aspx'>Bluegreen</a> / 
									<a class="bcrumblink" href="../../owner/home.aspx">Owners</a> / 
									<a class="bcrumblink" href="home.aspx">traveler Plus</a> / 
									Owner Avdentures
								</td>
							</tr>
							<tr>
								<td id="page-title">
									<h2 id="ownerAdventures">Owner Adventures</h2>
								</td>
							</tr>
							<tr>
								<td vAlign="bottom" align="left" width="495" colSpan="3" height="52">







<!-- Start CONTENT 
///////////////////////////////////////////////////////// -->


<h2>Owner Adventures</h2>
 
<p>When you embark on an Owner Adventures group vacation you are among friends who value their vacations as much as you do. Accompanied by Bluegreen Vacations' hosts, these unique getaways feature private events, exciting excursions, exclusive amenities and more. Bluegreen Vacations has brought owners together in Alaska, Hawaii, California, Europe and other exciting destinations. Here’s where we’re going next:
</p> 

<p>
<ul>
	
	<!--<li><a href="#1">Chef-Inspired Culinary Adventure</a></li>-->
    <li><a href="#2">Hawaii Land &amp; Sea</a></li>
	<li><a href="#3">Treasures of Italy</a></li>
    <li><a href="#4">Australia 2015! Adventure Down Under</a></li> 
</ul>
</p>
<!--
<div class="BackToTop" style="border-bottom: 1px dotted #666666; padding-bottom: 10px; text-align: right"></div>

<h2 id="1" name="1">Chef-Inspired Culinary Adventure </h2>
<p><strong>January 23-27, 2014</strong><br />
4-night Gourmet Inclusive vacation at the El Dorado Royale Resort in Riviera Maya, Mexico</span></p>
<p>Love to watch cooking competition shows like "The Next Iron Chef" or "MasterChef"? Is the kitchen your favorite room of the house? Or do you just like savoring a great meal? Bluegreen Vacations has planned a unique tropical fantasy camp for cooks (and those who appreciate them) in the beautiful setting of the Mexican Caribbean in January.<br />
<br />

Leave the winter chills behind at the AAA 4-diamond <a href="http://www.eldoradosparesorts.com/hotels/el_dorado_royale/">El Dorado Royale</a>, an adults-only destination that provides a Gourmet Inclusive experience with all the luxuries you can imagine. Your stay at the resort includes four nights' accommodations in an elegant <strong>Oceanfront Jacuzzi Junior Suite</strong> featuring an oceanfront terrace and a private indoor Jacuzzi for two, plus all meals and snacks (gourmet, of course!), unlimited domestic and international premium alcoholic beverages, non-motorized sporting activities and more. It's all part of your vacation package!</p>
		
		<p>But that's not all. This Owner Adventure is unlike any other we've hosted. The El Dorado Royale's cuisine is their trademark, and the resort employs a dedicated team of star chefs trained at some of the finest <strong>Michelin</strong>- and <strong>Zagat</strong>-rated restaurants around the world. Not only do you get to indulge in their culinary masterpieces, but you'll also learn how to cook like a gourmet chef and engage in a fun-filled "Iron Chef" like cooking competition with your fellow Bluegreen Vacations owners.  We've planned some other entertaining experiences for you as well, including a tequila-tasting event!</p>
	  <p>Group activities will take only two to four hours per day, so you'll have plenty of time for snorkeling, tennis, golf or scuba clinics at the resort. Enjoy live music and shows at the El Dorado Royale nightly, or head over to Cancun, where the nightlife really sizzles. Pack your apron and join us for this most "tasteful" Bluegreen Vacations adventure. <br />
	  </p>	
    <p><strong>Daily Itinerary:</strong><br />
    </p>
      <p><strong>Day 1</strong><br />
	  Take up your utensils and meet your culinary competitors at a Welcome Cookout on the beach, where you'll participate in the grilling and drink mixing, guided by El Dorado Royale professional chefs and bartenders.<br />
  <br />
  <strong>Day 2</strong><br />
	  The real fun begins with a morning cooking class with the El Dorado Royale's creative chef in Fuentes, the resort's one-of-a-kind culinary theater. After the class, teams will be chosen for the next day's friendly cooking competition; there will be a maximum of 12 participants, and the rest of the group will cheer the teams on and sample the results. The rest of the day is yours to refresh in any of the resort's 13 swimming pools or order a cool beverage at a swim-up bar. That night, enjoy a sampling of tequilas.<br />
  <br />
	  <strong>Day 3</strong><br />
	  Get ready for the big event, an "Iron Chef" style competition where members of your group are the stars! Win or lose, it's sure to be a recipe for fun.<br />
	  <br />
	  <strong>Day 4</strong><br />
	  Your last full day, you'll tour the resort's greenhouses and see where all the produce for its seven exceptional restaurants is grown. Later enjoy a wine tasting with the El Dorado Royale's sommelier.<br />
	  </p>
      <table width="497" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td width="336" border="0" align="left" valign="middle"><p><strong>Exclusive Owner Adventures Perks:</strong><br />
          </p>
            <ul class="adventureBull">
              <li>Private Welcome Cookout</li>
              <li>Cooking class with the El Dorado Royale's creative chef</li>
              <li>Private tequila-tasting event</li>
              <li>Team cooking competition<br />
              </li>
              <li>Greenhouse tour</li>
              <li>Wine tasting with the El Dorado Royale's sommelier</li>
              <li>Bluegreen Vacations travel professional &amp; executive hosts</li>
              <li>Gift bag </li>
              <li>Airport transfers in Cancun</li>
              <li>Taxes and gratuities</li>
          </ul></td>
          <td width="161" align="right" valign="top"><strong><span style="vertical-align:top;">
          <img src="/TravelerPlus/owner/images/01.jpg" alt="Chef-Inspired-Culinary-Adventure" width="152" height="101" class="adventurePic_OA" align="right" /></span></strong></td>
        </tr>
        <tr>
          <td colspan="2" align="left" valign="middle"><p><strong><br />
            Gourmet Inclusive Resort Package Inclusions:</strong><br />
          </p>
            <ul class="adventureBull">
              <li>Gourmet meals and snacks at resort restaurants</li>
              <li>24-hour room service</li>
              <li>Unlimited beverages and domestic and international premium alcoholic drinks </li>
              <li>In-room stocked mini-bar</li>
              <li>13 swimming pools</li>
              <li>Non-motorized water sports with equipment provided (kayaks and snorkeling)</li>
              <li>Scuba clinics in pool</li>
              <li>Fitness center, sauna and steam room</li>
              <li>Land sports with equipment provided (tennis and bicycling)</li>
              <li>Personal concierge service<br />
              </li>
              <li>Scheduled resort activities and nightly entertainment</li>
              <li>Complimentary shuttle to Cancun and Playa del Carmen</li>
          </ul></td>
        </tr>
      </table>
    <p><strong><br />
Price:</strong><br />
From $1,389<sup>(1)(2)</sup> per person, based on double occupancy (airfare not included)</span><br /> 
    <br />
    <strong>Exclusive benefit for Traveler Plus members&mdash;pay with annual, unrestricted Points, a credit card or a combination of both!</strong><br />
    <br />
    <p style="color:#ff9900;"><strong>Reservation deadline extended to December 3, 2014!</strong></p>
    <p><strong>Call 800.459.1597 to Book Your Culinary Adventure Today!</strong></p>

<div class="BackToTop" style="border-bottom: 1px dotted #666666; padding-bottom: 10px; text-align: right"><a href="#top" class="ttt">Back to Top</a></div>
-->
<h2 id="2" name="2">Hawaii Land & Sea</h2>
<p><strong>May 22-31, 2014</strong>
	<br />
	 2-night stay in Honolulu plus a 7-night cruise to five ports of call on Maui, the Big Island of Hawaii and Kauai.</p>

        <div>
         <img src="/TravelerPlus/owner/images/02.jpg" alt="Hawaii Land &amp; Sea" width="152" height="101" class="adventurePic_OA" align="right" />
 		<p>Visit America's paradise and explore the beauty, history and natural wonders of the Hawaiian Islands on this land-and-sea adventure. Your vacation starts with two nights at the Waikiki Beach Marriott Resort &amp; Spa, situated only steps from world-famous Waikiki Beach and next to the Diamond Head Crater. Your stay on the island of Oahu includes a custom, 8-hour Pearl Harbor VIP Military Base Tour, the only tour that includes the World War II battlefields. Historians in WWII-era uniforms will be your guides at stops that include the Pearl Harbor Visitor's Center Museum and USS Arizona Memorial. You'll also follow the Japanese flight pattern to Oahu's famous fighter command post, Wheeler Field, and make stops at Schofield Army Barracks, Fort Shafter, the Punchbowl National Memorial and Home of the Brave, a private museum full of WWII memorabilia. You'll still have a full day to shop the International Market, climb Diamond Head, snorkel at Hanauma Bay or watch the sun set over Waikiki Beach. Soon you'll board Norwegian Cruise Line's <em>Pride of America</em> for a 7-night cruise and enjoy a private Welcome Aboard cocktail party for your group.</p>
        </div>
 		<p><strong>MAUI</strong><br />
    You'll have plenty of time to see Maui's natural splendor because you'll have two full days in port. Venture to the Iao Valley or the top of the Haleakala Crater. Leave early for the crater (4 a.m.) and take warm clothes (coat, hat and gloves!) to see a most spectacular sunrise&mdash;you'll never experience anything else like it. Take the twisting road to Hana, where the joy is in the journey, not the destination. At the quaint seaside town of Lahaina, see the century-old Banyan Tree. There are a plethora of shore excursions on the island of Maui. Book early because they fill up fast!</p>
 		<p><strong>HAWAII &ndash; Hilo &amp; Kona</strong><br />
    In Hilo on the Big Island of Hawaii, you'll see beautiful Rainbow Falls and a fabulous orchid garden, plus visit Volcano National Park and the Mauna Loa Macadamia Nut Factory, all of which are included in Bluegreen Vacations' group excursion. This tour ends with a visit to one of the Big Island's famous black-sand beaches&mdash;where you'll get a close-up look at large sea turtles sunning themselves. While in Kona, make sure to visit one of the many dozens of coffee farms and find out what makes 100% Kona coffee so unique and flavorful. </p>
 		<p><strong>KAUAI</strong><br />
    Kauai, Hawaii's "Garden Isle," has dramatic mountains and cool rainforests, as well as the Waimea Canyon, a breathtaking gorge often called the "Grand Canyon of the Pacific." Your stop in port includes an exclusive group excursion to the restored 1930s Kilohana Manor House and the best luau in Hawaii. You'll ride on the Kauai Plantation Railway to 22 North, the manor house that was the most expensive and beautiful home ever built on the island. Don a grass skirt for a hula lesson before a private group dinner, and then sit back and enjoy premium, front-row seating for the Kalamaku Show, which includes traditional fire knife dancing. As you sail back to Honolulu, you'll have a magnificent view of the cliffs of the famed Na Pali Coast.</p>
	
 <p>You can't duplicate this Hawaiian land and sea adventure, so book this trip today!	</p>
 		<div>
        <p><strong>Exclusive Owner Adventures Perks:</strong><br />
          </p>
            <ul class="adventureBull">
              <li>2-night, pre-cruise stay at the Waikiki Beach Marriott Resort &amp; Spa</li>
              <li>Pearl Harbor VIP Military Base Tour &amp; WWII Battlefields</li>
              <li>7-night cruise aboard the <em>Pride of America </em></li>
              <li>Welcome Aboard private cocktail party</li>
              <li>$50 Onboard Credit good toward any onboard purchases</li>
              <li>Rainbow Falls &amp; Volcano National Park private group excursion on the Big Island of Hawaii</li>
              <li>Kilohana Manor House, Private Luau &amp; Kalamaku Show group excursion on Kauai</li>
              <li>Chocolate-covered strawberries delivered to your cabin</li>
              <li>Bluegreen Vacations travel professional &amp; executive hosts</li>
              <li>Gift bag</li>
          </ul>
                      
		  <p><strong><br />
	      Price:</strong><br />
		    From $2,499<sup>(1)(2)</sup> per person, based on double occupancy and inside stateroom (airfare not included)<br />
		    <br />
		    <strong>
	        Exclusive benefit for Traveler Plus members—pay with annual, unrestricted Points,

		      a credit card or a combination of both!</strong><br />
		    <br />
		    <strong>Call 800.459.1597 to Book Your Hawaiian Adventure Today!
	      </strong><br />
	      </p>

<div class="BackToTop" style="border-bottom: 1px dotted #666666; padding-bottom: 10px; text-align: right"><a href="#top" class="ttt">Back to Top</a></div>

<h2 id="3" name="3">Treasures of Italy:</h2>
<p><strong>November 2-13, 2014</strong>
	<br />
	 11-night land tour, round-trip from Rome (airfare not included)</p>
		<div>
                  <img src="/TravelerPlus/owner/images/03.jpg" alt="Treasures of Italy" width="152" height="101" class="adventurePic_OA" align="right" />
 		<p>Beautiful cities, ancient ruins, architectural marvels, magnificent churches and stunning landscapes&mdash;Italy holds all these sparkling jewels, plus the world's greatest collection of art and sculpture, tantalizing cuisine and the finest wines. Bluegreen Vacations has planned a treasure hunt of this most romantic and fascinating country next fall, and you won't want to miss it! </p>
        </div>
		<p>Your vacation starts with an unhurried day in Rome, affording you the opportunity for rest and relaxation after your long journey. Meet up with your Bluegreen Vacations Hosts and fellow owners that evening for a guided drive through the Eternal City's historical center, topped off with a scrumptious 4-course meal. You'll have plenty of free time the next day for exploring on your own, shopping and sampling an authentic Italian pizza pie before meeting up with our Globus tour guide for a Welcome Dinner! </p>
   <p> On Day Three your adventure begins in earnest aboard a luxury motorcoach destined for the key sights of Rome, Lucca, Florence, Venice, Assisi and other must-see destinations. Visit architectural and artistic gems such as St. Peter's Square and Basilica, the Sistine Chapel and the Colosseum in Rome and Michelangelo's <em>David</em> in Florence. Enter Venice in style by private boat and see its heirlooms, including the lavish Doges' Palace and the famous Bridge of Sighs. Marvel at the legendary Leaning Tower in Pisa and Juliet's balcony in Verona. Visit St. Francis' Basilica at peaceful Assisi and admire its priceless frescoes. Reach Orvieto, perched high atop a volcanic rock, by modern funicular, or cliff railway. Sample a tasty treat at a local <em>pasticceria</em> before returning to Rome, where you can say "arrivederci" to your fellow treasure hunters at a special Farewell Dinner. </p>
    </strong><br />
    <strong>Price:</strong><br />
From $2,889<sup>(3)(4)</sup> per person, based on double occupancy (airfare not included)<br /> <!--update superscript 12-01-13 Dromero-->
<br />
<strong> Exclusive benefit for Traveler Plus members only&mdash;pay with annual, unrestricted Points, a credit card or a combination of both!</strong><div>
      
      
        <p><table width="497" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td width="336" align="left" valign="middle"><p><strong>Exclusive Owner Adventures Perks:</strong><br />
          </p>
            <ul class="adventureBull">
              <li>1-night, pre-tour stay in Rome</li>
              <li>Roman Highlights at Night guided tour </li>
              <li>Private Welcome Dinner with wine </li>
              <li>Round-trip airport transfers in Rome</li>
              <li>Bluegreen travel professional &amp; executive hosts </li>
              <li>Gift bag </li>
          </ul></td>
        </tr>
        <tr>
          <td colspan="2" align="left" valign="middle"><p><strong><br />
            Tour Package Inclusions:</strong><br />
          </p>
            <ul class="adventureBull">
              <li>Accommodations in first-class hotels in Rome, Lucca, Florence, Venice Island and Assisi</li>
              <li>Full buffet breakfast daily, plus five dinners</li>
              <li>Wine tasting and light dinner with wine at Verrazzano Castle</li>
              <li>Farewell Dinner with wine in Rome</li>
              <li>Luxury motorcoach transportation while on tour</li>
              <li>Admission fees for included tours</li>
              <li>Boat ride to Cinque Terre, weather permitting</li>
              <li>Train ride to Rapallo</li>
              <li>Private boat ride to Venice</li>
              <li>Funicular ride (cliff railway) at Orvieto</li>
              <li>Tour director &amp; local guides (gratuities not included)</li>
          </ul></td>
        </tr>
      </table></p>
      
  </div>
      <p><strong>Call 800.459.1597 to Book Your Treasures of Italy Vacation Today! </strong>
		  <br />
		  <br />
		  <strong>Daily Itinerary (with meals included):</strong><br />
        
        <p><strong>Day 1 &mdash; Rome with Bluegreen Vacations</strong><br />
          Arrive in Rome and have a relaxing afternoon to get refreshed after your long journey. Then enjoy a fabulous evening with your Bluegreen Vacations hosts and fellow owners. Experience the beauty of the city at night on a scenic drive topped off with a private dinner and complimentary wine. (Dinner)<br />
          <br />
  <strong>Day 2 &mdash; Rome on Your Own</strong><br />
          Spend the day exploring Rome on your own. At 5 p.m., meet your Tour Director and traveling companions and leave the hotel for a special welcome dinner with wine at one of Rome's lively restaurants. (Dinner)<br />
  <br />
  <strong>Day 3 &mdash; Rome</strong><br />
          Sightseeing with your Local Guide starts with a visit to the fascinating Vatican Museums and Sistine Chapel, famous for Michelangelo's ceiling paintings and <em>The Last Judgment.</em> Continue to monumental St. Peter's Square and Basilica. Cross the Tiber and visit the Colosseum and the Roman Forum, where Roman legions marched in triumph. To make the most of your stay, join our optional Roman Highlights excursion to see the Spanish Steps, Trevi Fountain and other sites and squares of medieval Rome made famous in the movie <em>Angels and Demons</em>. (Breakfast)<br />
  <br />
  <strong>DAY 4 &mdash; Rome&ndash;Pisa&ndash;Lucca</strong><br />
    Head north along the Tyrrhenian coastline through an area where many a town was founded at the very beginning of recorded history by the mysterious, highly civilized Etruscans. Stop in Pisa for lunch and have someone take the traditional picture of you pushing back the amazing Leaning Tower, 180 feet high and no less than 12 feet out of perpendicular. Then continue to medieval Lucca, completely surrounded by the original medieval wall. An optional dinner at a local <em>agriturismo</em> farm is available. (Breakfast)</p>
        <p><strong>DAY 5 &mdash; Lucca-Excursion to Cinque Terre<br />
      </strong>Drive to La Spezia on the Mediterranean coast and enjoy your excursion to Cinque Terre, a UNESCO World Heritage Site, including a boat ride, weather permitting. The name dates back to the 15th century and is derived from five little medieval villages standing on stony spurs along five miles of the rocky coastline: Riomaggiore, Manarola, Corniglia, Vernazza and Monterosso. From here a train ride brings you to Rapallo. Back in Lucca, enjoy an orientation walk through the town. (Breakfast and Dinner)</p>
        <p><strong>DAY 6 &mdash; Lucca&ndash;Siena&ndash;San Gimignano&ndash;Florence</strong><br />
Enjoy a scenic day in Tuscany. This morning visit Siena and walk through its ancient narrow lanes to beautiful Piazza del Campo, theater of the biannual Palio, Siena's spectacular medieval-style horse race. Next is San Gimignano, located on a hill and the most picturesque of Italy's perfectly preserved medieval towns. In the afternoon, you will drive along the Chianti Road leading north to Florence. Next is the highlight of the day: a stop at splendid Verrazzano Castle to hear about the fine art of blending four types of grapes to make the famous Chianti. Enjoy a wine tasting with an early dinner of local specialties. (Breakfast and Dinner)</p>
      <p><strong>DAY 7 &mdash; Florence</strong><br />
During your walking tour with a Local Guide, visit the Academy of Fine Arts to see Michelangelo's celebrated <em>David</em>. Admire the magnificent marble cathedral, Giotto's Bell Tower, the Baptistry's heavy bronze Gates of Paradise and sculpture-studded Signoria Square. Explore this fascinating city so beautifully filmed in movies like <em>A Room with a View</em> and <em>Tea with Mussolini</em> and browse through the local shops. Leather goods and gold jewelry sold by the ounce are attractive buys. To make the most of your stay, join optional excursions to the magnificent Uffizi Gallery or a dinner outing to a fine Tuscan restaurant. (Breakfast)</p>
      <p><strong>DAY 8 &mdash; Florence&ndash;Verona&ndash;Venice Island</strong><br />
Stop in Verona, setting of Shakespeare's <em>Romeo and Juliet</em>. Take pictures of Juliet's balcony and rub the shining breast on her statue for good luck. Next admire the Arena, an incredibly well-preserved pink marble Roman amphitheater where gladiators used to fight. Built in the first century, it is now the magical venue for world-famous opera performances. Arrive in Venice, a powerful magnet for romantics and art lovers from around the globe. Tonight is your chance to join an optional gondola ride and sample the city's fine restaurants. (Breakfast).</p>
        <p><strong>DAY 9 &mdash; Venice Island</strong><br />
          Enter in style by private boat and enjoy morning sightseeing with your Local Guide. Visit St. Mark's Square and the Byzantine Basilica, lavish Doges' Palace and the world-famous Bridge of Sighs. Also watch skilled glassblowers fashion their delicate objects in the traditional manner. An optional boat ride to the picturesque Island of Burano is available. (Breakfast)<br />
        </p>
        <p><strong>DAY 10 &mdash; Venice Island&ndash;Ravenna&ndash;Assisi</strong><br />
          Today we drive along the coastline of the Adriatic Sea to Ravenna. See the famous mosaics in the 6th-century Basilica of St. Apollinaris in Classe. Next is peaceful Assisi. During your walking tour with a Local Guide, visit St. Clare's Church and St. Francis' Basilica, the hub of a religious order devoted to the ideals of humility, forgiveness, simplicity and love for all God's creatures. Hear about monastic life and admire the priceless frescoes adorning the walls of the church. (Breakfast and Dinner)<br />
        </p>
        <p><strong>DAY 11 &mdash; Assisi&ndash;Orvieto&ndash;Rome</strong><br />
Following the Tiber Valley, reach Orvieto, perched high atop a volcanic rock. Access denied to many a would-be conqueror through the ages is easy for you! Ride a modern funicular (cliff railway) right through the forbidding ramparts. Taste local pastries at a traditional <em>pasticceria</em>. You will have time to browse through tempting shops in the lanes off Piazza del Duomo and to visit the fabulous gothic cathedral and its San Brizio Chapel. In the late afternoon, we return to Rome. This evening, a special farewell dinner with wine awaits you at a local restaurant so you can say, "Arrivederci, Roma!" (Breakfast and Dinner)<br />
    </p>
        <p><strong>DAY 12 &mdash; Rome</strong><br />
Your vacation ends with breakfast this morning. (Breakfast) </p>

<div class="BackToTop" style="border-bottom: 1px dotted #666666; padding-bottom: 10px; text-align: right"><a href="#top" class="ttt">Back to Top</a></div>
<!--here starts Australia under-->
<h2 id="4" name="4">Adventure Down Under:</h2>
<p><strong>January 7-25, 2015</strong>
	<br />
2-night stay in Melbourne, 2-night stay in Sydney, plus a 14-night cruise from Sydney to Singapore</p>
	<p>
    <div>
    <img src="/TravelerPlus/owner/images/australia.jpg" alt="Cuddle a koala - Adventure Down Under " width="152" height="101" class="adventurePic_OA" align="right" />
 	<p align="left">Cuddle a koala, see the sights of Sydney  and Melbourne and even ride an elephant on Bluegreen Vacations&rsquo; Adventure Down  Under, a grand 18-night, land-and-cruise tour of Australia set for January 7–25, 2015. It&rsquo;s the longest, most ambitious Owner Adventures vacation we&rsquo;ve ever  planned, an extravaganza taking you from Melbourne to Sydney and cities along  the northern Australian coast, then beyond to exotic Bali and cosmopolitan Singapore.  It&rsquo;s a once-in-a-lifetime trip for Bluegreen owners and their guests.<strong></strong></p>
    </div>
    </p>
        <p align="left">Australia features a  varied landscape of pristine beaches, sprawling deserts, snow-capped  mountains, tropical rain forests, multicultural cities, stunning architecture  and awe-inspiring natural wonders. In a word, it&rsquo;s diverse! On this Owner  Adventures vacation you&rsquo;ll get a good taste of the amazing variety that  Australia has to offer. </p>
<p align="left">While it will be  winter in the United States, it will be summer in Australia in January, so you  can shed the sweaters, scarves and parkas for shorts, flip flops and a swimsuit.  Many of the cities you&rsquo;ll be visiting will be in Australia&rsquo;s tropical zone.</p>

<p align="left"><strong>Price:</strong><br />
  From $4,299<sup>(5)(6)</sup> per person, based on double occupancy (airfare not  included)<br /></p>
 <p><strong>Exclusive benefit for  Traveler Plus members only—pay with annual Points, a credit card or a  combination of both!</strong><br /></p>
        <p align="left">Ready to head Down  Under?<br /></p>
<p><strong>January 7-8 — Melbourne, Australia</strong><br />
Your adventure starts  in laid-back Melbourne in southern Australia, the most &ldquo;European&rdquo; of  Australia&rsquo;s cities. Your 2-night stay here includes a private guided city tour  with morning tea and lunch on your first day. Refresh and relax in your Deluxe  room at the Rydges Melbourne, a luxury hotel located in the heart of the  Theatre Precinct with a rooftop pool and an abundance of cafes, shops and  entertainment venues nearby. The next day, you&rsquo;ll have a full-day excursion  that includes a ride on the historic Puffing Billy Steam Train through the  scenic mountainous region of the Dandenong Ranges. Sample superb wines and  enjoy lunch at the Fergusson Winery in the Yarra Valley, and then explore the  wildlife at the Healesville Sanctuary. Get an up-close look at iconic  Australian animals like koalas, kangaroos, dingoes and the confounding  platypus. <br />
          <br />
  <strong>January 9-10 — Sydney, Australia</strong><br />
Enjoy your included  buffet breakfast before you fly to Australia&rsquo;s most populous city, Sydney,  where you&rsquo;ll have a half-day sightseeing tour. (NOTE: The flight from Melbourne  to Sydney is not included in your package and must be purchased separately.)  Stop at Mrs. Macquarie&rsquo;s Point, which offers a perfect spot to photograph  beautiful Sydney Harbor, the dramatic Opera House and the Harbor Bridge. You&rsquo;ll  drive closer to these sights on your tour, and then visit world-famous Bondi  Beach. On your second day you&rsquo;ll take an all-day tour that includes a stop at  Featherdale Wildlife Park, where you can feed kangaroos and wallabies or cuddle  a koala. Stop at Echo Point Lookout for panoramic views of the southern Blue  Mountains and the famous Three Sisters rock formation. Lunch at the Carrington  Hotel is included on your tour. See more of the Blue Mountains at Scenic World,  where you can stroll through the rain forest on a scenic walkway, float over  the canopy in a glass-floored Skyway car, ride the steepest incline railway in  the world or journey higher on an aerial cable car. <br />
</p>
<p align="left"><strong>January 11 — Anchors Away on  the <em>Celebrity Century</em></strong><br />
Your 14-day Celebrity  cruise journeys from Sydney to Singapore, with lots of fascinating stops in  between. You&rsquo;ll appreciate the 
    <em>Century&rsquo;s </em>warm, open spaces and soaring atriums,  with world-class restaurants and the totally cool Martini Bar, the first ice bar  at sea where the bartenders will put on a high-energy show for you. This  evening, you&rsquo;ll meet up with your Bluegreen host and fellow travelers at a  private Welcome Aboard cocktail reception. <br />
  <br />
<strong>January</strong><strong> 12-13 — At Sea </strong></p>
<strong>January 14 — Airlie Beach, Australia</strong><br />
Your first port is a  bustling village nestled between the steep mountains of Conway National Park  and the sparkling blue waters of the Coral Sea. A gateway to the Great Barrier  Reef and Whitsunday Islands, it&rsquo;s the perfect location to dive, sail, snorkel  or swim. If you&rsquo;re more of a land-lover, try an optional four-wheel drive tour  through the rain forest, or make the easy walk up to the observation platform on  Mount Rooper to see amazing views of the surrounding islands.
<p><strong>January 15-16 — Cairns, Australia<br />
</strong>The cosmopolitan city  of Cairns in far north Queensland is another delightful launching point for  exploration of the Great Barrier Reef and the prehistoric Daintree rain forest,  both World Heritage Sites. We&rsquo;ve planned a private group excursion to the rain  forest. Ride aboard the historic Kuranda Scenic Railway and ascend 1,076 feet  above sea level along a breathtaking route. Once in the village of Kuranda, you  can browse the craft markets for souvenirs or Aboriginal art. You&rsquo;ll get a  bird&rsquo;s eye view of the forest canopy as you ride the Skyrail Rainforest  Cableway, gliding along in cars that offer 360-degree views. Your ship will  overnight in Cairns, so you&rsquo;ll have plenty of time to take a catamaran cruise  to the Great Barrier Reef, comb the souvenir shops and eateries in town or  spend a day on the man-made saltwater lagoon and sandy beach on the Esplanade.</p>
<strong>January</strong><strong> 17-18 — At Sea </strong>
<p align="left"><strong>January 19 — Darwin, Australia</strong><br />
Darwin is a compact  city on the top end of the continent. The cool breezes that temper the tropical  heat and humidity make this a very outdoors-oriented place, where you&rsquo;ll find  shady parks with exotic flowering trees like frangipani, banyan and tamarind. A  good way to see the major highlights is on a Tour Tub open-air bus. Visit the  Museum and Art Gallery of the Northern Territory, the Overland Telegraph  Museum, Fannie Bay Gaol (historic prison of the queen) and the Botanic Gardens.</p>
<strong>January</strong><strong> 20-21 — At Sea </strong>
<p align="left"><strong>January 22 — Benoa, Bali</strong><br />
Indonesia&rsquo;s top  tourist destination is an exotic paradise of soaring volcanoes, cool lakes,  rushing rivers, lush forests and palm-lined beaches. Scattered around the  &ldquo;Island of the Gods&rdquo; are thousands of Hindu temples and places of worship, and  you&rsquo;ll notice daily rituals, hanging charms&nbsp; and reverence for  the volcanic cone of Gunung Agung, the Holy Mountain. Take a walk on Bali&rsquo;s  wild side on your included Elephant Safari Ride group excursion. Elephant  Safari Park at Taro is acclaimed as one of the world&rsquo;s best such facilities.  Watch the elephants feeding, bathing and &ldquo;painting&rdquo; pictures, and even take a  half-hour ride on one of these magnificent animals through the forest. Your  tour also includes a stop at Tanah Lot, one of the most popular places of  interest in Bali. The unusual rock formation is home to a pilgrimage temple,  the Pura Tanah Lot, and is a cultural icon.</p>
<strong>January</strong><strong> 23-24 — At Sea </strong>
<p><strong>January 25 — Singapore</strong><br />
Your Owner Adventures vacation ends in the wealthy, modern city-state of the Republic of Singapore, one of the world’s great commercial centers. You may head straight to the airport if you like, but why not take advantage of our optional post-cruise stay to experience some of this clean, safe, multicultural city? The post-cruise package, priced from $269 per person based on double occupancy, includes a half-day guided sightseeing tour; a 1-night stay at the Grand Park City Hall hotel, downtown and close to train stations and Singapore attractions; breakfast the following morning; and transfers to the airport.  <br /></p>

<p align="left"><strong>Exclusive Owner Adventures Perks:</strong></p>
<ul>
  <li>2-night stay at the Rydges Melbourne</li>
  <li>Half-day private guided city tour of Melbourne with  morning tea and lunch</li>
  <li>Full-day excursion in Melbourne through the  Dandenong Ranges and Yarra Valley, including a ride on the Puffing Billy Steam  Train, wine tasting and lunch at the Fergusson Winery, plus a visit to the  Healesville Sanctuary</li>
  <li>2-night stay at the Rydges Sydney</li>
  <li>Half-day sightseeing tour of Sydney with a visit to  world-famous Bondi Beach</li>
  <li>Full-day excursion in Sydney, including stops at  Featherdale Wildlife Park, Echo Point Lookout, and Scenic World, plus lunch at  the Carrington Hotel</li>
  <li>$50  Onboard Credit</li>
  <li>Private Welcome Aboard cocktail party</li>
  <li>Chocolate-covered strawberries, fruit plate and a  bottle of wine or champagne delivered to your stateroom </li>
  <li>Kuranda rain forest group excursion in Cairns</li>
  <li>Elephant Safari Park and Tanah Lot group  excursion at Bali, with an elephant ride and lunch at Babek Bengil</li>
  <li>Farewell cocktail party</li>
</ul>
<p align="left"><strong>Cross Australia off  your bucket list in 2015! Call Bluegreen Travel Services at 800.459.1597 to  make your reservations.</strong></p>
<p>&nbsp;</p>
<div class="BackToTop" style="border-bottom: 1px dotted #666666; padding-bottom: 10px; text-align: right"><a href="#top" class="ttt">Back to Top</a></div>
<!--here stops Australia under-->
<div style="color:#999; font-size:10px">
<p><a name="dop1" id="dop1"></a><strong> NORWEGIAN CRUISE LINE DETAILS OF PARTICIPATION:</strong><br />
  (1)Fares shown are in U.S. dollars and are per person, based on double occupancy. Government taxes, fees, and fuel supplement (where applicable) are additional. NCL reserves the right to charge a fuel supplement without prior notice should the closing price of West Texas Intermediate Fuel increase above $65 per barrel on the NYMEX (New York Mercantile Exchange Index). In the event a fuel supplement is charged, NCL will have sole discretion to apply the supplementary charge to both existing and new bookings, regardless of whether such bookings have been paid in full. Such supplementary charges are not included in the cruise fare. The fuel supplement charge will not exceed $10.00 per passenger per day. Prices are based on availability and subject to change. Guests are advised to carefully read the <a style="color:#999;" href="https://www.ncl.com/csimages/602/727/NCL_Guest_Ticket_Contract.pdf" target="_blank">Terms &amp; Conditions of the Guest Ticket Contract</a>, which affect your legal rights and are binding. </p>

<p><a name="dop2" id="2"></a><strong>GREAT VACATION DESTINATIONS DETAILS OF PARTICIPATION</strong><br />
 (2)Price is per person based on double occupancy, cruise only, for select sailing date and stateroom category.  Transfers and Port charges are included. Government taxes and fees are additional. Prices, additional fees, itineraries and availability are subject to change without notice. Fares shown are in U.S. dollars for new reservations only and are subject to availability.  Reservations must be booked by February 14, 2014. Deposit of $250 per person due at time of booking. Final payment due by February 24, 2014. Please call us at 800.459.1597 for more information. Offers include one onboard credit per cabin good toward any onboard purchases, one chocolate covered strawberry platter per cabin, and cocktail party invitation for first and second guests in cabin only. Limit one ticket per paying passenger for included shore excursions, as noted. Onboard service charges are additional, may be automatically added to your onboard account, and are subject to your discretion Certain terms, conditions and restrictions may apply.  These offers may not be combined with any other offers/promotions and may be withdrawn at any time. Not included: air transportation*, items of a personal nature, restaurant fees, some beverages, and photographs. Great Vacation Destinations, doing business as Bluegreen Travel Services, is located at 12400 S. International Drive, Orlando, FL 32821.  Great Vacation Destinations is registered with the State of California as a Seller of travel Reg. No. 2068362-50 (registration as a seller of travel does not constitute approval by the State of California); Washington Seller of travel Reg. No. 602-283-711; and registered with the Airlines Reporting Corporation ("ARC") number 15-72225-4. In the event of a conflict between Norwegian Cruise Line's Details of Participation and Bluegreen Details of Participation, Norwegian Cruise Line's Details of Participation shall govern.</p>
<p>*You can add these during or after the reservation process.<br />
</p>
<p>
  <a name="dop3" id="dop3"></a><strong>GLOBUS DETAILS OF PARTICIPATION:</strong><br /> 
    (3)Price is per person based on double occupancy and includes accommodations, airports transfers to/from hotel, tour transportation, daily breakfasts, and five dinners. Certain terms, conditions, and restrictions may apply. Fares shown are in U.S. dollars for new reservations only and are subject to availability. Not included: Government taxes and fees, gratuities, alcohol, beverages, food outside the contracted Globus menu as presented at a hotel or restaurant, optional excursions, vacation insurance, baggage fees and all other items of a personal nature. Cancellation fees apply: 45 &ndash; 22 days prior to commencement of services: 20% of total price; 21 &ndash; 8 days prior to commencement of services: 30% of total price; 7 &ndash; 1 day(s) prior to commencement of services: 50% of total price; on departure day and later: 100% of total price. Baggage allowance: one suitcase per person not to exceed 30" x 21" x 11" and weight not exceeding 50 lbs. Travelers must report any disability requiring special assistance while on tour at the time the reservation is made.  The purchase of any travel services provided by Group Voyages, Inc., doing business as Globus, Monograms, and Avalon Waterways (hereinafter "Globus") constitutes a contractual agreement between you and Globus, and represents your acceptance of Globus Terms &amp; Conditions, which can be found online at <a style="color:#999;" href="http://www.globusjourneys.com/terms" target="_blank">Globus Journey's Terms</a>. Group Voyages, Inc. located at 5301 South Federal Circle, Littleton, CO 80123, is an independent company licensed to market and distribute travel products under the globus brand name, and arrange for the vacation services offered in its brochure or on its Web site, including transportation, sightseeing, and accommodations through independent contracts. CST#2017032-20.<br />
</p>

<p><a name="dop4" id="dop4"></a><strong>BLUEGREEN DETAILS OF PARTICIPATION:</strong><br />
    (4)Price is per person based on double occupancy and includes 
    accommodations, airport transfer to/from hotel, tour transportation, daily 
    breakfasts, and five dinners. Reservations must be booked by July 16, 2014. Non-refundable, non-transferrable deposit of $250 per person is due at time of booking. Final payment due by August 16, 2014. Prices, additional fees, itineraries, and availability are subject to change without notice. Certain terms, conditions, and restrictions may apply. Please call us at 800.459.1597 for additional information. Fares shown are in U.S. dollars for new reservations only and are subject to availability. Not included: airfare*, government taxes and fees, gratuities, alcohol, beverages, food outside the contracted Globus menu as presented at a hotel or restaurant, optional excursions, vacation insurance, baggage fees and all other items of a personal nature. Great Vacation Destinations, doing business as Bluegreen Travel Services, is located at 12400 S. International Drive, Orlando, FL 32821. Great Vacation Destinations is registered with the State of California as a Seller of travel Reg. No. 2068362-50 (registration as a seller of travel does not constitute approval by the State of California); Washington Seller of travel Reg. No. 602-283-711; and registered with the Airlines Reporting Corporation ("ARC") number 15-72225-4. In the event of a conflict between Globus' Details of Participation and Bluegreen Details of Participation, Globus' Details of Participation shall govern.</p>
<p>*You can add these during or after the reservation process.</p>
<p><a name="dop5" id="dop5"></a><strong>CELEBRITY CRUISES DETAILS OF PARTICIPATION:</strong><br />
		  (5)Prices shown in U.S. dollars. Prices are per person, cruise only, based on double occupancy, specified stateroom category, in USD and subject to availability. Governmental departure taxes and fees of $178.30 per person are additional. In case of currency fluctuations of more than 3.00% or amendments in local government taxes, we reserve the right to adjust the tour price accordingly. Certain restrictions may apply. Celebrity Cruises Inc. reserves the right to change, whether via an increase or decrease, any published rates, including cruise rates and airfare charges, without prior notice. We reserve the right to impose on any existing booking or new bookings (whether paid in full or not) a fuel supplement [of up to $10 per guest per day on all guests if the price of West Texas Intermediate fuel exceeds $65.00 per barrel] or other matters without prior notice as provided in our Passenger Ticket Contract. Click   <a style="color:#999;" href="http://media.celebritycruises.com/celebrity/content/en_US/pdf/Celebrity_CTC_Eff_08_01_10.pdf" target="_blank">here</a> to see Passenger Ticket Contract. In addition, we reserve the right to pass through any third party imposed fuel or other surcharges, also without prior notice. The guest will remain liable for any applicable taxes, fees or surcharges that may be assessed by any governmental or quasi-governmental agencies.</p>
<a name="dop6" id="dop6"></a><strong>GREAT VACATION DESTINATIONS, INC. DETAILS OF PARTICIPATION:</strong><br />
		  (6)Price is per person based on double occupancy, for cruise and hotel only, for select travel date and stateroom category.  Port charges, round-trip airport transfers in Melbourne, Sydney and Singapore, transfers to port in Sydney, baggage handling while on tour (2 bags per person maximum) and pre-paid gratuities are included. Airfare, including the flight from Melbourne to Sydney, is not included. Government taxes and fees of $178.30 are additional. Prices, additional fees, itineraries and availability are subject to change without notice. Fares shown are in U.S. dollars for new reservations only and are subject to availability. Reservations must be booked by October 1, 2014. Deposit of $1,000 per person due at time of booking. Final payment due October 1, 2014. Please call us at 800.459.1597 for more information.  Offer includes one onboard credit per cabin and one order each of chocolate-covered strawberries and a fruit plate, and one bottle of wine or champagne delivered to your cabin. Onboard service charges are additional, may be automatically added to your onboard account, and are subject to your discretion. Limit one ticket per paying passenger for included shore excursions, as noted. Certain terms, conditions and restrictions may apply. These offers may not be combined with any other offers/promotions and may be withdrawn at any time. Not included: air transportation,* car rental,* items of a personal nature, restaurant fees, some beverages, and photographs. Great Vacation Destinations, Inc., doing business as Bluegreen Travel Services, is located at 12400 S. International Drive, Orlando, FL 32821.  Great Vacation Destinations, Inc. is registered with the State of California as a Seller of travel Reg. No. 2068362-50 (registration as a seller of travel does not constitute approval by the State of California); Washington Seller of travel Reg. No. 602-283-711; and registered with the Airlines Reporting Corporation (“ARC”) number 15-72225-4. In the event of a conflict between Celebrity Cruises’ Details of Participation and Great Vacation Destinations’ Details of Participation, Celebrity Cruises’ Details of Participation shall govern.

  <p> *You can add these during or after the reservation process.</p>
    <br />


</div>

<!-- END CONTENT 
//////////////////////////////////////////////// -->







<!--div class="BackToTop"><a href="#top" class="ttt">Back to Top</a></div-->
								</td>
							</tr>
							<tr>
								<td colSpan="3">
									<!-- footer -->
									<includecontrolfooter:footer id="footer" runat="server"></includecontrolfooter:footer>
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
</body>
</html>