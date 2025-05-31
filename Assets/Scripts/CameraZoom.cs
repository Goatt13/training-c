using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private CinemachineCamera cineCam;
    [SerializeField] private float zoomSpeed = 5f;
    [SerializeField] private float minZoom = 3f;
    [SerializeField] private float maxZoom = 10f;

    void Update()
{
    if (cineCam == null) return; // cineCam atanmadıysa işlem yapma

    float scroll = Mouse.current.scroll.ReadValue().y;

    if (Mathf.Abs(scroll) > 0.01f)
    {
        float currentSize = cineCam.Lens.OrthographicSize;
        float newSize = currentSize - scroll * zoomSpeed * Time.deltaTime;
        newSize = Mathf.Clamp(newSize, minZoom, maxZoom);
        cineCam.Lens.OrthographicSize = newSize;
    }
}
}