using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HoopCollision : MonoBehaviour
{
    private ChildHoop UpperLower;
    private ChildHoop Middle;

    public float deleteDelay = 2.0f; // Delay in seconds
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        // subscribe collision events

        UpperLower = transform.Find("UpperLowerCollider").GetComponent<ChildHoop>();
        UpperLower.OnTriggerEnter2D_Action += UpperLowerTrigger;

        Middle = transform.Find("MiddleCollider").GetComponent<ChildHoop>();
        Middle.OnTriggerExit2D_Action += MiddleTrigger;
    }

    private void UpperLowerTrigger(Collider2D collision)
    {
        if(collision.tag == "Player")
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void MiddleTrigger(Collider2D collision)
    {
        if (collision.tag == "Player")
            Destroy(gameObject, deleteDelay);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
