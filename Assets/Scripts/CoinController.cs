using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour {

	private Transform _player;
	private GameManager _gm;

	// Use this for initialization
	void Start () {
		_player = GameObject.FindGameObjectWithTag("Player").transform;
		_gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(!_gm.magnetActive) return;

		if(Vector3.Distance(transform.position, _player.position) < _gm.magnetDistance) {
			var direction = (_player.position - transform.position).normalized;
			transform.position += direction * _gm.magnetSpeed;
		}
	}
}
