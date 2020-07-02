using FT_Aranda.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FT_Aranda.API
{
    public class WritterLog : IWritterLog
    {
        // Globals
        private readonly string path;
        private readonly CustomSettings _customSettings;

        public WritterLog(IOptions<CustomSettings> options)
        {
            // Dependency injection
            _customSettings = options.Value;
            path = _customSettings.PathLog;
        }

        #region Methods

        public void WriteLog(string source, string method, object data)
        {
            try
            {
                var log = new
                {
                    Date = DateTime.Now,
                    ApiSource = source,
                    Method = method,
                    Request = JsonConvert.SerializeObject(data)
                };

                string nameFile = $"{source}_{DateTime.Now.Year}{DateTime.Now.Month}{DateTime.Now.Day}.txt";
                string fullPath = $"{path}\\{nameFile}";

                if (!File.Exists(fullPath))
                {
                    using (StreamWriter file = File.CreateText(fullPath))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Serialize(file, log);
                    }
                }
                else
                {
                    File.AppendAllText(fullPath, JsonConvert.SerializeObject(log));
                }
            }
            catch (WritterLogException ex)
            {
                throw ex;
            }
        }

        #endregion
    }

    public class WritterLogException : Exception
    {
        public WritterLogException(string message) : base(message)
        {

        }
    }
}
