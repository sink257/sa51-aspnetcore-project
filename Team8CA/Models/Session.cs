using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Team8CA.Models
{
    [Table("Sessions")]
    public class Session
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string SessionID { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public long Timestamp { get; set; }
        public string CustomerID { get; set; }
        public Session()
        {

        }

        public Session(string SessionID, string Username, string FirstName, long Timestamp, string CustomerID)
        {
            this.SessionID = SessionID;
            this.Username = Username;
            this.FirstName = FirstName;
            this.Timestamp = Timestamp;
            this.CustomerID = CustomerID;
        }
    }
}
