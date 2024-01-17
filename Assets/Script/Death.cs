using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum DEATH_TYPE
{
    DOOR,
    LIGHT,
    CAMERA
}

public class Death : MonoBehaviour
{
    [SerializeField] DEATH_TYPE type;
    public bool IsSafe()
    {
        switch (type)
        {
            case DEATH_TYPE.DOOR:
                return FindObjectOfType<ScriptOnOff>().isDoorClose;
            case DEATH_TYPE.LIGHT:
                return !FindObjectOfType<ScriptOnOff>().isLightOff;
            case DEATH_TYPE.CAMERA:
                return !FindObjectOfType<ChangeCam>().IsWatchingScreen();
            default:
                Debug.LogError("TYPE IS NOT DEFINED");
                return false;
        }
    }
}
