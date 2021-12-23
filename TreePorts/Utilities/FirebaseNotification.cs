using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace TreePorts.Utilities
{
    public class FirebaseNotification
    {

        private static string serverKey = "AAAAr6KBLK0:APA91bFx7cxNKLfxF_UQl2LwdcVLRLTBwECubPR0CiH2gV8PjxbeRZVuLv88XUW1g-y3DHdMlr5xME_bBIRfooq1XSTbs-1dYC57rcNVx8mgdxukf1eiZLL0_rLTAVjJUsKYi6BPowWw";
        private static string senderId = "754345651373";
        private static string webAddr = "https://fcm.googleapis.com/fcm/send";


        private FirebaseNotification()
        {
        }


        public static string SendNotification(string deviceToken, string title, string massage)
        {
            var result = "-1";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
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
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(payload); //serializer.Serialize(payload);
                streamWriter.Write(json);
                streamWriter.Flush();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
            return result;
        }


        public static string SendNotificationToTopic(string topic, string title, string massage)
        {
            var result = "-1";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
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
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(payload); //serializer.Serialize(payload);
                streamWriter.Write(json);
                streamWriter.Flush();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
            return result;
        }


        public static string SendNotificationToMultiple(List<string> devicesTokens, string title, string massage)
        {
            var result = "-1";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
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
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(payload); //serializer.Serialize(payload);
                streamWriter.Write(json);
                streamWriter.Flush();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
            return result;
        }



    }



}

