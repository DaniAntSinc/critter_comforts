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

    private int id;
    private int level;
    private string biome;

    private List<Component> components;

    public RoomNode(int id, int level, string biome)
    {
        this.id = id;
        this.level = level;
        this.biome = biome;

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
        return String.Format("{0} {1} {2}", id, level, biome);
    }
}
