using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Molecule : MonoBehaviour {
	[HideInInspector] public int animState = 1;
	[HideInInspector] public Animator anim;
	private Rigidbody2D rb;

	private float frSpeed = 3;
	private float speed = 2;

	void Awake(){
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = new Vector3(Random.Range(1,speed), Random.Range(1,speed), 0);
	}

	void FixedUpdate(){
		switch(animState){
			case -1:
				rb.velocity = frSpeed * (rb.velocity.normalized);
			break;
			case 1:
				rb.velocity = speed * (rb.velocity.normalized);
			break;
		}
	}
}
