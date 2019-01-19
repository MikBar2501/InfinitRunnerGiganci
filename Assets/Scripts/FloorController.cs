using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour {

	public GameObject floor1, floor2;
	public GameObject[] tiles;

	private void FixedUpdate() {
		if(!GameManager.instance.inGame) return;
		floor1.transform.position -= new Vector3(GameManager.instance.worldScrollingSpeed,0f,0f);
		floor2.transform.position -= new Vector3(GameManager.instance.worldScrollingSpeed,0f,0f);

		if(floor1.transform.position.x < -18f){
			var newTile = Instantiate(tiles[Random.Range(0,tiles.Length)],
			floor1.transform.position += new Vector3(60.82f,0f,0f),Quaternion.identity);
			newTile.transform.parent = gameObject.transform;
			Destroy(floor1);
			floor1 = floor2;
			floor2 = newTile;
		}
	}

}
