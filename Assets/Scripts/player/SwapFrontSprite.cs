using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SwapFrontSprite : MonoBehaviour {

	public Tile highlightTile;
	public Tilemap highlightMap;

	private Vector3Int previous;

//do late so that the player has a chance to move in update if necessary
	//private void LateUpdate(){
	public void SwapTiles(string dir){
		//(1 or -1 )here
		//get current grid location
		Vector3Int currentCell = highlightMap.WorldToCell(transform.position);
		//add one in a direction(change this later)
		if (dir == "left") {
			currentCell.x -= 1;
		} else if (dir == "right") {
			currentCell.x += 1;
		}

		//if the position has changed
		if (currentCell != previous) {
			Debug.Log ("swap");
			//set the new tile
			highlightMap.SetTile (currentCell, highlightTile);
			//erase previous
			//highlightMap.SetTile (previous, null);
			//save the new position for next frame
			previous = currentCell;
		}
	}

	public void HitTile(string direction){
		//call function in tileHelper / tilemap
		FindObjectOfType<TileHelper>().SwapTiles(direction,transform.position);
		Debug.Log ("hit tile()");
	}
}
