using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HashBucket.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HashBucket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HashBucketController : ControllerBase
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        /***
        *  If HashKey input is in db, return corresponding encrypted value.
        *  Otherwise, return random string so that client can't discover
        *  valid HashKeys
        ***/
        [HttpGet("{hashKey}")]
        public ActionResult<string> Get(string hashKey)
        {
            return "Mock EncryptedValue for " + hashKey;
        }

        /***
         *  Input: exposed CORS-disabled API, key and value can be any string.
         *  Takes the stores the EncryptedValue in the DB with key HashKey
        ***/
        [HttpPost]
        public void Post([FromBody] EncryptedKeyValue value)
        {
            log.Warn("Received key: " + value.HashKey + " and value: " + value.EncryptedValue);
        }

    }
}