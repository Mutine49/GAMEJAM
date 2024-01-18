using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NightManager : MonoBehaviour
{
    public static NightManager instance;
    public static NightManager Instance
    {
        get
        {
              return instance;
        }
    }

    public Action<float> timerCallback;
    public Action nightStopped;
    public Action<int> newNightId;

    [SerializeField] timer tObject;
    [SerializeField] List<Graphic> graphics;
    [SerializeField] TMP_Text text;
    public int currentNight;
    // Start is called before the first frame update
    void Awake()
    {
        if(instance != null)
        {
            Destroy(instance);
            return;
        }

        instance = this;
        tObject.callback += NextNight;
        tObject.updateCallback += (x) =>
        {
            timerCallback?.Invoke(x);
        };
        currentNight = -1;
        NextNight();
    }

    void SetColorOfAllGraphics(float a)
    {
        foreach (var g in graphics)
        {
            Color c = g.color;
            c.a = a;
            g.color = c;
        }
    }

    IEnumerator FadeCoroutine()
    {
        float timer = 0.0f;
        float fadeIn = 2.5f;
        while(timer <= fadeIn)
        {
            timer += Time.deltaTime; 
            foreach(var g in graphics)
            {
                SetColorOfAllGraphics(Mathf.Lerp(0f, 1f, timer / fadeIn));
            }
            yield return new WaitForEndOfFrame();
        }
        SetColorOfAllGraphics(1f);
        yield return new WaitForSeconds(3f);
        timer = 0f;
        while (timer <= fadeIn)
        {
            timer += Time.deltaTime;
            foreach (var g in graphics)
            {
                SetColorOfAllGraphics(Mathf.Lerp(1f, 0f, timer / fadeIn));
            }
            yield return new WaitForEndOfFrame();
        }
        SetColorOfAllGraphics(0f);
        tObject.ResetTimer();
        newNightId.Invoke(currentNight);
        yield return null;
    }

    void NextNight()
    {
        nightStopped?.Invoke();
        currentNight++;
        if (currentNight >= 3)
        {
            FindObjectOfType<GameOverScript>().Win();
        }
        else
        {
            text.text = "Night " + (currentNight + 1);
            StartCoroutine(FadeCoroutine());
        }

    }
}
