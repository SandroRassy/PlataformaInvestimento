using Layer.Domain.Entities;
using Layer.Repository.Base;
using Layer.Repository.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Repository
{
    public sealed class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(IMongoCollection<Usuario> collectionName) : base(collectionName)
        {
        }

        public UsuarioRepository(IConnectionFactory connectionFactory, string databaseName, string collectionName)
            : base(connectionFactory, databaseName, collectionName)
        {
        }

        public Usuario QueryFilter(string cpf)
        {
            var retorno = new Usuario();

            if (!String.IsNullOrEmpty(cpf))
                retorno = _collectionName.AsQueryable<Usuario>().FirstOrDefault(w => w.CPF == cpf);            

            return retorno;
        }
    }
}
