using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RoomTreeFactory
{
    public const int MAX_DEPTH = 10;
    // Rather than const arrays, a config should be used to allow custom runs, like all bosses
    // These are one-indexed. The boss will occur 9 levels into the run
    private readonly int[] BOSS_LEVELS = { 9 };
    private readonly int[] MINIBOSS_LEVELS = { 4 };
    private readonly int[] SHOP_LEVELS = { 3, 5, 8 };
    private readonly int[] BONDING_LEVELS = { 2, 5, 7 };
    private readonly int MAX_PORTALS_PER_ROOM = 2;

    private int nodeCount;

    public RoomTreeFactory()
    {
        nodeCount = 0;
    }

    private RoomNode CreateVictoryRoom()
    {
        RoomNode leaf = new RoomNode(nodeCount++, MAX_DEPTH, "victory");
        leaf.AddComponent(RoomNode.Component.Bonding_Basic);
        leaf.AddComponent(RoomNode.Component.Event_Finish);
        return leaf;
    }

    private RoomNode CreateRoomNode(int current_depth)
    {
        Random r = RunDataManager.GetRandomUtilForComponent("map");
        string[] roomOptions = { "mystery", "swamp", "desert", "volcano", "forest" };
        RoomNode node = new RoomNode(nodeCount++, current_depth, r.Element(roomOptions));

        if (BOSS_LEVELS.Contains(current_depth))
        {
            node.AddComponent(RoomNode.Component.Fight_Boss);
        }
        if (MINIBOSS_LEVELS.Contains(current_depth))
        {
            node.AddComponent(RoomNode.Component.Fight_Miniboss);
        }
        if (SHOP_LEVELS.Contains(current_depth))
        {
            node.AddComponent(RoomNode.Component.Shop_Basic);
        }
        if (BONDING_LEVELS.Contains(current_depth))
        {
            node.AddComponent(RoomNode.Component.Bonding_Basic);
        }
        node.AddComponent(RoomNode.Component.Fight_Standard);

        return node;
    }

    /*
     * Recursively builds the map out to a designated depth
     * Because the whole map isn't revealed, there are a few liberties in the room structure
     * Namely, a single boss will exist in as many as MAX_PORTALS_PER_ROOM ^ MAX_DEPTH rooms.
     * 
     * This could probably be adjusted in the RunDataManager to call another populate instance
     * whenever a leaf is reached that doesn't have a victory condition/max depth.
     * In that instance, it would be somewhat asynchronous generation.
     */
    private void Populate(TreeNode<RoomNode> n, int depth)
    {
        // Iteration has hit the maximum level
        if (depth == MAX_DEPTH)
        {
            n.AddChild(CreateVictoryRoom());
            return;
        }
        int portals = MAX_PORTALS_PER_ROOM; // TODO RNG
        for (int i = 0; i < portals; i++)
        {
            Populate(n.AddChild(CreateRoomNode(depth)), depth + 1);
        }
    }

    // By using a tree, I'm fully aware that I'm committing to storing an unnecessary amount of data.
    // 1, it makes the loading scene have more stuff to do
    // 2, it allows backtracking (eventually?)
    // 3, i like trees
    public TreeNode<RoomNode> Create()
    {
        // TODO get seeded rng
        RoomNode start = CreateRoomNode(1);
        TreeNode<RoomNode> root = new TreeNode<RoomNode>(start);
        Populate(root, 2);

        return root;
    }
}
