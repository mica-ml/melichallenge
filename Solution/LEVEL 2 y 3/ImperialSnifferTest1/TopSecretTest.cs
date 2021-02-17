using NUnit.Framework;
using ImperialSniffer;
using ImperialSniffer.Models;
using ImperialSniffer.Controllers;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;
using ImperialSniffer.Controllers.TopSecret_SplitControllers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace ImperialSnifferTest
{
    public class Tests
    {

        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// Unit Test for GetLocation, get correctly the location.
        /// </summary>
        [Test]
        public void GetLocationTestOK()
        {
            //parameters
            AllSatellitesData allSatellitesData = new AllSatellitesData();
            allSatellitesData.satellites.Add(new SatelliteData("kenobi", 583, new List<string>(new string[] { "este", "", "", "mensaje", "" })));
            allSatellitesData.satellites.Add(new SatelliteData("skywalker", 223, new List<string>(new string[] { "", "es", "", "", "secreto" })));
            allSatellitesData.satellites.Add(new SatelliteData("sato", 500, new List<string>(new string[] { "este", "", "un", "", "" })));
            float locationXexpected = (float)0.4;
            float locationYexpected = (float)98.6;

            // Act
            ShipLocator shipLocator = new ShipLocator();
            XYCoordinates location = shipLocator.GetLocation(allSatellitesData);


            // Assert
            Assert.AreEqual(locationXexpected, location.GetCoordinateX());
            Assert.AreEqual(locationYexpected, location.GetCoordinateY());
            Assert.Pass();
        }

        /// <summary>
        /// Correct ended unit Test for GetMessage. It get correctly the final Message.
        /// </summary>
        [Test]
        public void GetMessageTestOK()
        {
            //parameters
            List<List<string>> allMessagesOk = new List<List<string>>();
            List<List<string>> allMessagesBad = new List<List<string>>();
            allMessagesOk.Add(new List<string>(new string[] { "este", "", "", "mensaje", "" }));
            allMessagesOk.Add(new List<string>(new string[] { "", "es", "", "", "secreto" }));
            allMessagesOk.Add(new List<string>(new string[] { "este", "", "un", "", "" }));
            allMessagesBad.Add(new List<string>(new string[] { "este", "", "", "mensaje", "" }));
            allMessagesBad.Add(new List<string>(new string[] { "", "es", "", "", "secreto" }));
            allMessagesBad.Add(new List<string>(new string[] { "este", "", "un", "" }));
            string messageExpected = "este es un mensaje secreto";
            MessageAssembler messageAssembler = new MessageAssembler();

            // Act
            string messageOk = messageAssembler.GetMessage(allMessagesOk);
            // Assert
            Assert.AreEqual(messageExpected, messageOk);
            Assert.Pass();

            // Act
            string messageBad = messageAssembler.GetMessage(allMessagesBad);
            // Assert
            Assert.AreEqual(messageExpected, messageOk);
            Assert.Pass();
        }

        /// <summary>
        /// Unit test, try to generate an error in the GetMessage method, I did not pass all the words in the message.
        /// </summary>
        [Test]
        public void GetMessageTestFail()
        {
            //parameters
            List<List<string>> allMessagesBad = new List<List<string>>();
            allMessagesBad.Add(new List<string>(new string[] { "este", "", "", "mensaje", "" }));
            allMessagesBad.Add(new List<string>(new string[] { "", "es", "", "", "secreto" }));
            allMessagesBad.Add(new List<string>(new string[] { "este", "", "un", "" }));
            string messageExpected = "este es un mensaje secreto";
            MessageAssembler messageAssembler = new MessageAssembler();

            // Act
            //string messageBad = messageAssembler.GetMessage(allMessagesBad);
            // Assert
            var ex = Assert.Throws<Exception>(() => messageAssembler.GetMessage(allMessagesBad));

            //Assert.AreEqual("Error, There is no enough messages received from satellites. It must be at least 3", ex.Message);
            Assert.AreEqual("Error, messages number of every list must have the same length", ex.Message);
            Assert.Pass();
        }

        /// <summary>
        /// Unit test for DataBase, call save and then Get for a particular satellite information.
        /// </summary>
        [Test]
        public void DbTestOK()
        {
            var options = new DbContextOptionsBuilder<TopSecretSplitContext>()
                            .UseInMemoryDatabase(Guid.NewGuid().ToString())
                            .EnableSensitiveDataLogging()
                            .Options;
            var context = new TopSecretSplitContext(options);

            TopSecretSplitController topSecretSplitController = new TopSecretSplitController(context);

            string parameter = "{\"distance\": 583.0,\"message\": [\"este\", \"\", \"un\", \"\", \"\"]}";

            topSecretSplitController.PostTopSecretSplit("kenobi", parameter);

            ActionResult<string> GetResponse = topSecretSplitController.GetTopSecretSplit("kenobi");

            string result = GetResponse.Value;

            //TopSecretSplitItem topSecretSplitItem1 = JsonConvert.DeserializeObject<TopSecretSplitItem>(result);
            string spectedResult = "{\"name\":\"kenobi\",\"distance\":583.0,\"message\":[\"este\",\"\",\"un\",\"\",\"\"]}";


            Assert.AreEqual(spectedResult, result);
            Assert.Pass();
        }

        /// <summary>
        /// Correct ended functional test. It set the 3 satellites information in the database and then ask for the result.
        /// </summary>
        [Test]
        public void TopSecretSplitFullTestOK()
        {
            var options = new DbContextOptionsBuilder<TopSecretSplitContext>()
                            .UseInMemoryDatabase(Guid.NewGuid().ToString())
                            .EnableSensitiveDataLogging()
                            .Options;
            var context = new TopSecretSplitContext(options);

            TopSecretSplitController topSecretSplitController = new TopSecretSplitController(context);

            string parameter1 = "{\"distance\": 583.0,\"message\": [\"este\", \"\", \"\", \"mensaje\", \"\"]}";
            string parameter2 = "{\"distance\": 223,\"message\": [\"\", \"es\", \"\", \"\", \"secreto\"]}";
            string parameter3 = "{\"distance\": 500,\"message\": [\"este\", \"\", \"un\", \"\", \"\"]}";

            topSecretSplitController.PostTopSecretSplit("kenobi", parameter1);
            topSecretSplitController.PostTopSecretSplit("skywalker", parameter2);
            topSecretSplitController.PostTopSecretSplit("sato", parameter3);

            ActionResult<string> GetResponse = topSecretSplitController.GetTopSecretSplitAll();

            string result = GetResponse.Value;

            //TopSecretSplitItem topSecretSplitItem1 = JsonConvert.DeserializeObject<TopSecretSplitItem>(result);
            string spectedResult = "{\"position\":{\"x\":0.4,\"y\":98.6},\"message\":\"este es un mensaje secreto\"}";


            Assert.AreEqual(spectedResult, result);
            Assert.Pass();
        }

        /// <summary>
        /// Integration Test, it should fail because I did not set the correct number of satellites Info.
        /// </summary>
        [Test]
        public void TopSecretSplitFullTestNotEnoughSatellitesInfoFail()
        {
            var options = new DbContextOptionsBuilder<TopSecretSplitContext>()
                            .UseInMemoryDatabase(Guid.NewGuid().ToString())
                            .EnableSensitiveDataLogging()
                            .Options;
            var context = new TopSecretSplitContext(options);

            TopSecretSplitController topSecretSplitController = new TopSecretSplitController(context);

            string parameter1 = "{\"distance\": 583.0,\"message\": [\"este\", \"\", \"\", \"\", \"\"]}";
       
            topSecretSplitController.PostTopSecretSplit("kenobi", parameter1);

            string spectedResult = "";
            string result = "";

            ActionResult<string> GetResponse = topSecretSplitController.GetTopSecretSplitAll();

            Assert.AreEqual(HttpStatusCode.NotFound, GetResponse.Result);
            Assert.Pass();
        }
    }
}
