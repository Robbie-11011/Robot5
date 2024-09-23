// Robert Laidley
// September 22nd, 2024

using UnityEngine;

/// <summary>
/// For camera position that follows the player
/// </summary>
public class Camera_Movement : MonoBehaviour
{
    /// <summary>
    /// The target
    /// </summary>
    private GameObject Player;

    /// <summary>
    /// The target's position
    /// </summary>
    private Vector3 Player_Position;
    
    /// <summary>
    /// The offset for depth and up and down primarily
    /// </summary>
    [Header("Position Offset")]
    public Vector3 Camera_Offset;

    /// <summary>
    /// Once at the very beginning
    /// </summary>
	private void Awake()
	{
        // Sets the target
		Player = GameObject.Find("Player");
	}

	/// <summary>
    /// Once at the beginning
    /// </summary>
	void Start()
    {

		transform.position = Player.transform.position;
    }

    /// <summary>
    /// Once per frame
    /// </summary>
    void Update()
    {
        Player_Position = Player.transform.position;

        Vector3 offset = Camera_Offset;

        float new_y = offset.y;

		if (Player_Position.y > offset.y + 2)
        {
            new_y += Player_Position.y;
		}

        Vector3 cameraPosition = new Vector3(Player_Position.x + offset.x, new_y, Player_Position.z + offset.z);

        transform.position = cameraPosition;
    }
}
