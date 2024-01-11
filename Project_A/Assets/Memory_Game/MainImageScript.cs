using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainImageScript : MonoBehaviour
{
    [SerializeField] private GameObject image_unknown;
    [SerializeField] private GameControllerScript gameController;
    bool isFliped = false;

    public void OnClick()
    {
        if (image_unknown.activeSelf && gameController.canOpen)
        {
            Vector3 originScale = transform.localScale;  
            Vector3 targetSclae = new Vector3(0f, originScale.y, originScale.z);

            
            transform.DOScale(targetSclae, 0.2f).OnComplete(() =>
            {
                image_unknown.SetActive(false);
                gameController.imageOpened(this);



                transform.DOScale(originScale, 0.2f);
            }
            );


            //image_unknown.SetActive(false);
            //gameController.imageOpened(this);
        }
    }

    private int _spriteId;
    public int spriteId
    {
        get { return _spriteId; }
    }

    public void ChangeSprite(int id, Sprite image)
    {
        _spriteId = id;
        GetComponent<Image>().sprite = image; //Gets the sprite renderer component to change the sprite.
    }

    public void Close()
    {
        Vector3 originScale = transform.localScale;
        Vector3 targetSclae = new Vector3(0f, originScale.y, originScale.z);
        transform.DOScale(targetSclae, 0.2f).OnComplete(() =>
        {
            image_unknown.SetActive(true);



            transform.DOScale(originScale, 0.2f);
        }
        );
        
    }

    public void GGo()
    {
        Vector3 originScale = new Vector3(1f, 1f, 1f);
        Vector3 targetSclae = new Vector3(1.1f, 1.1f, 1.1f);

        transform.DOScale(targetSclae, 0.2f).OnComplete(() =>
        {

            transform.DOScale(originScale, 0.2f);
        }
            );
    }
}
