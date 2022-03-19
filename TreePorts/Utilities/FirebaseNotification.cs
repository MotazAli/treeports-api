using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace TreePorts.Utilities
{
    public class FirebaseNotification
    {

        private static string serverKey = "AAAAr6KBLK0:APA91bFx7cxNKLfxF_UQl2LwdcVLRLTBwECubPR0CiH2gV8PjxbeRZVuLv88XUW1g-y3DHdMlr5xME_bBIRfooq1XSTbs-1dYC57rcNVx8mgdxukf1eiZLL0_rLTAVjJUsKYi6BPowWw";
        private static string senderId = "754345651373";
        private static string webAddr = "https://fcm.googleapis.com/fcm/send";


        public static async Task<string?> SendNotification(string deviceToken, string title, string massage,CancellationToken cancellationToken)
        {
            var data = new Dictionary<string, string>()
                {
                    {"Title", title},
                    {"Body", massage}
                };



            var payload = new
            {
                to = deviceToken,
                priority = "high",
                content_available = true,
                data = data
                //notification = new
                //{
                //    body = massage,
                //    title = title
                //},
                //content_available = true
            };


            var body = Utility.ConvertToJson(payload);
            using var client = new HttpClient();
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(webAddr),
                Headers = {
                        { HttpRequestHeader.Authorization.ToString(), $"key={serverKey}" },
                        {"Sender", $"id={senderId}"},
                        { HttpRequestHeader.Accept.ToString(), "application/json" },
                        //{ "X-Version", "1" }
                    },
                Content = new StringContent(body, Encoding.UTF8, "application/json")
            };
            var response = await client.SendAsync(httpRequestMessage, cancellationToken);
            if (response.StatusCode == HttpStatusCode.OK) return await response.Content.ReadAsStringAsync(cancellationToken);

            return null;

        }





        public static async Task<string> SendNotification(string deviceToken, string title, string massage)
        {
            
            var result = "-1";
            var httpWebRequest = WebRequest.CreateHttp(webAddr);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Headers.Add(string.Format("Authorization: key={0}", serverKey));
            httpWebRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
            httpWebRequest.Method = "POST";


            var data = new Dictionary<string, string>()
                {
                    {"Title", title},
                    {"Body", massage}
                };



            var payload = new
            {
                to = deviceToken,
                priority = "high",
                content_available = true,
                data = data
                //notification = new
                //{
                //    body = massage,
                //    title = title
                //},
                //content_available = true
            };
            //var serializer = new JavaScriptSerializer();
            using (var streamWriter = new StreamWriter(await httpWebRequest.GetRequestStreamAsync()))
            {
                string json = JsonConvert.SerializeObject(payload); //serializer.Serialize(payload);
                await streamWriter.WriteAsync(json);
                await streamWriter.FlushAsync();
            }

            var httpResponse =  await httpWebRequest.GetResponseAsync();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = await streamReader.ReadToEndAsync();
            }
            return result;
        }


        public static async Task<string> SendNotificationToTopic(string topic, string title, string massage)
        {
            var result = "-1";
            var httpWebRequest = WebRequest.CreateHttp(webAddr);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Headers.Add(string.Format("Authorization: key={0}", serverKey));
            httpWebRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
            httpWebRequest.Method = "POST";


            var data = new Dictionary<string, string>()
                {
                    {"Title", title},
                    {"Body", massage}
                };



            var payload = new
            {
                to = "/topics/" + topic,
                priority = "high",
                content_available = true,
                data = data
                //notification = new
                //{
                //    body = massage,
                //    title = title
                //},
                //content_available = true
            };
            //var serializer = new JavaScriptSerializer();
            using (var streamWriter = new StreamWriter(await httpWebRequest.GetRequestStreamAsync()))
            {
                string json = JsonConvert.SerializeObject(payload); //serializer.Serialize(payload);
                await streamWriter.WriteAsync(json);
                await streamWriter.FlushAsync();
            }

            var httpResponse =  await httpWebRequest.GetResponseAsync();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = await streamReader.ReadToEndAsync();
            }
            return result;
        }


        public static async Task<string> SendNotificationToMultiple(List<string> devicesTokens, string title, string massage)
        {
            var result = "-1";
            var httpWebRequest = WebRequest.CreateHttp(webAddr);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Headers.Add(string.Format("Authorization: key={0}", serverKey));
            httpWebRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
            httpWebRequest.Method = "POST";


            var data = new Dictionary<string, string>()
                {
                    {"Title", title},
                    {"Body", massage}
                };



            var payload = new
            {
                registration_ids = devicesTokens,
                priority = "high",
                content_available = true,
                data = data
                //notification = new
                //{
                //    body = massage,
                //    title = title
                //},
                //content_available = true
            };
            //var serializer = new JavaScriptSerializer();
            using (var streamWriter = new StreamWriter( await httpWebRequest.GetRequestStreamAsync()))
            {
                string json = JsonConvert.SerializeObject(payload); //serializer.Serialize(payload);
                await streamWriter.WriteAsync(json);
                await streamWriter.FlushAsync();
            }

            var httpResponse =  await httpWebRequest.GetResponseAsync();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
            return result;
        }



    }



}

