using UnityEngine;
using System.Collections;

public class ArmRotation : MonoBehaviour {

	public int rotationOffset = 90;

	// Update is called once per frame
	void Update () {
		// subtracting the position of the player from the mouse position
		Vector3 difference = Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position;
		difference.Normalize ();		// normalizing the vector. Meaning that all the sum of the vector will be equal to 1

		float rotZ = Mathf.Atan2 (difference.y, difference.x) * Mathf.Rad2Deg;	// find the angle in degrees
        float angle = rotZ;
        // Coordinates are expressed as intervals between -180 and 180. This way we can change it to 0 to 360.
        if (angle < 0) angle = 360 + angle;

        Vector4 limits = GlobalStats.currentStats.GetLimitAimAngle();
        if (((angle <= limits.x && angle >= limits.z) || (angle >= limits.y && angle <= limits.w)))
        {
            GlobalStats.currentStats.SetMaxAngleReached(false);
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset);
        }
        else
        {
            GlobalStats.currentStats.SetMaxAngleReached(true);
        }
        GlobalStats.currentStats.SetCurrentArmRotation(transform.rotation.z);
	}
}
