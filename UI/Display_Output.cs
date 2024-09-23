// Robert Laidley
// September 23rd, 2024

using TMPro;
using UnityEngine;

/// <summary>
/// Displays the output of the UIManager
/// This could be more abstract, but that will have to come in the future
/// </summary>
public class Display_Output : MonoBehaviour
{
	/// <summary>
	/// A reference to the UI Manager script
	/// </summary>
	private UIManager Manager;

	/// <summary>
	/// A reference to the ouput text element
	/// </summary>
	private TextMeshProUGUI TextOutput;

	/// <summary>
	/// Once at the very beginning
	/// </summary>
	private void Awake()
	{
		// Get the text component
		TextOutput = GetComponent<TextMeshProUGUI>();

		// Find the object
		GameObject manager = GameObject.Find("UIManager");

		// Get the manager component from the object
		Manager = manager.GetComponent<UIManager>();
	}

	/// <summary>
	/// Once per frame
	/// </summary>
	private void Update()
	{
		if (Manager != null && TextOutput != null)
		{
			UpdateStatsText();
		}
	}

	/// <summary>
	/// Updates the text and color of the output
	/// </summary>
	private void UpdateStatsText()
	{
		TextOutput.text = Manager.Output.Text;
		TextOutput.color = Manager.Output.TextColor;
	}
}
