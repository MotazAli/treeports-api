using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreePorts.Models;

namespace TreePorts.Utilities
{
    public class CountriesCode
{



	public static string[] CountriesNames = new string[]
{
	"Afghanistan",
	"Albania",
	"Algeria",
	"American Samoa",
	"Andorra",
	"Angola",
	"Anguilla",
	"Antarctica",
	"Antigua and Barbuda",
	"Argentina",
	"Armenia",
	"Aruba",
	"Australia",
	"Austria",
	"Azerbaijan",
	"Bahamas",
	"Bahrain",
	"Bangladesh",
	"Barbados",
	"Belarus",
	"Belgium",
	"Belize",
	"Benin",
	"Bermuda",
	"Bhutan",
	"Bolivia",
	"Bosnia and Herzegovina",
	"Botswana",
	"Bouvet Island",
	"Brazil",
	"British Indian Ocean Territory",
	"Brunei Darussalam",
	"Bulgaria",
	"Burkina Faso",
	"Burundi",
	"Cambodia",
	"Cameroon",
	"Canada",
	"Cape Verde",
	"Cayman Islands",
	"Central African Republic",
	"Chad",
	"Chile",
	"China",
	"Christmas Island",
	"Cocos (Keeling) Islands",
	"Colombia",
	"Comoros",
	"Congo",
	"Congo, the Democratic Republic of the",
	"Cook Islands",
	"Costa Rica",
	"Cote D'Ivoire",
	"Croatia",
	"Cuba",
	"Cyprus",
	"Czech Republic",
	"Denmark",
	"Djibouti",
	"Dominica",
	"Dominican Republic",
	"Ecuador",
	"Egypt",
	"El Salvador",
	"Equatorial Guinea",
	"Eritrea",
	"Estonia",
	"Ethiopia",
	"Falkland Islands (Malvinas)",
	"Faroe Islands",
	"Fiji",
	"Finland",
	"France",
	"French Guiana",
	"French Polynesia",
	"French Southern Territories",
	"Gabon",
	"Gambia",
	"Georgia",
	"Germany",
	"Ghana",
	"Gibraltar",
	"Greece",
	"Greenland",
	"Grenada",
	"Guadeloupe",
	"Guam",
	"Guatemala",
	"Guinea",
	"Guinea-Bissau",
	"Guyana",
	"Haiti",
	"Heard Island and Mcdonald Islands",
	"Holy See (Vatican City State)",
	"Honduras",
	"Hong Kong",
	"Hungary",
	"Iceland",
	"India",
	"Indonesia",
	"Iran, Islamic Republic of",
	"Iraq",
	"Ireland",
	"Israel",
	"Italy",
	"Jamaica",
	"Japan",
	"Jordan",
	"Kazakhstan",
	"Kenya",
	"Kiribati",
	"Korea, Democratic People's Republic of",
	"Korea, Republic of",
	"Kuwait",
	"Kyrgyzstan",
	"Lao People's Democratic Republic",
	"Latvia",
	"Lebanon",
	"Lesotho",
	"Liberia",
	"Libyan Arab Jamahiriya",
	"Liechtenstein",
	"Lithuania",
	"Luxembourg",
	"Macao",
	"Macedonia, the Former Yugoslav Republic of",
	"Madagascar",
	"Malawi",
	"Malaysia",
	"Maldives",
	"Mali",
	"Malta",
	"Marshall Islands",
	"Martinique",
	"Mauritania",
	"Mauritius",
	"Mayotte",
	"Mexico",
	"Micronesia, Federated States of",
	"Moldova, Republic of",
	"Monaco",
	"Mongolia",
	"Montserrat",
	"Morocco",
	"Mozambique",
	"Myanmar",
	"Namibia",
	"Nauru",
	"Nepal",
	"Netherlands",
	"Netherlands Antilles",
	"New Caledonia",
	"New Zealand",
	"Nicaragua",
	"Niger",
	"Nigeria",
	"Niue",
	"Norfolk Island",
	"Northern Mariana Islands",
	"Norway",
	"Oman",
	"Pakistan",
	"Palau",
	"Palestinian Territory, Occupied",
	"Panama",
	"Papua New Guinea",
	"Paraguay",
	"Peru",
	"Philippines",
	"Pitcairn",
	"Poland",
	"Portugal",
	"Puerto Rico",
	"Qatar",
	"Reunion",
	"Romania",
	"Russian Federation",
	"Rwanda",
	"Saint Helena",
	"Saint Kitts and Nevis",
	"Saint Lucia",
	"Saint Pierre and Miquelon",
	"Saint Vincent and the Grenadines",
	"Samoa",
	"San Marino",
	"Sao Tome and Principe",
	"Saudi Arabia",
	"Senegal",
	"Serbia and Montenegro",
	"Seychelles",
	"Sierra Leone",
	"Singapore",
	"Slovakia",
	"Slovenia",
	"Solomon Islands",
	"Somalia",
	"South Africa",
	"South Georgia and the South Sandwich Islands",
	"Spain",
	"Sri Lanka",
	"Sudan",
	"Suriname",
	"Svalbard and Jan Mayen",
	"Swaziland",
	"Sweden",
	"Switzerland",
	"Syrian Arab Republic",
	"Taiwan, Province of China",
	"Tajikistan",
	"Tanzania, United Republic of",
	"Thailand",
	"Timor-Leste",
	"Togo",
	"Tokelau",
	"Tonga",
	"Trinidad and Tobago",
	"Tunisia",
	"Turkey",
	"Turkmenistan",
	"Turks and Caicos Islands",
	"Tuvalu",
	"Uganda",
	"Ukraine",
	"United Arab Emirates",
	"United Kingdom",
	"United States",
	"United States Minor Outlying Islands",
	"Uruguay",
	"Uzbekistan",
	"Vanuatu",
	"Venezuela",
	"Viet Nam",
	"Virgin Islands, British",
	"Virgin Islands, US",
	"Wallis and Futuna",
	"Western Sahara",
	"Yemen",
	"Zambia",
	"Zimbabwe",
};

	/// <summary>
	/// Country abbreviations
	/// </summary>
	public static string[] Abbreviations = new string[]
	{
	"AF",
	"AL",
	"DZ",
	"AS",
	"AD",
	"AO",
	"AI",
	"AQ",
	"AG",
	"AR",
	"AM",
	"AW",
	"AU",
	"AT",
	"AZ",
	"BS",
	"BH",
	"BD",
	"BB",
	"BY",
	"BE",
	"BZ",
	"BJ",
	"BM",
	"BT",
	"BO",
	"BA",
	"BW",
	"BV",
	"BR",
	"IO",
	"BN",
	"BG",
	"BF",
	"BI",
	"KH",
	"CM",
	"CA",
	"CV",
	"KY",
	"CF",
	"TD",
	"CL",
	"CN",
	"CX",
	"CC",
	"CO",
	"KM",
	"CG",
	"CD",
	"CK",
	"CR",
	"CI",
	"HR",
	"CU",
	"CY",
	"CZ",
	"DK",
	"DJ",
	"DM",
	"DO",
	"EC",
	"EG",
	"SV",
	"GQ",
	"ER",
	"EE",
	"ET",
	"FK",
	"FO",
	"FJ",
	"FI",
	"FR",
	"GF",
	"PF",
	"TF",
	"GA",
	"GM",
	"GE",
	"DE",
	"GH",
	"GI",
	"GR",
	"GL",
	"GD",
	"GP",
	"GU",
	"GT",
	"GN",
	"GW",
	"GY",
	"HT",
	"HM",
	"VA",
	"HN",
	"HK",
	"HU",
	"IS",
	"IN",
	"ID",
	"IR",
	"IQ",
	"IE",
	"IL",
	"IT",
	"JM",
	"JP",
	"JO",
	"KZ",
	"KE",
	"KI",
	"KP",
	"KR",
	"KW",
	"KG",
	"LA",
	"LV",
	"LB",
	"LS",
	"LR",
	"LY",
	"LI",
	"LT",
	"LU",
	"MO",
	"MK",
	"MG",
	"MW",
	"MY",
	"MV",
	"ML",
	"MT",
	"MH",
	"MQ",
	"MR",
	"MU",
	"YT",
	"MX",
	"FM",
	"MD",
	"MC",
	"MN",
	"MS",
	"MA",
	"MZ",
	"MM",
	"NA",
	"NR",
	"NP",
	"NL",
	"AN",
	"NC",
	"NZ",
	"NI",
	"NE",
	"NG",
	"NU",
	"NF",
	"MP",
	"NO",
	"OM",
	"PK",
	"PW",
	"PS",
	"PA",
	"PG",
	"PY",
	"PE",
	"PH",
	"PN",
	"PL",
	"PT",
	"PR",
	"QA",
	"RE",
	"RO",
	"RU",
	"RW",
	"SH",
	"KN",
	"LC",
	"PM",
	"VC",
	"WS",
	"SM",
	"ST",
	"SA",
	"SN",
	"CS",
	"SC",
	"SL",
	"SG",
	"SK",
	"SI",
	"SB",
	"SO",
	"ZA",
	"GS",
	"ES",
	"LK",
	"SD",
	"SR",
	"SJ",
	"SZ",
	"SE",
	"CH",
	"SY",
	"TW",
	"TJ",
	"TZ",
	"TH",
	"TL",
	"TG",
	"TK",
	"TO",
	"TT",
	"TN",
	"TR",
	"TM",
	"TC",
	"TV",
	"UG",
	"UA",
	"AE",
	"GB",
	"US",
	"UM",
	"UY",
	"UZ",
	"VU",
	"VE",
	"VN",
	"VG",
	"VI",
	"WF",
	"EH",
	"YE",
	"ZM",
	"ZW"
	};




		//public static string[] CountriesAreaCodes = new string[]{ "93", "355", "213",
		//		"376", "244", "672", "54", "374", "297", "61", "43", "994", "973",
		//		"880", "375", "32", "501", "229", "975", "591", "387", "267", "55",
		//		"673", "359", "226", "95", "257", "855", "237", "1", "238", "236",
		//		"235", "56", "86", "61", "61", "57", "269", "242", "682", "506",
		//		"385", "53", "357", "420", "45", "253", "670", "593", "20", "503",
		//		"240", "291", "372", "251", "500", "298", "679", "358", "33",
		//		"689", "241", "220", "995", "49", "233", "350", "30", "299", "502",
		//		"224", "245", "592", "509", "504", "852", "36", "91", "62", "98",
		//		"964", "353", "44", "972", "39", "225", "1876", "81", "962", "7",
		//		"254", "686", "965", "996", "856", "371", "961", "266", "231",
		//		"218", "423", "370", "352", "853", "389", "261", "265", "60",
		//		"960", "223", "356", "692", "222", "230", "262", "52", "691",
		//		"373", "377", "976", "382", "212", "258", "264", "674", "977",
		//		"31", "687", "64", "505", "227", "234", "683", "850", "47", "968",
		//		"92", "680", "507", "675", "595", "51", "63", "870", "48", "351",
		//		"1", "974", "40", "7", "250", "590", "685", "378", "239", "966",
		//		"221", "381", "248", "232", "65", "421", "386", "677", "252", "27",
		//		"82", "34", "94", "290", "508", "249", "597", "268", "46", "41",
		//		"963", "886", "992", "255", "66", "228", "690", "676", "216", "90",
		//		"993", "688", "971", "256", "44", "380", "598", "1", "998", "678",
		//		"39", "58", "84", "681", "967", "260", "263" };





		public static long[] CountriesAreaCodes = new long[]{
			93



		   ,355


			,213


			,1684


			,376


			,244


			,1264


			,672


			,1268


			,54


			,374


		  ,297


			,61


			,43


			,994


			,1242


			,973


			,880


			,1246


		   ,375


			,32


			,501


			,229


			,1441


			,975


			,591


			,387


			,267


			,55


			,246


			,1284


			,673


			,359


			,226


			,257


			,855


			,237


			,1


			,238


			,1345


			,236


			,235


			,56


		   ,86


			,61


			,61


			,57


			,269


			,682


			,506


			,385


			,53


			,599


			,357


			,420


			,243


		   ,45


			,253


			,1767


			,1809


			,670


			,593


			,20


		   ,503


		   ,240


			,291


			,372


			,251


			,500


			,298


		   ,679


			,358


			,33


			,689


			,241


			,220


			,995


		   ,49


			,233


			,350


			,30


			,299


		   ,1473


			,1671


			,502


			,441481


		   ,224


			,245


			,592


			,509


			,504


		   ,852


			,36


			,354


		   ,91


			,62


			,98


		   ,964


			,53


		 ,441624


		   ,972


			,39


		   ,225


		   ,1876


			,81


			,441534


			,962


		   ,7


			,254


		   ,686


			,383


			,965


		   ,996


			,856


		   ,371


			,961


			,266


			,231


			,218


		   ,423


		  ,370


			,352


			,853


		   ,389


		   ,261


		  ,265


		   ,60


		   ,960


			,223


			,356


			,692


		  ,222


		   ,230


		   ,262


		   ,52


		   ,691


		   ,373


		 ,377


			,976


			,382


		   ,1664


			,212


			,258


		  ,95


		   ,264


			,674


		   ,977


			,31


			,599


			,687


		  ,64



			,505


			,227


			,234


			,683


			,850


			,1670


			,47


		   ,968


		   ,92


			,680


			,970


			,507


			,675


			,595


		   ,51


		   ,63


		   ,64


			,48


			,351


		   ,1787


			,974


		   ,242


			,262


		   ,40


		  ,7


			,250


			,590


			,290


			,1869


			,1758


			,590


			,508


			,1784


			,685


			,378


			,239


			,966


			,221


			,381


			,248


		   ,232


		   ,65


		   ,1721


			,421


			,386


			,677


			,252


		  ,27


		   ,82


		   ,211


		   ,34


			,94


		  ,249


		 ,597


		   ,47


			,268


		   ,46


			,41


		   ,963


			,886


			,992


			,255


		   ,66


			,228


		   ,690


			,676


		   ,1868


		   ,216


			,90


		   ,993


			,1649


			,688


			,1340


			,256


			,380


		   ,971


			,44


			,1


		   ,598


			,998


			,678


			,379


			,58


			,84


		   ,681


			,212


			,967


			,260


			,263};


        public static List<Country> allConutries = new List<Country>() {


         new Country(){ Name ="Afghanistan",
            Code=(long)93,
            Iso="AF"}

        ,
        new Country(){
            Name ="Albania",
            Code=(long)355,
            Iso="AL"}

        ,
        new Country(){
            Name ="Algeria",
            Code=(long)213,
            Iso="DZ"}

        ,
        new Country(){
            Name ="American Samoa",
            Code=(long)1684,
            Iso="AS"}

        ,
        new Country(){
            Name ="Andorra",
            Code=(long)376,
            Iso="AD"}

        ,
        new Country(){
            Name ="Angola",
            Code=(long)244,
            Iso="AO"}

        ,
        new Country(){
            Name ="Anguilla",
            Code=(long)1264,
            Iso="AI"}

        ,
        new Country(){
            Name ="Antarctica",
            Code=(long)672,
            Iso="AQ"}

        ,
        new Country(){
            Name ="Antigua and Barbuda",
            Code=(long)1268,
            Iso="AG"}

        ,
        new Country(){
            Name ="Argentina",
            Code=(long)54,
            Iso="AR"}

        ,
        new Country(){
            Name ="Armenia",
            Code=(long)374,
            Iso="AM"}

        ,
        new Country(){
            Name ="Aruba",
            Code=(long)297,
            Iso="AW"}

        ,
        new Country(){
            Name ="Australia",
            Code=(long)61,
            Iso="AU"}

        ,
        new Country(){
            Name ="Austria",
            Code=(long)43,
            Iso="AT"}

        ,
        new Country(){
            Name ="Azerbaijan",
            Code=(long)994,
            Iso="AZ"}

        ,
        new Country(){
            Name ="Bahamas",
            Code=(long)1242,
            Iso="BS"}

        ,
        new Country(){
            Name ="Bahrain",
            Code=(long)973,
            Iso="BH"}

        ,
        new Country(){
            Name ="Bangladesh",
            Code=(long)880,
            Iso="BD"}

        ,
        new Country(){
            Name ="Barbados",
            Code=(long)1246,
            Iso="BB"}

        ,
        new Country(){
            Name ="Belarus",
            Code=(long)375,
            Iso="BY"}

        ,
        new Country(){
            Name ="Belgium",
            Code=(long)32,
            Iso="BE"}

        ,
        new Country(){
            Name ="Belize",
            Code=(long)501,
            Iso="BZ"}

        ,
        new Country(){
            Name ="Benin",
            Code=(long)229,
            Iso="BJ"}

        ,
        new Country(){
            Name ="Bermuda",
            Code=(long)1441,
            Iso="BM"}

        ,
        new Country(){
            Name ="Bhutan",
            Code=(long)975,
            Iso="BT"}

        ,
        new Country(){
            Name ="Bolivia",
            Code=(long)591,
            Iso="BO"}

        ,
        new Country(){
            Name ="Bosnia and Herzegovina",
            Code=(long)387,
            Iso="BA"}

        ,
        new Country(){
            Name ="Botswana",
            Code=(long)267,
            Iso="BW"}

        ,
        new Country(){
            Name ="Brazil",
            Code=(long)55,
            Iso="BR"}

        ,
        new Country(){
            Name ="British Indian Ocean Territory",
            Code=(long)246,
            Iso="IO"}

        ,
        new Country(){
            Name ="British Virgin Islands",
            Code=(long)1284,
            Iso="VG"}

        ,
        new Country(){
            Name ="Brunei",
            Code=(long)673,
            Iso="BN"}

        ,
        new Country(){
            Name ="Bulgaria",
            Code=(long)359,
            Iso="BG"}

        ,
        new Country(){
            Name ="Burkina Faso",
            Code=(long)226,
            Iso="BF"}

        ,
        new Country(){
            Name ="Burundi",
            Code=(long)257,
            Iso="BI"}

        ,
        new Country(){
            Name ="Cambodia",
            Code=(long)855,
            Iso="KH"}

        ,
        new Country(){
            Name ="Cameroon",
            Code=(long)237,
            Iso="CM"}

        ,
        new Country(){
            Name ="Canada",
            Code=(long)1,
            Iso="CA"}

        ,
        new Country(){
            Name ="Cape Verde",
            Code=(long)238,
            Iso="CV"}

        ,
        new Country(){
            Name ="Cayman Islands",
            Code=(long)1345,
            Iso="KY"}

        ,
        new Country(){
            Name ="Central African Republic",
            Code=(long)236,
            Iso="CF"}

        ,
        new Country(){
            Name ="Chad",
            Code=(long)235,
            Iso="TD"}

        ,
        new Country(){
            Name ="Chile",
            Code=(long)56,
            Iso="CL"}

        ,
        new Country(){
            Name ="China",
            Code=(long)86,
            Iso="CN"}

        ,
        new Country(){
            Name ="Christmas Island",
            Code=(long)61,
            Iso="CX"}

        ,
        new Country(){
            Name ="Cocos Islands",
            Code=(long)61,
            Iso="CC"}

        ,
        new Country(){
            Name ="Colombia",
            Code=(long)57,
            Iso="CO"}

        ,
        new Country(){
            Name ="Comoros",
            Code=(long)269,
            Iso="KM"}

        ,
        new Country(){
            Name ="Cook Islands",
            Code=(long)682,
            Iso="CK"}

        ,
        new Country(){
            Name ="Costa Rica",
            Code=(long)506,
            Iso="CR"}

        ,
        new Country(){
            Name ="Croatia",
            Code=(long)385,
            Iso="HR"}

        ,
        new Country(){
            Name ="Cuba",
            Code=(long)53,
            Iso="CU"}

        ,
        new Country(){
            Name ="Curacao",
            Code=(long)599,
            Iso="CW"}

        ,
        new Country(){
            Name ="Cyprus",
            Code=(long)357,
            Iso="CY"}

        ,
        new Country(){
            Name ="Czech Republic",
            Code=(long)420,
            Iso="CZ"}

        ,
        new Country(){
            Name ="Democratic Republic of the Congo",
            Code=(long)243,
            Iso="CD"}

        ,
        new Country(){
            Name ="Denmark",
            Code=(long)45,
            Iso="DK"}

        ,
        new Country(){
            Name ="Djibouti",
            Code=(long)253,
            Iso="DJ"}

        ,
        new Country(){
            Name ="Dominica",
            Code=(long)1767,
            Iso="DM"}

        ,
        new Country(){
            Name ="Dominican Republic",
            Code=(long)1809,
            Iso="DO"}

        ,
         new Country(){
            Name ="Dominican Republic",
            Code=(long)1829,
            Iso="DO"}

        ,
          new Country(){
            Name ="Dominican Republic",
            Code=(long)1849,
            Iso="DO"}

        ,
        new Country(){
            Name ="East Timor",
            Code=(long)670,
            Iso="TL"}

        ,
        new Country(){
            Name ="Ecuador",
            Code=(long)593,
            Iso="EC"}

        ,
        new Country(){
            Name ="Egypt",
            Code=(long)20,
            Iso="EG"}

        ,
        new Country(){
            Name ="El Salvador",
            Code=(long)503,
            Iso="SV"}

        ,
        new Country(){
            Name ="Equatorial Guinea",
            Code=(long)240,
            Iso="GQ"}

        ,
        new Country(){
            Name ="Eritrea",
            Code=(long)291,
            Iso="ER"}

        ,
        new Country(){
            Name ="Estonia",
            Code=(long)372,
            Iso="EE"}

        ,
        new Country(){
            Name ="Ethiopia",
            Code=(long)251,
            Iso="ET"}

        ,
        new Country(){
            Name ="Falkland Islands",
            Code=(long)500,
            Iso="FK"}

        ,
        new Country(){
            Name ="Faroe Islands",
            Code=(long)298,
            Iso="FO"}

        ,
        new Country(){
            Name ="Fiji",
            Code=(long)679,
            Iso="FJ"}

        ,
        new Country(){
            Name ="Finland",
            Code=(long)358,
            Iso="FI"}

        ,
        new Country(){
            Name ="France",
            Code=(long)33,
            Iso="FR"}

        ,
        new Country(){
            Name ="French Polynesia",
            Code=(long)689,
            Iso="PF"}

        ,
        new Country(){
            Name ="Gabon",
            Code=(long)241,
            Iso="GA"}

        ,
        new Country(){
            Name ="Gambia",
            Code=(long)220,
            Iso="GM"}

        ,
        new Country(){
            Name ="Georgia",
            Code=(long)995,
            Iso="GE"}

        ,
        new Country(){
            Name ="Germany",
            Code=(long)49,
            Iso="DE"}

        ,
        new Country(){
            Name ="Ghana",
            Code=(long)233,
            Iso="GH"}

        ,
        new Country(){
            Name ="Gibraltar",
            Code=(long)350,
            Iso="GI"}

        ,
        new Country(){
            Name ="Greece",
            Code=(long)30,
            Iso="GR"}

        ,
        new Country(){
            Name ="Greenland",
            Code=(long)299,
            Iso="GL"}

        ,
        new Country(){
            Name ="Grenada",
            Code=(long)1473,
            Iso="GD"}

        ,
        new Country(){
            Name ="Guam",
            Code=(long)1671,
            Iso="GU"}

        ,
        new Country(){
            Name ="Guatemala",
            Code=(long)502,
            Iso="GT"}

        ,
        new Country(){
            Name ="Guernsey",
            Code=(long)44,
            Iso="GG"}

        ,
        new Country(){
            Name ="Guernsey",
            Code=(long)1481,
            Iso="GG"}

        ,
        new Country(){
            Name ="Guinea",
            Code=(long)224,
            Iso="GN"}

        ,
        new Country(){
            Name ="Guinea Bissau",
            Code=(long)245,
            Iso="GW"}

        ,
        new Country(){
            Name ="Guyana",
            Code=(long)592,
            Iso="GY"}

        ,
        new Country(){
            Name ="Haiti",
            Code=(long)509,
            Iso="HT"}

        ,
        new Country(){
            Name ="Honduras",
            Code=(long)504,
            Iso="HN"}

        ,
        new Country(){
            Name ="Hong Kong",
            Code=(long)852,
            Iso="HK"}

        ,
        new Country(){
            Name ="Hungary",
            Code=(long)36,
            Iso="HU"}

        ,
        new Country(){
            Name ="Iceland",
            Code=(long)354,
            Iso="IS"}

        ,
        new Country(){
            Name ="India",
            Code=(long)91,
            Iso="IN"}

        ,
        new Country(){
            Name ="Indonesia",
            Code=(long)62,
            Iso="ID"}

        ,
        new Country(){
            Name ="Iran",
            Code=(long)98,
            Iso="IR"}

        ,
        new Country(){
            Name ="Iraq",
            Code=(long)964,
            Iso="IQ"}

        ,
        new Country(){
            Name ="Ireland",
            Code=(long)353,
            Iso="IE"}

        ,
        new Country(){
            Name ="Isle of Man",
            Code=(long)441624,
            Iso="IM"}

        ,
        new Country(){
            Name ="Israel",
            Code=(long)972,
            Iso="IL"}

        ,
        new Country(){
            Name ="Italy",
            Code=(long)39,
            Iso="IT"}

        ,
        new Country(){
            Name ="Ivory Coast",
            Code=(long)225,
            Iso="CI"}

        ,
        new Country(){
            Name ="Jamaica",
            Code=(long)1876,
            Iso="JM"}

        ,
        new Country(){
            Name ="Japan",
            Code=(long)81,
            Iso="JP"}

        ,
        new Country(){
            Name ="Jersey",
            Code=(long)441534,
            Iso="JE"}

        ,
        new Country(){
            Name ="Jordan",
            Code=(long)962,
            Iso="JO"}

        ,
        new Country(){
            Name ="Kazakhstan",
            Code=(long)7,
            Iso="KZ"}

        ,
        new Country(){
            Name ="Kenya",
            Code=(long)254,
            Iso="KE"}

        ,
        new Country(){
            Name ="Kiribati",
            Code=(long)686,
            Iso="KI"}

        ,
        new Country(){
            Name ="Kosovo",
            Code=(long)383,
            Iso="XK"}

        ,
        new Country(){
            Name ="Kuwait",
            Code=(long)965,
            Iso="KW"}

        ,
        new Country(){
            Name ="Kyrgyzstan",
            Code=(long)996,
            Iso="KG"}

        ,
        new Country(){
            Name ="Laos",
            Code=(long)856,
            Iso="LA"}

        ,
        new Country(){
            Name ="Latvia",
            Code=(long)371,
            Iso="LV"}

        ,
        new Country(){
            Name ="Lebanon",
            Code=(long)961,
            Iso="LB"}

        ,
        new Country(){
            Name ="Lesotho",
            Code=(long)266,
            Iso="LS"}

        ,
        new Country(){
            Name ="Liberia",
            Code=(long)231,
            Iso="LR"}

        ,
        new Country(){
            Name ="Libya",
            Code=(long)218,
            Iso="LY"}

        ,
        new Country(){
            Name ="Liechtenstein",
            Code=(long)423,
            Iso="LI"}

        ,
        new Country(){
            Name ="Lithuania",
            Code=(long)370,
            Iso="LT"}

        ,
        new Country(){
            Name ="Luxembourg",
            Code=(long)352,
            Iso="LU"}

        ,
        new Country(){
            Name ="Macao",
            Code=(long)853,
            Iso="MO"}

        ,
        new Country(){
            Name ="Macedonia",
            Code=(long)389,
            Iso="MK"}

        ,
        new Country(){
            Name ="Madagascar",
            Code=(long)261,
            Iso="MG"}

        ,
        new Country(){
            Name ="Malawi",
            Code=(long)265,
            Iso="MW"}

        ,
        new Country(){
            Name ="Malaysia",
            Code=(long)60,
            Iso="MY"}

        ,
        new Country(){
            Name ="Maldives",
            Code=(long)960,
            Iso="MV"}

        ,
        new Country(){
            Name ="Mali",
            Code=(long)223,
            Iso="ML"}

        ,
        new Country(){
            Name ="Malta",
            Code=(long)356,
            Iso="MT"}

        ,
        new Country(){
            Name ="Marshall Islands",
            Code=(long)692,
            Iso="MH"}

        ,
        new Country(){
            Name ="Mauritania",
            Code=(long)222,
            Iso="MR"}

        ,
        new Country(){
            Name ="Mauritius",
            Code=(long)230,
            Iso="MU"}

        ,
        new Country(){
            Name ="Mayotte",
            Code=(long)262,
            Iso="YT"}

        ,
        new Country(){
            Name ="Mexico",
            Code=(long)52,
            Iso="MX"}

        ,
        new Country(){
            Name ="Micronesia",
            Code=(long)691,
            Iso="FM"}

        ,
        new Country(){
            Name ="Moldova",
            Code=(long)373,
            Iso="MD"}

        ,
        new Country(){
            Name ="Monaco",
            Code=(long)377,
            Iso="MC"}

        ,
        new Country(){
            Name ="Mongolia",
            Code=(long)976,
            Iso="MN"}

        ,
        new Country(){
            Name ="Montenegro",
            Code=(long)382,
            Iso="ME"}

        ,
        new Country(){
            Name ="Montserrat",
            Code=(long)1664,
            Iso="MS"}

        ,
        new Country(){
            Name ="Morocco",
            Code=(long)212,
            Iso="MA"}

        ,
        new Country(){
            Name ="Mozambique",
            Code=(long)258,
            Iso="MZ"}

        ,
        new Country(){
            Name ="Myanmar",
            Code=(long)95,
            Iso="MM"}

        ,
        new Country(){
            Name ="Namibia",
            Code=(long)264,
            Iso="NA"}

        ,
        new Country(){
            Name ="Nauru",
            Code=(long)674,
            Iso="NR"}

        ,
        new Country(){
            Name ="Nepal",
            Code=(long)977,
            Iso="NP"}

        ,
        new Country(){
            Name ="Netherlands",
            Code=(long)31,
            Iso="NL"}

        ,
        new Country(){
            Name ="Netherlands Antilles",
            Code=(long)599,
            Iso="AN"}

        ,
        new Country(){
            Name ="New Caledonia",
            Code=(long)687,
            Iso="NC"}

        ,
        new Country(){
            Name ="New Zealand",
            Code=(long)64,
            Iso="NZ"}

        ,
        new Country(){
            Name ="Nicaragua",
            Code=(long)505,
            Iso="NI"}

        ,
        new Country(){
            Name ="Niger",
            Code=(long)227,
            Iso="NE"}

        ,
        new Country(){
            Name ="Nigeria",
            Code=(long)234,
            Iso="NG"}

        ,
        new Country(){
            Name ="Niue",
            Code=(long)683,
            Iso="NU"}

        ,
        new Country(){
            Name ="North Korea",
            Code=(long)850,
            Iso="KP"}

        ,
        new Country(){
            Name ="Northern Mariana Islands",
            Code=(long)1670,
            Iso="MP"}

        ,
        new Country(){
            Name ="Norway",
            Code=(long)47,
            Iso="NO"}

        ,
        new Country(){
            Name ="Oman",
            Code=(long)968,
            Iso="OM"}

        ,
        new Country(){
            Name ="Pakistan",
            Code=(long)92,
            Iso="PK"}

        ,
        new Country(){
            Name ="Palau",
            Code=(long)680,
            Iso="PW"}

        ,
        new Country(){
            Name ="Palestine",
            Code=(long)970,
            Iso="PS"}

        ,
        new Country(){
            Name ="Panama",
            Code=(long)507,
            Iso="PA"}

        ,
        new Country(){
            Name ="Papua New Guinea",
            Code=(long)675,
            Iso="PG"}

        ,
        new Country(){
            Name ="Paraguay",
            Code=(long)595,
            Iso="PY"}

        ,
        new Country(){
            Name ="Peru",
            Code=(long)51,
            Iso="PE"}

        ,
        new Country(){
            Name ="Philippines",
            Code=(long)63,
            Iso="PH"}

        ,
        new Country(){
            Name ="Pitcairn",
            Code=(long)64,
            Iso="PN"}

        ,
        new Country(){
            Name ="Poland",
            Code=(long)48,
            Iso="PL"}

        ,
        new Country(){
            Name ="Portugal",
            Code=(long)351,
            Iso="PT"}

        ,
        new Country(){
            Name ="Puerto Rico",
            Code=(long)1787,
            Iso="PR"}

        ,
        new Country(){
            Name ="Puerto Rico",
            Code=(long)1939,
            Iso="PR"}

        ,
        new Country(){
            Name ="Qatar",
            Code=(long)974,
            Iso="QA"}

        ,
        new Country(){
            Name ="Republic of the Congo",
            Code=(long)242,
            Iso="CG"}

        ,
        new Country(){
            Name ="Reunion",
            Code=(long)262,
            Iso="RE"}

        ,
        new Country(){
            Name ="Romania",
            Code=(long)40,
            Iso="RO"}

        ,
        new Country(){
            Name ="Russia",
            Code=(long)7,
            Iso="RU"}

        ,
        new Country(){
            Name ="Rwanda",
            Code=(long)250,
            Iso="RW"}

        ,
        new Country(){
            Name ="Saint Barthelemy",
            Code=(long)590,
            Iso="BL"}

        ,
        new Country(){
            Name ="Saint Helena",
            Code=(long)290,
            Iso="SH"}

        ,
        new Country(){
            Name ="Saint Kitts and Nevis",
            Code=(long)1869,
            Iso="KN"}

        ,
        new Country(){
            Name ="Saint Lucia",
            Code=(long)1758,
            Iso="LC"}

        ,
        new Country(){
            Name ="Saint Martin",
            Code=(long)590,
            Iso="MF"}

        ,
        new Country(){
            Name ="Saint Pierre and Miquelon",
            Code=(long)508,
            Iso="PM"}

        ,
        new Country(){
            Name ="Saint Vincent and the Grenadines",
            Code=(long)1784,
            Iso="VC"}

        ,
        new Country(){
            Name ="Samoa",
            Code=(long)685,
            Iso="WS"}

        ,
        new Country(){
            Name ="San Marino",
            Code=(long)378,
            Iso="SM"}

        ,
        new Country(){
            Name ="Sao Tome and Principe",
            Code=(long)239,
            Iso="ST"}

        ,
        new Country(){
            Name ="Saudi Arabia",
            Code=(long)966,
            Iso="SA"}

        ,
        new Country(){
            Name ="Senegal",
            Code=(long)221,
            Iso="SN"}

        ,
        new Country(){
            Name ="Serbia",
            Code=(long)381,
            Iso="RS"}

        ,
        new Country(){
            Name ="Seychelles",
            Code=(long)248,
            Iso="SC"}

        ,
        new Country(){
            Name ="Sierra Leone",
            Code=(long)232,
            Iso="SL"}

        ,
        new Country(){
            Name ="Singapore",
            Code=(long)65,
            Iso="SG"}

        ,
        new Country(){
            Name ="Sint Maarten",
            Code=(long)1721,
            Iso="SX"}

        ,
        new Country(){
            Name ="Slovakia",
            Code=(long)421,
            Iso="SK"}

        ,
        new Country(){
            Name ="Slovenia",
            Code=(long)386,
            Iso="SI"}

        ,
        new Country(){
            Name ="Solomon Islands",
            Code=(long)677,
            Iso="SB"}

        ,
        new Country(){
            Name ="Somalia",
            Code=(long)252,
            Iso="SO"}

        ,
        new Country(){
            Name ="South Africa",
            Code=(long)27,
            Iso="ZA"}

        ,
        new Country(){
            Name ="South Korea",
            Code=(long)82,
            Iso="KR"}

        ,
        new Country(){
            Name ="South Sudan",
            Code=(long)211,
            Iso="SS"}

        ,
        new Country(){
            Name ="Spain",
            Code=(long)34,
            Iso="ES"}

        ,
        new Country(){
            Name ="Sri Lanka",
            Code=(long)94,
            Iso="LK"}

        ,
        new Country(){
            Name ="Sudan",
            Code=(long)249,
            Iso="SD"}

        ,
        new Country(){
            Name ="Suriname",
            Code=(long)597,
            Iso="SR"}

        ,
        new Country(){
            Name ="Svalbard and Jan Mayen",
            Code=(long)47,
            Iso="SJ"}

        ,
        new Country(){
            Name ="Swaziland",
            Code=(long)268,
            Iso="SZ"}

        ,
        new Country(){
            Name ="Sweden",
            Code=(long)46,
            Iso="SE"}

        ,
        new Country(){
            Name ="Switzerland",
            Code=(long)41,
            Iso="CH"}

        ,
        new Country(){
            Name ="Syria",
            Code=(long)963,
            Iso="SY"}

        ,
        new Country(){
            Name ="Taiwan",
            Code=(long)886,
            Iso="TW"}

        ,
        new Country(){
            Name ="Tajikistan",
            Code=(long)992,
            Iso="TJ"}

        ,
        new Country(){
            Name ="Tanzania",
            Code=(long)255,
            Iso="TZ"}

        ,
        new Country(){
            Name ="Thailand",
            Code=(long)66,
            Iso="TH"}

        ,
        new Country(){
            Name ="Togo",
            Code=(long)228,
            Iso="TG"}

        ,
        new Country(){
            Name ="Tokelau",
            Code=(long)690,
            Iso="TK"}

        ,
        new Country(){
            Name ="Tonga",
            Code=(long)676,
            Iso="TO"}

        ,
        new Country(){
            Name ="Trinidad and Tobago",
            Code=(long)1868,
            Iso="TT"}

        ,
        new Country(){
            Name ="Tunisia",
            Code=(long)216,
            Iso="TN"}

        ,
        new Country(){
            Name ="Turkey",
            Code=(long)90,
            Iso="TR"}

        ,
        new Country(){
            Name ="Turkmenistan",
            Code=(long)993,
            Iso="TM"}

        ,
        new Country(){
            Name ="Turks and Caicos Islands",
            Code=(long)1649,
            Iso="TC"}

        ,
        new Country(){
            Name ="Tuvalu",
            Code=(long)688,
            Iso="TV"}

        ,
        new Country(){
            Name ="U.S.Virgin Islands",
            Code=(long)1340,
            Iso="VI"}

        ,
        new Country(){
            Name ="Uganda",
            Code=(long)256,
            Iso="UG"}

        ,
        new Country(){
            Name ="Ukraine",
            Code=(long)380,
            Iso="UA"}

        ,
        new Country(){
            Name ="United Arab Emirates",
            Code=(long)971,
            Iso="AE"}

        ,
        new Country(){
            Name ="United Kingdom",
            Code=(long)44,
            Iso="GB"}

        ,
        new Country(){
            Name ="United States",
            Code=(long)1,
            Iso="US"}

        ,
        new Country(){
            Name ="Uruguay",
            Code=(long)598,
            Iso="UY"}

        ,
        new Country(){
            Name ="Uzbekistan",
            Code=(long)998,
            Iso="UZ"}

        ,
        new Country(){
            Name ="Vanuatu",
            Code=(long)678,
            Iso="VU"}

        ,
        new Country(){
            Name ="Vatican",
            Code=(long)379,
            Iso="VA"}

        ,
        new Country(){
            Name ="Venezuela",
            Code=(long)58,
            Iso="VE"}

        ,
        new Country(){
            Name ="Vietnam",
            Code=(long)84,
            Iso="VN"}

        ,
        new Country(){
            Name ="Wallis and Futuna",
            Code=(long)681,
            Iso="WF"}

        ,
        new Country(){
            Name ="Western Sahara",
            Code=(long)212,
            Iso="EH"}

        ,
        new Country(){
            Name ="Yemen",
            Code=(long)967,
            Iso="YE"}

        ,
        new Country(){
            Name ="Zambia",
            Code=(long)260,
            Iso="ZM"}

        ,
        new Country(){
            Name ="Zimbabwe",
            Code=(long)263,
            Iso="ZW"}

        };



}
}
