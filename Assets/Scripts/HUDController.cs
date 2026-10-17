using UnityEngine;
using TMPro; // TMP_Text
 
// Updates the HUD every frame from the ScoreManager and PlayerPowerUps.
public class HUDController : MonoBehaviour
{
    [Header("Score")]
    [SerializeField] private TMP_Text scoreText;
    [Header("Health")]
    [SerializeField] private TMP_Text healthText;

    [Header("Power-Up Timers")]
    [SerializeField] private TMP_Text speedBoostText;
    [SerializeField] private TMP_Text scoreBoostText;
    [SerializeField] private TMP_Text jumpBoostText;

    private PlayerPowerUps playerPowerUps;

    private void Start()
    {
        playerPowerUps = Object.FindAnyObjectByType<PlayerPowerUps>();
    }
 
    private void Update()
    {
        UpdateScore();
        UpdateHealth();
        UpdatePowerUpTimers();
    }
    
    private void UpdateScore()
    {
        // The Score property from Lab 4: anyone can read it
        scoreText.text = "Score: " + ScoreManager.Instance.Score;
    }
    private void UpdateHealth()
    {
        if (healthText != null && PlayerHealth.Instance != null)
        {
            healthText.text = "Player Life: " + PlayerHealth.Instance.CurrentHealth;
        }
    }

    private void UpdatePowerUpTimers()
    {
        if(playerPowerUps == null) return;

        // Speed Boost timer
        if(speedBoostText != null)
        {
            if(playerPowerUps.hasSpeedBoost)
                speedBoostText.text = "Speed Boost Timer: " + Mathf.Ceil(playerPowerUps.SpeedBoostTimeRemaining) + "s";
            else
                speedBoostText.text = ""; //Hide text when inactive
        }
        //Score Boost timer
        if (scoreBoostText !=null)
        {
            if(playerPowerUps.hasScoreBoost)
                scoreBoostText.text="Score Boost Timer: "+ Mathf.Ceil(playerPowerUps.ScoreBoostTimeRemaining) + "s";
            else
                scoreBoostText.text="";
        }

        //jump Boost timer
        if (jumpBoostText != null)
        {
            if(playerPowerUps.hasJumpBoost)
                jumpBoostText.text="Jump Boost Timer: "+ Mathf.Ceil(playerPowerUps.JumpBoostTimeRemaining) + "s";
            else
                jumpBoostText.text= "";
        }
    }
 
}
