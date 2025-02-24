using FUForum.BackendServer.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FUForum.BackendServer.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>().Property(x => x.Id).HasMaxLength(50).IsUnicode(false);
            builder.Entity<User>().Property(x => x.Id).HasMaxLength(50).IsUnicode(false);
            builder.Entity<LabelInKnowledgeBase>().HasKey(x => new { x.KnowledgeBaseId, x.LabelId });
            builder.Entity<Permission>().HasKey(x => new { x.FunctionId, x.RoleId, x.CommandId });
            builder.Entity<Vote>().HasKey(x => new { x.KnowledgeBaseId, x.UserId });
            builder.Entity<CommandInFunction>().HasKey(x => new { x.CommandId, x.FunctionId });
            builder.HasSequence("KnowledgeBaseSequence");
        }
        public DbSet<Command> Commands { set; get; }
        public DbSet<CommandInFunction> CommandInFunctions { set; get; }

        public DbSet<ActivityLog> ActivityLogs { set; get; }
        public DbSet<Category> Categories { set; get; }
        public DbSet<Comment> Comments { set; get; }
        public DbSet<Function> Functions { set; get; }
        public DbSet<KnowledgeBase> KnowledgeBases { set; get; }
        public DbSet<Label> Labels { set; get; }
        public DbSet<LabelInKnowledgeBase> LabelInKnowledgeBases { set; get; }
        public DbSet<Permission> Permissions { set; get; }
        public DbSet<Report> Reports { set; get; }
        public DbSet<Vote> Votes { set; get; }

        public DbSet<Attachment> Attachments { get; set; }
    }
}
