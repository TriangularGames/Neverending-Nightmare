using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowBrain : MonoBehaviour
{
    private List<GameObject> crows;
    private bool GameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        crows = new List<GameObject>();
        StartCoroutine("SpawnFlock");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnFlock()
    {
        while (!GameOver) {
            yield return new WaitForSeconds(Random.Range(1f, 1f));
            int flockSize = Random.Range(3, 10);

            List<GameObject> flock = new List<GameObject>();

            Vector2 pos = new Vector2(0, 0);
            Vector2 vel = new Vector2(-1, 0);

            int crowsToGrab = flockSize;
            for (int i = 0; crowsToGrab > 0 && i < flockSize; i++)
            {
                GameObject crow = ObjectPool.SharedInstance.GetPooledObject("Crow");
                flock.Add(crow);

                crow.SetActive(true);

                crow.GetComponent<Rigidbody2D>().transform.position = pos +
                    new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));

                float angle = Mathf.Atan2(vel.y, vel.x) + Random.Range(-1, 1);
                Vector2 velUp = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                crow.GetComponent<Rigidbody2D>().velocity = velUp;

                crowsToGrab--;
            }

            GameObject[] flockArray = flock.ToArray();
            for (int c = 0; c < flockSize; c++)
            {
                flock[c].GetComponent<Crow>().setFlock(flockArray);
            }


            GameOver = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().GameOver;
        }
    }
}
