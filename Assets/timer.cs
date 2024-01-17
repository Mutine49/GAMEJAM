using TMPro;
using UnityEngine;

public class timer : MonoBehaviour
{
    TMP_Text textObject;
    float timers = 0f;
    // Start is called before the first frame update
    void Start()
    {
        textObject = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        timers += Time.deltaTime;
        textObject.text = AdjustTime(timers);
    }

    string AdjustTime(float time)
    {
        int h = (int)(time / 60);
        int m = (int)(time % 60);

        return $"{h:00}:{m:00}";
    }
}