using Microsoft.EntityFrameworkCore;
using W9_assignment_template.Data;

namespace W9_assignment_template.Services;

public class Menu
{
    private readonly GameContext _gameContext;

    public Menu(GameContext gameContext)
    {
        _gameContext = gameContext;
    }

    public void Show()
    {
        while (true)
        {
            Console.WriteLine("1. Display Rooms");
            Console.WriteLine("2. Display Characters");
            Console.WriteLine("3. Add a Character");
            Console.WriteLine("4. Find a Character");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    DisplayRooms();
                    break;
                case "2":
                    DisplayCharacters();
                    break;
                case "3":
                    AddCharacter();
                    break;
                case "4":
                    FindCharacter();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid option, please try again.");
                    break;
            }
        }
    }

    public void DisplayRooms()
    {
        var rooms = _gameContext.Rooms.Include(r => r.Characters).ToList();

        foreach (var room in rooms)
        {
            Console.WriteLine($"Room: {room.Name} - {room.Description}");
            foreach (var character in room.Characters)
            {
                Console.WriteLine($"    Character: {character.Name}, Level: {character.Level}");
            }
        }
    }

    public void DisplayCharacters()
    {
        var characters = _gameContext.Characters.ToList();
        if (characters.Any())
        {
            Console.WriteLine("\nCharacters:");
            foreach (var character in characters)
            {
                Console.WriteLine($"Character ID: {character.Id}, Name: {character.Name}, Level: {character.Level}, Room ID: {character.RoomId}");
            }
        }
        else
        {
            Console.WriteLine("No characters available.");
        }
    }

    public void AddCharacter()
    {
        while (true)
        {
            var characters = _gameContext.Characters.ToList();
            var rooms = _gameContext.Rooms.Include(r => r.Characters).ToList();
            bool found = false;

            Console.Write("Enter character name: ");
            var name = Console.ReadLine();

            Console.Write("Enter character level: ");
            var level = int.Parse(Console.ReadLine());

            Console.Write("Enter room ID for the character: ");
            var roomId = int.Parse(Console.ReadLine());

            Console.Write("Enter character type: ");
            var characterType = Console.ReadLine();

            switch (characterType.ToLower())
            {
                case "goblin":
                    var goblin = new Goblin
                    {
                        Name = name,
                        Level = level,
                        RoomId = roomId
                    };

                    // Moves through the rooms list to find the right ID
                    for (int i = 0; i < rooms.Count; i++)
                    {
                        if (rooms[i].Id == roomId)
                        {
                            found = true;
                        }
                    }


                    // Final results
                    if (found)
                    {
                        _gameContext.Characters.Add(goblin);
                        rooms[roomId - 1].Characters.Add(goblin);
                        _gameContext.SaveChanges();
                    }
                    else
                    {
                        Console.WriteLine("The room you selected does not exist.");
                    }
                    break;
                case "player":
                    var player = new Player
                    {
                        Name = name,
                        Level = level,
                        RoomId = roomId
                    };

                    // Moves through the rooms list to find the right ID
                    for (int i = 0; i < rooms.Count; i++)
                    {
                        if (rooms[i].Id == roomId)
                        {
                            found = true;
                        }
                    }


                    // Final results
                    if (found)
                    {
                        _gameContext.Characters.Add(player);
                        rooms[roomId - 1].Characters.Add(player);
                        _gameContext.SaveChanges();
                    }
                    else
                    {
                        Console.WriteLine("The room you selected does not exist.");
                    }
                    break;
                default:
                    Console.WriteLine("The character type you selected does not exist.");
                    break;
            }

            // Exit while loop
            if (found)
            {
                break;
            }
        }
    }

    public void FindCharacter()
    {
        var characters = _gameContext.Characters.ToList();

        Console.Write("Enter character name to search: ");
        var name = Console.ReadLine();

        try
        {
            // LINQ search for right name
            var result = characters.Where(c => c.Name.ToLower() == name.ToLower()).Select(c => c).First();
            if (result != null)
            {
                var character = result;
                Console.WriteLine($"Character ID: {character.Id}\nName: {character.Name}\nLevel: {character.Level}\nRoom ID: {character.RoomId}");
            }
            else
            {
                Console.WriteLine($"There is no character by the name of {name}.");
            }
        }
        catch (InvalidOperationException e)
        {
            // Don't know why it's catching an exception, but here's the code for catching it
            Console.WriteLine($"There is no character by the name of {name}.");
        }
    }
}
