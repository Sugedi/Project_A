//using System.Collections;
//using UnityEngine;

//public class destructibleWall : MonoBehaviour
//{
//    public bool wallisDamage; // 데미지를 받은 상태 여부
//    public int wallHealth = 10;
//    public MeshRenderer[] wallmeshs; // SkinnedMeshRenderer 배열

//    public GameObject wall80Per;
//    public GameObject wall50Per;
//    public GameObject wall10Per;

//    void Awake()
//    {
//        wallmeshs = GetComponentsInChildren<MeshRenderer>(); // MeshRenderer 배열 가져오기
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
//        // 피격 상태를 활성화하고 메쉬들의 색상을 파란색으로 변경
//        wallisDamage = true; // 피격 상태 활성화

//        // 깜빡임 효과를 위한 변수 설정
//        float wallblinkDuration = 1f; // 깜빡이는 총 시간
//        float wallblinkRate = 0.1f; // 깜빡임 간격
//        float wallelapsedTime = 0f; // 경과 시간

//        // 깜빡이는 동안의 반복 루프
//        while (wallelapsedTime < wallblinkDuration)
//        {
//            // 메쉬들의 색상을 파란색으로 변경
//            foreach (MeshRenderer mesh in wallmeshs)
//            {
//                mesh.material.color = Color.white;
//            }

//            // 깜빡임 간격만큼 대기
//            yield return new WaitForSeconds(wallblinkRate);

//            // 메쉬들의 색상을 흰색으로 변경
//            foreach (MeshRenderer mesh in wallmeshs)
//            {
//                mesh.material.color = Color.red;
//            }

//            // 다시 깜빡임 간격만큼 대기
//            yield return new WaitForSeconds(wallblinkRate);

//            // 경과 시간 업데이트
//            wallelapsedTime += wallblinkRate * 9;
//        }

//        // 피격 상태를 비활성화하고 메쉬들의 색상을 흰색으로 변경
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
        // 피격 상태를 활성화하고 자식 메쉬들의 색상을 파란색으로 변경
        wallisDamage = true; // 피격 상태 활성화

        // 깜빡임 효과를 위한 변수 설정
        float wallblinkDuration = 1f; // 깜빡이는 총 시간
        float wallblinkRate = 0.1f; // 깜빡임 간격
        float wallelapsedTime = 0f; // 경과 시간
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

        // 깜빡이는 동안의 반복 루프
        while (wallelapsedTime < wallblinkDuration)
        {
            // 자식 메쉬들의 색상을 파란색으로 변경
            //foreach (Transform child in transform)
            //{
            //    MeshRenderer childMesh = child.GetComponent<MeshRenderer>();
            //    if (childMesh != null)
            //    {
            //        childMesh.material.color = Color.white;
            //    }
            //}

            // 깜빡임 간격만큼 대기
            yield return new WaitForSeconds(wallblinkRate);

            // 자식 메쉬들의 색상을 흰색으로 변경
            foreach (Transform child in transform)
            {
                MeshRenderer childMesh = child.GetComponent<MeshRenderer>();
                if (childMesh != null)
                {
                    childMesh.material.color = Color.red;
                }
            }

            // 다시 깜빡임 간격만큼 대기
            yield return new WaitForSeconds(wallblinkRate);

            // 경과 시간 업데이트
            wallelapsedTime += wallblinkRate * 9;
        }

        // 피격 상태를 비활성화하고 자식 메쉬들의 색상을 원래 색으로 변경
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


