﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HashBucket.Models;
using HashBucket.Models.DB;
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
        private readonly HashBucketContext _context;

        public HashBucketController(HashBucketContext context)
        {
            _context = context;
        }

        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        /***
        *  If HashKey input is in db, return corresponding encrypted value.
        *  Otherwise, return random string so that client can't discover
        *  valid HashKeys
        ***/
        [HttpGet("{hashKey}")]
        public ActionResult<string> Get(string hashKey)
        {
            log.Warn("Request body for key: " + hashKey);
            var body = _context.hashValues.Find(hashKey).EncryptedValue;

            // your key is invalid, you get indistinguishable junk
            // TODO, we shouldn't user random for security
            if (body == null)
            {
                var random = new Random();
                return new string(Enumerable.Repeat(chars, 100)
                  .Select(s => s[random.Next(s.Length)]).ToArray());
            }
            return body;
        }
    

        /***
         *  Input: exposed CORS-disabled API, key and value can be any string.
         *  Takes the stores the EncryptedValue in the DB with key HashKey
        ***/
        [HttpPost]
        public void Post([FromBody] EncryptedKeyValue value)
        {
            log.Warn("Received key: " + value.HashKey + " and value: " + value.EncryptedValue);
            _context.hashValues.Add(value);
        }

    }
}