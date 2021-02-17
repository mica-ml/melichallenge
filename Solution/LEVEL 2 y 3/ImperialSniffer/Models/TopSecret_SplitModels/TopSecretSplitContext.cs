using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImperialSniffer.Models
{
    /// <summary>
    /// Generate the database context with the particular Item for TopSecretSplitItem
    /// </summary>
    public class TopSecretSplitContext : DbContext
    {
        public TopSecretSplitContext(DbContextOptions<TopSecretSplitContext> options)
            : base(options)
        {
        }
        public DbSet<TopSecretSplitItem> TopSecretSplitItems { get; set; }
    }
}
