using UnityEngine;
using System.Collections;


public class Animation_Controller : MonoBehaviour {
	
	private Animator animation_Controller;
	
	
	private float moveV = 0f;
	private float moveH = 0f;
	
	private Character_motion character_motion;
	
	// Use this for initialization
	void Start () {
		character_motion = GetComponent<Character_motion>();
		animation_Controller = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
		int moveHInt=0;
		int moveVInt=0;
		moveH = character_motion.moveH;
		moveV = character_motion.moveV;
		
		if (moveH > 0)moveHInt = 1;
		else if (moveH < 0)moveHInt = -1;
		
		
		if (moveV > 0)moveVInt = 1;
		else if (moveV < 0)moveVInt = -1;
		
		animation_Controller.SetInteger("player_H",moveHInt);
		animation_Controller.SetInteger("player_V",moveVInt);
		
	}
	
	void FixedUpdate(){

	}
}
