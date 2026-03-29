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

		if (count >= 6) {
			winTextObject.SetActive(true); 
			Destroy(GameObject.FindGameObjectWithTag("Enemy")); 
		}
	}

	void FixedUpdate() {

		if ((-10 <= rb.position.x) && (rb.position.x <= 10) && (-10 <= rb.position.z) && (rb.position.z <= 10)) {
			Vector3 movement = new Vector3 (movementX, 0.0f, movementY); 
			rb.AddForce(movement * speed);
		} else {
			rb.MovePosition(new Vector3(0, 0.5f, 0)); 
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("pickUp")) {
			other.gameObject.SetActive(false); 
			count = count + 1; 
			setCountText(); 

		}
	}

	private void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.CompareTag("Enemy")) {
			Destroy(gameObject); 
			winTextObject.gameObject.SetActive(true); 
			winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose"; 
		}
	}

}
