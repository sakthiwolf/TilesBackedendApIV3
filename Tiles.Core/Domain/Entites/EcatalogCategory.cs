﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiles.Core.Domain.Entites
{
    public class EcatalogCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string CoverPhoto { get; set; } = null!;
    }
}
