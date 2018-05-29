using UnityEngine;


public static class CameraExtension {

    static private Vector3 _tempVector;
    
	public static Vector3 GetCenter(this Camera camera)
    {
        _tempVector.Set(Screen.width, Screen.height, 0);
        return camera.ScreenToWorldPoint(_tempVector);
    }
		
}