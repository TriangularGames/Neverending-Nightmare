using UnityEngine;

public class OpacityEditor : MonoBehaviour
{
    public float transparency;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, transparency);
    }
}
