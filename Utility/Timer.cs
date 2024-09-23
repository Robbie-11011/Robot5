// Robert Laidley
// September 23rd, 2024
// Assisted by ChatGPT

using UnityEngine;

/// <summary>
/// A custom timer for general use
/// </summary>
public class Timer : MonoBehaviour
{
	/// <summary>
	/// Cooldown time in seconds
	/// </summary>
	public float CooldownTime;

	/// <summary>
	/// The remaining time in the timer
	/// </summary>
	private float RemainingTime;

	/// <summary>
	/// Property to handle whether the timer is running
	/// </summary>
	private bool isRunning = false;

	/// <summary>
	/// Property to check if the timer is running
	/// Read only
	/// </summary>
	public bool IsRunning => isRunning;

	/// <summary>
	/// Once per frame
	/// </summary>
	private void Update()
	{
		if (isRunning)
		{
			RemainingTime -= Time.deltaTime;
			if (RemainingTime <= 0f)
			{
				isRunning = false;
			}
		}
	}

	/// <summary>
	/// Start the timer
	/// </summary>
	public void StartTimer()
	{
		RemainingTime = CooldownTime;
		isRunning = true;
	}

	/// <summary>
	/// Pause the timer
	/// </summary>
	public void PauseTimer()
	{
		isRunning = false;
	}

	/// <summary>
	/// Resume the timer
	/// </summary>
	public void ResumeTimer()
	{
		isRunning = true;
	}

	/// <summary>
	/// Reset the timer
	/// </summary>
	public void ResetTimer()
	{
		RemainingTime = CooldownTime;
		isRunning = true;
	}
}
