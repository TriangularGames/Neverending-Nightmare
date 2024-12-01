using System.Collections;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    public float scrollSpeed;
    public ItemPickup pickup;
    private ParticleSystem ps;
    private AudioSource audioS;
    private Camera cam;
    private bool Collected = false;

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        audioS = GetComponent<AudioSource>();
        ps = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            if (!GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().GameOver)
            {
                if (cam.WorldToScreenPoint(transform.position).x > 0)
                {
                    transform.Translate(Vector2.left * scrollSpeed * Time.deltaTime);
                }
                else
                {
                    gameObject.SetActive(false);
                }
                
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !Collected)
        {
            Collected = true;
            if (pickup.type == "PowerUp")
            {
                audioS.Play();
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().increasePowerUp();
            }
            else if (pickup.type == "PickUp")
            {
                audioS.Play();
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().increasePoints();
            }
            else if (pickup.type == "SlowDown")
            {
                audioS.Play();
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().slowDown();
            }
            StartCoroutine("Perish");
            
        }
    }

    IEnumerator Perish()
    {
        ps.Play();
        yield return new WaitForSeconds(0.5f);
        Collected = false;
        gameObject.SetActive(false);
        StopCoroutine("Perish");
        yield return null;
    }
}
