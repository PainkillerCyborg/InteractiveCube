// using UnityEngine;
//using System.Collections;
//
//public class RotationControl : MonoBehaviour
//{
//
//	// Use this for initialization
//	void Start ()
//	{
//	
//	}
//	
//	// Update is called once per frame
//	void Update ()
//	{
//		Debug.Log (Input.mousePosition.x + " " + Input.mousePosition.y + " " + Input.mousePosition.z);
//	}
//}

using UnityEngine;
using System.Collections;

public class RotationControl : MonoBehaviour
{
	public float rotSpeed;
	public float rotX, rotY;
	public float rotXOriginal, rotYOriginal;
	void Start ()
	{
		//Original values of the cube's rotation. Used to reset the cube.
		rotXOriginal = transform.rotation.eulerAngles.x;
		rotYOriginal = transform.rotation.eulerAngles.y;
	}

	void Update ()
	{
		///Rotation of the cube is done with right click dragging. Works only till the time the cursor is upon the cube.
		if (Input.GetMouseButton (1)) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit)) {
				rotX = rotSpeed * Input.GetAxis ("Mouse X");
				rotY = rotSpeed * Input.GetAxis ("Mouse Y");
				transform.RotateAround (Vector3.zero, Vector3.up, -rotX);
				transform.RotateAround (Vector3.zero, Vector3.right, rotY);
			}
		}
	}

	/// <summary>
	/// Resets the rotation of the cube.
	/// </summary>
	public void ResetRotation ()
	{
		transform.eulerAngles = new Vector3 (rotXOriginal, rotYOriginal, 0);
	}
	
}