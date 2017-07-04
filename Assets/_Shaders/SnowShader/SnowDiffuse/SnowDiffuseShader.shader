Shader "Custom/Snow Diffuse" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_SnowStep("Snow", Range(0,1))=0.5
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Lambert vertex:vert noforwardadd noshadow

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		fixed _SnowStep;

		struct Input {
			float2 uv_MainTex;
			fixed3 customColor;
			fixed4 vertexColor:COLOR;
		};

		void vert(inout appdata_full v, out Input o)
		{
			UNITY_INITIALIZE_OUTPUT(Input, o);
			o.customColor = step(1-_SnowStep,v.normal.y)*fixed3(1,1,1);
		}

		void surf (Input IN, inout SurfaceOutput o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb * IN.vertexColor + IN.customColor;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
