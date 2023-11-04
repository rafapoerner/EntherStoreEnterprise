using ESE.Core.Data;

namespace ESE.Clients.API.Models
{
    public interface IClientRepository : IRepository<Client>
    {
        void ToAdd(Client client);

        Task<IEnumerable<Client>> GetAll();
        Task<Client> GetByCpf(string cpf);
    }
}
