using System.Collections;
using UnityEngine;

public class Cobweb : MonoBehaviour
{
    public float scrollSpeed;
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeInHierarchy)
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine("Perish");
    }

    IEnumerator Perish()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        StopCoroutine("Perish");
        yield return null;
    }
}
