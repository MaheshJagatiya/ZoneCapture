using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private TrailController trailController;
    [SerializeField] int lives = 3;
 //   [SerializeField] int GameCompletePercentage = 80;
    private float progress;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        GameEvents.OnPlayerDied += OnPlayerDied;
        GameEvents.OnAreaCaptured += OnAreaCaptured;
    }

    private void OnDisable()
    {
        GameEvents.OnPlayerDied -= OnPlayerDied;
        GameEvents.OnAreaCaptured -= OnAreaCaptured;
    }

    void Start()
    {       
        GameEvents.OnLivesChanged?.Invoke(lives);
        GameEvents.OnAreaCaptured?.Invoke(0f);
    }

    private void OnPlayerDied()
    {
        if (lives <= 0) return;
        lives--;
        GameEvents.OnLivesChanged?.Invoke(lives);
        trailController?.ResetTrail();

        if (lives <= 0)
        {         
            Debug.Log("Game Over");
            GameEvents.OnGameOver?.Invoke();
            
        }
    }

    private void OnAreaCaptured(float percent)
    {
        progress = percent;

        if (progress >= RoundManager.Instance.GetTargetPercentage())
        {           
            Debug.Log("Level Complete");
            GameEvents.OnLevelComplete?.Invoke();
            RoundManager.Instance.NextRound();

        }
    }
   
}
