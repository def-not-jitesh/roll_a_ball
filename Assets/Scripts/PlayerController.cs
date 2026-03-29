using System.Collections;
using System.Collections.Generic; 
using Unity.VisualScripting; 
using UnityEngine;
using UnityEngine.InputSystem; 
using TMPro; 


public class PlayerController : MonoBehaviour {
	
	private Rigidbody rb; 
	private float movementX; 
	private float movementY; 
	private int count = 0; 

	public float speed = 0; 
	public TextMeshProUGUI countText;
       	public GameObject winTextObject;	

	void Start() {
		rb = GetComponent<Rigidbody>(); 
		setCountText(); 

		winTextObject.SetActive(false);
    	}

	void OnMove(InputValue movementValue) {
		Vector2 movementVector = movementValue.Get<Vector2>(); 

		movementX = movementVector.x;
		movementY = movementVector.y; 
	}

	void setCountText() {
		countText.text = "Count: " + count.ToString(); 
	}

	void FixedUpdate() {
		Vector3 movement = new Vector3 (movementX, 0.0f, movementY); 
		rb.AddForce(movement * speed); 
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("pickUp")) {
			other.gameObject.SetActive(false); 
			count = count + 1; 
			setCountText(); 

			if (count >= 6) {
				winTextObject.SetActive(true); 
			}
		}
	}

}
