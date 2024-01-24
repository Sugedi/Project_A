
using UnityEngine;
using TMPro;

public class PlayerAmmoUI : MonoBehaviour
{
    public Weapon Weapon; 
    public TextMeshProUGUI Ammotext; // TextMeshProUGUI로 변경

    void Start()
    {
        // 초기화 등의 코드...
    }

    void Update()
    {

       Ammotext.text =  Weapon.curAmmo.ToString() + " / " + Weapon.maxAmmo.ToString();
    }
}
