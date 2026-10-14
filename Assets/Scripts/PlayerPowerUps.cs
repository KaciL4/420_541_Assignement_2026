using System.Collections;
using UnityEngine;

public class PlayerPowerUps : MonoBehaviour
{
    private CharacterMovement movement;


    [Header("Active States (Debug)")]
    public bool hasSpeedBoost = false;
    public bool hasScoreBoost = false;
    public bool hasJumpBoost = false;

    private void Start()
    {
        movement = GetComponent<CharacterMovement>();
        
    }

    // SPEED BOOST - allow player to click shift to speed up (Orange)
    public void ActivateSpeedBoost(float duration)
    {
        StartCoroutine(SpeedBoostRoutine(duration));
    }

    private IEnumerator SpeedBoostRoutine(float duration)
    {
        hasSpeedBoost = true;
        yield return new WaitForSeconds(duration);
        hasSpeedBoost = false;
    }

    // SCORE BOOST - double the score pickup (Purple)
    public void ActivateScoreBoost(float duration)
    {
        StartCoroutine(ScoreBoostRoutine(duration));
    }

    private IEnumerator ScoreBoostRoutine(float duration)
    {
        hasScoreBoost = true;
        if (ScoreManager.Instance != null) 
        {
            ScoreManager.Instance.SetDoubleScore(true);
        }
        
        yield return new WaitForSeconds(duration);
        
        hasScoreBoost = false;
        if (ScoreManager.Instance != null) 
        {
            ScoreManager.Instance.SetDoubleScore(false);
        }
    }

    //JUMP BOOST - allow player to double jump (Blue)
    public void ActivateJumpBoost(float duration)
    {
        StartCoroutine(JumpBoostRoutine(duration));
    }

    private IEnumerator JumpBoostRoutine(float duration)
    {
        hasJumpBoost = true;
        yield return new WaitForSeconds(duration);
        hasJumpBoost = false;
    }
}