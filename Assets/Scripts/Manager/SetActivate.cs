using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class SetActivate : MonoBehaviour
    {
        [SerializeField] private GameObject _guiObject;

        void Start()
        {
            if (_guiObject == null)
            {
                Debug.LogError("GUI Object is not assigned in GuiActivator.");
                return;
            }

            _guiObject.SetActive(false);
        }

        void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player") && !_guiObject.activeSelf)
            {
                _guiObject.SetActive(true);
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (_guiObject != null)
            {
                _guiObject.SetActive(false);
            }
        }
    }

}
