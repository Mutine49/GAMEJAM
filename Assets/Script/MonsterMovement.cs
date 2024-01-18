using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterMovement : MonoBehaviour
{
    int StepID; // ID of the current step
    [SerializeField] Vector2[] ActionTime; // Array of the action time
    [SerializeField] float[] ReverseChance; // Array for the chance that a robot walks backwards
    [SerializeField] int KillTime;

    [SerializeField] public int NightID; // ID of the current night
    [SerializeField] int MonsterID; // ID of the current monster
    [SerializeField] Transform waypointsParent; // Parent of all the steps
    [SerializeField] Transform endingWaypoint; // Parent of all the steps

    int ComputeWaypointIndex()
    {
        float random = Random.value;
        if (ReverseChance.Length > 0 || random > ReverseChance[NightID])
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

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {

        switch (MonsterID)
        {
            case 1 :
            case 2 :
                Step();
                break;
            case 3 :
                CamLight();
                break;
        }

    }

    bool waitingForPlayerToLookAtMe = false;
    void Update()
    {
        if (waitingForPlayerToLookAtMe)
        {
            if (!FindObjectOfType<ChangeCam>().IsWatchingScreen())
            {
                switch (MonsterID)
                {
                    case 3:
                        Debug.Log("Hello");
                        Invoke("Jumpscare", KillTime - NightID);
                        waitingForPlayerToLookAtMe = false;
                        break;

                }

            }
        }
    }


    // 
    void Step()
    {
        Debug.Log(StepID);
        transform.position = waypointsParent.GetChild(StepID).position;
        StepID = ComputeWaypointIndex();
        if (StepID != waypointsParent.childCount)
        {
            Invoke("Step", Random.Range(ActionTime[NightID].x, ActionTime[NightID].y));
        }
        else
        {
            transform.position = waypointsParent.GetChild(waypointsParent.childCount - 1).position;
            Invoke("Jumpscare", KillTime - NightID);
        }
    }

    void CamLight()
    {
        if (FindObjectOfType<ChangeCam>().IsWatchingScreen() && Random.value < 1f)
        {
            transform.position = transform.position = waypointsParent.GetChild(1).position;
            waitingForPlayerToLookAtMe = true;
        }
        else
        {
            
            Invoke("CamLight", Random.Range(ActionTime[0].x, ActionTime[0].y) - NightID);
        }
    }

    // Jumpscare MOVE DANS DOORDEATH.cs
    void Jumpscare()
    {
        if (GetComponent<Death>().IsSafe())
        {
            StepID = 0;
            Step();
            Debug.Log("Safe");
        }
        else
        {
            transform.position = endingWaypoint.position;
            Debug.Log("GameOver");
            FindObjectOfType<GameOverScript>().GameOver();
        }

    }
}

