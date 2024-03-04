using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour
{
    public Terrain terrain;
    public Terrain treeterrain;
    public GameObject treeobj;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < terrain.terrainData.treeInstanceCount; i++)
        {
            

            TreeInstance treeInstance = treeterrain.terrainData.treeInstances[i];


            GameObject capsule = Instantiate(treeobj);
            //capsule.layer = 1<<17;
            capsule.transform.position = Vector3.Scale(treeInstance.position, terrain.terrainData.size) + Terrain.activeTerrain.transform.position;
            capsule.transform.parent = terrain.transform;
            capsule.transform.localScale = new Vector3(0.5f, treeInstance.heightScale, 0.5f);
            capsule.transform.rotation = Quaternion.Euler(0, treeInstance.rotation * Mathf.Rad2Deg, 0);

        }
        List<TreeInstance> trees = new List<TreeInstance>(terrain.terrainData.treeInstances);
        for (int i = 0; i < terrain.terrainData.treeInstanceCount; i++)
        {
            
            terrain.terrainData.treeInstances[i] = new TreeInstance();
            trees[i] = new TreeInstance();
            
        }
        terrain.terrainData.treeInstances = trees.ToArray();
        treeterrain.gameObject.SetActive(false);
    }

}
