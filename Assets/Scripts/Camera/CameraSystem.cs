using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    // TODO check game state

    [Header("Components")]
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    private CinemachineTransposer transposer;

    [Header("System config")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;

    [SerializeField] private float zoomSpeed;

    [SerializeField] private Vector3 inputDir;
    [SerializeField] private float rotateDir;
    [SerializeField] private Vector3 zoomOffset;

    private void Awake()
    {
        transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
    }

    private void Start()
    {
        zoomOffset = transposer.m_FollowOffset;
    }

    private void Update()
    {
        CameraRotation();
        CameraMovement();
        CameraZoom();
    }

    private void CameraRotation()
    {
        rotateDir = 0f;
        rotateDir = Input.GetKey(KeyCode.Q) ? 1 : Input.GetKey(KeyCode.E) ? -1 : 0;

        if (Input.GetMouseButtonDown(2))
            transform.eulerAngles = Vector3.zero;

        RotateCamera();
    }
    private void CameraMovement()
    {
        inputDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        inputDir.Normalize();

        MoveCamera();
    }
    private void CameraZoom()
    {
        Vector3 zoomDir = zoomOffset.normalized;

        zoomOffset += zoomDir * -Input.mouseScrollDelta.y;

        if (zoomOffset.magnitude < 10)
            zoomOffset = zoomDir * 10;

        if (zoomOffset.magnitude > 40)
            zoomOffset = zoomDir * 40;

        if (Input.GetMouseButtonDown(2))
            zoomOffset = new Vector3(0, 20, -15);

        transposer.m_FollowOffset = Vector3.Lerp(transposer.m_FollowOffset, zoomOffset, Time.deltaTime * zoomSpeed);
    }

    private void MoveCamera()
    {
        Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;
        moveDir.Normalize();

        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }
    private void RotateCamera()
    {
        transform.eulerAngles += Vector3.up * rotateDir * rotateSpeed * Time.deltaTime;
    }
}
