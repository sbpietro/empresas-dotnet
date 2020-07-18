using App_Empresas_Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace App_Empresas_Repository_Impl.Context
{
    public class AppEmpresaContext : DbContext
    {
        public AppEmpresaContext(DbContextOptions<AppEmpresaContext> contextOptions) : base(contextOptions)
        {

        }

        public DbSet<Empresa> Empresas { get; set; }

        public DbSet<TipoEmpresa> TipoEmpresas { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Empresa>(empresaMap =>
            {
                empresaMap.ToTable("TB_EMPRESA").HasKey(x => x.Id);

                empresaMap.Property(x => x.Nome)
                    .HasColumnName("nome")
                    .HasColumnType("varchar(50)")
                    .IsRequired();

                empresaMap.Property(x => x.Cnpj)
                    .HasColumnName("numero_cnpj")
                    .HasColumnType("varchar(18)")
                    .IsRequired();

                empresaMap.Property(x => x.IdTipoEmpresa)
                    .HasColumnName("id_tipo_empresa")
                    .IsRequired();

                empresaMap.HasOne(x => x.TipoEmpresa)
                    .WithMany(x => x.Empresas)
                    .HasForeignKey(x => x.IdTipoEmpresa);

            });

            modelBuilder.Entity<TipoEmpresa>(entity =>
            {
                entity.ToTable("TB_TIPO_EMPRESA").HasKey(x => x.Id);

                entity.Property(x => x.Nome)
                    .HasColumnName("nome")
                    .HasColumnType("varchar(50)")
                    .IsRequired();

                entity.Property(x => x.Sigla)
                    .HasColumnName("sigla")
                    .HasColumnType("varchar(8)")
                    .IsRequired();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("TB_USERS").HasKey(x => x.UserID);

                entity.Property(x => x.AccessKey)
                    .HasColumnName("AccessKey")
                    .HasColumnType("varchar(32)")
                    .IsRequired();
            });
        }

    }
}
