using UnityEngine;
using TMPro;
using System.Collections;

public class TileSpawner : MonoBehaviour
{
    public GameObject tilePrefab;
    public Transform spawnArea;
    public float gameDuration = 20f;
    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;

    private int score = 0;
    private bool gameActive = false;
    private float timeLeft;
    private bool playerInside = false;

    void Start()
    {
        countdownText.gameObject.SetActive(false);
        scoreText.text = "Score: 0";
        timerText.text = "Enter Zone & Press F";
    }

    void Update()
    {
        if (playerInside && !gameActive && Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(StartGame());
        }
        if (gameActive && Input.GetKeyDown(KeyCode.R))
        {
            ResetGame();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            timerText.text = "Press F to Start";
            Debug.Log("Entered");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            timerText.text = "Enter Zone & Press F";
            Debug.Log("Exit");
        }
    }

    IEnumerator StartGame()
    {
        countdownText.gameObject.SetActive(true);
        timerText.gameObject.SetActive(false);

        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        countdownText.text = "GO!";
        yield return new WaitForSeconds(0.5f);
        countdownText.gameObject.SetActive(false);

        timeLeft = gameDuration;
        gameActive = true;
        score = 0;
        scoreText.text = "Score: 0";
        SpawnTile();
        StartCoroutine(GameTimer());
    }

    IEnumerator GameTimer()
    {
        while (timeLeft > 0)
        {
            timerText.text = "Time: " + Mathf.Ceil(timeLeft).ToString();
            yield return new WaitForSeconds(1f);
            timeLeft--;
        }
        EndGame();
    }

    void EndGame()
    {
        gameActive = false;
        timerText.text = "Game Over! Final Score: " + score;
    }

    void ResetGame()
    {
        StopAllCoroutines();
        gameActive = false;
        foreach (GameObject tile in GameObject.FindGameObjectsWithTag("Tile"))
        {
            Destroy(tile);
        }
        timerText.text = "Enter Zone & Press F";
        scoreText.text = "Score: 0";
    }

    public void SpawnTile()
    {
        if (!gameActive) return;

        Vector3 randomPos = new Vector3(
            Random.Range(-spawnArea.localScale.x / 2, spawnArea.localScale.x / 2),
            Random.Range(-spawnArea.localScale.y / 2, spawnArea.localScale.y / 2),
            spawnArea.position.z
        );

        GameObject newTile = Instantiate(tilePrefab, randomPos, Quaternion.identity);
        newTile.GetComponent<Tile>().spawner = this;
    }

    public void IncreaseScore()
    {
        if (!gameActive) return;
        score++;
        scoreText.text = "Score: " + score;
        SpawnTile();
    }
}
