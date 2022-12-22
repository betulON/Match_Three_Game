using UnityEngine;
using System.Collections;

public class Board : MonoBehaviour {

	public int width;
	public int height;
	public float borderSize;

	public GameObject tilePrefab;
	public GameObject[] gamePiecePrefabs;

	Tile[,] _allTiles;
	GamePiece[,] _allGamePieces;

	void Start () 
	{
		_allTiles = new Tile[width,height];
		_allGamePieces = new GamePiece[width,height];
		
		SetupTiles();
		SetupCamera();
		FillRandom();
	}
	
	void SetupTiles()
	{
		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				GameObject tile = Instantiate (tilePrefab, new Vector3(i, j, 0), Quaternion.identity) as GameObject;

				tile.name = "Tile (" + i + "," + j + ")";

				_allTiles[i,j] = tile.GetComponent<Tile>();

				tile.transform.parent = transform;
				
				_allTiles[i,j].Init(i, j, this);

			}
		}
	}

	void SetupCamera()
	{
		// Understand this
		Camera.main.transform.position = new Vector3((float)(width-1) / 2f, (float)(height-1) / 2f, -10f);
		float aspectRatio = (float)Screen.width / (float)Screen.height;
		float verticalSize = (float)height / 2f + borderSize;
		float horizontalSize = ((float)width / 2f + borderSize) / aspectRatio;
		
		Camera.main.orthographicSize = verticalSize > horizontalSize ? verticalSize : horizontalSize;

	}

	void FillRandom()
	{
		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				GameObject randomPiece = Instantiate(GetRandomGamePiece(), Vector3.zero, Quaternion.identity);
				
				if (randomPiece != null)
				{
					PlaceGamePiece(randomPiece.GetComponent<GamePiece>(), i , j);
				}
			}
		}
	}

	GameObject GetRandomGamePiece()
	{
		int randomIndex = Random.Range(0, gamePiecePrefabs.Length);

		if (gamePiecePrefabs[randomIndex] == null)
		{
			Debug.LogWarning("The game piece object is NULL!");
		}
		return gamePiecePrefabs[randomIndex];
	}

	void PlaceGamePiece(GamePiece gamePiece, int x, int y)
	{
		if (gamePiece == null)
		{
			Debug.LogWarning("The game piece object is NULL!");
		}

		var transformGamePiece = gamePiece.transform;
		transformGamePiece.position = new Vector3(x, y, 0);
		transformGamePiece.rotation = Quaternion.identity;
		gamePiece.setCoordinates(x, y);
	}


}
