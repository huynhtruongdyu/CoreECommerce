﻿using CEC.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace CEC.Application.Abstractions.Contexts
{
    public interface IApplicationDbContext
    {
        DbSet<Product> Products { get; set; }
    }
}