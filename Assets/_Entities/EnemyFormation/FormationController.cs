using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationController : MonoBehaviour
{
    // Public variables.
    public GameObject enemyPrefab;
    public float width = 10f;
    public float height = 5f;
    public float speed = 5f;
    public float spawnDelay = 0.5f;

    // Private variables.
    private bool movingRight = false;
    private float xMin;
    private float xMax;

    // Use this for initialization
    void Start()
    {
        float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, distanceToCamera));
        Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, distanceToCamera));
        xMax = rightBoundary.x;
        xMin = leftBoundary.x;
        Spawn();
    }

    // Draws a Gizmo to help with development.
    public void OnDrawGizmo()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }

    // Spawns a new formation of enemies.
    void Spawn()
    {
        foreach (Transform child in transform)
        {
            GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = child;
        }
    }

    // Spawns enemies until formation is full.
    private void SpawnUntilFull()
    {
        Transform freePosition = NextFreePosition();
        if (freePosition)
        {
            GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = freePosition;
        }
        if (NextFreePosition())
        {
            Invoke("SpawnUntilFull", spawnDelay);
        }
    }

    // Returns the next position free in a formation.
    private Transform NextFreePosition()
    {
        foreach (Transform childPositionGameObject in transform)
        {
            if (childPositionGameObject.childCount == 0)
            {
                return childPositionGameObject;
            }
        }
        return null;
    }

    // Returns true if all enemies are dead.
    private bool AllMembersDead()
    {
        foreach (Transform childPositionGameObject in transform)
        {
            if (childPositionGameObject.childCount > 0)
            {
                return false;
            }
        }
        return true;
    }

    // Update is called once per frame.
    void Update()
    {
        if (movingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        float rightEdgeOfFormation = transform.position.x + (0.5f * width);
        float leftEdgeOfFormation = transform.position.x - (0.5f * width);
        if (leftEdgeOfFormation <= xMin)
        {
            movingRight = true;
        }
        else if (rightEdgeOfFormation > xMax)
        {
            movingRight = false;
        }
        if (AllMembersDead())
        {
            Debug.Log("Empty Formation");
            SpawnUntilFull();
        }
    }
}