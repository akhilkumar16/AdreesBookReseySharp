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
        //uc3//
        [TestMethod]
        public void OnCallingPutAPI_ReturnContactObjects()
        {
            //Arrange
            RestRequest request = new RestRequest("/Contacts/4", Method.Put);
            request.RequestFormat = DataFormat.Json;

            request.AddBody(new Contact
            {
                FirstName = "virat",
                LastName = "kholi",
                PhoneNumber = "12345689",
                Address = "hnk",
                City = "banglore",
                State = "kt",
                Zip = "124568",
                Email = "kholi@gmail.com"
            });
            //Act
            RestResponse response = client.ExecuteAsync(request).Result;
            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Contact contact = JsonConvert.DeserializeObject<Contact>(response.Content);
            Assert.AreEqual("virat", contact.FirstName);
            Assert.AreEqual("kholi", contact.LastName);
            Assert.AreEqual("124568", contact.Zip);
            Console.WriteLine(response.Content);
        }
        //uc4//
        [TestMethod]
        public void OnCallingDeleteAPI_ReturnSuccessStatus()
        {
            //Arrange
            RestRequest request = new RestRequest("/Contacts/5", Method.Delete);
            //Act
            RestResponse response = client.ExecuteAsync(request).Result;
            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Console.WriteLine(response.Content);
        }
    }
}
   