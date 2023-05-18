using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howest.MagicCards.Shared.DTO
{
    public record CardReadDTO(
        string Name,
        string? ManaCost,
        string? Type,
        string? Image,
        string? Text
    );
}
