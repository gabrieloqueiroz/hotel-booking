using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entities = Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class RoomConfiguration : IEntityTypeConfiguration<Entities.Room>
{

    public void Configure(EntityTypeBuilder<Entities.Room> builder)
    {
        builder.HasKey(x => x.Id);
        builder.OwnsOne(x => x.Price)
            .Property(x => x.Currency);

        builder.OwnsOne(x => x.Price)
            .Property(x => x.Value);
    }
}
