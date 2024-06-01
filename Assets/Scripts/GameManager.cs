using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Player player;
    [SerializeField] private ParticleSystem explosionEffect;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text livesText;

    private int score;
    private int lives;

    public int Score => score;
    public int Lives => lives;

    private void Awake()
    {
        if (Instance != null) 
        {
            DestroyImmediate(gameObject);
        } 
        else 
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    /// <summary>
    /// Start new game.
    /// </summary>
    private void Start()
    {
        NewGame();
    }

    /// <summary>
    /// Restart game if player presses return (enter) and has no lives left.
    /// </summary>
    private void Update()
    {
        if (lives <= 0 && Input.GetKeyDown(KeyCode.Return)) 
        {
            NewGame();
        }
    }

    /// <summary>
    /// Start a new game.
    /// </summary>
    private void NewGame()
    {
        // Destroy all asteroids.
        Asteroid[] asteroids = FindObjectsOfType<Asteroid>();

        for (int i = 0; i < asteroids.Length; i++) 
        {
            Destroy(asteroids[i].gameObject);
        }

        // Hide game over UI and reset score/lives.
        gameOverUI.SetActive(false);
        SetScore(0);
        SetLives(3);
        Respawn();
    }

    /// <summary>
    /// Update score/UI.
    /// </summary>
    /// <param name="score">Current score.</param>
    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString();
    }

    /// <summary>
    /// Update lives/UI.
    /// </summary>
    /// <param name="lives">Current lives.</param>
    private void SetLives(int lives)
    {
        this.lives = lives;
        livesText.text = lives.ToString();
    }

    /// <summary>
    /// Respawn player.
    /// </summary>
    private void Respawn()
    {
        player.transform.position = Vector3.zero;
        player.gameObject.SetActive(true);
    }

    /// <summary>
    /// Asteroid destruction.
    /// </summary>
    /// <param name="asteroid">Asteroid to be destroyed.</param>
    public void OnAsteroidDestroyed(Asteroid asteroid)
    {
        explosionEffect.transform.position = asteroid.transform.position;
        explosionEffect.Play();

        // Score is based on asteroid size.
        if (asteroid.size < 0.7f) 
        {
            SetScore(score + 100);
        } 
        else if (asteroid.size < 1.4f) 
        {
            SetScore(score + 50);
        } 
        else 
        {
            SetScore(score + 25);
        }
    }

    /// <summary>
    /// Player death.
    /// </summary>
    /// <param name="player">Player instance.</param>
    public void OnPlayerDeath(Player player)
    {
        player.gameObject.SetActive(false);

        explosionEffect.transform.position = player.transform.position;
        explosionEffect.Play();

        SetLives(lives - 1);

        // Check if game is over.
        if (lives <= 0) 
        {
            gameOverUI.SetActive(true);
        } 
        else 
        {
            Invoke(nameof(Respawn), player.respawnDelay);
        }
    }
}