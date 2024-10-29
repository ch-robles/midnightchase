using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapFollowCharacter : MonoBehaviour
{
	public Transform characterReference;
	public float characterOffset = 100f;

	private void Update()
	{
		if(characterReference != null)
		{
			transform.position = new Vector3(characterReference.position.x, characterReference.position.y + characterOffset, characterReference.position.z);
		}
	}
}
