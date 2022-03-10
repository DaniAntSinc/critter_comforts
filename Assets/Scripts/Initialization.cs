using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Initialization : MonoBehaviour
{
    public static InitializationStage currentStage;
    public GameObject portalPrefab;

    private void Awake()
    {
        currentStage = InitializationStage.Initializing;
        StartCoroutine("BuildRoom");
    }

    IEnumerator BuildRoom()
    {
        yield return new WaitUntil(() => SceneManager.GetActiveScene().buildIndex == (int)SceneIndexes.ROOM_INSTANCE);
        LoadIsland();
        yield return null;
        LoadPortals();
        yield return null;
        LoadCreatures();
        yield return null;
        LoadInteractions();
        yield return null;
        Finish();
    }

    private void LoadIsland()
    {
        Initialization.currentStage = InitializationStage.Island;
        // Get proper biome object
    }

    private void LoadPortals()
    {
        Initialization.currentStage = InitializationStage.Portals;
        Vector3 basePosition = new Vector3(-6.92f, 2.13f, -8f);
        Quaternion baseRotation = Quaternion.Euler(0f, 90f, 0f);
        string[] destination = new string[] { "swamp", "desert", "volcano", "forest", "mystery" };

        List<RoomNode> destinations = RunDataManager.GetDestinationCandidatesForCurrentRoom();
        foreach (RoomNode node in destinations)
        {
            GameObject instance = Instantiate(portalPrefab, basePosition, baseRotation);
            instance.GetComponent<PortalStatus>().SetDestination(node.Biome);
            basePosition.z += 9;
        }
        // Get current node's portals from RunDataManager
        // Instantiate a prefab with location and destination data for each
    }

    private void LoadCreatures()
    {
        Initialization.currentStage = InitializationStage.Creatures;
        // For each encounter, get animals and instantiate the ambassador
    }

    private void LoadInteractions()
    {
        Initialization.currentStage = InitializationStage.Interactables;
        // Get non-fight interactions and drop a notice here.
    }

    private void Finish()
    {
        // Set stage to done at the very end
        Initialization.currentStage = InitializationStage.Done;
    }
}
