using UnityEngine;
using System.Collections;

public class GamePlayController : MonoBehaviour
{

	public void BackToMainMenu()
	{
		Application.LoadLevel("MainMenu");
	}

}
