using System.Collections;
using UnityEngine;

public class PowerUpBar : MonoBehaviour
{
    public RectTransform mask;
    public RectTransform barFill;
    public AudioSource audioS;

    private Flashlight flashlight;

    private Vector2 ogPos;
    private float ogWidth;
    private float widthMax = 430f;

    private bool DecreaseBarOccurring = false;

    // Start is called before the first frame update
    void Start()
    {
        flashlight = GameObject.FindGameObjectWithTag("Flashlight").GetComponent<Flashlight>();
        ogPos = barFill.position;
        ogWidth = mask.sizeDelta.x * mask.localScale.x;
    }

    public void increaseBar()
    {
        if (mask.sizeDelta.x < widthMax)
        {
            if (DecreaseBarOccurring)
            {
                DecreaseBarOccurring = false;
                StopCoroutine("DecreaseBar");
                mask.sizeDelta = new Vector2(ogWidth, mask.sizeDelta.y);
                barFill.position = ogPos;
            }
            mask.sizeDelta = new Vector2(mask.sizeDelta.x + 50, mask.sizeDelta.y);
            barFill.position = ogPos;
        }
    }

    public bool IsFull()
    {
        return mask.sizeDelta.x >= widthMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && (mask.sizeDelta.x > widthMax && !DecreaseBarOccurring))
        {
            DecreaseBarOccurring = true;
            //Maybe play a sound when this occurs?
            StartCoroutine("DecreaseBar");
        }
    }

    IEnumerator DecreaseBar()
    {
        audioS.Play();
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().PowerupActivated();
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().PushBack();
        while (mask.sizeDelta.x != ogWidth)
        {
            flashlight.setBrightness(-0.05f);
            mask.sizeDelta = new Vector2(mask.sizeDelta.x - 50, mask.sizeDelta.y);
            barFill.position = ogPos;
            yield return new WaitForSeconds(0.5f);
        }
        DecreaseBarOccurring = false;
        StopCoroutine("DecreaseBar");
        yield return null;
    }
}
