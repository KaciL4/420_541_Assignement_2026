using System.Collections;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public enum PickupType { StandardScore, SpeedBoost, ScoreBoost, JumpBoost }
    
    [Header("Pickup Settings")]
    [SerializeField] private PickupType type;
    
    [SerializeField] private int points = 50; 
    [SerializeField] private AudioClip pickupSound;

    [Header("Respawn Settings")]
    [SerializeField] private float respawnDelay = 30f;

    [SerializeField] private GameObject visuals;

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerPickupSound();

            //handle Standard Score Pickup (Destroyed permanently)
            if (type == PickupType.StandardScore)
            {
                if (ScoreManager.Instance != null)
                {
                    ScoreManager.Instance.AddScore(points);
                }
                
                Destroy(gameObject);
                return; 
            }

            //handle Boost Power-Ups
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
                
              // Disappear, disable collision, and call Reappear() after 30 seconds
                if (visuals != null) visuals.SetActive(false);
                GetComponent<Collider>().enabled = false;
                Invoke(nameof(Reappear), respawnDelay);
            }
        }
    }

    private void Reappear()
    {
        if (visuals != null) visuals.SetActive(true);
        GetComponent<Collider>().enabled = true;
    }

    private void TriggerPickupSound()
    {
        // Play if a sound clip was assigned in the Inspector[cite: 1]
        if (pickupSound != null)
        {
            // Plays the sound at the pickup's exact 3D position[cite: 1]
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
        }
    }
}