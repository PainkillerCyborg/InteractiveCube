using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CubeFunctions : MonoBehaviour
{
	public ParticleSystem fireAndIceParticles, weatherParticles;
	public AudioSource drumAudioSource, guitarAudioSource, synthAudioSource;
	public Renderer cubeFaceUp, cubeFaceDown, cubeFaceLeft, cubeFaceRight, cubeFaceFront, cubeFaceBack;
	public Material weatherPattern_Mat, symbolisms_Mat, cubeFaceUp_Mat, cubeFaceDown_Mat, cubeFaceLeft_Mat, cubeFaceRight_Mat, cubeFaceFront_Mat, cubeFaceBack_Mat, skyBox_Mat;
	public Material skyUp_Mat, skyDown_Mat, skyRight_Mat, skyLeft_Mat, skyFront_Mat, skyBack_Mat;
	public ColorSlider slider;
	public bool isFireAndIceActive, isSoundMixerActive, isWeatherActive, isShapeShiftActive, isEarthActive, isAlchemyActive;
	public Light directionalLight;
	public MeshFilter plane, diamond, triangle;
	public List<Vector3> planeData = new List<Vector3> ();
	public List<Vector3> diamondData = new List<Vector3> ();
	public List<Vector3> triangleData = new List<Vector3> ();

	void Start ()
	{
		for (int i=0; i<4; i++) {
			planeData [i] = plane.mesh.vertices [i];//Storing Vertices data for the shapeshifting module in a temporary variable used to reset the plane mesh back to normal.
		}
	}

	void Update ()
	{
		if (Input.GetMouseButtonUp (0)) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 40f));
			if (Physics.Raycast (ray, out hit)) {
				DeactivateAll ();//Makes sure other modules switch off before anything else starts.
				if (hit.collider.name == "Front") {
					ActivateFireAndIce (true);//Play Fire and Ice
					Debug.Log ("Front");
				} else if (hit.collider.name == "Back") {
					ActivateEarth (true);//Play Earth in Space
					Debug.Log ("Back");
				} else if (hit.collider.name == "Right") {
					ActivateSoundMixer (true);//Play Sound Mixer
					Debug.Log ("Right");
				} else if (hit.collider.name == "Left") {
					ActivateShapeShift (true);//Play Shape Shifting
					Debug.Log ("Left");
				} else if (hit.collider.name == "Up") {
					ActivateWeather (true);//Play Weather change
					Debug.Log ("Up");
				} else if (hit.collider.name == "Down") {
					ActivateAlchemy (true);//Play Alchemy symbols
					Debug.Log ("Down");
				}
				slider.DynamicInitializeAllFunctions ();// Dynamically sets the respective module's values in relation to the Slider's current value.
			}
		}
	}
	/// <summary>
	/// Deactivates all modules
	/// </summary>
	public void DeactivateAll ()
	{
		ActivateAlchemy (false);
		ActivateEarth (false);
		ActivateFireAndIce (false);
		ActivateShapeShift (false);
		ActivateSoundMixer (false);
		ActivateWeather (false);
	}

	/// <summary>
	/// Activates the fire and ice module
	/// </summary>
	public void ActivateFireAndIce (bool active)
	{
		isFireAndIceActive = active;
		if (active) {
			fireAndIceParticles.gameObject.SetActive (true);
		} else {
			fireAndIceParticles.gameObject.SetActive (false);
		}
	}

	/// <summary>
	/// Activates the sound mixer module
	/// </summary>

	public void ActivateSoundMixer (bool active)
	{
		isSoundMixerActive = active;
		if (active) {
			drumAudioSource.gameObject.SetActive (true);
			guitarAudioSource.gameObject.SetActive (true);
			synthAudioSource.gameObject.SetActive (true);
			drumAudioSource.Play ();
			guitarAudioSource.Play ();
			synthAudioSource.Play ();
		} else {
			cubeFaceRight_Mat.color = new Color (1, 1, 1, 1);
			drumAudioSource.gameObject.SetActive (false);
			guitarAudioSource.gameObject.SetActive (false);
			synthAudioSource.gameObject.SetActive (false);
			drumAudioSource.Stop ();
			guitarAudioSource.Stop ();
			synthAudioSource.Stop ();
		}
	}

	/// <summary>
	/// Activates the weather change module
	/// </summary>
	public void ActivateWeather (bool active)
	{
		isWeatherActive = active;
		if (active) {
			cubeFaceUp.GetComponent<Renderer> ().material = weatherPattern_Mat;
			weatherParticles.gameObject.SetActive (true);
			directionalLight.intensity = 1.5f;
		} else {
			cubeFaceUp.GetComponent<Renderer> ().material = cubeFaceUp_Mat;
			weatherParticles.gameObject.SetActive (false);
			directionalLight.transform.rotation = Quaternion.Euler (new Vector3 (20f, 330f, 1f));
			directionalLight.intensity = 1.5f;
			directionalLight.color = new Color32 (200, 200, 200, 200);
		}
	}

	/// <summary>
	/// Activates the shape shift module
	/// </summary>

	public void ActivateShapeShift (bool active)
	{
		isShapeShiftActive = active;
		if (active) {
			for (int i=0; i<4; i++) {
				diamondData [i] = diamond.mesh.vertices [i];
				triangleData [i] = triangle.mesh.vertices [i];
			}
		} else {
			plane.mesh.SetVertices (planeData);
		}
	}

	/// <summary>
	/// Activates the earth in space module
	/// </summary>
	public void ActivateEarth (bool active)
	{
		isEarthActive = active;
		if (active) {
			cubeFaceUp.material = skyUp_Mat;
			cubeFaceDown.material = skyDown_Mat;
			cubeFaceLeft.material = skyLeft_Mat;
			cubeFaceRight.material = skyRight_Mat;
			cubeFaceFront.material = skyFront_Mat;
			cubeFaceBack.material = skyBack_Mat;
			directionalLight.gameObject.transform.rotation = Quaternion.Euler (new Vector3 (12f, 330f, 1f));
			directionalLight.intensity = 3f;
			skyBox_Mat.SetFloat ("Exposure", 1.5f);
		} else {
			cubeFaceUp.material = cubeFaceUp_Mat;
			cubeFaceDown.material = cubeFaceDown_Mat;
			cubeFaceLeft.material = cubeFaceLeft_Mat;
			cubeFaceRight.material = cubeFaceRight_Mat;
			cubeFaceFront.material = cubeFaceFront_Mat;
			cubeFaceBack.material = cubeFaceBack_Mat;
			directionalLight.gameObject.transform.rotation = Quaternion.Euler (new Vector3 (12f, 330f, 1f));
			directionalLight.intensity = 1.5f;
			skyBox_Mat.SetFloat ("Exposure", 1f);
		}
	}

	/// <summary>
	/// Activates the alchemy symbols module
	/// </summary>
	public void ActivateAlchemy (bool active)
	{
		isAlchemyActive = active;
		if (active) {
			cubeFaceDown.material = symbolisms_Mat;
		} else {
			cubeFaceDown.material = cubeFaceDown_Mat;
		}
	}

	/// <summary>
	/// Plays the Fire and Ice interaction with Color Slider
	/// </summary>
	public void DanceFireAndIce (float speed, float sliderValue, Color32 col)
	{
		float gravityVal = 0f;
		if (sliderValue <= 512) {
			gravityVal = (sliderValue - 0) / (512);
		} else if (sliderValue > 512) {
			gravityVal = 1 - ((sliderValue - 512) / 512);
		}
		fireAndIceParticles.gravityModifier = gravityVal;
		fireAndIceParticles.startSpeed = speed;
		fireAndIceParticles.startColor = col;
	}

	/// <summary>
	/// Plays the Sound mixer interaction with Color Slider
	/// </summary>
	public void SoundMixer (float guitarVolume, float drumVolume, float synthVolume)
	{
		cubeFaceRight_Mat.color = new Color32 ((byte)guitarVolume, (byte)drumVolume, (byte)synthVolume, (byte)255);
		guitarVolume = guitarVolume / 255;
		drumVolume = drumVolume / 255;
		synthVolume = synthVolume / 255;
		guitarAudioSource.volume = guitarVolume;
		drumAudioSource.volume = drumVolume;
		synthAudioSource.volume = synthVolume;
	}

	/// <summary>
	/// Plays the Weather change interaction with Color Slider
	/// </summary>
	public void InvokeWeather (float lerpValue01, float lerpValue02, float particleSpeed, float particleSize, float particleGravity, Color32 particleColor, Vector3 particleRotation, Color32 lightColor)
	{

		cubeFaceUp.GetComponent<Renderer> ().material.SetFloat ("_lerp01", lerpValue01);
		cubeFaceUp.GetComponent<Renderer> ().material.SetFloat ("_lerp02", lerpValue02);
		weatherParticles.startSpeed = particleSpeed;
		weatherParticles.startSize = particleSize;
		weatherParticles.gravityModifier = particleGravity;
		weatherParticles.startColor = particleColor;
		weatherParticles.transform.localRotation = Quaternion.Euler (particleRotation);
		directionalLight.color = lightColor;
	}

	/// <summary>
	/// Plays the Shape shift interaction with Color Slider
	/// </summary>
	public void ShapeShift (float shiftVar)
	{
		List<Vector3> tempVerts = new List<Vector3> (4);
		tempVerts = planeData;
		if (shiftVar <= 0.5f) {
			float tempVal = shiftVar / 0.5f;
			float tempVertX = -5 + ((2.5f * tempVal) / 0.5f);
			float tempVertZ = 5 - ((2.5f * tempVal) / 0.5f);
			tempVerts [2] = new Vector3 (tempVertX, 0, tempVertZ);
			plane.mesh.SetVertices (tempVerts);

		} else {
			float tempVal = (shiftVar - 0.5f) / 0.5f;
			float tempVertX2 = -tempVal;
			float tempVertZ2 = tempVal;
			float tempVertX3 = Mathf.Abs (-5 + ((3f * tempVal) / 0.5f));
			float tempVertZ3 = -tempVertX3;
			tempVerts [2] = new Vector3 (tempVertX2, 0, tempVertZ2);
			tempVerts [3] = new Vector3 (tempVertX3, 0, tempVertZ3);
			plane.mesh.SetVertices (tempVerts);

		}
	}

	/// <summary>
	/// Plays the Earth in space interaction with Color Slider
	/// </summary>
	public void EarthCube (float sliderVal)
	{
		float rotY = 330f - ((90 * sliderVal) / 0.5f);
		directionalLight.transform.rotation = Quaternion.Euler (new Vector3 (directionalLight.transform.rotation.eulerAngles.x, rotY, directionalLight.transform.rotation.eulerAngles.z));
	}

	/// <summary>
	/// Plays the Alchemy Symbols interaction with Color Slider
	/// </summary>
	public void Alchemist (float sliderVal)
	{
		if (sliderVal < 256) {
			float tempVal = (sliderVal) / 256;
			cubeFaceDown.material.SetFloat ("_lerp01", tempVal);
			cubeFaceDown.material.SetFloat ("_lerp02", 0);
			cubeFaceDown.material.SetFloat ("_lerp03", 0);

		} else if (sliderVal >= 256 && sliderVal < 512) {
			float tempVal02 = (sliderVal - 256) / 256;
			cubeFaceDown.material.SetFloat ("_lerp01", 1 - tempVal02);
			cubeFaceDown.material.SetFloat ("_lerp02", 0);
			cubeFaceDown.material.SetFloat ("_lerp03", tempVal02 * 0.5f);

		} else if (sliderVal >= 512 && sliderVal < 768) {
			float tempVal03 = (sliderVal - 512) / 256;
			cubeFaceDown.material.SetFloat ("_lerp01", 0);
			cubeFaceDown.material.SetFloat ("_lerp02", tempVal03);
			cubeFaceDown.material.SetFloat ("_lerp03", 0.5f - tempVal03);

		} else if (sliderVal >= 768) {
			float tempVal04 = (sliderVal - 768) / 256;
			cubeFaceDown.material.SetFloat ("_lerp01", 0);
			cubeFaceDown.material.SetFloat ("_lerp02", 1);
			cubeFaceDown.material.SetFloat ("_lerp03", tempVal04);

		}
	}
}