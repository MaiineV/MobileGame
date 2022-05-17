using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    bool canSpawn = true;
    bool isRunning = false;

    public float timer;
    public float minTimer;
    public float timerSpeed;

    public List<GameObject> posibleObstacles;
    public List<Transform> posibleSpawns;

    private void Start()
    {
        EventManager.Subscribe("ChangeBool", ChangeBool);
    }

    void Update()
    {
        if (canSpawn && isRunning)
        {
            canSpawn = false;
            StartCoroutine(SpawnObstacles());
        }
    }

    protected void ChangeBool(params object[] parameter)
    {
        isRunning = !isRunning;
    }

    IEnumerator SpawnObstacles()
    {
        yield return new WaitForSeconds(1);

        int ammountOfObject = Random.Range(0, 2);
        List<int> SelectSpawns = new List<int>();

        for (int i = 0; i < ammountOfObject + 1; i++)
        {
            int typeOfObstacles = Random.Range(0, posibleObstacles.Count);
            int transformOfObstacles = Random.Range(0, posibleSpawns.Count);
            SelectSpawns.Add(transformOfObstacles);

            if(i != 0)
            {
                while (SelectSpawns[i - 1] == transformOfObstacles)
                {
                    transformOfObstacles = Random.Range(0, posibleSpawns.Count);
                }
            }

            GameObject thisItem = Instantiate(posibleObstacles[typeOfObstacles], posibleSpawns[transformOfObstacles].position, Quaternion.identity);
            thisItem.GetComponent<ItemsMove>().canMove = isRunning;
        }
        yield return new WaitForSeconds(timer);

        if (timer > minTimer)
            timer -= timerSpeed;

        canSpawn = true;
    }
}
