using UnityEngine;
using TwitchChat;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class LoginScreen : Screen 
{
    public CanvasGroup loadingGroup;

    public Image loadingIcon;
    public TMP_InputField channelNameInput;
    public Button channelButton;
    public GameObject channelText;

    public override void Initialize()
    {
        base.Initialize();

        loadingGroup.blocksRaycasts = false;
        loadingGroup.interactable = false;
        loadingGroup.alpha = 0;

        loadingIcon.transform.DORotate(new Vector3(0, 0, 360), 1f, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Restart);

        channelNameInput.onSubmit.AddListener(OnInputSubmit);
    }

    public override void Show()
    {
        base.Show();
        channelNameInput.Select();
    }

    private void OnInputSubmit(string value)
    {
        Connect();
    }

    public void ShowLoading()
    {
        loadingGroup.blocksRaycasts = true;
        loadingGroup.interactable = true;
        loadingGroup.DOFade(1, 0.2f)
            .OnComplete(() =>
            {
                channelNameInput.gameObject.SetActive(false);
                channelButton.gameObject.SetActive(false);
                channelText.gameObject.SetActive(false);
            });
    }

    public void Connect()
    {
        ShowLoading();
        channelNameInput.onSubmit.RemoveAllListeners();
        TwitchController.Login(channelNameInput.text.ToLower());
    }
}
