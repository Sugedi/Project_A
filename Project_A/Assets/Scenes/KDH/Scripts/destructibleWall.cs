//using System.Collections;
//using UnityEngine;

//public class destructibleWall : MonoBehaviour
//{
//    public bool wallisDamage; // �������� ���� ���� ����
//    public int wallHealth = 10;
//    public MeshRenderer[] wallmeshs; // SkinnedMeshRenderer �迭

//    public GameObject wall80Per;
//    public GameObject wall50Per;
//    public GameObject wall10Per;

//    void Awake()
//    {
//        wallmeshs = GetComponentsInChildren<MeshRenderer>(); // MeshRenderer �迭 ��������
//    }
//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.tag == "Bullet")
//        {
//            wallHealth -= 1;
//            StartCoroutine(WallOnDamage());
//            if (wallHealth < 0)
//                {
//                DestroyWall();
//                }

//        }

//    }


//    IEnumerator WallOnDamage()
//    {
//        // �ǰ� ���¸� Ȱ��ȭ�ϰ� �޽����� ������ �Ķ������� ����
//        wallisDamage = true; // �ǰ� ���� Ȱ��ȭ

//        // ������ ȿ���� ���� ���� ����
//        float wallblinkDuration = 1f; // �����̴� �� �ð�
//        float wallblinkRate = 0.1f; // ������ ����
//        float wallelapsedTime = 0f; // ��� �ð�

//        // �����̴� ������ �ݺ� ����
//        while (wallelapsedTime < wallblinkDuration)
//        {
//            // �޽����� ������ �Ķ������� ����
//            foreach (MeshRenderer mesh in wallmeshs)
//            {
//                mesh.material.color = Color.white;
//            }

//            // ������ ���ݸ�ŭ ���
//            yield return new WaitForSeconds(wallblinkRate);

//            // �޽����� ������ ������� ����
//            foreach (MeshRenderer mesh in wallmeshs)
//            {
//                mesh.material.color = Color.red;
//            }

//            // �ٽ� ������ ���ݸ�ŭ ���
//            yield return new WaitForSeconds(wallblinkRate);

//            // ��� �ð� ������Ʈ
//            wallelapsedTime += wallblinkRate * 9;
//        }

//        // �ǰ� ���¸� ��Ȱ��ȭ�ϰ� �޽����� ������ ������� ����
//        wallisDamage = false;
//        foreach (MeshRenderer mesh in wallmeshs)
//        {
//            mesh.material.color = Color.white;
//        }
//    }




//    private void DestroyWall()
//    {
//        Destroy(gameObject);
//    }






//}


using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class destructibleWall : MonoBehaviour
{
    public int wallHealth = 10;

    // Child components
    public GameObject wall80Per;
    public GameObject wall50Per;
    public GameObject wall10Per;

    // Blinking variables
    private bool wallisDamage = false;
    private Renderer[] wallRenderers;

    void Start()
    {
        // Initialize the state based on the initial wall health
        UpdateWallComponents();
        wallRenderers = GetComponentsInChildren<Renderer>();
        SetChildComponentsActive(wall80Per, true);
        SetChildComponentsActive(wall50Per, false);
        SetChildComponentsActive(wall10Per, false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            wallHealth -= 1;

            // Update the wall components based on the new health value
            UpdateWallComponents();

            if (wallHealth <= 0)
            {
                DestroyWall();
            }
            else
            {
                // Trigger the blinking effect
                StartCoroutine(WallOnDamage());
            }
        }
    }

    private void UpdateWallComponents()
    {
        // Update the child components based on the wall health
        if ( wallHealth <= 5 && wallHealth > 0)
        {
            SetChildComponentsActive(wall80Per, false);
            SetChildComponentsActive(wall50Per, true);
            SetChildComponentsActive(wall10Per, false);
        }
        else if (wallHealth <= 0)
        {
            SetChildComponentsActive(wall80Per, false);
            SetChildComponentsActive(wall50Per, false);
            SetChildComponentsActive(wall10Per, true);
        }
    }

    private void SetChildComponentsActive(GameObject childObject, bool active)
    {
        // Set the specified child object and its children active or inactive
        childObject.SetActive(active);
    }

    IEnumerator WallOnDamage()
    {
        // �ǰ� ���¸� Ȱ��ȭ�ϰ� �ڽ� �޽����� ������ �Ķ������� ����
        wallisDamage = true; // �ǰ� ���� Ȱ��ȭ

        // ������ ȿ���� ���� ���� ����
        float wallblinkDuration = 1f; // �����̴� �� �ð�
        float wallblinkRate = 0.1f; // ������ ����
        float wallelapsedTime = 0f; // ��� �ð�
                                    // Store the original material color

        // Store the original material color
        List<Color> originalColors = new List<Color>();
        List<Material> originalMaterials = new List<Material>();

        foreach (Transform child in transform)
        {
            MeshRenderer childMesh = child.GetComponent<MeshRenderer>();

            if (childMesh != null)
            {
                originalColors.Add(childMesh.material.color);
                Material originalMaterial = new Material(childMesh.material);
                originalMaterial.CopyPropertiesFromMaterial(childMesh.material); // Deep copy
                originalMaterials.Add(originalMaterial);
            }
        }

        // �����̴� ������ �ݺ� ����
        while (wallelapsedTime < wallblinkDuration)
        {
            // �ڽ� �޽����� ������ �Ķ������� ����
            //foreach (Transform child in transform)
            //{
            //    MeshRenderer childMesh = child.GetComponent<MeshRenderer>();
            //    if (childMesh != null)
            //    {
            //        childMesh.material.color = Color.white;
            //    }
            //}

            // ������ ���ݸ�ŭ ���
            yield return new WaitForSeconds(wallblinkRate);

            // �ڽ� �޽����� ������ ������� ����
            foreach (Transform child in transform)
            {
                MeshRenderer childMesh = child.GetComponent<MeshRenderer>();
                if (childMesh != null)
                {
                    childMesh.material.color = Color.red;
                }
            }

            // �ٽ� ������ ���ݸ�ŭ ���
            yield return new WaitForSeconds(wallblinkRate);

            // ��� �ð� ������Ʈ
            wallelapsedTime += wallblinkRate * 9;
        }

        // �ǰ� ���¸� ��Ȱ��ȭ�ϰ� �ڽ� �޽����� ������ ���� ������ ����
        wallisDamage = false;
        for (int i = 0; i < transform.childCount; i++)
        {
            MeshRenderer childMesh = transform.GetChild(i).GetComponent<MeshRenderer>();
            if (childMesh != null)
            {
                childMesh.material.color = originalColors[i];
            }
        }
    }


    private void DestroyWall()
    {
        Destroy(gameObject);
    }
}


