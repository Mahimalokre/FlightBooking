using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedData.Models
{
    public  class ResponseTokenModel
    {
        public string? Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
