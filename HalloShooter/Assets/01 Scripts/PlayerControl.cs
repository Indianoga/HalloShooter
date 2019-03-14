using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour 
{
	[SerializeField]
	GameObject target;
	[SerializeField]
	LayerMask whoIsEnemy;
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
			TargetControl();
			ShootControl();
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
				Destroy(enemyCheck.collider.gameObject);

			}
		}
	}
}
