// Robert Laidley
// September 23rd, 2024

using TMPro;
using UnityEngine;

/// <summary>
/// Display the stats of the player's abilities
/// </summary>
public class DisplayAbilityStats : MonoBehaviour
{
	/// <summary>
	/// A reference to the player's manager
	/// </summary>
	private Player_Manager PlayerManager;

	/// <summary>
	/// The output text element
	/// </summary>
	public TextMeshProUGUI TextOutput;

	/// <summary>
	/// Once at the very beginning
	/// </summary>
	private void Awake()
	{
		TextOutput = GetComponent<TextMeshProUGUI>();

		PlayerManager = GameObject.Find("Player").GetComponent<Player_Manager>();
	}

	/// <summary>
	/// Once per frame
	/// </summary>
	private void Update()
	{
		// If the player manager and text element exist
		if (PlayerManager != null && TextOutput != null)
		{
			UpdateStatsText();
		}
	}

	/// <summary>
	/// Updates the text
	/// </summary>
	private void UpdateStatsText()
	{
		AbilityStats a1 = PlayerManager.Ability1.Stats;
		AbilityStats a2 = PlayerManager.Ability2.Stats;


		TextOutput.text = $"Ability 1: {PlayerManager.Ability1.Prefab.GetComponent<Ability>().Name}\n" +
						 $"Cool Time: {a1.Timer.Duration}\n" +
						 $"Duration: {a1.Duration}\n" +
						 $"Power: {a1.Element_Type.Power}\n" +
						 $"X-Offset: {a1.Offset.x}\n" +
						 $"Y-Offset: {a1.Offset.y}\n" +						 
						 $"X-Velocity: {a1.Velocity.x}\n" +
						 $"Y-Velocity: {a1.Velocity.y}\n" +
						 $"Cost: {a1.Cost.Total}\n" +
						 $"\n\n" +
						 $"Ability 2: {PlayerManager.Ability2.Prefab.GetComponent<Ability>().Name}\n" +
						 $"Cool Time: {a2.Timer.Duration}\n" +
						 $"Duration: {a2.Duration}\n" +
						 $"Power: {a2.Element_Type.Power}\n" +
						 $"X-Offset: {a2.Offset.x}\n" +
						 $"Y-Offset: {a2.Offset.y}\n" +
						 $"X-Velocity: {a2.Velocity.x}\n" +
						 $"Y-Velocity: {a2.Velocity.y}\n" +
						 $"Cost: {a2.Cost.Total}\n" +
						 $"\n\n";
		
	}
}
