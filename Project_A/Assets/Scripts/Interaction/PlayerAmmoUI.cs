
using UnityEngine;
using TMPro;

public class PlayerAmmoUI : MonoBehaviour
{
    public Weapon Weapon; 
    public TextMeshProUGUI Ammotext; // TextMeshProUGUI�� ����

    void Start()
    {
        // �ʱ�ȭ ���� �ڵ�...
    }

    void Update()
    {

       Ammotext.text =  Weapon.curAmmo.ToString() + " / " + Weapon.maxAmmo.ToString();
    }
}
