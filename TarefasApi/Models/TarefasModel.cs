using System.ComponentModel.DataAnnotations.Schema;
using TarefasApi.Enums;

namespace TarefasApi.Models
{
    public class TarefasModel
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public DateTime DataDeInicio { get; set; }
        public DateTime DataDeFinalizacao { get; set; }
        public int EstimativaEmDias { get; set; } 
        public StatusTarefa Status { get; set; }
        public int? UsuarioId { get; set; }
        public virtual UsuariosModel? Usuario { get; set; }

        [NotMapped]
        public string? ArquivoPDF { get; set; }
    }
}
