// Robert Laidley
// September 22nd, 2024

using System;
using System.Collections.Generic;

/// <summary>
/// A class to hold the resistance counts and damage counts
/// </summary>
[Serializable]
public class Ego
{
	/// <summary>
	/// A list of elements that the Ego is resistant to
	/// </summary>
	public DamageTypes Resistances;

	/// <summary>
	/// A list of elements that the Ego has taken damage from
	/// These are used to determine the new Ego's resistances
	/// </summary>
	public DamageTypes Damage_Counters;

	/// <summary>
	/// The default values of resistances and damage counters
	/// </summary>
	public Defaults2 Default_Values;

	/// <summary>
	/// Default constructor
	/// </summary>
	public Ego()
	{
		Resistances.Types = new List<Element>();
		Damage_Counters.Types = new List<Element>();

		Resistances.Bounds.BaseMax = 1000;
		Damage_Counters.Bounds.BaseMax = 1000;

		Resistances.Bounds.Max = 1000;
		Damage_Counters.Bounds.Max = 1000;
	}

	/// <summary>
	/// Parameterized constructor with default values passed
	/// </summary>
	/// <param name="counterValue"></param>
	/// <param name="resValue"></param>
	public Ego(float counterValue, float resValue)
	{
		Damage_Counters.Types = new List<Element>();
		Resistances.Types = new List<Element>();

		Damage_Counters.Bounds.BaseMax = 1000;
		Resistances.Bounds.BaseMax = 1000;

		Damage_Counters.Bounds.Max = 1000;
		Resistances.Bounds.Max = 1000;

		Default_Values.Counter = counterValue;
		Default_Values.Resistance = resValue;
	}

	/// <summary>
	/// Initialize the Ego's overall values
	/// </summary>
	public void Initialize()
	{
		Resistances.Types = new List<Element>();
		Damage_Counters.Types = new List<Element>();

		for (int i = 0; i < 8; i++)
		{
			Resistances.Types.Add(new Element((Elements)i, Default_Values.Resistance));
			Damage_Counters.Types.Add(new Element((Elements)i, Default_Values.Counter));
		}
	}

	/// <summary>
	/// Set all damage counters to a specific value
	/// </summary>
	/// <param name="power"></param>
	public void CountersSetAll(int power)
	{
		Damage_Counters.Types = new List<Element>();

		for (int i = 0; i < 8; i++)
		{
			Damage_Counters.Types.Add(new Element((Elements)i, power));
		}
	}

	/// <summary>
	/// Add values to a damagetype in the ego
	/// This is used for damage counters to add to the counter
	/// </summary>
	/// <param name="types"></param>
	/// <param name="other"></param>
	public void AddValue(DamageTypes types, Element other)
	{

		types.Types[(int)other.Type].Power += other.Power;

		if (types.Types[(int)other.Type].Power < types.Bounds.Min)
		{
			types.Types[(int)other.Type].Power = types.Bounds.Min;
		}
		else if (types.Types[(int)other.Type].Power > types.Bounds.Max)
		{
			types.Types[(int)other.Type].Power = types.Bounds.Max;
		}
	}

	/// <summary>
	/// Adjusts the damage taken based on the resistance
	/// </summary>
	/// <param name="resistances"></param>
	/// <param name="other"></param>
	/// <returns></returns>
	public static Element ModifyDamage(DamageTypes resistances, Element other)
	{
		float resistance = resistances.Types[(int)other.Type].Power;

		Element modifiedElement = new Element(other.Type, LScaling(other.Power, resistance));

		return modifiedElement;
	}

	/// <summary>
	/// The formula for the applied damage based on the resistance value
	/// </summary>
	/// <param name="dmg"></param>
	/// <param name="res"></param>
	/// <returns></returns>
	private static float LScaling(float dmg, float res)
	{
		return (dmg * (1 - (res / 1000)));
	}

	/// <summary>
	/// Combines two sets of resistances
	/// </summary>
	/// <param name="egoGet"></param>
	/// <param name="egoGive"></param>
	public static void AddResistances(Ego egoGet, Ego egoGive)
	{
		float pwrGive;

		for (int i = 0; i < 8; i++)
		{
			pwrGive = egoGive.Resistances.Types[i].Power;

			egoGet.Resistances.Types[i].Power += egoGive.Resistances.Types[i].Power;

			if (egoGet.Resistances.Types[i].Power > egoGet.Resistances.Bounds.Max)
			{
				egoGet.Resistances.Types[i].Power = egoGet.Resistances.Bounds.Max;
			}
		}
	}
}
