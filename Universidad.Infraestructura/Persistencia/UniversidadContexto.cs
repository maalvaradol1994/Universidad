using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Universidad.Infraestructura.Persistencia;

public partial class UniversidadContexto : DbContext
{
    public UniversidadContexto(DbContextOptions<UniversidadContexto> options)
        : base(options)
    {
    }

    public virtual DbSet<Estudiante> Estudiantes { get; set; }

    public virtual DbSet<Estudiante_Materia_Profesor> Estudiante_Materia_Profesors { get; set; }

    public virtual DbSet<Estudiante_Programa> Estudiante_Programas { get; set; }

    public virtual DbSet<Estudiantes_Por_Materia> Estudiantes_Por_Materias { get; set; }

    public virtual DbSet<Materia> Materias { get; set; }

    public virtual DbSet<Profesor_Materium> Profesor_Materia { get; set; }

    public virtual DbSet<Profesore> Profesores { get; set; }

    public virtual DbSet<Programa> Programas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.HasKey(e => e.est_id).HasName("PRIMARY");

            entity.HasIndex(e => e.est_identificacion, "est_identificacion_UNIQUE").IsUnique();

            entity.HasIndex(e => e.est_usuario_id, "est_usuario_id_UNIQUE").IsUnique();

            entity.Property(e => e.est_id).HasColumnType("int(11)");
            entity.Property(e => e.est_activo)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("smallint(1)");
            entity.Property(e => e.est_identificacion)
                .HasMaxLength(30)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.est_nombre).HasMaxLength(100);
            entity.Property(e => e.est_usuario_id)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");

            entity.HasOne(d => d.est_usuario).WithOne(p => p.Estudiante)
                .HasForeignKey<Estudiante>(d => d.est_usuario_id)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_Estudiante_Usuario");
        });

        modelBuilder.Entity<Estudiante_Materia_Profesor>(entity =>
        {
            entity.HasKey(e => e.estmatpr_id).HasName("PRIMARY");

            entity.ToTable("Estudiante_Materia_Profesor");

            entity.HasIndex(e => e.estmatpr_estudiante_id, "fk_Estudiante_Programa_Estudiante");

            entity.HasIndex(e => e.estmatpr_profesor_materia_id, "fk_Estudiante_Programa_profesor");

            entity.Property(e => e.estmatpr_id).HasColumnType("int(11)");
            entity.Property(e => e.estmatpr_activo)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("smallint(1)");
            entity.Property(e => e.estmatpr_estudiante_id)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
            entity.Property(e => e.estmatpr_profesor_materia_id)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");

            entity.HasOne(d => d.estmatpr_estudiante).WithMany(p => p.Estudiante_Materia_Profesors)
                .HasForeignKey(d => d.estmatpr_estudiante_id)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_Estudiante_Programa_Estudiante");

            entity.HasOne(d => d.estmatpr_profesor_materia).WithMany(p => p.Estudiante_Materia_Profesors)
                .HasForeignKey(d => d.estmatpr_profesor_materia_id)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_Estudiante_Programa_profesor");
        });

        modelBuilder.Entity<Estudiante_Programa>(entity =>
        {
            entity.HasKey(e => e.estprog_id).HasName("PRIMARY");

            entity.ToTable("Estudiante_Programa");

            entity.HasIndex(e => e.estprog_estudiante_id, "fk_Estudiante_Estudiante");

            entity.HasIndex(e => e.estprog_programa_id, "fk_Estudiante_Programa");

            entity.Property(e => e.estprog_id).HasColumnType("int(11)");
            entity.Property(e => e.estprog_activo)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("smallint(1)");
            entity.Property(e => e.estprog_estudiante_id)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
            entity.Property(e => e.estprog_programa_id)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
            entity.Property(e => e.estprog_total_creditos)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");

            entity.HasOne(d => d.estprog_estudiante).WithMany(p => p.Estudiante_Programas)
                .HasForeignKey(d => d.estprog_estudiante_id)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_Estudiante_Estudiante");

            entity.HasOne(d => d.estprog_programa).WithMany(p => p.Estudiante_Programas)
                .HasForeignKey(d => d.estprog_programa_id)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_Estudiante_Programa");
        });

        modelBuilder.Entity<Estudiantes_Por_Materia>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Estudiantes_Por_Materias");

            entity.Property(e => e.estudiante_nombre).HasMaxLength(100);
            entity.Property(e => e.id_estudiante).HasColumnType("int(11)");
            entity.Property(e => e.id_materia).HasColumnType("int(11)");
            entity.Property(e => e.identificacion)
                .HasMaxLength(30)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.materia)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.valor_creditos)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("smallint(6)");
        });

        modelBuilder.Entity<Materia>(entity =>
        {
            entity.HasKey(e => e.mat_id).HasName("PRIMARY");

            entity.Property(e => e.mat_id).HasColumnType("int(11)");
            entity.Property(e => e.mat_activo)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("smallint(6)");
            entity.Property(e => e.mat_codigo)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.mat_nombre)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.mat_valor_creditos)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("smallint(6)");
        });

        modelBuilder.Entity<Profesor_Materium>(entity =>
        {
            entity.HasKey(e => e.profmat_id).HasName("PRIMARY");

            entity.HasIndex(e => e.profmat_materia_id, "fk_Profesor_Materia");

            entity.HasIndex(e => e.profmat_profesor_id, "fk_Profesor_Profesor");

            entity.Property(e => e.profmat_id).HasColumnType("int(11)");
            entity.Property(e => e.profmat_activo)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("smallint(1)");
            entity.Property(e => e.profmat_materia_id)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
            entity.Property(e => e.profmat_profesor_id)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");

            entity.HasOne(d => d.profmat_materia).WithMany(p => p.Profesor_Materia)
                .HasForeignKey(d => d.profmat_materia_id)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_Profesor_Materia");

            entity.HasOne(d => d.profmat_profesor).WithMany(p => p.Profesor_Materia)
                .HasForeignKey(d => d.profmat_profesor_id)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_Profesor_Profesor");
        });

        modelBuilder.Entity<Profesore>(entity =>
        {
            entity.HasKey(e => e.prof_id).HasName("PRIMARY");

            entity.HasIndex(e => e.prof_usuario_id, "prof_usuario_id_UNIQUE").IsUnique();

            entity.Property(e => e.prof_id).HasColumnType("int(11)");
            entity.Property(e => e.prof_activo)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("smallint(1)");
            entity.Property(e => e.prof_identificacion)
                .HasMaxLength(30)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.prof_nombre).HasMaxLength(100);
            entity.Property(e => e.prof_usuario_id)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");

            entity.HasOne(d => d.prof_usuario).WithOne(p => p.Profesore)
                .HasForeignKey<Profesore>(d => d.prof_usuario_id)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_Profesor_Usuario");
        });

        modelBuilder.Entity<Programa>(entity =>
        {
            entity.HasKey(e => e.prog_id).HasName("PRIMARY");

            entity.Property(e => e.prog_id).HasColumnType("int(11)");
            entity.Property(e => e.prog_activo)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("smallint(1)");
            entity.Property(e => e.prog_nombre)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.prog_total_creditos)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("smallint(6)");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.usu_id).HasName("PRIMARY");

            entity.HasIndex(e => e.usu_identificacion, "usu_identificacion").IsUnique();

            entity.HasIndex(e => e.usu_usuario, "usu_usuario").IsUnique();

            entity.Property(e => e.usu_id).HasColumnType("int(11)");
            entity.Property(e => e.usu_activo)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("smallint(1)");
            entity.Property(e => e.usu_clave).HasMaxLength(100);
            entity.Property(e => e.usu_identificacion).HasMaxLength(30);
            entity.Property(e => e.usu_usuario).HasMaxLength(100);

            entity.HasOne(d => d.usu_identificacionNavigation).WithOne(p => p.Usuario)
                .HasPrincipalKey<Estudiante>(p => p.est_identificacion)
                .HasForeignKey<Usuario>(d => d.usu_identificacion)
                .HasConstraintName("fk_Usuario_Estudiante");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
