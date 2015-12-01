using UnityEngine;
using System.Collections;

public class WorldGenerator : MonoBehaviour
{

	public GameObject[] Asteroids;

	//the radius you want GameObjects to spawn in
	public float radius;
	private Vector3 newPos;
	private Collider2D[] otherObjects;
	public float spawnFrequency;
	public float distanceFromObjects = 1;
	public float despawnDistance;

	Vector3 FindNewPos ()
	{
		do {
			newPos = new Vector3 (transform.position.x + Random.Range (-radius, radius), transform.position.y + Random.Range (-radius, radius), 0);
			otherObjects = Physics2D.OverlapCircleAll (newPos, distanceFromObjects);
		} while (otherObjects.Length > 0);
		return newPos;
	}

	void Start ()
	{

		InvokeRepeating ("SpawnObject", 0.1f, spawnFrequency);
	}

	void SpawnObject ()
	{
		newPos = FindNewPos ();
		GameObject asteroid = GameObject.Instantiate (Asteroids [Random.Range (0, Asteroids.Length)], newPos, Quaternion.identity) as GameObject;
	}

	void OnDrawGizmos ()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawLine (transform.position, newPos);

	}
}
