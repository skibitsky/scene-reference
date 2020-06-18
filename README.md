# SceneReference [![openupm](https://img.shields.io/npm/v/com.skibitsky.scene-reference?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.skibitsky.scene-reference/)

> Custom property that allows to reference scene asset directly from the Editor

<img src="https://github.com/skibitsky/SceneReference/raw/master/.github/Images/screenshot.png" width="580">

## Usage

```csharp
public class SceneLoader : MonoBehaviour
{
    [SerializeField] private SceneReference sceneToLoad;

    private void Start()
    {
        SceneManager.LoadSceneAsync(sceneToLoad);
    }
}
```

## Installation

### Install via OpenUPM

The package is available on the [openupm registry](https://openupm.com). It's recommended to install it via [openupm-cli](https://github.com/openupm/openupm-cli).

```
openupm add com.skibitsky.scene-reference
```

### Install via Git URL

Open *Packages/manifest.json* with your favorite text editor. Add the following line to the dependencies block.

    {
        "dependencies": {
            "com.skibitsky.scene-reference": "https://github.com/skibitsky/SceneReference.git"
        }
    }

Notice: Unity Package Manager records the current commit to a lock entry of the *manifest.json*. To update to the latest version, change the hash value manually or remove the lock entry to resolve the package.

    "lock": {
      "com.skibitsky.scene-reference": {
        "revision": "master",
        "hash": "..."
      }
    }

