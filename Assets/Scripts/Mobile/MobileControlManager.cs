using UnityEngine;
using System.Collections;

public class MobileControlManager : MonoBehaviour
{
    static string firstTouch;
    static string secondTouch;

	void Update()
	{
		if (Input.touchCount == 0)
		{
			PlayerPrefs.SetString ("HorizontalDirection", "stop");
			PlayerPrefs.SetString ("Jump", "stop");
            PlayerPrefs.SetString ("Interact", "false");
            PlayerPrefs.SetString("Action", "false");
            firstTouch = "none";
            secondTouch = "none";
        }

		else if (Input.touchCount > 0)
		{
			Vector3 ray0;
			Vector3 ray1;

			if (Input.GetTouch (0).phase == TouchPhase.Began)
			{
				ray0 = Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position);
				Vector2 touchpos = new Vector2 (ray0.x, ray0.y);

				if (GetComponent<Collider2D> () == Physics2D.OverlapPoint (touchpos))
				{
                    firstTouch = GetComponent<Collider2D>().name;
					if (GetComponent<Collider2D> ().name == "Left")
					{
						PlayerPrefs.SetString ("HorizontalDirection", "Left");
					}
					else if (GetComponent<Collider2D> ().name == "Right")
					{
						PlayerPrefs.SetString ("HorizontalDirection", "Right");
                    }
                    else if (GetComponent<Collider2D> ().name == "Jump")
					{
						PlayerPrefs.SetString ("Jump", "Jump");
                    }
                    else if (GetComponent<Collider2D>().name == "Interact")
                    {
                        PlayerPrefs.SetString("Interact", "true");
                    }
                    else if (GetComponent<Collider2D>().name == "Action")
                    {
                        PlayerPrefs.SetString("Action", "true");
                    }
                    else if (GetComponent<Collider2D>().name == "Pause")
                    {
                        if (PlayerPrefs.GetString("Pause") == "true")
                            PlayerPrefs.SetString("Pause", "false");
                        else
                            PlayerPrefs.SetString("Pause", "true");
                    }
                }
			}
            else if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                if (firstTouch == "Left" || firstTouch == "Right")
                    PlayerPrefs.SetString("HorizontalDirection", "stop");
                else if (firstTouch == "Jump")
                    PlayerPrefs.SetString("Jump", "stop");
                else if (firstTouch == "Action")
                    PlayerPrefs.SetString("Action", "false");
            }

            if (Input.GetTouch (1).phase == TouchPhase.Began)
			{
				ray1 = Camera.main.ScreenToWorldPoint (Input.GetTouch (1).position);
				Vector2 touchpos = new Vector2 (ray1.x, ray1.y);

				if (GetComponent<Collider2D> () == Physics2D.OverlapPoint (touchpos))
				{
                    secondTouch = GetComponent<Collider2D>().name;
                    if (GetComponent<Collider2D>().name == "Left")
                    {
                        PlayerPrefs.SetString("HorizontalDirection", "Left");
                    }
                    else if (GetComponent<Collider2D>().name == "Right")
                    {
                        PlayerPrefs.SetString("HorizontalDirection", "Right");
                    }
                    else if (GetComponent<Collider2D> ().name == "Jump")
					{
						PlayerPrefs.SetString ("Jump", "Jump");
					}
                    else if (GetComponent<Collider2D>().name == "Pause")
                    {
                        if (PlayerPrefs.GetString("Pause") == "true")
                            PlayerPrefs.SetString("Pause", "false");
                        else
                            PlayerPrefs.SetString("Pause", "true");
                    }
                    else if (GetComponent<Collider2D>().name == "Action")
                    {
                        PlayerPrefs.SetString("Action", "true");
                    }
                }
            }
			else if (Input.GetTouch (1).phase == TouchPhase.Ended)
			{
                if (firstTouch == "Jump" || firstTouch == "Action")
                    PlayerPrefs.SetString("HorizontalDirection", "stop");

                PlayerPrefs.SetString ("Jump", "stop");
                PlayerPrefs.SetString("Action", "false");
                secondTouch = "none";
            }
		}
	}

/*	public MoveDirection Direction;

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
	}*/
}
