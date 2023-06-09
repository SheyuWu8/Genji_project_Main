using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ammo : MonoBehaviour
{
    public int ammoMax = 24;
    public int ammo;
    public bool isFiring;
    public bool Empty;
    public float reloadTime = 2f;
    public Text ammoDisplay;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        ammoDisplay.text = ammo.ToString();




        if (Input.GetMouseButtonDown(0) && !isFiring && ammo > 0)
        { 
            Empty = false;
            isFiring = true;
            ammo -= 3;
            isFiring = false;
        }   
        if(ammo == 0)
        {
            Empty = true;
            Reload();
        }
        
        
      
    }

    public void Reload()
    {
        int reloadAmount = ammoMax - ammo;
        ammo += reloadAmount;
    }
    
    
}
