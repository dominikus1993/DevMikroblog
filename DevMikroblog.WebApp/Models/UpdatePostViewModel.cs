using System.Runtime.Serialization;

namespace DevMikroblog.WebApp.Models
{
    [DataContract]
    public class UpdatePostViewModel
    {
        [DataMember]
        public long Id { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Message { get; set; }
    }
}