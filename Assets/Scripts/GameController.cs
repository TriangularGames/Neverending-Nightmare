using System.Collections;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public SpriteRenderer darkness;
    private PickupSpawner pickup;
    private GameObject chase;
    private Flashlight flashlight;

    private PowerUpBar puBar;
    private TextMeshProUGUI pointsText;
    public GameObject RestartMenu;
    public TextMeshProUGUI restartTitle;
    public TextMeshProUGUI restartText;

    private float frequency = 4f;

    private int powerupActivated = 0;
    private int timesSlowed = 0;

    public TextMeshProUGUI totalsText;

    private int pointTotal = 0;
    private bool increaseOccured = false;

    private float waitTime = 4f;

    public bool GameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        //0.7647059
        darkness.color = new Color(0f, 0f, 
            0f, GameObject.FindGameObjectWithTag("DarknessLevel").GetComponent<DarknessTest>().getDarkness());
        puBar = GetComponent<PowerUpBar>();
        pickup = GetComponent<PickupSpawner>();
        flashlight = GameObject.FindGameObjectWithTag("Flashlight").GetComponent<Flashlight>();
        pointsText = GameObject.FindGameObjectWithTag("PointText").GetComponent<TextMeshProUGUI>();
        chase = GameObject.FindGameObjectWithTag("Chaser");
        StartCoroutine("ItemSpawning");
    }

    private void Update()
    {
        if (!increaseOccured && (pointTotal % 5 == 0 && pointTotal != 0))
        {
            increaseOccured = true;
            chase.GetComponent<ChaserController>().speedUp(0.02f);
            if (frequency > 1)
            {
                frequency -= 1;
            }
            
        }

        if (pointTotal > 100)
        {
            EndGame();
            restartTitle.text = "You Win!";
            restartText.text = "You evaded the shadow that haunts your nightmares...but can you do it again?";
        }
    }

    IEnumerator ItemSpawning()
    {
        while (!GameOver)
        {
            pickup.SpawnItem();

            yield return new WaitForSeconds(waitTime + Random.Range(0, frequency));
        }
    }

    public void increasePoints()
    {
        pointTotal++;
        if (pointTotal % 5 != 0){
            increaseOccured = false;
        }
        pointsText.text = ": " + pointTotal;
    }

    internal void slowDown()
    {
        timesSlowed++;
        chase.GetComponent<ChaserController>().speedDown(0.02f);
        frequency += 0.5f;
    }

    public void EndGame()
    {
        totalsText.text = "Points: " + pointTotal + "\nPowers Activated: " + 
            powerupActivated + "\nTimes Slowed: " + timesSlowed;
        GameOver = true;
        chase.GetComponent<ChaserController>().endlife();
        GameObject[] backgrounds = GameObject.FindGameObjectsWithTag("Background");
        RestartMenu.SetActive(true);
        restartTitle.text = "Game Over";
        restartText.text = "The shadowy figure got the better of you it seems...would you care to run again?";

        foreach (GameObject background in backgrounds)
        {
            background.GetComponent<Parallax>().scrollSpeed = 0f;
        }
    }

    public void RestartGame()
    {
        chase.GetComponent<ChaserController>().endlife();
        GameObject[] backgrounds = GameObject.FindGameObjectsWithTag("Background");
        RestartMenu.SetActive(false);

        foreach (GameObject background in backgrounds)
        {
            background.GetComponent<Parallax>().resetSpeed();
        }
    }

    public void increasePowerUp()
    {
        if (!puBar.IsFull())
        {
            flashlight.setBrightness(0.05f);
            puBar.increaseBar();
        }
    }

    public void PowerupActivated()
    {
        powerupActivated++;
    }

    public void PushBack()
    {
        chase.transform.position = new Vector2 (chase.transform.position.x - 3f, chase.transform.position.y);
    }

    public void setWait(int time)
    {
        waitTime = time;
    }
}
