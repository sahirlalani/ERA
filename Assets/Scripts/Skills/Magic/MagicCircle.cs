using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCircle : MonoBehaviour
{
    public Color OriginalColor, FireMagicCircleColor, IceMagicCircleColor, LightningMagicCircleColor;
    public GameObject player;
    public List<GameObject> Spells;
    //PlayerController playerController;
    public Transform pos;
    public SpriteRenderer MagicCircleSprite;

    // Start is called before the first frame update
    void Start()
    {
        MagicCircleSprite = GetComponent<SpriteRenderer>();
        //playerController = player.GetComponent<PlayerController>();
        //this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public IEnumerator FireMagic()
    //{
        //.color = FireMagicCircleColor;
        //this.gameObject.SetActive(true);
        //playerController._isCasting = true;

        //yield return new WaitForSeconds(2f);

        //this.gameObject.SetActive(false);
        //playerController._isCasting = false;
    //}

    public void FireMagicCircle()
    {
        MagicCircleSprite.color = FireMagicCircleColor;
    }

    public void IceMagicCircle()
    {
        MagicCircleSprite.color = IceMagicCircleColor;
    }

    public void ResetMagicCircle()
    {
        MagicCircleSprite.color = OriginalColor;
    }

}
