// Robert Laidley [8.28.24] [8189]

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Inventory class built for a Unity character
/// </summary>
public class Inventory : MonoBehaviour
{
	/// <summary>
	/// A list of Inventory items
	/// </summary>
	public List<Item> Items;

	/// <summary>
	/// In-game currency
	/// </summary>
	public int Credits;

	/// <summary>
	/// A cur/Bmax/max struct for the weight of the inventory
	/// </summary>
	public Stat Weight;

	/// <summary>
	/// Number of slots in the inventory
	/// </summary>
	public int SlotCount;

	private void Awake()
	{
		Items = new List<Item>();
	}

	// Start is called before the first frame update
	void Start()
	{
		AddItem(new Item("Crystal", .5f, 1, 16));
		AddItem(new Item("Sword", 3));
	}

	// Update is called once per frame
	void Update()
	{
		CleanSlots();
		UpdateWeight();
	}

	/// <summary>
	/// Check for slots with a quantity of 0 and replace it with an empty item
	/// </summary>
	private void CleanSlots()
	{
		for (int i = 0; i < Items.Count; i++)
		{
			if (Items[i].Quantity <= 0)
			{
				Items[i] = ClearSlot();
			}
		}
	}

	/// <summary>
	/// Return an empty slot
	/// </summary>
	/// <returns></returns>
	private Item ClearSlot()
	{
		return new Item("empty", 0);
	}

	/// <summary>
	/// Updates the weight of all the items in the inventory every time it is called
	/// </summary>
	private void UpdateWeight()
	{
		Weight.Value = 0;

		// Add up all the items' weights and assign the value to the current weight of the inventory
		foreach (var item in Items)
		{
			Weight.Value += (Item.GetTotalWeight(item));
		}
	}

	public void AddItem(Item item)
	{


		for (int i = 0; i < Items.Count; i++)
		{
			Item slot = Items[i];

			if (slot.ID == item.ID && item.Stackable)
			{
				if (slot.Quantity + item.Quantity <= slot.StackLimit)
				{
					slot.Quantity += item.Quantity;
				} else
				{
					slot.Quantity += slot.StackLimit - slot.Quantity;
				}
				return;
			}
		}

		if (Items.Count < SlotCount)
		{
			Items.Add(item);
		} else
		{
			Debug.Log("Inventory Full");
		}
	}

	public void AddCredits(int credits)
	{
		Credits += credits;
	}

	public void SetCredits(int credits)
	{
		Credits = credits;
	}

	public bool RemoveCredits(int cost)
	{
		if (Credits - cost >= 0)
		{
			Credits -= cost;
			return true;
		} else
		{
			return false;
		}
	}
}
