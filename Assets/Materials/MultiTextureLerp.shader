Shader "Custom/MultiTextureLerp" {
	Properties {
		_FirstTex ("First Texture", 2D) = ""{}
		_lerp01("First - Second Lerp", Range(0,1)) = 0
		_SecondTex ("Second Texture", 2D) = ""{}
		_lerp02("Second - Third Lerp", Range(0,1)) = 0
		_ThirdTex ("Third Texture", 2D) = ""{}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Lambert

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		
		sampler2D _FirstTex;
		sampler2D _SecondTex;
		sampler2D _ThirdTex;
		float _lerp01;
		float _lerp02;
		
		struct Input {
			float2 uv_FirstTex;
			float2 uv_SecondTex;
			float2 uv_ThirdTex;
		};


	void surf (Input IN, inout SurfaceOutput o) {
		
		float4 tex01_Data = tex2D(_FirstTex, IN.uv_FirstTex);
		float4 tex02_Data = tex2D(_SecondTex, IN.uv_SecondTex);
		float4 tex03_Data = tex2D(_ThirdTex, IN.uv_ThirdTex);
		float4 combineTex01; 
		//combineTex01 = tex01_Data;
		combineTex01 = lerp(tex01_Data, tex02_Data, _lerp01);
		combineTex01 = lerp(combineTex01, tex03_Data, _lerp02);
		combineTex01.a = 1.0;
		o.Albedo = combineTex01.rgb;
		o.Alpha = combineTex01.a;

	}
ENDCG
} 
FallBack "Diffuse"
}

