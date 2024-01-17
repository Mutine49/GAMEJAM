using System;
using TMPro;
using UnityEngine;

public class timer : MonoBehaviour
{
    [SerializeField] float triggerTime = 360;
    public Action callback;
    TMP_Text textObject;
    float timers = 0f;
    bool started;
    public  Action<float> updateCallback;

    // Start is called before the first frame update
    void Start()
    {
        textObject = GetComponent<TMP_Text>();
    }

    public void StartTimer()
    {
        started = true;
    }

    public void StopTimer()
    {
        started = false;
        timers = 0f;
    }

    public void PauseTimer()
    {
        started = false;
    }

    public void ResetTimer()
    {
        timers = 0f;
        started = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(started)
        {
            timers += Time.deltaTime;
            updateCallback?.Invoke(timers);
            if (timers >= triggerTime)
            {
                timers = triggerTime;
                PauseTimer();
                callback?.Invoke();
            }
            textObject.text = AdjustTime(timers);
        }

    }

    string AdjustTime(float time)
    {
        int h = (int)(time / 60);
        int m = (int)(time % 60);

        return $"{h:00}:{m:00}";
    }
}