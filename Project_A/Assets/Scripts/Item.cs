using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Type { Ammo, Heart, Weapon }; // ������ �ƴ� �ϳ��� Ÿ���̱� ������ ������ �Ʒ��� ���� ����
    public Type type; // �� ������ � ���� �Ǵ� ������ ��Ÿ����. ���� ���, �������� ������ ��Ÿ���ų� �پ��� ������ ������ ���� �� �ִ�.
    public int value;
}