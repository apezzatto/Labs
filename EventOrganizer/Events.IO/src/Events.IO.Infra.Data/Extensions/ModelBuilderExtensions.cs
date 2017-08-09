using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Events.IO.Infra.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void AddConfiguration<TEntity>(this ModelBuilder modelbuilder,
            EntityTypeConfiguration<TEntity> configuration) where TEntity : class
        {
            configuration.Map(modelbuilder.Entity<TEntity>());
        }
    }
}
