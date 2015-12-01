using UnityEngine;
using System.Collections;

/// <summary>
/// Handle hitpoints and damages
/// </summary>
public class AI_LifeSupport : MonoBehaviour
{

	public AI_CPU CPU;
	private GameObject thePlayer;
	/// <summary>
	/// Total hitpoints
	/// </summary>
	private int hp;
	private int maxHp;
	
	/// <summary>
	/// Enemy or player?
	/// </summary>
	public bool isEnemy = true;
	SpriteRenderer sprite;
	public GameObject deathExplosion;

	
	void Start ()
	{
		CPU = transform.root.gameObject.GetComponent<AI_CPU> ();
		hp = CPU.HP;
		maxHp = CPU.MaxHP;
		thePlayer = GameObject.FindGameObjectWithTag ("Player");
		sprite = transform.root.gameObject.GetComponent<SpriteRenderer> ();
		
	}
	
	void Update ()
	{
		float x = (float)CPU.HP / (float)CPU.MaxHP;
		x = 1 - x;
		sprite.color = new Color ((2.0f * x), (2.0f * (1 - x)), 0);
	}
	
	/// <summary>
	/// Inflicts damage and check if the object should be destroyed
	/// </summary>
	/// <param name="damageCount"></param>
	public void Damage (int damageCount)
	{
		hp -= damageCount;
		
		if (hp <= 0) {
			// Dead!
			var Explosion = GameObject.Instantiate (deathExplosion, transform.position, Quaternion.identity) as GameObject;
			Explosion.GetComponent<Detonator> ().size *= (Mathf.CeilToInt (transform.localScale.x * 1.5f) + Mathf.CeilToInt (transform.localScale.y * 1.5f));
			float distToPlayer = Vector3.Distance (transform.position, thePlayer.transform.position);
			Destroy (transform.root.gameObject);
		}
	}
	
	void OnCollisionEnter2D (Collision2D otherCollider)
	{
		// Is this a shot?
		ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript> ();
		if (shot == null)
			print ("NULL");
		if (shot != null) {
			// Avoid friendly fire
			if (shot.isEnemyShot != isEnemy) {
				Damage (shot.damage);
				print ("DAMAGE");
				// Destroy the shot
				Destroy (shot.gameObject); // Remember to always target the game object, otherwise you will just remove the script
			}
		}
	}
}