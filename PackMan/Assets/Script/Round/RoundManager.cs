using UnityEngine;

//RoundMnage
//Round complte then one new enemy add wihh +1 speed and coontinur work asper upgrade round
//as per round caplite then area capture also increase for complete round
//max game work with 3 round and then start with first round
public class RoundManager : MonoBehaviour
{
    public static RoundManager Instance;

    [SerializeField] int currentRound = 1;

    [SerializeField] int maxRounds = 3;

    [SerializeField] int baseTargetAreaPercntage = 80;

    [SerializeField] int levelCompleteIncreateAreaCount = 3;

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
        return baseTargetAreaPercntage + (currentRound - 1) * levelCompleteIncreateAreaCount;
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
