using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour 
{	
	public int life;
	[SerializeField]
	GameObject target;
	[SerializeField]
	LayerMask whoIsEnemy;
	[SerializeField]
	GameObject damagePrefab;

	[SerializeField]
	GameObject [] lifeImage;
	[SerializeField]
	GameObject[] bulletsImage;
	bool gameOver;
	bool feedbackImage;
	int count = 0;
	int shootCount = 4;
	bool isReloadin;

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
		StartCoroutine("Reload");
		while (true)
		{
			LifeManager();
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
			Debug.Log("Bullet " + shootCount);
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
		
		if(shootCount > count && !isReloadin)
		{
			if(Input.GetButtonDown("Fire1"))
			{
				shootCount--;
				SoundManager.instance.Play("Player", SoundManager.instance.clipList.shoot,1f);
				RaycastHit enemyCheck; 
				if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out enemyCheck,100,whoIsEnemy))
				{
				
					enemyCheck.collider.gameObject.GetComponent<EnemyControl>().TakeDamage();

				}
			}
		}
		if(!isReloadin)
		{
				if(shootCount == 4)
				{
					for (int i = 0; i < bulletsImage.Length; i++)
					{
						bulletsImage[i].SetActive(true);
					}
				}
				else if(shootCount == 3)
				{
					bulletsImage[3].SetActive(false);
				}
				else if(shootCount == 2)
				{
					bulletsImage[2].SetActive(false);
				}
				else if(shootCount == 1)
				{
					bulletsImage[1].SetActive(false);
				}
				else if(shootCount == 0)
				{
					bulletsImage[0].SetActive(false);
					
				}
		}
		

		if(shootCount == count)
		{
			isReloadin = true;
		}
		
	}

	IEnumerator Reload()
	{
		while (true)
		{
			yield return null;
			if(isReloadin)
			{
				yield return new WaitForSeconds(0.3f);
				shootCount++;
				if(shootCount == 1)
				{
					bulletsImage[0].SetActive(true);
				}
				if(shootCount == 2)
				{
					bulletsImage[1].SetActive(true);
				}
				if(shootCount == 3)
				{
					bulletsImage[2].SetActive(true);
				}
				if(shootCount == 4)
				{
					bulletsImage[3].SetActive(true);
				}
			
			}
				
				if(shootCount == 4)
				{
					isReloadin = false;
				}
		}

		
	}

	void LifeManager()
	{
		if(life == 4)
		{
			for (int i = 0; i < lifeImage.Length; i++)
			{
				lifeImage[i].SetActive(true);
			}
		}
		else if(life == 3)
		{
			lifeImage[3].SetActive(false);
		}
		else if(life == 2)
		{
			lifeImage[2].SetActive(false);
		}
		else if(life == 1)
		{
			lifeImage[1].SetActive(false);
		}
		else if(life == 0)
		{
			lifeImage[0].SetActive(false);
			StartCoroutine("GameOverManager");
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

	IEnumerator GameOverManager()
	{
		yield return new WaitForSeconds(3);
		SceneManager.LoadScene(0);
	}

	
}
