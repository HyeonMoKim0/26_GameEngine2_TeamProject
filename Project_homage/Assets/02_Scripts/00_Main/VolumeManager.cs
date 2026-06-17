using UnityEngine;
using UnityEngine.Audio; // 오디오 믹서 제어를 위해 필수 추가
using UnityEngine.UI;    // UI 슬라이더 연동을 위해 필수 추가

public class VolumeManager : MonoBehaviour
{
    public static VolumeManager Instance;

    [Header("Audio Mixer")]
    public AudioMixer audioMixer; // 인스펙터에서 MainMixer를 연결하세요.

    [Header("UI Sliders")]
    public Slider masterSlider;
    public Slider bgmSlider;
    public Slider sfxSlider;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        // 게임 시작 시, 이전에 저장된 볼륨 설정을 불러옵니다. (기본값은 최대 음량인 1f)
        SetSliderAndMixer("MasterVol", masterSlider, PlayerPrefs.GetFloat("MasterVol", 1f));
        SetSliderAndMixer("BGMVol", bgmSlider, PlayerPrefs.GetFloat("BGMVol", 1f));
        SetSliderAndMixer("SFXVol", sfxSlider, PlayerPrefs.GetFloat("SFXVol", 1f));

        // 슬라이더 값이 변경될 때 실행될 리스너(함수)를 연결합니다.
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    // --- 슬라이더 연동 함수들 ---

    public void SetMasterVolume(float value)
    {
        ChangeVolume("MasterVol", value);
    }

    public void SetBGMVolume(float value)
    {
        ChangeVolume("BGMVol", value);
    }

    public void SetSFXVolume(float value)
    {
        ChangeVolume("SFXVol", value);
    }

    // --- 내부 핵심 로직 함수들 ---

    /// <summary>
    /// 실제 오디오 믹서의 데시벨 값을 변경하고 기기에 저장하는 함수
    /// </summary>
    private void ChangeVolume(string parameterName, float sliderValue)
    {
        // 슬라이더 최소값(0)일 때는 오디오 믹서를 -80dB(음소거)로 만듭니다.
        if (sliderValue <= 0.0001f)
        {
            audioMixer.SetFloat(parameterName, -80f);
        }
        else
        {
            // 로그 스케일 공식 적용: 슬라이더 0~1 값을 데시벨(-40dB ~ 0dB) 범위로 선형 변환
            // 20f를 곱하면 음량 변화가 사람 귀에 가장 자연스럽게 들립니다.
            float dbValue = Mathf.Log10(sliderValue) * 20f;
            audioMixer.SetFloat(parameterName, dbValue);
        }

        // 앱이 꺼져도 유지되도록 로컬에 볼륨 값 저장
        PlayerPrefs.SetFloat(parameterName, sliderValue);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// 초기화할 때 UI 슬라이더 위치와 오디오 믹서 싱크를 맞춰주는 함수
    /// </summary>
    private void SetSliderAndMixer(string parameterName, Slider slider, float savedValue)
    {
        if (slider != null)
        {
            slider.minValue = 0f;
            slider.maxValue = 1f;
            slider.value = savedValue; // 슬라이더 UI 위치 변경 -> 리스너에 의해 ChangeVolume도 자동 실행됨
        }
    }
}