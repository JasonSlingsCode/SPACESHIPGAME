using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AI_Engines : MonoBehaviour
{
	public AI_CPU CPU;
	Vector2 target;
	bool rotateToTarget;
	bool travelToTarget;
	public List<GameObject> Thrusters = new List<GameObject> ();
	Plane groundPlane;
	Vector3 MousePosition;
	AudioSource audioSource;

	void Awake ()
	{
		CPU = transform.root.gameObject.GetComponent<AI_CPU> ();
		audioSource = GetComponent<AudioSource> ();
		foreach (Transform child in transform) {
			Thrusters.Add (child.gameObject);
		}
	}
	
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void FixedUpdate ()
	{
		groundPlane = new Plane (transform.position, CPU.ShipFrontPoint.transform.position, CPU.ShipRightPoint.transform.position);
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		float rayDistance;
		if (groundPlane.Raycast (ray, out rayDistance))
			CPU.Marker.transform.position = ray.GetPoint (rayDistance);
		
		MousePosition = CPU.Marker.transform.position;
		
		float distance = Vector3.Distance (MousePosition, ClosestPointOnLine (CPU.ShipRearPoint.transform.position, CPU.ShipFrontPoint.transform.position, MousePosition));
		float distanceLeft = Vector3.Distance (CPU.ShipLeftPoint.transform.position, MousePosition);
		float distanceRight = Vector3.Distance (CPU.ShipRightPoint.transform.position, MousePosition);
		
		
		if (distanceLeft < distanceRight) {
			// LEFT
			CPU.gameObject.transform.RotateAround (CPU.gameObject.transform.position, ray.direction, CPU.Torque * distance * Time.deltaTime);
			if (distance > 0) {
				foreach (GameObject go in Thrusters) {
					if (go.name == "LeftThruster")
						go.SetActive (false);
					if (go.name == "RightThruster")
						go.SetActive (true);
				}
			}
		}
		if (distanceLeft > distanceRight) {
			// RIGHT
			CPU.gameObject.transform.RotateAround (CPU.gameObject.transform.position, ray.direction, -CPU.Torque * distance * Time.deltaTime);
			if (distance > 0) {
				foreach (GameObject go in Thrusters) {
					if (go.name == "LeftThruster")
						go.SetActive (true);
					if (go.name == "RightThruster")
						go.SetActive (false);
				}
			}
		}
		if (distance < 1) {
			foreach (GameObject go in Thrusters) {
				if (go.name == "LeftThruster")
					go.SetActive (false);
				if (go.name == "RightThruster")
					go.SetActive (false);
			}
		}
	}
	
	Vector3 ClosestPointOnLine (Vector3 vA, Vector3 vB, Vector3 vPoint)
	{
		var vVector1 = vPoint - vA;
		var vVector2 = (vB - vA).normalized;
		
		var d = Vector3.Distance (vA, vB);
		var t = Vector3.Dot (vVector2, vVector1);
		
		if (t <= 0)
			return vA;
		
		if (t >= d)
			return vB;
		
		var vVector3 = vVector2 * t;
		
		var vClosestPoint = vA + vVector3;
		
		return vClosestPoint;
	}

	public void ForwardThrust (bool movingForward)
	{
		if (movingForward) {
			var mouseClick = Input.mousePosition;
			mouseClick.z = transform.position.z - Camera.main.transform.position.z;
			target = CPU.ShipFrontPoint.transform.position;
			rotateToTarget = false;
			travelToTarget = true;
			//print("NEW TARGET: " + target);
			// if (AudioSource.isPlaying) return;
			audioSource.volume = 1;
		} else if (!movingForward) {
			travelToTarget = false;
			foreach (GameObject go in Thrusters) {
				if (go.name == "MainThruster")
					go.SetActive (false);
			}
			audioSource.volume = 0;
		}
		
		if (rotateToTarget == false && travelToTarget == true) {
			
			var distanceToTarget = Vector2.Distance (CPU.gameObject.transform.position, target);
			//print("Distance: " + distanceToTarget);
			if (distanceToTarget > 1) {
				Vector2 direction = (target - new Vector2 (CPU.gameObject.transform.position.x, CPU.gameObject.transform.position.y)).normalized;
				CPU.gameObject.GetComponent<Rigidbody2D> ().velocity = Vector2.ClampMagnitude (CPU.gameObject.GetComponent<Rigidbody2D> ().velocity, CPU.maxSpeed);
				CPU.gameObject.GetComponent<Rigidbody2D> ().AddForce (direction * CPU.moveSpeed, ForceMode2D.Force);
				foreach (GameObject go in Thrusters) {
					if (go.name == "MainThruster")
						go.SetActive (true);
				}
				audioSource.volume = 1;
			} else if (distanceToTarget <= 1) {
				foreach (GameObject go in Thrusters) {
					if (go.name == "MainThruster")
						go.SetActive (false);
				}
				audioSource.volume = 0;
			}
		}
	}
}
