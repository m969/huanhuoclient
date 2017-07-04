Shader "Custom/SnowShader" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		//法线贴图
		_Bump("Bump", 2D) = "bump" {}
		//表示覆盖在岩石上雪的数量， 范围从0~1
		_Snow("Snow Level", Range(0, 1)) = 0.1
		//积雪的颜色
		_SnowColor("Snow Color", Color) = (1, 1, 1, 1)
		//积雪的方向
		_SnowDirection("Snow Direction", Vector) = (0, 1, 0)
		//积雪的厚度
		_SnowDepth("Snow Depth", Range(0, 0.3))=0.1
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Lambert

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _Bump;
		float _Snow;
		float4 _SnowColor;
		float4 _SnowDirection;
		float _SnowDepth;

		struct Input {
			float2 uv_MainTex;
			float2 uv_Bump;
			float3 worldNormal;
			INTERNAL_DATA
		};

		void surf (Input IN, inout SurfaceOutput o) 
		{
			//该像素的真实颜色值
			half4 c = tex2D(_MainTex, IN.uv_MainTex);
			//从凹凸贴图中得到该像素的法向量
			o.Normal = UnpackNormal(tex2D(_Bump, IN.uv_Bump));
			//得到世界坐标系下的真正法向量（而非凹凸贴图产生的法向量，要做一个切空间到世界坐标系的转换）和雪落下相反方向的点乘结果，即两者余弦值，并和_Snow（积雪程度）比较
			if(dot(IN.worldNormal, _SnowDirection.xyz)>lerp(1,-1,_Snow))
			{
				//此处我们可以看出_Snow参数只是一个插值项，当上述夹角余弦值大于  
				//lerp(1,-1,_Snow)=1-2*_Snow时，即表示此处积雪覆盖，所以此值越大，  
				//积雪程度程度越大。此时给覆盖积雪的区域填充雪的颜色  
				o.Albedo = _SnowColor.rgb;
			}
			else
			{
				//否则使用物体原先颜色，表示未覆盖积雪   
				o.Albedo = _SnowColor.rgb;
			}
			o.Alpha = 1;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
