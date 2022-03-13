using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomUI : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    /*
     * Simple structure for now; scope and structure will surely increase
     */
    [SerializeField] private GameObject currentLevelText;
    [SerializeField] private GameObject maxLevelText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SetLevelDisplay");
    }

    IEnumerator SetLevelDisplay()
    {
        yield return new WaitUntil(() => Initialization.currentStage == InitializationStage.Done);
        int currentDepth = RunDataManager.GetCurrentDepth() + 1;
        int maxDepth = RunDataManager.GetMaxDepth();
        currentLevelText.GetComponent<Text>().text = String.Format("{0}", currentDepth);
        maxLevelText.GetComponent<Text>().text = String.Format("{0}", maxDepth);
        canvas.SetActive(true);
        // Maybe todo - on infinite runs, use ∞
    }
}
