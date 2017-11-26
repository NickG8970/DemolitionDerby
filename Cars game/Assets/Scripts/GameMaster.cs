using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance; // A public reference to itself that can be accessed without a reference (because it's static)
    [HideInInspector] public int enemiesKilled; // The total number of enemies you have killed (can also be accessed without a reference)
    [HideInInspector] public int enemiesAlive;

    public GameObject gameOverPanel; // Reference to the Game Over panel.
    public GameObject missionCompletePanel; // Reference to the Mission Complete panel.
    public TextMeshProUGUI enemiesKilledText; // Reference to the Enemies Killed text.
    public GameObject explosionEffectPrefab; // Reference to the explosion prefab.
    public CameraFollow camFollow; // Reference to the CameraFollow component on the main camera.

    private GameObject player; // Reference to the player.

    bool gameFinished = false;

    public int currentMission;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Multiple GameMasters in the scene!");
            return;
        }

        instance = this;
    }

    void Start()
    {
        enemiesKilled = 0; // We must reset enemiesKilled at the start of each game because it's static so the value will carry over scenes.
        player = FindObjectOfType<Player>().gameObject;
    }

    void Update()
    {
        if (enemiesAlive == 0 && !gameFinished)
        {
            FinishGame();
            gameFinished = true;
        }
    }

    void FinishGame()
    {
        int missionReached = PlayerPrefs.GetInt("missionReached");
        if (currentMission >= missionReached)
        {
            PlayerPrefs.SetInt("missionReached", missionReached + 1);
        }

        missionCompletePanel.SetActive(true);
    }

    public void GameOver() // Can be called through the public static instance when the game is over.
    {
        StartCoroutine(GameOverAnimations());
    }

    IEnumerator GameOverAnimations() // Using a coroutine allows me to wait for specfic amounts of seconds before running code.
    {
        yield return new WaitForSeconds(camFollow.cinematicDampening * 5); // Wait until the camera has started moving.
        Vector3 playerPos = player.transform.position; // Grab the player's position.

        GameObject explosionEffect = Instantiate(explosionEffectPrefab, new Vector3(playerPos.x, playerPos.y + 2, playerPos.z), explosionEffectPrefab.transform.rotation);
        ParticleSystem particleSystem = explosionEffect.GetComponent<ParticleSystem>();
        float explosionDuration = particleSystem.main.duration;

        Time.timeScale = 0.45f; // Slow the time down to just below half speed.

        Destroy(explosionEffect, explosionDuration);

        yield return new WaitForSeconds(explosionDuration); // Wait for the explosion to finish.

        Time.timeScale = 0; // Freeze time.
        gameOverPanel.SetActive(true); // Show the Game Over panel.
        enemiesKilledText.text = enemiesKilled.ToString(); // Set the enemies killed text to the amount of enemies you've killed.
    }

    public void Retry()
    {
        Time.timeScale = 1; // Unfreeze time.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the scene by loading the scene that is active.
    }

    public void NextMission()
    {
        AudioManager.instance.StopAllSounds();
        SceneManager.LoadScene("MissionSelect");
    }

    public void Exit()
    {
        Application.Quit(); // Quit the game.
    }
}