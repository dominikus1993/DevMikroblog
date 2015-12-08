﻿using System.Collections.Generic;

namespace DevMikroblog.Domain.Model
{
    public class Tag
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<Post> Post { get; set; } = new List<Post>();
    }
}
