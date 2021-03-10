using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour
{

	private Animator mainAnim, ballAnim;

	void Awake()
	{
		mainAnim = GameObject.Find("Main Holder").GetComponent<Animator>();
		ballAnim = GameObject.Find("Balls Holder").GetComponent<Animator>();
	}

	public void PlayGame()
	{
		Application.LoadLevel("GamePlay");
	}

	public void SelectBall()
	{
		mainAnim.Play("FadeOut");
		ballAnim.Play("FadeIn");
	}

	public void BackToMenu()
	{
		mainAnim.Play("FadeIn");
		ballAnim.Play("FadeOut");
	}


} // MainMenuController



