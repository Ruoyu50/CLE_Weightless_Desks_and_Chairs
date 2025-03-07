using UnityEngine;
using System.Collections;

public class PathFollower : MonoBehaviour {

	//declaring a variable for speed 
  private float speed; 

//add if you want to have different speeds for different parts of your scene
  	public float[] speeds; 
	public Transform pathParent;
	// public float rotationSpeed; 

	public Transform[] targetObjects; 
	public float rotationSpeed = 0.1f; 
	Transform targetPoint;
	Transform targetObject; 
	int index;

//uncomment to see pathway of camera
	// void OnDrawGizmos()
	// {
	// 	Vector3 from;
	// 	Vector3 to;
	// 	for (int a=0; a<pathParent.childCount; a++)
	// 	{
	// 		from = pathParent.GetChild(a).position;
	// 		to = pathParent.GetChild((a+1) % pathParent.childCount).position;
	// 		Gizmos.color = new Color (1, 0, 0);
	// 		Gizmos.DrawLine (from, to);
	// 	}
	// }
	void Start () {
		index = 0;
    
        //sets intial waypoint to first in pathParent GameObject
		targetPoint = pathParent.GetChild (index);    
		targetObject = targetObjects[index]; 
          
        transform.position = targetPoint.position; 

        // Check if speeds array has enough elements
        if (speeds.Length > 0 && speeds.Length > index) {
            speed = speeds[index];
        } else {
            // Fallback if there are no speeds or fewer speeds than waypoints
            speed = 1f; // default speed
        }
	}
	
	// Update is called once per frame
	void Update () {
		//ensures index is within bounds of our path 
      	if(index < pathParent.childCount){
			//moves camera towards the next target point 
			transform.position = Vector3.MoveTowards (transform.position, targetPoint.position, speed * Time.deltaTime);
			
			Quaternion targetRotation = Quaternion.LookRotation(targetObject.position - transform.position);
			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
     
			
			if (Vector3.Distance (transform.position, targetPoint.position) < 0.1f) {
				//stops moving camera if it's at the last waypoint
				index++;

				 // Ensure that we are not trying to access a speed if we haven't created one in the Unity editor
                if (index < speeds.Length) {
                    speed = speeds[index]; // Update the speed if we have created a speed in the Unity editor
                } else {
                    // if we haven't created enough speeds in the editor, set the camera speed to a default
                    speed = 1f; // Default speed or some fallback logic
                }

				//camera has reached the end of the path, set it's position to the last waypoint 
                if (index < pathParent.childCount) {
                    targetPoint = pathParent.GetChild(index);
					targetObject = targetObjects[index]; 
                }
            }
		}
	}
}