using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.Dto
{
    public class OneNetInput
    {
        public int type { get; set; }
        public int dev_id { get; set; }
        public string ds_id { get; set; }
        public int at { get; set; }
        public string value { get; set; }
        public int status { get; set; }
        public int login_type { get; set; }
        public int cmd_type { get; set; }
        public string cmd_id { get; set; }
        public string msg_signature { get; set; }
        public string nonce { get; set; }
        public string enc_msg { get; set; }
    }
}
