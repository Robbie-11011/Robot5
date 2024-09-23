// Robert Laidley
// September 23rd, 2024

using UnityEngine;

/// <summary>
/// Movement for a slime specifically
/// It hops, though, maybe it should also slunk 
/// (I am basing the hopping off of Terraria slimes)
/// (I am basing slunking off of several anime)
/// </summary>
public class Slime_Movement : MonoBehaviour
{
	/// <summary>
	/// A reference to the slimes rigidbody
	/// </summary>
	private Rigidbody2D SlimeRigidbody;

	/// <summary>
	/// A timer for in between hops
	/// </summary>
	private Timer HopCooldownTimer;

	/// <summary>
	/// The velocity of the slime
	/// Long-term
	/// </summary>
	public Vector2 BaseVelocity;

	/// <summary>
	/// The velocity of the slime
	/// Short-term
	/// </summary>
	public Vector2 Velocity;

	/// <summary>
	/// Minimum cooldown time
	/// </summary>
	public float Cooldown_Min;

	/// <summary>
	/// Actual cooldown time
	/// </summary>
	public float Cooldown;

	/// <summary>
	/// Max cooldown time
	/// </summary>
	public float Cooldown_Max;

	/// <summary>
	/// Once at the very beginning
	/// </summary>
	private void Awake()
	{
		SlimeRigidbody = GetComponent<Rigidbody2D>();
		HopCooldownTimer = gameObject.AddComponent<Timer>();
	}

	/// <summary>
	/// Once per frame
	/// </summary>
	void Update()
	{
		if (HopCooldownTimer.IsRunning)
		{
			Velocity = new Vector2(0, 0);
		}
		else
		{
			Cooldown = Random.Range(Cooldown_Min, Cooldown_Max);

			HopCooldownTimer.CooldownTime = Cooldown;

			Velocity = BaseVelocity;

			GameObject player = GameObject.Find("Player");

			if (player.GetComponent<Transform>().position.x < GetComponent<Transform>().position.x)
			{
				Velocity = new Vector2(Cooldown * -1, Cooldown);
			}
			else
			{
				Velocity = new Vector2(Cooldown, Cooldown);
			}

			HopCooldownTimer.ResetTimer();
			SlimeRigidbody.AddForce(Velocity, ForceMode2D.Impulse);
		}
	}
}
