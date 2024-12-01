using System.Collections;
using UnityEngine;

public class ChaserController : MonoBehaviour
{
    private GameObject player;
    private AudioSource audioS;
    private AudioSource music;

    private float speed = 0.06f;
    private float startPos;

    private bool visible;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        audioS = GetComponent<AudioSource>();
        music = GameObject.FindGameObjectWithTag("GameController").GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine("Particles");
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, -1.29f),new Vector2(player.transform.position.x, -5.76f), speed * Time.deltaTime);
    }

    IEnumerator Particles()
    {
        while (speed != 0)
        {
            yield return new WaitForSeconds(Random.Range(0, 10) + 5f);
            if (visible)
            {
                audioS.Play();
                GetComponentInChildren<ParticleSystem>().Play();
            }
        }
        yield return null;
    }

    public void speedUp(float increase)
    {
        speed += increase;
        music.pitch += 0.06f;
    }

    public void speedDown(float decrease)
    {
        speed -= decrease;
        music.pitch -= 0.06f;
    }

    public void resetlife()
    {
        speed = 0.06f;
        transform.position = new Vector2(startPos, -1.29f);
    }

    public void endlife()
    {
        speed = 0f;
    }

    void OnBecameInvisible()
    {
        visible = false;
    }
    void OnBecameVisible()
    {
        visible = true;
    }
}
