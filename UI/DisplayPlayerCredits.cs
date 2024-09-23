// Robert Laidley
// September 22nd, 2024

using TMPro;
using UnityEngine;

/// <summary>
/// This is for display the player's current number of credits un the UI
/// </summary>
public class DisplayPlayerCredits : MonoBehaviour
{
    /// <summary>
    /// The inventory the credits are stored in
    /// </summary>
    private Inventory PlayerInventory;

    /// <summary>
    /// The text UI element to display those credits
    /// </summary>
    private TextMeshProUGUI CreditDisplay;

    /// <summary>
    /// Once at the very beginning
    /// </summary>
	private void Awake()
	{
		PlayerInventory = GameObject.Find("Player").GetComponent<Inventory>();
        CreditDisplay = GetComponent<TextMeshProUGUI>();
	}

	/// <summary>
    /// Once at the beginning
    /// </summary>
	void Start()
    {
        Display();
    }

    /// <summary>
    /// Once per frame
    /// </summary>
    void Update()
    {
        Display();
    }

    /// <summary>
    /// Displays the current number of credits
    /// </summary>
    private void Display()
    {
        CreditDisplay.text = $"CREDITS: {PlayerInventory.Credits}";
    }
}
