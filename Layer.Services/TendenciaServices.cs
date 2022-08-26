using Layer.Domain.Entities;
using Layer.Repository;
using Layer.Repository.Interfaces;
using Layer.Services.Base;
using Layer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Services
{
    public class TendenciaServices : Services<Tendencia>, ITendenciaService
    {
        private readonly ITendenciaRepository _tendenciaRepository;
        public TendenciaServices(ITendenciaRepository tendenciaRepository) : base(tendenciaRepository)
        {
            _tendenciaRepository = tendenciaRepository;
        }
    }
}
