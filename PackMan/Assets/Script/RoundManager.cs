using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public static RoundManager Instance;

    [SerializeField] int currentRound = 1;

    [SerializeField] int maxRounds = 3;

    [SerializeField] int baseTargetAreaPercntage = 80;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public int GetTargetPercentage()
    {
        return baseTargetAreaPercntage + (currentRound - 1) * 3;
    }

    public int GetEnemyCount()
    {
        return  currentRound-1; // 1,2,3
    }

    public void NextRound()
    {
        currentRound = (currentRound < maxRounds) ? currentRound + 1 : 1;
    }

   
}
