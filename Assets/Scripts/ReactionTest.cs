using UnityEngine;
using TMPro;
using System.Collections;

public class ReactionTest : MonoBehaviour
{
    public Renderer screenRenderer;
    public TextMeshProUGUI reactionTimeText;
    private float greenTime;
    private bool testActive = false;
    private bool waitingForGreen = false;

    void Start()
    {
        ResetScreen();
    }

    void ResetScreen()
    {
        screenRenderer.material.color = Color.white;
        reactionTimeText.text = "Shoot to Start!";
        testActive = false;
        waitingForGreen = false;
        Debug.Log("🔄 Screen Reset - Ready to Start");
    }

    public void OnShot()
    {
        if (!testActive)
        {
            Debug.Log("Starting Reaction Test...");
            StartCoroutine(StartReactionTest());
        }
        else if (waitingForGreen)
        {
            reactionTimeText.text = "Too Soon! Try Again.";
            Debug.Log("Too Soon! Restarting...");
            StartCoroutine(DelayedReset());
        }
        else
        {
            float reactionTime = (Time.time - greenTime) * 1000f;
            Debug.Log("Reaction Time: " + reactionTime.ToString("F0") + " ms");

            if (reactionTimeText != null)
            {
                reactionTimeText.text = "Reaction Time: " + reactionTime.ToString("F0") + " ms";
            }

            StartCoroutine(DelayedReset());
        }
    }

    IEnumerator StartReactionTest()
    {
        testActive = true;
        waitingForGreen = true;
        screenRenderer.material.color = Color.red;
        reactionTimeText.text = "Wait for Green...";
        Debug.Log("Test Started - Waiting for Green...");

        float waitTime = Random.Range(2f, 4f);
        yield return new WaitForSeconds(waitTime);

        waitingForGreen = false;
        screenRenderer.material.color = Color.green;
        reactionTimeText.text = "Shoot Now!";
        greenTime = Time.time;
        Debug.Log("GREEN! Shoot Now!");
    }

    IEnumerator DelayedReset()
    {
        yield return new WaitForSeconds(2f);
        ResetScreen();
    }
}
