using UnityEngine;
using UnityEngine.UIElements;

namespace LlamAcademy
{
    [RequireComponent(typeof(PanelRenderer))]
    public class UIAudioFaderBridge : MonoBehaviour
    {
        [SerializeField] private AudioFader AudioFader;
        
        private PanelRenderer PanelRenderer;
        private Button ToggleMusicButton;
        private FloatField FadeDurationField;

        private void Awake()
        {
            PanelRenderer = GetComponent<PanelRenderer>();
        }

        private void OnEnable()
        {
            PanelRenderer.RegisterUIReloadCallback(HandleUIReload);
        }

        private void HandleUIReload(PanelRenderer panelRenderer, VisualElement rootElement, int _)
        {
            ToggleMusicButton = rootElement.Q<Button>();
            ToggleMusicButton.RegisterCallback<ClickEvent>(HandleToggleMusicClicked);

            FadeDurationField = rootElement.Q<FloatField>();
            FadeDurationField.RegisterCallback<ChangeEvent<float>>(HandleFadeDurationChange);
        }

        private void HandleFadeDurationChange(ChangeEvent<float> evt)
        {
            AudioFader.FadeDuration = evt.newValue;
        }

        private void HandleToggleMusicClicked(ClickEvent evt)
        {
            AudioFader.ToggleMusic();
        }
    }
}
