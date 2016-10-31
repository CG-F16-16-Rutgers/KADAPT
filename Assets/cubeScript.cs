using UnityEngine;
using System.Collections;

public class cubeScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Player")
        {
            GetComponent<Rigidbody>().isKinematic = true;
            //print("Collided");
        }
    }

}
