// Robert Laidley
// September 22nd, 2024

using System;
using UnityEngine;

/// <summary>
/// A manager script for the player
/// This takes all of the other classes and ties them together
/// </summary>
public class Player_Manager : MonoBehaviour
{
	/// <summary>
	/// The stat of the player: hp, mana, etc.
	/// </summary>
	private Stats stats;

	/// <summary>
	/// The Egos object for the list of egos
	/// CRUD methods included in the egos
	/// </summary>
	[NonSerialized]
	public Egos PlayerEgos;

	/// <summary>
	/// The animator for the player
	/// </summary>
	private Animator PlayerAnimator;

	/// <summary>
	/// The key code for interacting with interactable objects
	/// Examples: loot, buttons
	/// </summary>
	public KeyCode Key_Interact;

	/// <summary>
	/// The current index for the egos list
	/// Best practice may be to put this in the Egos class, 
	///		but this is what is being done for now.
	/// </summary>
	public int Ego_Index;

	/// <summary>
	/// The ability slot for ability 1
	/// Current in dev: fireball
	/// </summary>
	public AbilitySlot Ability1;

	/// <summary>
	/// The instance created
	/// </summary>
	private GameObject Instance1;

	/// <summary>
	/// If the ability is a single instance
	/// There can only be one at a time
	/// Like a fireball that can only be launched again once the other instance has been destroyed
	/// Single Instance
	/// </summary>
	public bool A1Single;

	/// <summary>
	/// If the ability is an on demand instance
	/// When the key stops, so does the ability
	/// Like a healing spell that stops as soon as you let go
	/// On Demand
	/// </summary>
	public bool A1Demand;

	/// <summary>
	/// A timer for the ability cooldown
	/// </summary>
	private Timer Timer1;

	// ---------------------------------------------------------------------------
	// REPEATING CODE, MISPLACED CODE, MESSY CODE
	// ---------------------------------------------------------------------------
	// The ability system needs to be reworked,
	//		but that is the not the current focus
	// The ability system is meant to be:
	//		- Universal of user
	//		- Universal of target
	//		- Universal of ability type (healing, projectile, melee, etc.)
	//		- Highly customizable by the user (setting offset, speed, power, etc.)
	// ---------------------------------------------------------------------------

	/// <summary>
	/// The second ability
	/// </summary>
	public AbilitySlot Ability2;

	/// <summary>
	/// The second instance of the ability
	/// </summary>
	private GameObject Instance2;

	/// <summary>
	/// Single instance 2
	/// </summary>
	public bool A2Single;

	/// <summary>
	/// On demand 2
	/// </summary>
	public bool A2Demand;

	/// <summary>
	/// Cooldown timer 2
	/// </summary>
	private Timer Timer2;

	// This would be the list of abilities
	// public List<AbilitySlot> Abilities;

	/// <summary>
	/// Ability 1 activate button boolean
	/// </summary>
	public bool ActivateAbility1Pressed;

	/// <summary>
	/// Ability 2 activate button boolean
	/// </summary>
	public bool ActivateAbility2Pressed;

	/// <summary>
	/// Interact button pressed
	/// </summary>
	public bool InteractPressed;

	/// <summary>
	/// Scroll wheel data
	/// </summary>
	public Vector2 Scroll;

	/// <summary>
	/// Mouse direction relative to the player
	/// [ Not yet developed fully ]
	/// Going to be used for aiming abilities
	/// </summary>
	public Vector3 Direction;

	/// <summary>
	/// A canvas with the UI text for a game over message
	/// </summary>
	public Canvas GameOverCanvas;

	/// <summary>
	/// Once at the very beginning
	/// </summary>
	private void Awake()
	{
		// Initialize the components
		PlayerEgos = GetComponent<Egos>();
		stats = GetComponent<Stats>();
		PlayerAnimator = GetComponent<Animator>();

		Timer1 = gameObject.AddComponent<Timer>();
		Timer2 = gameObject.AddComponent<Timer>();
	}

	/// <summary>
	/// Once at the beginning
	/// </summary>
	void Start()
    {
		Ego newEgo = new Ego(0, 500);
		newEgo.Initialize();

		PlayerEgos.AddEgo(newEgo);
	}

    /// <summary>
	/// Once every frame
	/// </summary>
    void Update()
    {
		ActivateAbility1Pressed = false;
		ActivateAbility2Pressed = false;
		InteractPressed = false;

		if (Time.timeScale > 0f && !stats.Dead)
		{
			ActivateAbility1Pressed = Input.GetMouseButton(0);

			Ability a1 = Ability1.Prefab.GetComponent<Ability>();
			Timer1.CooldownTime = a1.Stats.Timer.Duration;
			Ability1.Stats.Cost = a1.Stats.Cost;
			Ability1.Stats.Cost.Total = Ability.CalcCost(Ability1.Stats);
			
			Ability a2 = Ability2.Prefab.GetComponent<Ability>();
			Timer2.CooldownTime = a2.Stats.Timer.Duration;
			Ability2.Stats.Cost = a2.Stats.Cost;
			Ability2.Stats.Cost.Total = Ability.CalcCost(Ability2.Stats);

			ActivateAbility2Pressed = Input.GetMouseButton(1);

			// Pressed "E"?
			InteractPressed = Input.GetKey(Key_Interact);

			// Get the mouse position in screen coordinates
			Vector3 mouseScreenPosition = Input.mousePosition;

			// Convert the mouse position to world coordinates
			Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

			// Get the player's position in world coordinates
			Vector3 playerPosition = transform.position;

			// Calculate the direction vector from the player to the mouse position
			Direction = mouseWorldPosition - playerPosition;

			// Set the Z coordinate to 0 for a 2D game
			Direction.z = 0;

			// Normalize the direction vector to get a unit vector
			Direction.Normalize();

			
		}

		// Handles death screen
		if (stats.Dead)
		{
			Time.timeScale = 0;
			GameOverCanvas.gameObject.SetActive(true);
		}

		Scroll = Input.mouseScrollDelta;

		// Ability 1
		UseAbility(gameObject, ActivateAbility1Pressed, ref Timer1, Ability1, ref Instance1, ref A1Single, ref A1Demand);

		// Ability 2
		UseAbility(gameObject, ActivateAbility2Pressed, ref Timer2, Ability2, ref Instance2, ref A2Single, ref A2Demand);

		// Handles death
		if (stats.Dead)
		{
			PlayerAnimator.SetTrigger("Dead");
		}

		// Handles scrolling

		// Scroll up
		if (Scroll.y > 0 && Ego_Index < PlayerEgos.Size.Value - 1)
		{
			// Increment
			Ego_Index++;
		}

		// Scroll down
		if (Scroll.y < 0 && Ego_Index > 0)
		{
			// Decrement
			Ego_Index--;
		}
	}

	/// <summary>
	/// Update the total cost of the abilities
	/// </summary>
	public void UpdateCost()
	{
		Ability1.Stats.Cost.Total = Ability.CalcCost(Ability1.Stats);
		Ability2.Stats.Cost.Total = Ability.CalcCost(Ability2.Stats);
	}

	/// <summary>
	/// When the collider is triggered
	/// </summary>
	/// <param name="target"></param>
	private void OnTriggerEnter2D(Collider2D target)
	{
		Ability other = target.GetComponent<Ability>();

		if (target.CompareTag("Attack2"))
		{
			Element element = Ego.ModifyDamage(PlayerEgos.GetEgos()[0].Resistances, other.Stats.Element_Type);


			stats.Hp.Value -= element.Power;
			PlayerEgos.EgoList[Ego_Index].Damage_Counters.Types[(int)other.Stats.Element_Type.Type].Power += other.Stats.Element_Type.Power;

			PlayerEgos.GetEgos()[0].AddValue(PlayerEgos.GetEgos()[0].Damage_Counters, element);
		}
	}

	/// <summary>
	/// Using an ability
	/// YES, VERY OVERCOMPLICATED BECAUSE THINGS THAT SHOULD BE SIMPLIFIED AND MOVED ARE PILED IN HERE
	/// </summary>
	/// <param name="gm"></param>
	/// <param name="click"></param>
	/// <param name="timer"></param>
	/// <param name="ability"></param>
	/// <param name="instance"></param>
	/// <param name="single"></param>
	/// <param name="demand"></param>
	public void UseAbility(GameObject gm, bool click, ref Timer timer, AbilitySlot ability, ref GameObject instance, ref bool single, ref bool demand)
	{
		//if (click && !timer.IsRunning)
		if ((click && !timer.IsRunning && !demand) || (click && demand))
		{
			if ((instance == null && single) || !single)
			{
				instance = Instantiate(ability.Prefab, gm.transform.position, gm.transform.rotation);

				instance.transform.parent = gm.transform;

				instance.GetComponent<SpriteRenderer>().flipX = gm.GetComponent<SpriteRenderer>().flipX;

				instance.GetComponent<Ability>().Initialize(gm, ability.Stats);

				single = instance.GetComponent<Ability>().SingleInstance;

				demand = instance.GetComponent<Ability>().OnDemand;

				timer.ResetTimer();
			}
		}
		else
		{
			if (instance != null && demand)
			{
				Destroy(instance);
			}
		}
	}
}
