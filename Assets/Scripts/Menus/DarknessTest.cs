using UnityEngine;
using UnityEngine.UI;

public class DarknessTest : MonoBehaviour
{
    private static DarknessTest instance;
    private float darknessLevel = 0.7647059f;

    private void Awake()
    {
        instantiateDarkness();
    }

    private void instantiateDarkness()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (this != instance)
        {
            Destroy(this.gameObject);
        }
    }

    public void setDarkness(float sliderValue)
    {
        darknessLevel = sliderValue;
        Image image = GameObject.FindGameObjectWithTag("DarknessTest").GetComponent<Image>();
        image.color = new Color(0f, 0f, 0f, darknessLevel);
    }

    public float getDarkness()
    {
        return darknessLevel;
    }
}
