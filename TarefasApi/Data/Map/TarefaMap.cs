using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TarefasApi.Models;

namespace TarefasApi.Data.Map
{
    public class TarefaMap : IEntityTypeConfiguration<TarefasModel>
    {
        public void Configure(EntityTypeBuilder<TarefasModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Descricao).HasMaxLength(2000);
            builder.Property(x => x.DataDeInicio).HasColumnType("date");
            builder.Property(x => x.DataDeFinalizacao).HasColumnType("date");
            builder.Property<int?>(x => x.EstimativaEmDias);
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.UsuarioId);

            builder.HasOne(x => x.Usuario);
        }
    }
}

