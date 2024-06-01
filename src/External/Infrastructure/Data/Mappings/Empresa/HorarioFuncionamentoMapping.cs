using Domain.Entities.Empresa;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Mappings
{
    public class HorarioFuncionamentoMapping : IEntityTypeConfiguration<HorarioFuncionamento>
    {
        public void Configure(EntityTypeBuilder<HorarioFuncionamento> builder)
        {
            builder.ToTable("HORARIO_FUNCIONAMENTO");

            builder.HasKey(h => h.Id);

            builder.Property(h => h.Id).HasColumnName("ID").IsRequired();
            builder.Property(h => h.EmpresaId).HasColumnName("EMPRESA_ID").IsRequired();
            builder.Property(h => h.Hora).HasColumnName("HORA").IsRequired();
            builder.Property(h => h.Minutos).HasColumnName("MINUTOS").IsRequired();

            builder
                .Property(e => e.Codigo)
                .HasColumnName("CODIGO")
                .IsRequired()
                .HasDefaultValueSql("NEWID()");

            builder
                .Property(h => h.DiaDaSemana)
                .HasColumnName("DIA_DA_SEMANA")
                .IsRequired()
                .HasConversion<int>();

            builder
                .HasOne(h => h.Empresa)
                .WithMany(e => e.HorariosDeFuncionamento)
                .HasForeignKey(h => h.EmpresaId);
        }
    }
}
