using Microsoft.EntityFrameworkCore;
using ProjectToSocial.Server.Models;
using Task = ProjectToSocial.Server.Models.Task;

namespace ProjectToSocial.Server.DataContext;

public partial class ApplicationContext : DbContext
{
    public ApplicationContext()
    {
    }

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Chat> Chats { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Subtask> Subtasks { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-5QT96PO;Initial Catalog=EternalTask;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Chat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CHATS__64C91E4EB1CB4382");

            entity.ToTable("CHATS");

            entity.HasIndex(e => new { e.IdFirstUser, e.IdSecondUser }, "UQ_CHATS_USERS").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID_CHAT");
            entity.Property(e => e.IdFirstUser).HasColumnName("ID_FIRST_USER");
            entity.Property(e => e.IdSecondUser).HasColumnName("ID_SECOND_USER");

            entity.HasOne(d => d.FirstUser).WithMany(p => p.ChatFirstUser)
                .HasForeignKey(d => d.IdFirstUser)
                .HasConstraintName("FK_CHATS_FIRST_USER_USERS");

            entity.HasOne(d => d.SecondUser).WithMany(p => p.ChatSecondUser)
                .HasForeignKey(d => d.IdSecondUser)
                .HasConstraintName("FK_CHATS_SECOND_USER_USERS");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__63BDAD11BD3D8457");

            entity.ToTable("MESSAGES");

            entity.Property(e => e.Id).HasColumnName("ID_MESSAGE");
            entity.Property(e => e.Content)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("CONTENT");
            entity.Property(e => e.IdChat).HasColumnName("ID_CHAT");
            entity.Property(e => e.IdProject).HasColumnName("ID_PROJECT");
            entity.Property(e => e.IdUser).HasColumnName("ID_USER");
            entity.Property(e => e.Time)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("TIME");

            entity.HasOne(d => d.Chat).WithMany(p => p.Messages)
                .HasForeignKey(d => d.IdChat)
                .HasConstraintName("FK_CHATS_MESSAGES");

            entity.HasOne(d => d.Project).WithMany(p => p.Messages)
                .HasForeignKey(d => d.IdProject)
                .HasConstraintName("FK_PROJECTS_MESSAGES");

            entity.HasOne(d => d.User).WithMany(p => p.Messages)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK_USERS_MESSAGES");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PROJECTS__DD4373E16A5E4453");

            entity.ToTable("PROJECTS");

            entity.Property(e => e.Id).HasColumnName("ID_PROJECT");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("NAME_PROJECT");

            entity.HasMany(d => d.Users).WithMany(p => p.Projects)
                .UsingEntity<Dictionary<string, object>>(
                    "ProgectMember",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_USERS_PROGECT_MEMBERS"),
                    l => l.HasOne<Project>().WithMany()
                        .HasForeignKey("IdProject")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_PROJECTS_PROGECT_MEMBERS"),
                    j =>
                    {
                        j.HasKey("IdProject", "IdUser").HasName("PK__PROGECT___C41C3BA550E03F94");
                        j.ToTable("PROGECT_MEMBERS");
                        j.IndexerProperty<int>("IdProject").HasColumnName("ID_PROJECT");
                        j.IndexerProperty<int>("IdUser").HasColumnName("ID_USER");
                    });
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__C7D0F6577D90E9C8");

            entity.ToTable("ROLES");

            entity.Property(e => e.Id).HasColumnName("ID_ROLE");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("user")
                .HasColumnName("NAME_ROLE");
        });

        modelBuilder.Entity<Subtask>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SUBTASKS__1A48C57DCAFB0B29");

            entity.ToTable("SUBTASKS");

            entity.Property(e => e.Id).HasColumnName("ID_SUBTASK");
            entity.Property(e => e.Completed)
                .HasDefaultValue(false)
                .HasColumnName("COMPLETED");
            entity.Property(e => e.Content)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CONTENT");
            entity.Property(e => e.IdTask).HasColumnName("ID_TASK");
            entity.Property(e => e.IdUser).HasColumnName("ID_USER");

            entity.HasOne(d => d.Task).WithMany(p => p.Subtasks)
                .HasForeignKey(d => d.IdTask)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TASKS_SUBTASKS");

            entity.HasOne(d => d.User).WithMany(p => p.Subtasks)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USERS_SUBTASKS");
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__D150942723F6722B");

            entity.ToTable("TASKS");

            entity.Property(e => e.Id).HasColumnName("ID_TASK");
            entity.Property(e => e.Completed)
                .HasDefaultValue(false)
                .HasColumnName("COMPLETED");
            entity.Property(e => e.Content)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CONTENT");
            entity.Property(e => e.IdProject).HasColumnName("ID_PROJECT");

            entity.HasOne(d => d.Project).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.IdProject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROJECTS_TASKS");

            entity.HasMany(d => d.Users).WithMany(p => p.Tasks)
                .UsingEntity<Dictionary<string, object>>(
                    "TaskAssignment",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_USERS_TASK_ASSIGNMENTS"),
                    l => l.HasOne<Task>().WithMany()
                        .HasForeignKey("IdTask")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_TASKS_TASK_ASSIGNMENTS"),
                    j =>
                    {
                        j.HasKey("IdTask", "IdUser").HasName("PK__TASK_ASS__C80FDC6317E29E0C");
                        j.ToTable("TASK_ASSIGNMENTS");
                        j.IndexerProperty<int>("IdTask").HasColumnName("ID_TASK");
                        j.IndexerProperty<int>("IdUser").HasColumnName("ID_USER");
                    });
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__USERS__95F48440E3FFDDA9");

            entity.ToTable("USERS");

            entity.HasIndex(e => e.Id, "IX_ROLES_USERS");

            entity.Property(e => e.Id).HasColumnName("ID_USER");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Family)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FAMILY");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FIRST_NAME");
            entity.Property(e => e.IdRole).HasColumnName("ID_ROLE");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LAST_NAME");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ROLES_USERS");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
