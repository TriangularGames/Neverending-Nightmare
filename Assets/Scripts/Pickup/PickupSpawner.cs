using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    public GameObject[] pickUps;
    public GameObject[] spawners;

    public float minDis;
    public float maxDis;

    int randomItem;
    int randomSpawner;

    public void SpawnItem()
    {
        float disBoyStalker = Mathf.Abs(Vector2.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, 
            GameObject.FindGameObjectWithTag("Chaser").transform.position));

        float percent = Mathf.Clamp(((disBoyStalker - minDis) / (maxDis - minDis)), 0.0f, 1.0f);

        //Chooses a random powerup
        int prob = Random.Range(0, 100);
        randomItem = (prob < 15 * percent) ? 2 : (prob < 80 * percent) ? 0 : 1;
        
        randomSpawner = Random.Range(0, spawners.Length);

        Vector2 spawnPos = spawners[randomSpawner].transform.position;

        //Spawns item
        if (pickUps[randomItem].tag == "PowerUp")
        {
            GameObject powerUp = ObjectPool.SharedInstance.GetPooledObject("PowerUp");
            if (powerUp != null)
            {
                powerUp.transform.position = spawnPos;
                powerUp.SetActive(true);
            }
            GameObject cobWeb = ObjectPool.SharedInstance.GetPooledObject("Cobweb");
            if (cobWeb != null)
            {
                cobWeb.transform.position = spawnPos;
                cobWeb.SetActive(true);
            }
        }
        else if (pickUps[randomItem].tag == "PickUp")
        {
            GameObject pickUp = ObjectPool.SharedInstance.GetPooledObject("PickUp");
            if (pickUp != null)
            {
                pickUp.transform.position = spawnPos;
                pickUp.SetActive(true);
            }
            GameObject cobWeb = ObjectPool.SharedInstance.GetPooledObject("Cobweb");
            if (cobWeb != null)
            {
                cobWeb.transform.position = spawnPos;
                cobWeb.SetActive(true);
            }
        }
        else if (pickUps[randomItem].tag == "SlowDown")
        {
            GameObject slowDown = ObjectPool.SharedInstance.GetPooledObject("SlowDown");
            if (slowDown != null)
            {
                slowDown.transform.position = spawnPos;
                slowDown.SetActive(true);
            }
            GameObject cobWeb = ObjectPool.SharedInstance.GetPooledObject("Cobweb");
            if (cobWeb != null)
            {
                cobWeb.transform.position = spawnPos;
                cobWeb.SetActive(true);
            }
        }
    }
}
