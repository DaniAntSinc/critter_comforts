using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] GameObject statusText;
    [SerializeField] GameObject completedText;
    // Loading percent stuff
    // Start is called before the first frame update

    private void UpdateLabel(string currentInstruction)
    {
        // Ideas to explore: List of items, current/completed, glowing active text?
        string completedInstruction = statusText.GetComponent<Text>().text;
        completedText.GetComponent<Text>().text += "\n" + completedInstruction;
        statusText.GetComponent<Text>().text = currentInstruction;
    }

    void Awake()
    {
        StartCoroutine("LoadAsync");
    }

    private void InitializeRun()
    {
        // TODO Here's where the configuration would need to be applied
        RunDataManager.BeginRun();
    }

    public IEnumerator LoadAsync()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        UpdateLabel("Scrying World...");
        if (!RunDataManager.HasExistingRun())
        {
            InitializeRun();
        }

        UpdateLabel("Divining Routes...");
        RunDataManager.CheckExtensionNeeded();

        UpdateLabel("Navigating Aether");
        yield return SceneManager.LoadSceneAsync((int)SceneIndexes.ROOM_INSTANCE, LoadSceneMode.Additive);
        yield return new WaitForSeconds(0.6f);

        SceneManager.SetActiveScene(SceneManager.GetSceneAt((int)SceneIndexes.ROOM_INSTANCE));
        UpdateLabel("Conjuring Fragments...");
        while (Initialization.currentStage == InitializationStage.Island)
        {
            yield return null;
        }

        UpdateLabel("Enchanting Portals...");
        while (Initialization.currentStage == InitializationStage.Portals)
        {
            yield return null;
        }

        UpdateLabel("Summoning Creatures...");
        while (Initialization.currentStage == InitializationStage.Creatures)
        {
            yield return null;
        }

        UpdateLabel("Transmuting Events...");
        while (Initialization.currentStage == InitializationStage.Interactables)
        {
            yield return null;
        }

        UpdateLabel("Dispelling Clutter...");
        while (Initialization.currentStage != InitializationStage.Done)
        {
            yield return null;
        }

        SceneManager.UnloadSceneAsync(currentScene);
        // Maybe transition scene to a better effect here?
    }
}
