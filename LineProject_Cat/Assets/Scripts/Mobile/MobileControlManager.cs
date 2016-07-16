using UnityEngine;
using System.Collections;

public class MobileControlManager : MonoBehaviour
{
	public MoveDirection Direction;

	private string HorizontalDirection;
	private string Jump;

	void OnMouseDown()
	{
		if (Direction == MoveDirection.Left)
		{
			HorizontalDirection = "Left";
			PlayerPrefs.SetString ("HorizontalDirection", HorizontalDirection);
		}
		else if (Direction == MoveDirection.Right)
		{
			HorizontalDirection = "Right";
			PlayerPrefs.SetString ("HorizontalDirection", HorizontalDirection);
		}

		if (Direction == MoveDirection.Jump)
		{
			Jump = "Jump";
			PlayerPrefs.SetString ("Jump", Jump);
		}
	}

	void OnMouseUp()
	{
		PlayerPrefs.SetString ("HorizontalDirection", "stop");
		PlayerPrefs.SetString ("Jump", "stop");
	}

	public enum MoveDirection
	{
		Right,
		Left,
		Jump
	}
}
