// Robert Laidley
// September 22nd, 2024

using UnityEngine;

/// <summary>
/// Melee ability
/// </summary>
public class Atk_Melee : Ability
{
	/// <summary>
	/// A reference to the rigidbody of the attack
	/// </summary>
	private Rigidbody2D MeleeRigidbody;

	/// <summary>
	/// A reference to the sprite renderer component
	/// </summary>
	private SpriteRenderer MeleeSpriteRenderer;

	/// <summary>
	/// The stats of the target
	/// This if for actually dealing damage
	/// </summary>
	private Stats TargetStats;

	/// <summary>
	/// Once at the very beginning
	/// </summary>
	private void Awake()
	{
		MeleeRigidbody = GetComponent<Rigidbody2D>();
		MeleeSpriteRenderer = GetComponent<SpriteRenderer>();
	}

	/// <summary>
	/// Once per frame
	/// </summary>
	void Update()
	{
		// Updates the mana cost
		UpdateCost();

		Vector2 finalVelocity = Stats.Velocity;

		// Flip the sprite based on direction
		if (MeleeSpriteRenderer.flipX)
		{
			finalVelocity.x *= -1;
		}

		MeleeRigidbody.velocity = finalVelocity;
	}

	/// <summary>
	/// Initializes the instance
	/// This is for the parent object to call
	/// </summary>
	/// <param name="parent"></param>
	/// <param name="passedStats"></param>
	public override void Initialize(GameObject parent, AbilityStats passedStats)
	{
		Parent = parent;

		TargetStats = parent.GetComponent<Stats>();
		Stats = passedStats;

		Destroy(gameObject, Stats.Duration);
	}
}
