using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Offset : MonoBehaviour
{

	public float RotationSpeedX;
	public float RotationSpeedY;

	private float OffsetX;
	private float OffsetY;
	private Renderer Render;

	void Start ()
	{

		Render = GetComponent<Renderer> ();

	}

	void FixedUpdate ()
	{

		OffsetX += 0.01f;
		OffsetY += 0.01f;

		Render.material.SetTextureOffset ("_MainTex", new Vector2 (OffsetX * RotationSpeedX, OffsetY * RotationSpeedY));
	    
	}
}