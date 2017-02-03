using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    // Public variables.
    public GameObject projectile;
    public LevelManager levelManager;
    public float health = 10f;
    public float padding = 0.55f;
    public float speed, projectileSpeed, firingRate;
    public AudioClip fireSound, lose;

    // Private variables.
    private Vector3 offset = new Vector3(0f, 0.75f, 0f);
    private float x_Min = -6.0f;
    private float x_Max = 6.0f;
    private float y_Min = -4.5f;
    private float y_Max = -2.5f;
    // Update is called once per frame
    void Start()
    {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        Vector3 upMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, distance));
        Vector3 downMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        x_Min = leftMost.x + padding;
        x_Max = rightMost.x - padding;
        y_Min = downMost.y + padding;
        y_Max = upMost.y - padding - 6;
    }
    void Fire()
    {
        GameObject beam = Instantiate(projectile, transform.position + offset, Quaternion.identity) as GameObject;
        beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, projectileSpeed * Time.deltaTime, 0f);
        AudioSource.PlayClipAtPoint(fireSound, transform.position, 1.0f);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire", 0.00001f, firingRate);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += Vector3.up * (speed / 1.5f) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += Vector3.down * (speed / 1.5f) * Time.deltaTime;
        }
        // Restrict player to gamespace.
        float new_X = Mathf.Clamp(transform.position.x, x_Min, x_Max);
        float new_Y = Mathf.Clamp(transform.position.y, y_Min, y_Max);
        transform.position = new Vector3(new_X, transform.position.y, transform.position.z);
        transform.position = new Vector3(transform.position.x, new_Y, transform.position.z);
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
                Destroy(gameObject);
                AudioSource.PlayClipAtPoint(lose, transform.position, 1.0f);
                levelManager.LoadNextLevel();
            }
        }
    }
}