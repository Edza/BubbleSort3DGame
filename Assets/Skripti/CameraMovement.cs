using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    public GameObject Player;
    private Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = transform.position - Player.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = Player.transform.position + offset;
	}
}
