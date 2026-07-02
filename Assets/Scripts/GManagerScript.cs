using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GManagerScript : MonoBehaviour {

	public Tilemap darkMap;
	public Tilemap blurMap;
	public Tilemap foregroundMap;
	public Tilemap backgroundMap;

	public Tile darkTile;
	public Tile blurTile;

	public bool isShrunk;

	public GameObject maskObject;

	// Use this for initialization
	void Start () {

		darkMap.origin = blurMap.origin = backgroundMap.origin;
		darkMap.size = blurMap.size = backgroundMap.size;

		foreach (Vector3Int p in darkMap.cellBounds.allPositionsWithin) {

			darkMap.SetTile (p, darkTile);

		}

		foreach (Vector3Int p in blurMap.cellBounds.allPositionsWithin) {

			blurMap.SetTile (p, blurTile);
		}


	}
	
	// Update is called once per frame
	void Update () {
		if (isShrunk) {
			ChangeTileSize (2);
			isShrunk = false;
		} else {
			//ChangeTileSize (1);
		}
	}

	void ChangeTileSize(int num){
		//looking to change cellsize during runtime?
		//Debug.Log("pixel size"+darkTile.sprite.pixelsPerUnit);
		//Debug.Log(foregroundMap.cellSize + " is the cellsize of tilemap foreground");
		//foregroundMap.cellSize.Set (10, 10, 0);
		foregroundMap.color = Color.red;//works
		//foregroundMap.cellGap.Set (1, 1, 0);
		//foreach (Vector3Int p in foregroundMap.cellBounds.allPositionsWithin) {
			//foregroundMap.EditorPreviewFloodFill (p, darkTile);//works
			//foregroundMap.SetTile(p, darkTile);//works
			//darkMap.SetTile (p, darkTile);//works
			//foregroundMap.cellSize.Set(10.0,10.0,0);
			//p.Set(10,10,0);
			//foregroundMap.cellBounds.size.Set( 10,10,0);
			//p.Scale(0.5);

		//}
		Debug.Log(foregroundMap.cellSize + " is the cellsize of tilemap foreground");
	}

}
