﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace MVC
{
    public static class GlobalVariables
    {
        public static HttpClient HttpClient = new HttpClient();

        static GlobalVariables()
        {
            HttpClient.BaseAddress = new Uri("https://localhost:44316/api/"); //server local
            //HttpClient.BaseAddress = new Uri("https://localhost:44329/api/"); // Server
            //HttpClient.BaseAddress = new Uri("https://localhost:44361/api/"); //local
            HttpClient.DefaultRequestHeaders.Clear();
            HttpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}