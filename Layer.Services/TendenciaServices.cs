using Layer.Domain.Entities;
using Layer.Repository;
using Layer.Repository.Interfaces;
using Layer.Services.Base;
using Layer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
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

        public void Inserir(string symbol, double currentprice)
        {
            var obj = new Tendencia(symbol, currentprice);

            if (Double.IsNormal(obj.CurrentPrice))
            {
                if (!(obj.CurrentPrice > 0))
                    throw new Exception($"Valor da currentprice precisa ser maior que zero.");

                if (String.IsNullOrEmpty(obj.Symbol))
                    throw new Exception($"Valor da currentprice precisa ser maior que zero.");

                _tendenciaRepository.Insert(obj);
            }
            else
            {
                throw new Exception($"Valor da currentprice não esta no formatado.");
            }            
        }
    }
}
