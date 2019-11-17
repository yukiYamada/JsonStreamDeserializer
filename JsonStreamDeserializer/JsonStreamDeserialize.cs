using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonStreamDeserialize
{
    public class JsonStreamDeserialize
    {
        public Dictionary<string, object> Deserialize(string jsonString)
        {
            var ret = new Dictionary<string, object>();
            var stream = new System.IO.MemoryStream(Encoding.GetEncoding("utf-8").GetBytes(jsonString));
            using (var reader = new System.IO.StreamReader(stream))
            {
                // 最初の{はいらない
                reader.ReadLine();
                JContainer result = (JContainer)Deserialize(reader, null, null).First().Value;
                foreach(JProperty value in result.Values().Values())
                {
                    ret.Add(value.Name, value.Value);
                }
            }
            return ret;
        }

        private Dictionary<string, object> Deserialize(System.IO.StreamReader reader, Newtonsoft.Json.Linq.JContainer parent, string parentType)
        {
            Dictionary<string, object> ret = new Dictionary<string, object>();
            string header = string.Empty;

            if (parent == null)
            {
                header = "dummy" + ret.Count.ToString();
                string jsonstring = "{" + header + ": { }}";
                JContainer container = (JContainer)JsonConvert.DeserializeObject(jsonstring);
                ret.Add(header, container);
                Deserialize(reader, (JContainer)container.First().Last(), "{");
            }

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (reader.EndOfStream)
                {
                    break;
                }
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                if (line.EndsWith(": {") || line.EndsWith(": ["))
                {
                    string newHeader = line.Split(':')[0].Replace("\t", "").Replace("\"", "");
                    string type = line.EndsWith(": {") ? "{" : "[";

                    string newHeaderJson = "{" + newHeader + ": " + (type == "{" ? "{}" : "[]") + "}";
                    JContainer newContainer = (JContainer)JsonConvert.DeserializeObject(newHeaderJson);
                    Deserialize(reader, (JContainer)newContainer.First().Last(), type);
                    parent.Add(newContainer.First());
                    continue;
                }

                string replacedLine = line.Replace("\t", "").Replace(" ", "").Replace(",", "");
                if (replacedLine == "{" || replacedLine == "[")
                {
                    string newHeader = "dummy";
                    string type = replacedLine == "{" ? "{" : "[";
                    string newHeaderJson = "{" + newHeader + ": " + (type == "{" ? "{}" : "[]") + "}";
                    JContainer newContainer = (JContainer)JsonConvert.DeserializeObject(newHeaderJson);
                    Deserialize(reader, (JContainer)newContainer.First().Last(), type);
                    parent.Add(newContainer.First().First);
                    continue;
                }

                if (replacedLine == "}" || replacedLine == "]")
                {
                    return null;
                }

                JToken jconvalue;
                if (line.IndexOf(":") != -1)
                {
                    if (replacedLine.StartsWith("{") && replacedLine.EndsWith("}"))
                    {
                        jconvalue = (JObject)JsonConvert.DeserializeObject(line.Replace("\t", "").TrimEnd(','));
                    }
                    else
                    {
                        jconvalue = ((JObject)JsonConvert.DeserializeObject("{" + line.Replace("\t", "").TrimEnd(',') + "}")).First;
                    }
                }
                else
                {
                    jconvalue = ((JObject)JsonConvert.DeserializeObject("{ \"dummy\" :[" + line.Replace("\t", "").TrimEnd(',') + "]}")).First.First.First;
                }

                parent.Add(jconvalue);
            }

            return ret;
        }
    }
}
