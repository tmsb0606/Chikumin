using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cysharp.Threading.Tasks;

public class Item : MonoBehaviour, ICarriable
{
    public enum Type
    {
        Wad,
        Jewelry,
        Rock,
        Branch,
        Car,
        Bike,
        Police,
        Clock,

    }
    // Start is called before the first frame update
    public Type itemType = Type.Wad;
    public List<GameObject> carryObjects = new List<GameObject>();
    public int minCarryNum = 1;
    public int maxCarryNum = 1;
    protected GameObject canvas;
    protected TextMeshProUGUI text;

    protected bool isGoal = false;

    private ItemEffectBase[] itemEffects;


    void Start()
    {
        canvas = Instantiate((GameObject)Resources.Load("Canvas"));
        
        canvas.transform.parent = this.transform;
        canvas.transform.localPosition = new Vector3(0, 0.5f, 0);

        //canvas = gameObject.transform.Find("Canvas").gameObject;
        text = canvas.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();

        itemEffects = GetComponents<ItemEffectBase>();
    }

    // Update is called once per frame
    void Update()
    {
        if(carryObjects.Count > 0)
        {
            canvas.SetActive(true);
            text.text = carryObjects.Count + "/" + minCarryNum;
        }

        if(canvas.active && carryObjects.Count == 0)
        {
            canvas.SetActive(false);
        }
    }
    public void Carry(GameObject gameObject)
    {
        carryObjects.Add(gameObject);
    }

    public void Effect()
    {
        if(itemEffects != null)
        {
            foreach(ItemEffectBase itemEffect in itemEffects)
            {
                itemEffect.Effect();
            }
            
        }
    }


    public async UniTask<bool> Animation()
    {
        float scalingRate = 0f;
        while (true)
        {
            this.transform.position += new Vector3(0, 0.1f, 0);
            if (transform.localScale.x >= 0)
            {
                transform.localScale -= new Vector3(scalingRate, scalingRate, scalingRate);
                scalingRate += 0.005f;

            }
            else
            {
                break;
            }
            await UniTask.Delay(1);
            if (isGoal)
            {
                break;
            }
        }
        return true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("enter");
        if (collision.gameObject.tag == "goalEnd")
        {
            isGoal = true; ;
        }
    }
}
