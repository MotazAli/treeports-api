using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TreePorts.Models;
using TreePorts.Utilities;

namespace TreePorts.Controllers
{

    public class TestToken { 
        public string token { get; set; } 
    }

    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        
        private readonly IWebHostEnvironment _hostingEnvironment;
        private IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork,IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _unitOfWork = unitOfWork;
        }


        [HttpPost("ShortToken")]
        public IActionResult ShortToken([FromBody] TestToken testToken) 
        {
            var shortToken = Convert.ToBase64String(Encoding.UTF8.GetBytes(testToken.token));
            return Ok(shortToken);
        }


        [HttpPost("LongToken")]
        public IActionResult LongToken([FromBody] TestToken testToken)
        {
            var longToken = Encoding.UTF8.GetString(Convert.FromBase64String(testToken.token));
            return Ok(longToken);
        }

        [HttpGet]
        public  ContentResult Get() 
        {
            //try
            //{
            //    var allConutries = CountriesCode.allConutries;
            //    foreach (Country country in allConutries)
            //    {
            //        country.CreationDate = DateTime.Now;
            //        country.CreatedBy = 1;
            //        await _unitOfWork.CountryRepository.Insert(country);
            //        await _unitOfWork.Save();
            //    }

            //    return "done";
            //}
            //catch (Exception e)
            //{
            //    return e.Message;
            //}



            //var path = _hostingEnvironment.ContentRootPath;
            //path = path + "\\Auth.json";
            //FirebaseApp app = null;
            //try
            //{
            //    app = FirebaseApp.Create(new AppOptions()
            //    {
            //        Credential = GoogleCredential.FromFile(path)
            //    }, "myApp");
            //}
            //catch (Exception ex)
            //{
            //    app = FirebaseApp.GetInstance("myApp");
            //}

            //var fcm = FirebaseMessaging.GetMessaging(app);
            //Message message = new Message()
            //{
            //    Notification = new Notification
            //    {
            //        Title = "My push notification title",
            //        Body = "Content for this push notification"
            //    },
            //    Data = null
            //    //new Dictionary<string, string>()
            //    // {
            //    //     { "AdditionalData1", "data 1" },
            //    //     { "AdditionalData2", "data 2" },
            //    //     { "AdditionalData3", "data 3" },
            //    // }
            //    ,

            //    Token = "WebsiteUpdates"
            //};

            //var result  = await fcm.SendAsync(message);








            var path = _hostingEnvironment.ContentRootPath + "/Templets/welcome.html";
            string content = System.IO.File.ReadAllText(path);

            return new ContentResult
            {
                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.OK,
                Content = content
            };

        }
    }
}
