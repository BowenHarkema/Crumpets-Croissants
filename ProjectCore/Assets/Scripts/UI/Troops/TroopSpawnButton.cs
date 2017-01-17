using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace AI_Core
{
    public class TroopSpawnButton : MonoBehaviour
    {

        [SerializeField]
        private float _CoolDownTimer;

        private float _ResetTimer;
        private bool _CoolingDown = false;
        private Image _TroopImage;
        private GameObject _Button;

        void Awake()
        {
            _ResetTimer = _CoolDownTimer;
        }

        void LateUpdate()
        {

            if (_CoolingDown)
            {
                _CoolDownTimer -= 1 * Time.deltaTime;
                _TroopImage.fillAmount = 1f - 1f / 5 * _CoolDownTimer;
                print(_TroopImage.fillAmount);
            }

            if (_CoolDownTimer <= 0)
            {
                _CoolingDown = false;
                _CoolDownTimer = _ResetTimer;
                _Button = null;
                _TroopImage = null;
            }
        }

        public void SpawnBakker()
        {
            if (_TroopImage == null)
            {
                _Button = GameObject.FindGameObjectWithTag("_Bakker");
                _TroopImage = _Button.GetComponent<Image>();
                _TroopImage.type = Image.Type.Filled;
                _CoolingDown = true;
               // new AlliedUnit();
            }
        }

        public void SpawnMusketman()
        {
            if (_TroopImage == null)
            {
                _Button = GameObject.FindGameObjectWithTag("_Musket");
                _TroopImage = _Button.GetComponent<Image>();
                _TroopImage.type = Image.Type.Filled;
                _CoolingDown = true;
            }
        }

        public void SpawnCavalry()
        {
            if (_TroopImage == null)
            {
                _Button = GameObject.FindGameObjectWithTag("_Cavalry");
                _TroopImage = _Button.GetComponent<Image>();
                _TroopImage.type = Image.Type.Filled;
                _CoolingDown = true;
            }
        }

        public void SpawnKanon()
        {
            if (_TroopImage == null)
            {
                _Button = GameObject.FindGameObjectWithTag("_Kanon");
                _TroopImage = _Button.GetComponent<Image>();
                _TroopImage.type = Image.Type.Filled;
                _CoolingDown = true;
            }
        }

        public void SpawnSwordsman()
        {
            if (_TroopImage == null)
            {
                _Button = GameObject.FindGameObjectWithTag("_Swordsman");
                _TroopImage = _Button.GetComponent<Image>();
                _TroopImage.type = Image.Type.Filled;
                _CoolingDown = true;
            }
        }
    }
}