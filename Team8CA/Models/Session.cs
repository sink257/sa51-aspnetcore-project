using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Team8CA.Models
{
    [Table("Sessions")]
    public class Session
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public long Timestamp { get; set; }
        public Session() { }

        public Session(string Id, string Username, long Timestamp)
        {
            this.Id = Id;
            this.Username = Username;
            this.Timestamp = Timestamp;
        }
    }
}
