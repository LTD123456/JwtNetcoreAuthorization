using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class Blog : BaseEntity
    {
        public Blog()
        {
            UpdatedTime = DateTime.Now;
        }
        public string Name { get; set; }

        public virtual List<Post> Posts { get; set; }
    }
}
