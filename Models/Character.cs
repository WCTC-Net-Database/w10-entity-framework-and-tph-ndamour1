namespace W9_assignment_template.Models;

public abstract class Character : ICharacter
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }

    // Foreign key to Room
    public int RoomId { get; set; }

    // Navigation property to Room
    public virtual Room Room { get; set; }

    // Navigation property to Abilities
    public virtual ICollection<Ability> Abilities { get; set; }

    public virtual void Attack(ICharacter target)
    {
        Console.WriteLine($"{Name} attacks {target.Name}!");
    }
}
