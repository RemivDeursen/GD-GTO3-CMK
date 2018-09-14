using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_InteractablesCollider : MonoBehaviour {
    public Player_Data player_Data;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public Text keyCountText;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Pickupable>() != null)
        {
            if (other.GetComponent<Pickupable>().currentType == Pickupable.ItemType.Key)
            {
                Destroy(other.gameObject);
                player_Data.keyCount += 1;
                keyCountText.text = player_Data.keyCount.ToString();
                return;
            }
        }
    }
}
