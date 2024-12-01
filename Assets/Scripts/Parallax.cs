using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float scrollSpeed;
    private float speed;

    public float Xend;
    public float Xstart;

    private void Start()
    {
        speed = scrollSpeed;
    }

    public void resetSpeed()
    {
        scrollSpeed = speed;
    }

    private void Update()
    {
        transform.Translate(Vector2.left * scrollSpeed * Time.deltaTime);
        
        if (transform.position.x < Xend)
        {
            Vector2 pos = new Vector2(Xstart, transform.position.y);
            transform.position = pos;
        }
    }
}
