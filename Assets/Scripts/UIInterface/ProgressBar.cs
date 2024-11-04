using UnityEngine;
using UnityEngine.UI;

namespace UIElements
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Image _imgFiller;
        [SerializeField] private Text _textValue;

        public void SetValue(float valueNormalized)
        {
            var valueInPercent = Mathf.RoundToInt(valueNormalized * 100f);

            _textValue.text = $"{valueInPercent}%";
            _imgFiller.fillAmount = valueNormalized;
        }
    }
}