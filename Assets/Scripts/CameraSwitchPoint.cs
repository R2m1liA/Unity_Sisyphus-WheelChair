using Unity.Cinemachine;
using UnityEngine;

public class CameraSwitchPoint : MonoBehaviour
{
    [SerializeField]
    private CinemachineCamera camera1;
    [SerializeField]
    private Level level1;

    [SerializeField]
    private CinemachineCamera camera2;
    [SerializeField]
    private Level level2;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CameraManager.Instance.SwitchCamera(camera1, camera2);
            LevelManager.Instance.SwitchLevel(level1, level2);
        }
    }
}
