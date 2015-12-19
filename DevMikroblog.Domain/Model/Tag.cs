using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DevMikroblog.Domain.Model
{
    [DataContract(IsReference = true)]
    public class Tag
    {

        [DataMember]
        public long Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
