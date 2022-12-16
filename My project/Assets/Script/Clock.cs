using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Clock : MonoBehaviour
{
    private TextMeshProUGUI clock;

   
    private float time;

    void Start()
    {
        clock = this.GetComponent<TextMeshProUGUI>();

        time = 0;
    }

    void Update()
    {
        time += Time.deltaTime;  
        int t = (int)time;
        int h = t / 3600;
        int m = (t % 3600) / 60;
        int s = t % 60;
        int d = (int)((time - t) * 10);

        clock.text = $"{h:00}:{m:00}:{s:00}.{d}";
    }
}
