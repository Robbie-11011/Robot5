// Robert Laidley
// September 22nd, 2024

using UnityEngine;

/// <summary>
/// A fireball spell
/// How is this different from the melee attack? Barely. The only difference is a rather insignificant one, so the future of attacks may be reduced to one.
/// </summary>
public class Atk_Fireball : Ability
{
	/// <summary>
	/// A reference to the rigidbody component of the fireball\
	/// For velocity
	/// </summary>
	private Rigidbody2D FireballRigidbody;

	/// <summary>
	/// A reference to the sprite renderer componenet of the fireball
	/// For image flipping
	/// </summary>
	private SpriteRenderer FireballSpriteRenderer;

	/// <summary>
	/// A reference to the target's stats
	/// For causing damage
	/// </summary>
	private Stats TargetStats;

	/// <summary>
	/// Once at the very beginning
	/// </summary>
	private void Awake()
	{
		FireballRigidbody = GetComponent<Rigidbody2D>();
		FireballSpriteRenderer = GetComponent<SpriteRenderer>();
	}

    /// <summary>
	/// Once per frame
	/// </summary>
    void Update()
    {
		UpdateCost();

		Vector2 finalVelocity = Stats.Velocity;

		if (FireballSpriteRenderer.flipX)
		{
			finalVelocity.x *= -1;
		}

		FireballRigidbody.velocity = finalVelocity;
	}

	/// <summary>
	/// The initialization of the ability
	/// Done by the parent object
	/// </summary>
	/// <param name="parent"></param>
	/// <param name="passedStats"></param>
	public override void Initialize(GameObject parent, AbilityStats passedStats)
	{
		Parent = parent;

		TargetStats = parent.GetComponent<Stats>();
		Stats = passedStats;

		// Subtracts mana from the user when cast
		TargetStats.Mana.Value -= Stats.Cost.Total;

		Destroy(gameObject, Stats.Duration);
	}

	/// <summary>
	/// Break when it hits something
	/// This could change later or simply becoming a boolean property, but this is the current iteration
	/// </summary>
	/// <param name="other"></param>
	private void OnTriggerEnter2D(Collider2D other)
	{
		// Catered toward the player
		// Will generalize

		if (gameObject.CompareTag("Attack1") && (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Ground")))
		{
			Destroy(gameObject);
		}
	}
}
