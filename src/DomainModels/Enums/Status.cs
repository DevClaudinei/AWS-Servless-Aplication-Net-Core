using System.Text.Json.Serialization;

namespace DomainModels.Enums;

//[Flags]
//[EnumAsInt]
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Status
{
    AguardandoPagamento = 1,
    PagamentoAprovado = 2,
    EnviadoParaTransportadora = 3,
    Entregue = 4,
    Cancelada = 5
}