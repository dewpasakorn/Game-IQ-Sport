using UnityEngine;
using DG.Tweening;
using TMPro;

public class CoinResult : MonoBehaviour
{
    [SerializeField] private Coins_Bag coinsBag;
    [SerializeField] private Transform enterNameTransform;
    [SerializeField] private RectTransform coinsTransform;
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private ContinueArea continueArea;

    private int currentCoins = 0;

    private void Start()
    {
        OpenUI();
    }

    private void OnEnable()
    {
        continueArea.OnClickEvent += EndGUI;
    }

    private void OnDisable()
    {
        continueArea.OnClickEvent -= EndGUI;
    }

    void EndGUI()
    {
        gameObject.SetActive(false);
        enterNameTransform.gameObject.SetActive(true);
    }

    private void OpenUI()
    {
        Vector3 targetScale = coinsTransform.localScale;
        coinsTransform.localScale = Vector3.zero;

        coinsTransform
            .DOScale(targetScale, 1f)
            .SetEase(Ease.OutBack);


        DOTween.To(
            () => currentCoins,
            x =>
            {
                currentCoins = x;
                coinsText.text = currentCoins.ToString("N0");
            },
            coinsBag.GetCurrentCoins(),
            1f
        ).SetEase(Ease.OutQuad);
    }
}
