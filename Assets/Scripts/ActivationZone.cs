using UnityEngine;

public class ActivationZone : MonoBehaviour
{
    private bool playerInZone = false;
    public GameObject targetSpawner;
    private TargetSpawner spawnerScript;

    void Start()
    {
        targetSpawner.SetActive(false);
        spawnerScript = targetSpawner.GetComponent<TargetSpawner>();
        Debug.Log("ActivationZone Initialized!");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = true;
            Debug.Log("Player entered activation zone! Press F to start, R to reset.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
            Debug.Log("Player left activation zone.");
        }
    }

    void Update()
    {
        if (playerInZone && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("▶ F Pressed! Starting Target Spawner...");
            targetSpawner.SetActive(true);
            spawnerScript.StartGame();
        }

        if (playerInZone && Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("R Pressed! Resetting the game...");
            spawnerScript.ResetGame();
        }
    }
}
