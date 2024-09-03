using System;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CameraController : MySingleton<CameraController>
{

    public Camera mainCamera;
    public Camera zoomCamera;
    
    public float cameraSpeed = 5.0f;
    public GameObject player;

    private Camera currentCamera;
    private bool isZoom;

    private void Awake()
    {
        mainCamera.enabled = true;
        zoomCamera.enabled = false;
        currentCamera = mainCamera;
        isZoom = false;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = player.transform.position;
        targetPosition.z = transform.position.z; // 카메라의 Z 위치 고정

        // 카메라 위치를 타겟 위치로 부드럽게 이동
        transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSpeed * Time.deltaTime);
    }

    public void SwitchCamera(bool zoom)
    {
        if (zoom)//줌인<이 게임에서는 줌인이 화면을 넓게 보여줌>
        {
            zoomCamera.enabled = true;
            mainCamera.enabled = false;
            currentCamera = zoomCamera;
            isZoom = true;
        }
        else
        {
            mainCamera.enabled = true;
            zoomCamera.enabled = false;
            currentCamera = mainCamera;
            isZoom = false;
        }
    }
    
    public Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = currentCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Z 값을 0으로 설정하여 2D 월드 좌표로 변환
        return mousePosition;
    }

    public bool GetZoomStatus()
    {
        return isZoom;
    }
}