using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField]
    private GameObject Pipe;            // посилання для префабу
    [SerializeField]
    private GameObject Energy;          // посилання для префабу
    [SerializeField]
    private GameObject Heart;          

    private float pipeSpawnTime = 3;    
    private float pipeDeltaTime = 3;    
    private float pipeTime;             
    private float energyTime;           
    private float heartTime;           

    void Start()
    {
        pipeTime = 0;
        energyTime = 0;
        heartTime = 0;
    }
    void Update()
    {
        pipeTime -= Time.deltaTime;
        if (pipeTime < 0)
        {
            pipeTime = pipeSpawnTime + pipeDeltaTime * (1 - GameMenu.GameDifficulty);
            
            SpawnPipe();
            if (Random.value < 0.33f)  
            {
                energyTime = pipeTime / 2;
            }
            if (Random.value < 0.09f)  
            {
                heartTime = pipeTime / 2;
            }
        }


        if (energyTime > 0)
        {
            energyTime -= Time.deltaTime;
            if (energyTime <= 0)
            {
                SpawnEnergy();
            }
        }
        if(heartTime > 0)
        {
            heartTime -= Time.deltaTime;
            if(heartTime <= 0)
            {
                SpawnHeart();
            }
        }
    }
    void SpawnPipe()
    {
        GameObject.Instantiate(Pipe,
            this.transform.position
                + Vector3.up * Random.Range(-Bird.PipeShift, Bird.PipeShift),
            Quaternion.identity);
    }
    void SpawnEnergy()
    {
        GameObject.Instantiate(Energy,
            this.transform.position
                + Vector3.up * Random.Range(-Bird.PipeShift, Bird.PipeShift),
            Quaternion.identity);
    }
    void SpawnHeart()
    {
        GameObject.Instantiate(Heart,
           this.transform.position
               + Vector3.up * Random.Range(-Bird.PipeShift, Bird.PipeShift),
           Quaternion.identity);
    }
}
