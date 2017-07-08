// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/OutLine2" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_OutLineColor ("OutLine Color", Color) = (0,0,0,0)
		_OutLineWidth ("OutLine Width", Range(0, 0.2)) = 0.1
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {

			// ---- forward rendering base pass:
	Pass {
		Cull Front
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#include "UnityCG.cginc"

		float4 _OutLineColor;
		float _OutLineWidth;

		struct v2f{
		  float4 pos:SV_POSITION;
		}; 

		v2f vert(appdata_base v){
			v2f o;
			o.pos=v.vertex;
			o.pos.xyz+=v.normal*_OutLineWidth;
			o.pos=UnityObjectToClipPos(o.pos);
			return o;
		}

		float4 frag(v2f i):COLOR{
			return _OutLineColor;
		}
		ENDCG
	}

		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
