using System.ComponentModel;

namespace TarefasApi.Enums
{
    public enum StatusTarefa
    {
        [Description("Agendada")]
        Agendada = 1,
        [Description("Em andamento")]
        EmAndamento = 2,
        [Description("Concluida")]
        Finalizada = 3,

    }
}
