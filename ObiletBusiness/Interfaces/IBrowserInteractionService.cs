using ObiletData.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObiletBusiness.Interfaces
{
    public interface IBrowserInteractionService
    {
        Task<SessionResponseDto> GetSesion();
    }
}
