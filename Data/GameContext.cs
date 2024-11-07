using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using W9_assignment_template.Models;

namespace W9_assignment_template.Data;

public class GameContext : DbContext
{
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Character> Characters { get; set; }

    public GameContext(DbContextOptions<GameContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure TPH for Character hierarchy
        modelBuilder.Entity<Character>()
            .HasDiscriminator<string>("Discriminator")
            .HasValue<Player>("Player")
            .HasValue<Goblin>("Goblin");
                
        // TODO Configure TPH for Ability hierarchy
        modelBuilder.Entity<Ability>()
            .HasDiscriminator<string>("Discriminator")
            .HasValue<PlayerAbility>("Shove")
            .HasValue<GoblinAbility>("Taunt");
        
        // Configure many-to-many relationship between Character and Ability
        modelBuilder.Entity<Character>()
            .HasMany(c => c.Abilities)
            .WithMany(a => a.Characters)
            .UsingEntity(j => j.ToTable("CharacterAbilities"));
        
        base.OnModelCreating(modelBuilder);
    }
}
