// Robert Laidley
// September 21st, 2024

using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A structure for stats in the game
/// Examples: HP, Mana, Stamina
/// </summary>
[Serializable]
public struct Stat
{
	/// <summary>
	/// The current value of the stat
	/// </summary>
	[Tooltip("The current value of the stat.")]
	public float Value;

	/// <summary>
	/// The max value of the stat
	/// </summary>
	[Tooltip("The long-term max value of the stat.")]
	public float BaseMax;

	/// <summary>
	/// The modified/temporary max value of the stat
	/// </summary>
	[Tooltip("I short-term max value of the stat")]
	public float Max;
}

/// <summary>
/// A structure for a set range that can be modified temporarily if needed
/// </summary>
[Serializable]
public struct Range
{
	/// <summary>
	/// The long-term minimum
	/// </summary>
	public float BaseMin;

	/// <summary>
	/// The short-term minimum
	/// </summary>
	public float Min;

	/// <summary>
	/// The long-term maximum
	/// </summary>
	public float BaseMax;

	/// <summary>
	/// The short-term maximum
	/// </summary>
	public float Max;
}

/// <summary>
/// A structure for a list of elements
/// Examples: Resistances, Damage_Counters
/// </summary>
[Serializable]
public struct DamageTypes
{
	/// <summary>
	/// The list of Elements
	/// Examples: Fire 500, Ice 200
	/// </summary>
	public List<Element> Types;

	/// <summary>
	/// The bounds of the Element power
	/// Example: 0 - 1000
	/// </summary>
	public Range Bounds;
}

/// <summary>
/// Default values for the Resistances and Counters
/// </summary>
[Serializable]
public struct Defaults2
{
	/// <summary>
	/// Default value for the resistance
	/// </summary>
	public float Resistance;

	/// <summary>
	/// Default value for the damage counter
	/// </summary>
	public float Counter;
}

/// <summary>
/// A string of text with color
/// </summary>
public struct Line
{
	/// <summary>
	/// The string of text
	/// </summary>
	public string Text;

	/// <summary>
	/// The color
	/// </summary>
	public Color TextColor;
}

/// <summary>
/// A structure for objects that require cooldowns
/// </summary>
[Serializable]
public struct CoolDown
{
	/// <summary>
	/// A cool boolean, indicating the objects has cooled down
	/// </summary>
	public bool IsCool;

	/// <summary>
	/// The duration of the timer
	/// </summary>
	public float Duration;

	/// <summary>
	/// A custom timer object for all the counting and such
	/// </summary>
	[NonSerialized]
	public Timer T;
}

/// <summary>
/// The cost of using the ability
/// </summary>
[Serializable]
public struct AbilityCost
{
	/// <summary>
	/// Cost of attack points per, healing points per, etc.
	/// </summary>
	public float Power;

	/// <summary>
	/// Cost of duration per
	/// </summary>
	public float Duration;

	/// <summary>
	/// Cost of cooldown per (negative)
	/// </summary>
	public float Cool;

	/// <summary>
	/// Total cost of the ability per
	/// </summary>
	public float Total;
}

/// <summary>
/// The stats of an ability
/// This is mostly here for organization
/// </summary>
[Serializable]
public struct AbilityStats
{
	/// <summary>
	/// A timer for cooldowns
	/// </summary>
	public CoolDown Timer;

	/// <summary>
	/// The element the ability uses
	/// </summary>
	public Element Element_Type;

	/// <summary>
	/// The duration of the ability
	/// </summary>
	public float Duration;

	/// <summary>
	/// Where the ability should be instantiate relative to the user
	/// </summary>
	public Vector2 Offset;

	/// <summary>
	/// How fast and in which direction the ability objects goes
	/// </summary>
	public Vector2 Velocity;

	/// <summary>
	/// The mana cost of the ability
	/// </summary>
	public AbilityCost Cost;
}

/// <summary>
/// This is just an offset for organizational purposes
/// Unity uses drawers with structs, so it makes a new section for it
/// </summary>
[Serializable]
public struct PositionOffset
{
	/// <summary>
	/// The offset desired, relative to the object
	/// </summary>
	public Vector3 offset;
}