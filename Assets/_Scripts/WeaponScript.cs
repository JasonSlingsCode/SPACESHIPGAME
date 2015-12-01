using UnityEngine;
using System.Collections;

/// <summary>
/// Launch projectile
/// </summary>
public class WeaponScript : MonoBehaviour
{
	public GameObject CPU;
	public GameObject HardpointMgr;
	//--------------------------------
	// 1 - Designer variables
	//--------------------------------

	/// <summary>
	/// Projectile prefab for shooting
	/// </summary>
	public Transform shotPrefab;

	/// <summary>
	/// Cooldown in seconds between two shots
	/// </summary>
	public float shootingRate = 0.25f;

	//--------------------------------
	// 2 - Cooldown
	//--------------------------------

	private float shootCooldown;

	void Start ()
	{
		CPU = transform.root.gameObject;
		shootCooldown = 0f;
		HardpointMgr = transform.parent.gameObject;
		audioSource = HardpointMgr.GetComponent<AudioSource> ();
		audioSource.clip = laserFireSound;
	}

	void Update ()
	{
		if (shootCooldown > 0) {
			shootCooldown -= Time.deltaTime;
		}
	}

	//--------------------------------
	// 3 - Shooting from another script
	//--------------------------------

	//--------------------------------
	// 4 - Audio
	//--------------------------------

	public AudioSource audioSource;
	public AudioClip laserFireSound;
	
	/// <summary>
	/// Create a new projectile if possible
	/// </summary>
	public void Attack (bool isEnemy)
	{
		if (CanAttack) {
			shootCooldown = shootingRate;

			// Create a new shot
			var shotTransform = Instantiate (shotPrefab) as Transform;

			audioSource.PlayOneShot (laserFireSound);

			Physics2D.IgnoreCollision (shotTransform.GetComponent<Collider2D> (), CPU.GetComponent<Collider2D> ());

			// Assign position
			shotTransform.position = transform.position;

			// The is enemy property
			ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript> ();
			if (shot != null) {
				print (shot);
				shot.isEnemyShot = isEnemy;
			}

			// Make the weapon shot always towards it
			MoveScript move = shotTransform.gameObject.GetComponent<MoveScript> ();
			if (move != null) {
				move.direction = this.transform.up; // towards in 2D space is in front of the sprite
			}
		}
	}

	/// <summary>
	/// Is the weapon ready to create a new projectile?
	/// </summary>
	public bool CanAttack {
		get {
			return shootCooldown <= 0f;
		}
	}
}