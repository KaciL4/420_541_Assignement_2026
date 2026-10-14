using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    private Animator animator;
    private CharacterMovement movement;
    private Rigidbody rb;

    private void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<CharacterMovement>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        animator.SetFloat("CharacterSpeed", rb.linearVelocity.magnitude);

        bool grounded = movement.IsGrounded;
        animator.SetBool("IsGrounded", grounded);

        //Check if player is moving forward/backward (vertical) and holding Left Shift
        bool isMoving = Input.GetAxisRaw("Vertical") != 0;
        bool wantToRun = Input.GetKey(KeyCode.LeftShift);
        
        // Pass the boolean state directly to your Animator controller
        animator.SetBool("isRunning", isMoving && wantToRun);

        if (Input.GetButtonUp("Fire1"))
        {
            animator.SetTrigger("doRoll");
        }

        if (grounded)
        {
            animator.ResetTrigger("doDoubleJump");
        }
        else
        {
            // Trigger Flip animation if double jump succeeds
            if (Input.GetKeyDown(KeyCode.Space) && movement.TryDoubleJump())
            {
                animator.SetTrigger("doDoubleJump");
            }
        }
    }
}