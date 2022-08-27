using Layer.Domain.Entities;

namespace Layer.API.Models.DTO
{
    public class UsuarioPosicaoDto
    {
        public string CPF { get; set; }
        public List<PosicaoDto> Positions { get; set; }

        public UsuarioPosicaoDto(string cPF, List<PosicaoDto> positions)
        {
            CPF = cPF;
            Positions = positions;
        }
    }
}
