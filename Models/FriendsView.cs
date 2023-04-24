using OnlineDiscussion.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace OnlineDiscussion.Models
{
    public class FriendsView
    {
        [Key]
        public int FriendId { get; set; }

        public TopicViewModel Topic { get; set; }

        public string email { get; set; }

        public OnlineDiscussionUser User { get; set; }
    }
}
