using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSelectorScript : MonoBehaviour
{
    Camera mainCamera;
    public LayerMask blockLayerMask;
    private PhysicalBlock currentBlock = null;
    private void Awake()
    {
        mainCamera = Camera.main;
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(currentBlock != null)
            {
                currentBlock.DeselectBlock();
            }

            CheckForBlocks();
        }
    }
    private void CheckForBlocks()
    {
        Vector2 mousePos = Input.mousePosition;

        Ray mouseRay = mainCamera.ScreenPointToRay(mousePos);

        RaycastHit hit;

        if(Physics.Raycast(mouseRay.origin, mouseRay.direction, out hit, 999, blockLayerMask))
        {
            PhysicalBlock block = hit.collider.GetComponent<PhysicalBlock>();

            if(block == null)
            {
                return;
            }

            block.SelectBlock();
            currentBlock = block;
        }
    }
}
