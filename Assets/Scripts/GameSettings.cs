using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    private readonly Dictionary<EPuzzleCategories, string> _puzzleCatDirectory =  new Dictionary<EPuzzleCategories, string>();
    private int _settings;
    private const int SettingsNumber = 2;
    private bool _muteFxPermanently = false;

    // enumerating number of tripples on the tswana Tripple matching game
    public enum EPairNumber
    {
        NotSet = 0,
        E10Pairs = 10,
        E15Pairs = 15,
        E20Pairs = 20,
    }

    // our game categories
    public enum EPuzzleCategories
    {
        NotSet,
        Fruits,
        Vegetables, 
    }

    // game settings, covering number of pairs & categories
    public struct Settings
    {
        public EPairNumber PairsNumber;
        public EPuzzleCategories PuzzleCategory;
    };

    private Settings _gameSettings;

    public static GameSettings Instance;

    // don't destroy script when loading new scene
    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(target:this);
            Instance = this;
        }
        else
        {
            Destroy(obj:this);
        }
    }

    // method to start the game, Reset game settings to the initial settings
    void Start()
    {
      SetPuzzleCatDirectory();
      _gameSettings = new Settings();
      ResetGameSettings(); 
    }

    private void SetPuzzleCatDirectory()
    {
        _puzzleCatDirectory.Add(EPuzzleCategories.Fruits, "Fruits");
        _puzzleCatDirectory.Add(EPuzzleCategories.Vegetables, "Vegetables");
    }

    // set number of pairs
    public void SetPairNumber(EPairNumber Number)
    {
        if ( _gameSettings.PairsNumber == EPairNumber.NotSet)
            _settings++;

        _gameSettings.PairsNumber = Number;
    }

    public void SetPuzzleCategories(EPuzzleCategories cat)
    {
        if ( _gameSettings.PuzzleCategory == EPuzzleCategories.NotSet)
            _settings++;

        _gameSettings.PuzzleCategory = cat;
    }

    // getter method for number of pairs
    public EPairNumber GetPairNumber()
    {
        return _gameSettings.PairsNumber;
    }

    // what category has been chosen
    public EPuzzleCategories GetPuzzleCategory()
    {
        return _gameSettings.PuzzleCategory;
    }

    // reset game settings, using category and pairs number
    public void ResetGameSettings()
    {
        _settings = 0;
        _gameSettings.PuzzleCategory = EPuzzleCategories.NotSet;
        _gameSettings.PairsNumber = EPairNumber.NotSet;
    }

    // check if we set all of the settings in the game
    public bool AllSettingsReady()
    {
        return _settings == SettingsNumber;
    }

    public string GetMaterialDirectoryName()
    {
        return "Materials/";
    }

    public string GetPuzzleCategoryTextureDirectoryName()
    {
        if (_puzzleCatDirectory.ContainsKey(_gameSettings.PuzzleCategory))
        {
            return "Graphics/PuzzleCat/" + _puzzleCatDirectory[_gameSettings.PuzzleCategory] + "/";
        }
        else
        {
            Debug.LogError("ERROR: CANNOT GET DIRECTORY NAME");
            return "";
        }
    }

    public void MuteSoundEffectPermanently(bool muted)
    {
        _muteFxPermanently = muted;
    }

    public bool IsSoundEffectMutedPermanently()
    {
        return _muteFxPermanently;
    }

}
