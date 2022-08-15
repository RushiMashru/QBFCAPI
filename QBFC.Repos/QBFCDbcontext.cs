using Microsoft.EntityFrameworkCore;
using QBFC.Models.DataModel;
using System;

namespace QBFC.Repos
{
    public class QBFCDbcontext: Microsoft.EntityFrameworkCore.DbContext
    {
        public QBFCDbcontext(DbContextOptions<QBFCDbcontext> options): base(options)
        {

        }

        public virtual DbSet<tAPILogs> tAPILogs { get; set; }
        public virtual DbSet<tAuthDetails> tAuthDetails { get; set; }
    }
}
