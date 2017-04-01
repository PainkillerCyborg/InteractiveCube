using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ColorSlider : MonoBehaviour
{

	public Image colorDisplay;
	public Slider slideColor;
	public Color32 chosenColor;
	public float r, g, b;
	public CubeFunctions cubeFunctionManager;
	void Start ()
	{
		chosenColor = Color.red;
		colorDisplay.color = chosenColor;
	}

	/// <summary>
	/// Function used by Slider's value change event.
	/// </summary>
	public void SetColor ()
	{
		float val = slideColor.value;
		if (val <= 256f) {
			chosenColor.r = (byte)255f;
			r = 255f;
			g = val;
			b = 0;
			chosenColor.g = (byte)val;
			chosenColor.b = 0;
			colorDisplay.color = chosenColor;
		} else if (val > 256 && val <= 512) {
			r = 255f - (val - 256);
			g = 255;
			b = 0;
			chosenColor.r = (byte)r;
			chosenColor.g = (byte)255f;
			chosenColor.b = 0;
			colorDisplay.color = chosenColor;
		} else if (val > 512 && val <= 768) {
			chosenColor.r = 0;
			chosenColor.g = (byte)255f;
			r = 0;
			g = 255;
			b = val - 512;
			chosenColor.b = (byte)(val - 512);
			colorDisplay.color = chosenColor;
		} else if (val > 768) {
			chosenColor.r = 0;
			r = 0;
			g = 256f - (val - 768);
			b = 255;
			chosenColor.g = (byte)g;
			chosenColor.b = (byte)255f;
			colorDisplay.color = chosenColor;
		}
		AllFunctionsSwitch ();
	}

	/// <summary>
	/// Normalized value of the Slider between 0 & 1
	/// </summary>
	/// <returns>The normalize value.</returns>
	float SliderNormalizeValue ()
	{
		float slideVal = 0f;
		slideVal = (slideColor.value - 0) / 1024;
		return slideVal;
	}

	/// <summary>
	/// Acts as a switch between all the modules. Only function to be called by the Slider's event function, rest is taken care within it.
	/// </summary>
	public void AllFunctionsSwitch ()
	{
		if (cubeFunctionManager.isFireAndIceActive) {
			cubeFunctionManager.DanceFireAndIce ((1 - SliderNormalizeValue ()) * 5, slideColor.value, chosenColor);
		} else if (cubeFunctionManager.isSoundMixerActive) {
			cubeFunctionManager.SoundMixer (chosenColor.r, chosenColor.g, chosenColor.b);
		} else if (cubeFunctionManager.isWeatherActive) {

			float r = Mathf.Clamp (chosenColor.r, 50f, 255f);
			float g = Mathf.Clamp (chosenColor.g, 50f, 255f);
			float b = Mathf.Clamp (chosenColor.b, 50f, 255f);
			Color32 colorVal = new Color32 ((byte)r, (byte)g, (byte)b, (byte)1f);

			float r2 = Mathf.Clamp (chosenColor.r, 64f, 128f);
			float g2 = Mathf.Clamp (chosenColor.g, 64f, 128f);
			float b2 = Mathf.Clamp (chosenColor.b, 64f, 128f);
			Color32 lightColVal = new Color32 ((byte)r2, (byte)g2, (byte)b2, (byte)1f);

			if (SliderNormalizeValue () <= 0.5f) {
				float tempValue = 15 - (SliderNormalizeValue () * 0.5f);
				float tempSize = Mathf.Clamp (tempValue, 3f, 15f);
				float gravityVal = (SliderNormalizeValue () - 0) / 0.5f;
				colorVal.a = 10;
				cubeFunctionManager.InvokeWeather (SliderNormalizeValue () * 2f, 0, 2f, tempSize, gravityVal, colorVal, new Vector3 (270f, 0, 0), lightColVal);
			} else {
				float gravityVal = 1 - (SliderNormalizeValue () - 0.5f) / 0.5f;
				colorVal.a = 50;
				float tempLerp = (SliderNormalizeValue () - 0.5f) / 0.5f;
				cubeFunctionManager.InvokeWeather (1, tempLerp, 2f, 2f, gravityVal, colorVal, new Vector3 (90f, 0, 0), lightColVal);
			}
		} else if (cubeFunctionManager.isShapeShiftActive) {
			cubeFunctionManager.ShapeShift (SliderNormalizeValue ());
		} else if (cubeFunctionManager.isEarthActive) {
			cubeFunctionManager.EarthCube (SliderNormalizeValue ());
		} else if (cubeFunctionManager.isAlchemyActive) {
			cubeFunctionManager.Alchemist (slideColor.value);
		}
	}

	/// <summary>
	/// Dynamically sets the values for all respective modules in relation to the Slider's current value. Called on mouse-click in CubeFunctions script.
	/// </summary>
	public void DynamicInitializeAllFunctions ()
	{
		if (cubeFunctionManager.isFireAndIceActive) {
			cubeFunctionManager.DanceFireAndIce ((1 - SliderNormalizeValue ()) * 5, slideColor.value, chosenColor);
		} else if (cubeFunctionManager.isSoundMixerActive) {
			cubeFunctionManager.SoundMixer (chosenColor.r, chosenColor.g, chosenColor.b);
		} else if (cubeFunctionManager.isWeatherActive) {
			cubeFunctionManager.InvokeWeather (0f, 0f, 2f, 15f, 0, new Color32 (255, 50, 50, 10), new Vector3 (270f, 0, 0), new Color32 (128, 64, 64, 255));
		} else if (cubeFunctionManager.isEarthActive) {
			cubeFunctionManager.EarthCube (SliderNormalizeValue ());
		} else if (cubeFunctionManager.isShapeShiftActive) {
			cubeFunctionManager.ShapeShift (0f);
		} else if (cubeFunctionManager.isAlchemyActive) {
			cubeFunctionManager.Alchemist (slideColor.value);
		}
	}

}