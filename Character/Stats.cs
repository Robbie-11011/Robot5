// Robert Laidley
// September 21st, 2024

using UnityEngine;

/// <summary>
/// The stats of a character
/// This manages life and death
/// </summary>
public class Stats : MonoBehaviour
{
	/// <summary>
	/// value, max, baseMax of health
	/// </summary>
    public Stat Hp;

	/// <summary>
	/// value, max, baseMax of magic power
	/// </summary>
    public Stat Mana;

	/// <summary>
	/// Am I cooked?
	/// </summary>
	public bool Dead;

	/// <summary>
	/// Once per frame
	/// </summary>
	private void Update()
	{
		if (!Dead)
		{

		CheckDead(Hp);
		CheckDead(Mana);
		} else
		{
			Hp.Value = 0;
		}
	}

	/// <summary>
	/// Checks if the stat is below or equal to 0
	/// Marks as dead if so
	/// </summary>
	/// <param name="stat"></param>
	private void CheckDead(Stat stat)
	{
		// Rubber band stat
		if (stat.Value > stat.Max)
		{
			stat.Value = stat.Max;
		}
		else if (stat.Value <= 0)
		{
			stat.Value = 0;

			// Yes, all that McDonalds finally got to you
			Dead = true;
		}
	}
}
