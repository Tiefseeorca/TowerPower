using UnityEngine;

public class CameraScript : MonoBehaviour
{
	static int TARGET_FPS = 60;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start() {
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = CameraScript.TARGET_FPS;
	}

	// Update is called once per frame
	void Update()
	{
        
	}
}