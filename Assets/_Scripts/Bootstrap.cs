using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] MonoBehaviour[] _initableScripts;

    private void Start()
    {
        foreach (var script in _initableScripts)
        {
            var init = script.GetComponent<IInitable>();

            if (init != null)
                init.Init();
            else
                Debug.LogError($"Not found IInitable in {script.gameObject}");
        }
    }
}
