using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class HitController : MonoBehaviour
{
    public float falltime = 1; 
    GameController GameController;

    private void Awake()
    {
        GameController = FindFirstObjectByType<GameController>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {    
        //Check kind of collision - is it the flyswatter?
        if (collision.gameObject.CompareTag("Swatter"))
        {
            gameObject.GetComponent<Collider2D>().enabled = false;

            //Is it a fly?
            if (GetComponent<CurvedPathMover>()) 
            {
                gameObject.GetComponent<CurvedPathMover>().enabled = false;
                GameController.flyHit++;
                StartCoroutine(Hit(falltime));
            }
            //Is it a bee?
            else if (GetComponent<BeeMover>())
            {
                gameObject.GetComponent<BeeMover>().fail = true;
                GameController.beesHits++;
                StartCoroutine(Hit(falltime));
            }
            //Is it a bumblebee?
            else if (GetComponent<BumbleBee>())
            {                 
                GameController.bumbleBeeHits++;

                Debug.Log("Collision! Bumblebee: " + GameController.bumbleBeeHits);

                if (GameController.bumbleBeeHits >= GameController.howManyBumbleBeeHits)
                {
                    gameObject.GetComponent<BumbleBee>().enabled = false;
                    Debug.Log("Collision! Bumblebee: OUT");
                    StartCoroutine(Hit(falltime));
                }
            }
             
        }
        //Is it a fly colliding with a bee
        if (collision.gameObject.CompareTag("Fly"))
        { 
            if (gameObject.CompareTag("Bee"))
            {
                //If is is a bee and NOT success(allready visited the flower) then fail
                if (!gameObject.GetComponent<BeeMover>().success) 
                {               
                    gameObject.GetComponent<BeeMover>().fail = true;
                }
            }
        }
    }

    private IEnumerator Hit(float value)
    {
        if (gameObject != null)
        {
            yield return new WaitForSeconds(value);
            gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
            Destroy(gameObject, value * 2);
        }
    }
}
