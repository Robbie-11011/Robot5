// Robert Laidley
// September 21st, 2024

using UnityEngine;

/// <summary>
/// A button that, when pressed, performs an action.
/// The action is meant to be more abstract, but this one simply adds a new ego to the player if possible.
/// It uses the current version of Trauma Mechanics by basing the new ego resistances off of the damage recieved and counted by the current ego.
/// </summary>
public class Button : MonoBehaviour
{
	/// <summary>
	/// A timer so there is a cooldown between button presses
	/// </summary>
	public Timer t;

	/// <summary>
	/// Whether the player is triggering the button or not
	/// </summary>
	public bool Triggered;

	/// <summary>
	/// The default resistance value for non-based egos
	/// </summary>
	public float Default_Resistance;

	/// <summary>
	/// The default damage counter value for non-based egos
	/// </summary>
	public float Default_Counter;

	/// <summary>
	/// The enum of button functions for the easy dropdown pick
	/// </summary>
	public ButtonFunction Function;

	/// <summary>
	/// The manager script used for the player
	/// </summary>
	GameObject p;

	/// <summary>
	/// Called once at the very beginning
	/// </summary>
	private void Awake()
	{
		t = gameObject.AddComponent<Timer>();
		p = GameObject.Find("Player");
	}

	/// <summary>
	/// Called once at the beginning
	/// </summary>
	void Start()
	{
		t.CooldownTime = 1;
		t.StartTimer();
	}

	/// <summary>
	/// Called once per frame
	/// </summary>
	void Update()
	{
		if (!t.IsRunning)
		{
			Player_Manager playerManager = p.GetComponent<Player_Manager>();

			if (Triggered && playerManager.InteractPressed)
			{
				// Adds a new ego based on the current with damage counters
				ExecutePreparedCommand();
				t.ResetTimer();
			}
		}
	}

	/// <summary>
	/// When the player trigger the button
	/// </summary>
	/// <param name="collision"></param>
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			Triggered = true;
		}
	}


	/// <summary>
	/// When the player stops triggering the button
	/// </summary>
	/// <param name="collision"></param>
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			Triggered = false;
		}
	}

	/// <summary>
	/// Execute the method defined by the dev
	/// </summary>
	private void ExecutePreparedCommand()
	{
		switch (Function)
		{
			case ButtonFunction.EgoAddBased:
				if (p.GetComponent<Inventory>().RemoveCredits(1))
				{
					AddBasedEgo();
				}
				else
				{
					Debug.Log("[ NOT ENOUGH CREDITS ]");
				}
				break;
			case ButtonFunction.EgoAddBlank:
				AddNewEgo(Default_Resistance, Default_Counter);
				break;
			case ButtonFunction.IncrementEgoMax:
				if (p.GetComponent<Inventory>().RemoveCredits(1))
				{
					IncremenEgoMax();
				}
				else
				{
					Debug.Log("[ NOT ENOUGH CREDITS ]");
				}
				break;
			default:
				Debug.Log("Why am I here?");
				break;
		}
	}

	/// <summary>
	/// Add an ego based off of the current ego
	/// </summary>
	private void AddBasedEgo()
	{
		Egos egos = p.GetComponent<Egos>();

		egos.AddBasedEgo(egos.EgoList[p.GetComponent<Player_Manager>().Ego_Index]);
	}

	/// <summary>
	/// Add an ego based on the dev-defined variables
	/// </summary>
	/// <param name="res"></param>
	/// <param name="cnt"></param>
	private void AddNewEgo(float res, float cnt)
	{
		Ego ego = new Ego();
		ego.Default_Values.Resistance = res;
		ego.Default_Values.Counter = cnt;
		ego.Initialize();

		p.GetComponent<Egos>().AddEgo(ego);
	}

	/// <summary>
	/// Increment the max number of egos the player can currently have
	/// </summary>
	private void IncremenEgoMax()
	{
		p.GetComponent<Egos>().Size.Max += 1;
	}
}
