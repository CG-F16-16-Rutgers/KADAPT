using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainCamera : MonoBehaviour {

    private float speed=10.0f;
    public Transform target;

    private float speedH = 10.0f;
    private float speedV = 10.0f;

    private float yaw = 0.0f;
    private float pitch = 90f;

    public static bool BarrierSelected;




    // Use this for initialization
    void Start () {

        BarrierSelected = false;


    }
	
	// Update is called once per frame
	void Update () {

        if (BarrierSelected) return;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
        }/*
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
        }*/


        if (Input.GetMouseButton(1))
        {
            yaw += speedH * Input.GetAxis("Mouse X");
            pitch -= speedV * Input.GetAxis("Mouse Y");


        }

        float zoomDist = Vector3.Distance(transform.position, target.position);

        zoomDist = zoomDist - Input.GetAxis("Mouse ScrollWheel") * speed;
        transform.position  = -transform.forward*zoomDist + target.position;
        transform.eulerAngles = new Vector3(pitch, yaw, 0);


    }
}
