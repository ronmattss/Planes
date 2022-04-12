using System;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class UIEvent : MonoBehaviour
    {
      

        private void OnEnable()
        {
            this.GetComponent<TMP_Text>().text = "" + SaveManager.Instance.GetCurrency();
            Debug.Log("Why you not Updating");

        }
    }
}