using UnityEngine;
using System.Collections;

public class DrawOnPointSphere : MonoBehaviour {
	
	public Transform ThisTransform;
	
	void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(ThisTransform.position, (transform.localScale.x / 2));
	}
}