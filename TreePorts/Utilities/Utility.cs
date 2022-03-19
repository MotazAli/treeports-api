using Microsoft.IdentityModel.Tokens;
using TreePorts.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using Microsoft.AspNetCore.Hosting;
using MimeKit;
using MailKit.Net.Smtp;
using TreePorts.DTO;
using Microsoft.AspNetCore.Http;
using System.Linq;
using SendGrid;
using SendGrid.Helpers.Mail;
using QRCoder;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.EntityFrameworkCore;
using QRCodeDecoderLibrary;
using System.Linq.Dynamic.Core;
using Nancy.Json;
using Microsoft.Extensions.Caching.Memory;
using TreePorts.DTO.Records;

namespace TreePorts.Utilities
{
	public class Utility
	{
		// Local Enviroment
		//private static string SupportServerURL = "http://localhost:4000";
		//private static string SupportServerToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VyVHlwZSI6InN1cHBvcnQiLCJhdWQiOiJKb2tlbiIsImV4cCI6MTYzNDA5NDEwMCwiaWF0IjoxNjM0MDg2OTAwLCJpc3MiOiJKb2tlbiIsImp0aSI6IjJxbW4ybzZzMjdlbXBjdDl1ODAwMDBiMSIsIm5hbWVpZCI6Ijk4MTIwZTQ5LTdjNjQtNDJiZi05YzdmLWMwNzBkN2FmZmE3MiIsIm5iZiI6MTYzNDA4NjkwMH0.c5eo_miuV9CG68Ef53F-ViJWbF2Tq89Y22JcpRtg8Lg";

		// Online Development Enviroment
		private static string SupportServerURL = "https://sender-support-dev.gigalixirapp.com";
		private static string SupportServerToken = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwidW5pcXVlX25hbWUiOiJTeXN0ZW0iLCJVc2VyVHlwZSI6IkFkbWluIiwibmJmIjoxNjMzNjc2OTcwLCJleHAiOjE2MzM2ODA1NzAsImlhdCI6MTYzMzY3Njk3MH0.UVdRATIWpaAEovC4vpXQ1e7CKSP4ZJsJvAyHD5ePI4-jRKRfwHJiIdDAK6fl1OBn8mbSR2Fl3JzsXM_I0XLCww";


		// Live Enviroment
		//private static string SupportServerURL = "http://localhost:4000";
		//private static string SupportServerToken = "";

		public static string GeneratePassword()
		{
			Random random = new Random();
			var pass = random.Next(0, 1000000);
			string passString = pass.ToString("000000");
			return passString;
		}

		public static object SetCacheForAuth(object key, object value, IMemoryCache cache) => 
			cache.Set(key, value, TimeSpan.FromSeconds(10));


		public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
		{
			using (var hmac = new HMACSHA512())
			{
				passwordSalt = hmac.Key;
				passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
			}

		}


		public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
		{
			using (var hmac = new HMACSHA512(passwordSalt))
			{
				var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
				for (int i = 0; i < computedHash.Length; i++)
				{
					if (computedHash[i] != passwordHash[i]) return false;
				}

				return true;
			}

		}


		public static string GenerateToken(string userId, string fullname, string userType, DateTime? expireDate)
		{
			var claims = new[]
			{
				new Claim(ClaimTypes.NameIdentifier,userId),
				new Claim(ClaimTypes.Name,fullname),
				new Claim("UserType",userType)
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Sender api From Aion eight Company"));
			var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
			SecurityTokenDescriptor tokenDescripter;
			if (expireDate != null)
			{
				tokenDescripter = new SecurityTokenDescriptor
				{
					Subject = new ClaimsIdentity(claims),
					Expires = expireDate,
					SigningCredentials = credentials
				};
			}
			else
			{
				tokenDescripter = new SecurityTokenDescriptor
				{
					Subject = new ClaimsIdentity(claims),
					//Expires = expireDate,
					SigningCredentials = credentials
				};
			}
			//var tokenDescripter = new SecurityTokenDescriptor
			//         {
			//             Subject = new ClaimsIdentity(claims),
			//             //Expires = expireDate,
			//             SigningCredentials = credentials
			//         };

			var tokenHandler = new JwtSecurityTokenHandler();
			var token = tokenHandler.CreateToken(tokenDescripter);
			return tokenHandler.WriteToken(token);


		}





		public static async Task<bool> SendSMSAsync(string message, string phone, CancellationToken cancellationToken = default)
		{
			using (var client = new HttpClient())
			{
				var httpRequestMessage = new HttpRequestMessage
				{
					Method = HttpMethod.Post,
					RequestUri = new Uri("https://api.sms.to/sms/send"),
					Headers = {
								{ HttpRequestHeader.Authorization.ToString(), "Bearer YaRF2PtXWrxFHlO8ss4g2cGCFXAveQjB" },
								{ HttpRequestHeader.Accept.ToString(), "application/json" },
								{ "X-Version", "1" }
							  },
					Content = new StringContent("{\n    \"message\": \"" + message + "\",\n    \"to\": \"+" + phone + "\",\n    \"sender_id\": \"Sender\"}", Encoding.UTF8, "application/json")
					//Content = new StringContent("{\n    \"message\": \"" + message + "\",\n    \"to\": \"+201227642025\",\n    \"sender_id\": \"Sender\"}", Encoding.UTF8, "application/json")
				};

				var response = await client.SendAsync(httpRequestMessage,cancellationToken);
				return (response.StatusCode == HttpStatusCode.OK);
			}
		}




		public static async Task<string?> SendFirebaseNotification(IWebHostEnvironment hostingEnvironment, string title, string massageBody, string userMessageToken, CancellationToken cancellationToken= default)
		{

			try
			{
				var result = await FirebaseNotification.SendNotification(userMessageToken, title, massageBody,cancellationToken);
				return result;

			}
			catch (Exception ex)
			{
				return "";
				//app = FirebaseApp.GetInstance("SenderFirebaseApp");
			}


		}


		//public static async Task<string> SendFirebaseNotification(IWebHostEnvironment hostingEnvironment,string title ,string massageBody,string userMessageToken)
		//{

		//	try
		//	{
		//		var path = hostingEnvironment.ContentRootPath;
		//		path = path + "/Auth.json";
		//		FirebaseApp app = null;

		//		if (FirebaseApp.GetInstance("SenderFirebaseApp") == null)
		//		{
		//			app = FirebaseApp.Create(new AppOptions()
		//			{
		//				Credential = GoogleCredential.FromFile(path)
		//			}, "SenderFirebaseApp");
		//		}
		//		else
		//			app = FirebaseApp.GetInstance("SenderFirebaseApp");

		//		AndroidConfig androidConfig = new AndroidConfig();
		//		androidConfig.Priority = Priority.High;
		//		// AndroidConfig.builder().setPriority(AndroidConfig.Priority.HIGH).build();

		//		var fcm = FirebaseMessaging.GetMessaging(app);
		//		Message message = new Message()
		//		{
		//                  Notification = new Notification
		//                  {
		//                      Title = title,
		//                      Body = massageBody
		//                  }
		//                  ,
		//                  //   Data = new Dictionary<string, string>()
		//                  //{
		//                  //    { "Title", title },
		//                  //    { "Body", massageBody },
		//                  //      // {"priority", "high" },
		//                  //    //{ "AdditionalData3", "data 3" },
		//                  //},
		//                  Token = userMessageToken,
		//			Android = androidConfig,
		//		};


		//		return await fcm.SendAsync(message);

		//	}
		//	catch (Exception ex)
		//	{
		//		return "";
		//		//app = FirebaseApp.GetInstance("SenderFirebaseApp");
		//	}


		//}


		public static async Task<bool> SendEmail(NotificationMetadata _notificationMetadata, MimeMessage mimeMessage)
		{
			using (SmtpClient smtpClient = new SmtpClient())
			{
				await smtpClient.ConnectAsync(_notificationMetadata.SmtpServer,
				_notificationMetadata.Port, true);
				await smtpClient.AuthenticateAsync(_notificationMetadata.UserName,
				_notificationMetadata.Password);
				await smtpClient.SendAsync(mimeMessage);
				await smtpClient.DisconnectAsync(true);
				return true;
			}
		}


		public static MimeMessage CreateMimeMessageFromEmailMessage(EmailMessage message)
		{
			var mimeMessage = new MimeMessage();
			mimeMessage.From.Add(message.Sender);
			mimeMessage.To.Add(message.Reciever);
			mimeMessage.Subject = message.Subject;
			mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
			{ Text = message.Content };
			return mimeMessage;
		}


		public static void getRequestUserIdFromToken(HttpContext context, out string userId, out string userType)
		{
			// Reading the AuthHeader which is signed with JWT
			string authHeader = context.Request.Headers["Authorization"];
			//if (authHeader == null || authHeader == "") return -1;

			if (authHeader == null) 
			{
				userId = "";
				userType = "";
				return;
			}

			string token = authHeader.Split(" ")[1];

			var handler = new JwtSecurityTokenHandler();
			var jsonToken = handler.ReadToken(token);
			var securityToken = handler.ReadToken(token) as JwtSecurityToken;
			var test = securityToken?.Claims;
			userId = securityToken?.Claims?.FirstOrDefault(claim => claim.Type == "userId")?.Value ?? "";
			userType = securityToken?.Claims?.FirstOrDefault(claim => claim.Type == "UserType")?.Value ?? "";
			//if (userId == null || userId == "") return -1;

			//return long.Parse(userId);

		}




		public static bool SaveImage(string imageBase64, string imageName, string dirPath)
		{
			try
			{
				//using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(imageBase64)))
				//{
				//	using (Bitmap bm = new Bitmap(ms))
				//	{

				//		//bm.Save("SavingPath" + "ImageName.jpg");
				//		bm.Save("~/" + dirPath + imageName);
				//	}
				//}


				//OR
				byte[] imgByteArray = Convert.FromBase64String(imageBase64);
				File.WriteAllBytes(dirPath+"/"+ imageName, imgByteArray);
				return true;
				//OR
				//string path = "~/"+ dirPath + imageName;
				//var bytess = Convert.FromBase64String(imageBase64);
				//using (var imageFile = new FileStream(path, FileMode.Create))
				//{
				//	imageFile.Write(bytess, 0, bytess.Length);
				//	imageFile.Flush();
				//}


			}
			catch (Exception e)
			{
				e.ToString();
				return false;
			}

		}



		public static async Task<string?> sendGridMail(string from_email, string from_name, string subject_email, string content, bool isHTMLContent = true, CancellationToken cancellationToken = default)
		{
			var apiKey = "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";
			var client = new SendGridClient(apiKey);

			var from = new EmailAddress("sender_system@p-delivery.com", "Sender system");
			var subject = subject_email;
			var to = new EmailAddress(from_email, "");
			string plainTextContent = "", htmlContent = "";
			if (isHTMLContent)
				htmlContent = content;
			else
			{

				plainTextContent = $@"# New Message #
From : {from_name}
Email : {from_email}
Message : {content} ";

			}


			var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
			var response = await client.SendEmailAsync(msg,cancellationToken).ConfigureAwait(false);
			return response.Body.ToString();
		}




		public static async Task<string> getDirectionsFromGoogleMap(string origin, string destination, string mode, CancellationToken cancellationToken= default)
		{
			string api_key = "AIzaSyDOEAaFxR6LsONryMrNiFi5t8zncaCHyX8";
			using (var client = new HttpClient())
			{
				string url = "https://maps.googleapis.com/maps/api/directions/json?origin=" + origin + "&destination=" + destination + "&sensor=false&mode=" + mode + "&key=" + api_key;
				var httpRequestMessage = new HttpRequestMessage
				{
					Method = HttpMethod.Get,

					RequestUri = new Uri(url),
					Headers = {
								//{ HttpRequestHeader.Authorization.ToString(), "Bearer YaRF2PtXWrxFHlO8ss4g2cGCFXAveQjB" },
								{ HttpRequestHeader.Accept.ToString(), "application/json" },
								//{ "X-Version", "1" }
							  },
					//Content = new StringContent("{\n    \"message\": \"" + message + "\",\n    \"to\": \"+" + phone + "\",\n    \"sender_id\": \"Sender\"}", Encoding.UTF8, "application/json")
					//Content = new StringContent("{\n    \"message\": \"" + message + "\",\n    \"to\": \"+201227642025\",\n    \"sender_id\": \"Sender\"}", Encoding.UTF8, "application/json")
				};

				var response = client.SendAsync(httpRequestMessage,cancellationToken).Result;
				return await response.Content.ReadAsStringAsync(cancellationToken);
			}
		}





		//public static  bool SendSMS(string message)
		//{

		//    using (var client = new HttpClient()) {
		//        //var request = new HttpRequestMessage(HttpMethod.Post, "https://api.sms.to/sms/send");
		//        //request.Headers.Add("Content-Type", "application/json");
		//        //request.Headers.Add("Authorization", "Bearer l1QJHrqhreqDbTy0YhTLR3d8HQNCr6Q1");
		//        //request.Content = new StringContent("{\n    \"message\": \"T" + message + "\",\n    \"to\": \"+201227642025\",\n    \"sender_id\": \"Rigor\"}", Encoding.UTF8, "application/json");
		//        ////var client = _clientFactory.CreateClient();
		//        //var response = await client.SendAsync(request);


		//        //var url = "https://api.sms.to/sms/send";
		//        //var data = new StringContent("{\n    \"message\": \"T" + message + "\",\n    \"to\": \"+201227642025\",\n    \"sender_id\": \"Rigor\"}", Encoding.UTF8, "application/json");
		//        //client.DefaultRequestHeaders.Add("Content-Type", "application/json");
		//        //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "l1QJHrqhreqDbTy0YhTLR3d8HQNCr6Q1");
		//        //var response = await client.PostAsync(url, data);

		//        //string result = response.Content.ReadAsStringAsync().Result;


		//        var httpRequestMessage = new HttpRequestMessage
		//        {
		//            Method = HttpMethod.Post,
		//            RequestUri = new Uri("https://api.sms.to/sms/send"),
		//            Headers = {
		//                        { HttpRequestHeader.Authorization.ToString(), "Bearer l1QJHrqhreqDbTy0YhTLR3d8HQNCr6Q1" },
		//                        { HttpRequestHeader.Accept.ToString(), "application/json" },
		//                        { "X-Version", "1" }
		//                      },
		//            Content = new StringContent("{\n    \"message\": \"" + message + "\",\n    \"to\": \"+201227642025\",\n    \"sender_id\": \"Sender\"}", Encoding.UTF8, "application/json")
		//        };


		//        var response =  client.SendAsync(httpRequestMessage).Result;
		//        return (response.StatusCode == HttpStatusCode.OK) ? true : false;

		//    }






		//    //var client = new RestClient("https://6jj3wz.api.infobip.com/sms/2/text/advanced");
		//    //client.Timeout = -1;
		//    //var request = new RestRequest(Method.POST);
		//    //request.AddHeader("Authorization", "App de3a57466bb8b15508a385b9679773f5-5d7e1aeb-93dc-4ae3-b018-cc4e7736a3c0");
		//    //request.AddHeader("Content-Type", "application/json");
		//    //request.AddHeader("Accept", "application/json");
		//    //request.AddParameter("application/json", "{\"messages\":[{\"from\":\"InfoSMS\",\"to\":[\"201156285259\"],\"text\":\"" + message+"\",\"language\":{\"languageCode\":\"EN\"}}]}", ParameterType.RequestBody);
		//    //IRestResponse response = client.Execute(request);
		//    //return (response.Content != "")? true : false;


		//    //TelerivetAPI tr = new TelerivetAPI("YLr5G_g8KisXFyfH02ZlYzY35s9Cg0yZwXXL");
		//    //Project project = tr.InitProjectById("PJbeed416950c746a5");

		//    //// send message
		//    //Message sent_msg = await project.SendMessageAsync(Telerivet.Client.Util.Options(
		//    //    "content", message,
		//    //    "to_number", "+966560585665"
		//    //));

		//    //return (sent_msg.Status == "sent") ? true : false;



		//    //var baseAddress = new Uri("https://apps.gateway.sa");

		//    //using var httpClient = new HttpClient{BaseAddress = baseAddress};
		//    //{

		//    //    using (var content = new StringContent(@"{
		//    //                                                  ""userName"": ""xxxxxx"",
		//    //                                                  ""numbers"": ""966xxxxxx"",
		//    //                                                  ""userSender"": ""xxxxxx"",
		//    //                                                  ""apiKey"": ""xxxxxx"",
		//    //                                                  ""msg"": ""xxxxxx""
		//    //                                                }", System.Text.Encoding.Default, "application/json"))
		//    //    {
		//    //        using (var response = await httpClient.PostAsync("/vendorsms/pushsms.aspx", content))
		//    //        {
		//    //            string responseHeaders = response.Headers.ToString();
		//    //            string responseData = await response.Content.ReadAsStringAsync();

		//    //            return responseData != null ? true : false;
		//    //        }
		//    //    }
		//    //}

		//    //return false;
		//}



		//public static bool SendSMS(string message)
		//{
		//    var api = new PlivoApi("MAOTDMYTFMMTU3MJM0MD", "ZTUwZTZkNDBkNmI0OWI3OWE1MmQ1ZTUwY2VhMmNk");
		//    var response = api.Message.Create(
		//        src: "+201156285259",
		//        dst: new List<String> { "+966560585665" },//{ "+201227642025" },
		//        text: message,
		//        url: "http://foo.com/sms_status/"
		//        );
		//    return response.StatusCode == 202 ? true : false;
		//}










		//public static string SendSMS(string message)
		//{
		//    var client = new RestClient("https://{baseUrl}/sms/2/text/advanced");
		//    client.Timeout = -1;
		//    var request = new RestRequest(Method.POST);
		//    request.AddHeader("Authorization", "de3a57466bb8b15508a385b9679773f5-5d7e1aeb-93dc-4ae3-b018-cc4e7736a3c0");
		//    request.AddHeader("Content-Type", "application/json");
		//    request.AddHeader("Accept", "application/json");
		//    request.AddParameter("application/json", "{\"messages\":[{\"from\":\"Rigor\",\"to\":[\"+201227642025\"],\"text\":\""+message+"\",\"language\":{\"languageCode\":\"EN\"}}]}", ParameterType.RequestBody);
		//    IRestResponse response = client.Execute(request);
		//    return response.Content;
		//}





		//public static string SendSMS(string message)
		//{
		//    String messageEncode = HttpUtility.UrlEncode(message);
		//    using (var wb = new WebClient())
		//    {
		//        byte[] response = wb.UploadValues("https://api.txtlocal.com/send/", new NameValueCollection()
		//        {
		//        {"apikey" , "q1HF6BYFWgY-J4FbcDz6fTmphVGIoExGB1WVhihrqS"},
		//        {"numbers" , "00201227642025"},
		//        {"message" , messageEncode},
		//        {"sender" , "Rigor"}
		//        });
		//        string result = System.Text.Encoding.UTF8.GetString(response);
		//        return result;
		//    }
		//}

		// Create QR Code
		public static OrderQrcode CreateQRCode(string captainUserAccountId, long orderId)
		{

			QRCodeGenerator qrGenerator = new QRCodeGenerator();
			StringBuilder builder = new StringBuilder();
			builder.Append(captainUserAccountId).Append(":").Append(orderId);
			//	string data = "{" + userId + ":" +orderId +"}";			
			QRCodeData qrCodeData = qrGenerator.CreateQrCode(builder.ToString(),
				QRCodeGenerator.ECCLevel.Q);
			QRCode qrCode = new QRCode(qrCodeData);
			Bitmap qrCodeImage = qrCode.GetGraphic(20);
			var codeInBytes = BitmapToBytes(qrCodeImage);
			var qRCode = new OrderQrcode
			{
				Code = codeInBytes,
				CaptainUserAccountId = captainUserAccountId,
				OrderId = orderId
			};
			qRCode.QrCodeUrl = ConvertImgToString(qRCode.Code);
			return qRCode;
		}

		// Convert BitMap Image to array of bytes
		private static byte[] BitmapToBytes(Bitmap img)
		{
			using (MemoryStream stream = new MemoryStream())
			{
				img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
				return stream.ToArray();
			}
		}
		public static string ByteArrayToStr(byte[] DataArray)
		{
			var Decoder = Encoding.UTF8.GetDecoder();
			int CharCount = Decoder.GetCharCount(DataArray, 0, DataArray.Length);
			char[] CharArray = new char[CharCount];
			Decoder.GetChars(DataArray, 0, DataArray.Length, CharArray, 0);
			return new string(CharArray);
		}

		public static Bitmap BytesToBitmap(byte[] byteArray)
		{

			using (MemoryStream ms = new MemoryStream(byteArray))
			{
				Bitmap img = (Bitmap)System.Drawing.Image.FromStream(ms);
				return img;
			}

		}
		public static string getQrCodeData(byte[] code)
		{
			string result = "";
			// 1- Convert array of bytes to bitmap
			Bitmap bitmap = BytesToBitmap(code);
			// 2 - Convert from BitMap to data
			QRDecoder decoder = new QRDecoder();
			var decoderInBytes = decoder.ImageDecoder(bitmap);
			return result = ByteArrayToStr(decoderInBytes[0]);
		}
		// Convert QrCode Byte to string Url
		public static string ConvertImgToString(byte[] qrCode)
		{
			var qrCodeBase64 = Convert.ToBase64String(qrCode);
			string qrCodeURL = string.Format("data:image/png;base64,{0}", qrCodeBase64);
			return qrCodeURL;
		}
		// Create QR Code

		// Pagination
		public static IQueryable<Q> Pagination<Q>(IQueryable<Q> query, int pageSize, int pageIndex)
		{
			var skip = (pageSize * (pageIndex - 1));
			var take = pageSize;
			var result = query.Skip(skip).Take(take);
			return result;
		}

		public static IQueryable<Q> Pagination<Q>(List<Q> query, int pageSize, int pageIndex)
		{
			var skip = (pageSize * (pageIndex - 1));
			var take = pageSize;
			var result = query.Skip(skip).Take(take).AsQueryable<Q>();
			return result;
		}



		// Pagination
		// Fillterattion
		public static IQueryable<T> GetFilter3<T>(FilterParameters parameters, IQueryable<T> queries, int skip, int take, out int totalResult)
		{
			//var type = queries.GetType().GetGenericArguments()[0].FullName;
			var config = new ParsingConfig
			{
				UseParameterizedNamesInDynamicQuery = true,
				IsCaseSensitive = false,
				AreContextKeywordsEnabled = true,
				ResolveTypesBySimpleName = true
			};


			if (parameters.FilterById != null)
			{
				queries = queries.Where("Order.Id == @0", parameters.FilterById);
			}
			if (parameters.FilterByDate != null)
			{
				queries = queries.Where("Order.CreationDate.Value.Date == @0 ", parameters.FilterByDate);
			}

			if (parameters.StartDate != null && parameters.EndDate != null)
			{
				queries = queries.Where("Order.CreationDate >= @0 && Order.CreationDate <= @1", parameters.StartDate, parameters.EndDate);
			}

			if (parameters.StatusTypeId != null)
			{
				queries = queries.Where("Order.StatusTypeId == @0", parameters.StatusTypeId);

			}
			if (parameters.CurrentStatusId != null)
			{
				queries = queries.Where("Order.CurrentStatus == @0", parameters.CurrentStatusId);

			}
			if (parameters.PaymentTypeId != null)
			{
				queries = queries.Where("Order.PaymentTypeId == @0", parameters.PaymentTypeId);
			}
			if (parameters.ProductTypeId != null)
			{
				queries = queries.Where("Order.ProductTypeId == @0", parameters.ProductTypeId);
			}
			if (parameters.AgentId != null)
			{
				queries = queries.Where("Agent.Id == @0", parameters.AgentId);
			}
			//if (!string.IsNullOrEmpty(parameters.FullName))
			//{

			//	queries = queries.Where(config, "Agent.Fullname.Contains(@0)", parameters.FullName);
			//}
			if (!string.IsNullOrEmpty(parameters.DriverName))
			{
				var nameArray = parameters.DriverName.Split(' ');
				if (nameArray.Length == 1)
				{
					queries = queries.Where(config, "Captain.FirstName.Contains(@0) || Captain.FamilyName.Contains(@0) ", nameArray[0]);
				}
				if (nameArray.Length == 2)
				{
					queries = queries.Where(config, "Captain.FirstName.Contains(@0) && Captain.FamilyName.Contains(@1) ", nameArray[0], nameArray[1]);
				}
				//queries = queries.Where(config, "FirstName FamilyName as FullName.Contains(@0)", parameters.DriverName);
			}
			if (!string.IsNullOrEmpty(parameters.CountryName))
			{


				queries = queries.Where(config, "Country.Name.Contains(@0)", parameters.CountryName);

			}
			if (!string.IsNullOrEmpty(parameters.CityName))
			{

				queries = queries.Where(config, "City.Name.Contains(@0)", parameters.CityName);
			}

			if (!string.IsNullOrEmpty(parameters.CountryArabicName))
			{

				queries = queries.Where(config, "Country.ArabicName.Contains(@0)", parameters.CountryArabicName);
			}
			if (!string.IsNullOrEmpty(parameters.CityArabicName))
			{

				queries = queries.Where(config, "City.ArabicName.Contains(@0)", parameters.CityArabicName);
			}
			if (!string.IsNullOrEmpty(parameters.ResidenceCountryName))
			{

				queries = queries.Where(config, "Country.Name.Contains(@0)", parameters.ResidenceCountryName);
			}
			if (!string.IsNullOrEmpty(parameters.ResidenceCityName))
			{

				queries = queries.Where(config, "City.Name.Contains(@0)", parameters.ResidenceCityName);
			}
			if (!string.IsNullOrEmpty(parameters.ResidenceCountryArabicName))
			{

				queries = queries.Where(config, "Country.Name.Contains(@0)", parameters.ResidenceCountryArabicName);
			}
			if (!string.IsNullOrEmpty(parameters.ResidenceCityArabicName))
			{

				queries = queries.Where(config, "City.Name.Contains(@0)", parameters.ResidenceCityArabicName);
			}
			if (!string.IsNullOrEmpty(parameters.Mobile))
			{

				queries = queries.Where(config, "Agent.Mobile.Contains(@0)", parameters.Mobile);
			}

			if (!string.IsNullOrEmpty(parameters.Mobile))
			{

				queries = queries.Where(config, "Captain.Mobile.Contains(@0)", parameters.Mobile);
			}

			if (!string.IsNullOrEmpty(parameters.Email))
			{

				queries = queries.Where(config, "AgeEmail.Contains(@0)", parameters.Email);
			}
			if (!string.IsNullOrEmpty(parameters.NationalNumber))
			{

				queries = queries.Where(config, "NationalNumber.Contains(@0)", parameters.NationalNumber);
			}
			if (!string.IsNullOrEmpty(parameters.VehiclePlateNumber))
			{

				queries = queries.Where(config, "Captain.VehiclePlateNumber.Contains(@0)", parameters.VehiclePlateNumber);
			}
			if (!string.IsNullOrEmpty(parameters.CustomerName))
			{

				queries = queries.Where(config, "Order.CustomerName.Contains(@0)", parameters.CustomerName);
			}
			if (!string.IsNullOrEmpty(parameters.CustomerAddress))
			{

				queries = queries.Where(config, "Order.CustomerAddress.Contains(@0)", parameters.CustomerAddress);
			}
			if (!string.IsNullOrEmpty(parameters.CustomerPhone))
			{

				queries = queries.Where(config, "Order.CustomerPhone.Contains(@0)", parameters.CustomerPhone);
			}
			if (!string.IsNullOrEmpty(parameters.OrderAgentName))
			{

				queries = queries.Where(config, "Agent.FullName.Contains(@0)", parameters.OrderAgentName);
			}
			if (!string.IsNullOrEmpty(parameters.OrderCityName))
			{

				queries = queries.Where(config, "City.Name.Contains(@0) || City.ArabicName.Contains(@0)", parameters.OrderCityName);
			}
			if (!string.IsNullOrEmpty(parameters.OrderCountryName))
			{

				queries = queries.Where(config, "Country.Name.Contains(@0) || Country.ArabicName.Contains(@0)", parameters.OrderCountryName);
			}
			if (!string.IsNullOrEmpty(parameters.OrderCaptain))
			{
				var nameArray = parameters.OrderCaptain.Split(' ');
				if (nameArray.Length == 1)
				{
					queries = queries.Where(config, "Captain.FirstName.Contains(@0) " +
						"|| Captain.FamilyName.Contains(@0) ", nameArray[0]);
				}
				if (nameArray.Length == 2)
				{
					queries = queries.Where(config, "Captain..FirstName.Contains(@0)" +
						" && Captain.FamilyName.Contains(@1) ", nameArray[0], nameArray[1]);
				}
				//queries = queries.Where(config, "FirstName FamilyName as FullName.Contains(@0)", parameters.DriverName);
			}
			totalResult = queries.Count();

			return queries.Skip(skip).Take(take); // here is the change

		}
		// Fillterattion




		// Pagination
		// Fillterattion
		public static IQueryable<T> GetFilter2<T>(FilterParameters parameters, IQueryable<T> queries, int skip, int take, out int totalResult)
		{
			//var type = queries.GetType().GetGenericArguments()[0].FullName;
			var config = new ParsingConfig
			{
				UseParameterizedNamesInDynamicQuery = true,
				IsCaseSensitive = false,
				AreContextKeywordsEnabled = true,
				ResolveTypesBySimpleName = true
			};


			if (parameters.FilterById != null)
			{
				queries = queries.Where("Id == @0", parameters.FilterById);
			}
			if (parameters.FilterByDate != null)
			{
				queries = queries.Where("CreationDate.Value.Date == @0 ", parameters.FilterByDate);
			}

			if (parameters.StartDate != null && parameters.EndDate != null)
			{
				queries = queries.Where("CreationDate >= @0 && CreationDate <= @1", parameters.StartDate, parameters.EndDate);
			}

			if (parameters.StatusTypeId != null)
			{
				queries = queries.Where("StatusTypeId == @0", parameters.StatusTypeId);

			}
			if (parameters.CurrentStatusId != null)
			{
				queries = queries.Where("CurrentStatus == @0", parameters.CurrentStatusId);

			}
			if (parameters.PaymentTypeId != null)
			{
				queries = queries.Where("PaymentTypeId == @0", parameters.PaymentTypeId);
			}
			if (parameters.ProductTypeId != null)
			{
				queries = queries.Where("ProductTypeId == @0", parameters.ProductTypeId);
			}
			if (parameters.AgentId != null)
			{
				queries = queries.Where("Agent.Id == @0", parameters.AgentId);
			}
			if (!string.IsNullOrEmpty(parameters.FullName))
			{

				queries = queries.Where(config, "Fullname.Contains(@0)", parameters.FullName);
			}
			if (!string.IsNullOrEmpty(parameters.DriverName))
			{
				var nameArray = parameters.DriverName.Split(' ');
				if (nameArray.Length == 1)
				{
					queries = queries.Where(config, "FirstName.Contains(@0) || FamilyName.Contains(@0) ", nameArray[0]);
				}
				if (nameArray.Length == 2)
				{
					queries = queries.Where(config, "FirstName.Contains(@0) && FamilyName.Contains(@1) ", nameArray[0], nameArray[1]);
				}
				//queries = queries.Where(config, "FirstName FamilyName as FullName.Contains(@0)", parameters.DriverName);
			}
			if (!string.IsNullOrEmpty(parameters.CountryName))
			{


				queries = queries.Where(config, "Country.Name.Contains(@0)", parameters.CountryName);

			}
			if (!string.IsNullOrEmpty(parameters.CityName))
			{

				queries = queries.Where(config, "City.Name.Contains(@0)", parameters.CityName);
			}

			if (!string.IsNullOrEmpty(parameters.CountryArabicName))
			{

				queries = queries.Where(config, "Country.ArabicName.Contains(@0)", parameters.CountryArabicName);
			}
			if (!string.IsNullOrEmpty(parameters.CityArabicName))
			{

				queries = queries.Where(config, "City.ArabicName.Contains(@0)", parameters.CityArabicName);
			}
			if (!string.IsNullOrEmpty(parameters.ResidenceCountryName))
			{

				queries = queries.Where(config, "Country.Name.Contains(@0)", parameters.ResidenceCountryName);
			}
			if (!string.IsNullOrEmpty(parameters.ResidenceCityName))
			{

				queries = queries.Where(config, "City.Name.Contains(@0)", parameters.ResidenceCityName);
			}
			if (!string.IsNullOrEmpty(parameters.ResidenceCountryArabicName))
			{

				queries = queries.Where(config, "Country.Name.Contains(@0)", parameters.ResidenceCountryArabicName);
			}
			if (!string.IsNullOrEmpty(parameters.ResidenceCityArabicName))
			{

				queries = queries.Where(config, "City.Name.Contains(@0)", parameters.ResidenceCityArabicName);
			}
			if (!string.IsNullOrEmpty(parameters.Mobile))
			{

				queries = queries.Where(config, "Mobile.Contains(@0)", parameters.Mobile);
			}
			if (!string.IsNullOrEmpty(parameters.Email))
			{

				queries = queries.Where(config, "Email.Contains(@0)", parameters.Email);
			}
			if (!string.IsNullOrEmpty(parameters.NationalNumber))
			{

				queries = queries.Where(config, "NationalNumber.Contains(@0)", parameters.NationalNumber);
			}
			if (!string.IsNullOrEmpty(parameters.VehiclePlateNumber))
			{

				queries = queries.Where(config, "VehiclePlateNumber.Contains(@0)", parameters.VehiclePlateNumber);
			}
			if (!string.IsNullOrEmpty(parameters.CustomerName))
			{

				queries = queries.Where(config, "CustomerName.Contains(@0)", parameters.CustomerName);
			}
			if (!string.IsNullOrEmpty(parameters.CustomerAddress))
			{

				queries = queries.Where(config, "CustomerAddress.Contains(@0)", parameters.CustomerAddress);
			}
			if (!string.IsNullOrEmpty(parameters.CustomerPhone))
			{

				queries = queries.Where(config, "CustomerPhone.Contains(@0)", parameters.CustomerPhone);
			}
			if (!string.IsNullOrEmpty(parameters.OrderAgentName))
			{

				queries = queries.Where(config, "Agent.FullName.Contains(@0)", parameters.OrderAgentName);
			}
			if (!string.IsNullOrEmpty(parameters.OrderCityName))
			{

				queries = queries.Where(config, "Agent.City.Name.Contains(@0) || Agent.City.ArabicName.Contains(@0)", parameters.OrderCityName);
			}
			if (!string.IsNullOrEmpty(parameters.OrderCountryName))
			{

				queries = queries.Where(config, "Agent.Country.Name.Contains(@0) || Agent.Country.ArabicName.Contains(@0)", parameters.OrderCountryName);
			}
			if (!string.IsNullOrEmpty(parameters.OrderCaptain))
			{
				var nameArray = parameters.OrderCaptain.Split(' ');
				if (nameArray.Length == 1)
				{
					queries = queries.Where(config, "UserAcceptedRequests.FirstOrDefault().User.FirstName.Contains(@0) " +
						"|| UserAcceptedRequests.FirstOrDefault().User.FamilyName.Contains(@0) ", nameArray[0]);
				}
				if (nameArray.Length == 2)
				{
					queries = queries.Where(config, "UserAcceptedRequests.FirstOrDefault().User.FirstName.Contains(@0)" +
						" && UserAcceptedRequests.FirstOrDefault().User.FamilyName.Contains(@1) ", nameArray[0], nameArray[1]);
				}
				//queries = queries.Where(config, "FirstName FamilyName as FullName.Contains(@0)", parameters.DriverName);
			}
			totalResult = queries.Count();

			return queries.Skip(skip).Take(take); // here is the change

		}
		// Fillterattion










		//// Pagination
		//// Fillterattion
		//public static IQueryable<T> GetFilter2<T>(FilterParameters parameters, IQueryable<T> queries,int skip,int take, out int totalResult)
		//{
		//	//var type = queries.GetType().GetGenericArguments()[0].FullName;
		//	var config = new ParsingConfig
		//	{
		//		UseParameterizedNamesInDynamicQuery = true,
		//		IsCaseSensitive = false,
		//		AreContextKeywordsEnabled = true,
		//		ResolveTypesBySimpleName = true
		//	};


		//	if (parameters.FilterById != null)
		//	{
		//		queries = queries.Where("Id == @0", parameters.FilterById);
		//	}
		//	if (parameters.FilterByDate != null)
		//	{
		//		queries = queries.Where("CreationDate.Value.Date == @0 ", parameters.FilterByDate);
		//	}

		//	if (parameters.StartDate != null && parameters.EndDate != null)
		//	{
		//		queries = queries.Where("CreationDate >= @0 && CreationDate <= @1", parameters.StartDate, parameters.EndDate);
		//	}

		//	if (parameters.StatusTypeId != null)
		//	{
		//		queries = queries.Where("StatusTypeId == @0", parameters.StatusTypeId);

		//	}
		//	if (parameters.CurrentStatusId != null)
		//	{
		//		queries = queries.Where("CurrentStatus == @0", parameters.CurrentStatusId);

		//	}
		//	if (parameters.PaymentTypeId != null)
		//	{
		//		queries = queries.Where("PaymentTypeId == @0", parameters.PaymentTypeId);
		//	}
		//	if (parameters.ProductTypeId != null)
		//	{
		//		queries = queries.Where("ProductTypeId == @0", parameters.ProductTypeId);
		//	}
		//	if (parameters.AgentId != null)
		//	{
		//		queries = queries.Where("AgentId == @0", parameters.AgentId);
		//	}
		//	if (!string.IsNullOrEmpty(parameters.FullName))
		//	{

		//		queries = queries.Where(config, "Fullname.Contains(@0)", parameters.FullName);
		//	}
		//	if (!string.IsNullOrEmpty(parameters.DriverName))
		//	{
		//		var nameArray = parameters.DriverName.Split(' ');
		//		if (nameArray.Length == 1)
		//		{
		//			queries = queries.Where(config, "FirstName.Contains(@0) || FamilyName.Contains(@0) ", nameArray[0]);
		//		}
		//		if (nameArray.Length == 2)
		//		{
		//			queries = queries.Where(config, "FirstName.Contains(@0) && FamilyName.Contains(@1) ", nameArray[0], nameArray[1]);
		//		}
		//		//queries = queries.Where(config, "FirstName FamilyName as FullName.Contains(@0)", parameters.DriverName);
		//	}
		//	if (!string.IsNullOrEmpty(parameters.CountryName))
		//	{


		//		queries = queries.Where(config, "Country.Name.Contains(@0)", parameters.CountryName);

		//	}
		//	if (!string.IsNullOrEmpty(parameters.CityName))
		//	{

		//		queries = queries.Where(config, "City.Name.Contains(@0)", parameters.CityName);
		//	}

		//	if (!string.IsNullOrEmpty(parameters.CountryArabicName))
		//	{

		//		queries = queries.Where(config, "Country.ArabicName.Contains(@0)", parameters.CountryArabicName);
		//	}
		//	if (!string.IsNullOrEmpty(parameters.CityArabicName))
		//	{

		//		queries = queries.Where(config, "City.ArabicName.Contains(@0)", parameters.CityArabicName);
		//	}
		//	if (!string.IsNullOrEmpty(parameters.ResidenceCountryName))
		//	{

		//		queries = queries.Where(config, "Country.Name.Contains(@0)", parameters.ResidenceCountryName);
		//	}
		//	if (!string.IsNullOrEmpty(parameters.ResidenceCityName))
		//	{

		//		queries = queries.Where(config, "City.Name.Contains(@0)", parameters.ResidenceCityName);
		//	}
		//	if (!string.IsNullOrEmpty(parameters.ResidenceCountryArabicName))
		//	{

		//		queries = queries.Where(config, "Country.Name.Contains(@0)", parameters.ResidenceCountryArabicName);
		//	}
		//	if (!string.IsNullOrEmpty(parameters.ResidenceCityArabicName))
		//	{

		//		queries = queries.Where(config, "City.Name.Contains(@0)", parameters.ResidenceCityArabicName);
		//	}
		//	if (!string.IsNullOrEmpty(parameters.Mobile))
		//	{

		//		queries = queries.Where(config, "Mobile.Contains(@0)", parameters.Mobile);
		//	}
		//	if (!string.IsNullOrEmpty(parameters.Email))
		//	{

		//		queries = queries.Where(config, "Email.Contains(@0)", parameters.Email);
		//	}
		//	if (!string.IsNullOrEmpty(parameters.NationalNumber))
		//	{

		//		queries = queries.Where(config, "NationalNumber.Contains(@0)", parameters.NationalNumber);
		//	}
		//	if (!string.IsNullOrEmpty(parameters.VehiclePlateNumber))
		//	{

		//		queries = queries.Where(config, "VehiclePlateNumber.Contains(@0)", parameters.VehiclePlateNumber);
		//	}
		//	if (!string.IsNullOrEmpty(parameters.CustomerName))
		//	{

		//		queries = queries.Where(config, "CustomerName.Contains(@0)", parameters.CustomerName);
		//	}
		//	if (!string.IsNullOrEmpty(parameters.CustomerAddress))
		//	{

		//		queries = queries.Where(config, "CustomerAddress.Contains(@0)", parameters.CustomerAddress);
		//	}
		//	if (!string.IsNullOrEmpty(parameters.CustomerPhone))
		//	{

		//		queries = queries.Where(config, "CustomerPhone.Contains(@0)", parameters.CustomerPhone);
		//	}
		//	if (!string.IsNullOrEmpty(parameters.OrderAgentName))
		//	{

		//		queries = queries.Where(config, "Agent.FullName.Contains(@0)", parameters.OrderAgentName);
		//	}
		//	if (!string.IsNullOrEmpty(parameters.OrderCityName))
		//	{

		//		queries = queries.Where(config, "Agent.City.Name.Contains(@0) || Agent.City.ArabicName.Contains(@0)", parameters.OrderCityName);
		//	}
		//	if (!string.IsNullOrEmpty(parameters.OrderCountryName))
		//	{

		//		queries = queries.Where(config, "Agent.Country.Name.Contains(@0) || Agent.Country.ArabicName.Contains(@0)", parameters.OrderCountryName);
		//	}
		//	if (!string.IsNullOrEmpty(parameters.OrderCaptain))
		//	{
		//		var nameArray = parameters.OrderCaptain.Split(' ');
		//		if (nameArray.Length == 1)
		//		{
		//			queries = queries.Where(config, "UserAcceptedRequests.FirstOrDefault().User.FirstName.Contains(@0) " +
		//				"|| UserAcceptedRequests.FirstOrDefault().User.FamilyName.Contains(@0) ", nameArray[0]);
		//		}
		//		if (nameArray.Length == 2)
		//		{
		//			queries = queries.Where(config, "UserAcceptedRequests.FirstOrDefault().User.FirstName.Contains(@0)" +
		//				" && UserAcceptedRequests.FirstOrDefault().User.FamilyName.Contains(@1) ", nameArray[0], nameArray[1]);
		//		}
		//		//queries = queries.Where(config, "FirstName FamilyName as FullName.Contains(@0)", parameters.DriverName);
		//	}
		//	totalResult = queries.Count();

		//	return queries.Skip(skip).Take(take); // here is the change

		//}
		//// Fillterattion








		// Pagination
		// Fillterattion
		public static IQueryable<T> GetFilter<T>(FilterParameters parameters, IQueryable<T> queries)
		{
			var type = queries.GetType().GetGenericArguments()[0].FullName;
			var config = new ParsingConfig
			{
				UseParameterizedNamesInDynamicQuery = true,
				IsCaseSensitive = false,
				AreContextKeywordsEnabled = true,
				ResolveTypesBySimpleName = true
			};


			if (parameters.FilterById != null)
			{
				queries = queries.Where("Id == @0", parameters.FilterById);
			}
			if (parameters.FilterByDate != null)
			{
				queries = queries.Where("CreationDate.Value.Date == @0 ", parameters.FilterByDate);
			}

			if (parameters.StartDate != null && parameters.EndDate != null)
			{
				queries = queries.Where("CreationDate >= @0 && CreationDate <= @1", parameters.StartDate, parameters.EndDate);
			}
			if (parameters.AgentId != null)
			{
				queries = queries.Where("Agent.Id == @0", parameters.AgentId);
			}
			if (parameters.StatusTypeId != null)
			{
				queries = queries.Where("StatusTypeId == @0", parameters.StatusTypeId);

			}
			if (parameters.CurrentStatusId != null)
			{
				queries = queries.Where("CurrentStatus == @0", parameters.CurrentStatusId);

			}
			if (parameters.PaymentTypeId != null)
			{
				queries = queries.Where("PaymentTypeId == @0", parameters.PaymentTypeId);
			}
			if (parameters.ProductTypeId != null)
			{
				queries = queries.Where("ProductTypeId == @0", parameters.ProductTypeId);
			}
			if (!string.IsNullOrEmpty(parameters.FullName))
			{

				queries = queries.Where(config, "Fullname.Contains(@0)", parameters.FullName);
			}
			if (!string.IsNullOrEmpty(parameters.DriverName))
			{
				var nameArray = parameters.DriverName.Split(' ');
				if(nameArray.Length == 1)
				{
					queries = queries.Where(config, "FirstName.Contains(@0) || FamilyName.Contains(@0) ", nameArray[0]  );
				}
				if (nameArray.Length == 2)
				{
					queries = queries.Where(config, "FirstName.Contains(@0) && FamilyName.Contains(@1) ", nameArray[0], nameArray[1]);
				}
				//queries = queries.Where(config, "FirstName FamilyName as FullName.Contains(@0)", parameters.DriverName);
			}
			if (!string.IsNullOrEmpty(parameters.CountryName))
			{


				queries = queries.Where(config, "Country.Name.Contains(@0)", parameters.CountryName);

			}
			if (!string.IsNullOrEmpty(parameters.CityName))
			{

				queries = queries.Where(config, "City.Name.Contains(@0)", parameters.CityName);
			}

			if (!string.IsNullOrEmpty(parameters.CountryArabicName))
			{

				queries = queries.Where(config, "Country.ArabicName.Contains(@0)", parameters.CountryArabicName);
			}
			if (!string.IsNullOrEmpty(parameters.CityArabicName))
			{

				queries = queries.Where(config, "City.ArabicName.Contains(@0)", parameters.CityArabicName);
			}
			if (!string.IsNullOrEmpty(parameters.ResidenceCountryName))
			{

				queries = queries.Where(config, "Country.Name.Contains(@0)", parameters.ResidenceCountryName);
			}
			if (!string.IsNullOrEmpty(parameters.ResidenceCityName))
			{

				queries = queries.Where(config, "City.Name.Contains(@0)", parameters.ResidenceCityName);
			}
			if (!string.IsNullOrEmpty(parameters.ResidenceCountryArabicName))
			{

				queries = queries.Where(config, "Country.Name.Contains(@0)", parameters.ResidenceCountryArabicName);
			}
			if (!string.IsNullOrEmpty(parameters.ResidenceCityArabicName))
			{

				queries = queries.Where(config, "City.Name.Contains(@0)", parameters.ResidenceCityArabicName);
			}
			if (!string.IsNullOrEmpty(parameters.Mobile))
			{

				queries = queries.Where(config, "Mobile.Contains(@0)", parameters.Mobile);
			}
			if (!string.IsNullOrEmpty(parameters.Email))
			{

				queries = queries.Where(config, "Email.Contains(@0)", parameters.Email);
			}
			if (!string.IsNullOrEmpty(parameters.NationalNumber))
			{

				queries = queries.Where(config, "NationalNumber.Contains(@0)", parameters.NationalNumber);
			}
			if (!string.IsNullOrEmpty(parameters.VehiclePlateNumber))
			{

				queries = queries.Where(config, "VehiclePlateNumber.Contains(@0)", parameters.VehiclePlateNumber);
			}
			if (!string.IsNullOrEmpty(parameters.CustomerName))
			{

				queries = queries.Where(config, "CustomerName.Contains(@0)", parameters.CustomerName);
			}
			if (!string.IsNullOrEmpty(parameters.CustomerAddress))
			{

				queries = queries.Where(config, "CustomerAddress.Contains(@0)", parameters.CustomerAddress);
			}
			if (!string.IsNullOrEmpty(parameters.CustomerPhone))
			{

				queries = queries.Where(config, "CustomerPhone.Contains(@0)", parameters.CustomerPhone);
			}
			if (!string.IsNullOrEmpty(parameters.OrderAgentName))
			{

				queries = queries.Where(config, "Agent.FullName.Contains(@0)", parameters.OrderAgentName);
			}
			if (!string.IsNullOrEmpty(parameters.OrderCityName))
			{

				queries = queries.Where(config, "Agent.City.Name.Contains(@0) || Agent.City.ArabicName.Contains(@0)", parameters.OrderCityName);
			}
			if (!string.IsNullOrEmpty(parameters.OrderCountryName))
			{

				queries = queries.Where(config, "Agent.Country.Name.Contains(@0) || Agent.Country.ArabicName.Contains(@0)", parameters.OrderCountryName);
			}
			if (!string.IsNullOrEmpty(parameters.OrderCaptain))
			{
				var nameArray = parameters.OrderCaptain.Split(' ');
				if (nameArray.Length == 1)
				{
					queries = queries.Where(config, "UserAcceptedRequests.FirstOrDefault().User.FirstName.Contains(@0) " +
						"|| UserAcceptedRequests.FirstOrDefault().User.FamilyName.Contains(@0) ", nameArray[0]);
				}
				if (nameArray.Length == 2)
				{
					queries = queries.Where(config, "UserAcceptedRequests.FirstOrDefault().User.FirstName.Contains(@0)" +
						" && UserAcceptedRequests.FirstOrDefault().User.FamilyName.Contains(@1) ", nameArray[0], nameArray[1]);
				}
				//queries = queries.Where(config, "FirstName FamilyName as FullName.Contains(@0)", parameters.DriverName);
			}
			return  queries;

		}
		// Fillterattion

		// Distance Calculations
		private static double deg2rad(double deg)
		{
			return (deg * Math.PI / 180.0);
		}
		private static double rad2deg(double rad)
		{
			return (rad / Math.PI * 180.0);
		}
		public static double distance(double lat1, double lon1, double lat2, double lon2, char unit = 'K')
		{
			if ((lat1 == lat2) && (lon1 == lon2))
			{
				return 0;
			}
			else
			{
				double theta = lon1 - lon2;
				double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
				dist = Math.Acos(dist);
				dist = rad2deg(dist);
				dist = dist * 60 * 1.1515;
				if (unit == 'K')
				{
					dist = dist * 1.609344;
				}
				else if (unit == 'N')
				{
					dist = dist * 0.8684;
				}
				return (dist);
			}
		}
		// Distance Calculations

		// Generate Coupons
		public static String GenerateCoupon(int copounLength)
		{

			var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
			var stringChars = new char[copounLength];
			var random = new Random();

			for (int i = 0; i < stringChars.Length; i++)
			{
				stringChars[i] = chars[random.Next(chars.Length)];
			}

			var finalString = new String(stringChars);
			return finalString;
		}
		// Generate Coupons

		// Http Hooks
		public static async Task<bool> ExecuteWebHook( string? url, string body, CancellationToken cancellationToken = default)
		{
			if (url is null || url.Length == 0) return false;

			using (var client = new HttpClient())
			{
				var httpRequestMessage = new HttpRequestMessage
				{
					Method = HttpMethod.Post,
					RequestUri = new Uri(url),
					Headers = {
						{ HttpRequestHeader.Authorization.ToString(), "Bearer YaRF2PtXWrxFHlO8ss4g2cGCFXAveQjB" },
						{ HttpRequestHeader.Accept.ToString(), "application/json" },
						{ "X-Version", "1" }
					},
					Content = new StringContent(body, Encoding.UTF8, "application/json")
				};
				var response = await client.SendAsync(httpRequestMessage,cancellationToken);
				return response.StatusCode == HttpStatusCode.OK;
				
			}
		}


		public static bool RegisterToGateway()
		{
			string url = "http://localhost:3000/register";
			var bodyObject = new
			{
				name = "core_service",
				host = "localhost",
				protocol= "http",
				port = "5001"
			};

			var body = ConvertToJson(bodyObject);



			using (var client = new HttpClient())
			{
				var httpRequestMessage = new HttpRequestMessage
				{
					Method = HttpMethod.Post,
					RequestUri = new Uri(url),
					Headers = {
						//{ HttpRequestHeader.Authorization.ToString(), "Bearer YaRF2PtXWrxFHlO8ss4g2cGCFXAveQjB" },
						{ HttpRequestHeader.Accept.ToString(), "application/json" },
						{ "X-Version", "1" }
					},
					Content = new StringContent(body, Encoding.UTF8, "application/json")
				};
				var response = client.SendAsync(httpRequestMessage).Result;
				//Console.WriteLine(response.Content.ReadAsStringAsync());
				return (response.StatusCode == HttpStatusCode.OK) ? true : false;

			}
		}


		public static async Task<bool> STCPaymentAsync(double amount, string mobile)
		{
			var clientCode = "";
			var username = "";
			var password = "";
			var branchID = "";
			var tellerID = "";
			var refNum = "";
			var billNumber = "";
			var merchantNote = "";

			string url = "https://Test.B2B.stcpay.com.sa/B2b.DirectPayment.WebApi";



			var DirectPaymentRequestMessage = new
			{
				BranchID = branchID,
				TellerID = tellerID,
				RefNum = refNum,
				BillNumber = billNumber,
				BillDate = DateTime.Now.ToString(),
				MobileNo = mobile,
				Amount = 0,
				MerchantNote = merchantNote,
				//TokenId = "string"
			};




			var bodyObject = new
			{
				DirectPaymentRequestMessage = DirectPaymentRequestMessage
			};

			var body = ConvertToJson(bodyObject);



			using (var client = new HttpClient())
			{
				var httpRequestMessage = new HttpRequestMessage
				{
					Method = HttpMethod.Post,
					RequestUri = new Uri(url),
					Headers = {
						{ HttpRequestHeader.Accept.ToString(), "application/json" },
						{ "X-ClientCode", clientCode },
						{ "X-UserName", username },
						{ "X-Password", password },
					},
					Content = new StringContent(body, Encoding.UTF8, "application/json")
				};
				var response = await client.SendAsync(httpRequestMessage);
				//Console.WriteLine(response.Content.ReadAsStringAsync());
				return (response.StatusCode == HttpStatusCode.OK) ? true : false;

			}
		}


		public static async Task<bool> RegisterAdminToSupportServiceServer(AdminUserResponse admin)
		{
			
			string url = $"{SupportServerURL}/api/v1/admins";
			var bodyObject = new
			{
				name = $"{admin.User.FirstName} {admin.User.LastName}",
				email = admin.UserAccount.Email,
				mobile = admin.User.Mobile,
				//password = admin.Password,
				reference_id = admin.User.Id.ToString()
			};

			var body = ConvertToJson(bodyObject);



			using (var client = new HttpClient())
			{
				var httpRequestMessage = new HttpRequestMessage
				{
					Method = HttpMethod.Post,
					RequestUri = new Uri(url),
					Headers = {
						//{ HttpRequestHeader.Authorization.ToString(), "Bearer YaRF2PtXWrxFHlO8ss4g2cGCFXAveQjB" },
						{ HttpRequestHeader.Accept.ToString(), "application/json" },
						{"Authorization", $"Bearer {SupportServerToken}"}
						//{ "X-Version", "1" }
					},
					Content = new StringContent(body, Encoding.UTF8, "application/json")
				};
				var response = await client.SendAsync(httpRequestMessage);
				//Console.WriteLine(response.Content.ReadAsStringAsync());
				return (response.StatusCode == HttpStatusCode.OK) ? true : false;

			}
		}

		public static async Task<bool> UpdateAdminTokenToSupportServiceServer(AdminUserAccount admin)
		{
			
			string url = $"{SupportServerURL}/api/v1/admins/updateToken";
			var bodyObject = new
			{
				token = admin.Token,
				reference_id = admin.AdminUserId.ToString()
			};

			var body = ConvertToJson(bodyObject);



			using (var client = new HttpClient())
			{
				var httpRequestMessage = new HttpRequestMessage
				{
					Method = HttpMethod.Post,
					RequestUri = new Uri(url),
					Headers = {
						//{ HttpRequestHeader.Authorization.ToString(), "Bearer YaRF2PtXWrxFHlO8ss4g2cGCFXAveQjB" },
						{ HttpRequestHeader.Accept.ToString(), "application/json" },
						{"Authorization", $"Bearer {SupportServerToken}"}
						//{ "X-Version", "1" }
					},
					Content = new StringContent(body, Encoding.UTF8, "application/json")
				};
				var response = await client.SendAsync(httpRequestMessage);
				//Console.WriteLine(response.Content.ReadAsStringAsync());
				return (response.StatusCode == HttpStatusCode.OK) ? true : false;

			}
		}


		// Json Serialize
		public static string ConvertToJson(Object obj)
		{
			var jsonString = new JavaScriptSerializer();
			var jsonStringResult = jsonString.Serialize(obj);
			return jsonStringResult;
		}
		// From Img path to base 64
		public static string ConvertImgPathTo64(string path)
		{
			using (System.Drawing.Image image = System.Drawing.Image.FromFile(path))
			{
				using (MemoryStream m = new MemoryStream())
				{
					image.Save(m, image.RawFormat);
					byte[] imageBytes = m.ToArray();

					// Convert byte[] to Base64 String
					string base64String = Convert.ToBase64String(imageBytes);
					return base64String;
				}
			}
		}
		public static Order UpdateOrder(Order targetOrder,Order order)
		{
			if (order?.AgentId != null)
				targetOrder.AgentId = order.AgentId;
			if (order?.CustomerName != "")
				targetOrder.CustomerName = order.CustomerName;
			if (order?.MoreDetails != "")
				targetOrder.MoreDetails = order.MoreDetails;
			if (order?.Description != "")
				targetOrder.Description = order.Description;
			if (order?.CustomerAddress != "")
				targetOrder.CustomerAddress = order.CustomerAddress;
			if (order?.ProductTypeId != null)
				targetOrder.ProductTypeId = order.ProductTypeId;
			if (order?.CustomerPhone != "")
				targetOrder.CustomerPhone = order.CustomerPhone;
			if (order?.PaymentTypeId != null)
				targetOrder.PaymentTypeId = order.PaymentTypeId;
			if (order?.ModifiedBy != null)
				targetOrder.ModifiedBy = order.ModifiedBy;

			if (order?.PickupLocationLat?.ToString() != "")
				targetOrder.PickupLocationLat = order.PickupLocationLat;

			if (order?.PickupLocationLong?.ToString() != "")
				targetOrder.PickupLocationLong = order.PickupLocationLong;

			if (order?.DropLocationLat?.ToString() != "")
				targetOrder.DropLocationLat = order.DropLocationLat;

			if (order?.DropLocationLong?.ToString() != "")
				targetOrder.DropLocationLong = order.DropLocationLong;

			targetOrder.ModificationDate = DateTime.Now;
			return targetOrder;
		}

	}
	
	

}






