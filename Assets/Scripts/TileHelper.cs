using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileHelper : MonoBehaviour {

	public Tile chosenTile;
	public Tilemap tileMap;

	private Vector3Int previous;
	private ArrayList tilemapArray;

	public void Start(){

		BoundsInt bounds = tileMap.cellBounds;
		TileBase[] allTiles = tileMap.GetTilesBlock (bounds);

		for (int x = 0; x < bounds.size.x; x++) {
			for (int y = 0; y < bounds.size.y; y++) {
				TileBase tile = allTiles [x + y * bounds.size.x];
				if (tile != null) {
					Debug.Log ("x:" + x + " y:" +y + " tile:" + tile.name); 
				} else {
					Debug.Log ("x:" + x + " y:" +y + " tile: (null)");
				}
			}
		}
		//foreach( Tile t in tileMap){
		//	Debug.Log ("tile " + t);
		//}
	}
		
	public void SwapTiles(string dir,Vector3 playerPos){
		//(1 or -1 )here
		//get current grid location
		Vector3Int currentCell = tileMap.WorldToCell(playerPos);
		//Vector3Int currentCell = tileMap
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
			tileMap.SetTile (currentCell, chosenTile);
			//erase previous
			//highlightMap.SetTile (previous, null);
			//save the new position for next frame
			previous = currentCell;
		} else if (currentCell == previous) {
			//erase tile on second hit
			tileMap.SetTile (previous, null);
		}
	}
}
/*
 * using a composite collider attached with tilecollision2d-notes!
 * reduces call time  by eliminating nested colliders.
 * However, at fast speeds the player can pass through collider line and get stuck?
 * A solution is to enable geometry type:polygons in composite collider!
 * 
 * 
 * */
