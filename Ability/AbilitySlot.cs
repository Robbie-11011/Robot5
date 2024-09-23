// Robert Laidley
// September 22nd, 2024

using System;
using UnityEngine;

/// <summary>
/// The slot for an ability
/// </summary>
[Serializable]
public class AbilitySlot
{
	/// <summary>
	/// The ability prefab to be instantiated
	/// </summary>
	public GameObject Prefab;

	/// <summary>
	/// The stats struct for the ability
	/// </summary>
	public AbilityStats Stats;
}
