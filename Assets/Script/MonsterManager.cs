using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NightSpawn
{
    public int time;
    public GameObject monsterPrefab;
}
public class MonsterManager : MonoBehaviour
{
    [SerializeField] Transform monsterTransform;
    [SerializeField] NightSpawn[] spawnsNight1;
    [SerializeField] NightSpawn[] spawnsNight2;
    [SerializeField] NightSpawn[] spawnsNight3;

    int currentNight = 0;
    int currentSpawnIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        NightManager.Instance.newNightId += (x) =>
        {
            currentNight = x;
        };
        NightManager.Instance.timerCallback += UpdateTimer;
        NightManager.Instance.nightStopped += KillAllMonsters;
    }

    void KillAllMonsters()
    {
        for(int i = monsterTransform.childCount -1; i >= 0; i--)
        {
            Destroy(monsterTransform.GetChild(i).gameObject);
        }
        currentSpawnIndex = 0;
    }
    private void UpdateTimer(float timer)
    {
        NightSpawn[] currentSpawnNight = null;
        switch(currentNight)
        {
            case 0:
                currentSpawnNight = spawnsNight1;
                break;
            case 1:
                currentSpawnNight = spawnsNight2;

                break;
            case 2:
                currentSpawnNight = spawnsNight3;
                break;
            default:
                break;
        }

        if (currentSpawnNight == null)
            return;

        if (currentSpawnIndex <= currentSpawnNight.Length -1 && timer >= currentSpawnNight[currentSpawnIndex].time)
        {
            Instantiate(currentSpawnNight[currentSpawnIndex].monsterPrefab, monsterTransform).GetComponentInChildren<MonsterMovement>().NightID = currentNight;
            currentSpawnIndex++;
        }
    }
}
