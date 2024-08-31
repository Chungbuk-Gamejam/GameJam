using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCameraBound : MonoBehaviour
{
    public CameraManager cameraManager;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 플레이어가 영역에 들어갔을 때
        {
            cameraManager.AdjustCameraToArea();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 플레이어가 영역에서 나갔을 때
        {
            cameraManager.ResetCamera();
        }
    }

}
