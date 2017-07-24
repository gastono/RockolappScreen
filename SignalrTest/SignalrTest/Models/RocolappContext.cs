namespace SignalrTest.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class RocolappContext : DbContext
    {
        public RocolappContext()
            : base("name=Rockolapp")
        {
        }

        public virtual DbSet<RocolappUser> RocolappUser { get; set; }
        public virtual DbSet<Screen> Screen { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
