using MessagePack;
using OnlineDiscussion.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace OnlineDiscussion.Models
{
    public class TopicViewModel
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int TopicId { get; set; }
        public string TopicName { get; set; }

        public string description { get; set; }

        public DateTime CreatedDate { get; set; }

        public OnlineDiscussionUser user { get; set; }
    }
}
