using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositioner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Grid grid;
    public void PlacePlayer()
    {
        transform.position = grid.startPos;
        transform.rotation = Quaternion.Euler(0,180,0);
        // Debug.Log("Call rotation.");
    }
}
