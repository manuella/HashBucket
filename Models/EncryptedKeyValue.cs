using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HashBucket.Models
{
    public class EncryptedKeyValue
    {
        [Key]
        public string HashKey { get; set; }
        public string EncryptedValue { get; set; }

    }
}
