using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Initialization : MonoBehaviour
{
    public static InitializationStage currentStage;
    public GameObject biomePrefab;
    public GameObject portalPrefab;
    public GameObject enemyPrefab;
    public GameObject shopPrefab;
    public GameObject winPrefab;

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
        // The biome object should have a few identifies for valid portal locations, and a linked reference(?) to enemies guarding the portals
        GameObject instance = Instantiate(biomePrefab);
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
            instance.GetComponent<PortalStatus>().SetDestination(node.Id, node.Biome);
            basePosition.z += 9;
        }
        // Get current node's portals from RunDataManager
        // Instantiate a prefab with location and destination data for each
    }

    private void LoadCreatures()
    {
        Initialization.currentStage = InitializationStage.Creatures;
        // For each encounter, get animals and instantiate the ambassador
        RoomNode room = RunDataManager.GetSchematicsForCurrentRoom();
        GameObject instance;
        foreach (RoomComponent c in room.Components)
        {
            switch (c)
            {
                case RoomComponent.Fight_Standard:
                    instance = Instantiate(enemyPrefab);
                    Debug.Log("Instanced standard encounter");
                    break;
                case RoomComponent.Fight_Miniboss:
                    instance = Instantiate(enemyPrefab);
                    instance.transform.localScale += new Vector3(1.5f, 1.5f, 1.5f);
                    Debug.Log("Instanced miniboss encounter");
                    break;
                case RoomComponent.Fight_Boss:
                    instance = Instantiate(enemyPrefab);
                    instance.transform.localScale += new Vector3(2.5f, 2.5f, 2.5f);
                    Debug.Log("Instanced boss encounter");
                    break;
                default: break; // Nothing to do here
            }
        }
    }

    private void LoadInteractions()
    {
        Initialization.currentStage = InitializationStage.Interactables;
        // Get non-fight interactions and drop a notice here.
        RoomNode room = RunDataManager.GetSchematicsForCurrentRoom();
        GameObject instance;
        foreach (RoomComponent c in room.Components)
        {
            switch (c)
            {
                case RoomComponent.Shop_Basic:
                    instance = Instantiate(shopPrefab);
                    Debug.Log("Instanced shop");
                    break;
                case RoomComponent.Bonding_Basic:
                    Debug.Log("Instanced bonding event");
                    break;
                case RoomComponent.Event_Finish:
                    Debug.Log("win");
                    instance = Instantiate(winPrefab);
                    break;
                default: break; // Nothing to do here
            }
        }
    }

    private void Finish()
    {
        // Spawn player
        // Set stage to done at the very end
        Initialization.currentStage = InitializationStage.Done;
    }
}
