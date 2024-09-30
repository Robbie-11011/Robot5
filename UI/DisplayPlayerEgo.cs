// Robert Laidley
// September 22nd, 2024

using TMPro;
using UnityEngine;

/// <summary>
/// A script specifcally for displaying the player's stats
/// </summary>
public class DisplayPlayerEgo : MonoBehaviour
{
	/// <summary>
	/// A reference to the player manager
	/// </summary>
	private Player_Manager PlayerManager;

	/// <summary>
	/// A reference to the player stats
	/// </summary>
	private Stats PlayerStats;

	/// <summary>
	/// A reference to the player egos
	/// </summary>
	private Egos PlayerEgos;

	/// <summary>
	/// A reference to the currently selected player ego
	/// </summary>
	private Ego SelectedEgo;

	/// <summary>
	/// A reference to the text UI element to display these stats to
	/// </summary>
	private TextMeshProUGUI StatsText;

	/// <summary>
	/// A toggle for the resistances and damage counters to be shown
	/// </summary>
	public bool Expanded;

	/// <summary>
	/// Once at the very beginning
	/// </summary>
	private void Awake()
	{
		StatsText = GetComponent<TextMeshProUGUI>();
		GameObject player = GameObject.Find("Player");

		PlayerManager = player.GetComponent<Player_Manager>();
		PlayerStats = player.GetComponent<Stats>();
		PlayerEgos = player.GetComponent<Egos>();

	}

	/// <summary>
	/// Once per frame
	/// </summary>
	void Update()
	{
		if (PlayerStats != null && StatsText != null && PlayerEgos != null)
		{
			SelectedEgo = PlayerEgos.GetEgos()[(int)PlayerManager.Ego_Index];
			Display();
		}
	}

	/// <summary>
	/// Displays the ego info
	/// </summary>
	void Display()
	{
		StatsText.text =
			$"Ego: {PlayerManager.Ego_Index + 1} / {PlayerEgos.Size.Value}\n" +
			$"Ego Limit: {PlayerEgos.Size.Max}\n\n";

		// Check if the res and cnt lists should be shown
		if (Expanded)
		{

			// Res header
			StatsText.text += $"[ RESISTANCES ]\n\n";

			// Display resistances
			for (int i = 0; i < 8; i++)
			{
				Element type = PlayerEgos.EgoList[PlayerManager.Ego_Index].Resistances.Types[i];

				StatsText.text += $"{type.Type}: {type.Power}\n";
			}

			// Cnt header
			StatsText.text += $"\n[ DAMAGE COUNTERS ]\n\n";

			// Display damage counters
			for (int i = 0; i < 8; i++)
			{
				Element type = PlayerEgos.EgoList[PlayerManager.Ego_Index].Damage_Counters.Types[i];

				StatsText.text += $"{type.Type}: {type.Power}\n";
			}
		}
	}
}
