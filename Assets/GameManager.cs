using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance{get; private set;}
    
    public PlayerManager playerManager;
    //public PactManager pactManager;
    //public ExplorationManager explorationManager;
    
    //public Player player;

    private int actualScore = 0;

    public void SaveScore()
    {
        if (PlayerPrefs.GetInt("Highest Score") > actualScore)
            return;
            
        PlayerPrefs.SetInt("Highest Score", actualScore);
    }

    public int GetHighestScore()
    {
        return PlayerPrefs.GetInt("Highest Score");
    }

    public int GetScore()
    {
        return actualScore;
    }

    public void AddToScore(in int score)
    {
        actualScore += score;
    }
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        
        playerManager = FindAnyObjectByType<PlayerManager>();
        // pactManager = FindAnyObjectByType<PactManager>();
        // explorationManager = FindAnyObjectByType<ExplorationManager>();
        // player = FindAnyObjectByType<Player>();
        CheckAttributes();
    }

    private void CheckAttributes()
    {
        if (playerManager == null)
            Debug.LogError("PlayerManager doesn't exist");        
        // if (pactManager == null)
        //     Debug.LogError("PactManager doesn't exist");        
        // if (explorationManager == null)
        //     Debug.LogError("ExplorationManager doesn't exist");        
        // if (player == null)
        //     Debug.LogError("Player doesn't exist");
    }
}
