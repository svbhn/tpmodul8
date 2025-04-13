using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

namespace tpmodul8_103022300081
{
    class CovidConfig
    {
        private JsonElement configJson;
        private string configFileName = "covid_config.json";

        private Dictionary<string, string> defaultConfig = new Dictionary<string, string>()
        {
            {"satuan_suhu", "celcius"},
            {"batas_hari_demam", "14"},
            {"pesan_ditolak", "Anda tidak diperbolehkan masuk ke dalam gedung ini"},
            {"pesan_diterima", "Anda dipersilahkan untuk masuk ke dalam gedung ini"}
        };

        public CovidConfig()
        {
            try
            {
                if (File.Exists(configFileName))
                {
                    string jsonString = File.ReadAllText(configFileName);
                    configJson = JsonSerializer.Deserialize<JsonElement>(jsonString);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading configuration: {ex.Message}");
                Console.WriteLine("Using default configuration...");
            }
        }

        public string GetConfig(string key)
        {
            if (configJson.ValueKind != JsonValueKind.Undefined)
            {
                try
                {
                    return configJson.GetProperty(key).GetString();
                }
                catch (Exception)
                {
                    
                }
            }

            if (defaultConfig.ContainsKey(key))
            {
                return defaultConfig[key];
            }

            return null;
        }

        public void UbahSatuan()
        {
            string currentUnit = GetConfig("satuan_suhu");

            var newConfig = new Dictionary<string, string>();

            foreach (var key in defaultConfig.Keys)
            {
                newConfig[key] = GetConfig(key);
            }

            if (currentUnit == "celcius")
            {
                newConfig["satuan_suhu"] = "fahrenheit";
            }
            else
            {
                newConfig["satuan_suhu"] = "celcius";
            }

            string jsonString = JsonSerializer.Serialize(newConfig, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(configFileName, jsonString);

            configJson = JsonSerializer.Deserialize<JsonElement>(jsonString);
        }
    }
}