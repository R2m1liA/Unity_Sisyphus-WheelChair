using UnityEngine;

public class Level : MonoBehaviour
{
    public int levelNumber;
    public GameObject SpawnPoint;
    public GameObject[] Rocks;
    public GameObject[] Targets;

    public GameObject[] Doors;
    public GameObject[] LevelMaps;

    private void Start()
    {
        ResetLevel();
    }

    private void Update()
    {
        LevelCheck();
    }

    public void ResetLevel()
    {
        if(Rocks.Length == 0)
        {
            return;
        }
        foreach (GameObject rock in Rocks)
        {          
            rock.GetComponent<Pushable>().Reset();
        }
        if (Targets.Length == 0)
        {
            return;
        }
        foreach (GameObject target in Targets)
        {
            target.GetComponent<Target>().Reset();
        }
        if (Doors.Length == 0)
        {
            return;
        }
        foreach (GameObject door in Doors)
        {
            door.SetActive(true);
        }
    }

    public void LevelCheck()
    {
        if (Targets.Length == 0)
        {
            return;
        }
        foreach(GameObject target in Targets)
        {
            if (!target.GetComponent<Target>().hasRock)
            {
                return;
            }
        }
        LevelComplete();
    }

    public void LevelComplete()
    {
        if (Doors.Length == 0)
        {
            return;
        }
        foreach(GameObject door in Doors)
        {
            door.SetActive(false);
        }
        if (LevelMaps.Length == 0)
        {
            return;
        }
        foreach(GameObject levelMap in LevelMaps)
        {
            levelMap.SetActive(true);
        }
    }
}
