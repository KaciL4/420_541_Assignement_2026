using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public enum PickupType { StandardScore, SpeedBoost, ScoreBoost, JumpBoost }
    
    [Header("Pickup Settings")]
    [SerializeField] private PickupType type;
    
    // Kept your exact variable name and default value from ScorePickup
    [SerializeField] private int points = 50; 
    [SerializeField] private AudioClip pickupSound;

    private void OnTriggerEnter(Collider other)
    {
        // Only react to the player, ignore everything else
        if (other.CompareTag("Player"))
        {
            TriggerPickupSound();

            // Handle Standard Score Pickup (Your exact logic)
            if (type == PickupType.StandardScore)
            {
                if (ScoreManager.Instance != null)
                {
                    ScoreManager.Instance.AddScore(points);
                }
                
                Destroy(gameObject);
                return; // Stop execution here for standard scores
            }

            // Handle Boost Power-Ups
            PlayerPowerUps powerUps = other.GetComponentInParent<PlayerPowerUps>();
            if (powerUps != null)
            {
                switch (type)
                {
                    case PickupType.SpeedBoost:
                        powerUps.ActivateSpeedBoost(5f);
                        break;
                    case PickupType.ScoreBoost:
                        powerUps.ActivateScoreBoost(10f);
                        break;
                    case PickupType.JumpBoost:
                        powerUps.ActivateJumpBoost(30f);
                        break;
                }
                
                //Remove the power-up from the scene after activation
                Destroy(gameObject);
            }
        }
    }
    private void TriggerPickupSound()
    {
        //play if a sound clip was assigned in the Inspector
        if (pickupSound != null)
        {
            //Plays the sound at the pickup's exact 3D position, then auto-destroys the audio runner
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
        }
    }
}
