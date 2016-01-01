using System.Runtime.Serialization;

namespace DevMikroblog.WebApp.Models
{
    [DataContract]
    public class AddCommentViewModel
    {
        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public long PostId { get; set; }
    }
}