using Microsoft.EntityFrameworkCore;

public class GimnasioDbContext : DbContext
{
    public GimnasioDbContext(DbContextOptions<GimnasioDbContext> options) : base(options) { }

    public DbSet<Alumno> Alumno { get; set; } = null!;
    public DbSet<Profesor> Profesor { get; set; } = null!;  
    public DbSet<Plan> Planes { get; set; } = null!;
    public DbSet<Asistencia> Asistencia { get; set; } = null!;
    public DbSet<AlumnoPlan> AlumnoPlan { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Relación Alumno - AlumnoPlan
        modelBuilder.Entity<AlumnoPlan>()
            .HasOne(ap => ap.Alumno)
            .WithMany(a => a.AlumnoPlanes)
            .HasForeignKey(ap => ap.AlumnoId)
            .OnDelete(DeleteBehavior.Cascade); // Si se borra el alumno, se eliminan sus registros en AlumnoPlan

        // Relación Plan - AlumnoPlan
        modelBuilder.Entity<AlumnoPlan>()
            .HasOne(ap => ap.Plan)
            .WithMany(p => p.AlumnoPlanes)
            .HasForeignKey(ap => ap.PlanId)
            .OnDelete(DeleteBehavior.Restrict); // No se puede eliminar un plan si hay alumnos inscritos


        modelBuilder.Entity<Alumno>()
            .HasIndex(a => a.DNI)
            .IsUnique();

        modelBuilder.Entity<Alumno>()
            .HasOne(a => a.Plan)
            .WithMany()
            .HasForeignKey(a => a.PlanId)
            .OnDelete(DeleteBehavior.Restrict); // No borrar un plan si hay alumnos con él

        
        modelBuilder.Entity<Profesor>()
            .HasIndex(p => p.Email)
            .IsUnique();

        modelBuilder.Entity<Plan>()
            .Property(p => p.Precio)
            .HasColumnType("decimal(10,2)");

        modelBuilder.Entity<Asistencia>()
            .HasOne(a => a.Alumno)
            .WithMany(a => a.Asistencias)
            .HasForeignKey(a => a.AlumnoId)
            .OnDelete(DeleteBehavior.Cascade); 

        modelBuilder.Entity<Asistencia>()
            .HasIndex(a => a.FHRegistro);
    }
}
