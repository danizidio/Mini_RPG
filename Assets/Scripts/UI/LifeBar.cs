using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    public delegate void UpdatingLifeBar(float currentLife, float maxLife);
    public UpdatingLifeBar onUpdateLifeBar;

    Animator anim;
    [SerializeField] Image _redBar, _whiteBar;

    [SerializeField] float whiteBarLossRate;

    private void Start()
    {
        onUpdateLifeBar += UpdateLifeBar;

        anim = this.GetComponent<Animator>();

        _redBar.fillAmount = 1;
        _whiteBar.fillAmount = 1;
    }

    public void UpdateLifeBar(float currentLife, float maxLife)
    {
        anim.SetTrigger("HIT");

        float value = currentLife / maxLife;

        _redBar.fillAmount = value;

        if (currentLife < 0)
        {
            _redBar.fillAmount = 0;
        }

        StartCoroutine(RedBarUpdate(value, currentLife, maxLife));
    }

    IEnumerator RedBarUpdate(float v, float currentLife, float maxLife)
    {
        yield return new WaitForSeconds(whiteBarLossRate);

        float value2 = currentLife / maxLife;

        _whiteBar.fillAmount = value2;

        if (value2 < 0)
        {
            value2 = 0;
        }
    }
}
