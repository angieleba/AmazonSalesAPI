using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmazonSales.Models
{
    public class UserFollowing
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public string FollowedUserId { get; set; }
        public User FollowedUser { get; set; }
    }
}
