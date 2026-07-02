using System.Collections.Generic;//for Dictionary

//static class is great! easily accessable and editable.
//!developer note-new Unity Tile-editor still buggy, game build lose collision on tiles unless, textures are marked read/write enabled under advanced!

public static class PlayerStats  {

	private static int kills, points;
	private static bool isGameOver;
	private static string playerName;

	public static Dictionary<string, int> levelScoresDict = new Dictionary<string, int>()
	{
		{ "SceneA", 0},
		{ "SceneB", 0},
		{ "SceneC", 0},
        { "SceneF", 0},
        {"SceneE", 0 }
	};
	//[System.Serializable]
	//private struct LevelPoints
	//{
	//	public int levelNum;
	//	public int points;
	//}

	//public LevelPoints[] pointsArray;
	//public static LevelPoints[] levelPointArr;

	//public static LevelPoints LevelPointArr
	//{
		//get{
		//	return levelPointArr;
		//}
		//set{
		//	levelPointArr = value;
		//}
	//}

	public static int Kills
	{
		get{
			return kills; 
		}
		set{
			kills = value;
		}
	}
	public static int Points
	{
		get{
			return points;
		}
		set{
			points = value;
		}
			
	}
	public static bool IsGameOver
	{
		get{
			return isGameOver;
		}
		set{
			isGameOver = value;
		}

	}
	public static string PlayerName
	{
		get{
			return playerName;
		}
		set{
			playerName = value;
		}

	}
}
