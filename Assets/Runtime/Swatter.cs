using System.Collections;
using UnityEngine;

public class Swatter : MonoBehaviour
{
    public GameObject flyswatter;
    public Vector2 flyswatterOffset;
    private AudioSource flyswatterAudioSource;
    GameController GameController;

    private void Start()
    {
        GameController = FindFirstObjectByType<GameController>();
        flyswatter.GetComponent<BoxCollider2D>().enabled = false;
        flyswatter.GetComponent<SpriteRenderer>().enabled = false;
        flyswatterAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        flyswatter.transform.position = mousePos2D + flyswatterOffset;

        if (Input.touchCount > 0)
        {
            if (Input.touches[0].tapCount == 1)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    flyswatter.GetComponent<SpriteRenderer>().enabled = true;
                    flyswatterAudioSource.Play();
                    flyswatter.transform.localScale /= 1.5f;
                    KillFlyUnderMouse();
                    flyswatter.GetComponent<BoxCollider2D>().enabled = true;
                }
                else if ((Input.GetTouch(0).phase == TouchPhase.Ended))
                {
                    flyswatter.GetComponent<SpriteRenderer>().enabled = false;
                    flyswatter.transform.localScale *= 1.5f;
                    flyswatter.GetComponent<BoxCollider2D>().enabled = false;
                }
            }
        }
    }

    void KillFlyUnderMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        flyswatter.transform.position = mousePos2D + flyswatterOffset;
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, GetComponent<BoxCollider2D>().size, 0, Camera.main.transform.forward);
        if (hit && hit.collider.gameObject.GetComponent<CurvedPathMover>() != null)
        {
            hit.collider.gameObject.GetComponent<CurvedPathMover>().enabled = false;
            StartCoroutine(FlyHit(hit.collider.gameObject));
        }
        if (hit && hit.collider.gameObject.GetComponent<BeeMover>() != null)
        {
            hit.collider.gameObject.GetComponent<BeeMover>().enabled = false;
            StartCoroutine(BeeHit(hit.collider.gameObject));
        }

    }

    private IEnumerator FlyHit(GameObject gameObject)
    {
        if (gameObject != null)
        {
            yield return new WaitForSeconds(1);
            gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
            Destroy(gameObject, 3f);
        }
    }

    private IEnumerator BeeHit(GameObject gameObject)
    {
        if (gameObject != null)
        {
            yield return new WaitForSeconds(1);
            gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
            Destroy(gameObject, 3f);
            GameController.beesHit = GameController.beesHit + 1;
        }
    }
}
