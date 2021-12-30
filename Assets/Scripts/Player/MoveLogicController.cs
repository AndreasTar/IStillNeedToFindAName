using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLogicController : MonoBehaviour
{
	
	[SerializeField] private float jumpForce = 400f;							// Amount of force added when the player jumps.
	[Range(0, 1)][SerializeField] private float speedWhenCrouched = .5f;		// Percentage of maxSpeed applied when the player crouches.
	[Range(0, 1)][SerializeField] private float movementSmoothing = .05f;       // How much to smooth out the movement. Higher values = more time to reach desired speed.
	[Range(0, 50)] [SerializeField] private float movementSpeedMultiplier = 10f;  // How much to multiply movement speed. This basically controlls movement speed.
	[Space]

	[SerializeField] private bool canMoveOnAir = false;		// Can the player move while on the air?
	[SerializeField] private bool canJumpOnAir = false;		// Can the player jump while on the air?
	[Space]

	[SerializeField] private LayerMask whatIsGround;			 // Ground Layer mask, what to consider as touchable ground. 
	[SerializeField] private Transform groundCheck;			 	 // A position for the programm to check around and determine if the player is grounded.
	[SerializeField] private Transform ceilingCheck;			 // A position for the programm to check around and determine if theres a ceiling nearby.
	[SerializeField] private Collider2D disableColliderOnCrouch; // Which collider to disable when the player crouches

	const float groundCheckRadius = .06f;	// Radius around groundCheck to check for ground.
	private bool isGrounded;				// Is the player grounded or not?
	const float ceilingCheckRadius = .06f;   // Radius around ceilingCheck to check for ceilings.

	private Rigidbody2D playerRB2D;		// The rigidbody of the player.
	private bool isFacingRight = true;  // Is the player facing right? 
	private Vector2 velocity = Vector2.zero;

	// `! This is for event handling, right now it has no use. Check CharContr.cs script for details.
	private bool wasCrouching = false;

    private void Awake()
    {
		playerRB2D = GetComponent<Rigidbody2D>(); // Fetches the Rigidbody2D component of the player.
    }

	// If a circlecast around the groundCheck.position hits anything designated as ground, the player is grounded.
    private void FixedUpdate()
    {
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius,whatIsGround);
    }

	public void Move(float move, bool crouch, bool jump)
    {
        // If crouching, check to see if the player can stand up
        if (crouch)
        {
            // If theres a ceiling above preventing the player from standing up, keep them crouched
            if (Physics2D.OverlapCircle(ceilingCheck.position, ceilingCheckRadius, whatIsGround))
            {
				crouch = true;
            }
        }

		// Control the player if grounded or canMoveOnAir is turned on
		if (isGrounded || canMoveOnAir)
        {
            // If crouching...
            if (crouch)
            {
				// ...was crouched last update?
                if (!wasCrouching)
                {
					wasCrouching = true;
                }

				// ...reduce speed when crouching
				move *= speedWhenCrouched;

				// ...and disable the assigned collider when crouching
				if (disableColliderOnCrouch != null)
                {
					disableColliderOnCrouch.enabled = false;
                }
            }
            else // If not crouching...
            {
				// ...enable the collider again
				if(disableColliderOnCrouch != null)
                {
					disableColliderOnCrouch.enabled = true;
                }

				// ...set it that he wasnt crouched last update
                if (wasCrouching)
                {
					wasCrouching = false;
                }
            }

            // Move the player by finding the targetVelocity...
            Vector2 targetVelocity = new Vector2(move * movementSpeedMultiplier, playerRB2D.velocity.y);
            // ...and smooth it out and apply it to the character
            playerRB2D.velocity = Vector2.SmoothDamp(playerRB2D.velocity, targetVelocity, ref velocity, movementSmoothing);

            // If the input is to the right and player is currently facing left...
            if (move>0 && !isFacingRight)
            {
				// ...Flip the player sprite
				Flip();
            }
			// Otherwise if the input is to the left and the player is currently facing right...
			else if (move < 0 && isFacingRight)
            {
				// ...Flip the player sprite
				Flip();
            }
        }

        // If the player should jump...
        if ((isGrounded || canJumpOnAir) && jump)
        {
			// ...add a vertical force to the player
			isGrounded = false;
			playerRB2D.AddForce(new Vector2(0f, jumpForce),ForceMode2D.Force);
        }
    }

    // Flips which way the character sprite is facing
    private void Flip()
	{
		// Switch the way the player is labelled as facing.
		isFacingRight = !isFacingRight;

		// Multiply the player's x local scale by -1.
		Vector2 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
