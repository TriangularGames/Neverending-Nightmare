using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crow : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Animator anim;
    private Camera cam;

    private List<GameObject> flock;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        flock = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (flock.Count > 0 && gameObject.activeInHierarchy)
        {
            //rb2d.velocity = new Vector2(1, 0);
        }

        if (cam.WorldToScreenPoint(transform.position).x > 0)
        {
            //do stuff
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void setFlock(GameObject[] flock)
    {
        for (int i =0; i < flock.Length; i++)
        {
            //We assume that the crows already have a pos & vel
            if (flock[i] != gameObject)
            {
                this.flock.Add(flock[i]);
            }
        }
    }

    private void removeFlock()
    {
        flock.Clear();
    }
}
