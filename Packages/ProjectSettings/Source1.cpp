
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    static public int NumberOfCollectibles = 5;

    [SerializeField]

    private GameObject[] ToSpawn = new GameObject[NumberOfCollectibles];

    //Interval Of Spawning.
    public float SpawnInterval = 2f;



    //Gets Random Position From Right Side of The Camera.
    private Vector3 GetRandomPosition()
    {
        //Orthographic Size is Height/2.
        float cameraHeight = Camera.main.orthographicSize;

        //To Calculate Width Multiply Height/2 with Aspect (width divided By Height).
        float cameraWidth = Camera.main.aspect * cameraHeight;

        //Set The X-position Of Spawn.
        float Xpostion = Camera.main.transform.position.x + cameraWidth + 2f;

        //Get The Random Value of Y-position of Spawn.
        float Yposition = Random.Range(-cameraHeight + 1f, cameraHeight - 1f);

        return new Vector3(Xpostion, Yposition, -1f);
    }

    //Spawns The GameObject
    void Spawn()
    {
        Vector3 SpawnPosition = GetRandomPosition();
        int index = Random.Range(0, 5);
        Instantiate(ToSpawn[index], SpawnPosition, ToSpawn[index].transform.rotation);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    void Start()
    {
        //Keeping On Invoking The Function After Some Interval of Time.
        //ToSpawn = new GameObject[3];
        InvokeRepeating("Spawn", 0f, SpawnInterval);
    }

}




























//old obstacleSpawner
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{

    [SerializeField] private GameObject ToSpawn;

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
        Vector2 SpawnPosition = GetRandomPosition();
        Instantiate(ToSpawn, SpawnPosition, ToSpawn.transform.rotation);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    void Start()
    {
        //Keeping On Invoking The Function After Some Interval of Time.
        InvokeRepeating("Spawn", Random.Range(3f, 10f), SpawnInterval);
    }

}

