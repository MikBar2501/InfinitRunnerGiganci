using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float jumpForce;
	public float liftingForce;
	public bool jumped;
	public bool doubleJump;

	private Rigidbody2D rb;
	public float startingY;

	Collider2D collider;

	// Use this for initialization
	void Start () {
		collider = gameObject.GetComponent<Collider2D>();
		rb = gameObject.GetComponent<Rigidbody2D>();
		startingY = transform.position.y + 0.03f;
		collider.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(!GameManager.instance.inGame) {
			return;
		} else {
			if(jumped && transform.position.y <= startingY) {
				jumped = false;
				doubleJump = false;
			}

			if(Input.GetMouseButtonDown(0)) {
				
				if(!jumped) {
					rb.velocity = (new Vector2(0f, jumpForce));
					jumped = true;
					SoundManager.instance.PlayOnceJump();
				} else if (!doubleJump) {
					rb.velocity = (new Vector2(0f, jumpForce));
					doubleJump = true;
					SoundManager.instance.PlayOnceJump();
				}
			}

			if(Input.GetMouseButton(0) && rb.velocity.y < 0) {
				rb.AddForce(new Vector2(0f,liftingForce * Time.deltaTime));
			}
		}
	}


	public void PlayerDeath() {
		GameManager.instance.GameOver();
		collider.enabled = false;
		rb.AddForce(new Vector2(0f,200f));
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag("Obstacle") && !GameManager.instance.isImmortal) {
			PlayerDeath();
		} 	else if (other.CompareTag("Coin")) {
			GameManager.instance.CoinCollected();
			Destroy(other.gameObject);
		}	else if (other.CompareTag("Immortality")) {
			GameManager.instance.ImmortalityCollected();
			Destroy(other.gameObject);
		}	else if (other.CompareTag("Magnet")) {
			GameManager.instance.MagnetCollected();
			Destroy(other.gameObject);
		}
	}
}
