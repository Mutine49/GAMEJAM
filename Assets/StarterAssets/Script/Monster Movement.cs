using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    int ComputeWaypointIndex()
    {
        float random = Random.value;
        if (random < ReverseChance[NightID])
        {
            return StepID + 1;
        }
        else
        {
            if (StepID > 0)
            {
                return StepID - 1;
            }
            else
            {
                return StepID;
            }

        }

    }

    int StepID; // ID of the current step
    float[] ActionTime; // Array of the action time
    float[] ReverseChance; // Array for the chance that a robot walks backwards
    float[] KillTime;

    [SerializeField] int NightID; // ID of the current night
    [SerializeField] int MonsterID; // ID of the current monster
    [SerializeField] Transform waypointsParent; // Parent of all the steps

    

    // Start is called before the first frame update
    void Start()
    {
        // Reverse chance list for frequency of falling back each night
        ReverseChance = new float[6];
        ReverseChance[1] = 0.5f;
        ReverseChance[2] = 0.6f;
        ReverseChance[3] = 0.7f;
        ReverseChance[4] = 0.8f;
        ReverseChance[5] = 0.9f;

        Invoke("Monster" + MonsterID, 0f);

    }


    // 
    void Step()
    {
        transform.position = waypointsParent.GetChild(StepID).position;
        StepID = ComputeWaypointIndex();
        if (StepID != waypointsParent.childCount)
        {
            Invoke("Monster" + MonsterID, ActionTime[NightID]);
            Debug.Log(StepID);
        }
        else
        {
            transform.position = waypointsParent.GetChild(waypointsParent.childCount-1).position;
            Invoke("GameOver", 5f);
        }
    }

    void GameOver()
    {
        Debug.Log("GameOver");
    }

    void Monster1()
    {
        if (NightID > 0)
        {
            // Action time list for frequency of movement each night
            ActionTime = new float[6];
            ActionTime[1] = Random.Range(10f, 8f);
            ActionTime[2] = Random.Range(9f, 7f);
            ActionTime[3] = Random.Range(7f, 6f);
            ActionTime[4] = Random.Range(6f, 5f);
            ActionTime[5] = Random.Range(5f, 3f);

            Debug.Log(ActionTime[NightID]);
            Invoke("Step", 0f);
        }
        
    }

    void Monster2()
    {
        
    }

    void Monster3()
    {

    }

    void Monster4()
    {
       
    }
}


