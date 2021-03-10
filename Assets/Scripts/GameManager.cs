using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

	public static GameManager instance;

	private AudioSource audio;

	private float volume = 1.0f;

	[SerializeField]
	private AudioClip rim_hit1, rim_hit2, bounce1, bounce2, net_sound;

	private BallCreator ballCreator;

	private int index = 0;

	private int balls = 10;

	void Awake()
	{
		MakeSingleton();
		audio = GetComponent<AudioSource>();
		ballCreator = GetComponent<BallCreator>();
	}

	void MakeSingleton()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	void OnLevelWasLoaded()
	{
		if (Application.loadedLevelName == "Gameplay")
		{
			CreateBall();
			GameObject.Find("Ball Text").GetComponent<Text>().text = "Balls " + balls;
		}
	}

	public void IncrementBalls(int increment)
	{
		balls += increment;

		if (balls > 10)
			balls = 10;

		GameObject.Find("Ball Text").GetComponent<Text>().text = "Balls " + balls;
	}

	public void DecrementBalls()
	{
		balls--;
		GameObject.Find("Ball Text").GetComponent<Text>().text = "Balls " + balls;
	}

	public void CreateBall()
	{
		ballCreator.CreateBall(index);
	}

	public void SetBallIndex(int index)
	{
		this.index = index;
	}

	public void PlaySound(int id)
	{

		switch (id)
		{

			case 1:
				audio.PlayOneShot(net_sound, volume);
				break;

			case 2:
				if (Random.Range(0, 2) > 1)
				{
					audio.PlayOneShot(rim_hit1, volume);
				}
				else
				{
					audio.PlayOneShot(rim_hit2, volume);
				}
				break;

			case 3:
				if (Random.Range(0, 2) > 1)
				{
					audio.PlayOneShot(bounce1, volume);
				}
				else
				{
					audio.PlayOneShot(bounce2, volume);
				}
				break;

			case 4:
				if (Random.Range(0, 2) > 1)
				{
					audio.PlayOneShot(bounce1, volume / 2);
				}
				else
				{
					audio.PlayOneShot(bounce2, volume / 2);
				}
				break;

			case 5:
				if (Random.Range(0, 2) > 1)
				{
					audio.PlayOneShot(rim_hit1, volume / 2);
				}
				else
				{
					audio.PlayOneShot(rim_hit2, volume / 2);
				}
				break;

		}

	}

} // GameManager







