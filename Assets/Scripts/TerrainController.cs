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

    // Start is called before the first frame update
    async void Start()
    {
        treeterrain.gameObject.SetActive(false);
        //await UniTask.WhenAll(LoadMap());
        trees = new List<TreeInstance>(terrain.terrainData.treeInstances);


/*        for (i = 0; i < 3000; i++)
        {


            TreeInstance treeInstance = treeterrain.terrainData.treeInstances[i];
            print(i);


            GameObject capsule = Instantiate(treeobj);
            //capsule.layer = 1<<17;
            capsule.transform.position = Vector3.Scale(treeInstance.position, terrain.terrainData.size) + Terrain.activeTerrain.transform.position;
            capsule.transform.parent = terrain.transform;
            capsule.transform.localScale = new Vector3(0.5f, treeInstance.heightScale, 0.5f);
            capsule.transform.rotation = Quaternion.Euler(0, treeInstance.rotation * Mathf.Rad2Deg, 0);

            terrain.terrainData.treeInstances[i] = new TreeInstance();
            trees[i] = new TreeInstance();

            if (i % 1000 == 0)
            {
                GarbageCollector.CollectIncremental(nanoseconds: 0UL);
            }



        }*/

        


    }

    private async void Update()
    {
        if (director.gameState == GameDirector.GameState.Load)
        {
            for (int j = 0; j < 30; j++)
            {
                CreateMap();
            }
/*            CreateMap();
            CreateMap();
            CreateMap();
            CreateMap();
            CreateMap();
            CreateMap();
            CreateMap();
            CreateMap();
            CreateMap();
            CreateMap();
            CreateMap();
            CreateMap();
            CreateMap();
            CreateMap();
            CreateMap();
            CreateMap();
            CreateMap();
            CreateMap();
            CreateMap();
            CreateMap();
            CreateMap();
            CreateMap();*/
        }


        //await UniTask.WhenAll(CraeateMapTask(), CraeateMapTask(), CraeateMapTask(), CraeateMapTask(), CraeateMapTask(), CraeateMapTask(), CraeateMapTask());



    }

    private async UniTask LoadMap()
    {
        
        treeterrain.gameObject.SetActive(false);
        await UniTask.SwitchToThreadPool();
        List<TreeInstance> trees = new List<TreeInstance>(terrain.terrainData.treeInstances);
        for (i = 0; i < terrain.terrainData.treeInstanceCount; i++)
        {


            TreeInstance treeInstance = treeterrain.terrainData.treeInstances[i];
            


            GameObject capsule = Instantiate(treeobj);
            //capsule.layer = 1<<17;
            capsule.transform.position = Vector3.Scale(treeInstance.position, terrain.terrainData.size) + Terrain.activeTerrain.transform.position;
            capsule.transform.parent = terrain.transform;
            capsule.transform.localScale = new Vector3(0.5f, treeInstance.heightScale, 0.5f);
            capsule.transform.rotation = Quaternion.Euler(0, treeInstance.rotation * Mathf.Rad2Deg, 0);

            terrain.terrainData.treeInstances[i] = new TreeInstance();
            trees[i] = new TreeInstance();

            if (i % 1000 == 0)
            {
                GarbageCollector.CollectIncremental(nanoseconds: 0UL);
            }



        }
        terrain.terrainData.treeInstances = trees.ToArray();
        
        await UniTask.SwitchToMainThread();
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

            //terrain.terrainData.treeInstances[i] = new TreeInstance();
            //trees[i] = new TreeInstance();

            if (i % 1000 == 0)
            {
                GarbageCollector.CollectIncremental(nanoseconds: 0UL);
            }

            Image.fillAmount = (float)i / treeterrain.terrainData.treeInstanceCount;

        }
        else
        {
            
            terrain.terrainData.treeInstances = trees.ToArray();
           
            //LoadUI.SetActive(false);
            LoadUI.GetComponent<PlayableDirector>().Play();
        }
    }

    private async UniTask CraeateMapTask()
    {
        await UniTask.SwitchToThreadPool();
        i++;
        if (i < terrain.terrainData.treeInstanceCount)
        {


            TreeInstance treeInstance = treeterrain.terrainData.treeInstances[i];
            


            GameObject capsule = Instantiate(treeobj);
            //capsule.layer = 1<<17;
            capsule.transform.position = Vector3.Scale(treeInstance.position, terrain.terrainData.size) + Terrain.activeTerrain.transform.position;
            capsule.transform.parent = terrain.transform;
            capsule.transform.localScale = new Vector3(0.5f, treeInstance.heightScale, 0.5f);
            capsule.transform.rotation = Quaternion.Euler(0, treeInstance.rotation * Mathf.Rad2Deg, 0);

            terrain.terrainData.treeInstances[i] = new TreeInstance();
            trees[i] = new TreeInstance();

            if (i % 1000 == 0)
            {
                GarbageCollector.CollectIncremental(nanoseconds: 0UL);
            }



        }
        else
        {

            terrain.terrainData.treeInstances = trees.ToArray();
           if(director.gameState == GameDirector.GameState.Load)
            {
                director.gameState = GameDirector.GameState.Start;
            }
        }

        await UniTask.SwitchToMainThread();
    }


    public void EndLoad()
    {
        LoadUI.SetActive(false);
        director.gameState = GameDirector.GameState.Start;
    }

}
