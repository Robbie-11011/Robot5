// Robert Laidley
// September 22nd, 2024

using UnityEngine;

/// <summary>
/// A healing spell for healing the user
/// </summary>
public class Sup_HealSelf : Ability
{
	/// <summary>
	/// A reference to the target's stats for healing
	/// </summary>
	private Stats targetStats;

	/// <summary>
	/// Once per frame
	/// </summary>
	private void Update()
	{
		UpdateCost();

		if (!Stats.Timer.T.IsRunning)
		{
			Stats.Timer.IsCool = true;
		}

		if (Stats.Timer.IsCool)
		{
			if (targetStats.Hp.Value + Stats.Element_Type.Power < targetStats.Hp.Max)
			{
				targetStats.Hp.Value += Stats.Element_Type.Power;
				targetStats.Mana.Value -= Stats.Cost.Total;

				Stats.Timer.IsCool = false;
				Stats.Timer.T.ResetTimer();
			}
		}
	}

	/// <summary>
	/// Initialize the healing ability from the parent
	/// </summary>
	/// <param name="parent"></param>
	/// <param name="passedStats"></param>
	public override void Initialize(GameObject parent, AbilityStats passedStats)
	{
		Parent = parent;
		targetStats = parent.GetComponent<Stats>();

		Stats = passedStats;

		Stats.Timer.T = gameObject.AddComponent<Timer>();
		Stats.Timer.T.CooldownTime = Stats.Timer.Duration;
		Stats.Timer.T.StartTimer();
	}
}
