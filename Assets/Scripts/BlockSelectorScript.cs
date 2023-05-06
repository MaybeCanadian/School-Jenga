using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BlockSelectorScript : MonoBehaviour
{
    #region Event Dispatchers
    public delegate void BlockSelectorEvent(PhysicalBlock block);
    public static BlockSelectorEvent OnBlockSelectionChanged;
    #endregion

    public static BlockSelectorScript instance;

    Camera mainCamera;
    public LayerMask blockLayerMask;
    private PhysicalBlock currentBlock = null;

    List<Button> uiButtons;
    private void Awake()
    {
        if(instance != this && instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            mainCamera = Camera.main;
        }
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (currentBlock != null)
            {
                currentBlock.DeselectBlock();
                currentBlock = null;
            }

            CheckForBlocks();

            OnBlockSelectionChanged?.Invoke(currentBlock);
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
