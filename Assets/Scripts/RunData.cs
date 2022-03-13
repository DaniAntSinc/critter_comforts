using System;
using System.Collections;
using System.Collections.Generic;

public static class RunData
{
    // Thinking that this should move to a better interface. Maybe RunStatus enum friends with other enums?
    public enum Status : ushort
    {
        None = 0,       // No run currently exists
        Pending = 1,    // Run has been initialized, but not yet started
        Active = 2,     // Run is ongoing
        Abandoned = 3,  // Run was abandoned or otherwise forfeit
        Lost = 4,       // Run ended with a Lose condition (e.g. lost all HP in combat)
        Successful = 5, // Run ended with a Victory condition (e.g. moving past the boss)
    }

    // Metadata
    public static Status CurrentStatus { get; private set; }
    public static string CreationTime { get; private set; }
    private static string Seed;

    // Run Data
    private static int Score; // Rather than a single integer, the score will be composed of different pieces. Maybe a score SO, instead?
    private static TreeNode<RoomNode> Map;
    private static TreeNode<RoomNode> CurrentRoom;
    //[Not yet implemented] private Creature[] Team

    // Clear out all data from any previous runs
    public static void Initialize()
    {
        CurrentStatus = Status.Pending;
        Score = 0;
        Map = null;
        // Get player's starter
    }

    public static void SetBaseSeed(string seed)
    {
        Seed = seed;
    }

    public static void SetMap(TreeNode<RoomNode> root)
    {
        // Where's the cursor?
        Map = root;
        CurrentRoom = root;
    }

    public static void AdvanceToRoom(TreeNode<RoomNode> nextRoom)
    {
        CurrentRoom = nextRoom;
    }

    public static TreeNode<RoomNode> GetMap()
    {
        return Map;
    }

    public static TreeNode<RoomNode> GetRoom()
    {
        return CurrentRoom;
    }

    public static void StartRun()
    {
        // Only start the run when the state is moved to "active"
        CurrentStatus = Status.Active;
        CreationTime = Convert.ToString((int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
    }

    public static void UpdateScore(int delta)
    {
        Score += delta;
    }

    public static int GetScore()
    {
        // Aggregate score 
        return Score;
    }
}
