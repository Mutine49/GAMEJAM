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

    [SerializeField] AudioSource FootStep;
    [SerializeField] AudioSource GlassBang;
    [SerializeField] AudioSource Doorhit;
    [SerializeField] AudioSource Scream;

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
                        GlassBang.Play();
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
        transform.rotation = waypointsParent.GetChild(StepID).rotation;
        FootStep.Play();
        StepID = ComputeWaypointIndex();
        if (StepID != waypointsParent.childCount)
        {
            Invoke("Step", Random.Range(ActionTime[NightID].x, ActionTime[NightID].y));
            if (StepID == waypointsParent.childCount - 1 && MonsterID == 2)
            {
                GlassBang.Play();
            }
        }
        else
        {
            transform.position = waypointsParent.GetChild(waypointsParent.childCount - 1).position;
            transform.rotation = waypointsParent.GetChild(waypointsParent.childCount - 1).rotation;
            if (MonsterID == 1)
            {
                GlassBang.Play();
            }
            Invoke("Jumpscare", KillTime - NightID);
        }
    }

    void CamLight()
    {
        if (FindObjectOfType<ChangeCam>().IsWatchingScreen() && Random.value < 0.5f)
        {
            transform.position = waypointsParent.GetChild(1).position;
            transform.rotation = waypointsParent.GetChild(1).rotation;
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
            switch (MonsterID)
            {
                case 1:
                case 2:
                    Doorhit.Play();
                    StepID = 0;
                    Step();
                    break;
                case 3:
                    FootStep.Play();
                    transform.position = waypointsParent.GetChild(0).position;
                    transform.rotation = waypointsParent.GetChild(0).rotation;
                    Invoke("CamLight",KillTime);
                    break;
            }
            Debug.Log("Safe");
        }
        else

        {
            transform.position = endingWaypoint.position;
            transform.rotation = endingWaypoint.rotation;
            Scream.Play();
            Debug.Log("GameOver");
            FindObjectOfType<GameOverScript>().GameOver();
        }

    }
}

