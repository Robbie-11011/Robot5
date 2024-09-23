// Robert Laidley [8.28.24] [8189]

using System;
using UnityEngine;

/// <summary>
/// An item for the inventory used as a placeholder for the to-be instantiated item
/// </summary>
[Serializable]
public class Item
{
	/// <summary>
	/// Type of item
	/// </summary>
	public ItemTypes Type;

	/// <summary>
	/// Item code
	/// </summary>
	public int ItemID;

	/// <summary>
	/// Item Code
	/// </summary>
	public string ID;

	/// <summary>
	/// Image for the item
	/// </summary>
	public Sprite Image;

	/// <summary>
	/// What should this Item be called?
	/// </summary>
	public string Name;

	/// <summary>
	/// How much does this item weigh? [kg]
	/// </summary>
	public float Weight;

	/// <summary>
	/// Is the item stackable?
	/// </summary>
	public bool Stackable;

	/// <summary>
	/// How many are stacked
	/// </summary>
	public int Quantity;

	/// <summary>
	/// How many in a stack?
	/// </summary>
	public int StackLimit;

	/// <summary>
	/// Default constructor sets weight to 0 and non-stackable
	/// </summary>
	public Item()
	{
		ID = "0-0";

		Name = "empty";
		Weight = 0;
		
		Stackable = false;
		Quantity = 1;
		StackLimit = 1;
	}

	/// <summary>
	/// Set the parameters of the item
	/// </summary>
	/// <param name="name">The name of the item</param>
	/// <param name="weight">The weight per item in kg</param>
	/// <param name="quantity">How many in the current stack?</param>
	/// <param name="stackLimit">How many in a stack?</param>
	public Item(string name, float weight, int quantity, int stackLimit)
	{
		Name = name;
		Weight = weight;
		Stackable = true;
		Quantity = quantity;
		StackLimit = stackLimit;
	}

	/// <summary>
	/// Set the parameters of the nonstackable item
	/// </summary>
	/// <param name="name">The name of the item</param>
	/// <param name="weight">The weight per item in kg</param>
	public Item(string name, float weight)
	{
		Name = name;
		Weight = weight;
		Stackable = false;
		Quantity = 1;
		StackLimit = 1;
	}

	/// <summary>
	/// Get the total weight of a stack
	/// </summary>
	/// <param name="item">The item stack you are hoping to get the weight of</param>
	/// <returns>Total weight of the stack in kg</returns>
	public static float GetTotalWeight(Item item)
	{
		return (item.Quantity * item.Weight);
	}
}
