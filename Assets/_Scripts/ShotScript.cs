using UnityEngine;
using System.Collections;

/// <summary>
/// Projectile behavior
/// </summary>
public class ShotScript : MonoBehaviour
{
    // 1 - Designer variables

    /// <summary>
    /// Damage inflicted
    /// </summary>
    public int damage = 1;

    /// <summary>
    /// Projectile damage player or enemies?
    /// </summary>
    public bool isEnemyShot = false;

    void Start()
    {

    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D otherCollider)
    {
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0f, 0f, 0f), 0.5f);
        float particles = GetComponent<ParticleSystem>().startSize;
        particles = Mathf.Lerp(particles, 0f, 0.2f);
        Destroy(gameObject, 0.5f); // Remember to always target the game object, otherwise you will just remove the script
        
    }
}