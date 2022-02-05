using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        RestClient client;

        [TestInitialize]
        public void SetUp()
        {
            client = new RestClient("http://localhost:3000");
        }
        private RestResponse GetContactList()
        {
            //Arrange
            RestRequest request = new RestRequest("/Contacts", Method.Get);
            //Act
            RestSharp.RestResponse response = client.ExecuteAsync(request).Result;
            return response;
        }
    }
}
