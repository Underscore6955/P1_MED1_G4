using UnityEngine;
using UnityEngine.UI;

public class FakeClock : MonoBehaviour
{
    // virker kun med legacy text og ikke textMeshPro.
    
    public float StartTimer; //start time of chapter (hours)
    public float StartMins; //(minutes) plz match with MinuteAmount fx. 45 if delay = 5. 

    private float Timer;
    private float Mins;


    public float TimeDelay; //number of seconds that pass before minute count inscreases. (like a timer)
    public float MinuteLeap; //num. minutes increase after delay timer.
    private float DelayComb;

    public Text DisplayClock;

    void Start()
    {
        Timer = StartTimer;
        Mins = StartMins;
    }

    void Update()
    {
        DelayComb += Time.deltaTime;

        if (DelayComb>=TimeDelay)
        {
            Mins += MinuteLeap;

            if (Mins >= 59.95f)
            {
                Mins = 0;
                Timer += 1;
            }

            DelayComb = 0;
        }

        if (Timer == 24)
        {
            Mins = 00;
            Timer = 00;
        }
        
        //int CurTimer = (int)Timer; (old code I don't wanna delete)
        //int CurMins = (int)Mins;

        string CurTimer = Timer.ToString("00"); //hjælp fra BudAi
        string CurMins = Mins.ToString("00");

        DisplayClock.text = CurTimer + ":" + CurMins; //displays UI text obj som "timer:min"
    }
}
