using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class HitController : MonoBehaviour
{
    public float falltime = 1;
    LevelController LevelController;

    private void Start()
    {
        LevelController = FindFirstObjectByType<LevelController>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    { 
        Debug.Log("Collision!");

        //Check kind of collision - is it the flyswatter?
        if (collision.gameObject.CompareTag("Swatter"))
        {
            gameObject.GetComponent<Collider2D>().enabled = false;

            //Is it a fly?
            if (GetComponent<CurvedPathMover>()) 
            {
                gameObject.GetComponent<CurvedPathMover>().enabled = false;
                LevelController.flyHit = LevelController.flyHit + 1;
            }
            //Is it a bee?
            else if (GetComponent<BeeMover>())
            {
                gameObject.GetComponent<BeeMover>().enabled = false;
                LevelController.beesHit = LevelController.beesHit + 1;
            }
            //Is it a blublebee?
            else if (GetComponent<BumblebeeSpawner>())
            {
                gameObject.GetComponent<BumblebeeSpawner>().enabled = false;
                LevelController.bumbleBeeHit = true;
            }
            //In any case, it is definitiv a hit
            StartCoroutine(Hit(falltime));
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
            Destroy(gameObject, 3f);
        }
    }
}
