<<<<<<< HEAD
﻿using System.Text.Json.Serialization;
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
>>>>>>> bec6640294993d8172af067491a66adacfa7e98a

namespace Models
{
    public class AccountDTO
    {
<<<<<<< HEAD
        [JsonPropertyName("Login")]
        public string Login { get; set; }=String.Empty;
        [JsonPropertyName("Password")]
        public string Password { get; set; } = String.Empty;
=======
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
>>>>>>> bec6640294993d8172af067491a66adacfa7e98a
    }
}
