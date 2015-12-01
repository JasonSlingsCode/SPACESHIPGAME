using UnityEngine;
using System.Collections;

/// <summary>
/// Simply moves the current game object
/// </summary>
public class MoveScript : MonoBehaviour
{
    // 1 - Designer variables

    /// <summary>
    /// Object speed
    /// </summary>
	public float shotSpeed;
    private Vector2 speed;

    /// <summary>
    /// Moving direction
    /// </summary>
    public Vector2 direction = new Vector2(-1, 0);

    private Vector2 movement;

    void Start()
    {
		speed = new Vector2 (shotSpeed, shotSpeed);
        // 2 - Movement
        movement = new Vector2(
          speed.x * direction.x,
          speed.y * direction.y);
          GetComponent<Rigidbody2D>().AddForce(movement, ForceMode2D.Impulse);
    }

    void FixedUpdate()
    {
        // Apply movement to the rigidbody
        // GetComponent<Rigidbody2D>().AddForce(movement, ForceMode2D.Impulse);
        // OR
        // GetComponent<Rigidbody2D>().velocity = movement;
    }
}