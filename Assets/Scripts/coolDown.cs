using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class coolDown : MonoBehaviour
{
    public Image dashCD;
    public float coolDown1 = 0.1f;
    bool isCooldown = false;
    public KeyCode LeftShift;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {dashCD.fillAmount = 0;
        dash();
    }
    void dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && isCooldown == false)
        {
            isCooldown = true;
            dashCD.fillAmount = 1;
        }
        if (isCooldown)
        {
            dashCD.fillAmount -= 1 / coolDown1 * Time.deltaTime;
            if (dashCD.fillAmount <= 0)
            {
                dashCD.fillAmount = 0;
                isCooldown = false;
            }
        }
    }
}
