using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AI_CPU : MonoBehaviour
{
	public GameObject ShipFrontPoint;
	public GameObject ShipRearPoint;
	public GameObject ShipLeftPoint;
	public GameObject ShipRightPoint;
	public GameObject HardpointManager;
	public List<GameObject> Hardpoints = new List<GameObject>();
	private AI_Hardpoints HardpointsAI;
	public GameObject Engines;
	private AI_Engines EngineAI;
	public GameObject Sensors;
	public GameObject LifeSupport;
	private AI_LifeSupport LifeSupportAI;
	public GameObject Marker;
	public int HP;
	public int MaxHP;
	public int Armor;
    public float moveSpeed = 2.0f;
    public float maxSpeed = 4.5f;
	public float Torque;
	public float MaxTorque;

    
    
	private Vector3 mousePos;

    void Awake()
    {
		EngineAI = Engines.GetComponent<AI_Engines> ();
		LifeSupportAI = LifeSupport.GetComponent<AI_LifeSupport> ();
		HardpointsAI = HardpointManager.GetComponent<AI_Hardpoints> ();
        foreach (Transform child in HardpointManager.transform)
        {
            Hardpoints.Add(child.gameObject);
        }
    }

    void Update()
    {
		mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		mousePos.z = 0.0f;
        // ...

        // 5 - Shooting
        bool shoot = Input.GetButton("Fire2");
        // Careful: For Mac users, ctrl + arrow is a bad idea

		HardpointsAI.UseHardpoints (shoot);

		// AUDIO


        // ...
    }

    void FixedUpdate()
    {
        bool move = Input.GetButton("Fire1");
		EngineAI.ForwardThrust (move);

        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
		Gizmos.DrawLine(transform.position, mousePos);
        Gizmos.color = Color.red;
		Gizmos.DrawLine(ShipRearPoint.transform.position, ShipFrontPoint.transform.position);
		Gizmos.color = Color.red;
		Gizmos.DrawLine(ShipLeftPoint.transform.position, ShipRightPoint.transform.position);
        Gizmos.color = Color.green;
		Gizmos.DrawLine(mousePos, ClosestPointOnLine(ShipFrontPoint.transform.position, ShipRearPoint.transform.position, mousePos));

    }

    Vector3 ClosestPointOnLine(Vector3 vA, Vector3 vB, Vector3 vPoint)
    {
        var vVector1 = vPoint - vA;
        var vVector2 = (vB - vA).normalized;

        var d = Vector3.Distance(vA, vB);
        var t = Vector3.Dot(vVector2, vVector1);

        if (t <= 0)
            return vA;

        if (t >= d)
            return vB;

        var vVector3 = vVector2 * t;

        var vClosestPoint = vA + vVector3;

        return vClosestPoint;
    }

}