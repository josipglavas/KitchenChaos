using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class OptionsUI : MonoBehaviour {

    public static OptionsUI Instance { get; private set; }

    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAltButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button gamepadInteractButton;
    [SerializeField] private Button gamepadInteractAltButton;
    [SerializeField] private Button gamepadPauseButton;

    [SerializeField] private TextMeshProUGUI soundEffectText;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactAltText;
    [SerializeField] private TextMeshProUGUI pauseText;
    [SerializeField] private TextMeshProUGUI gamepadInteractText;
    [SerializeField] private TextMeshProUGUI gamepadInteractAltText;
    [SerializeField] private TextMeshProUGUI gamepadPauseText;

    [SerializeField] private Transform pressToRebindKeyTransform;

    private Action onCloseButtonAction;

    private void Awake() {
        Instance = this;
        soundEffectsButton.onClick.AddListener(() => {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        musicButton.onClick.AddListener(() => {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();

        });

        backButton.onClick.AddListener(() => {
            Hide();
            onCloseButtonAction();
        });

        moveUpButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.Move_Up);
        });

        moveDownButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.Move_Down);
        });

        moveLeftButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.Move_Left);
        });

        moveRightButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.Move_Right);
        });

        interactButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.Interact);
        });

        interactAltButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.InteractAlt);
        });

        pauseButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.Pause);
        });

        gamepadInteractButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.GamePad_Interact);
        });

        gamepadInteractAltButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.GamePad_InteractAlt);
        });

        gamepadPauseButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.GamePad_Pause);
        });
    }

    private void Start() {
        GameManager.Instance.OnGameUnPaused += GameManager_OnGameUnPaused;
        UpdateVisual();

        HidePressToRebindKey();
        Hide();
    }

    private void GameManager_OnGameUnPaused(object sender, System.EventArgs e) {
        Hide();
    }

    private void UpdateVisual() {
        soundEffectText.text = "Sound effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);

        moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        interactAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlt);
        pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
        gamepadInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamePad_Interact);
        gamepadInteractAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamePad_InteractAlt);
        gamepadPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamePad_Pause);
    }

    public void Show(Action onCloseButtonAction) {
        this.onCloseButtonAction = onCloseButtonAction;
        gameObject.SetActive(true);

        soundEffectsButton.Select();
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    private void ShowPressToRebindKey() {
        pressToRebindKeyTransform.gameObject.SetActive(true);
    }

    private void HidePressToRebindKey() {
        pressToRebindKeyTransform.gameObject.SetActive(false);
    }

    private void RebindBinding(GameInput.Binding binding) {
        ShowPressToRebindKey();
        GameInput.Instance.RebindBinding(binding, () => {
            HidePressToRebindKey();
            UpdateVisual();
        });
    }
}