using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    
    [SerializeField] private GameObject[] ToSpawn;
    LinkedList<GameObject> SpawnedList = new LinkedList<GameObject>();
    LinkedList<GameObject> RemoveAbleObjects = new LinkedList<GameObject>();
    private Vector3 lastSpawn;
    public float spawnDistanceThreshold = 2.0f;
    //private LinkedList<GameObject> Addptr;

    //Interval Of Spawning.
    public float SpawnInterval = 2f;

    //Gets Random Position From Right Side of The Camera.
    private Vector2 GetRandomPosition()
    {
        //Orthographic Size is Height/2.
        float cameraHeight = Camera.main.orthographicSize;

        //To Calculate Width Multiply Height/2 with Aspect (width divided By Height).
        float cameraWidth = Camera.main.aspect * cameraHeight;

        //Set The X-position Of Spawn.
        float Xpostion = Camera.main.transform.position.x + cameraWidth + 2f;

        //Get The Random Value of Y-position of Spawn.
        float Yposition = Random.Range(-cameraHeight + 1f, cameraHeight - 1f);

        return new Vector2(Xpostion, Yposition);
    }

    //Spawns The GameObject.

    void Spawn()
    {
        Vector2 SpawnPosition;
        do
        {
            SpawnPosition = GetRandomPosition();
        }
        while (Vector2.Distance(SpawnPosition, lastSpawn) < spawnDistanceThreshold);
        int index = Random.Range(0, ToSpawn.Length);
        
        SpawnedList.AddLast(Instantiate(ToSpawn[index], SpawnPosition, ToSpawn[index].transform.rotation));

        lastSpawn = SpawnPosition;
    }

    private void UpdatePositions()
    {

        foreach (GameObject ObjectSpawned in SpawnedList)
        {
            ObjectSpawned.transform.position += (Vector3.left * 2.5f) * Time.deltaTime;
            if (ObjectSpawned.transform.position.x < Camera.main.transform.position.x - (Camera.main.orthographicSize * Camera.main.aspect) - 2f)
            {
                RemoveAbleObjects.AddLast(ObjectSpawned);
            }
        }

        foreach (GameObject remove in RemoveAbleObjects)
        {
            SpawnedList.Remove(remove);
            Destroy(remove);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }
    /*  public bool CheckOverlap()
      {

          //Call this function to update collider's transform !!
          Physics2D.SyncTransforms();

          //Don't forget to get the colliders size, and not the renderer's !!
          Vector2 boxSize = ToSpawn.GetComponent<Collider2D>().bounds.size;

          var rads = 1; //Default Radians.
          //Angle is in degrees !!
          float angle = rads * Mathf.Rad2Deg;

          var colliders = Physics2D.OverlapBoxAll(transform.position, boxSize, angle);


          if (colliders.Length == 0)
              return false;

          //check if collider is not the current's gameobject collider !!
          else if (colliders.Length == 1)
              if (colliders[0].gameObject == gameObject)
                  return false;

          return true;
      } */

    void Start()
    {
        //Keeping On Invoking The Function After Some Interval of Time.
        InvokeRepeating("Spawn", Random.Range(3f, 10f), SpawnInterval);
    }

    void Update()
    {
        UpdatePositions();
    }
}