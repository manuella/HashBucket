using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HashBucket.Models.DB
{
    public class HashBucketContext : DbContext
    {
        public HashBucketContext(DbContextOptions<HashBucketContext> options) : base(options) { }
        public DbSet<EncryptedKeyValue> hashValues { get; set; }
    }
}