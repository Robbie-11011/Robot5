// Robert Laidley
// September 20th, 2024

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class for a list of egos
/// </summary>
public class Egos : MonoBehaviour
{
	/// <summary>
	/// A list of ego objects
	/// </summary>
	public List<Ego> EgoList;

	/// <summary>
	/// The size of the list (current, max, baseMax)
	/// BaseMax may not be necessary due to Egos not being removed after they are added currently
	/// </summary>
	public Stat Size;

	/// <summary>
	/// Awake calls once at the start
	/// </summary>
	private void Awake()
	{
		Initialize();
	}

	/// <summary>
	/// Adds a new ego to the list
	/// </summary>
	/// <param name="ego"></param>
	public bool AddEgo(Ego ego)
	{

		if (Size.Value < Size.Max)
		{
			Ego newEgo = ego;

			EgoList.Add(newEgo);
			Size.Value++;
			return true;
		}
		else
		{
			return false;
		}
	}

	/// <summary>
	/// Adds a new ego based on the current ego's damage counters
	/// </summary>
	/// <param name="ego"></param>
	/// <returns></returns>
	public bool AddBasedEgo(Ego ego)
	{
		Ego newEgo = new Ego();

		newEgo.Default_Values.Counter = 0;
		newEgo.Default_Values.Resistance = 0;

		newEgo.Initialize();

		newEgo.Resistances = ego.Damage_Counters;
		Ego.AddResistances(newEgo, ego);

		ego.CountersSetAll(0);

		return AddEgo(newEgo);
	}

	/// <summary>
	/// Removes the indicated ego from the list
	/// </summary>
	/// <param name="index"></param>
	public bool DropEgo(Ego ego)
	{
		if (EgoList.Count > 0)
		{
			EgoList.Remove(ego);
			Size.Max--;
			return false;
		}
		else
		{
			return false;
		}
	}

	/// <summary>
	/// Gets the list of egos
	/// </summary>
	/// <returns></returns>
	public List<Ego> GetEgos()
	{
		return EgoList;
	}

	/// <summary>
	/// Updates an Ego in the list to the passed in Ego
	/// </summary>
	/// <param name="index"></param>
	/// <param name="newEgo"></param>
	public void SetEgoById(int index, Ego newEgo)
	{
		EgoList[index] = newEgo;
	}

	/// <summary>
	/// Gets an ego in the list of egos at the specified index
	/// </summary>
	/// <param name="index"></param>
	/// <returns></returns>
	public Ego GetEgoById(int index)
	{
		return EgoList[index];
	}

	/// <summary>
	/// Resets the list of egos
	/// </summary>
	public void Initialize()
	{
		EgoList = new List<Ego>();

		Size.Value = 0;
	}
}
