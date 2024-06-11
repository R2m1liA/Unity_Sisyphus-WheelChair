using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;
    public static LevelManager Instance => instance;
    public Level currentLevel;

    public AudioClip ResetSoundFXClip;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        if(InputManager.Instance.RestartValue)
        {
            ResetLevel();
        }
    }

    public void SwitchLevel(Level level1, Level level2)
    {
        if (currentLevel == level1)
        {
            currentLevel = level2;
        }
        else if (currentLevel == level2)
        {
            currentLevel = level1;
        }
        else
        {
            Debug.LogError("Current level is not found.");
        }
    }

    public void ResetLevel()
    {
        PlayerController player = FindAnyObjectByType<PlayerController>();
        player.transform.position = currentLevel.SpawnPoint.transform.position;
        player.targetPosition = currentLevel.SpawnPoint.transform.position;
        currentLevel.ResetLevel();
        SoundManager.Instance.PlaySFX(ResetSoundFXClip);
    }

}
