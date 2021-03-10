using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour
{

	private int touchedGround = 0;

	private bool touchedRam;

	void OnCollisionEnter2D(Collision2D target)
	{

		if (target.gameObject.tag == "Holder")
		{
			if (GameManager.instance != null)
			{
				if (Random.Range(0, 2) > 1)
				{
					GameManager.instance.PlaySound(3);
				}
				else
				{
					GameManager.instance.PlaySound(4);
				}
			}
		}

		if (target.gameObject.tag == "Ram")
		{
			touchedRam = true;
			if (GameManager.instance != null)
			{
				if (Random.Range(0, 2) > 1)
				{
					GameManager.instance.PlaySound(2);
				}
				else
				{
					GameManager.instance.PlaySound(5);
				}
			}
		}

		if (target.gameObject.tag == "Ground")
		{
			touchedGround++;

			if (touchedGround <= 3)
			{

				if (GameManager.instance != null)
				{
					if (Random.Range(0, 2) > 1)
					{
						GameManager.instance.PlaySound(3);
					}
					else
					{
						GameManager.instance.PlaySound(4);
					}
				}

			}

		}

		if (target.gameObject.tag == "Table")
		{
			touchedRam = true;
			if (GameManager.instance != null)
			{
				if (Random.Range(0, 2) > 1)
				{
					GameManager.instance.PlaySound(2);
				}
				else
				{
					GameManager.instance.PlaySound(5);
				}
			}
		}

	}

	void OnTriggerEnter2D(Collider2D target)
	{

		if (target.tag == "Net")
		{

			if (touchedRam)
			{
				GameManager.instance.IncrementBalls(1);
			}
			else
			{
				GameManager.instance.IncrementBalls(2);
			}

			if (GameManager.instance != null)
			{
				GameManager.instance.PlaySound(1);
			}
		}

	}


} // BallScript

