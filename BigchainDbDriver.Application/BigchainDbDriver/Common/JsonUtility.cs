﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigchainDbDriver.Common
{
    public class JsonUtility
    {

        public static string SerializeTransactionIntoCanonicalString(string json)
        {
            // Parse json string into JObject.
            var parsedObject = JObject.Parse(json);

            // Sort properties of JObject.
            var normalizedObject = SortPropertiesAlphabetically(parsedObject);

            // Serialize JObject .
            return JsonConvert.SerializeObject(normalizedObject);
        }

        private static JObject SortPropertiesAlphabetically(JObject original)
        {
            var result = new JObject();

            foreach (var property in original.Properties().ToList().OrderBy(p => p.Name))
            {
                var value = property.Value as JObject;

                if (value != null)
                {
                    value = SortPropertiesAlphabetically(value);
                    result.Add(property.Name, value);
                }
                else
                {
                    result.Add(property.Name, property.Value);
                }
            }

            return result;
        }

    }
}
