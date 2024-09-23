// Robert Laidley
// September 23rd, 2024

using UnityEngine;

/// <summary>
/// The ability class for attacks, heals, etc.
/// </summary>
public class Ability : MonoBehaviour
{
	/// <summary>
	/// Name of the ability
	/// For example: Fireball, Heal, Punch
	/// </summary>
    public string Name;

	/// <summary>
	/// The stats of the ability
	/// </summary>
	public AbilityStats Stats;

	/// <summary>
	/// Are there multiple of the same instances allowed?
	/// </summary>
	public bool SingleInstance;

	/// <summary>
	/// Will the object be destroyed immediately after release?
	/// </summary>
	public bool OnDemand;

	/// <summary>
	/// Is this a melee ability?
	/// This may not be necessary
	/// </summary>
	public bool Melee;

	/// <summary>
	/// Where did this ability instance originate?
	/// </summary>
	protected GameObject Parent;

	/// <summary>
	/// Once per frame
	/// </summary>
	private void Update()
	{
		UpdateCost();
	}

	/// <summary>
	/// Updates the cost of the mana for the ability based on the ability customizations
	/// </summary>
	protected void UpdateCost()
	{
		Stats.Cost.Total = CalcCost(Stats);
	}

	/// <summary>
	/// Initialization called by the parent
	/// </summary>
	/// <param name="parent"></param>
	/// <param name="passedStats"></param>
	public virtual void Initialize(GameObject parent, AbilityStats passedStats)
	{
		Parent = parent;
		Stats = passedStats;
	}

	/// <summary>
	/// Calculates the cost of the ability
	/// </summary>
	/// <param name="stats"></param>
	/// <returns></returns>
	public static float CalcCost(AbilityStats stats)
	{
		return stats.Duration * stats.Cost.Duration - stats.Timer.Duration * stats.Cost.Cool + stats.Element_Type.Power * stats.Cost.Power;
	}
}
