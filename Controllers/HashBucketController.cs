using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HashBucket.Models;
using HashBucket.Models.DB;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HashBucket.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllOrigins")]
    [ApiController]
    public class HashBucketController : ControllerBase
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly HashBucketContext _context;

        public HashBucketController(HashBucketContext context)
        {
            _context = context;
            _context.hashValues.Add(new EncryptedKeyValue() { HashKey = "sanity", EncryptedValue = "check" });
        }

        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        /***
        *  If HashKey input is in db, return corresponding encrypted value.
        *  Otherwise, return random string so that client can't discover
        *  valid HashKeys
        ***/
        [HttpGet("{hashKey}")]
        [EnableCors("AllowOrigin")]
        public ActionResult<string> Get(string hashKey)
        {
            log.Warn("Request body for key: " + hashKey);
            var body = _context.hashValues.Find(hashKey);
            // your key is invalid, you get indistinguishable junk
            // TODO, we shouldn't user random for security
            // TODO this should be consistent per key, so that 
            // all responses are consistent

            // Right now I can tell if a key is valid if it returns
            // the same thing twice in a row

            if (body == null)
            {
                var random = new Random();
                return new string(Enumerable.Repeat(chars, 100)
                  .Select(s => s[random.Next(s.Length)]).ToArray());
            }
            return body.EncryptedValue;
        }
    

        /***
         *  Input: exposed CORS-disabled API, key and value can be any string.
         *  Takes the stores the EncryptedValue in the DB with key HashKey
         *  
         *  If we attempt attempt to the key, we can either throw an exception (signaling to an attacker that this key is valid)
         *  or overwrite the previous data. I'm unsure which option is preferable at this point, TODO: make this judgement based
         *  on the application
        ***/
        [HttpPost]
        [EnableCors("AllOrigins")]
        public IActionResult Post([FromBody] EncryptedKeyValue value)
        {
            log.Warn("Received key: " + value.HashKey + " and value: " + value.EncryptedValue);
            var x = _context.hashValues.Add(new EncryptedKeyValue() { HashKey = value.HashKey, EncryptedValue = value.EncryptedValue });
            try
            {
                _context.SaveChanges();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                log.Error("Failed to update DB: " + e.Message + e.InnerException);
            }

            return new OkResult();
        }

    }
}