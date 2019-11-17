using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Test.Util
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod00()
        {
            System.Text.StringBuilder val = new System.Text.StringBuilder();
            val.AppendLine("{}");
            main(val.ToString());
        }

        [TestMethod]
        public void TestMethod01()
        {
            System.Text.StringBuilder val = new System.Text.StringBuilder();
            val.AppendLine("{");
            val.AppendLine("    \"key1\": {\"valkey1\":1},");
            val.AppendLine("    \"key2\": {\"valkey2\":2}");
            val.AppendLine("}");

            main(val.ToString());
        }
        
        [TestMethod]
        public void TestMethod02()
        {
            System.Text.StringBuilder val = new System.Text.StringBuilder();
            val.AppendLine("{");
            val.AppendLine("    \"sample\": [");
            val.AppendLine("        \"value1\",");
            val.AppendLine("        \"value2\"");
            val.AppendLine("    ]");
            val.AppendLine("}");

            main(val.ToString());

        }

        [TestMethod]
        public void TestMethod03()
        {
            System.Text.StringBuilder val = new System.Text.StringBuilder();
            val.AppendLine("{");
            val.AppendLine("    \"key1\": {");
            val.AppendLine("        \"child1\": [");
            val.AppendLine("            \"value1\",");
            val.AppendLine("            \"value2\",");
            val.AppendLine("        ]");  
            val.AppendLine("    }");
            val.AppendLine("}");

            main(val.ToString());
        }

        [TestMethod]
        public void TestMethod04()
        {
            System.Text.StringBuilder val = new System.Text.StringBuilder();
            val.AppendLine("{");      
            val.AppendLine("    \"key1\": {");
            val.AppendLine("        \"valkey1\":\"valval1\",");
            val.AppendLine("        \"valkey2\":\"valval2\",");
            val.AppendLine("    }");
            val.AppendLine("}");

            main(val.ToString());
        }

        [TestMethod]
        public void TestMethod05()
        {
            System.Text.StringBuilder val = new System.Text.StringBuilder();
            val.AppendLine("{");
            val.AppendLine("    \"key1\": {");
            val.AppendLine("        \"valkey1\":\"valval1\",");
            val.AppendLine("        \"valkey2\":\"valval2\",");
            val.AppendLine("    },");
            val.AppendLine("    \"key2\": {\"valkey1\":1},");
            val.AppendLine("    \"key3\": {");
            val.AppendLine("        \"valkey1\":\"valval1\",");
            val.AppendLine("        \"valkey2\":\"valval2\",");
            val.AppendLine("    }");
            val.AppendLine("}");

            main(val.ToString());
        }

        [TestMethod]
        public void TestMethod06()
        {
            System.Text.StringBuilder val = new System.Text.StringBuilder();
            val.AppendLine("{");
            val.AppendLine("    \"key1\": {");
            val.AppendLine("        \"valkey1\":\"valval1\",");
            val.AppendLine("        \"sub1\": {");
            val.AppendLine("            \"valkey1\":\"valval1\",");
            val.AppendLine("            \"valkey2\":\"valval2\"");
            val.AppendLine("        },");
            val.AppendLine("        \"sub2\": [");
            val.AppendLine("            \"valval1\",");
            val.AppendLine("            \"valval2\"");
            val.AppendLine("        ]");
            val.AppendLine("    },");
            val.AppendLine("    \"key2\": {\"valkey1\":1}");
            val.AppendLine("}");

            main(val.ToString());
        }

        [TestMethod]
        public void TestMethod07()
        {
            System.Text.StringBuilder val = new System.Text.StringBuilder();
            val.AppendLine("{");
            val.AppendLine("    \"key1\": [");
            val.AppendLine("        [");
            val.AppendLine("            \"subobject1\",");
            val.AppendLine("            \"subobject2\"");
            val.AppendLine("        ],");
            val.AppendLine("        [");
            val.AppendLine("            {\"subobject1\": \"subvalue1\"},");
            val.AppendLine("            {\"subobject2\": \"subvalue2\"}");
            val.AppendLine("        ],");
            val.AppendLine("        {");
            val.AppendLine("            \"subobject1\": {");
            val.AppendLine("                \"subsubobject1\": \"subvalue1\",");
            val.AppendLine("                \"subsubobject2\": \"subvalue2\",");
            val.AppendLine("                \"subsubobject3\": [");
            val.AppendLine("                    \"value\"");
            val.AppendLine("                ],");
            val.AppendLine("                \"subsubobject4\": {},");
            val.AppendLine("                \"subsubobject5\": []");
            val.AppendLine("             }");
            val.AppendLine("        },");
            val.AppendLine("        {");
            val.AppendLine("            \"valkey3\":\"valval3\",");
            val.AppendLine("            \"valkey4\":\"valval4\"");
            val.AppendLine("        }");
            val.AppendLine("    ],");
            val.AppendLine("    \"key2\": {");
            val.AppendLine("        \"subobject1\": [");
            val.AppendLine("            \"subobject1\",");
            val.AppendLine("            \"subobject2\"");
            val.AppendLine("        ],");
            val.AppendLine("        \"subobject2\": [");
            val.AppendLine("            {\"subobject1\": \"subvalue1\"},");
            val.AppendLine("            {\"subobject2\": \"subvalue2\"}");
            val.AppendLine("        ],");
            val.AppendLine("        \"subobject4\": {");
            val.AppendLine("            \"valkey3\":\"valval3\",");
            val.AppendLine("            \"valkey4\":\"valval4\"");
            val.AppendLine("        }");
            val.AppendLine("    },");
            val.AppendLine("    \"subsubobject4\": {},");
            val.AppendLine("    \"subsubobject5\": []");
            val.AppendLine("}");

            main(val.ToString());
        }

        private void main(string json)
        {
            var obj = new JsonStreamDeserialize.JsonStreamDeserialize();

            var expected = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            var actual = obj.Deserialize(json);

            Assert.AreEqual(expected.Count, actual.Count);
            foreach (var key in expected.Keys)
            {
                Assert.AreEqual(JsonConvert.SerializeObject(expected[key]), JsonConvert.SerializeObject(actual[key]));
            }
        }
    }
}
