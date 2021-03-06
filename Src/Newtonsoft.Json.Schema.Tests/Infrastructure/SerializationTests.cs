﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Newtonsoft.Json.Schema.Tests.Infrastructure
{
    [TestFixture]
    public class SerializationTests : TestFixtureBase
    {
        [Test]
        public void SerializeSchema()
        {
            JSchema schema = new JSchema
            {
                Properties =
                {
                    { "first", new JSchema { Type = JSchemaType.String | JSchemaType.Null } }
                }
            };

            string s1 = schema.ToString();
            string s2 = JsonConvert.SerializeObject(schema, Formatting.Indented);

            Assert.AreEqual(s1, s2);
        }

        [Test]
        public void SerializeError()
        {
            ValidationError error = new ValidationError
            {
                LineNumber = 11,
                LinePosition = 5,
                Value = "A value!",
                Message = "A message!",
                Path = "sdf.sdf",
                ErrorType = ErrorType.MinimumLength,
                SchemaId = new Uri("test.xml", UriKind.RelativeOrAbsolute),
                SchemaBaseUri = new Uri("test.xml", UriKind.RelativeOrAbsolute),
                Schema = new JSchema { Type = JSchemaType.Number },
                ChildErrors =
                {
                    new ValidationError
                    {
                        Message = "Child message!"
                    }
                }
            };

            string json = JsonConvert.SerializeObject(error, Formatting.Indented);

            StringAssert.AreEqual(@"{
  ""Message"": ""A message!"",
  ""LineNumber"": 11,
  ""LinePosition"": 5,
  ""Path"": ""sdf.sdf"",
  ""Value"": ""A value!"",
  ""SchemaId"": ""test.xml"",
  ""SchemaBaseUri"": ""test.xml"",
  ""ErrorType"": ""minLength"",
  ""ChildErrors"": [
    {
      ""Message"": ""Child message!"",
      ""LineNumber"": 0,
      ""LinePosition"": 0,
      ""Path"": null,
      ""Value"": null,
      ""SchemaId"": null,
      ""SchemaBaseUri"": null,
      ""ErrorType"": ""none"",
      ""ChildErrors"": []
    }
  ]
}", json);
        }
    }
}