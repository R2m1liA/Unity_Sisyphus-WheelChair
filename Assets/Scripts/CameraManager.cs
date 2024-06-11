using Unity.Cinemachine;
using Unity.Collections;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private static CameraManager _instance;
    public static CameraManager Instance => _instance;

    [SerializeField]
    private CinemachineCamera[] cameraList;

    public CinemachineCamera defaultCamera;

    [SerializeField]
    [ReadOnly] private CinemachineCamera currentCamera;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void Start()
    {
        cameraList = GetComponentsInChildren<CinemachineCamera>();
        foreach (var camera in cameraList)
        {
            camera.Priority = 0;
        }

        currentCamera = defaultCamera;
        defaultCamera.Priority = 1;
    }

    public void SwitchCamera(CinemachineCamera camera1, CinemachineCamera camera2)
    {
        Time.timeScale = 0;
        if (currentCamera == camera1)
        {
            camera1.Priority = 0;
            camera2.Priority = 1;
            currentCamera = camera2;
        }
        else if (currentCamera == camera2)
        {
            camera2.Priority = 0;
            camera1.Priority = 1;
            currentCamera = camera1;
        }
        else
        {
            Debug.LogError("Current camera is not found.");
        }
        Time.timeScale = 1;
    }
    
}
