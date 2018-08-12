using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathUtil  {

	/// <summary>
	/// Gets the angle in degrees based on two positions.
	/// </summary>
	public static float GetAngleBasedOnPosition(Vector3 currentPosition, Vector3 targetPosition)
	{
		Vector2 direction = (targetPosition - currentPosition).normalized;
		return (Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg);
	}

}
