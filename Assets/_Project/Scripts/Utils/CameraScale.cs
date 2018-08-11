using UnityEditor;
using UnityEngine;
[ExecuteInEditMode]
public class CameraScale : MonoBehaviour {

	[SerializeField] private Camera _camera;

	[SerializeField] private float _pixelsPerUnit = 100; // 100 pixels = 1 unit
	[SerializeField] private const int _targetWidth = 1920; // Width IN PIXELS of the target resolution 1920x1080

	// For debugging purposes
	[Header ("DEBUG")]
	[SerializeField] private float _currentWidth;
	[SerializeField] private float _currentHeight;

	void Awake () {
		if (_camera == null)
			_camera = Camera.main;
	}
	void Start () {
		// For debugging purposes
		// _currentWidth = Screen.width;
		// _currentHeight = Screen.height;

		// Rescale by the width
		int heightInPixels = Mathf.RoundToInt (_targetWidth / (float) Screen.width * Screen.height);

		float heightUnits = heightInPixels / _pixelsPerUnit;
		// Conversion
		_camera.orthographicSize = heightUnits / 2;
	}

	/*
	Esse trecho só serve para debug, para realizar a atualização da scene com o aspect ratio sem a necessidade de apertar o botão play
	Isso só rodará no editor e fora do play mode
	 */
#if UNITY_EDITOR
	void Update () {
		if (EditorApplication.isPlaying)
			return;
		// For debugging purposes
		// _currentWidth = Screen.width;
		// _currentHeight = Screen.height;

		// Rescale by the width
		int heightInPixels = Mathf.RoundToInt (_targetWidth / (float) Screen.width * Screen.height);

		float heightUnits = heightInPixels / _pixelsPerUnit;
		// Conversion
		_camera.orthographicSize = heightUnits / 2;
	}
#endif
}