using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public GameObject replayLabel;
    public GameObject quitLabel;
    public Text lifeText;
    public Text scoreText;
    public AudioSource audioSourceBGM;
    public AudioSource audioSourceSE;
    public AudioClip mainBGM;
    public AudioClip gameoverBGM;
    public AudioClip hitSE;
    public int initialLife;
    public float spawnClockSec;
    public float accelerateClockSec;
    public float delayClockSec;
    public float accelerateIncidencePer;
    public float delayIncidencePer;

    private GameObject player;
    private int life;
    private int score;
    private float elapsedTime;
    private bool isPlaying;

    private Vector3 screenMinPos;
    private Vector3 screenMaxPos;

    void Awake()
    {
        instance = this;
    }

    void Start() {
        screenMinPos = Camera.main.ScreenToWorldPoint(Vector3.zero);
        screenMaxPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0.0f));

        StartGame();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
            return;
        }

        if (!isPlaying)
        {
            return;
        }

        elapsedTime += Time.deltaTime;

        score = (int)(elapsedTime * 100);
        scoreText.text = score.ToString();
    }

    public void OnHitEvent() {
        if (!isPlaying)
        {
            return;
        }

        life--;
        lifeText.text = life.ToString();

        audioSourceSE.PlayOneShot(hitSE);

        if (life <= 0)
        {
            StopGame();
            return;
        }
    }

    private void StartGame()
    {
        player      = Instantiate(playerPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
        life        = initialLife;
        score       = 0;
        elapsedTime = 0;
        isPlaying   = true;

        InvokeRepeating("SpawnEnemy",        spawnClockSec,      spawnClockSec);
        InvokeRepeating("AccelerateEnemies", accelerateClockSec, accelerateClockSec);
        InvokeRepeating("DelayEnemies",      delayClockSec,      delayClockSec);

        audioSourceBGM.clip = mainBGM;
        audioSourceBGM.Play();
    }

    private void StopGame()
    {
        isPlaying = false;

        PlayerController playerController = player.GetComponent<PlayerController>();
        playerController.RemoveControl();

        replayLabel.SetActive(true);
        quitLabel.SetActive(true);

        CancelInvoke();

        audioSourceBGM.clip = gameoverBGM;
        audioSourceBGM.Play();
    }

    private void RestartGame()
    {
        SceneManager.LoadScene("Main");
    }

    private void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_STANDALONE
        UnityEngine.Application.Quit();
        #endif
    }

    private void SpawnEnemy() {
        float x = Random.Range(screenMinPos.x, screenMaxPos.x);
        float y = Random.Range(screenMinPos.y, screenMaxPos.y);
        switch (Random.Range(0, 4)) {
            case 0: x = screenMinPos.x; break;
            case 1: x = screenMaxPos.x; break;
            case 2: y = screenMinPos.y; break;
            case 3: y = screenMaxPos.y; break;
        }

        GameObject enemy = Instantiate(enemyPrefab, new Vector3(x, y, 0f), Quaternion.identity);
        EnemyBehaviour enemyBehaviour = enemy.GetComponent<EnemyBehaviour>();
        enemyBehaviour.target = player;
    }

    private void AccelerateEnemies() {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (Random.Range(1, 101) <= accelerateIncidencePer)
            {
                EnemyBehaviour enemyBehaviour = enemy.GetComponent<EnemyBehaviour>();
                enemyBehaviour.Accelerate();
            }
        }
    }

    private void DelayEnemies() {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (Random.Range(1, 101) <= delayIncidencePer)
            {
                EnemyBehaviour enemyBehaviour = enemy.GetComponent<EnemyBehaviour>();
                enemyBehaviour.Delay();
            }
        }
    }
}
