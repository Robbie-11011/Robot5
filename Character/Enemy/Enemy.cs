// Robert Laidley
// September 22nd, 2024

using TMPro;
using UnityEngine;

/// <summary>
/// The enemy script for all enemies, if you can believe it
/// </summary>
public class Enemy : MonoBehaviour
{
	/// <summary>
	/// The stats for the enemy
	/// </summary>
    private Stats stats;

	/// <summary>
	/// The first ability for enemies
	/// There is only one right now, 
	///		though making a list might be better if that ever changes
	/// </summary>
    public AbilitySlot Ability1;

	/// <summary>
	/// The timer for the Ability1
	/// </summary>
	//private Timer timer1;

	/// <summary>
	/// The animator for managing the various animations
	/// </summary>
	private Animator anim;

	/// <summary>
	/// The collider for triggers and collisions
	/// </summary>
	private BoxCollider2D bx;

	/// <summary>
	/// The physics component for collision
	/// </summary>
	private Rigidbody2D rb;

	/// <summary>
	/// The prefab for the loot prompt
	/// </summary>
	public GameObject Loot;

	/// <summary>
	/// The created instance for the loot prompt
	/// </summary>
	private GameObject loot_instance;

	/// <summary>
	/// A bool for the Player triggering the collision box
	/// Used when turned into loot
	/// </summary>
	private bool PlayerTriggered;

	/// <summary>
	/// The player object for checking interaction
	/// </summary>
	private GameObject Player;

	/// <summary>
	/// Some text for a health bar
	/// It will be a healthbar like the player has, but for now, it is simply numbers
	/// </summary>
	public TextMeshProUGUI Healthbar;

	/// <summary>
	/// Once a the very beginning
	/// </summary>
	private void Awake()
	{
		Player = GameObject.Find("Player");

		stats = gameObject.GetComponent<Stats>();

		Ability1.Stats.Timer.T = gameObject.AddComponent<Timer>();
		Ability1.Stats.Timer.T.CooldownTime = Ability1.Stats.Timer.Duration;
		Ability1.Stats.Timer.T.StartTimer();

		anim = GetComponent<Animator>();
		bx = GetComponent<BoxCollider2D>();
		rb = GetComponent<Rigidbody2D>();
	}

    /// <summary>
	/// Once per frame
	/// </summary>
    void Update()
    {
		Vector2 newPosition = transform.position;
		newPosition.y += bx.size.y / 2;
		// newPosition.y += 0.4f;

		Healthbar.transform.position = newPosition;

		Healthbar.text = $"{stats.Hp.Value}/{stats.Hp.Max}";

		// Handle Death
		if (stats.Dead)
		{
			
			if (loot_instance == null)
			{
				loot_instance = Instantiate(Loot, transform.position, transform.rotation);

				loot_instance.transform.parent = transform;
			}

			anim.SetTrigger("Dead");

			tag = "Loot";
			gameObject.layer = 14;
			bx.isTrigger = true;
			rb.bodyType = RigidbodyType2D.Static;
		}

		// While cooldown is not current
		if (!Ability1.Stats.Timer.T.IsRunning && !stats.Dead)
		{
			// Instantiate Attack
			GameObject instance = Instantiate(Ability1.Prefab, transform.position, transform.rotation);

			// Define attack
			Ability ability = instance.GetComponent<Ability>();

			// Initialize attack
			ability.Initialize(gameObject, Ability1.Stats);

			// This is parent of instance
			instance.transform.parent = transform;

			// Resets the timer
			Ability1.Stats.Timer.T.ResetTimer();
		}

		Player_Manager playerManager = Player.GetComponent<Player_Manager>();
		Stats playerStats = Player.GetComponent<Stats>();

		if (PlayerTriggered && playerManager.InteractPressed && stats.Dead)
		{
			playerStats.Mana.Value += stats.Mana.Value;

			if (playerStats.Mana.Value < 0)
			{
				playerStats.Mana.Value = 0;
			}
			else if (playerStats.Mana.Value > playerStats.Mana.Max)
			{
				playerStats.Mana.Value = playerStats.Mana.Max;
			}

			Destroy(gameObject);
		}
	}

	/// <summary>
	/// Checks for collisions/triggers entering
	/// </summary>
	/// <param name="target"></param>
	private void OnTriggerEnter2D(Collider2D target)
	{
		Ability other = target.gameObject.GetComponent<Ability>();

		if (target.CompareTag("Attack1"))
		{
			stats.Hp.Value -= other.Stats.Element_Type.Power;
			anim.SetTrigger("Hurt");
		}

		if (target.CompareTag("Player") && !PlayerTriggered && stats.Dead)
		{
			PlayerTriggered = true;
		}
	}


	/// <summary>
	/// Checks for collisions/triggers exiting
	/// </summary>
	/// <param name="target"></param>
	private void OnTriggerExit2D(Collider2D target)
	{
		if (target.CompareTag("Player") && PlayerTriggered && stats.Dead)
		{
			PlayerTriggered = false;
		}
	}
}
