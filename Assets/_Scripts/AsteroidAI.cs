using UnityEngine;
using System.Collections;

public class AsteroidAI : MonoBehaviour
{
	public WorldGenerator WorldGen;
	public float minSize;
	public float maxSize;

	public float minTorque;
	public float maxTorque;

	public float minForce;
	public float maxForce;
	
	private Rigidbody2D rb;
	private GameObject player;

	private SpriteRenderer sprite;
	private HealthScript hpScript;

	void Awake ()
	{
		hpScript = GetComponent<HealthScript> ();
		sprite = GetComponent<SpriteRenderer> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		WorldGen = GameObject.Find ("WorldGenerator").GetComponent<WorldGenerator>();
		float size = Random.Range (minSize, maxSize);
		float torque = Random.Range (minTorque, maxTorque);
		float force = Random.Range (minForce, maxForce);
		transform.Rotate(0.0f, 0.0f, Random.Range(0.0f, 360.0f));
		transform.localScale = new Vector3 (size, size, size);
		rb = GetComponent<Rigidbody2D> ();
		rb.mass = size * 2000;
		rb.AddTorque (torque, ForceMode2D.Impulse);
		rb.AddForce (transform.up * force, ForceMode2D.Impulse);
		hpScript.maxHp = Mathf.RoundToInt(30 * size);
		hpScript.hp = Mathf.RoundToInt(hpScript.maxHp - Random.Range (0, hpScript.maxHp));
	}
	// Use this for initialization
	void Start ()
	{
		InvokeRepeating ("KillIdle", 1f, 1f);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void KillIdle()
	{
		float distance = Vector3.Distance (transform.position, player.transform.position);
		if (distance > WorldGen.despawnDistance)
			Destroy (gameObject, 0.1f);
	}


}
