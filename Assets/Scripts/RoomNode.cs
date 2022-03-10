using System;
using System.Collections;
using System.Collections.Generic;

public class RoomNode
{
    public enum Component : ushort
    {
        Fight_Standard = 0,
        Fight_Miniboss = 1,
        Fight_Boss = 2,
        Shop_Basic = 3,
        Bonding_Basic = 4,
        Event_Finish = 5,
    }

    public int Id { get; private set; }
    public int Level { get; private set; }
    public string Biome { get; private set; }

    private List<Component> components;

    public RoomNode(int id, int level, string biome)
    {
        this.Id = id;
        this.Level = level;
        this.Biome = biome;

        this.components = new List<Component>();
    }

    // Insert the given component as something that will spawn in the room.
    // Should there be a limit to the number of encounters?
    public void AddComponent(Component component)
    {
        components.Add(component);
    }

    public override string ToString()
    {
        return String.Format("ID {0} Depth {1} Biome {2}", Id, Level, Biome);
    }
}
