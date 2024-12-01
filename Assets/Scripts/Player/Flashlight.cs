using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public Transform pivot;
    private SpriteRenderer spriteR;

    // Start is called before the first frame update
    void Start()
    { 
        spriteR = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        var pos = Camera.main.WorldToScreenPoint(pivot.transform.position);
        var dir = Input.mousePosition - pos;
        var angle = Mathf.Clamp(Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg, -10, 90);
        pivot.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void setBrightness(float increase)
    {
        spriteR.color = new Color (1f,1f,1f,spriteR.color.a + increase);
    }
}
