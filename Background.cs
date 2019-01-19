using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {

    [SerializeField] float speed = 0.1f;
    Material material;
    Vector2 offset;


	// Use this for initialization
	void Start () {

        material = GetComponent<Renderer>().material;
        offset = new Vector2(0, speed);
        
		
	}
	
	// Update is called once per frame
	void Update () {
        material.mainTextureOffset += offset * Time.deltaTime;

    }
}
