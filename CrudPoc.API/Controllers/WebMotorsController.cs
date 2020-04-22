using System;
using System.Collections.Generic;
using CrudPoc.API.Models;
using CrudPoc.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CrudPoc.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebMotorsController : ControllerBase
    {
        private readonly FileLog _fileLog;
        private readonly string _webMotorsUrl;
        private readonly string _fileLogDirectory;
        private readonly IConfiguration _configuration;

        public WebMotorsController(IConfiguration configuration)
        {
            _fileLog = new FileLog();
            _configuration = configuration;
            _webMotorsUrl = configuration.GetSection("WebMotors").GetValue<string>("APIUrl");
            _fileLogDirectory = configuration.GetSection("AppSettings").GetSection("LogDirectory").Value;
        }

        [HttpGet, Route("Make")]
        public IEnumerable<MakeWM> GetAllMakes()
        {
            try
            {
                _fileLog.WriteInfo("Obtendo os fabricantes.", _fileLogDirectory);
                return new ApiClient(_webMotorsUrl).Get<IEnumerable<MakeWM>>(_configuration.GetSection("WebMotors").GetValue<string>("MakeMethod")).Result;
            }
            catch (Exception e)
            {
                _fileLog.WriteException(e, _fileLogDirectory);
                throw;
            }

        }

        [HttpGet, Route("Model")]
        public IEnumerable<ModelWM> GetModelsByMake(int makeID)
        {
            try
            {
                _fileLog.WriteInfo("Obtendo os modelos.", _fileLogDirectory);
                return new ApiClient(_webMotorsUrl).Get<IEnumerable<ModelWM>>($"{_configuration.GetSection("WebMotors").GetValue<string>("ModelMethod")}{makeID}").Result;
            }
            catch (Exception e)
            {
                _fileLog.WriteException(e, _fileLogDirectory);
                throw;
            }
        }

        [HttpGet, Route("Version")]
        public IEnumerable<VersionWM> GetVersionsByModel(int modelID)
        {
            try
            {
                _fileLog.WriteInfo("Obtendo as versões.", _fileLogDirectory);
                return new ApiClient(_webMotorsUrl).Get<IEnumerable<VersionWM>>($"{_configuration.GetSection("WebMotors").GetValue<string>("VersionMethod")}{modelID}").Result;
            }
            catch (Exception e)
            {
                _fileLog.WriteException(e, _fileLogDirectory);
                throw;
            }
        }
    }
}