using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    static public int NumberOfCollectibles = 4;

    [SerializeField]

    private GameObject ToSpawn;

    //Interval Of Spawning.
    public float SpawnInterval = 2f;
    private float cameraWidth;
    private float cameraHeight;
    public float Ythreshold = 1.0f;
    LinkedList<GameObject> RemovedCollectables = new LinkedList<GameObject>();
    LinkedList<GameObject> CollectablesList = new LinkedList<GameObject>();


    //Gets Random Position From Right Side of The Camera.
    private Vector3 GetRandomPosition()
    {
        //Orthographic Size is Height/2.
        cameraHeight = Camera.main.orthographicSize;

        //To Calculate Width Multiply Height/2 with Aspect (width divided By Height).
        cameraWidth = Camera.main.aspect * cameraHeight;

        //Set The X-position Of Spawn.
        float Xpostion = Camera.main.transform.position.x + cameraWidth + 2f;

        //Get The Random Value of Y-position of Spawn.
        float Yposition = Random.Range(-cameraHeight + Ythreshold, cameraHeight - Ythreshold);

        return new Vector3(Xpostion, Yposition, 0f); //Change this..
    }

    //Spawns The GameObject.
    void SpawnCollectables()
    {
        Vector3 SpawnPosition = GetRandomPosition();
        //  int index = Random.Range(0, NumberOfCollectibles);

        if (ToSpawn.name != "Coin") //If the GameObject Is not a Coin and Does not Overlap.
            Instantiate(ToSpawn, SpawnPosition, ToSpawn.transform.rotation);
        else
        {
            //Place three Coins In A Row. If it is A Coin.
            int count = 0;
            do
            {
                // if (CheckOverlap(index)) break;
                Instantiate(ToSpawn, SpawnPosition, ToSpawn.transform.rotation);
                SpawnPosition.x += 3.5f;
                count++;
            } while (count != 3);
        }
    }

    private void CheckPositionOutsideCamera()
    {
        foreach (GameObject collectable in CollectablesList)
        {
            if (collectable.transform.position.x < Camera.main.transform.position.x - (Camera.main.orthographicSize * Camera.main.aspect) - 2f)
            {
                RemovedCollectables.AddLast(collectable);
            }
        }
        foreach (GameObject remove in RemovedCollectables)
        {
            CollectablesList.Remove(remove);
            Destroy(remove);
        }
    }

    //OverLap Check..
    /*  public bool CheckOverlap(int ind)
      {

          //Call this function to update collider's transform !!
          Physics2D.SyncTransforms();

          //Don't forget to get the colliders size, and not the renderer's !!
          Vector2 boxSize = ToSpawn[ind].GetComponent<Collider2D>().bounds.size;

          var rads = 0; //Default Radians.
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
        if (ToSpawn.name == "Hoop")
        {
            Ythreshold = 2.0f;
        }
        //Keeping On Invoking The Function After Some Interval of Time.
        //ToSpawn = new GameObject[3];
        InvokeRepeating("SpawnCollectables", 0f, SpawnInterval);

    }

    private void Update()
    {
        CheckPositionOutsideCamera();
    }

}