using UnityEngine;
using System.Collections;

public class AI_Hardpoints : MonoBehaviour {

	public AI_CPU CPU;
	AudioSource audioSource;

	void Awake()
	{
		CPU = transform.root.gameObject.GetComponent<AI_CPU> ();
		audioSource = GetComponent<AudioSource> ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UseHardpoints(bool usingHardpoints)
	{
		if (usingHardpoints)
		{
			foreach (GameObject hardpoint in CPU.Hardpoints)
			{
				WeaponScript weapon = hardpoint.GetComponent<WeaponScript>();
				if (weapon != null)
				{
					// false because the player is not an enemy
					weapon.Attack(false);
				}
				
			}
		}
	}
}
