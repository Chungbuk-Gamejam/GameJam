using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    static public CameraManager instance;

    [Header("Camera")]
    [Tooltip("카메라가 따라갈 목표")]
    [SerializeField] private GameObject target;
    [Tooltip("카메라가 따라가는 속도")]
    [SerializeField] private float moveSpeed;
    [Tooltip("카메라 위치 설정")]
    [SerializeField] private Vector3 targetPosition;
    [Tooltip("카메라가 움직일 수 있는 영역")]
    [SerializeField] private BoxCollider2D currentBound;

    private Vector3 originalPosition;
    private float originalSize;
    public BoxCollider2D targetArea; // 카메라가 비춰야 할 영역

    private Vector3 minBound;
    private Vector3 maxBound;

    private float halfWidth;
    private float halfHeight;

    private Camera theCamera;

    private bool isActivated = false;
   
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");

        theCamera = GetComponent<Camera>();
        minBound = currentBound.bounds.min;
        maxBound = currentBound.bounds.max;
        halfHeight = theCamera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;

        originalPosition = theCamera.transform.position;
        originalSize = theCamera.orthographicSize;

        UpdateCurrentBound();
        UpdateCameraPosition();
    }

    void LateUpdate()
    {
        if (target != null)
        {
            //UpdateCurrentBound();
            UpdateCameraPosition();
        }
    }

    //카메라가 비추는 영역을 계속 갱신 해주는 함수
    private void UpdateCurrentBound()
    {
        Collider2D[] colliders = Physics2D.OverlapPointAll(target.transform.position);
        foreach (var collider in colliders)
        {
            if (collider is BoxCollider2D && collider.CompareTag("Camera"))
            {
                SetBound(collider as BoxCollider2D);
                break;
            }
        }
    }

    //카메라가 목표를 따라 움직이도록 하는 함수
    private void UpdateCameraPosition()
    {
        Debug.Log(currentBound);
        if (currentBound != null)
        {
            targetPosition.Set(target.transform.position.x, target.transform.position.y, this.transform.position.z);

            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.fixedDeltaTime);

            float clampedX = Mathf.Clamp(this.transform.position.x, minBound.x + halfWidth, maxBound.x - halfWidth);
            float clampedY = Mathf.Clamp(this.transform.position.y, minBound.y + halfHeight, maxBound.y - halfHeight);

            this.transform.position = new Vector3(clampedX, clampedY, this.transform.position.z);
        }
    }

    //카메라가 비추는 영역을 다시 설정하는 함수
    public void SetBound(BoxCollider2D newBound)
    {
        currentBound = newBound;
        minBound = currentBound.bounds.min;
        maxBound = currentBound.bounds.max;
    }

    public void AdjustCameraToArea()
    {
        isActivated = true;
        // BoxCollider2D의 크기에 맞게 카메라 위치 조정
        Bounds bounds = targetArea.bounds;
        theCamera.transform.position = new Vector3(bounds.center.x, bounds.center.y, theCamera.transform.position.z);

        // 카메라의 orthographicSize를 박스 콜라이더의 높이에 맞춰 조정
        float aspectRatio = (float)Screen.width / Screen.height;
        theCamera.orthographicSize = bounds.size.y / 2;

        // 카메라의 너비가 영역을 벗어나지 않도록 조정
        float cameraWidth = theCamera.orthographicSize * aspectRatio;
        if (cameraWidth > bounds.size.x / 2)
        {
            theCamera.orthographicSize = bounds.size.x / 2 / aspectRatio;
        }
    }

    public void ResetCamera()
    {
        isActivated = false;
        // 카메라를 원래 위치와 사이즈로 복구
        theCamera.transform.position = originalPosition;
        theCamera.orthographicSize = originalSize;
    }
}
