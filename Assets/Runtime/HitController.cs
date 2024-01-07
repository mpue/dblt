using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class HitController : MonoBehaviour
{
    public float falltime = 1;

    void OnCollisionEnter2D(Collision2D collision)
    { 
        Debug.Log("Collision!");

        //Check kind of Collision
        if (collision.gameObject.CompareTag("Swatter"))
        {
            Debug.Log("Collision with Swatter!");
            if (GetComponent<CurvedPathMover>()) 
            {
                gameObject.GetComponent<CurvedPathMover>().enabled = false;
            }
            else if (GetComponent<BeeMover>())
            {
                gameObject.GetComponent<BeeMover>().enabled = false;
            }
            else if (GetComponent<BumblebeeSpawner>())
            {
                gameObject.GetComponent<BumblebeeSpawner>().enabled = false;               
            }
            StartCoroutine(Hit(falltime));
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
