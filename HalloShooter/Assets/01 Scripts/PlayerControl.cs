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
	[SerializeField]
	GameObject damagePrefab;
	bool gameOver;
	bool feedbackImage;

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

			FeedBack();	
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
			SoundManager.instance.Play("Player", SoundManager.instance.clipList.shoot,1f);
			RaycastHit enemyCheck; 
			if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out enemyCheck,100,whoIsEnemy))
			{
				
				enemyCheck.collider.gameObject.GetComponent<EnemyControl>().TakeDamage();

			}
		}
	}
	void FeedBack()
	{
		if(!feedbackImage)
		{
			damagePrefab.SetActive(false);
		}
		else
		{
			damagePrefab.SetActive(true);
		}
	}
	private void OnTriggerStay(Collider other) 
	{
		if (other.gameObject.CompareTag("Enemy"))
		{
			feedbackImage = true;
		}	
		else
		{
			feedbackImage = false;
		}
			
	}



	
}
