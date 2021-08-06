using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInGame : MonoBehaviour
{
    public OptionMenu optionMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("`"))
        {
            optionMenu.gameObject.SetActive(!optionMenu.gameObject.activeSelf);
        }
    }
}
