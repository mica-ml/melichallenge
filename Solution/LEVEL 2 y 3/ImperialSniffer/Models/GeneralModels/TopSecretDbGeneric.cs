using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImperialSniffer.Models
{
    public interface ITopSecretContext : IDisposable
    {
        DbSet<TopSecretSplitItem> TopSecretSplitItems { get; }
        int SaveChanges();
        void MarkAsModified(TopSecretSplitItem item);
    }
}
