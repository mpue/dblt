using System.Collections;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class FlyKiller : MonoBehaviour
{
    public Sprite deadSprite;
    public GameObject klatsche;
    public Vector2 klatscheOffset;
    private AudioSource klatscheAudioSource;

    private void Start()
    {
        klatsche.GetComponent<BoxCollider2D>().enabled = false;
        klatsche.GetComponent<SpriteRenderer>().enabled = false;
        klatscheAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        klatsche.transform.position = mousePos2D + klatscheOffset;

        if (Input.touchCount > 0)
        {
            if (Input.touches[0].tapCount == 1)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    klatsche.GetComponent<SpriteRenderer>().enabled = true;
                    klatscheAudioSource.Play();
                    klatsche.transform.localScale /= 1.5f;  
                    KillFlyUnderMouse();
                    klatsche.GetComponent<BoxCollider2D>().enabled = true;
                }
                else if ((Input.GetTouch(0).phase == TouchPhase.Ended))
                {
                    klatsche.GetComponent<SpriteRenderer>().enabled = false;
                    klatsche.transform.localScale *= 1.5f;
                    klatsche.GetComponent<BoxCollider2D>().enabled = false;
                }
            }
        }
    }

    void KillFlyUnderMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        klatsche.transform.position = mousePos2D + klatscheOffset;
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, GetComponent<BoxCollider2D>().size, 0, Camera.main.transform.forward);
        if (hit && hit.collider.gameObject.GetComponent<CurvedPathMover>() != null)
        {
            hit.collider.gameObject.GetComponent<CurvedPathMover>().enabled = false;
            StartCoroutine(FlyDie(hit.collider.gameObject));
        }

    }


    private IEnumerator FlyDie(GameObject gameObject)
    {
        yield return new WaitForSeconds(1);
        gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        Destroy(gameObject, 3f);
    }

}
