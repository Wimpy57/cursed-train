using Core.Scripts.Achievements;
using TMPro;
using UnityEngine;

namespace Core.Scripts.UI
{
    public class AchievementUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private Color _achievedColor;
        [SerializeField] private Color _unachievedColor;
        
        public void Initialize(Achievement achievement)
        {
            _title.text = AchievementConfig.AchievementInfo[achievement][0];
            if (PlayerPrefs.HasKey(((int)achievement).ToString()) && PlayerPrefs.GetInt(((int)achievement).ToString()) == 1)
            {
                _description.text = AchievementConfig.AchievementInfo[achievement][1];
                _title.color = _achievedColor;
                _description.color = _achievedColor;
            }
            else
            {
                _description.text = "???";
                _title.color = _unachievedColor;
                _description.color = _unachievedColor;
            }
        }
    }
}
