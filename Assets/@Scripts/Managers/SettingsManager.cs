using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class SettingsManager : Singleton<SettingsManager>
{
    private AudioManager _audioManager;
    private CinemachineManager _camManager;

    // Settings Audio Value
    private float _masterVolume;

    // Settings Control Value
    private bool _mouseReverse;
    private float _fov;
    private float _sensitivity;

    public override bool Initialize()
    {
        if (!base.Initialize()) return false;

        _audioManager = AudioManager.Instance;
        _camManager = CinemachineManager.Instance;

        // TODO: JSON이나 PlayerPrefs에서 저장된 값 불러오기



        _masterVolume = JsonManager.Instance.userData.masterVolume;
        _mouseReverse = JsonManager.Instance.userData.mouseReverse;
        _fov = JsonManager.Instance.userData.fov;
        _sensitivity = JsonManager.Instance.userData.sensitivity;
        JsonManager.Instance.SaveUserDataToJson();

        //_masterVolume = PlayerPrefs.GetFloat("Settings_MasterVolume", 100);
        //_mouseReverse = PlayerPrefs.GetInt("Settings_Inversion", 0) == 1;
        //_fov = PlayerPrefs.GetFloat("Settings_Fov", 90);
        //_sensitivity = PlayerPrefs.GetFloat("Settings_Sensitivity", 50);

        SetMasterVolume(_masterVolume);
        SetFOV(_fov);
        SetMouseSensitivity(_sensitivity);

        return true;
    }

    #region Audio Settings

    public float MasterVolume
    {
        get => _masterVolume;
        set => SetMasterVolume(value);
    }

    public bool SetMasterVolume(float volume)
    {
        if (_audioManager == null) return false;

        _audioManager.Source.volume = volume * 0.01f;
        _masterVolume = volume;
        JsonManager.Instance.userData.masterVolume = volume;
        JsonManager.Instance.SaveUserDataToJson();
        //    PlayerPrefs.SetFloat("Settings_MasterVolume", volume);

        return true;
    }

    #endregion

    #region Control Settings

    public bool MouseReverse 
    { 
        get => _mouseReverse; 
        set => SetReverse(value); 
    }

    public float FOV
    {
        get => _fov;
        set => SetFOV(value);
    }

    public float MouseSensitivity
    {
        get => _sensitivity;
        set => SetMouseSensitivity(value);
    }

    public bool SetReverse(bool reverse)
    {
        if (_camManager.PovExtension == null)
            return false;

        _mouseReverse = reverse;
        _camManager.PovExtension.MouseInversion = reverse;


        JsonManager.Instance.userData.mouseReverse = reverse;
        JsonManager.Instance.SaveUserDataToJson();
        // PlayerPrefs.SetInt("Settings_Inversion", _mouseReverse ? 1 : 0);

        return true;
    }

    public bool SetFOV(float fov)
    {
        if (_camManager.Vcam == null || _camManager.WeaponCam == null)
            return false;

        _fov = fov;
        _camManager.DefaultFOV = fov;

        JsonManager.Instance.userData.fov = fov;
        JsonManager.Instance.SaveUserDataToJson();
        // PlayerPrefs.SetFloat("Settings_Fov", fov);

        return true;
    }

    public bool SetMouseSensitivity(float sensitivity)
    {
        if (_camManager.PovExtension == null)
            return false;

        _sensitivity = sensitivity;
        _camManager.PovExtension.MouseSensitivity = sensitivity;

        JsonManager.Instance.userData.sensitivity = sensitivity;
        JsonManager.Instance.SaveUserDataToJson();
        // PlayerPrefs.SetFloat("Settings_Sensitivity", sensitivity);

        return true;
    }

    #endregion
}