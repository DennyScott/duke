using UnityEngine;

public static class CSTransform {

	/// <summary>Allow User to Call SetX to change the x position of Transform</summary>
	/// <param name="transform">transform</param>
	/// <param name="x">Desired X Value for position</param>
	public static void SetX(this Transform transform, float x) {
		var newPosition = 
			new Vector3(x, transform.position.y, transform.position.z);

		transform.position = newPosition;
	}

	/// <summary>Allow User to call SetY to change the y position of Transform</summary>
	/// <param name="transform">transform</param>
	/// <param name="y">Desired Y Value for position</param>
	public static void SetY(this Transform transform, float y) {
		var newPosition = 
			new Vector3(transform.position.x, y, transform.position.z);

		transform.position = newPosition;
	}
}
