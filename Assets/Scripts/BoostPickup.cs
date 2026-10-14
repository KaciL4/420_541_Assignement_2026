using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public enum PickupType { StandardScore, SpeedBoost, ScoreBoost, JumpBoost }
    [Header("Pickup Settings")]
    [SerializeField] private PickupType type;
    
    [Header("Values & Timing")]
    [SerializeField] private int points = 50; 
    [SerializeField] private float boostDuration = 10f; // Duration for the power-up boost
    [SerializeField] private float respawnDelay = 5f;   // Time hidden before respawning (0 or negative = destroy permanently)
    
    [Header("Audio & Effects")]
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private GameObject pickupEffect; // Particle prefab
    
    private Collider itemCollider;
    private Renderer itemRenderer;

    private void Awake()
    {
        itemCollider = GetComponent<Collider>();
        itemRenderer = GetComponent<Renderer>(); // Automatically finds MeshRenderer, SpriteRenderer, etc.
    }
    private void OnTriggerEnter(Collider other)
    {
        // Only react to the player, ignore everything else
        if (!other.CompareTag("Player")) return;

        // play sound and spawn particle effect if assigned
        if (pickupSound != null && GetComponent<AudioSource>() != null)
        {
            GetComponent<AudioSource>().PlayOneShot(pickupSound);
        }
        if (pickupEffect != null)
        {
            Instantiate(pickupEffect, transform.position, Quaternion.identity);
        }
        //Handle Standard Score Pickup
        if (type == PickupType.StandardScore)
        {
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddScore(points);
            }
            
            HandlePickupConsumption();
            return;
        }

        //handle Boost Power-Ups
        PlayerPowerUps powerUps = other.GetComponentInParent<PlayerPowerUps>();
        if (powerUps != null)
        {
            switch (type)
            {
                case PickupType.SpeedBoost:
                    powerUps.ActivateSpeedBoost(boostDuration);
                    break;
                case PickupType.ScoreBoost:
                    powerUps.ActivateScoreBoost(boostDuration);
                    break;
                case PickupType.JumpBoost:
                    powerUps.ActivateJumpBoost(boostDuration);
                    break;
            }
            
            HandlePickupConsumption();
        }
    }
    private void HandlePickupConsumption()
    {
        //if respawnDelay is set to 0 or less, destroy it permanently
        if (respawnDelay <= 0f)
        {
            Destroy(gameObject);
            return;
        }
        // disable the renderer (hides it) and disable the collider
        if (itemRenderer != null)
        {
            itemRenderer.enabled = false;
        }
        if (itemCollider != null)
        {
            itemCollider.enabled = false;
        }
        Invoke(nameof(Reappear), respawnDelay);
    }

    private void Reappear()
    {
        //Re-enable the renderer and collider
        if (itemRenderer != null)
        {
            itemRenderer.enabled = true;
        }
        if (itemCollider != null)
        {
            itemCollider.enabled = true;
        }
    }
}