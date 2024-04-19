using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayTimeController : MonoBehaviour
{
    const float secondsInDay = 86400f;
    const float phaseLength = 900f;
    float time;
    private int days;
    [SerializeField] float timeScale = 60f;   // Tỷ lệ thời gian game trên thời gian thực
    [SerializeField] float startAtTime = 28800f; // in seconds
    [SerializeField] Color nightLightColor;
    [SerializeField] AnimationCurve nightTimeCurve;   // Đường cong điều chỉnh chu kỳ ngày đêm
    [SerializeField] Color dayLightColor = Color.white;
    [SerializeField] TextMeshProUGUI text;   // Hiển thị thời gian lên màn hình
    [SerializeField] Light2D globalLight;
    private int days;
    List<TimeAgent> agents;
    private void Awake()
    {
        agents = new List<TimeAgent>();
    }

    private void Start()
    {
        time = startAtTime;
    }

    public void Subscribe(TimeAgent timeAgent)
    {
        agents.Add(timeAgent);
    }

    public void Unsubscribe(TimeAgent timeAgent)
    {
        agents.Remove(timeAgent);
    }
    float Hours
    {
        get { return time/3600;}
    }
    float Minutes
    {
        get { return time % 3600 / 60f; }
    }

    private void Update()
    {
        time += Time.deltaTime * timeScale; // 1s life = 1 min game
        TimeValueCalculation();    
        DayLight();
        if (time > secondsInDay)
        {
            NextDay();
        }
        TimeAgents();
    }
    int oldPhase = 0;
    private void TimeAgents()
    {
        int currentPhase = (int)(time/phaseLength);
        if(oldPhase != currentPhase)
        {
            oldPhase = currentPhase;
            for(int i=0;i<agents.Count;i++)
            {
            agents[i].Invoke();
            }
            
        }
    }
    private void DayLight()
    {
        float v = nightTimeCurve.Evaluate(Hours);
        Color c = Color.Lerp(dayLightColor, nightLightColor, v);
        globalLight.color = c;
    }
    private void TimeValueCalculation()
    {
        int hh = (int)Hours;
        int mm = (int)Minutes;
        text.text = hh.ToString("00") + ":" + mm.ToString("00");
    }
    private void NextDay()
    {
        time -= secondsInDay;
        days += 1;
    }
}
