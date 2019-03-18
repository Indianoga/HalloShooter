using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour 
{	
	public int life;
	[SerializeField]
	GameObject target;
	[SerializeField]
	LayerMask whoIsEnemy;

	bool gameOver;
	

	// Use this for initialization
	void Start () 
	{
		StartCoroutine("Action");	
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}	

	IEnumerator Action()
	{
		while (true)
		{
			yield return null;
			if(!gameOver)
			{
				TargetControl();
				ShootControl();
			}
			if(life <= 0)
			{
				gameOver = true;
			}		
		}
	}

	void TargetControl()
	{
		Vector3 mousePos = Input.mousePosition;
		mousePos.z = 0.32f;
		target.transform.position = Camera.main.ScreenToWorldPoint(mousePos);
		Cursor.visible = false;
	}

	void ShootControl()
	{
		if(Input.GetButtonDown("Fire1"))
		{
			RaycastHit enemyCheck; 
			if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out enemyCheck,100,whoIsEnemy))
			{
				
				enemyCheck.collider.gameObject.GetComponent<EnemyControl>().TakeDamage();

			}
		}
	}

	private void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag("Enemy"))
		{
			
		}	
	}
}
