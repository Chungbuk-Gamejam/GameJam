using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCameraBound : MonoBehaviour
{
    public CameraManager cameraManager;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // �÷��̾ ������ ���� ��
        {
            cameraManager.AdjustCameraToArea();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // �÷��̾ �������� ������ ��
        {
            cameraManager.ResetCamera();
        }
    }

}
