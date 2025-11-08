using System;
using UnityEngine;

public class SuccessFeedbackController : MonoBehaviour
{

    [SerializeField]
    private GameObject successPrefab;

    [SerializeField]
    private CameraController cameraController;

    [SerializeField]
    private GridController gridController;

    [SerializeField]
    private Transform spawnTransform;

    private void Start()
    {
        gridController.SuccessfulGrid += SpawnSuccess;
    }

    private void SpawnSuccess()
    {
        cameraController.Shake(CameraController.ShakeType.LIGHT);
        Instantiate(successPrefab, spawnTransform);
    }
}
