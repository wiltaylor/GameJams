using Assets.Systems.PlayerManager;
using UnityEngine;

public class CameraControll : MonoBehaviour
{

    private Camera _camera;

	private void Start ()
    {
		PlayerService.Instance.CameraCentre += InstanceOnCameraCentre;
        _camera = GetComponent<Camera>();

    }

    private void InstanceOnCameraCentre(object sender, CameraEventArgs cameraEventArgs)
    {
        var obj = cameraEventArgs.ObjectToCentreOn;
        
        transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, transform.position.z);
        
    }
}
