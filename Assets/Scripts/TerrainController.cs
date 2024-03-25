using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.Scripting;
using UnityEngine.Playables;

using System.Threading.Tasks;

public class TerrainController : MonoBehaviour
{
    public Terrain terrain;
    public Terrain treeterrain;
    public GameObject treeobj;

    public GameDirector director;

    int i = 0;
    List<TreeInstance> trees;

    public GameObject LoadUI;
    public Image Image;

    public bool endTerrainCreate = false;
    [SerializeField]private GameStateController stateController;

    // Start is called before the first frame update
     void Start()
    {
        treeterrain.gameObject.SetActive(false);
        trees = new List<TreeInstance>(terrain.terrainData.treeInstances);


        


    }

    private  void Update()
    {
        if (director.gameState == GameDirector.GameState.Load)
        {
            for (int j = 0; j < 30; j++)
            {
                CreateMap();
            }
        }


 



    }

   
    private void CreateMap()
    {
        i++;
        if (i < treeterrain.terrainData.treeInstanceCount)
        {


            TreeInstance treeInstance = treeterrain.terrainData.treeInstances[i];
            


            GameObject capsule = Instantiate(treeobj);
            //capsule.layer = 1<<17;
            capsule.transform.position = Vector3.Scale(treeInstance.position, terrain.terrainData.size) + Terrain.activeTerrain.transform.position;
            capsule.transform.parent = terrain.transform;
            capsule.transform.localScale = new Vector3(treeInstance.widthScale, treeInstance.heightScale, treeInstance.widthScale);
            capsule.transform.rotation = Quaternion.Euler(0, treeInstance.rotation * Mathf.Rad2Deg, 0);



            if (i % 1000 == 0)
            {
                GarbageCollector.CollectIncremental(nanoseconds: 0UL);
            }

            Image.fillAmount = (float)i / treeterrain.terrainData.treeInstanceCount;

        }
        else
        {
            
            terrain.terrainData.treeInstances = trees.ToArray();
           
            endTerrainCreate = true;
        }
    }
    public async UniTask<bool> endCreate()
    {
        while (true)
        {
            if (endTerrainCreate)
            {
                break;
            }
            await Task.Delay(100);
        }
        return true;
    }


   

}
