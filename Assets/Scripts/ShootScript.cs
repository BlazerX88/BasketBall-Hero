﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ShootScript : MonoBehaviour
{

	public float power = 2.0f;
	public float life = 1.0f;
	public float dead_sense = 25f;

	public int dots = 30;

	private Vector2 startPosition;
	private bool shoot = false, aiming = false, hit_ground = false;

	private GameObject Dots;
	private List<GameObject> projectilesPath;

	private Rigidbody2D myBody;
	private Collider2D myCollider;

	void Awake()
	{
		myBody = GetComponent<Rigidbody2D>();
		myCollider = GetComponent<Collider2D>();
	}

	void Start()
	{

		Dots = GameObject.Find("dots");
		myBody.isKinematic = true;
		myCollider.enabled = false;
		startPosition = transform.position;
		projectilesPath = Dots.transform.Cast<Transform>().ToList().ConvertAll(t => t.gameObject);
		for (int i = 0; i < projectilesPath.Count; i++)
		{
			projectilesPath[i].GetComponent<Renderer>().enabled = false;
		}
	}

	void Update()
	{
		Aim();

		if(hit_ground)
        {
			life -= Time.deltaTime;

			Color c = GetComponent<Renderer>().material.GetColor("_Color");
			GetComponent<Renderer>().material.SetColor("_Color", new Color(c.r, c.g, c.g, life));
		}

		if (life < 0)
		{

			if (GameManager.instance != null)
			{
				GameManager.instance.CreateBall();
			}

			Destroy(gameObject);
		}
	}

	void Aim()
	{
		if (shoot)
			return;

		if (Input.GetAxis("Fire1") == 1)
		{
			if (!aiming)
			{
				aiming = true;
				startPosition = Input.mousePosition;
				CalculatePath();
				ShowPath();
			}
			else
			{
				CalculatePath();
			}

		}
		else if (aiming && !shoot)
		{
			if (inDeadZone(Input.mousePosition) || inReleaseZone(Input.mousePosition))
			{
				aiming = false;
				HidePath();
				return;
			}
			myBody.isKinematic = false;
			myCollider.enabled = true;
			shoot = true;
			aiming = false;
			myBody.AddForce(GetForce(Input.mousePosition));
			HidePath();
			GameManager.instance.DecrementBalls();
		}
	}

	Vector2 GetForce(Vector3 mouse)
	{
		return (new Vector2(startPosition.x, startPosition.y) - new Vector2(mouse.x, mouse.y)) * power;
	}

	bool inDeadZone(Vector2 mouse)
	{
		if (Mathf.Abs(startPosition.x - mouse.x) <= dead_sense && Mathf.Abs(startPosition.y - mouse.y) <= dead_sense)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	bool inReleaseZone(Vector2 mouse)
	{
		if (mouse.x <= 70)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	void CalculatePath()
	{
		Vector2 vel = GetForce(Input.mousePosition) * Time.fixedDeltaTime / myBody.mass;
		for (int i = 0; i < projectilesPath.Count; i++)
		{
			projectilesPath[i].GetComponent<Renderer>().enabled = true;
			float t = i / 30f;
			Vector3 point = PathPoint(transform.position, vel, t);
			point.z = 1.0f;
			projectilesPath[i].transform.position = point;
		}
	}

	Vector2 PathPoint(Vector2 startP, Vector2 startVel, float t)
	{
		return startP + startVel * t + 0.5f * Physics2D.gravity * t * t;
	}

	void HidePath()
	{
		for (int i = 0; i < projectilesPath.Count; i++)
		{
			projectilesPath[i].GetComponent<Renderer>().enabled = false ;
		}
	}

	void ShowPath()
	{
		for (int i = 0; i < projectilesPath.Count; i++)
		{
			projectilesPath[i].GetComponent<Renderer>().enabled = true;
		}
	}

	void OnCollisionEnter2D(Collision2D target)
	{
		if (target.gameObject.tag == "Ground")
		{
			hit_ground = true;
		}
	}

} // ShootScript
