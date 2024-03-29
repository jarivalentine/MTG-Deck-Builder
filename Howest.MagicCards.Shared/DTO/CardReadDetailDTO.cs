﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howest.MagicCards.Shared.DTO
{
    public record CardReadDetailDTO(
        long Id,
        string Name,
        string? ConvertedManaCost,
        string? Type,
        string? OriginalImageUrl,
        string? Text
    );
}
