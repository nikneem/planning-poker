using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;

namespace HexMaster.Helpers.Configuration
{
    public class MongoDbSettings
    {

        public const string SettingName = "MongoDb";

        public string Hostname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string DatabaseName { get; set; }

        public string GetConnectionString()
        {
            return $"mongodb://{Username}:{Password}@{Hostname}";
        }
    }
}
