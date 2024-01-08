using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Type { Ammo, Heart, Weapon }; // 변수가 아닌 하나의 타입이기 때문에 변수는 아래에 따로 생성
    public Type type; // 이 변수는 어떤 유형 또는 종류를 나타낸다. 예를 들어, 아이템의 유형을 나타내거나 다양한 종류의 정보를 담을 수 있다.
    public int value;
}