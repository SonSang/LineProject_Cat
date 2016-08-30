using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CatSelectToMenu : MonoBehaviour
{
	public void GoToMenu()
	{
		Debug.Log ("Clicked");
		SceneManager.LoadScene ("Title Menu");
	}
}
