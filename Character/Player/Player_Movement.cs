// Robert Laidley
// September 23rd, 2024

using UnityEngine;

/// <summary>
/// A script for managing the movement of the player
/// </summary>
public class Player_Movement : MonoBehaviour
{
	/// <summary>
	/// The key for jumping
	/// </summary>
	public KeyCode Key_Jump;

	/// <summary>
	/// The key for sprinting
	/// </summary>
	public KeyCode Key_Sprint;

	/// <summary>
	/// Y-Axis Number of Jumps
	/// </summary>
	public Stat Jumps;

	/// <summary>
	/// X-Axis Movement
	/// Long-term
	/// </summary>
	public float BaseSpeed;

	/// <summary>
	/// X-Axis Movement
	/// Short-term
	/// </summary>
	public float Speed;

	/// <summary>
	/// The multiplier for movement speed when sprinting
	/// </summary>
	public float SprintMod;

	/// <summary>
	/// The animation speed multiplier
	/// </summary>
	public float AnimSpeed;

	/// <summary>
	/// Y-Axis Speed
	/// Long-term
	/// </summary>
	public float BaseJumpForce;

	/// <summary>
	/// X-Axis Movement
	/// Short-term
	/// </summary>
	public float JumpForce;

	/// <summary>
	/// Rigidbody object for collision and movement
	/// </summary>
	private Rigidbody2D PlayerRigidbody;

	/// <summary>
	/// A reference to the sprite renderer for image direction
	/// </summary>
	private SpriteRenderer PlayerSpriteRenderer;

	/// <summary>
	/// A reference to the animator to handle animations
	/// </summary>
	private Animator PlayerAnimator;

	/// <summary>
	/// A reference to the stats of the object
	/// </summary>
	private Stats PlayerStats;

	/// <summary>
	/// X-Axis Movement Modifier
	/// </summary>
	public float MoveInput;

	/// <summary>
	/// Y-Axis Movement Check
	/// </summary>
	public bool JumpPressed;

	/// <summary>
	/// Sprint bool check
	/// </summary>
	private bool SprintPressed;

	/// <summary>
	/// Y-Axis Touching Grass Check
	/// </summary>
	public bool Grounded;


	/// <summary>
	/// Once at the very beginning
	/// </summary>
	private void Awake()
	{
		// Physics Component Init
		PlayerRigidbody = GetComponent<Rigidbody2D>();
		PlayerSpriteRenderer = GetComponent<SpriteRenderer>();
		PlayerAnimator = GetComponent<Animator>();
		PlayerStats = GetComponent<Stats>();
	}

	/// <summary>
	/// Once at the beginning
	/// </summary>
	void Start()
	{
		// Y-Axis Number of Jumps
		Jumps.Max = Jumps.BaseMax;

		// X-Axis Speed Init
		Speed = BaseSpeed;

		// Y-Axis Speed Init
		JumpForce = BaseJumpForce;
	}

	/// <summary>
	/// Oncer per frame
	/// </summary>
	void Update()
	{
		if (Time.timeScale > 0f && !PlayerStats.Dead)
		{
			// X-Axis Modifier Check
			MoveInput = Input.GetAxis("Horizontal");

			// Y-Axis Held Check
			JumpPressed = Input.GetKeyDown(Key_Jump);

			// Shift Held Check
			SprintPressed = Input.GetKey(Key_Sprint);
		}

		// Handling sprinting
		if (SprintPressed)
		{
			Speed = BaseSpeed * SprintMod;
			PlayerAnimator.speed = AnimSpeed * SprintMod;
		} else
		{
			Speed = BaseSpeed;
			PlayerAnimator.speed = AnimSpeed;
		}

		// You cannot move if you are dead (WISDOM(DUH(LOL)))
		if (PlayerStats.Dead)
		{
			MoveInput = 0;

			JumpPressed = false;
		}

		// X-Axis Direction
		if (MoveInput > 0)
		{
			PlayerSpriteRenderer.flipX = false;
			PlayerAnimator.SetBool("Moving", true);
		}
		else if (MoveInput < 0)
		{
			PlayerSpriteRenderer.flipX = true;
			PlayerAnimator.SetBool("Moving", true);
		}
		else
		{
			PlayerAnimator.SetBool("Moving", false);
		}

		// X-Axis Movement Handler
		PlayerRigidbody.velocity = new Vector2(MoveInput * Speed, PlayerRigidbody.velocity.y);

		Jumps.Value = (Grounded) ? 0 : Jumps.Value;

		// Y-Axis Movement Handler
		if (JumpPressed && ((Jumps.Value < Jumps.Max - 1) || (Grounded && Jumps.Max > 0)))
		{
			PlayerRigidbody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
			Jumps.Value++;
		}

		// This should all be pulled out into methods,
		//		but there be little time for that now.
	}

	/// <summary>
	/// Once per fixed framerate frame
	/// </summary>
	private void FixedUpdate()
	{
		// Move the player
		if (MoveInput != 0)
		{
			PlayerRigidbody.velocity = new Vector2(MoveInput * Speed, PlayerRigidbody.velocity.y);
		}
		else
		{
			PlayerRigidbody.velocity = new Vector2(0,PlayerRigidbody.velocity.y);
		}
	}

	/// <summary>
	/// Upon hitting the ground
	/// </summary>
	/// <param name="collision"></param>
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Ground"))
		{
			Grounded = true;
		}
	}

	/// <summary>
	/// Upon leaving the grounf
	/// </summary>
	/// <param name="collision"></param>
	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Ground"))
		{
			Grounded = false;
		}
	}
}
