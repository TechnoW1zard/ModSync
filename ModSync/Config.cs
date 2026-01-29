using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace ModSync
{

    public class Config
    {
        public string ModFolder { get; set; }
        public string Server { get; set; }

        public static void SaveConfig(Config config)
        {
            System.IO.File.WriteAllText("config.json", JsonSerializer.Serialize(config));
        }
    }
}
