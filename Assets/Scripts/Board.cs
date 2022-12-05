using UnityEngine;
using System.Collections;

public class Board : MonoBehaviour {

	public int width;
	public int height;
	public float borderSize;

	public GameObject tilePrefab;

	Tile[,] _allTiles;

	void Start () 
	{
		_allTiles = new Tile[width,height];
		SetupTiles();
		SetupCamera();
	}
	
	void SetupTiles()
	{
		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				GameObject tile = Instantiate (tilePrefab, new Vector3(i, j, 0), Quaternion.identity) as GameObject;

				tile.name = "Tile (" + i + "," + j + ")";

				_allTiles [i,j] = tile.GetComponent<Tile>();

				tile.transform.parent = transform;


			}
		}
	}

	void SetupCamera()
	{
		// Understand this
		Camera.main.transform.position = new Vector3((float)(width-1) / 2f, (float)(height-1) / 2f, -10f);
		float aspectRatio = (float)Screen.width / (float)Screen.height;
		float verticalSize = (float)height / 2f + borderSize;
		float horizontalSize = (float)width / 2f + borderSize;
		
		Camera.main.orthographicSize = horizontalSize > verticalSize ? horizontalSize : verticalSize;
	}


}
