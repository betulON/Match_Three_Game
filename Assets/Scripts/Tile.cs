using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour
{
	public int _xIndex;
	public int _yIndex;

	private Board _board;

	public void Init(int xIndex, int yIndex, Board board)
	{
		_xIndex = xIndex;
		_yIndex = yIndex;
		_board = board;
	}
}
