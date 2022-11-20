using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace ShowText
{
    public class TextManager : MonoBehaviour
    {
        public static System.Action OnCallText;

        TMP_Text _txtObj;

        Canvas _canvas;

        [Tooltip("ADICIONE ITENS PARA APRESENTAR VARIOS TEXTOS SEGUIDOS")]
        [SerializeField] string[] _dialogs;

        int _txtNumber = 0;

        private void Start()
        {
            _canvas = GetComponentInChildren<Canvas>();
            _canvas.worldCamera = FindObjectOfType<Camera>();

            _txtObj = GetComponentInChildren<TMP_Text>();
        }

        void Talking()
        {
            GetComponent<Animator>().SetTrigger("SHOW");

            if (_txtNumber < _dialogs.Length)
            {
                _txtObj.GetComponentInChildren<TMP_Text>().text = _dialogs[_txtNumber];

                _txtNumber++;
            }
            else
            {
                Destroy(this.gameObject);
            }

            GetComponent<Animator>().SetTrigger("HIDE");
        }

        private void OnEnable()
        {
            OnCallText += Talking;
        }
        private void OnDisable()
        {
            OnCallText -= Talking;
        }
    }
}


