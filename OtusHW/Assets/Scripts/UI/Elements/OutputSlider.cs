using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Elements
{
    public class OutputSlider: MonoBehaviour, IDisposable
    {
        [SerializeField] private RectTransform slider;
        [SerializeField] private TMP_Text valueOutput;
        [Space(5)]
        [SerializeField] private SliderSkinSwitcher skinSwitcher;
        [Space(10)] 
        [SerializeField] private float animateSpeed;
        
        private string _prefix;
        
        private Vector2 _maxSizeDelta;

        private IEnumerator _animation;
        
        private void Awake()
        {
            _maxSizeDelta = slider.sizeDelta;
        }

        public void SetPrefix(string prefix)
        {
            _prefix = prefix;
        }

        public void SetValue(float curValue, float maxValue, float removedValue)
        {
            float rate = Mathf.Clamp01( (curValue - removedValue) / (maxValue - removedValue));
            
            ChangeOutput(curValue, maxValue);
            ChangeRate(rate);
        }
        
        public void SetValue(float curValue, float maxValue)
        {
            float rate = Mathf.Clamp01(curValue / maxValue);
            
            ChangeOutput(curValue, maxValue);
            ChangeRate(rate);
        }
        
        public void Dispose()
        {
            if (_animation != null)
            {
                StopCoroutine(_animation);
                _animation = null;
            }
        }

        private void ChangeOutput(float curValue, float maxValue)
        {
            string output = _prefix != null ? $"{_prefix}: {curValue}/{maxValue}" : 
                $"{curValue} / {maxValue}";
            
            valueOutput.text = output;
        }
        
        private void ChangeRate(float rate)
        {
            Dispose();
            
            skinSwitcher.ChangeCompetedStatus(rate >= 1f);
            
            Vector2 nextSizeDelta = GetNextSizeDelta(rate);
            
            if (rate == 0f)
            {
                slider.sizeDelta = nextSizeDelta;
            }
            else
            {
                _animation = Animate();
                StartCoroutine(_animation);
            }

            IEnumerator Animate()
            {
                Vector2 curSizeDelta = slider.sizeDelta;
                
                while (Vector2.Distance(nextSizeDelta, curSizeDelta) > 0.01f)
                {
                    curSizeDelta = Vector2.MoveTowards(curSizeDelta, nextSizeDelta, animateSpeed * Time.deltaTime);
                    slider.sizeDelta = curSizeDelta;
                    
                    yield return null;
                }
            }
        }

        private Vector2 GetNextSizeDelta(float rate)
        {
            Vector2 nextSizeDelta = _maxSizeDelta;
            nextSizeDelta.x *= rate;
            
            return nextSizeDelta;
        }
        
        [Serializable]
        private sealed class SliderSkinSwitcher
        {
            [SerializeField] private Image sliderImage;
            [Space(5)]
            [SerializeField] private Sprite progressSlider;
            [SerializeField] private Sprite completedSlider;

            public void ChangeCompetedStatus(bool isCompleted)
            {
                sliderImage.sprite = isCompleted ? completedSlider : progressSlider;
            }
        }
    }
}