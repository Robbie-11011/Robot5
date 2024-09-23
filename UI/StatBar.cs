// Robert Laidley
// September 20th, 2024

using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A script for stat bar objects.
/// It reflects the stats (value, max, baseMax) of a selected stat in a colored UI form
/// </summary>
public class StatBar : MonoBehaviour
{
	/// <summary>
	/// Value, Max, BaseMax of a stat such as hp or mana
	/// </summary>
	private Stat stat;

	/// <summary>
	/// The starting width of the stat bar
	/// </summary>
	private float baseWidth;

	/// <summary>
	/// The current width of the stat bar
	/// </summary>
	private float width;

	/// <summary>
	/// The colored part indicating the stat's positive section
	/// </summary>
	public Image ForeGround;

	/// <summary>
	/// The less-colored or non-colored part indicating the stat's negative section
	/// </summary>
	public Image BackGround;

	/// <summary>
	/// The Target object that the stat bar is mirroring
	/// This would be a Character object if there was enough time for that level of abstraction
	/// </summary>
	private GameObject Target;

	/// <summary>
	/// The Target stat that the stat bar is mirroring
	/// </summary>
	private Stats TargetStats;

	/// <summary>
	/// The text UI element that displays the number values on top of the colored bars
	/// </summary>
	public TextMeshProUGUI TextStat;

	/// <summary>
	/// The target defined by the dev
	/// Defines an enum for an easy dropdown selection
	/// </summary>
	public Targets Target_Type;

	/// <summary>
	/// The monitored stat defined by the dev
	/// </summary>
	public Types Stat_Type;

	/// <summary>
	/// The stat bar color defined by the dev
	/// </summary>
	public Colors Selected_Color;

	/// <summary>
	/// The current or new color of the stat bar
	/// </summary>
	private Color Current_Color;

	/// <summary>
	/// Awake is called once at the very beginning
	/// </summary>
	private void Awake()
	{
		// Sets the target to the dev-defined gameObject
		SetTarget();

		// Gets the initial base width of the positive bar
		baseWidth = ForeGround.transform.localScale.x;
	}

	/// <summary>
	/// Update is called once per frame
	/// </summary>
	void Update()
	{
		// Checks if the target is dead
		CheckEmptyTarget();

		// Refreshes the target object, stat type, and color
		RefreshTarget();

		// Sets the size, position, value, color, and text of the stat bar
		FormatStatBar();
	}

	/// <summary>
	/// Sets the target to null when it is destroyed
	/// This avoids references to a dead object
	/// </summary>
	private void CheckEmptyTarget()
	{
		if (Target.IsDestroyed())
		{
			Target = null;
		}
	}

	/// <summary>
	/// Sets the target and stat type target
	/// Sets the color
	/// </summary>
	private void RefreshTarget()
	{
		// Checks if the target has been destroyed or simply not selected
		if (Target != null)
		{
			SetTarget();
		}

		// Sets the current color based on the dropdown selection by the dev
		switch (Selected_Color)
		{
			case Colors.red:
				Current_Color = UnityEngine.Color.red;
				break;
			case Colors.magenta:
				Current_Color = UnityEngine.Color.magenta;
				break;
			case Colors.blue:
				Current_Color = UnityEngine.Color.blue;
				break;
			case Colors.cyan:
				Current_Color = UnityEngine.Color.cyan;
				break;
			case Colors.green:
				Current_Color = UnityEngine.Color.green;
				break;
			case Colors.yellow:
				Current_Color = UnityEngine.Color.yellow;
				break;
			case Colors.white:
				Current_Color = UnityEngine.Color.white;
				break;
			case Colors.grey:
				Current_Color = UnityEngine.Color.grey;
				break;
			case Colors.black:
				Current_Color = UnityEngine.Color.black;
				break;
			case Colors.clear:
				Current_Color = UnityEngine.Color.clear;
				break;
		}
	}

	/// <summary>
	/// Sets the current target for the monitoring stat bar
	/// </summary>
	private void SetTarget()
	{
		// Finds the first character target and sets it as target
		switch (Target_Type)
		{
			case Targets.player:
				Target = GameObject.FindFirstObjectByType<Player_Manager>().gameObject;
				break;
			case Targets.enemy:
				Target = GameObject.FindFirstObjectByType<Enemy>().gameObject;
				break;
		}

		// Gets the stats component from the target
		TargetStats = Target.GetComponent<Stats>();

		// Gets the stats and displays the desired type (defined by the dev)
		switch (Stat_Type)
		{
			case Types.hp:
				stat = TargetStats.Hp;
				break;
			case Types.mana:
				stat = TargetStats.Mana;
				break;
		}
	}

	private void FormatStatBar()
	{
		// Sets the front color to the chosen color
		ForeGround.color = Current_Color;

		// Gets the ratio of the stat
		float ratio = stat.Value / stat.Max;

		// Sets the width based on that ratio
		width = baseWidth * ratio;

		// Gets the localscale of the foreground
		Vector2 localScale = ForeGround.rectTransform.localScale;

		// Gets the local position of the background (x) and the foreground (y)
		Vector2 localPosition = new Vector2(BackGround.gameObject.transform.localPosition.x, ForeGround.gameObject.transform.localPosition.y);

		// This sets the ratio of the stat bar to that of the stat's current/max ratio
		localScale.x = width;

		// This moves over the statbar
		// A left justify, though not 100% perfect
		localPosition.x += (width - baseWidth) * 50;

		// Sets the changes
		ForeGround.rectTransform.localScale = localScale;
		ForeGround.gameObject.transform.localPosition = localPosition;

		// Justifies center the text
		TextStat.transform.position = BackGround.transform.position;

		// Sets the text to the current stat current/max ratio
		TextStat.text = $"{stat.Value}/{stat.Max}";
	}
}
