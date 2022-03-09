using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Interface with the RunData storage
public static class RunDataManager
{
    /*
     * Returns a bool that indicates whether there is a run that can be loaded
     * Should be a combination of state and save data
     */
    public static bool HasExistingRun()
    {
        return RunData.CurrentStatus == RunData.Status.Pending || RunData.CurrentStatus == RunData.Status.Active;
    }

    private static void RequireExistingRun()
    {
        if (!HasExistingRun())
        {
            throw new RunNotActiveException("No run currently exists");
        }
        return;
    }

    public static void BeginRun()
    {
        // Run 
        RunData.Initialize();
        RunData.SetBaseSeed("[test seed]");
        // Generate Map
        RoomTreeFactory factory = new RoomTreeFactory();
        RunData.SetMap(factory.Create());
    }

    /*
     * Future-proofing: Rather than having a single seed, roll with the Mersenne Twister
     * from Songbird and store instanced RNG mechanics.
     * Seed will be modified in pattern of [base_seed].[component], e.g. "1234.map".
     * The purpose of this is to remove any impact on the RNG of one system on another component,
     * so changing the number of nodes created by the map won't influence enemy behavior.
     */
    public static void GetRandomUtilForComponent(string component)
    {
        throw new NotImplementedException("Custom RNG not implemented");
    }

    /*
     * Navigate the RunData structure to get the information associated with the current room
     */
    public static RoomNode GetSchematicsForCurrentRoom()
    {
        RequireExistingRun();
        // TODO This returns the first element of the map, rather than a specific index. A cursor should be used to track id and traversal
        return RunData.GetMap().Data;
        //throw new NotImplementedException("Room Schematics not implemented");
    }

    public static void GetDestinationCandidatesForCurrentRoom()
    {
        RequireExistingRun();
    }

    private static void MillRandomComponent(string component, int iterations)
    {
        throw new NotImplementedException("Random Milling not implemented");
    }

    public static void Load()
    {
        /*
         * I haven't managed long-term storage for random components before, so this might be
         * over-engineered. Main concept is to have identical behavior between a continuous game
         * and one that has been saved/restored multiple times.
         * - Store base seed
         * - Store hash of base seed for validity check
         * - Store each RNG component, as well as the number of times it's been invoked
         * - Before continuing, mill each component to discard the "duplicate" values. 
         */
        throw new NotImplementedException("Load not implemented");
    }

    public static void Save()
    {
        RequireExistingRun();
        throw new NotImplementedException("Save not implemented");
    }
}
