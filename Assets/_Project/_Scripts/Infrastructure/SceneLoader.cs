using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
public class SceneLoader
{ //объеденить с Scene из Toolbar

    public void LoadScene( string sceneName )
    {
        if ( SceneManager.GetActiveScene().name == sceneName )
        {
            Debug.Log($"<color=red> same scene </color>");
            return;
        }

        SceneManager.LoadScene( sceneName );
    }
}
}