using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPickup : MonoBehaviour2D {
    public Item item;

    private bool isEmpty;

    public AudioClip sound;
    LayerSort layerSort;

    public float waitTime = 1f;

    public void Start() {
        GetComponent<SpriteRenderer>().sprite = item.iconAnim.sprites[0];
        layerSort = GetComponent<LayerSort>();
    }

    private void OnValidate()
    {
        if (item != null)
        {
            GetComponent<SpriteRenderer>().sprite = item.iconAnim.sprites[0];
            name = item.ItemName;
        }
    }   

    float t = 0;
    public override void Update()
    {
        t += Time.unscaledDeltaTime * 5;
        float x = Mathf.Sin(t) * 0.004f;
        height2D += x * 2;
        //layerSort.offset -= Mathf.RoundToInt(((x * 2) * 65));
        transform.position = new Vector3(transform.position.x, transform.position.y + x, transform.position.z);
        base.Update();
    }

    public virtual void OnTriggerEnter2D(Collider2D collision) {
        Player p = collision.GetComponent<Player>();
        if (p != null) {
            Item itemCopy = item.GetCopy();
            if (p.GetComponent<TInv.InventoryObject>().AddItem(itemCopy))
            {
                p.ppn.Display(GetComponent<SpriteRenderer>().sprite);
                Instantiate(GameManager.instance.genericSoundObject, transform.position, Quaternion.identity).GetComponent<AudioSource>().clip = sound;
                Destroy(gameObject);
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision) {
    }
}
