using Domain.Room.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Room;

public class RoomConfiguration : IEntityTypeConfiguration<RoomEntity>
{

    public void Configure(EntityTypeBuilder<RoomEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.OwnsOne(x => x.Price)
            .Property(x => x.Currency);

        builder.OwnsOne(x => x.Price)
            .Property(x => x.Value);
    }
}
