using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DestructableWall : MonoBehaviour {

	public Sprite dmgSprite;
	public int hp = 2;

	//private SpriteRenderer spriteRenderer;
	public TilemapRenderer  tilemapRenderer;
	private Tilemap tilemap;
	//private Tile tile;
	// Use this for initialization
	void Start () {
		//spriteRenderer = GetComponent<SpriteRenderer> ();
		//tilemapRenderer = GetComponent<TilemapRenderer>();
		//tilemap = GetComponent<Tilemap>();
		//tile = GetComponent<Tile>();
	}
	
	public void DamageWall(int dmg,Vector2 pos){
		//spriteRenderer.sprite = dmgSprite;
		//tilemapRenderer.

		hp -= dmg;
		if (hp <= 0) {
			//deactivates all tiles on level!
			//gameObject.SetActive (false);
			//gameObject.GetComponent<Tile>().sprite = dmgSprite;
			//Debug.Log("wall damage "+gameObject.GetComponent<TileData>().ToString());
			//Debug.Log("wall"+tilemapRenderer.GetType());
			//Debug.Log("wall"+tilemap.name);
			//tilemap.SetTile (new Vector2Int (pos.x,pos.y), dmgSprite);
			//tilemap.SetTile(pos,dmgSprite);
			//tilemap.SetTile (new Vector3Int (pos.x,pos.y,0), dmgSprite);
		}
	}
}
