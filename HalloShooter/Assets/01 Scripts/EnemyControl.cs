using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControl : MonoBehaviour {
	[SerializeField]
	GameObject cam;
	[SerializeField]
	GameObject target;
	[SerializeField]
	GameObject enemySystemPrefab;
	public EnemyInfo enemyInfo;
	[HideInInspector]
	bool damageControl;
	PlayerControl player;
	EnemySpawnerControl enemySpawnerControl;
	NavMeshAgent agent;
	
	// Use this for initialization
	void Start () 
	{
		target = GameObject.FindGameObjectWithTag ("Player");
		cam = GameObject.FindGameObjectWithTag("MainCamera");
		enemySystemPrefab = GameObject.FindGameObjectWithTag("EnemyControl");
		player = cam.GetComponent<PlayerControl>();
		enemySpawnerControl = enemySystemPrefab.GetComponent<EnemySpawnerControl>();
		agent = GetComponent<NavMeshAgent>();
		
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
			Walk();
			
		}
	}


	void Walk()
	{
		if(this != null)
		{
			agent.SetDestination(target.transform.position);
		}
		
	}

	public void TakeDamage()
	{
		enemyInfo.life--;
		transform.Translate(new Vector3(0,0,-0.5f));
		if(enemyInfo.life <= 0)
		{
			Destroy(gameObject);
			enemySpawnerControl.spawnerControl--;
		}
	}

	private void OnTriggerStay(Collider other) 
	{
		if(other.gameObject.CompareTag("MainCamera"))
		{

			
			if(!damageControl)
			{
				StartCoroutine("Attack");
			}
			
		}			
	}
	
	IEnumerator Attack()
	{
		player.life--;
		damageControl = true;
		yield return new WaitForSeconds(2f);
		damageControl = false;
	}

	

}

[System.Serializable]
public class EnemyInfo
{
	public string type;
	public int life;
	public int strenght;
}
