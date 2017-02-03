using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFormation : MonoBehaviour
{
    private Vector3 offset = new Vector3(0.0f, -0.75f, 0.0f);
    private ScoreKeeper scoreKeeper;
    public GameObject projectile;
    public float projectileSpeed;
    public float health;
    public float shotsPerSecond = 0.5f;
    public int scoreValue = 150;
    public AudioClip explode, fireSound;

    void Start()
    {
        scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        Projectile missile = collider.gameObject.GetComponent<Projectile>();
        if (missile)
        {
            health -= missile.GetDamage();
            missile.Hit();
            if (health <= 0)
            {
                Die();
            }
        }
    }
    void Update()
    {
        float probability = shotsPerSecond * Time.deltaTime;
        if (Random.value < probability)
        {
            Fire();
        }

    }
    void Fire()
    {
        GameObject beam = Instantiate(projectile, transform.position + offset, Quaternion.identity) as GameObject;
        beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, -projectileSpeed, 0f);
        AudioSource.PlayClipAtPoint(fireSound, transform.position, 1.0f);
    }
    void Die()
    {
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(explode, transform.position, 0.5f);
        scoreKeeper.Score(scoreValue);
    }
}
