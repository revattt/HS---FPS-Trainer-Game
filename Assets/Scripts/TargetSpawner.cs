using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class TargetSpawner : MonoBehaviour
{
    public GameObject targetPrefab;
    public Vector3 spawnArea = new Vector3(10, 5, 10);
    public float spawnInterval = 2f;
    public float targetLifetime = 3f;
    public float spawnDuration = 20f;

    public TextMeshProUGUI timerText;
    public Shooting shootingScript;

    private bool stopSpawning = false;
    private float timeLeft;
    private bool firstSpawnDone = false;

    public void StartGame()
    {
        stopSpawning = false;
        timeLeft = spawnDuration;
        firstSpawnDone = false; // Reset this flag

        if (shootingScript != null)
        {
            shootingScript.ResetScores();
        }

        UpdateTimerUI();
        StartCoroutine(SpawnTimer());


        Invoke(nameof(SpawnTarget), 0f);
        InvokeRepeating(nameof(SpawnTarget), spawnInterval, spawnInterval);
    }

    void SpawnTarget()
    {
        if (stopSpawning) return;

        if (!firstSpawnDone)
        {
            firstSpawnDone = true;
        }
        else
        {
            Vector3 randomPos = new Vector3(
                Random.Range(-spawnArea.x / 2, spawnArea.x / 2),
                1f,
                Random.Range(-spawnArea.z / 2, spawnArea.z / 2)
            );

            randomPos += transform.position;

            GameObject newTarget = Instantiate(targetPrefab, randomPos, Quaternion.identity);
            Destroy(newTarget, targetLifetime);
        }
    }

    IEnumerator SpawnTimer()
    {
        while (timeLeft > 0)
        {
            UpdateTimerUI();
            yield return new WaitForSeconds(1f);
            timeLeft--;
        }

        stopSpawning = true;
        timerText.text = "";
        Debug.Log("Time's up!");
    }

    void UpdateTimerUI()
    {
        timerText.text = "Time Left: " + Mathf.Ceil(timeLeft).ToString();
    }

    public void ResetGame()
    {
        Debug.Log("Resetting Target Spawner...");

        CancelInvoke(nameof(SpawnTarget));
        StopAllCoroutines();

        StartGame();
    }
}
