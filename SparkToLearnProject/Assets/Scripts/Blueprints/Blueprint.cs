﻿using UnityEngine;

public class Blueprint : MonoBehaviour
{
    [HideInInspector]
    public GameObject Object;

    [SerializeField]
    private StartCodeblock _startCodeblock;

    [SerializeField]
    private GameObject _codeblockPrefab;
    [SerializeField]
    private GameObject _whenCodeblockPrefab;

    [SerializeField]
    private Transform _codeblockParent;

    private CanvasGroup _canvasGroup;

    public Renderer _shaderRenderen;
    public Renderer _environmentRenderen;
    public bool _isProp;
    public bool _isEnvironment;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Hide();
        }
    }

    // Show and hide the blueprints using canvas group so code within can still run
    public void Show()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;
    }

    public void Hide()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;
    }

    /// <summary>
    /// Create an event codeblock
    /// </summary>
    public void InstantiateCodeblock()
    {
        GameObject codeblockInstance = Instantiate(_codeblockPrefab);
        codeblockInstance.transform.SetParent(_codeblockParent);
        codeblockInstance.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        codeblockInstance.transform.localScale = new Vector3(1f, 1f, 1);
        codeblockInstance.GetComponent<SetColorShader>().SetUpColorChanger(_shaderRenderen, _isEnvironment, _isProp, _environmentRenderen);
    }

    /// <summary>
    /// Create a when codeblock
    /// </summary>
    public void InstantiateWhenCodeblock()
    {
        GameObject codeblockInstance = Instantiate(_whenCodeblockPrefab);
        codeblockInstance.transform.SetParent(_codeblockParent);
        codeblockInstance.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        codeblockInstance.transform.localScale = new Vector3(1, 1, 1);
    }

    /// <summary>
    /// Start the blueprint
    /// </summary>
    public void ExecuteBlueprint()
    {
        _startCodeblock.ExecuteCodeblock();
    }
}
