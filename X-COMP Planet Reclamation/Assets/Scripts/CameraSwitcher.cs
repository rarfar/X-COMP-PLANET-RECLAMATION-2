using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera[] cameras;
    private int activeCameraIndex = 0;

    public void SwitchCamera(int newCameraIndex)
    {
        // Disable the current active camera
        cameras[activeCameraIndex].gameObject.SetActive(false);

        // Enable the new camera
        cameras[newCameraIndex].gameObject.SetActive(true);

        // Update the active camera index
        activeCameraIndex = newCameraIndex;
    }
}