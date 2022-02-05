using AdressBookResetSharp;
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
        // UC2 //
        [TestMethod]
        public void ReadEntriesFromJsonServer()
        {
            RestResponse response = GetContactList();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            List<Contact> employeeList = JsonConvert.DeserializeObject<List<Contact>>(response.Content);
            Assert.AreEqual(2, employeeList.Count);
            foreach (Contact c in employeeList)
            {
                Console.WriteLine($"Id: {c.Id}\tFullName: {c.FirstName} {c.LastName}" +
                    $"\tPhoneNo: {c.PhoneNumber}\tAddress: {c.Address}\tCity: {c.City}" +
                    $"\tState: {c.State}\tZip: {c.Zip}\tEmail: {c.Email}");
            }
        }
    }
}