using UnityEngine;

// Keeps track of the player's score.
// It's a singleton: there is only ever one, reachable from any script
// through ScoreManager.Instance.
public class ScoreManager : MonoBehaviour
{
    // The single shared instance that any script can reach.
    // "static" means it belongs to the class, not to one object.
    public static ScoreManager Instance { get; private set; }

    // Anyone can read the score, only ScoreManager can change it
    public int Score { get; private set; }

    private bool isDoubleScoreActive = false; //for purple pickup double score boost

    // Awake runs before any Start(), so Instance is ready before coins need it
    private void Awake()
    {
        // If a ScoreManager already exists, remove this extra component
        // (only the script, not the whole Player)
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        // keep score alive between levels
        DontDestroyOnLoad(gameObject);
    }


    // Called by coins when they're collected
    public void AddScore(int amount)
    {
        // Double  points if  Purple Pickup boost is active 
        int finalAmount = isDoubleScoreActive ? amount * 2 : amount;

        Score += finalAmount;


        // Print the new total to the Console window
        Debug.Log("Score: " + Score);
    }

    // method for PlayerPowerUps script to turn the boost on/off 
    public void SetDoubleScore(bool isActive)
    {
        isDoubleScoreActive = isActive;
        Debug.Log("Double Score Status Changed: " + isActive);
    }

        // "static" means this value belongs to the ScoreManager CLASS,
    // not to one ScoreManager object. Objects are destroyed when a
    // new scene loads, but static values stay in memory.
    public static int FinalScore { get; private set; }
 
    // Called just before we leave the level
    public void SaveFinalScore()
    {
        FinalScore = Score;
    }

}