using Cysharp.Threading.Tasks;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlaySoundTask: EventTask
    {
        private readonly AudioClip _audioClip;
        private readonly bool _withWait;

        public PlaySoundTask(AudioClip audioClip, bool withWait)
        {
            _audioClip = audioClip;            
            _withWait = withWait;
        }
        
        protected override void OnStart()
        {
            PlayClip().Forget();
        }

        private async UniTask PlayClip()
        {
            while (AudioPlayer.Instance == null)
            {
                await UniTask.Yield();
            }

            float curTime = 0f;
            float maxTime = _audioClip.length;

            AudioPlayer.Instance.PlaySound(_audioClip);

            if (_withWait == true)
            {
                while (curTime < maxTime)
                {
                    curTime += Time.deltaTime;
                    await UniTask.Yield();
                }
            }
            Complete();
        }
    }
}