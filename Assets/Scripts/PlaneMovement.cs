using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlaneMovement : MonoBehaviour
{

    public DrawWithMouse drawController;
    Vector3[] positions;
    bool startMovement = false;
    public float speed = 10f;
    int moveIndex = 0;
    bool colliderCheck = false;
    //UnityEngine.Camera cam;

    [SerializeField] public Text ScoreText;
    private int score = 0;

    private void Start()
    {
        //cam = UnityEngine.Camera.main;
    }
    private void OnMouseDrag()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (!GetComponent<Collider2D>().OverlapPoint(mousePosition) || colliderCheck)
        {
            if(!colliderCheck)
            {
                drawController.startLine(transform.position);
            }
            drawController.UpdateLine();
            colliderCheck = true;
            //GetComponent<Collider2D>().IsSceneBound
        }
    }

    private void OnMouseUp()
    {
        positions = new Vector3[drawController.line.positionCount];
        drawController.line.GetPositions(positions);
        startMovement = true;
        moveIndex = drawController.line.positionCount/15;
        if (moveIndex > positions.Length - 1 || !colliderCheck)
        {
            startMovement = false;
        }
        colliderCheck = false;
    }

    void OnBecameInvisible()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    

    
    // Update is called once per frame
    void Update()
    {



        if (startMovement)
        {
           
            
            Vector2 currentpos = positions[moveIndex];
            transform.position = Vector2.MoveTowards(transform.position, currentpos, speed * Time.deltaTime);

            Vector2 dir = currentpos - (Vector2)transform.position;
            float angle = Mathf.Atan2(dir.normalized.y, dir.normalized.x);
            //if()
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg), 0.025f);

            float distance = Vector2.Distance(transform.position, currentpos);
            if (distance < 0.05f)
            {
                moveIndex++;
            }

            if (moveIndex > positions.Length - 1)
            {
                startMovement = false;
               // transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }

        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, 0f), 0.025f);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (collision.gameObject.tag == "Collectible")
        {
            Destroy(collision.gameObject);
            score++;
            ScoreText.text = score.ToString();
        }
        if(collision.gameObject.tag == "Hoop")
        {
            score++;
            ScoreText.text = score.ToString();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.tag == "Collectible")
        //{
        //    Destroy(collision.gameObject);
        //    score++;
        //    ScoreText.text = score.ToString();
        //}
    }
    
}
