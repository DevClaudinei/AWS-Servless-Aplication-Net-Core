using DomainModels.Entities;
using System.Threading.Tasks;

namespace DomainServices.Interfaces;

public interface IVendaService
{
    Task<string> CadastrarVenda(Venda venda);
    void AtualizarVenda(string id, Venda venda);
    Task<Venda> BuscarVendaPorId(string id);
}