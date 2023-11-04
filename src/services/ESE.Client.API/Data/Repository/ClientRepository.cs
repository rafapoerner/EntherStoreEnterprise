using ESE.Clients.API.Models;
using ESE.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace ESE.Clients.API.Data.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly ClientsContext _context;

        public ClientRepository(ClientsContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<Client>> GetAll()
        {
            return await _context.Clients.AsNoTracking().ToListAsync();
        }

        public async Task<Client> GetByCpf(string cpf)
        {
            return await _context.Clients.FirstOrDefaultAsync(c => c.Cpf.Numero == cpf);
        }

        public void ToAdd(Client client)
        {
            _context.Clients.Add(client);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
