using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// IDamageable �������̽��� �������� ���� �� �ִ� ������Ʈ�� �����մϴ�.
public interface IDamageable
{
    // TakeHit �޼���� �������� �浹 ������ �޾ƿͼ� ó���մϴ�.
    void TakeHit(float damage, RaycastHit hit);

}
