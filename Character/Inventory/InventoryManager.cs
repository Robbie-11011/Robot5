// Robert Laidley
// September 22nd, 2024

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the Inventory UI
/// </summary>
public class InventoryManager : MonoBehaviour
{
	/// <summary>
	///  The Inventory being displayed
	/// </summary>
	public Inventory TargetInventory;

	/// <summary>
	/// The canvas the inventory will be displayed on
	/// </summary>
	public Canvas TargetCanvas;

	/// <summary>
	///  The list of image slots
	/// </summary>
	public List<GameObject> Blocks;

	/// <summary>
	/// The list of images used for the items
	/// </summary>
	public List<Image> Images;

	/// <summary>
	/// A boolean for whether the Inventory key is hit or not
	/// </summary>
	private bool OpenInventory;

	/// <summary>
	/// The open inventory key to check for
	/// </summary>
	public KeyCode OpenInventoryKey;

	/// <summary>
	/// A boolean for whether this UI is active or not
	/// </summary>
	public bool IsActive;

	/// <summary>
	/// Once at the very beginning
	/// </summary>
	private void Awake()
	{
		foreach (var block in Blocks)
		{
			Images.Add(block.GetComponent<Image>());
		}
	}

	/// <summary>
	/// Once at the beginning
	/// </summary>
	private void Start()
	{
		TargetCanvas.gameObject.SetActive(false);
	}

	/// <summary>
	/// Once per frame
	/// </summary>
	private void Update()
	{
		RefreshImages();

		if (Time.deltaTime > 0)
		{
			OpenInventory = Input.GetKeyDown(OpenInventoryKey);

			if (OpenInventory)
			{
				ToggleUI();
			}
		}
	}

	/// <summary>
	/// Toggle the Inventory UI on and off
	/// </summary>
	public void ToggleUI()
	{
		IsActive = !IsActive;
		TargetCanvas.gameObject.SetActive(IsActive);
	}

	/// <summary>
	/// Refreshes the visual side of the inventory
	/// </summary>
	private void RefreshImages()
	{
		for (int i = 0; i < TargetInventory.Items.Count; i++)
		{
			Images[i].sprite = TargetInventory.Items[i].Image;
		}
	}
}
