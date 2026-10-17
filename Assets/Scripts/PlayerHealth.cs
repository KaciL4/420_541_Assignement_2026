using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance;

    [Header("Health Settings")]
    public int maxHealth = 3;
    private int currentHealth;
    [Header("Scene Settings")]

    public int CurrentHealth => currentHealth;

    [Tooltip("Loaded Scene to restart the Game")]
    public Object firstLevelScene;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnCollisionEnter(Collision collision)
    {
        CheckTrap(collision.gameObject);
    }

    // Use this if your traps/player use "Is Trigger" colliders
    private void OnTriggerEnter(Collider other)
    {
        CheckTrap(other.gameObject);
    }

    void CheckTrap(GameObject hitObject)
    {
        // Check if the object collided with redTrap
        if (hitObject.CompareTag("RedTrap"))
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log("Player Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            RestartLevel();
        }
    }
    void RestartLevel()
    {
        SceneManager.LoadScene(firstLevelScene.name);
    }
}
