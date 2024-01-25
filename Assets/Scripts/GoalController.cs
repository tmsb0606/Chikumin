using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GoalController : MonoBehaviour
{
    public int score = 0;
    public int num = 10;
    public int[] itemNum;
    private AudioSource audioSource;
    public AudioClip CoinSE;
    // Start is called before the first frame update
    void Start()
    {
        itemNum = new int[num];
        Array.Fill(itemNum, 0);
        audioSource = GameObject.Find("SoundDirector").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "item")
        {
            collision.gameObject.GetComponent<Item>().carryObjects[0].GetComponent<ChikuminBase>().carryObjectList[0] = null;
            collision.gameObject.transform.parent = null;

            collision.gameObject.SetActive(false);
            score += 1000000;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "item")
        {
            other.gameObject.GetComponent<Item>().carryObjects[0].GetComponent<ChikuminBase>().carryObjectList.Clear();
            itemNum[(int)other.gameObject.GetComponent<Item>().itemType] += 1;
            print(other.gameObject.GetComponent<Item>().itemType + ":" + itemNum[(int)other.gameObject.GetComponent<Item>().itemType]);
            other.gameObject.transform.parent = null;
            other.gameObject.SetActive(false);
            score += 1000000;
            audioSource.PlayOneShot(CoinSE);
        }
    }
}
