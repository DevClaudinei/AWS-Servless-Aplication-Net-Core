using Application.Models;
using System.Threading.Tasks;

namespace AppServices.Services.Interfaces;

public interface IVendaAppService
{
    Task<string> CadastrarVenda(CreateVendaRequest vendaRequest);
    void AtualizarVenda(string id, UpdateVendaRequest updateVendaRequest);
    Task<VendaResult> BuscarVendaPorId(string id);
}