using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGameAreaTrigger : MonoBehaviour
{
    [SerializeField]
    private PropTransparenter _sellZoneWall;

    [SerializeField]
    private PropTransparenter _warehouseWall;

    private void Awake()
    {
        _sellZoneWall.UnRevealWithRendererDisable();
        _sellZoneWall.SetParams(0, 0.5f);
        _warehouseWall.SetParams(0, 0.5f);
    }

    private void OnTriggerEnter(Collider collider)
    {
        
        Vector3 dir = collider.transform.position - transform.position;
        Vector3 clearedDir = new Vector3(dir.x, 0, 0);
        clearedDir.Normalize();
        Debug.Log(clearedDir);

        if (clearedDir == Vector3.left)
        {
            _sellZoneWall.RevealWithRendererEnable();
            _warehouseWall.UnRevealWithRendererDisable();
        }
        else if(clearedDir == -Vector3.left)
        {
            _sellZoneWall.UnRevealWithRendererDisable();
            _warehouseWall.RevealWithRendererEnable();
        }
    }
}
